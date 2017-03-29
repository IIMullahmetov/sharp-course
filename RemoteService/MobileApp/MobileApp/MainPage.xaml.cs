﻿using System;
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
        ClientConnection cc = new ClientConnection();

        public MainPage()
		{
			InitializeComponent();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(cc.Connection), "10.10.0.1");
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
                OKButton.BackgroundColor = Color.Red;
            }
            else
            {
                DisplayAlert("Ошибка", "Введите IP", "ОK");
            }
        }
           
        public async void AsyncRequest(string message)
        {
            try
            {
                int code = await cc.Request(message);
                Console.WriteLine(code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
    }
}