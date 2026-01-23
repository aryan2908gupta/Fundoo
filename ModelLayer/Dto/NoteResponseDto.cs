using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Dto
{
    public class NoteResponseDto
    {
        public int NotesId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Colour { get; set; } = "#FFFFFF";
        public DateTime? Reminder { get; set; }

        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }
    }
}
