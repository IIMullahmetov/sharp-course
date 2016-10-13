using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WPF_MVVM.Models
{
    [DataContract]
    public class Project
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<Note> Goals { get; set; } = new List<Note>();

        [DataMember]
        public List<Note> Active { get; set; } = new List<Note>();

        [DataMember]
        public List<Note> Done { get; set; } = new List<Note>();

        [DataMember]
        public List<Note> Canceled { get; set; } = new List<Note>();

        public Project() { }

        public Project(string name, string desc)
        {
            Name = name;
            Description = desc;
        }
    }
}
