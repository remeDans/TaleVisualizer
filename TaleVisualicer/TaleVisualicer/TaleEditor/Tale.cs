using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingEditor
{
    public class Tale : IEnumerable<Page>
    {

        #region Atributos
        public Page page;
        private String title,language,author,url,license;
        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        public String Language
        {
            get { return language; }
            set { language = value; }
        }
        public String Author
        {
            get { return author; }
            set { author = value; }
        }
        public String Url
        {
            get { return url; }
            set { url = value; }
        }
        public String License
        {
            get { return license; }
            set { license = value; }
        }
        private DateTime dateOfCreation;
        public DateTime DateOfCreation
        {
            get { return dateOfCreation; }
            set { dateOfCreation = value; }
        }
        private SortedSet<Page> pages;
        public SortedSet<Page> Pages
        {
            get { return pages; }
            set { pages = value; }
        }
        
        private String music;
        public String Music
        {
            get { return music; }
            set { music = value; }
        }
        private String background;
        public String Background
        {
            get { return background; }
            set { background = value; }
        }
        #endregion

        #region Constructores
        public Tale()
        {
            this.pages = new SortedSet<Page>(new byIndexPage());
            this.dateOfCreation = DateTime.Now;
            this.title = "";
            this.language = "";
            this.author = "";
            this.url = "";
            this.license = "";
            this.Background = "";
            this.music = "";
        }

        public Tale(DateTime dateofcreation,String title,String language,String author,String url,String license, String background, String music)
        {
            
            this.dateOfCreation = dateofcreation;
            this.title = title;
            this.language = language;
            this.author = author;
            this.url = url;
            this.license = license;
            this.background = background;
            this.music = music;
            this.pages = new SortedSet<Page>(new byIndexPage());
        }
#endregion

        #region Iterable

        public IEnumerator<Page> GetEnumerator()
        {
            foreach (Page pi in pages)
                yield return pi;
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Page
        public Page InsertPage(Page page)
        {
            Page result = null;

            IEnumerable<Page> pagAux = pages.Where(pagWhere => pagWhere.Index == page.Index);

            if (pagAux != null && pagAux.Count() != 0)
            {
                result = pagAux.First();
                pages.Remove(result);
            }
            pages.Add(page);
            return result;
        }


        /*public void EditTale(DateTime dateofcreation, String title, String language, String author, String url, String license)
        {
            this.dateOfCreation = dateofcreation;
            this.title = title;
            this.language = language;
            this.author = author;
            this.url = url;
            this.license = license;
            this.pages = new SortedSet<Page>(new byIndexPage());

        }*/


        public void RemovePage(int index)
        {
            Page page=null;

            if (index > -1 && index < pages.Count)
            {
                page = GetPage(index);
                pages.Remove(page);
            }

        }

        public Page GetPage(int index)
        {
            Page page = null;

            try
            {
                if (index > -1 && index < pages.Count)
                {
                    page = Pages.Where<Page>(pag => pag.Index == index).First<Page>();

                }
            }
            catch { }

            return page;
        }

        #endregion

        #region ToString
        public override String ToString()
        {
            String cuento;
            cuento = "Cuento: \r\n\t" + "pagina: " + page + "\r\n\tFecha de creación: " + dateOfCreation.ToString() + "\r\n\tTitulo: " + title + "\r\n\tLenguaje: " + language + "\r\n\tAutor: " + author + "\r\n\turl: " + url + "\r\n\tlicencia: " + license + "\r\n\tPages: ";
            foreach (Page pag in pages)
            {
                cuento += pag.ToString();
            }
            return cuento;
        }
        #endregion

        #region Comparador
        private class byIndexPage : IComparer<Page>
        {
            public int Compare(Page x, Page y)
            {
                return x.Index.CompareTo(y.Index);
            }
        }
        #endregion
    }
}
