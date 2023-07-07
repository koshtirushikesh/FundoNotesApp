using System;
using System.Collections.Generic;
using System.Text;

namespace CommanLayer
{
    public class NoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }
        public DateTime Reminder { get; set; }
        public bool ISArchive { get; set; }
        public bool IsPinned { get; set; }
        public bool IsTrash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
