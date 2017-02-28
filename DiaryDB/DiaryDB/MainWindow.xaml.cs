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

using DiaryDB.dbDataSetTableAdapters;

namespace DiaryDB
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DiaryDB.dbDataSet dbDataSet = ((DiaryDB.dbDataSet)(this.FindResource("dbDataSet")));
            // Загрузить данные в таблицу diary. Можно изменить этот код как требуется.
            DiaryDB.dbDataSetTableAdapters.diaryTableAdapter dbDataSetdiaryTableAdapter = new DiaryDB.dbDataSetTableAdapters.diaryTableAdapter();
            dbDataSetdiaryTableAdapter.Fill(dbDataSet.diary);
            System.Windows.Data.CollectionViewSource diaryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("diaryViewSource")));
            diaryViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу events. Можно изменить этот код как требуется.
            DiaryDB.dbDataSetTableAdapters.eventsTableAdapter dbDataSeteventsTableAdapter = new DiaryDB.dbDataSetTableAdapters.eventsTableAdapter();
            dbDataSeteventsTableAdapter.Fill(dbDataSet.events);
            System.Windows.Data.CollectionViewSource eventsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("eventsViewSource")));
            eventsViewSource.View.MoveCurrentToFirst();
            // Загрузить данные в таблицу places. Можно изменить этот код как требуется.
            DiaryDB.dbDataSetTableAdapters.placesTableAdapter dbDataSetplacesTableAdapter = new DiaryDB.dbDataSetTableAdapters.placesTableAdapter();
            dbDataSetplacesTableAdapter.Fill(dbDataSet.places);
            System.Windows.Data.CollectionViewSource placesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("placesViewSource")));
            placesViewSource.View.MoveCurrentToFirst();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Сохранение данных
            this.dbDataSetdiaryTableAdapter.Update(this.dbDataSet.diary);
            //Обновление данных из источника
            this.dbDataSetdiaryTableAdapter.Fill(this.dbDataSet.diary);
            //Обновление состояния навигатора
            //this.diaryDataGridView_CurrentCellChanged(diaryDataGridView, e);
        }
    }
}
