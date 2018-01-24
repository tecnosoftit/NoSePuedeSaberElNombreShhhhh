using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Ssoft.ManejadorExcepciones;
using Ssoft.Sql;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Ssoft.Utils
{
    public class Utils
    {
        // Plantillas
        public const string PlantillaReservaSabreCar = "PlantillaReservaSabreCar.aspx";
        public const string PlantillaReservaSabreAir = "PlantillaReservaSabre.aspx";
        public const string PlantillaReservaSabreHot = "PlantillaReservaSabre.aspx";
        public const string PlantillaReservaHotelBedsHot = "PlantillaReservaHotelB.aspx";

        public Utils()
        {
        }
        public string LlenarDatos(string sCaracter, string sValor, int iTotal)
        {
            string sResp = string.Empty;
            for (int i = sValor.Length; i < iTotal; i++)
            {
                sResp = sResp + sCaracter;
            }
            sResp = sResp + sValor;
            return sResp;
        }

        public string CargarImagen(string strRuta, FileUpload fImagen, bool bolReplace)
        {
            string strFileName = string.Empty;
            string strExtencion = string.Empty;

            strFileName = fImagen.PostedFile.FileName;
            strFileName = Path.GetFileName(strFileName);
            try
            {
                strExtencion = strFileName.Substring(strFileName.LastIndexOf("."), strFileName.Length - strFileName.LastIndexOf("."));

                if (strExtencion != ".jpg")
                {
                    if (strExtencion != ".gif")
                    {
                        if (strExtencion != ".png")
                        {
                            if (strExtencion != ".bmp")
                            {
                                if (strExtencion != ".jpeg")
                                {
                                    if (strExtencion != ".JPEG")
                                    {
                                        if (strExtencion != ".JPG")
                                        {
                                            if (strExtencion != ".GIF")
                                            {
                                                if (strExtencion != ".PNG")
                                                {
                                                    if (strExtencion != ".BMP")
                                                    {
                                                        return "0";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return "0";
            }
            try
            {
                if (File.Exists(strRuta + "\\" + strFileName))
                {
                    if (bolReplace)
                    {
                        File.Delete(strRuta + "\\" + strFileName);
                        fImagen.PostedFile.SaveAs(strRuta + "\\" + strFileName);
                    }
                    else
                    {
                        return "1";
                    }
                }
                else
                {
                    fImagen.PostedFile.SaveAs(strRuta + "\\" + strFileName);
                }
                return strFileName;
            }
            catch
            {
                return "2";
            }
        }
        public clsParametros CargarImagen(string strRuta, FileUpload fImagen, List<string> lExtencion, int iLenght, bool bolReplace)
        {
            string strFileName = string.Empty;
            string strExtencion = string.Empty;
            bool bRespuesta = false;
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 0;
            strFileName = fImagen.PostedFile.FileName;
            strFileName = Path.GetFileName(strFileName);
            try
            {
                int iCoun = lExtencion.Count;
                strExtencion = strFileName.Substring(strFileName.LastIndexOf("."), strFileName.Length - strFileName.LastIndexOf("."));
                for (int i = 0; i < iCoun; i++)
                {
                    if (lExtencion[i].ToString().ToUpper().Equals(strExtencion.ToUpper()))
                    {
                        bRespuesta = true;
                        cParametros.Id = 1;
                        break;
                    }
                }
                if (bRespuesta)
                {
                    int iLenghtFileKB = 0;
                    try
                    {
                        iLenghtFileKB = (fImagen.PostedFile.ContentLength / 1024);
                    }
                    catch { }
                    if (iLenghtFileKB <= iLenght)
                    {
                        bRespuesta = true;
                        cParametros.Id = 1;
                    }
                    else
                    {
                        bRespuesta = false;
                        cParametros.Id = 0;
                        cParametros.Message = "El archivo tiene un tamaño de " + iLenghtFileKB.ToString() + " KB que es superiro a " + iLenght.ToString() + " KB ";
                        cParametros.Tipo = clsTipoError.Library;
                        cParametros.Severity = clsSeveridad.Moderada;
                        cParametros.Metodo = "Cargar imagenes";
                        cParametros.ViewMessage.Add("El archivo tiene un tamaño de " + iLenghtFileKB.ToString() + " KB que es superiro a " + iLenght.ToString() + " KB ");
                        cParametros.Sugerencia.Add("Por favor intente de nuevo");
                        ExceptionHandled.Publicar(cParametros);
                    }
                    //if (bRespuesta)
                    //{
                    //    //if (fImagen.Width.Value <= 480 && fImagen.Height.Value <= 640)
                    //    if (fImagen.Width.Value <= 200 && fImagen.Height.Value <= 200)
                    //    {
                    //        bRespuesta = true;
                    //        cParametros.Id = 1;
                    //    }
                    //    else
                    //    {
                    //        bRespuesta = false;
                    //        cParametros.Id = 0;
                    //        cParametros.Message = "El archivo no tiene las dimensiones establecidas";
                    //        cParametros.Tipo = clsTipoError.Library;
                    //        cParametros.Severity = clsSeveridad.Moderada;
                    //        cParametros.Metodo = "Cargar imagenes";
                    //        cParametros.ViewMessage.Add("El archivo tiene las dimensiones establecidas");
                    //        cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    //        ExceptionHandled.Publicar(cParametros);
                    //    }
                    //}
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "Extencion invalida";
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.Metodo = "Cargar imagenes";
                    cParametros.ViewMessage.Add("La extencion es inválida");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    ExceptionHandled.Publicar(cParametros);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "Cargar imagenes";
                ExceptionHandled.Publicar(cParametros);
            }
            if (bRespuesta)
            {
                try
                {
                    if (File.Exists(strRuta + "\\" + strFileName))
                    {
                        if (bolReplace)
                        {
                            File.Delete(strRuta + "\\" + strFileName);
                            fImagen.PostedFile.SaveAs(strRuta + "\\" + strFileName);
                            cParametros.Id = 1;
                        }
                        else
                        {
                            cParametros.Id = 0;
                            cParametros.Message = "El archivo ya existe";
                            cParametros.ViewMessage.Add("El archivo ya existe");
                            cParametros.Sugerencia.Add("Por favor intente de nuevo");
                            cParametros.Tipo = clsTipoError.Library;
                            cParametros.Severity = clsSeveridad.Moderada;
                            cParametros.Metodo = "Cargar imagenes";
                            ExceptionHandled.Publicar(cParametros);
                        }
                    }
                    else
                    {
                        fImagen.PostedFile.SaveAs(strRuta + "\\" + strFileName);
                        cParametros.Id = 1;
                    }
                    cParametros.DatoAdic = strFileName;
                }
                catch (Exception Ex)
                {
                    cParametros.Id = 0;
                    cParametros.Message = Ex.Message.ToString();
                    cParametros.Source = Ex.Source.ToString();
                    cParametros.Tipo = clsTipoError.Library;
                    cParametros.Severity = clsSeveridad.Moderada;
                    cParametros.StackTrace = Ex.StackTrace.ToString();
                    cParametros.Metodo = "Cargar imagenes";
                    cParametros.ViewMessage.Add("Problemas al cargar el archivo");
                    cParametros.Sugerencia.Add("Por favor intente de nuevo");
                    ExceptionHandled.Publicar(cParametros);
                }
            }
            return cParametros;
        }

        public DataSet AddDataset(DataTable dtData)
        {
            DataSet dsData = new DataSet();
            if (dtData != null)
            {
                if (dtData.DataSet != null)
                    dsData = dtData.DataSet;
                else
                    dsData.Tables.Add(dtData);
            }
            return dsData;
        }
        public DataSet AddDataset(DataTable dtData, DataSet dsData)
        {
            dsData = dtData.DataSet.Copy();
            return dsData;
        }
        public DataTable AddDataTable(DataSet dsData, int iPosTable)
        {
            DataTable dtData = new DataTable();
            dtData = dsData.Tables[iPosTable].Copy();
            return dtData;
        }
        public DataTable AddDataTable(DataTable dtData, DataRow drData)
        {
            dtData.Rows.Add(drData.ItemArray);
            return dtData;
        }
        public void AddDatasetNew(DataTable dtData, DataSet dsData)
        {
            DataTable dtDataNew = dtData.Clone();
            foreach (DataRow drData in dtData.Rows)
            {
                DataRow drNewRow = dtDataNew.NewRow();
                for (int i = 0; i < drData.ItemArray.Length; i++)
                {
                    drNewRow[i] = drData[i];
                }
                dtDataNew.Rows.Add(drNewRow);
                dtDataNew.AcceptChanges();
            }
            dsData.Tables.Add(dtDataNew);
        }
        public void EscribeLineaHtml(DataTable TblReserva, StreamWriter w, string fontColor, string strNombreResponsable)
        {
            StringBuilder html = new StringBuilder();
            #region tr
            html.AppendLine("<tr>");
            html.AppendLine("   <td>");
            html.AppendLine("Estimado(a) " + strNombreResponsable + " usted ha solicitado los siguientes servicios: ");
            html.AppendLine("</br>");
            #region division
            html.AppendLine("   <div id='DivisionCorreoUnion'> ");
            for (int i = 0; i < TblReserva.Rows.Count; i++)
            {   //Tabla por Plan
                #region tablaExterior
                html.AppendLine("<table width=836 cellspacing=0 cellpadding=0>");
                html.AppendLine("<tr>");
                html.AppendLine("<td class=planesSuperiorIzquierda></td>");
                html.AppendLine("<td class=planesSuperiorMedio></td>");
                html.AppendLine("<td class=planesSuperiorDerecha></td>");
                html.AppendLine("</tr>");
                html.AppendLine("<tr>");
                html.AppendLine("<td class=planesIzquierda></td>");
                html.AppendLine("<td class=planesMedio>");
                #region contenido
                //Contenido   
                html.AppendLine("<table width=940 cellspacing=0 cellpadding=2 class=textoNormal>");
                //Fila
                for (int c = 0; c < TblReserva.Columns.Count; c++)
                {
                    if (!TblReserva.Rows[i][c].ToString().Equals(""))
                    {
                        if (!TblReserva.Columns[c].ColumnName.Equals("datoUno")
                            && !TblReserva.Columns[c].ColumnName.Equals("datoDos")
                            && !TblReserva.Columns[c].ColumnName.Equals("datoUno")
                            && !TblReserva.Columns[c].ColumnName.Equals("datoTres")
                            && !TblReserva.Columns[c].ColumnName.Equals("datoCuatro")
                            && !TblReserva.Columns[c].ColumnName.Equals("AlternativoUno")
                            && !TblReserva.Columns[c].ColumnName.Equals("AlternativoDos")
                            && !TblReserva.Columns[c].ColumnName.Equals("AlternativoTres")
                            && !TblReserva.Columns[c].ColumnName.Equals("AlternativoCuatro")
                            && !TblReserva.Columns[c].ColumnName.Equals("AlternativoCinco")
                            && !TblReserva.Columns[c].ColumnName.Equals("TipoMoneda")
                            && !TblReserva.Columns[c].ColumnName.Equals("NombreTipoPlan")
                            && !TblReserva.Columns[c].ColumnName.Equals("ZonaGeografica")
                            && !TblReserva.Columns[c].ColumnName.Equals("seccionPublicacion")
                            && !TblReserva.Columns[c].ColumnName.Equals("TipoPlan")
                            && !TblReserva.Columns[c].ColumnName.Equals("Temporada")
                            && !TblReserva.Columns[c].ColumnName.Equals("CodigoTarifa")
                            && !TblReserva.Columns[c].ColumnName.Equals("CodigoPlan")
                            && !TblReserva.Columns[c].ColumnName.Equals("Ciudad")
                            && !TblReserva.Columns[c].ColumnName.Equals("TipoPropiedad")
                            && !TblReserva.Columns[c].ColumnName.Equals("Categoria")
                            && !TblReserva.Columns[c].ColumnName.Equals("Acomodacion")
                            && !TblReserva.Columns[c].ColumnName.Equals("NombreTabla")
                            && !TblReserva.Columns[c].ColumnName.Equals("TipoHabitacion")
                            && !TblReserva.Columns[c].ColumnName.Equals("TipoPasajero")

                            )
                        {
                            html.AppendLine("<tr>");
                            html.AppendLine("<td><strong>" + TblReserva.Columns[c].ColumnName + "</strong></td>");
                            html.AppendLine("<td><span>" + TblReserva.Rows[i][c].ToString() + "</span></td>");
                            html.AppendLine("<td></td>");
                            html.AppendLine("</tr>");
                        }

                    }
                }
                //Fin Fila
                html.AppendLine("</Table>");
                //Fin Contenido        
                #endregion
                html.AppendLine("</td>");
                html.AppendLine("<td class=planesDerecha></td>");
                html.AppendLine("</tr>");
                html.AppendLine("<tr>");
                html.AppendLine("   <td class=planesInferiorIzquierda></td>");
                html.AppendLine("   <td class=planesInferiorMedio></td>");
                html.AppendLine("   <td class=planesInferiorDerecha></td>");
                html.AppendLine("</tr>");
                html.AppendLine("<tr>");
                html.AppendLine("   <td colspan=3>&nbsp;</td>");
                html.AppendLine("</tr>");
                html.AppendLine("</table>");
                //Fin tabla por plan
                #endregion
            }
            html.AppendLine("       </div>");
            #endregion
            html.AppendLine("   </td>");
            html.AppendLine("</tr>");
            #endregion
            w.Write(html.ToString());
        }
        public void EscribeCabecera(StreamWriter w, string bgColor, string fontColor)
        {
            //"http://192.168.0.2/App_Themes/Imagenes/"
            string rutaImagenes = clsValidaciones.ObtenerUrlImages();
            //string rutaImagenes = "http://www.unionderepresentaciones.com/App_Themes/Imagenes/";
            StringBuilder html = new StringBuilder();
            bgColor = " bgcolor=\"Black\" ";
            fontColor = " style=\"font-family: Arial, Helvetica, sans-serif; font-size: 14px;color: White; font-weight: bold;\" ";
            html.AppendLine("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">");
            //INICIO HTML
            html.AppendLine("<html>");
            //INICIO ENCABEZADO
            html.AppendLine("<head>");
            html.AppendLine("<title>Generador de Reportes</title>");
            html.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />");
            //INICIA ESTILOS
            html.AppendLine("<style type=text/css>");

            html.AppendLine("body {");
            html.AppendLine("margin-left: 10px;");
            html.AppendLine("margin-top: 10px;");
            html.AppendLine("margin-right: 10px;");
            html.AppendLine("margin-bottom: 10px;");
            html.AppendLine("font-family:Arial, Helvetica, sans-serif;");
            html.AppendLine("background-image:url(fondo.jpg);");
            html.AppendLine("background-position:top;");
            html.AppendLine("background-repeat:repeat-x;");
            html.AppendLine("background-color:#FFFFFF;");
            html.AppendLine("font-size:11px;");
            html.AppendLine("}");

            html.AppendLine(".panelInferiorIzquierda{");
            html.AppendLine("width:7px;");
            html.AppendLine("height:7px;");
            html.AppendLine("background-image:url(" + rutaImagenes + "index_r22_c2.jpg);");
            html.AppendLine("background-repeat:no-repeat;");
            html.AppendLine("}");

            html.AppendLine(".panelInferiorMedio{");
            html.AppendLine("background-image:url(" + rutaImagenes + "index_r22_c3.jpg);");
            html.AppendLine("background-repeat:repeat-x;");
            html.AppendLine("height:7px;");
            html.AppendLine("}");

            html.AppendLine(".panelInferiorDerecha{");
            html.AppendLine("width:7px;");
            html.AppendLine("background-image:url(" + rutaImagenes + "index_r22_c23.jpg);");
            html.AppendLine("background-repeat:repeat-y;");
            html.AppendLine("}");

            html.AppendLine(".planesSuperiorIzquierda{");
            html.AppendLine("width:5px;");
            html.AppendLine("background-image:url(" + rutaImagenes + "index_r16_c12.jpg);");
            html.AppendLine("background-repeat:no-repeat;");
            html.AppendLine("}");

            html.AppendLine(".planesSuperiorMedio{");
            html.AppendLine("background-image:url(" + rutaImagenes + "index_r16_c13.jpg);");
            html.AppendLine("height:5px;");
            html.AppendLine("}");

            html.AppendLine(".planesSuperiorDerecha{");
            html.AppendLine("width:5px;");
            html.AppendLine("background-image:url(" + rutaImagenes + "index_r16_c19.jpg);");
            html.AppendLine("background-repeat:no-repeat;");
            html.AppendLine("}");

            html.AppendLine(".planesDerecha{");
            html.AppendLine("width:5px;");
            html.AppendLine("background-image:url(" + rutaImagenes + "index_r17_c19.jpg);");
            html.AppendLine("background-repeat:repeat-y;");
            html.AppendLine("}");

            html.AppendLine(".planesSuperiorIzquierda{");
            html.AppendLine("width:5px;");
            html.AppendLine("	  background-image:url(" + rutaImagenes + "index_r16_c12.jpg);");
            html.AppendLine("background-repeat:no-repeat;");
            html.AppendLine(" }");

            html.AppendLine(".fondoContenido{");
            html.AppendLine("background-color:#E3E7ED;");
            html.AppendLine("background-image:url(" + rutaImagenes + "fondoContenido.jpg);");
            html.AppendLine("background-repeat:repeat-x;");
            html.AppendLine("background-position:bottom;");
            html.AppendLine("text-align:center;");
            html.AppendLine("text-align:center;");
            html.AppendLine("}");

            html.AppendLine(".textoNormal{");
            html.AppendLine("color:#000000;");
            html.AppendLine("font-size:11px;");
            html.AppendLine("text-align:left;");
            html.AppendLine("font-weight:normal;");
            html.AppendLine("}");

            html.AppendLine("</style>");
            //FINALIZA ESTILOS
            html.AppendLine(" </head>");
            //FIN ENCABEZADO
            //INICIO CUERPO
            html.AppendLine("<body>");
            html.AppendLine("<table width=950 align=center cellspacing=4 cellpadding=0>");
            html.AppendLine("<tr>");
            html.AppendLine("<td><img alt='' src='" + rutaImagenes + "LogoUnion.jpg' /></td>");
            html.AppendLine("</tr>");
            w.Write(html.ToString());
        }
        public int DoHtml(string ruta, DataTable TblDatosReserva, string strNombreResponsable)
        {
            FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter w = new StreamWriter(fs);
            try
            {
                EscribeCabecera(w, null, null);
                EscribeLineaHtml(TblDatosReserva, w, null, strNombreResponsable);
                EscribePiePagina(w);
                w.Close();
                return 1;

            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                w.Close();
                return 0;
            }
        }
        public void EscribePiePagina(StreamWriter w)
        {

        }
        public int CrearHTML(string ruta, string documento)
        {
            FileStream fs = new FileStream(ruta, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter w = new StreamWriter(fs);
            try
            {
                w.Write(documento.ToString());
                w.Close();
                return 1;

            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                w.Close();
                return 0;
            }
        }

        public string ObtenerDireccionWeb(string strNombrePagina)
        {            
            string sPagina = strNombrePagina;
            int intSegementos = HttpContext.Current.Request.Url.Segments.Length - 1;
            StringBuilder strRuta = new StringBuilder("http://" + HttpContext.Current.Request.Url.Authority);
            for (int s = 0; s < intSegementos; s++)
            {
                strRuta.Append(HttpContext.Current.Request.Url.Segments[s]);
            }
            strRuta.Append(sPagina);
            return strRuta.ToString();
        }
        public string ObtenerRutaWeb(UserControl strPaginaActual, string strNombrePagina)
        {
            string sPagina = strNombrePagina;
            try
            {
                sPagina = AdicionaSesion(strNombrePagina);
            }
            catch { }
            int intSegementos = strPaginaActual.Request.Url.Segments.Length - 1;
            StringBuilder strRuta = new StringBuilder("http://" + strPaginaActual.Request.Url.Authority);
            for (int s = 0; s < intSegementos; s++)
            {
                strRuta.Append(strPaginaActual.Request.Url.Segments[s]);
            }
            strRuta.Append(sPagina);
            return strRuta.ToString();
        }
        public string ObtenerRutaWeb(Page strPaginaActual, string strNombrePagina)
        {
            string sPagina = strNombrePagina;
            try
            {
                sPagina = AdicionaSesion(strNombrePagina);
            }
            catch { }
            int intSegementos = strPaginaActual.Request.Url.Segments.Length - 1;
            StringBuilder strRuta = new StringBuilder("http://" + strPaginaActual.Request.Url.Authority);
            for (int s = 0; s < intSegementos; s++)
            {
                strRuta.Append(strPaginaActual.Request.Url.Segments[s]);
            }
            strRuta.Append(sPagina);
            return strRuta.ToString();
        }
        public string ObtenerRutaWeb(string strRutaHttp, string strNombrePagina)
        {
            string sPagina = strNombrePagina;
            try
            {
                sPagina = AdicionaSesion(strNombrePagina);
            }
            catch { }
            StringBuilder strRuta = new StringBuilder(strRutaHttp);
            strRuta.Append(sPagina);
            return strRuta.ToString();
        }
        public DataTable SelectDistinct
            (string[] pColumnNames,
            DataTable pOriginalTable,
            string sOrden)
        {
            //FILTRAR LOS RESULTADOS
            DataTable distinctTable = new DataTable();

            int numColumns = pColumnNames.Length;

            for (int i = 0; i < numColumns; i++)
            {

                distinctTable.Columns.Add(pColumnNames[i], pOriginalTable.Columns[pColumnNames[i]].DataType);

            }

            Hashtable trackData = new Hashtable();

            foreach (DataRow currentOriginalRow in pOriginalTable.Rows)
            {

                StringBuilder hashData = new StringBuilder();

                DataRow newRow = distinctTable.NewRow();

                for (int i = 0; i < numColumns; i++)
                {

                    hashData.Append(currentOriginalRow[pColumnNames[i]].ToString());

                    newRow[pColumnNames[i]] = currentOriginalRow[pColumnNames[i]];

                }

                if (!trackData.ContainsKey(hashData.ToString()))
                {

                    trackData.Add(hashData.ToString(), null);

                    distinctTable.Rows.Add(newRow);

                }
            }

            if (sOrden != null)
            {
                //ORDENAR LOS RESULTADOS
                DataTable dtOrden = distinctTable.Copy();
                DataRow[] drRegistros = dtOrden.Select(null, sOrden);
                if (drRegistros != null && drRegistros.Length > 0)
                {
                    distinctTable.Rows.Clear();

                    foreach (DataRow drRegistro in drRegistros)
                    {
                        DataRow drDistinct = distinctTable.NewRow();
                        drDistinct[0] = drRegistro[0];
                        distinctTable.Rows.Add(drDistinct);
                    }
                }
            }

            return distinctTable;
        }
        /// <summary>
        /// Metodo que retorna una tabla en forma randomica
        /// </summary>
        /// <param name="dtTable"> Tabla original</param>
        /// <param name="iCantidadMax"> Cantidad de registros</param>
        /// <param name="bCantidadMax"> Si se requiere llenar la tabla con la cantidad maima de registros o no (si= true, No= false)</param>
        /// <returns></returns>
        public DataTable dtRandom(DataTable dtTable, int iCantidadMax, bool bCantidadMax)
        {
            DataTable dtTableResp = new DataTable();
            dtTableResp = dtTable.Clone();

            try
            {
                if (dtTable.Rows.Count > 0)
                {
                    string[] iPosArr = new string[iCantidadMax];
                    for (int i = 0; i < iCantidadMax; i++)
                    {
                        iPosArr[i] = "-";
                    }
                    Random rndPos = new Random();
                    bool Incluye = true;
                    int iPos = 0;
                    int iCantidad = iCantidadMax;
                    if (iCantidad > dtTable.Rows.Count)
                        iCantidad = dtTable.Rows.Count;

                    int k = 0;
                    while (iCantidad > k)
                    {
                        Incluye = true;
                        iPos = rndPos.Next(dtTable.Rows.Count);
                        for (int j = 0; j < iCantidad; j++)
                        {
                            if (iPosArr[j].Trim() == iPos.ToString().Trim())
                            {

                                Incluye = false;

                            }
                        }
                        if (Incluye)
                        {
                            iPosArr[k] = iPos.ToString();
                            k++;
                        }
                    }
                    if (iCantidad < iCantidadMax)
                    {
                        int Contador = 0;
                        for (int m = iCantidad; m < iCantidadMax; m++)
                        {
                            iPosArr[m] = iPosArr[Contador];
                            Contador++;
                        }
                    }
                    if (bCantidadMax)
                    {
                        // si se requiere llenar todo el dataset con el maximo de posiciones
                        for (int m = 0; m < iCantidadMax; m++)
                        {
                            dtTableResp.Rows.Add(dtTable.Rows[int.Parse(iPosArr[m])].ItemArray);
                        }
                    }
                    else
                    {
                        // si no se requiere llenar todo el dataset con el maximo de posiciones
                        for (int m = 0; m < iCantidad; m++)
                        {
                            dtTableResp.Rows.Add(dtTable.Rows[int.Parse(iPosArr[m])].ItemArray);
                        }
                    }
                }
            }
            catch { }

            return dtTableResp;
        }
        public string ObtenerPlantillaHTML(string Ruta)
        {
            WebRequest mywebReq;
            WebResponse mywebResp;
            StreamReader sr;
            string strHTML;

            try
            {
                mywebReq = WebRequest.Create(Ruta);
                mywebReq.Timeout = 900000;
                mywebResp = mywebReq.GetResponse();
                sr = new StreamReader(mywebResp.GetResponseStream());
                strHTML = sr.ReadToEnd();
            }
            catch 
            {
                strHTML = null;
            }
            return strHTML;
        }
        #region  [ Codigo para recuperar estructuras de tablas ]
        public string DatasetStructura(DataSet dsDatos)
        {
            StringBuilder sRespuesta = new StringBuilder();
            try
            {
                sRespuesta.Append("Realciones: ");
                sRespuesta.Append(Environment.NewLine);
                sRespuesta.Append(Environment.NewLine);
                int iPos = dsDatos.Relations.Count;

                for (int i = 0; i < iPos; i++)
                {
                    sRespuesta.Append(dsDatos.Relations[i].RelationName);
                    sRespuesta.Append(Environment.NewLine);
                }

                sRespuesta.Append(Environment.NewLine);
                sRespuesta.Append(Environment.NewLine);
                sRespuesta.Append("Tablas: ");
                sRespuesta.Append(Environment.NewLine);
                sRespuesta.Append(Environment.NewLine);
                iPos = dsDatos.Tables.Count;
                int iPosRows = 0;
                int iPosConstrain = 0;
                int iPosPrimaryKey = 0;
                for (int i = 0; i < iPos; i++)
                {
                    iPosRows = dsDatos.Tables[i].Columns.Count;
                    sRespuesta.Append("Tabla:  " + dsDatos.Tables[i].TableName);
                    sRespuesta.Append(Environment.NewLine);
                    sRespuesta.Append(Environment.NewLine);
                    sRespuesta.Append("Columnas");
                    sRespuesta.Append(Environment.NewLine);
                    sRespuesta.Append(Environment.NewLine);
                    for (int j = 0; j < iPosRows; j++)
                    {
                        sRespuesta.Append(dsDatos.Tables[i].Columns[j].ColumnName);
                        sRespuesta.Append(Environment.NewLine);
                    }
                    iPosConstrain = dsDatos.Tables[i].Constraints.Count;
                    sRespuesta.Append(Environment.NewLine);
                    sRespuesta.Append(Environment.NewLine);
                    sRespuesta.Append("Constraints");
                    sRespuesta.Append(Environment.NewLine);
                    sRespuesta.Append(Environment.NewLine);
                    for (int k = 0; k < iPosConstrain; k++)
                    {
                        sRespuesta.Append(dsDatos.Tables[i].Constraints[k].ConstraintName);
                        sRespuesta.Append(Environment.NewLine);
                    }
                    iPosPrimaryKey = dsDatos.Tables[i].PrimaryKey.Length;
                    sRespuesta.Append(Environment.NewLine);
                    sRespuesta.Append(Environment.NewLine);
                    sRespuesta.Append("PrimaryKey");
                    sRespuesta.Append(Environment.NewLine);
                    sRespuesta.Append(Environment.NewLine);
                    for (int m = 0; m < iPosPrimaryKey; m++)
                    {
                        sRespuesta.Append("Name  :  " + dsDatos.Tables[i].PrimaryKey[m].Caption);
                        sRespuesta.Append(Environment.NewLine);
                        sRespuesta.Append("Column:  " + dsDatos.Tables[i].PrimaryKey[m].ColumnName);
                        sRespuesta.Append(Environment.NewLine);
                    }
                    sRespuesta.Append(Environment.NewLine);
                }
            }
            catch (Exception Ex)
            {
                clsParametros cMensaje = new clsParametros();
                cMensaje.Id = 0;
                cMensaje.Message = Ex.Message.ToString();
                cMensaje.Source = Ex.Source.ToString();
                cMensaje.Tipo = clsTipoError.Aplication;
                cMensaje.Severity = clsSeveridad.Moderada;
                cMensaje.StackTrace = Ex.StackTrace.ToString();
                ExceptionHandled.Publicar(cMensaje);
            }
            return sRespuesta.ToString(); ;
        }
        #endregion
        /*METODOPARA ADICIONAR LA RUTA DE LAS IMAGENE A UNA TABLA, SI LA VARIABLE BOOL ES TRUE CARGA EL CONTENIDO DE LA PAGINA*/
        public void ModificarTablaRPTImagenes(DataTable Tabla, UserControl PageSource, bool bCargarControl)
        {
            string Ruta = clsValidaciones.ObtenerUrlPlanes();
            int i = 0;
            while (i < Tabla.Rows.Count)
            {
                try
                {
                    if (!Tabla.Rows[i]["Imagen"].ToString().Equals(""))
                    {
                        Tabla.Rows[i]["Imagen"] = Ruta + Tabla.Rows[i]["Imagen"].ToString();
                    }
                    else
                    {
                        Tabla.Rows[i]["Imagen"] = Ruta + "spacer.gif";
                    }
                }
                catch
                {
                    if (!Tabla.Rows[i]["strImagen"].ToString().Equals(""))
                    {
                        Tabla.Rows[i]["strImagen"] = Ruta + Tabla.Rows[i]["strImagen"].ToString();
                    }
                    else
                    {
                        Tabla.Rows[i]["strImagen"] = Ruta + "spacer.gif";
                    }
                }
                i++;
            }
            if (bCargarControl)
            {
                clsControls.CargarControl(PageSource, Tabla);
            }
        }
        public void AddRows(DataTable TablaFinal, DataTable TablaOrigen, string sOrden)
        {
            try
            {
                TablaFinal.Merge(TablaOrigen);
                //DataRow[] drDatas = TablaOrigen.Select();
                //foreach (DataRow drData in drDatas)
                //{
                //    DataRow drDataNew = TablaFinal.NewRow();
                //    int iCount = drData.ItemArray.Length;
                //    for (int i = 0; i < iCount; i++)
                //    {
                //        drDataNew[i] = drData[i];
                //    }
                //    TablaFinal.Rows.Add(drDataNew);
                //}
                if (sOrden != null)
                {
                    TablaFinal.DefaultView.Sort = sOrden;
                }
            }
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.Complemento = "AddRows";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static string Codificar(string texto)
        {
            return Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(texto));
        }
        public static string DeCodificar(string texto)
        {
            return Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(texto));
        }
        public void abreVentana(string ventana, Page pageSource)
        {
            string Clientscript = "<script>window.open('" + ventana + "')</script>";

            ClientScriptManager cs = pageSource.ClientScript;
            if (!cs.IsStartupScriptRegistered("WOpen"))
            {
                cs.RegisterStartupScript(pageSource.GetType(), "WOpen", Clientscript);

            }
        }
        public string AdicionaSesion(string sPagina)
        {
            string sPaginaNew = sPagina;
            try
            {
                string sSesionId = new clsCacheControl().RecuperarSesionId();
                string sUnion = "?";
                string sSesion = "idSesion=";
                if (!sPagina.Contains(sSesion))
                {
                    if (sPagina.Contains(sUnion))
                        sUnion = "&";
                    sPaginaNew += sUnion + sSesion + sSesionId;
                }
            }
            catch { }
            return sPaginaNew;
        }
        public string AdicionaUser(string sPagina)
        {
            string sPaginaNew = sPagina;
            string sParam = string.Empty;
            try
            {
                bool bEntra = true;
                if (HttpContext.Current.Request.QueryString["idC"] != null)
                {
                    if (HttpContext.Current.Request.QueryString["idC"].ToString() != "0")
                    {
                        sParam = "idC=" + HttpContext.Current.Request.QueryString["idC"].ToString();
                        if (sParam.Contains(","))
                            sParam = sParam.Substring(0, sParam.IndexOf(","));
                        bEntra = false;
                    }
                }
                if (HttpContext.Current.Request.QueryString["idE"] != null)
                {
                    if (HttpContext.Current.Request.QueryString["idE"].ToString() != "0")
                    {
                        string sParamTemp = HttpContext.Current.Request.QueryString["idE"].ToString();
                        if (sParamTemp.Contains(","))
                            sParamTemp = sParamTemp.Substring(0, sParamTemp.IndexOf(","));
                        if (sParam.Length.Equals(0))
                            sParam = "idE=" + sParamTemp;
                        else
                            sParam += "&idE=" + sParamTemp;
                        bEntra = false;
                    }
                }
                if (bEntra)
                {
                    clsCache cCache = new csCache().cCache();
                    if (cCache != null)
                    {
                        if (cCache.Contacto != null)
                        {
                            if (cCache.Contacto != "0")
                            {
                                sParam = "idC=" + cCache.Contacto;
                                if (cCache.Empresa != null)
                                {
                                    if (cCache.Empresa != "0")
                                    {
                                        if (sParam.Length.Equals(0))
                                            sParam = "idE=" + cCache.Empresa;
                                        else
                                            sParam = "&idE=" + cCache.Empresa;
                                        bEntra = false;
                                    }
                                }
                            }
                            else
                            {
                                if (cCache.Empresa != null)
                                {
                                    if (cCache.Empresa != "0")
                                    {
                                        if (sParam.Length.Equals(0))
                                            sParam = "idE=" + cCache.Empresa;
                                        else
                                            sParam = "&idE=" + cCache.Empresa;
                                        bEntra = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (cCache.Empresa != null)
                            {
                                if (cCache.Empresa != "0")
                                    sParam = "idE=" + cCache.Empresa;
                                bEntra = false;
                            }
                        }
                    }
                }
                if (bEntra)
                {
                    sParam = "idE=" + clsValidaciones.GetKeyOrAdd("idEmpresa","3");
                }
                if (!sParam.Length.Equals(0))
                {
                    string sUnion = "?";
                    if (sPagina.Contains(sUnion))
                        sUnion = "&";
                    sPaginaNew += sUnion + sParam;
                }
            }
            catch { }
            return sPaginaNew;
        }
        public string EliminaSesion(string sPagina)
        {
            string sPaginaNew = sPagina;
            try
            {
                int iPosTotal = 0;
                int iPosIncial = sPagina.Length;
                if (sPagina.Contains("idSesion="))
                {
                    try
                    {
                        iPosIncial = sPagina.IndexOf("idSesion=");
                        iPosIncial--;
                    }
                    catch { }
                    string[] sLista1 = clsValidaciones.Lista(sPagina, "?");

                    if (sLista1 != null)
                    {
                        string[] sLista2 = clsValidaciones.Lista(sLista1[1], "&");
                        if (sLista2 != null)
                        {
                            for (int i = 0; i < sLista2.Length; i++)
                            {
                                if (sLista2[i].Contains("idSesion"))
                                {
                                    iPosTotal = sLista2[i].Length;
                                    iPosTotal++;
                                    break;
                                }
                            }
                        }
                    }
                    if (iPosTotal > 0)
                    {
                        sPaginaNew = sPagina.Remove(iPosIncial, iPosTotal);
                        if (sPaginaNew.Contains(".aspx&"))
                            sPaginaNew = sPaginaNew.Replace(".aspx&", ".aspx?");
                    }
                }
            }
            catch { }
            return sPaginaNew;
        }
    }
}
