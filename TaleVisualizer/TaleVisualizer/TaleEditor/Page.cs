using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TestingEditor
{
    public class Page : IEnumerable<Pictogram>
    {
        #region Atributos
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private String music,background;
        public String Music
        {
            get { return music; }
            set { music = value; }
        }
        public String Background
        {
            get { return background; }
            set { background = value; }
        }

        private SortedSet<Pictogram> pictograms;
        public SortedSet<Pictogram> Pictograms
        {
            get { return pictograms; }
            //set { pictograms = value; }
        }
        #endregion

        #region Constructor
        public Page()
        {
            this.index = -1;
            this.music = "";
            this.background = "";
            this.pictograms = new SortedSet<Pictogram>(new byIndexPictogram());
        }

       public Page(int index, String music, String background)
        {
            this.index = index;
            this.music = music;
            this.background = background;
            this.pictograms = new SortedSet<Pictogram>(new byIndexPictogram());
        }

       public Page(int index)
       {
           this.index = index;
           this.music = "";
           this.background = "";
           this.pictograms = new SortedSet<Pictogram>(new byIndexPictogram());
       }
        #endregion

       #region Iterable
       public IEnumerator<Pictogram> GetEnumerator()
       {
           foreach(Pictogram pi in pictograms)
                yield return pi;
       }
       System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
       {
           return GetEnumerator();
       }
       #endregion

       #region Pictogram
       /* public void EditPage(int index, String music, String background)
        {
            this.index = index;
            this.music = music;
            this.background = background;
            this.pictograms = new SortedSet<Pictogram>(new byIndexPictogram());
        }*/

       public void RemovePictogram(int index)
       {
           Pictogram picto;

           picto = GetPictogram(index);
           pictograms.Remove(picto);

       }

        public Pictogram GetPictogram(int index)
       {
            Pictogram pictogram = null;

            try
            {
                if(index >=0 && index< 10)
                {
                    pictogram = Pictograms.Where<Pictogram>(pic => pic.Index == index).First<Pictogram>();

                    //var pictogram = from pi in pictograms where pi.Index == index select pi;  //pictograms.Se .Where<Pictogram>(p => p.Index == index)
                }

            }
            catch { }

            return pictogram;          
       }

        public void SetPictogram(Pictogram pictogram)
        {
            InsertPictogram(pictogram);
        }

        public Pictogram InsertPictogram(Pictogram pictogram)
       {
          Pictogram result = null;
          IEnumerable<Pictogram> picAux = pictograms.Where(picWhere => picWhere.Index == pictogram.Index);
          if (picAux !=null && picAux.Count() !=0)
          {
              result=picAux.First();
              pictograms.Remove(result);
          }

          pictograms.Add(pictogram);
          return result;
       }

       #endregion

       #region ToString
       public override String ToString()
        {
            String pagina;
            pagina =  "Page: " + "\r\n\tIndice: " + index + "\r\n\tImagen de fondo: " + background + "\r\n\tMusic: " + music + "\r\n\tPictogram: ";
            foreach (Pictogram pi in pictograms)
            {
                pagina += pi.ToString();
            }
            return pagina;
        }

       #endregion

       #region Comparador
       private class byIndexPictogram : IComparer<Pictogram>
        {

            public int Compare(Pictogram x, Pictogram y)
            {
                return x.Index.CompareTo(y.Index);
              
            }
        }
       #endregion
    }
}
