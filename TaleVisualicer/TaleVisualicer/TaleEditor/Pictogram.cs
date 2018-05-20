using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using System.Windows;

namespace TestingEditor
{
    #region enum
    public enum WordType
    {
        NombreComun,
        Descriptivo,
        Verbo,
        Miscelanea,
        NombrePropio,
        ContenidoSocial,
        Ninguno
    }
    #endregion

    public class Pictogram
    {

        #region Atributos
        private String image, textToRead, sound, word;
       
        public String ImageName
        {
            get { return image; }
            set
            {
                if (image !=null)
                { image = value; }
                else
                { throw new ArgumentNullException(); }
            }
        }
     
        public String TextToRead
        {
            get { return textToRead; }
            set
            {
                if (textToRead != null)
                { textToRead = value; }
                else
                { throw new ArgumentNullException(); }
            }
        }

        /// <summary>
        /// Sonido asociado al pictograma. Este sonido puede ser reproducido en lugar de el texto indicado en TextToRead.
        /// Nota: Un pictograma no tiene por que tener sonidos asociados.
        /// </summary>
        public String Sound
        {
            get { return sound; }
            set { sound = value; }
                
        }

        public String Word
        {
            get { return word; }
            set
            {
                if (word != null)
                { word = value; }
                else
                { throw new ArgumentNullException(); }
            }
        }


       private Color colorWordType;
        public Color ColorWordType
        {
            get { return colorWordType; }
        }

        /// <summary>
        /// Indice de cada pictogrma en la pagina?
        /// Nota: Son 10 pictogramas no se si poner la restricción aqui
        /// </summary>
        private int index;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private WordType type;
        public WordType Type
        {
            get { return type; }
            set {
                type = value;
                colorWordType = getColorByType(value); }
        }

#endregion

        #region Constructor

        public Pictogram(int index)
        {
            this.index = index;
            this.image = "";
            this.textToRead = "";
            this.sound = "";
            this.word = "";
            this.type = WordType.Ninguno;
            this.colorWordType = getColorByType(this.type);
        }

        public Pictogram(int index, String image, String whatYouRead, String sound, String word, WordType type)
        {
            this.index = index;
            this.image = image;
            this.textToRead = whatYouRead;
            this.sound = sound;
            this.word = word;
            this.type = type;
            this.colorWordType = getColorByType(type);
        }

        #endregion

        #region Métodos

        /* public void EditPictogram(int index, String image, String whatYouRead, String sound, String word, WordType type)
        {
            this.index = index;
            this.image = image;
            this.textToRead = whatYouRead;
            this.sound = sound;
            this.word = word;
            this.type = type;
            this.colorWordType = getColorByType(type);
        }*/


        public Color getColorByType(WordType Wt)
        {

             Color color = Color.White;
            switch (Wt)
            {
                case WordType.NombreComun:
                    color = Color.Orange;
                    break;
                case WordType.Descriptivo:
                    color = Color.Blue;
                    break;
                case WordType.Verbo:
                    color = Color.Green;
                    break;
                case WordType.Miscelanea:
                    color = Color.Black;
                    break;
                case WordType.NombrePropio:
                    color = Color.Black;
                    break;
                case WordType.ContenidoSocial:
                    color = Color.Black;
                    break;
                case WordType.Ninguno:
                    color = Color.Transparent;
                    break;
                default:
                    color = Color.Transparent;
                    break;
            }

            return color;
        }
        #endregion

        #region ToString
        public override String ToString()
        {
            Color c = colorWordType;
            return "Pictogram: " + "\r\n\tIndice: " + index + "\r\n\tImagen: " + image + "\r\n\tTexto a leer: " + textToRead + "\r\n\tSonido: " + sound + "\r\n\tPalabra: " + word + "\r\n\tTipo de palabra: " + colorWordType.ToString();
        }
#endregion



}
}
