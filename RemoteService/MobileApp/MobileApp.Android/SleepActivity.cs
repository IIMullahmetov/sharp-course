using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MobileApp.Droid
{
    [Activity(Label = "MobileApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false)]
    class SleepActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.VolumeDown)
            {
                return true;
            }

            if (keyCode == Keycode.VolumeUp)
            {
                return true;
            }
            return base.OnKeyUp(keyCode, e);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.VolumeDown)
            {
                MainPage.Request(-1);
                return true;
            }

            if (keyCode == Keycode.VolumeUp)
            {
                MainPage.Request(-2);
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }
    }
}