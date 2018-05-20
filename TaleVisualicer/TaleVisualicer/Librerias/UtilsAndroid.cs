using System;
using System.Linq;

namespace Android
{
    public static class UtilsAndroid
    {

        public static Boolean isArchive(String nameArchive)
        {
            Boolean res = false;
            if(nameArchive != "")
            {
                if(nameArchive.Count()>=5)
                {
                    String b = nameArchive;
                    int tamBackground = b.Length;
                    String p = nameArchive.Substring(tamBackground - 4);
                    if (p.Contains("."))
                    {
                        res = true;
                    }
                }
            }

            return res;
        }

    public static String GetExtension(String fileName)
        {
            String extension = "";
            int tamName = fileName.Length;
            if (tamName > 4)
            {
                extension = fileName.Substring(tamName - 3);
            }

            return extension;
        }

        public static String ChangeToRelativePath(String path)
        {
            String res = "";
            if (path!="")
            {
                String auxPath = path;
                auxPath = auxPath.Replace('\\', '/');
                var cadena = auxPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                res = cadena.Last();
            }
            return res;
        }

    }
}
