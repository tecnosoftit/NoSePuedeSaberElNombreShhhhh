using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Ssoft.ManejadorExcepciones;

namespace Ssoft.Utils
{
    public static class clsGaleria
    {
        private const string IMAGE = "strImagen";
        private const string TITLE = "strDescripcion";
        private const string DESCRIPTION = "strComentarios";
        private const string POS1 = "leaf";
        private const string POS2 = "drop";
        private const string POS3 = "bigleaf";
        private const string POS4 = "lizard";
        private const string SEUDONIMO = "strSeudonimo";
        private const string LUGAR = "strLugar";
        private const string TEMATICA = "strTematica";
        private const string OBSERVACIONES = "strObservaciones";
        private const string ESTATUS = "strEstado";

        public static string setGaleria1(DataTable dtData)
        {
            StringBuilder sGaleria = new StringBuilder();
            try
            {
                sGaleria.AppendLine("<div id='thumbs' class='navigation'>");
                sGaleria.AppendLine("<ul class='thumbs noscript'>");
                int iCount = dtData.Rows.Count;
                for (int i = 0; i < iCount; i++)
                {
                    //    <a class="thumb" href="http://farm2.static.flickr.com/1260/930424599_e75865c0d6.jpg" title="Title #23">
                    //        <img src="http://farm2.static.flickr.com/1260/930424599_e75865c0d6_s.jpg" alt="Title #23" />
                    //    </a>
                    //    <div class="caption">
                    //        <div class="download">
                    //            <a href="http://farm2.static.flickr.com/1260/930424599_e75865c0d6_b.jpg">Download Original</a>
                    //        </div>
                    //        <div class="image-title">Title #23</div>
                    //        <div class="image-desc">Description</div>
                    //    </div>
                    //</li>
                    sGaleria.AppendLine("<li>");
                    if (i.Equals(0))
                    {
                        sGaleria.AppendLine("<a class='thumb' name='" + POS1 + "' href='" + dtData.Rows[i][IMAGE].ToString() + "' title='" + dtData.Rows[i][TITLE].ToString() + "' width='75' height='75'>");
                    }
                    else
                    {
                        if (i.Equals(1))
                        {
                            sGaleria.AppendLine("<a class='thumb' name='" + POS2 + "' href='" + dtData.Rows[i][IMAGE].ToString() + "' title='" + dtData.Rows[i][TITLE].ToString() + "'>");
                        }
                        else
                        {
                            if (i.Equals(2))
                            {
                                sGaleria.AppendLine("<a class='thumb' name='" + POS3 + "' href='" + dtData.Rows[i][IMAGE].ToString() + "' title='" + dtData.Rows[i][TITLE].ToString() + "'>");
                            }
                            else
                            {
                                if (i.Equals(3))
                                {
                                    sGaleria.AppendLine("<a class='thumb' name='" + POS4 + "' href='" + dtData.Rows[i][IMAGE].ToString() + "' title='" + dtData.Rows[i][TITLE].ToString() + "'>");
                                }
                                else
                                {
                                    sGaleria.AppendLine("<a class='thumb' href='" + dtData.Rows[i][IMAGE].ToString() + "' title='" + dtData.Rows[i][TITLE].ToString() + "'>");
                                }
                            }
                        }
                    }

                    sGaleria.AppendLine("<img src='" + dtData.Rows[i][IMAGE].ToString() + "' alt='Title #" + i.ToString() + "' width='75' height='75' />");
                    sGaleria.AppendLine("</a>");
                    sGaleria.AppendLine("<div class='caption'>");
                    sGaleria.AppendLine("<div class='download'>");
                    sGaleria.AppendLine("<a href='" + dtData.Rows[i][IMAGE].ToString() + "'>Ampliar Imagen</a>");
                    sGaleria.AppendLine("</div>");
                    sGaleria.AppendLine("<div class='image-title'>" + dtData.Rows[i][TITLE].ToString() + "</div>");
                    sGaleria.AppendLine("<div class='image-desc'><strong>Comentario del Autor: </strong>" + dtData.Rows[i][DESCRIPTION].ToString() + "<br />");
                    sGaleria.AppendLine("Seudonimo del autor: " + dtData.Rows[i][SEUDONIMO].ToString() + "<br />");
                    sGaleria.AppendLine("Lugar donde fue tomada: " + dtData.Rows[i][LUGAR].ToString() + "<br />");
                    sGaleria.AppendLine("Temática: " + dtData.Rows[i][TEMATICA].ToString() + "<br />");
                    if (!string.IsNullOrEmpty(dtData.Rows[i][OBSERVACIONES].ToString()))
                        sGaleria.AppendLine("<strong>Observación: </strong>" + dtData.Rows[i][OBSERVACIONES].ToString() + "<br />");
                    if (!string.IsNullOrEmpty(dtData.Rows[i][ESTATUS].ToString()))
                        sGaleria.AppendLine("<strong>Estatus: " + dtData.Rows[i][ESTATUS].ToString() + "</strong><br />");
                    sGaleria.AppendLine("</div>");
                    sGaleria.AppendLine("</div>");
                    sGaleria.AppendLine("</li>");
                }
                sGaleria.AppendLine("</ul>");
                sGaleria.AppendLine("</div>");
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Galeria_1";
                ExceptionHandled.Publicar(cParametros);
            }
            return sGaleria.ToString();
        }
    }
}
