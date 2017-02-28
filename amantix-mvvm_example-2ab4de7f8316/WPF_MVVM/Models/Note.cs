using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WPF_MVVM.Entities;

namespace WPF_MVVM.Models
{
    [DataContract]
    public class Note
    {
        [DataMember]
        public NoteCategory NoteCategory { get; set; }

        [DataMember]
        public string Name { get; set;}

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public TimeSpan WorkTime { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        public Note() { }

        public Note(NoteCategory nc, string name, string desc, TimeSpan wt, DateTime dt)
        {
            NoteCategory = nc;
            Name = name;
            Description = desc;
            WorkTime = wt;
            StartTime = dt;
        }
    }
}
