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
    public class Note
    {
        [DataMember]
        public string Name { get; set;}

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public TimeSpan WorkTime { get; set; }

        public Note() { }

        public Note(string name, string desc)
        {
            Name = name;
            Description = desc;
        }
    }
}
