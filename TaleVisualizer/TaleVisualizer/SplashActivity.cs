﻿using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace TaleVisualizer
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
ScreenOrientation = ScreenOrientation.Landscape, Icon = "@mipmap/icon")]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            this.RequestedOrientation = ScreenOrientation.Landscape;
            base.OnResume();
            this.RequestedOrientation = ScreenOrientation.Landscape;
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup ()
        {
            await Task.Delay(2000); // Simulate a bit of startup work.

            Boolean bandActivity = GetDefaultSharedPreferences.pref.GetBoolean("mostrarTutorial", true);
            if (bandActivity)
            {
                //No saved tutorial
                Intent intent = new Intent(this, typeof(TutorialActivity));
                this.StartActivity(intent);
            }

            else
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                this.StartActivity(intent);
            }

        }
    }
}