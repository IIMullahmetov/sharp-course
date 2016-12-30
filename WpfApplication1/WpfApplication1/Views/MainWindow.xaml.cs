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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
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

        private void TodoTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Binding binding = new Binding();

            binding.ElementName = "TodoTasks"; // элемент-источник
            binding.Path = new PropertyPath("Text"); // свойство элемента-источника
            TextName.SetBinding(TextBlock.TextProperty, binding); // установка привязки для элемента-приемника
        }

        private void ActiveTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Binding binding = new Binding();

            binding.ElementName = "doneName"; // элемент-источник
            binding.Path = new PropertyPath("Text"); // свойство элемента-источника
            TextName.SetBinding(TextBlock.TextProperty, binding); // установка привязки для элемента-приемника
        }

        private void DoneTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Binding binding = new Binding();

            binding.ElementName = "doneName"; // элемент-источник
            binding.Path = new PropertyPath("Text"); // свойство элемента-источника
            TextName.SetBinding(TextBlock.TextProperty, binding); // установка привязки для элемента-приемника
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            StackPanel sp = (sender as StackPanel);
            string nr = (sp.Children[1] as TextBlock).Text;
            Console.WriteLine(nr);
        }
    }
}
