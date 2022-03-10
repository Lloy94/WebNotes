using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotes.Models
{
    public class Note
    {
        public IdentityUser User;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string NoteInfo { get; set; }

        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
    }
}
