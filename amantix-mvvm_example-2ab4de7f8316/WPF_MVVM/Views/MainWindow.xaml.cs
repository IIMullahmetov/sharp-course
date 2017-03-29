using System;
using System.Collections.Generic;
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

namespace WPF_MVVM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        private void List_MouseEnter(object sender, MouseEventArgs e)
        {
            var enterAnimation = new ThicknessAnimation();
            enterAnimation.From = doneList.Margin;
            enterAnimation.To = new Thickness(5);
            enterAnimation.Duration = TimeSpan.FromSeconds(1);
            doneList.BeginAnimation(MarginProperty, enterAnimation);
        }

        
        private void List_MouseLeave(object sender, MouseEventArgs e)
        {
            var enterAnimation = new ThicknessAnimation();
            enterAnimation.From = doneList.Margin;
            enterAnimation.To = new Thickness(5);
            enterAnimation.Duration = TimeSpan.FromSeconds(1);
            doneList.BeginAnimation(MarginProperty, enterAnimation);
        }
        
        private void button_AddNote_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_RemoveNote_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
