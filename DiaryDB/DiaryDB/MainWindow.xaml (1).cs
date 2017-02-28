using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiaryDB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DiaryContext context = new DiaryContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource diaryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("diaryViewSource")));
            context.Diary.Load();
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            diaryViewSource.Source = context.Diary.Local;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            this.context.Dispose();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
            this.diaryDataGrid.Items.Refresh();
        }
    }
}
