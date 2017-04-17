using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MobileApp.Droid
{
    [Activity(Label = "MobileApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new MobileApp.App());
        }
/*
        protected override void OnPause()
        {
            StartActivity(typeof(SleepActivity));
        }
*/
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

        public void GoToActivity()
        {
            StartActivity(typeof(SleepActivity));
        }
    }
}

