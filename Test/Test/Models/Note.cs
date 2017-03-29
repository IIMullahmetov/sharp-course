using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Test.Models
{
    class Note
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan WorkTime { get; set; }

        public Note(string name, string description)
        {
            Name = name;
            Description = description;
        } 
    }

    class Project
    {
        List<Note> Goals { get; set; } = new List<Note>();
        List<Note> Active { get; set; } = new List<Note>();
        List<Note> Done { get; set; } = new List<Note>();
        List<Note> Canceled { get; set; } = new List<Note>();
    }
}
