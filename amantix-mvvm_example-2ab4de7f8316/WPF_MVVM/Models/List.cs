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
    public class List
    {
        [DataMember]
        public List<Project> Projects { get; set; } = new List<Project>();

    }
}
