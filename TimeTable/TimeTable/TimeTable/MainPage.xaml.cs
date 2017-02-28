using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TimeTable
{
	public partial class MainPage : TabbedPage
    {
        object refresh;
        object[] timetable;

		public MainPage ()
		{
			InitializeComponent ();

            if (CheckRefresh())
                Connection();
		}

        private bool CheckRefresh() //нужно ли обновить расписание
        {
            if (App.Current.Properties.TryGetValue("refresh", out refresh)) //проверка на обновление при открытии приложения
            {
                if ((bool)refresh)
                    return true;
            }
            else
            {
                return true;
            }
            return false;
        }

        private void Connection() //подключение к интернету
        {
            Connection connection = new Connection();

            while (!connection.CheckConnection())
            {
                Message message = new Message(false, this, "Отсутствует подключение к интернету", "Попробовать подключиться снова?", null, "Да", "Нет");
                if (!message.getResult())
                    break;
            }
        }

        private void Save() //сохранение расписания на устройстве
        {
            App.Current.Properties.Add("timetable", timetable);
        }
    }
}
