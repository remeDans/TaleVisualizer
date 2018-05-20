using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Android
{
    public static class UtilsImageAndroid
    {
        #region imageFunciona

        public static void SetImage(string pathFile, ImageView imgBackground)
        {
            BitmapFactory.Options options = GetBitmapOptionsOfImage(pathFile);
            Bitmap bitmapToDisplay = LoadScaledDownBitmapForDisplay(pathFile, options, 150, 150);
            imgBackground.SetImageBitmap(bitmapToDisplay);

        }

        private static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            float height = options.OutHeight;
            float width = options.OutWidth;
            double inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = (int)(height / 2);
                int halfWidth = (int)(width / 2);

                // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
                while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return (int)inSampleSize;
        }

        private static Bitmap LoadScaledDownBitmapForDisplay(string pathFile, BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Calculate inSampleSize
            options.InSampleSize = CalculateInSampleSize(options, reqWidth, reqHeight);
            // Decode bitmap with inSampleSize set
            //options.InJustDecodeBounds = false;
            return BitmapFactory.DecodeFile(pathFile);
        }

        private static BitmapFactory.Options GetBitmapOptionsOfImage(string pathFile)
        {
            BitmapFactory.Options options = new BitmapFactory.Options
            {
                InJustDecodeBounds = false
            };

            return options;
        }

        #endregion imageFunciona

        #region imageFuncionaResource (pero tiene que estar en el activity)

        /*public static int CalculateInSampleSizeResource(BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Raw height and width of image
            float height = options.OutHeight;
            float width = options.OutWidth;
            double inSampleSize = 1D;

            if (height > reqHeight || width > reqWidth)
            {
                int halfHeight = (int)(height / 2);
                int halfWidth = (int)(width / 2);

                // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
                while ((halfHeight / inSampleSize) > reqHeight && (halfWidth / inSampleSize) > reqWidth)
                {
                    inSampleSize *= 2;
                }
            }

            return (int)inSampleSize;
        }

        public static async Task<Bitmap> LoadScaledDownBitmapForDisplayAsyncResource(Resources res, int imageID, BitmapFactory.Options options, int reqWidth, int reqHeight)
        {
            // Calculate inSampleSize
            options.InSampleSize = CalculateInSampleSizeResource(options, reqWidth, reqHeight);
            // Decode bitmap with inSampleSize set
            options.InJustDecodeBounds = false;
            return await BitmapFactory.DecodeResourceAsync(res, imageID, options);
        }

        public static async void SetImageResource(int imageID, ImageView imgBackground)
        {
            BitmapFactory.Options options = await GetBitmapOptionsOfImageResource(imageID);
            Bitmap bitmapToDisplay = await LoadScaledDownBitmapForDisplayAsyncResource(Resources, imageID, options, 150, 150);
            imgBackground.SetImageBitmap(bitmapToDisplay);
            bitmapToDisplay.Dispose();
        }


        async static Task<BitmapFactory.Options> GetBitmapOptionsOfImageResource(int imageID)
        {
            BitmapFactory.Options options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };

            // The result will be null because InJustDecodeBounds == true.
            Bitmap result = await BitmapFactory.DecodeResourceAsync(Resources, imageID, options);
            return options;
        }*/

        #endregion imageFuncionaResource (pero tiene que estar en el activity)
    }

    }