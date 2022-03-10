using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotes.Models.Identity
{
    public class Role : IdentityRole
    {
        public const string Users = "users";
    }
}
