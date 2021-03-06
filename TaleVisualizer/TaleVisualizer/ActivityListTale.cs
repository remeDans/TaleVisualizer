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


namespace TaleVisualizer
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

            pathTale = "";

            items = JsonConvert.DeserializeObject<List<string>>(Intent.GetStringExtra("files"));


            if (items.Count >= 0)
            {
                if (items.Count <= 7)
                {
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
                else
                {
                    //Establecemos el layout main
                    SetContentView(Resource.Layout.listTale);

                    //Obtenemos el linear layout donde colocar los botones
                    LinearLayout llBotonera = FindViewById<LinearLayout>(Resource.Id.llBotonera);
                    llBotonera.SetBackgroundColor(Color.ParseColor("#fffff0"));

                    //Creamos las propiedades de layout que tendrán los botones.
                    //Son LinearLayout.LayoutParams porque los botones van a estar en un LinearLayout.
                    LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                    lp.SetMargins(5, 5, 5, 5);


                    for (int i = 0; i < items.Count; i++)
                    {
                        btnTales = new Button(this);
                        btnTales.Text = items[i];
                        btnTales.SetPadding(20, 20, 20, 20);
                        btnTales.SetTextSize(Android.Util.ComplexUnitType.Sp, 18);
                        btnTales.SetTypeface(null, Android.Graphics.TypefaceStyle.Bold);
                        btnTales.SetTextColor(Color.ParseColor("#000000"));

                        //Asignamos propiedades de layout al boton
                        btnTales.LayoutParameters = lp;
                        //Añadimos el botón a la botonera
                        llBotonera.AddView(btnTales);

                        btnTales.Click += BtnTales_Click;
                    }
                }

            }

        }



        private void BtnTales_Click(object sender, EventArgs e)
        {
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
                alert.SetTitle("Advertencia");
                alert.SetMessage("Este cuento está precargado");
                alert.Show();
            }
        }


        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        protected override void OnResume()
        {
            this.RequestedOrientation = ScreenOrientation.Landscape;
            base.OnResume();
            this.RequestedOrientation = ScreenOrientation.Landscape;
        }

    }


}