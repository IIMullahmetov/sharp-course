using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_MVVM.Context;
using WPF_MVVM.Models;
using WPF_MVVM.ViewModels;

namespace WPF_MVVM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskDbContext _context = new TaskDbContext();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //_context.Notes.Load();
            
        }

        private void List_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                textBox1.Text = item.DataContext.ToString(); //?????
                textBox2.Text = "Yeee";
            }
        }

        //private void List_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    TextBox tb = (TextBox)sender;
        //    DragDrop.DoDragDrop(tb, tb.Text, DragDropEffects.Move);
        //}

        private void List_MouseEnter(object sender, MouseEventArgs e)
        {
            /*
            DoubleAnimation borderAnimation = new DoubleAnimation();
            borderAnimation.From = ;
            borderAnimation.To = 100;
            borderAnimation.Duration = new Duration(new TimeSpan(0, 0, 5));
            borderAnimation.FillBehavior = FillBehavior.HoldEnd;
            borderAnimation.BeginAnimation(Border.HeightProperty, borderAnimation);
            */
            /*
            var enterAnimation = new ThicknessAnimation();
            enterAnimation.From = DoneList.Margin;
            enterAnimation.To = new Thickness(-5);
            enterAnimation.Duration = TimeSpan.FromSeconds(0.5);
            DoneList.BeginAnimation(MarginProperty, enterAnimation);
            */
        }

        private void List_MouseLeave(object sender, MouseEventArgs e)
        {
            var enterAnimation = new ThicknessAnimation();
            enterAnimation.From = DoneList.Margin;
            enterAnimation.To = new Thickness(0);
            enterAnimation.Duration = TimeSpan.FromSeconds(0.5);
            DoneList.BeginAnimation(MarginProperty, enterAnimation);
        }


        private void List_Drop(object sender, DragEventArgs e)
        {

        }

        private void button_AddNote_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_RemoveNote_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
