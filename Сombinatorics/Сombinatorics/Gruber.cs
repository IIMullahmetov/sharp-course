using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace EntityGruber
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Cnum { get; set; }
        public string Cname { get; set; }
        public string City { get; set; }
        public int Rating { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    [Table("Salespeople")]
    public class Salesperson
    {
        [Key]
        public int Snum { get; set; }
        public string Sname { get; set; }
        public string City { get; set; }
        public decimal Comm { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Onum { get; set; }
        public DateTime Odate { get; set; }
        public decimal Amt { get; set; }

        public int Snum { get; set; }
        public virtual Salesperson Salesperson { get; set; }

        public int Cnum { get; set; }
        public virtual Customer Customer { get; set; }
    }

    public class GruberContextInitializer : DropCreateDatabaseAlways<GruberContext>
    {
        protected override void Seed(GruberContext context)
        {
            /*
            var s1 = new Salesperson { Sname = "Peel", City = "London", Comm = 0.12M };
            var s2 = new Salesperson { Sname = "Serres", City = "San Jose", Comm = 0.13M };
            var s3 = new Salesperson { Sname = "Motika", City = "London", Comm = 0.11M };
            var s4 = new Salesperson { Sname = "Rifkin", City = "Barcelona", Comm = 0.15M };
            var s5 = new Salesperson { Sname = "Axelrod", City = "New York", Comm = 0.10M };
            context.Salespeople.Add(s1);
            context.Salespeople.Add(s2);
            context.Salespeople.Add(s3);
            context.Salespeople.Add(s4);
            context.Salespeople.Add(s5);
            var c1 = new Customer { Cname = "Hoffman", City = "London", Rating = 100 };
            var c2 = new Customer { Cname = "Giovanni", City = "Rome", Rating = 200 };
            var c3 = new Customer { Cname = "Liu", City = "San Jose", Rating = 200 };
            var c4 = new Customer { Cname = "Grass", City = "Berlin", Rating = 300 };
            var c5 = new Customer { Cname = "Clemens", City = "London", Rating = 100 };
            var c6 = new Customer { Cname = "Cisneros", City = "San Jose", Rating = 300 };
            var c7 = new Customer { Cname = "Pereira", City = "Rome", Rating = 100 };
            context.Customers.Add(c1);
            context.Customers.Add(c2);
            context.Customers.Add(c3);
            context.Customers.Add(c4);
            context.Customers.Add(c5);
            context.Customers.Add(c6);
            context.Customers.Add(c7);

            var o = new List<Order>()
{
new Order {Amt = 18.69M, Customer = c1, Salesperson = s1, Odate = new DateTime(2015, 10, 3)},
new Order {Amt = 767.19M, Customer = c2, Salesperson = s1, Odate = new DateTime(2015, 10, 3)},
new Order {Amt = 1900.10M, Customer = c3, Salesperson = s2, Odate = new DateTime(2015, 10, 3)},
new Order {Amt = 5160.45M, Customer = c2, Salesperson = s3, Odate = new DateTime(2015, 10, 3)},
new Order {Amt = 1098.16M, Customer = c2, Salesperson = s3, Odate = new DateTime(2015, 10, 3)},
new Order {Amt = 1713.23M, Customer = c2, Salesperson = s3, Odate = new DateTime(2015, 10, 4)},
new Order {Amt = 75.75M, Customer = c2, Salesperson = s3, Odate = new DateTime(2015, 10, 4)},
new Order {Amt = 4723.00M, Customer = c2, Salesperson = s3, Odate = new DateTime(2015, 10, 5)},
new Order {Amt = 1309.95M, Customer = c2, Salesperson = s3, Odate = new DateTime(2015, 10, 6)},
new Order {Amt = 9891.88M, Customer = c2, Salesperson = s3, Odate = new DateTime(2015, 10, 6)},

};

            //var s1 = new Salesperson { Sname = "John", City = "New York", Comm = 0.2M };
            //var s2 = new Salesperson { Sname = "James", City = "New York", Comm = 0.15M };
            //var s3 = new Salesperson { Sname = "Mary", City = "London", Comm = 0.2M };
            //context.Salespeople.Add(s1);
            //context.Salespeople.Add(s2);
            //context.Salespeople.Add(s3);

            //var c1 = new Customer { Cname = "Peter", City = "Chicago", Rating = 100 };
            //var c2 = new Customer { Cname = "Jane", City = "Boston", Rating = 200 };
            //var c3 = new Customer { Cname = "Kate", City = "Liverpool", Rating = 300 };
            //context.Customers.Add(c1);
            //context.Customers.Add(c2);
            //context.Customers.Add(c3);

            //var o = new List<Order>()
            //{
            //    new Order {Amt = 1000, Customer = c1, Salesperson = s1, Odate = new DateTime(2016, 11, 1)},
            //    new Order {Amt = 2500, Customer = c2, Salesperson = s1, Odate = new DateTime(2016, 11, 1)},
            //    new Order {Amt = 500, Customer = c3, Salesperson = s2, Odate = new DateTime(2016, 11, 5)},
            //    new Order {Amt = 1200, Customer = c2, Salesperson = s3, Odate = new DateTime(2016, 11, 5)},
            //};
            //context.Orders.AddRange(o);*/
            context.SaveChanges();

        }
    }

    public class GruberContext : DbContext
    {
        static GruberContext()
        {
            Database.SetInitializer<GruberContext>(new GruberContextInitializer());
        }
        public GruberContext()
            : base("EntityGruber")
        { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Salesperson> Salespeople { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}