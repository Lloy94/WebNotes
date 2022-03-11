using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Models;
using WebNotes.Models.Identity;

namespace WebNotes.Services
{
    public class WebNoteDbContext : IdentityDbContext<User, Role, string>
    {

        public WebNoteDbContext(DbContextOptions<WebNoteDbContext> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<Note> Notes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                   => optionsBuilder
                   .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetDefaultTableName();
                if (currentTableName.Contains("<"))
                {
                    currentTableName = currentTableName.Split('<')[0];
                }
                modelBuilder.Entity(entity.Name).ToTable(Helper.ToUnderscoreCase(currentTableName));
            }
        }
    }
}
