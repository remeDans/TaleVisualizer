using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace TaleVisualizer
{
    [Activity(Label = "Tutorial", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
ScreenOrientation = ScreenOrientation.Landscape)]//Theme = "@style/MyTheme.Base"
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