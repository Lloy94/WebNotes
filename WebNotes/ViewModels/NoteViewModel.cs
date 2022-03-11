using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebNotes.ViewModels
{
    public class NoteViewModel
    {
        [Required]
        [Display(Name = "Содержание записки")]
        public string NoteInfo { get; set; }

    }
}
