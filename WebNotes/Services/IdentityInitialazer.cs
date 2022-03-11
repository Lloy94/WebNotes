using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.Models.Identity;

namespace WebNotes.Services
{
    public class IdentityInitialazer
    {
        private readonly WebNoteDbContext _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;

        public IdentityInitialazer(
             WebNoteDbContext db,
             UserManager<User> UserManager,
             RoleManager<Role> RoleManager)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }

        public async Task InitializeAsync()
        {

            try
            {
                await InitializeIdentityAsync();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        
        private async Task InitializeIdentityAsync()
        {
    
            async Task CheckRole(string RoleName)
                 {
                    if (await _RoleManager.RoleExistsAsync(RoleName))
                    return;
                     else
                    {
                    await _RoleManager.CreateAsync(new Role { Name = RoleName });
                     }
                 }

            await CheckRole(Role.Users);
    
   
        }
    }
} 