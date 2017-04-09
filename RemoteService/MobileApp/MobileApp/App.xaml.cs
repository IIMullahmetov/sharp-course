using Android.Content;
using MobileApp.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MobileApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            var mainPage = new MainPage();
            MainPage = mainPage;
        }

        protected override void OnSleep ()
		{
            // Handle when your app sleeps
        }

		protected override void OnResume ()
		{
            // Handle when your app resumes
        }
	}
}
