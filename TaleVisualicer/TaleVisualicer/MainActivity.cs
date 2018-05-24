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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);

            this.RequestedOrientation = ScreenOrientation.Landscape;


            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //SIN barra de navvegación y title bar
            /*int uiOptions = (int)SystemUiFlags.HideNavigation | (int)SystemUiFlags.ImmersiveSticky;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            Window.DecorView.SystemUiVisibilityChange += visibilityListener;*/


            //------- ADS ----------------
            mAdView = FindViewById<AdView>(Resource.Id.adView);
            //var adRequest = new AdRequest.Builder().Build();
            //MobileAds.Initialize(this, "ca-app-pub-1150356505181337/1058283349");
            AdRequest adRequest = new AdRequest.Builder()
                .AddTestDevice(AdRequest.DeviceIdEmulator)
                .Build();
            mAdView.LoadAd(adRequest);


            

            /*mInterstitialAd = new InterstitialAd(this);
            mInterstitialAd.AdUnitId = GetString(Resource.String.test_interstitial_ad_unit_id);

            mInterstitialAd.AdListener = new AdListener(this);*/
            //----------- END ADS ------------
            
            

            imgAdd = FindViewById<ImageButton>(Resource.Id.imgAddTales);
            imgPreloadTale = FindViewById<ImageButton>(Resource.Id.imgPreloadTale);

            imgAdd.Click += ImgAdd_Click;
            imgPreloadTale.Click += ImgPreloadTale_Click;
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
                    alert.SetTitle("No hay cuentos precargados");
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
                alert.SetTitle("No hay cuentos precargados");
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
                alert.SetTitle("No hay archivos con la extensión *.tale");
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

        #region opciones_inicio
        /*private void visibilityListener(object sender, Android.Views.View.SystemUiVisibilityChangeEventArgs e)
        {
            var newUiOptions = (int)e.Visibility;

            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            newUiOptions |= (int)SystemUiFlags.LayoutStable;
            newUiOptions |= (int)SystemUiFlags.LayoutHideNavigation;
            newUiOptions |= (int)SystemUiFlags.LayoutFullscreen;
            newUiOptions |= (int)SystemUiFlags.HideNavigation;
            newUiOptions |= (int)SystemUiFlags.Fullscreen;
            newUiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)newUiOptions;
        }
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            
            int uiOptions = (int)Window.DecorView.SystemUiVisibility;

            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            uiOptions |= (int)SystemUiFlags.LayoutStable;
            uiOptions |= (int)SystemUiFlags.LayoutHideNavigation;
            uiOptions |= (int)SystemUiFlags.LayoutFullscreen;
            uiOptions |= (int)SystemUiFlags.HideNavigation;
            uiOptions |= (int)SystemUiFlags.Fullscreen;
            uiOptions |= (int)SystemUiFlags.ImmersiveSticky;

            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
        }*/
        #endregion opciones_inicio

        //Desactivado el botón Back
        public override void OnBackPressed()
        {
            return;
        }

        #region ADS
        /*protected void RequestNewInterstitial()
        {
            var adRequest = new AdRequest.Builder().Build();
            mInterstitialAd.LoadAd(adRequest);
        }*/


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

           

            /*if (!mInterstitialAd.IsLoaded)
            {
                RequestNewInterstitial();
            }*/
        }



        protected override void OnRestart()
        {
            this.RequestedOrientation = ScreenOrientation.Landscape;
            base.OnRestart();
            this.RequestedOrientation = ScreenOrientation.Landscape;
        }


       /* protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {

            base.OnRestoreInstanceState(savedInstanceState);
            this.RequestedOrientation = ScreenOrientation.Landscape;

            
        }*/


        
        


        /*protected override void OnStart()
        {
            

            base.OnStart();
            this.RequestedOrientation = ScreenOrientation.Landscape;
        }*/

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

            public override void OnAdClosed()
            {
                //that.RequestNewInterstitial();
                //that.BeginSecondActivity();
            }
        }

        class OnClickListener : Java.Lang.Object, View.IOnClickListener
        {
            MainActivity that;

            public OnClickListener(MainActivity t)
            {
                that = t;
            }

            public void OnClick(View v)
            {
                /*if (that.mInterstitialAd.IsLoaded)
                {
                    that.mInterstitialAd.Show();
                }
                else
                {
                    //that.BeginSecondActivity();
                }*/
            }
        }
        #endregion ADS
    }

}

