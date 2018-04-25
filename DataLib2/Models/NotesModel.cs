using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLib2.Models
{
    public class NotesModel
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Item { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsStarred { get; set; }
        public DateTime CachedDate { get; set; }
    }
}
