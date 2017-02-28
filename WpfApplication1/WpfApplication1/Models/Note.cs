using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Models
{
    public class Note
    {
        [Key]
        public int ID { get; set; }

        public string NoteCategory { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public Note() { }

        public Note(string nc, string name, string desc, DateTime st, DateTime et)
        {
            NoteCategory = nc;
            Name = name;
            Description = desc;
            StartTime = st;
            EndTime = et;
        }
    }
}
