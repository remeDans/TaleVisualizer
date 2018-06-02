using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TaleVisualizer
{
    [Activity(Label = "TutorialActivity", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
ScreenOrientation = ScreenOrientation.Landscape)]
    public class TutorialActivity : Activity
    {
        CheckBox check;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.tutorial);

            Button btnPrueba = FindViewById<Button>(Resource.Id.btnPrueba);

            check = FindViewById<CheckBox>(Resource.Id.checkbox);

            btnPrueba.Click += BtnPrueba_Click;
        }

        private void BtnPrueba_Click(object sender, EventArgs e)
        {
            if (check.Checked)
            {
                ISharedPreferencesEditor edit = GetDefaultSharedPreferences.pref.Edit();
                edit.PutBoolean("mostrarTutorial", false);
                edit.Apply();
            }
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);

        }


        public override void OnBackPressed() { }

    }
}