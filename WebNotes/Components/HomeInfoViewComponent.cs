using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Services.InSql;
using WebNotes.ViewModels;

namespace WebNotes.Components
{
    public class HomeInfoViewComponent : ViewComponent
    {
        private static InSqlNoteData _NoteService;
        public HomeInfoViewComponent([FromServices] InSqlNoteData NoteService) { _NoteService = NoteService; }
        public IViewComponentResult Invoke()
        {
            var orders = _NoteService.GetUserNotes(User.Identity!.Name).Result.ToList();
            int count = orders.Count();
            if (User.Identity?.IsAuthenticated == true)
                return View("HomeInfo",count);
            else return View();
            
        }
    }
}
