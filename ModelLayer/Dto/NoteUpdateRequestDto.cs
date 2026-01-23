using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelLayer.Dto
{
    public class NoteUpdateRequestDto
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public string? Colour { get; set; }

        public DateTime? Reminder { get; set; }
    }
}
