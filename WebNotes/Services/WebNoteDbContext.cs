using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Models;
using WebNotes.Models.Identity;

namespace WebNotes.Services
{
    public class WebNoteDbContext : DbContext
    { 
    
        public WebNoteDbContext(DbContextOptions<WebNoteDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

    }
}
