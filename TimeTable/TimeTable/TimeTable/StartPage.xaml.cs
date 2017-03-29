using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TimeTable
{
    public partial class StartPage : ContentPage
    {
        ArrayList groups;

        public StartPage()
        {
            InitializeComponent();

            SetListGroups();
        }

        private void SetListGroups() //достаем список групп
        {
            groups = new ArrayList() { "11 - 505", "11 - 506", "11 - 507" };

            foreach (string item in groups) //добавляем элементы в Picker
            {
                GroupPicker.Items.Add(item);
            }
        }

        private void ConfirmClicked(object sender, EventArgs e) //обработчик кнопки
        {
            try
            {
                var group = GroupPicker.Items[GroupPicker.SelectedIndex]; //достаем номер группы
                App.Current.Properties.Add("group", group); //сохраняем номер группы

                ToMainPage();
            }
            catch (ArgumentOutOfRangeException)
            {
                new Message(true, this, "Ошибка", "Вы не выбрали группу", "OK", null, null);
            }
        }

        private async void ToMainPage()
        {
            await Navigation.PushAsync(new MainPage()); //открываем главное окно
            Navigation.RemovePage(this); //удаляем окно выбора группы из стека
        }
        /*
        private void Message(string title, string message, string answer) //уведомление
        {
            DisplayAlert(title, message, answer);
        }*/
    }
}

