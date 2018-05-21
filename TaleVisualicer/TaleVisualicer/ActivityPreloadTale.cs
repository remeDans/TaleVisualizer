using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Android;
using static Android.Views.ViewGroup;

namespace TaleVisualicer
{

    [Activity(Label = "ActivityPreloadTale", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
ScreenOrientation = ScreenOrientation.Landscape, Theme = "@style/MyTheme.Base")]
    public class ActivityPreloadTale : Activity
    {
        Button btnTales;
        Button btnDelete;
        List<string> items;

        string pathTale;
        string nameArchiveClick;
        Boolean isDelete;
        Boolean clickDelete;

        protected override void OnResume()
        {
            base.OnResume();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SIN barra de navvegación y title bar
            /*int uiOptions = (int)SystemUiFlags.HideNavigation | (int)SystemUiFlags.ImmersiveSticky;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            Window.DecorView.SystemUiVisibilityChange += visibilityListener;*/

            pathTale = "";
         
            items = JsonConvert.DeserializeObject<List<string>>(Intent.GetStringExtra("dir"));

            if (items.Count() != 0)
            {
                var layout = new LinearLayout(this);
                layout.Orientation = Orientation.Vertical;
                layout.SetGravity(GravityFlags.Center);

                
                

                for (int i = 0; i < items.Count; i++)
                {
                    var layoutH = new LinearLayout(this);
                    layoutH.SetGravity(GravityFlags.Center);
                    layoutH.SetBackgroundColor(Color.ParseColor("#fffff0"));
                    

                    btnTales = new Button(this);
                    string nameArchiveAbsolutePath = items[i];
                    nameArchiveClick = UtilsAndroid.ChangeToRelativePath(nameArchiveAbsolutePath);
                    btnTales.Text = nameArchiveClick;
                    btnTales.SetPadding(20, 20, 20, 20);
                    btnTales.SetTextSize(Android.Util.ComplexUnitType.Sp, 18);
                    btnTales.SetTypeface(null, TypefaceStyle.Bold);
                    btnTales.SetTextColor(Color.ParseColor("#000000"));
                    btnTales.Id = i;
                    
                    

                    btnDelete = new Button(this);
                    btnDelete.Text = "Eliminar";
                    btnDelete.SetPadding(20, 20, 20, 20);
                    btnDelete.SetTextSize(Android.Util.ComplexUnitType.Sp, 18);
                    btnDelete.SetTypeface(null, TypefaceStyle.Bold);
                    btnTales.SetTextColor(Color.ParseColor("#000000"));
                    btnDelete.Id = i;

                    layoutH.AddView(btnTales);
                    layoutH.AddView(btnDelete);
                    layout.AddView(layoutH);
                    

                    btnTales.Click += BtnTales_Click;
                    btnDelete.Click += BtnDelete_Click;
                }

                SetContentView(layout);
            }
            else
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("No hay cuentos precargados");
                alert.Show();

                SetContentView(Resource.Layout.Main);
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
        }

        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int id = ((Button)sender).Id;
            Button btn =FindViewById<Button>(id);
            string name = btn.Text;

            Directory.Delete(this.ApplicationInfo.DataDir + "/files" + "/VS" + "/" + name, true);
            string[] dir = Directory.GetDirectories(this.ApplicationInfo.DataDir + "/files" + "/VS");

            var intent = new Intent(this, typeof(ActivityPreloadTale));
            intent.PutExtra("dir", JsonConvert.SerializeObject(dir));
            StartActivity(intent);
        }

        private void BtnTales_Click(object sender, EventArgs e)
        {
            string nameArchiveAbsolutePath = ((Button)sender).Text;
            string nameArchive = UtilsAndroid.ChangeToRelativePath(nameArchiveAbsolutePath);

            //pathTale = Android.OS.Environment.ExternalStorageDirectory + "/Download" + "/VS" + "/" + nameArchive;
            pathTale = this.ApplicationInfo.DataDir + "/files" + "/VS" + "/" + nameArchive;

            var intent = new Intent(this, typeof(ActivityTale));
            intent.PutExtra("path", JsonConvert.SerializeObject(pathTale));
            StartActivity(intent);
        }

        #region opciones_inicio
        /*private void visibilityListener(object sender, Android.Views.View.SystemUiVisibilityChangeEventArgs e)
        {
            var newUiOptions = (int)e.Visibility;
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
    }
}