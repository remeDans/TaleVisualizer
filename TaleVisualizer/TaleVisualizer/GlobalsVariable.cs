using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace TaleVisualizer
{


    public static class GetDefaultSharedPreferences
    { 
        public static ISharedPreferences pref = Application.Context.GetSharedPreferences("Info", FileCreationMode.Private);
    }
}