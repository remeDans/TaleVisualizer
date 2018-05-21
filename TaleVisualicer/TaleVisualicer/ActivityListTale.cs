﻿using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;
using Android;


namespace TaleVisualicer
{
    [Activity(Label = "ListTale", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
    ScreenOrientation = ScreenOrientation.Landscape, Theme = "@style/MyTheme.Base")]
    public class ActivityListTale : Activity
    {
        Button btnTales;
        List<string> items;
        string pathTale;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //SIN barra de navvegación y title bar
           /* int uiOptions = (int)SystemUiFlags.HideNavigation | (int)SystemUiFlags.ImmersiveSticky;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            Window.DecorView.SystemUiVisibilityChange += visibilityListener;*/

            pathTale = "";

            items = JsonConvert.DeserializeObject<List<string>>(Intent.GetStringExtra("files"));

            var layout = new LinearLayout(this);

            for (int i = 0; i < items.Count; i++)
            {
                var layoutH = new FrameLayout(this);
                layout.SetGravity(GravityFlags.Center);
                layout.Orientation = Android.Widget.Orientation.Vertical;
                layout.SetBackgroundColor(Color.ParseColor("#fffff0"));

                btnTales = new Button(this);
                btnTales.Text = items[i];
                btnTales.SetPadding(20, 20, 20, 20);
                btnTales.SetTextSize(Android.Util.ComplexUnitType.Sp, 18);
                btnTales.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                btnTales.SetTextColor(Color.ParseColor("#000000"));

                layoutH.AddView(btnTales);
                layout.AddView(layoutH);

                btnTales.Click += BtnTales_Click;
            }

            SetContentView(layout);
        }

        private void BtnTales_Click(object sender, EventArgs e)
        {
            //ESTE FUNCIONA PERFECTAMENTE
            //loadTaleDownloadDownload(sender);

            //ESTA HECHO PERO NO CONSIGO PONERLO EN LA PUBLICA MAS QUE NADA XK NO SE LA RUTA
            loadTaleDownloadData(sender);
        }


        public void loadTaleDownloadData(Object sender)
        {
            string nameArchiveAbsolutePath = ((Button)sender).Text;
            string nameArchiveWithExtension = UtilsAndroid.ChangeToRelativePath(nameArchiveAbsolutePath);
            int tamNnameArchiveWithExtension = nameArchiveWithExtension.Length;
            string nameArchive = nameArchiveWithExtension.Substring(0, tamNnameArchiveWithExtension - 5);

            string documentsPath = this.ApplicationInfo.DataDir + "/files" + "/VS";

            if (!Directory.Exists(documentsPath))
            {
                Directory.CreateDirectory(documentsPath);
            }

            if (!Directory.Exists(documentsPath + "/" + nameArchive))
            {
                /*AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Cargando...");
                alert.Show();*/

                File.Copy(nameArchiveAbsolutePath, System.IO.Path.Combine(documentsPath, nameArchiveWithExtension));
           
                File.Copy(System.IO.Path.Combine(documentsPath, nameArchiveWithExtension), System.IO.Path.Combine(documentsPath, nameArchive + ".zip"));

                pathTale = documentsPath + "/" + nameArchive;

                if (!Directory.Exists(pathTale))
                {
                    Directory.CreateDirectory(pathTale);
                }

                FastZip zip = new FastZip();
                zip.ExtractZip(System.IO.Path.Combine(documentsPath, nameArchive + ".zip"), pathTale, null);

                File.Delete(System.IO.Path.Combine(documentsPath, nameArchiveWithExtension));
                File.Delete(pathTale + ".zip");

                var intent = new Intent(this, typeof(ActivityTale));
                intent.PutExtra("path", JsonConvert.SerializeObject(pathTale));
                StartActivity(intent);
            }
            else
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Este cuento está precargado");
                alert.Show();
            }
        }


        public void loadTaleDownloadDownload(Object sender)
        {
            string nameArchiveAbsolutePath = ((Button)sender).Text;
            string nameArchiveWithExtension = UtilsAndroid.ChangeToRelativePath(nameArchiveAbsolutePath);
            int tamNnameArchiveWithExtension = nameArchiveWithExtension.Length;
            string nameArchive = nameArchiveWithExtension.Substring(0, tamNnameArchiveWithExtension - 5);
            string documentsPath = Android.OS.Environment.ExternalStorageDirectory + "/Download" + "/VS";

            if (!Directory.Exists(documentsPath))
            {
                Directory.CreateDirectory(documentsPath);
            }

            if (!Directory.Exists(documentsPath + "/" + nameArchive))
            {
                /*AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Cargando...");
                alert.Show();*/

                File.Copy(nameArchiveAbsolutePath, System.IO.Path.Combine(documentsPath, nameArchiveWithExtension));
                File.Copy(System.IO.Path.Combine(documentsPath, nameArchiveWithExtension), System.IO.Path.Combine(documentsPath, nameArchive + ".zip"));

                pathTale = Android.OS.Environment.ExternalStorageDirectory + "/Download" + "/VS" + "/" + nameArchive;

                if (!Directory.Exists(pathTale))
                {
                    Directory.CreateDirectory(pathTale);
                }

                FastZip zip = new FastZip();
                zip.ExtractZip(System.IO.Path.Combine(documentsPath, nameArchive + ".zip"), pathTale, null);

                File.Delete(System.IO.Path.Combine(documentsPath, nameArchiveWithExtension));
                File.Delete(pathTale + ".zip");

                var intent = new Intent(this, typeof(ActivityTale));
                intent.PutExtra("path", JsonConvert.SerializeObject(pathTale));
                StartActivity(intent);
            }
            else
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Este cuento está precargado");
                alert.Show();
            }
        }


        public override void OnBackPressed()
        {
            base.OnBackPressed();
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