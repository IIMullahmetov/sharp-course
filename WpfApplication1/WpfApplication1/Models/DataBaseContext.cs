using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using WpfApplication1.Models;

namespace WpfApplication1.Models
{
    public class DataBaseInitializer : DropCreateDatabaseAlways<DataBaseContext>//DropCreateDatabaseAlways<DataBaseContext>//CreateDatabaseIfNotExists<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            var notes = new List<Note>
            {
                new Note("Goals", "name1", "desc1", DateTime.Now, DateTime.Now),
                new Note("Goals", "name2", "desc2", DateTime.Now, DateTime.Now),
                new Note("Active", "name3", "desc3", DateTime.Now, DateTime.Now),
                new Note("Active", "name4", "desc4",DateTime.Now, DateTime.Now),
                new Note("Done", "name5", "desc5", DateTime.Now, DateTime.Now),
                new Note("Done", "name6", "desc6", DateTime.Now, DateTime.Now),
                new Note("Canceled", "name7", "desc7", DateTime.Now, DateTime.Now),
                new Note("Canceled", "name8", "desc8", DateTime.Now, DateTime.Now),
            };

            //context.Tasks.AddRange(tasks);
            notes.ForEach(s => context.Notes.Add(s));
            //context.SaveChanges();
        }
    }

    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("DataBaseContext")
        {
            Database.SetInitializer(new DataBaseInitializer());
        }

        public virtual DbSet<Note> Notes { get; set; }
    }
}
