using System;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using System.Text;  

namespace Ssoft.Utils
{
    public class clsHttpZipper
    {
        #region Compres
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uncompressedString"></param>
        /// <returns></returns>
        public string Compress(string uncompressedString, Enum_Encoding eEncoding)
        {
            string sEncoding = EncodingIn(eEncoding);

            byte[] bytData = Encoding.
                GetEncoding(sEncoding).GetBytes(uncompressedString);
            MemoryStream ms = new MemoryStream();
            Stream s = new DeflaterOutputStream(ms);
            s.Write(bytData, 0, bytData.Length);
            s.Close();
            byte[] compressedData = (byte[])ms.ToArray();
            return System.Convert.ToBase64String(compressedData, 0, compressedData.Length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compressedString"></param>
        /// <returns></returns>
        public string DeCompress(string compressedString, Enum_Encoding eEncoding)
        {
            string sEncoding = EncodingIn(eEncoding);

            string uncompressedString = "";
            int totalLength = 0;
            byte[] bytInput = System.Convert.FromBase64String(compressedString);

            byte[] writeData = new byte[bytInput.Length];
            Stream s2 = new InflaterInputStream(new MemoryStream(bytInput));
            while (true)
            {
                int size = s2.Read(writeData, 0, writeData.Length);
                if (size > 0)
                {
                    totalLength += size;
                    uncompressedString += Encoding.
                        GetEncoding(sEncoding).GetString(writeData, 0, size);
                }
                else
                {
                    break;
                }
            }
            s2.Close();
            return uncompressedString;
        }

        /// <summary>
        /// Dado una respuesta enviada por un sitio web realiza la descomprensión
        /// según el formato en el cual responde el sitio
        /// http://www.flowgroup.fr/en/kb/technical/compressionhttp.aspx
        /// </summary>
        /// <param name="hwrResponse"></param>
        /// <returns></returns>
        public static Stream GetResponseStream(HttpWebResponse hwrResponse)
        {
            // select the right decompression stream
            if (hwrResponse.ContentEncoding == "gzip")
                return new GZipInputStream(hwrResponse.GetResponseStream());
            else if (hwrResponse.ContentEncoding == "deflate")
                return new InflaterInputStream(hwrResponse.GetResponseStream());
            else
                return hwrResponse.GetResponseStream();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sEncoding"></param>
        /// <returns></returns>
        public static string EncodingIn(Enum_Encoding eEncoding)
        {
            string sEncoding = "UTF-8";
            switch (eEncoding)
            {
                case Enum_Encoding.Unicode:
                    sEncoding = "UTF-16";
                    break;
                case Enum_Encoding.UTF7:
                    sEncoding = "UTF-7";
                    break;
                case Enum_Encoding.UTF8:
                    sEncoding = "UTF-8";
                    break;
                case Enum_Encoding.UTF32:
                    sEncoding = "UTF-32";
                    break;
                case Enum_Encoding.ISO88591:
                    sEncoding = "UTF-8";
                    break;
            }
            return sEncoding;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sEncoding"></param>
        /// <returns></returns>
        public static Enum_Encoding EncodingOut(string sEncoding)
        {
            sEncoding = sEncoding.ToUpper();
            Enum_Encoding eEncoding = Enum_Encoding.UTF8;
            switch (sEncoding)
            {
                case "UTF-16":
                    eEncoding = Enum_Encoding.Unicode;
                    break;
                case "UNIDOCE":
                    eEncoding = Enum_Encoding.Unicode;
                    break;
                case "UTF-7":
                    eEncoding = Enum_Encoding.UTF7;
                    break;
                case "UTF-8":
                    eEncoding = Enum_Encoding.UTF8;
                    break;
                case "UTF-32":
                    eEncoding = Enum_Encoding.UTF32;
                    break;
                case "ISO-8859-1":
                    eEncoding = Enum_Encoding.ISO88591;
                    break;
            }
            return eEncoding;
        }
        #endregion
    }
}
