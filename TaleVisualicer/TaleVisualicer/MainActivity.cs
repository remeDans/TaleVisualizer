using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Content.PM;
using System.IO;
using Newtonsoft.Json;
using Android.Content;
using Android.Support.V7.App;
using Android.Gms.Ads;
using TaleVisualicer;
using System;

namespace TaleVisualicer
{

    [Activity(Label = "ApplicationName",  ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
ScreenOrientation = ScreenOrientation.Landscape, Theme = "@style/MyTheme.Base", NoHistory = true) ]//
    public class MainActivity : AppCompatActivity
    {
        ImageButton imgAdd;
        ImageButton imgPreloadTale;

        protected InterstitialAd mInterstitialAd;
        protected AdView mAdView;

        public bool startApp;
        int countMainActivity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);

            countMainActivity = 0;

            this.RequestedOrientation = ScreenOrientation.Landscape;

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //------- ADS ----------------
            mAdView = FindViewById<AdView>(Resource.Id.adView);
            AdRequest adRequest = new AdRequest.Builder()
                .AddTestDevice(AdRequest.DeviceIdEmulator)
                .Build();
            mAdView.LoadAd(adRequest);
            //----------- END ADS ------------

            imgAdd = FindViewById<ImageButton>(Resource.Id.imgAddTales);
            imgPreloadTale = FindViewById<ImageButton>(Resource.Id.imgPreloadTale);

            imgAdd.Click += ImgAdd_Click;
            imgPreloadTale.Click += ImgPreloadTale_Click;


            //INTENTO DE HACER VARIABLES GLOBALES
            //--------------------------------------

            /*if(countMainActivity==0)
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Tutorial");
                alert.SetMessage("Los cuentos se encuentran en taleeditor.wordpress.com y se tienen que guardar en la carpeta 'Downloads' ");
                alert.Show();
            }*/

            //string stringFromApplicationClass = ((GlobalsVariable)this.Application).myString;//This will contain "Hello World"
            //((GlobalsVariable)this.Application).myString = "Changed from the activity";// now the value is set to a new value.

            /*Android.App.AlertDialog.Builder alert1 = new Android.App.AlertDialog.Builder(this);
            alert1.SetTitle(((GlobalsVariable)this.Application).myString);
            
            alert1.Show();

            countMainActivity++;*/
        }


        private void ImgPreloadTale_Click(object sender, System.EventArgs e)
        {
            //string documentsPath = Android.OS.Environment.ExternalStorageDirectory + "/Download" + "/VS";
            string documentsPath = this.ApplicationInfo.DataDir + "/files" + "/VS";

            if (Directory.Exists(documentsPath))
            {
                string[] dir =Directory.GetDirectories(documentsPath);

                if(dir.Length==0)
                {
                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                    alert.SetTitle("Advertencia");
                    alert.SetMessage("No hay cuentos precargados");
                    alert.Show();
                }
                else
                {
                    //Enviamos la información
                    var intent = new Intent(this, typeof(ActivityPreloadTale));
                    intent.PutExtra("dir", JsonConvert.SerializeObject(dir));
                    StartActivity(intent);
                }
            }
            else
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Advertencia");
                alert.SetMessage("No hay cuentos precargados");
                alert.Show();
            }
        }

        private void ImgAdd_Click(object sender, System.EventArgs e)
        {
            string documentsPath = Android.OS.Environment.ExternalStorageDirectory + "/Download";
            string[] files = null;

            files = Directory.GetFiles(documentsPath, "*.tale");
            int numFiles = files.Length;

            if (numFiles == 0)
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Advertencia");
                alert.SetMessage("No hay archivos con la extensión *.tale");
                alert.Show();
            }
            else
            {
                //Enviamos la información
                var intent = new Intent(this, typeof(ActivityListTale));
                intent.PutExtra("files", JsonConvert.SerializeObject(files));

                StartActivity(intent);
            }
        }


        //Desactivado el botón Back
        public override void OnBackPressed()
        {
            return;
        }

        #region ADS

        protected override void OnPause()
        {
            if (mAdView != null)
            {
                mAdView.Pause();
            }
            base.OnPause();
        }

        protected override void OnResume()
        {
            this.RequestedOrientation = ScreenOrientation.Landscape;
            base.OnResume();
            this.RequestedOrientation = ScreenOrientation.Landscape;
            if (mAdView != null)
            {
                mAdView.Resume();
            }
        }


        protected override void OnRestart()
        {
            this.RequestedOrientation = ScreenOrientation.Landscape;
            base.OnRestart();
            this.RequestedOrientation = ScreenOrientation.Landscape;
        }


        protected override void OnDestroy()
        {
            if (mAdView != null)
            {
                mAdView.Destroy();
            }
            base.OnDestroy();
        }


        class AdListener : Android.Gms.Ads.AdListener
        {
            MainActivity that;

            public AdListener(MainActivity t)
            {
                that = t;
            }
        }

        class OnClickListener : Java.Lang.Object
        {
            MainActivity that;

            public OnClickListener(MainActivity t)
            {
                that = t;
            }

        }
        #endregion ADS
    }

}

