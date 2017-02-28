using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

            List list = new List();

            Project project = new Project("First Project", "Comment1");
            list.Projects.Add(project);

            Project project1 = new Project("Second Project", "Comment2");
            list.Projects.Add(project1);

            project.Goals.Add(new Note("First Name", "First Description....................kuhkuhkuhkuhhkuhkuhkuhkuhkuhkuhkuhkuhkuhkuhkuhkuhkuhkuhkuhkuhukuhkuh"));
            project.Goals.Add(new Note("Second Name", "Second Description"));

            project.Active.Add(new Note("Active 1", "It's active 1"));
            project.Active.Add(new Note("Active 2", "It's active 2"));

            project.Done.Add(new Note("Done 1", "It's done 1"));
            project.Done.Add(new Note("Done 2", "It's done 2"));

            project.Canceled.Add(new Note("Canceled 1", "It's canceled 1"));
            project.Canceled.Add(new Note("Canceled 2", "It's canceled 2"));


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
        
            
            var MainViewModel = new MainViewModel(project);
            view.DataContext = MainViewModel;
            
            view.Show();
        }
    }
}
