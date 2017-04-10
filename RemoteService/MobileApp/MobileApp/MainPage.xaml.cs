using Android.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp
{
	public partial class MainPage : ContentPage
	{
        static ClientConnection cc;

        public MainPage(int imageBufferLength, int codeBufferLength)
		{
			InitializeComponent();
            cc = new ClientConnection(imageBufferLength, codeBufferLength);
            AsyncConnection();
        }

        public async void AsyncConnection()
        {
            try
            {
                images.ItemsSource = await cc.Connection(IPEntry.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OKButton_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(IPEntry.Text)) {
                //ThreadPool.QueueUserWorkItem(new WaitCallback(cc.Connection), IPEntry.Text);
                AsyncConnection();
                OKButton.BackgroundColor = Color.Red;
            }
            else
            {
                DisplayAlert("Ошибка", "Введите IP", "ОK");
            }
        }
           
        public static async void AsyncRequest(string message)
        {
            int code = await cc.Request(message);
            //TODO: ЗДЕСЬ РЕАКЦИЯ НА ОТВЕТ
        }

        private void NextClicked(object sender, EventArgs e)
        {
            AsyncRequest("-1");
            NextButton.BackgroundColor = Color.Red;
        }

        private void PrevClicked(object sender, EventArgs e)
        {
            AsyncRequest("-2");
            PrevButton.BackgroundColor = Color.Red;
        }

        private void PlayClicked(object sender, EventArgs e)
        {
            AsyncRequest("-3");
            PlayButton.BackgroundColor = Color.Red;
        }

        private void ExitClicked(object sender, EventArgs e)
        {
            AsyncRequest("-4");
            ExitButton.BackgroundColor = Color.Red;
        }

        private void images_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var index = ((images.ItemsSource as List<ImageSource>).IndexOf(e.SelectedItem as ImageSource) + 1).ToString();
            AsyncRequest(index);
        }

        public static void Request(string message)
        {
            AsyncRequest(message);
        }
    }
}
