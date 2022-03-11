using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Models.Identity;
using WebNotes.Services.InSql;
using WebNotes.ViewModels;

namespace WebNotes.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly InSqlNoteData _NoteService;
        private readonly UserManager<User> _UserManager;

        public NoteController(InSqlNoteData NoteService, UserManager<User> UserManager) 
        {
            _UserManager = UserManager;
            _NoteService = NoteService;
        }
        public IActionResult Index() => View(new NoteViewModel());
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(NoteViewModel Model)
        {
            var user = await _UserManager.GetUserAsync(User);
            await _NoteService.CreateNote(user.UserName, Model);
            return RedirectToAction("Index", "Home");
        }
    }
}
