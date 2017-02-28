using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM
{
    class Gruber
    {
        [Table("Goals")]
        public class Goals
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
            public string Name { get; set; }
            public string Description { get; set; }
            public TimeSpan WorkTime { get; set; }
            public DateTime StartTime { get; set; }

            public int? Snum { get; set; }
            public virtual Salesperson Salesperson { get; set; }
            public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        }

        [Table("Salespeople")]
        public class Salesperson
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Snum { get; set; }
            public string Sname { get; set; }
            public string City { get; set; }
            public decimal Comm { get; set; }

            public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        }

        [Table("Orders")]
        public class Order
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Onum { get; set; }
            public DateTime Odate { get; set; }
            public decimal Amt { get; set; }

            public int Snum { get; set; }
            public virtual Salesperson Salesperson { get; set; }

            public int Cnum { get; set; }
            public virtual Customer Customer { get; set; }
        }
    }
}
