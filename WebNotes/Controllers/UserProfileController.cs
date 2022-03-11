using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Models;
using WebNotes.Services.InSql;
using WebNotes.ViewModels;

namespace WebNotes.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {

        public async Task<IActionResult> Index([FromServices] InSqlNoteData NoteService)
        {
            var orders = await NoteService.GetUserNotes(User.Identity!.Name);

            return View(orders.Select(order => new NoteViewModel
            {
                NoteInfo = order.NoteInfo,
                Date=order.Date

            })); ;
        }
    }
}