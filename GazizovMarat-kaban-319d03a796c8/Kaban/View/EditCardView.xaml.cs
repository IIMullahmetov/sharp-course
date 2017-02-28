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
using System.Windows.Shapes;
using Kaban.Model;
using Kaban.ViewModel;

namespace Kaban.View
{
    /// <summary>
    /// Логика взаимодействия для EditCardView.xaml
    /// </summary>
    public partial class EditCardView : Window
    {


        public EditCardView()
        {
            InitializeComponent();
        }
        public void SetDataContext(Card card)
        {
            DataContext = new CardViewModel(card);
        }

        public void SetDataContext(CardViewModel selectedCardViewModel)
        {
            DataContext = selectedCardViewModel;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
