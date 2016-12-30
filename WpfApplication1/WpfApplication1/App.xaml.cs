using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using WpfApplication1.Models;
using WpfApplication1.ViewModels;
using System.Linq;
using System.Data.Entity;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {

            DataBaseContext ctx = new DataBaseContext();
            ctx.Notes.Load();
            var data = ctx.Notes.Local;//(from Tasks in ctx.Tasks
                                       ////where Tasks.Status == Statuses.TODO
                                       //select Tasks).ToList();


            //select t;
            MainWindow view = new MainWindow(); // создали View
            MainViewModel viewModel = new MainViewModel(data, ctx);
            //MainViewModel viewModel = new MainViewModel(ctx.Tasks.ToList(), ctx); // Создали ViewModel

            view.DataContext = viewModel; // положили ViewModel во View в качестве DataContext
            view.Show();
        }
    }
}
