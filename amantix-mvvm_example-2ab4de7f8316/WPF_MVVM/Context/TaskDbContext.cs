using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using WPF_MVVM.Entities;
using WPF_MVVM.Models;
using WPF_MVVM.ViewModels;

namespace WPF_MVVM.Context
{
    public class DataBaseInitializer : DropCreateDatabaseAlways<TaskDbContext>
    {

        protected override void Seed(TaskDbContext context)
        {
            var notes = new List<Note>
            {
                new Note(NoteCategory.Goals, "name1", "desc1", TimeSpan.Zero, DateTime.Now),
                new Note(NoteCategory.Goals, "name2", "desc2", TimeSpan.Zero, DateTime.Now),
                new Note(NoteCategory.Active, "name3", "desc3", TimeSpan.Zero, DateTime.Now),
                new Note(NoteCategory.Active, "name4", "desc4", TimeSpan.Zero, DateTime.Now),
                new Note(NoteCategory.Done, "name5", "desc5", TimeSpan.Zero, DateTime.Now),
                new Note(NoteCategory.Done, "name6", "desc6", TimeSpan.Zero, DateTime.Now),
                new Note(NoteCategory.Canceled, "name7", "desc7", TimeSpan.Zero, DateTime.Now),
                new Note(NoteCategory.Canceled, "name8", "desc8", TimeSpan.Zero, DateTime.Now),
            };

            foreach (var note in notes)
                context.Notes.Add(note);

            context.SaveChanges();
            base.Seed(context);
        }
    }
    public class TaskDbContext : DbContext
    {

        public DbSet<Note> Notes { get; set; }
        public TaskDbContext() :
            base("TaskDbContex")
        {
            Database.SetInitializer(new DataBaseInitializer());
        }


    }
}