using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_MVVM.Commands;
using WPF_MVVM.Context;
using WPF_MVVM.Entities;
using WPF_MVVM.Models;
using WPF_MVVM.ViewModels;
using WPF_MVVM.Views;

namespace WPF_MVVM
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            //var view = new StartWindow();
            var view = new MainWindow();

            //List list = new List();

            //Project project = new Project("First Project", "Comment1");
            //list.Projects.Add(project);

            //Project project1 = new Project("Second Project", "Comment2");
            //list.Projects.Add(project1);

            //project.Goals.Add(new Note("First Name", "First Description....................kuhkuhkuhkuhhkuhkuhkuhkuhkuhkuh", TimeSpan.Zero, DateTime.Now));
            //project.Goals.Add(new Note("Second Name", "Second Description", TimeSpan.Zero, DateTime.Now));

            //project.Active.Add(new Note("Active 1", "It's active 1", TimeSpan.Zero, DateTime.Now));
            //project.Active.Add(new Note("Active 2", "It's active 2", TimeSpan.Zero, DateTime.Now));

            //project.Done.Add(new Note("Done 1", "It's done 1", TimeSpan.Zero, DateTime.Now));
            //project.Done.Add(new Note("Done 2", "It's done 2", TimeSpan.Zero, DateTime.Now));

            //project.Canceled.Add(new Note("Canceled 1", "It's canceled 1", TimeSpan.Zero, DateTime.Now));
            //project.Canceled.Add(new Note("Canceled 2", "It's canceled 2", TimeSpan.Zero, DateTime.Now));


            /*
            using (var sw = new StreamWriter("output.xml"))
            {
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(Project));
                xs.Serialize(sw, project.Goals);
            }

            /*
            using (var sr = new StreamReader("output.xml"))
            {
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(Project));
                project=(Project)xs.Deserialize(sr);
            }
            */

            //var StartViewModel = new StartViewModel(list);
            //view.DataContext = StartViewModel;

            //DataBaseInitializer dbi = new DataBaseInitializer();
            //dbi.Seed();
            
            
            
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

            foreach (var note in notes) {
                //.Add(note);
                //context.Notes.Add(note); 

            //context.SaveChanges();
            //base.Seed(context);
            }
            

            var MainViewModel = new MainViewModel();
            view.DataContext = MainViewModel;

            view.Show();

            //new Excel(list, project, false);
            //new Excel(list, project, true);
        }
    }
}
