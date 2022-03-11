using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebNotes.ValidationRules;

namespace WebNotes.ViewModels
{
    public class NoteViewModel
    {
        [NotNullOrWhiteSpaceValidator]
        [Display(Name = "Содержание записки")]
        public string NoteInfo { get; set; }

        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;

    }
}
