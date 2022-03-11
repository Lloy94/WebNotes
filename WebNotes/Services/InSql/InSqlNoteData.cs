using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Models;
using WebNotes.Models.Identity;
using WebNotes.ViewModels;

namespace WebNotes.Services.InSql
{
    public class InSqlNoteData
    {
        private readonly WebNoteDbContext _db;
        private readonly UserManager<User> _UserManager;


        public InSqlNoteData(WebNoteDbContext db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }

        public async Task<IEnumerable<Note>> GetUserNotes(string UserName)
        {
            var notes= await _db.Notes
               .Where(o => o.User.UserName == UserName)
               .ToArrayAsync()
               .ConfigureAwait(false);
            return notes;
        }

        public async Task<Note> GetNoteById(int id)
        {
            var note = await _db.Notes
               .Include(o => o.User)
               .FirstOrDefaultAsync(o => o.Id == id)
               .ConfigureAwait(false);
            return note;
        }

        public async Task<Note> CreateNote(string UserName,  NoteViewModel NoteModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName).ConfigureAwait(false);

            if (user is null)
                throw new InvalidOperationException($"Пользователь {UserName} не найден");

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var note = new Note
            {
                User = user,
                NoteInfo   = NoteModel.NoteInfo,
                Date = DateTimeOffset.UtcNow,
                
            };

            
            await _db.Notes.AddAsync(note);
            //await _db.Set<OrderItem>().AddRangeAsync(order.Items); // нет необходимости!

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return note;
        }
    }
}
