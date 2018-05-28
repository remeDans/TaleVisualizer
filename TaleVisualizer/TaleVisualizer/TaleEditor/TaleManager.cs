using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingEditor
{
    public class TaleManager
    {
        #region atributos
        Tale tale;

        int nextPictogramIndex;
        public int NextPictogramIndex
        {
            get
            {
                if (nextPictogramIndex < currentPictogramIndex)
                {
                    nextPictogramIndex = currentPictogramIndex;
                }

                do
                {
                    nextPictogramIndex++;
                }
                while (CurrentPage.GetPictogram(nextPictogramIndex) == null && nextPictogramIndex < 10);

                if(CurrentPage.GetPictogram(nextPictogramIndex) == null)
                {
                    nextPictogramIndex = -1;
                }

                return nextPictogramIndex;
            }
        }

        int currentPageIndex;
        /// <summary>
        /// Índice del página actual del cuento
        /// </summary>
        public int CurrentPageIndex
        {
            get { return currentPageIndex; }
        }

        public int NumberOfPictograms
        {
            get { return CurrentPage.Pictograms.Count; }
        }

        int currentPictogramIndex;
        /// <summary>
        /// Índice del pictograma actual del cuento
        /// </summary>
        public int CurrentPictogramIndex
        {
            get { return currentPictogramIndex; }
            set { currentPictogramIndex = value; }
        }

        /// <summary>
        /// Página actual del cuento
        /// </summary>
        public Page CurrentPage
        {
            get { return tale.GetPage(currentPageIndex); }
        }

        /// <summary>
        /// Páginas del cuento
        /// </summary>
        public SortedSet<Page> GetPages
        {
            get { return tale.Pages; }
        }

        /// <summary>
        /// Pictograma actual del cuento
        /// </summary>
        public Pictogram CurrentPictogram
        {
            get { return CurrentPage.GetPictogram(currentPictogramIndex); }
        }

        /// <summary>
        /// Fecha de Creación del cuento
        /// </summary>
        public DateTime DateOfCreation
        {
            get { return tale.DateOfCreation; }
            set { tale.DateOfCreation = value; }
        }
        /// <summary>
        /// Título del cuento
        /// </summary>
        public String Title
        {
            get { return tale.Title; }
            set { tale.Title = value; }
        }
        /// <summary>
        /// Lenguaje del cuento
        /// </summary>
        public String Language
        {
            get { return tale.Language; }
            set { tale.Language = value; }
        }
        /// <summary>
        /// Autor del cuento
        /// </summary>
        public String Author
        {
            get { return tale.Author; }
            set { tale.Author = value; }
        }
        /// <summary>
        /// Url del cuento
        /// </summary>
        public String Url
        {
            get { return tale.Url; }
            set { tale.Url = value; }
        }
        /// <summary>
        /// Licencia que va a tener el cuento
        /// </summary>
        public String License
        {
            get { return tale.License; }
            set { tale.License = value; }
        }
        /// <summary>
        /// Música del cuento
        /// </summary>
        public String Music
        {
            get { return tale.Music; }
            set { tale.Music = value; }
        }
        /// <summary>
        /// Fondo de la portada del cuento
        /// </summary>
        public String Background
        {
            get { return tale.Background; }
            set { tale.Background = value; }
        }
        /// <summary>
        /// Número de páginas del cuento
        /// </summary>
        public int NumberOfPages
        {
            get { return tale.Pages.Count; }
        }
        #endregion atributos

        #region Constructor

        /// <summary>
        /// Constructor vacio
        /// </summary>
        public TaleManager()
        {
            tale = new Tale();
            this.currentPageIndex = -1;
            this.currentPictogramIndex= -1;
            this.nextPictogramIndex = -1;
        }

        /// <summary>
        /// Constructor: Crea un nuevo cuento con parámetros
        /// </summary>
        /// <param name="dateofcreation"></param>
        /// <param name="title"></param>
        /// <param name="language"></param>
        /// <param name="author"></param>
        /// <param name="url"></param>
        /// <param name="license"></param>
        /// <param name="background"></param>
        ///  <param name="music"></param>
        public TaleManager(DateTime dateofcreation, String title, String language, String author, String url, String license, String background, String music)
        {
            tale = new Tale(dateofcreation, title, language, author, url, license, background,music);
            this.currentPageIndex = -1;
            this.currentPictogramIndex = -1;
            this.nextPictogramIndex = -1;
        }
        #endregion Constructor

        #region Funciones

        /// <summary>
        /// Devuelve true si estas en la portada
        /// </summary>
        public bool IsFrontPage()
        {
            bool ret = false;
            if (CurrentPageIndex == -1)
            {
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// Añade una página
        /// </summary>
        /// <param name="page">Página a añadir</param>
        public bool InsertPage(Page page)
        {
            bool ret = false;

            if (tale != null && page.Index >= 0)
            {
                if (page.Index < this.NumberOfPages)
                {
                    displacePages(currentPageIndex);
                }
                tale.Pages.Add(page);
                ret = true;
            }
            else
            {
                throw new ArgumentNullException();
            
            }
            return ret;
        }

        /// <summary>
        /// Desplaza páginas hacia la derecha o hacia la izquierda
        /// </summary>
        /// <param name="first">El ínndice desde donde quieres mover la página</param>
        /// <param name="toLeft">Por defecto: Hacia la derecha</param>
        private void displacePages(int first, bool toLeft = false)
        {
            displacePages(first, this.NumberOfPages - 1, toLeft);
        }

        /// <summary>
        /// Desplaza páginas hacia la derecha o hacia la izquierda
        /// </summary>
        /// <param name="first">El índice desde donde quieres mover la página</param>
        /// <param name="last">El índice hasta donde quieres mover la página</param>
        /// <param name="toLeft">Por defecto: Hacia la derecha</param>
        private void displacePages(int first, int last, bool toLeft = false)
        {
            // derecha
            if (!toLeft)
            {
                for (int i = last; i > first; i--)
                {
                    Page p = tale.ElementAt(i);
                    tale.RemovePage(i);
                    p.Index++;
                    tale.InsertPage(p);
                }
            }
            else //izquierda
            {
                for (int i = first; i <= last; i++)
                {
                    Page p = tale.ElementAt(i);
                    tale.RemovePage(p.Index);
                    p.Index--;
                    tale.InsertPage(p);
                }
            }

        }

        /// <summary>
        /// Elimina una página por un índice
        /// </summary>
        /// <param name="index">Índice de la página que quieres eliminar</param>
        public void RemovePage(int index)
        {
            if (tale != null)
            {
                if (index <= tale.Pages.Count && index>=0)
                {
                    tale.Pages.RemoveWhere(p => p.Index == index);
                    displacePages(index, true);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Elimina los pictogramas de una página
        /// </summary>
        /// <param name="index">Índice de la página de los pictogramas que quieres eliminar</param>
        public void RemovePictogramsPage(int index)
        {
            Page page = tale.GetPage(index);
            page.Pictograms.Clear();
        }



        /// <summary>
        /// Devuelve true si estas en una página
        /// </summary>
        public bool IsPage()
        {
            bool ret = false;
            if (CurrentPageIndex >= 0)
            {
                ret = true;
            }
            return ret;
        }

        public bool HasPage()
        {
            bool ret = false;
            if (NumberOfPages > 0)
            {
                ret = true;
            }
            return ret;
        }

        /*public int NumberOfPictogramPage()
        {
            
        }*/

        /// <summary>
        /// Devuelve true si estas en un pictograma
        /// </summary>
        public bool IsPictogram()
        {
            bool ret = false;

            if (CurrentPageIndex >= 0)
            {
                if (CurrentPictogram != null)
                {
                    if (currentPictogramIndex >= 0 && currentPictogramIndex <= 9)
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Devuelve un pictograma por un índice
        /// </summary>
        /// <param name="index">Índice del pictograma que quieres</param>
        public Pictogram GetPictogram(int index)
        {
            return CurrentPage.GetPictogram(index);
        }

        /// <summary>
        /// Cambia el pictograma de una página
        /// </summary>
        /// <param name="pictogram">Pictograma que quieres cambiar</param>
        public void SetPictogram(Pictogram pictogram)
        {
            CurrentPage.SetPictogram(pictogram);
        }

        /// <summary>
        /// Cambia la imagen del pictograma actual
        /// </summary>
        /// <param name="imageName">Nombre de la imagen que quieres cambiar</param>
        public void SetPictogramImageName(String imageName)
        {
            GetPictogram(CurrentPictogramIndex).ImageName = imageName;
        }

        /// <summary>
        /// Cambia el sonido del pictograma actual
        /// </summary>
        /// <param name="sound">Sonido que quieres cambiar</param>
        public void SetPictogramSound(String sound)
        {
            GetPictogram(CurrentPictogramIndex).Sound = sound;
        }

        /// <summary>
        /// Cambia el texto que leer del pictograma actual
        /// </summary>
        /// <param name="textToRead">Texto que leer que quieres cambiar</param>
        public void SetPictogramTextToRead(String textToRead)
        {
            GetPictogram(CurrentPictogramIndex).TextToRead = textToRead;
        }

        /// <summary>
        /// Cambia el tipo del pictograma actual
        /// </summary>
        /// <param name="type">Tipo que quieres cambiar</param>
        public void SetPictogramType(WordType type)
        {
            GetPictogram(CurrentPictogramIndex).Type = type;
        }

        /// <summary>
        /// Cambia la palabra del pictograma actual
        /// </summary>
        /// <param name="word">Palabra que quieres cambiar</param>
        public void SetPictogramWord(String word)
        {
            if(word!="")
            {
                GetPictogram(CurrentPictogramIndex).Word = word;
            }
        }

        /// <summary>
        /// Inserta una pictograms al final de la página
        /// </summary>
        public void InsertPictogram()
        {
            InsertPictogram(tale.GetPage(currentPageIndex).Pictograms.Count());
        }

        /// <summary>
        /// Inserta un pictograma nuevo en la página en la posición indicada
        /// </summary>
        /// <param name="index">Posición para el nuevo pictograma</param>
        public void InsertPictogram(int index)
        {
            if (tale.GetPage(currentPageIndex) != null)
            {
                if (index <= 10 && index >= 0)
                {
                    displacePictograms(index);
                    tale.GetPage(currentPageIndex).InsertPictogram(new Pictogram(index));
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Inserta un pictograma en el xml
        /// </summary>
        /// <param name="pictogram">Pictograma que quieres insertar</param>
        ///<param name="page">Página donde quieres insertar el pictograma</param>
        public void InsertPictogram(Pictogram pictogram, Page page)
        {
            if (pictogram != null)
            {
                if (pictogram.Index <= 10 && pictogram.Index >= 0)
                {
                   if(page!=null)
                    {
                        page.Pictograms.Add(pictogram);
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }



        /// <summary>
        /// Desplaza pictogramas hacia la derecha o hacia la izquierda
        /// </summary>
        /// <param name="first">El ínndice desde donde quieres mover el pictograma</param>
        /// <param name="toLeft">Por defecto: Hacia la derecha</param>
        private void displacePictograms(int first, bool toLeft = false)
        {
            displacePictograms(first, -1, toLeft);
        }

        /// <summary>
        /// Desplaza páginas hacia la derecha o hacia la izquierda
        /// </summary>
        /// <param name="first">El ínndice desde donde quieres mover la página</param>
        /// <param name="last">El índice hasta donde quieres mover la página</param>
        /// <param name="toLeft">Por defecto: Hacia la derecha</param>
        private void displacePictograms(int first, int last, bool toLeft = false)
        {
            if (last == -1)
                last = tale.GetPage(currentPageIndex).Pictograms.Count - 1;

            // derecha
            if (!toLeft)
            {
                for (int i = last; i >= first; i--)
                {
                    Pictogram p = tale.GetPage(currentPageIndex).ElementAt(i);
                    tale.GetPage(currentPageIndex).RemovePictogram(i);
                    p.Index++;
                    tale.GetPage(currentPageIndex).InsertPictogram(p);
                }
            }
            else //izquierda
            {
                for (int i = first; i <= last; i++)
                {
                    Pictogram p = tale.GetPage(currentPageIndex).ElementAt(i);
                    tale.GetPage(currentPageIndex).RemovePictogram(i);
                    p.Index--;
                    tale.GetPage(currentPageIndex).InsertPictogram(p);
                }
            }

        }

        /// <summary>
        /// Elimina una pictograma por un índice
        /// </summary>
        /// <param name="index">Índice del pictograma que quieres eliminar</param>
        public void RemovePictogram(int index)
        {
            if (tale.GetPage(currentPageIndex) != null)
            {
                if (index <= 10 && index >= 0)
                {
                    RemovePictogram(index);
                    displacePictograms(index + 1, true);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Devuelve true si un pictograma existe
        /// </summary>
        /// <returns></returns>
        public bool PictogramExist()
        {
            return CurrentPage.Pictograms.Where(pic => pic.Index == CurrentPictogramIndex).Count()!=0;
        }

        /// <summary>
        /// Va a la página anterior
        /// </summary>
        /// <returns></returns>
        public bool GoToPreviousPage()
        {
            bool ret = false;

            if (currentPageIndex - 1 >= -1)
            {
                currentPageIndex = currentPageIndex - 1;
                ret = true;
            }
            return ret;
        }


        /// <summary>
        /// Va a la página siguiente
        /// </summary>
        /// <returns></returns>
        public bool GoToNextPage()
        {
            bool ret = false;

            if (currentPageIndex + 1 < this.NumberOfPages)
            {
                currentPageIndex = currentPageIndex + 1;
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// Va a la portada
        /// </summary>
        /// <returns></returns>
        public int GoToFrontPage()
        {
            currentPageIndex = -1;
            return currentPageIndex;
        }


        /// <summary>
        /// Va a la última página
        /// </summary>
        /// <returns></returns>
        public int GoToEndPage()
        {
            currentPageIndex = this.NumberOfPages-1;
            return currentPageIndex;
        }

        #endregion funciones

        #region funcionesNoUtilizables
        /// <summary>
        /// Mover páginas de una posición a otra
        /// </summary>
        /// <param name="oldIndex">Índice en el que está la página</param>
        /// <param name="newIndex">Índice al que quieres enviar la página</param>
        private void MovePage(int oldIndex, int newIndex)
        {
            if (tale != null)
            {
                if (newIndex <= tale.Pages.Count)
                {
                    Page page = tale.ElementAt(oldIndex);
                    tale.RemovePage(oldIndex);

                    if (newIndex < oldIndex)
                    {
                        displacePages(newIndex, oldIndex - 1);
                    }
                    else
                    {
                        displacePages(oldIndex + 1, newIndex, true);
                    }
                    page.Index = newIndex;
                    tale.InsertPage(page);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Editar una página por un índice dado
        /// </summary>
        /// <param name="index">Índice de la página</param>
        private void SetPage(int index)
        {
            if (tale != null)
            {
                if (index <= tale.Pages.Count)
                {
                    tale.InsertPage(new Page(index));
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Mover pictogramas de una posición a otra
        /// </summary>
        /// <param name="oldIndex">Índice en el que está el pictograma</param>
        /// <param name="newIndex">Índice al que quieres enviar el pictogramaa</param>
        private void MovePictogram(int oldIndex, int newIndex)
        {
            if (tale.GetPage(currentPageIndex) != null)
            {
                if (newIndex <= 10 && newIndex >= 0)
                {
                    Pictogram pictogram = tale.GetPage(currentPageIndex).ElementAt(oldIndex);
                    tale.GetPage(currentPageIndex).RemovePictogram(oldIndex);

                    if (newIndex < oldIndex)
                    {
                        displacePictograms(newIndex, oldIndex - 1);
                    }
                    else
                    {
                        displacePictograms(oldIndex + 1, newIndex, true);
                    }
                    pictogram.Index = newIndex;
                    tale.page.InsertPictogram(pictogram);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        #endregion funcionesNoUtilizables
    }
}
