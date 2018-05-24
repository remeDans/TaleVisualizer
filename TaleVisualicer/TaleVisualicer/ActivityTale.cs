using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using TestingEditor;

namespace TaleVisualicer
{
    [Activity(Label = "ActivityTale", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
ScreenOrientation = ScreenOrientation.Landscape, Theme = "@style/MyTheme.Base", NoHistory = true)]
    public class ActivityTale : Activity
    {
        List<ImageView> images;
        List<TextView> textBlocksWord;
        List<RelativeLayout> borders;

        TextView lblFrontPage;
        FrameLayout frmLytFrontPage;
        FrameLayout frmLytPage;
        Button btnPreviousPage;
        Button btnNextPage;
        ImageView imgBackgroundFrontPage;
        ImageView imgBackgroundPage;
        LinearLayout lnrLytNavigation;

        TableLayout tLyt;
        LinearLayout lnr;

        MediaPlayer mediaPlayerTale;
        Boolean hasFinishedOrStopped;

        XmlDocument xmlDocLog;
        String xmlRead;

        public TaleManager taleManager;

        string pathTale;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.LytTales);

            //SIN barra de navvegación y title bar
            /*int uiOptions = (int)SystemUiFlags.HideNavigation | (int)SystemUiFlags.ImmersiveSticky;
            Window.DecorView.SystemUiVisibility = (StatusBarVisibility)uiOptions;
            Window.DecorView.SystemUiVisibilityChange += visibilityListener;*/

            lblFrontPage = FindViewById<TextView>(Resource.Id.lblFrontPage);
            btnPreviousPage = FindViewById<Button>(Resource.Id.btnPreviousPage);
            btnNextPage = FindViewById<Button>(Resource.Id.btnNextPage);

            frmLytFrontPage = FindViewById<FrameLayout>(Resource.Id.frmLytFrontPage);
            frmLytPage = FindViewById<FrameLayout>(Resource.Id.frmLytPage);
            imgBackgroundFrontPage = FindViewById<ImageView>(Resource.Id.imgBackgroundFrontPage);
            imgBackgroundPage = FindViewById<ImageView>(Resource.Id.imgBackgroundPage);
            tLyt = FindViewById<TableLayout>(Resource.Id.tLyt);
            lnr = FindViewById<LinearLayout>(Resource.Id.lnr);

            lnrLytNavigation = FindViewById<LinearLayout>(Resource.Id.lnrLytNavigation);

            #region brdPagePictoIni

            textBlocksWord = new List<TextView>();
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic1));
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic2));
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic3));
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic4));
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic5));
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic6));
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic7));
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic8));
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic9));
            textBlocksWord.Add(FindViewById<TextView>(Resource.Id.txtWordPic10));

            images = new List<ImageView>();
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic1));
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic2));
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic3));
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic4));
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic5));
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic6));
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic7));
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic8));
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic9));
            images.Add(FindViewById<ImageView>(Resource.Id.imgPic10));

            borders = new List<RelativeLayout>();
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic1));
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic2));
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic3));
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic4));
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic5));
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic6));
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic7));
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic8));
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic9));
            borders.Add(FindViewById<RelativeLayout>(Resource.Id.brdPic10));

            #endregion brdPagePictoIni  WindowStartupLocation = WindowStartupLocation.CenterScreen;


            mediaPlayerTale = new MediaPlayer();

            btnPreviousPage.Click += BtnPreviousPage_Click;
            btnNextPage.Click += BtnNextPage_Click;

            pathTale = JsonConvert.DeserializeObject<string>(Intent.GetStringExtra("path"));
        
            GetXml(pathTale + "/index.xml");
            //ChangeToAbsolutePathAndroid(pathTale+ "/");
            ChangeToAbsolutePathAndroid2(pathTale + "/");
            UpdateGUI();
        }

         #region xmlGET
        private void GetXml(String fileName)
        {
            taleManager = null;
            xmlDocLog = null;
            using (StreamReader sr = new StreamReader(File.OpenRead(fileName)))
            {
                xmlRead = sr.ReadToEnd();
            }

            xmlDocLog = new XmlDocument();
            xmlDocLog.LoadXml(xmlRead);

            taleManager = new TaleManager();
            taleManager = GetTale();

            closeXML();
        }
        #endregion xmlGET


        private void ShowTaleGUI()
        {
            frmLytFrontPage.Visibility = ViewStates.Visible;
            frmLytPage.Visibility = ViewStates.Gone;
            lnrLytNavigation.Visibility = ViewStates.Visible;
        }

        private void ShowPageGUI()
        {
            frmLytFrontPage.Visibility = ViewStates.Gone;
            frmLytPage.Visibility = ViewStates.Visible;
            lnrLytNavigation.Visibility = ViewStates.Visible;
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

        #region Navigation

        private void BtnPreviousPage_Click(object sender, EventArgs e)
        {
            if (taleManager.GoToPreviousPage())
            {
                UpdateGUI();
            }
        }

        private void BtnNextPage_Click(object sender, EventArgs e)
        {
            if (taleManager.GoToNextPage())
            {
                UpdateGUI();
            }
        }

        private void UpdateGUI()
        {
            if (taleManager == null)
            {
                //menuCloseTale.IsEnabled = false;
                //ShowStartInterface();
            }

            if (taleManager != null)
            {
                //menuCloseTale.IsEnabled = true;

                if (taleManager.CurrentPageIndex == -1)
                {
                    UpdateTaleAndroidGUI();
                    ShowTaleGUI();
                }
                else
                {
                    UpdatePageAndroidGUI();
                    //UpdatePageAndroidGUI2();
                    ShowPageGUI();
                }
            }

            UpdateNavigation();
        }

        private void UpdateNavigation()
        {
            if (taleManager != null)
            {
                if (hasFinishedOrStopped)
                {
                    //btnPlay.IsEnabled = true;
                    //btnStop.IsEnabled = false;

                    if (taleManager.CurrentPageIndex == -1)
                    {
                        //btnGoToFrontPage.IsEnabled = false;
                        //btnGoToEndPage.IsEnabled = true;

                        btnPreviousPage.Enabled = false;
                        btnNextPage.Enabled = true;

                        if (taleManager.NumberOfPages == 0)
                        {
                            //btnGoToFrontPage.IsEnabled = false;
                            btnPreviousPage.Enabled = false;
                            //btnGoToEndPage.IsEnabled = false;
                            btnNextPage.Enabled = false;
                        }
                    }
                    else
                    {
                        if (taleManager.CurrentPageIndex >= 0 && taleManager.CurrentPageIndex < taleManager.NumberOfPages - 1)
                        {
                            //btnGoToFrontPage.IsEnabled = true;
                            btnPreviousPage.Enabled = true;
                            //btnGoToEndPage.IsEnabled = true;
                            btnNextPage.Enabled = true;
                        }
                        else
                        {
                            //btnGoToFrontPage.IsEnabled = true;
                            btnPreviousPage.Enabled = true;
                            //btnGoToEndPage.IsEnabled = false;
                            btnNextPage.Enabled = false;

                            //btnPlay.IsEnabled = true;
                            //btnStop.IsEnabled = false;
                        }
                    }
                }
                else
                {
                    /*btnPlay.IsEnabled = false;
                    btnStop.IsEnabled = true;*/
                }
            }
        }


        public override void OnBackPressed()
        {
            mediaPlayerTale.Stop();
            base.OnBackPressed();
        }

        protected override void OnPause()
        {
            mediaPlayerTale.Stop();
            base.OnPause();
        }


        protected override void OnStart()
        {
            if(taleManager!=null)
            {
                if(taleManager.IsFrontPage())
                {
                    if (taleManager.Music != "")
                    {
                        mediaPlayerTale = MediaPlayer.Create(this, Android.Net.Uri.Parse(taleManager.Music));
                        mediaPlayerTale.Start();
                    } 
                }

                if (taleManager.IsPage())
                {
                    if (taleManager.CurrentPage.Music != "")
                    {
                        mediaPlayerTale = MediaPlayer.Create(this, Android.Net.Uri.Parse(taleManager.CurrentPage.Music));
                        mediaPlayerTale.Start();
                    }
                }
            }

            base.OnStart();
        }


        private void UpdateTaleAndroidGUI()
        {
            mediaPlayerTale.Stop();
            //mediaPlayerTale.Dispose();

            frmLytFrontPage.SetBackgroundColor(Color.WhiteSmoke);
            frmLytFrontPage.SetBackgroundResource(0);
            imgBackgroundPage.SetImageBitmap(null);

            if (taleManager != null)
            {
                lblFrontPage.Text = taleManager.Title;

                //-- Background
                String background = taleManager.Background;
                int tamBackground = background.Length;
                if (tamBackground > 0)
                {
                    if (UtilsAndroid.isArchive(background))
                    {
                        UtilsImageAndroid.SetImage(background, imgBackgroundFrontPage);
                    }

                    if (background.Contains("#"))
                    {
                        frmLytFrontPage.SetBackgroundColor(Color.ParseColor(background));
                    }
                }

                if (taleManager.Music != "")
                {
                    mediaPlayerTale = MediaPlayer.Create(this, Android.Net.Uri.Parse(taleManager.Music));
                    mediaPlayerTale.Start();
                }

            }

            Application.Dispose();
            Application.OnTrimMemory(TrimMemory.RunningCritical);

        }

        private void UpdatePageAndroidGUI()
        {
            mediaPlayerTale.Stop();
            //mediaPlayerTale.Dispose();

            frmLytPage.SetBackgroundColor(Color.WhiteSmoke);
            imgBackgroundPage.SetImageBitmap(null);

            for (int i = 0; i < 10; i++)
            {
                textBlocksWord[i].Text = "";
                images[i].SetImageResource(0);
                images[i].SetImageBitmap(null);
                borders[i].SetBackgroundColor(Color.Transparent);
            }

            //-- Background
            TestingEditor.Page auxPage = taleManager.CurrentPage;

            if (auxPage.Background != "")
            {
                String background = auxPage.Background;
                int tamBackground = background.Length;
                if (tamBackground > 0)
                {
                    String p = background.Substring(tamBackground - 4);
                    UtilsImageAndroid.SetImage(background, imgBackgroundFrontPage);

                    if (p.Contains("."))
                    {
                        UtilsImageAndroid.SetImage(background, imgBackgroundPage);
                    }

                    if (background.Contains("#"))
                    {
                        frmLytPage.SetBackgroundColor(Color.ParseColor(background));
                    }
                }
            }

            if (auxPage.Music != "")
            {
                mediaPlayerTale = MediaPlayer.Create(this, Android.Net.Uri.Parse(auxPage.Music));
                mediaPlayerTale.Start();
            }

            //-- PICTOGRAMAS ----

            foreach (Pictogram pictogram in auxPage.Pictograms)
            {
                int i = pictogram.Index;
                textBlocksWord[i].Text = pictogram.TextToRead;

                string path = pictogram.ImageName;

                if (path != "")
                {
                    Bitmap bit = BitmapFactory.DecodeFile(path);
                    images[i].SetImageBitmap(bit);
                    
                    EditBorderAndroid(borders[i], pictogram.getColorByType(pictogram.Type), images[i], 10);
                    
                }

                
            }

            Application.Dispose();
            Application.OnTrimMemory(TrimMemory.RunningCritical);
        }

        private void UpdatePageAndroidGUI2()
        {
            mediaPlayerTale.Stop();

            frmLytPage.SetBackgroundColor(Color.WhiteSmoke);
            imgBackgroundPage.SetImageBitmap(null);

            for (int i = 0; i < 10; i++)
            {
                textBlocksWord[i].Text = "";
                images[i].SetImageResource(0);
                borders[i].SetBackgroundColor(Color.Transparent);
            }

            //-- Background
            TestingEditor.Page auxPage = taleManager.CurrentPage;

            if (auxPage.Background != "")
            {
                String background = auxPage.Background;
                int tamBackground = background.Length;
                if (tamBackground > 0)
                {
                    String p = background.Substring(tamBackground - 4);
                    UtilsImageAndroid.SetImage(background, imgBackgroundFrontPage);

                    if (p.Contains("."))
                    {
                        UtilsImageAndroid.SetImage(background, imgBackgroundPage);
                    }

                    if (background.Contains("#"))
                    {
                        frmLytPage.SetBackgroundColor(Color.ParseColor(background));
                    }
                }
            }

            if (auxPage.Music != "")
            {
                mediaPlayerTale = MediaPlayer.Create(this, Android.Net.Uri.Parse(auxPage.Music));
                mediaPlayerTale.Start();
            }

            

            

            //-- PICTOGRAMAS ----

            /*foreach (Pictogram pictogram in auxPage.Pictograms)
            {
                int i = pictogram.Index;
                borders[i].Visibility = ViewStates.Gone;
                textBlocksWord[i].Visibility = ViewStates.Gone;
            }

            foreach (Pictogram pictogram in auxPage.Pictograms)
            {
                

                int i = pictogram.Index;

                textBlocksWord[i].Text = pictogram.TextToRead;

                string path = pictogram.ImageName;

                borders[i].Visibility = ViewStates.Visible;
                textBlocksWord[i].Visibility = ViewStates.Visible;

                if (path != "")
                {
                    Bitmap bit = BitmapFactory.DecodeFile(path);
                    images[i].SetImageBitmap(bit);
                    EditBorder(borders[i], pictogram.getColorByType(pictogram.Type), images[i], 10);
                }
            }*/
        }

        private void EditBorderAndroid(RelativeLayout border, Color color, ImageView img, int borderSize)
        {
            border.SetBackgroundColor(color);
            img.SetPadding(borderSize, borderSize, borderSize, borderSize);
        }

        #endregion Navigation


        private void ChangeToAbsolutePathAndroid2(String location)
        {
            //cuento
            if (taleManager.Background != "" && UtilsAndroid.isArchive(taleManager.Background))
            {
                taleManager.Background = location + "/Imagenes/0/" + taleManager.Background;
            }
            if (taleManager.Music != "" && UtilsAndroid.isArchive(taleManager.Music))
            {
                taleManager.Music = location + "/Audios/0/" + taleManager.Music;
            }
            //páginas
            foreach (Page page in taleManager.GetPages)
            {
                int numPage = page.Index + 1;
                if (page.Background != "" && UtilsAndroid.isArchive(page.Background))
                {
                    page.Background = location + "/Imagenes/" + numPage + "/" + page.Background;
                }
                if (page.Music != "" && UtilsAndroid.isArchive(page.Music))
                {
                    page.Music = location + "/Audios/" + numPage + "/" + page.Music;
                }

                foreach (Pictogram picto in page.Pictograms)
                {
                    if (picto.ImageName != "" && UtilsAndroid.isArchive(picto.ImageName))
                    {
                        picto.ImageName = location + "/Imagenes/" + numPage + "/" + picto.ImageName;
                    }
                    if (picto.Sound != "" && UtilsAndroid.isArchive(picto.Sound))
                    {
                        picto.Sound = location + "/Audios/" + numPage + "/" + picto.Sound;
                    }
                }
            }
        }


        private void ChangeToAbsolutePathAndroid(String location)
        {
            //cuento
            if (taleManager.Background != "" && UtilsAndroid.isArchive(taleManager.Background))
            {
                taleManager.Background = location + taleManager.Background;
            }
            if (taleManager.Music != "" && UtilsAndroid.isArchive(taleManager.Music))
            {
                taleManager.Music = location + taleManager.Music;
            }
            //páginas
            foreach (Page page in taleManager.GetPages)
            {
                int numPage = page.Index + 1;
                if (page.Background != "" && UtilsAndroid.isArchive(page.Background))
                {
                    page.Background = location + page.Background;
                }
                if (page.Music != "" && UtilsAndroid.isArchive(page.Music))
                {
                    page.Music = location + page.Music;
                }

                foreach (Pictogram picto in page.Pictograms)
                {
                    if (picto.ImageName != "" && UtilsAndroid.isArchive(picto.ImageName))
                    {
                        picto.ImageName = location + picto.ImageName;
                    }
                    if (picto.Sound != "" && UtilsAndroid.isArchive(picto.Sound))
                    {
                        picto.Sound = location + picto.Sound;
                    }
                }
            }
        }

    

        #region LogStoreAndroid

        public TaleManager GetTale()
        {
            String title = GetTitle();
            DateTime date = GetDateOfCreation();
            String language = GetLanguage();
            String author = GetAuthor();
            String url = GetUrl();
            String license = GetLicense();
            String background = GetBackground();
            String music = GetMusic();

            TaleManager taleManager = new TaleManager(date, title, language, author, url, license, background, music);

            int i = 0;
            int j = 0;

            XmlNodeList pages = (XmlNodeList)xmlDocLog.SelectNodes(("/tale/page"));

            foreach (XmlElement page in pages)
            {
                if (page != null)
                {
                    String musicPage = page.GetAttribute("music");
                    String backgroundPage = page.GetAttribute("background");
                    Page p = new Page(i, musicPage, backgroundPage);
                    taleManager.InsertPage(p);

                    foreach (XmlElement pictogram in page.ChildNodes)
                    {
                        if (pictogram != null)
                        {
                            String index = pictogram.GetAttribute("index");
                            j = int.Parse(index);
                            String imageNamePictogram = pictogram.GetAttribute("image");
                            String soundPictogram = pictogram.GetAttribute("sound");
                            String textToReadPictogram = pictogram.GetAttribute("textToRead");
                            String wordPictogram = pictogram.GetAttribute("word");
                            String typePictogram = pictogram.GetAttribute("wordType");
                            WordType typePicto = WordType.ContenidoSocial;
                            foreach (var type in Enum.GetValues(typeof(WordType)))
                            {
                                if (typePictogram == type.ToString())
                                {
                                    typePicto = (WordType)type;
                                }
                            }
                            taleManager.InsertPictogram(new Pictogram(j, imageNamePictogram, textToReadPictogram, soundPictogram, wordPictogram, typePicto), p);
                        }
                    }
                }
                i++;
            }


            return taleManager;
        }


        private String GetCData(String field)
        {
            XmlElement xe = (XmlElement)xmlDocLog.SelectSingleNode("/tale");
            return xe.GetAttribute(field);
        }

        public DateTime GetDateOfCreation()
        {
            String value = GetCData("dateOfCreation");
            return DateTime.Parse(value);
        }

        public String GetTitle()
        {
            return GetCData("title");
        }

        public String GetLanguage()
        {
            return GetCData("language");
        }

        public String GetAuthor()
        {
            return GetCData("author");
        }

        public String GetLicense()
        {
            return GetCData("license");
        }

        public String GetUrl()
        {
            return GetCData("url");
        }

        public String GetBackground()
        {
            return GetCData("background");
        }

        public String GetMusic()
        {
            return GetCData("music");
        }

        public void closeXML()
        {
            xmlDocLog = null;
        }

        #endregion LogStoreAndroid

    }
}