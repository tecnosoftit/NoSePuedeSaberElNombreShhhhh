using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using Ssoft.ManejadorExcepciones;
using Ssoft.Utils;
using SsoftQuery.Reserva;
using SsoftQuery.Vuelos;
using System.IO;
using Ssoft.Pages.PaginaContactenos;

namespace Ssoft.Pages
{
    public class csUtilitarios
    {
        private const string sUtilitario0 = "";
        private const string sUtilitario1 = "http://www.xe.com/pca/";
        private const string sUtilitario2 = "http://maps.google.com";
        private const string sUtilitario3 = "http://www.weather.com/weather/today/Bogota+Colombia+COXX0004";
        private const string sUtilitario4 = "http://www.carlsonwagonlit.com.co/viajes/index.phtml?Di=3&Lin=L04";
        private const string sUtilitario5 = "";
        private const string sUtilitario6 = "";
        public csUtilitarios()
        {
        }
        public void setCargar(UserControl PageSource, CommandEventArgs e)
        {
            try
            {
                string sRuta = e.CommandArgument.ToString();
                if (sRuta.Contains("http://aliacorp.ssoftcolombia.com"))
                {
                    sRuta = sRuta.Replace("http://aliacorp.ssoftcolombia.com", clsValidaciones.GetKeyOrAdd("URL_APLICACION", "http://aliacorp.ssoftcolombia.com"));
                }
                HtmlGenericControl cPanel = (HtmlGenericControl)PageSource.FindControl("cPanel");
                StringBuilder dPanel = new StringBuilder();
                dPanel.AppendLine(" <iframe  id=iframe scrolling='auto' src='" + sRuta + "' frameBorder=0  width=100% height=700></iframe>");
                cPanel.Controls.Clear();
                cPanel.InnerHtml = dPanel.ToString();
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
                cParametros.Complemento = "Utilitarios ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public string SetBuscarPlantillaCorreo(string strtpoplantilla)
        {
            clsParametros cParametros = new clsParametros();
            string strEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "6");
            string shtml = "";
            try
            {

                if (new csCache().cCache() != null)
                {
                    strEmpresa = new csCache().cCache().Empresa;
                }
                if (clsValidaciones.GetKeyOrAdd("validavalorestaempresapadre", "true").Trim().ToUpper().Equals("TRUE"))
                {
                    strEmpresa = clsValidaciones.GetKeyOrAdd("idempresa", "6");
                }

                clsSerializer obj = new clsSerializer();
                string sUrl = sUrl = HttpContext.Current.Server.MapPath("../html/Correos/" + strEmpresa + "/" + strtpoplantilla + ".html");
                if (File.Exists(sUrl))
                {
                    shtml = obj.RecuperaFileGen(sUrl);
                }
                else
                {
                    ExceptionHandled.Publicar("la plantilla de correo ubicada en la Dir:" + sUrl + "No existe por favor vericar que la plantilla se halla creado");

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
                cParametros.Complemento = "setCorreos libreria Page csUtilitarios";
                ExceptionHandled.Publicar(cParametros);

                shtml = "";
            }
            return shtml;


        }
        public void setCorreos(string strReserva, string strFop, string TpoCorreo)
        {

            clsParametros cParametros = new clsParametros();
            string strCopias = "";
            string strCopiasOcultas = "";
            string strtpoplantilla = "";
            string strForm = "";
            string strTo = "";
            string strAsunto = "";
            string strEmpresa = "";

            if (new csCache() != null)
            {
                strEmpresa = new csCache().cCache().Empresa;
            }
            else
            {
                strEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "3");
            }

            DataTable dtCorreos = new CsConsultasVuelos().SPConsultaTabla("SPConultaPlantillaCorreos", new string[3] { TpoCorreo, strFop, strEmpresa });
            try
            {
                if (dtCorreos != null)
                {
                    strtpoplantilla = dtCorreos.Rows[0]["strArchivo"].ToString();
                    strForm = dtCorreos.Rows[0]["strform"].ToString();
                    strTo = "";
                    if (strTo == "" && HttpContext.Current.Session["$CorreoContacto"] != null)
                    {
                        strTo = HttpContext.Current.Session["$CorreoContacto"].ToString();
                    }
                    try
                    {
                        strCopias = dtCorreos.Rows[0]["strcopia"].ToString();
                    }
                    catch { }

                    try
                    {
                        strAsunto = dtCorreos.Rows[0]["strasunto"].ToString() + " " + strReserva;
                    }
                    catch
                    {
                        strAsunto = "Notificación de reserva" + " " + strReserva;
                    }

                    try
                    {
                        strCopiasOcultas = dtCorreos.Rows[0]["strcopiaoculta"].ToString();
                    }
                    catch { }

                    string shtml = SetBuscarPlantillaCorreo(strtpoplantilla);

                    if (shtml != "")
                    {

                        if (clsValidaciones.GetKeyOrAdd("TpoCorreoSabre", "RSABRE").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaReservaVuelos(shtml, strReserva);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoHotelesTT", "RTT").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaReservaHTT(shtml, strReserva);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }

                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoPlanes", "RPL").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaReservaPlanes(shtml, strReserva);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoContactenos", "CTS").Equals(TpoCorreo))
                        {
                            //string[] Correo = strReserva.Split('|');
                            //strTo = Correo[0];
                            shtml = SetCargaPlantillaContactenos(shtml);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoEnvioAmigo", "AMG").Equals(TpoCorreo))
                        {

                            string[] Correo = strReserva.Split('|');
                            strTo = Correo[0];

                            shtml = SetCargaPlantillaEnvioAmigo(shtml, Correo[1], Correo[2]);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }



                        new clsEmail().EnviarMensaje(strAsunto, OperacionEmail.Email, strTo, strCopias, strCopiasOcultas, FormatMail.PlantillaHTML, strForm, shtml);
                    }
                    else
                    {
                        ExceptionHandled.Publicar("El correo no fue enviado porq no se encontro la plantilla correspondiente");
                    }

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
                cParametros.Complemento = "setCorreos libreria Page csUtilitarios";
                ExceptionHandled.Publicar(cParametros);

            }


        }
        public void setCorreosHotelFail(string strReserva, string strFop, string TpoCorreo, string fto, string comentarios)
        {
            clsParametros cParametros = new clsParametros();
            string strCopias = "";
            string strCopiasOcultas = "";
            string strtpoplantilla = "";
            string strForm = "";
            string strTo = "";
            string strAsunto = "";
            string strEmpresa = "";

            if (new csCache() != null)
            {
                strEmpresa = new csCache().cCache().Empresa;
            }
            else
            {
                strEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "3");
            }

            DataTable dtCorreos = new CsConsultasVuelos().SPConsultaTabla("SPConultaPlantillaCorreos", new string[3] { TpoCorreo, strFop, strEmpresa });
            try
            {
                if (dtCorreos != null)
                {
                    strtpoplantilla = dtCorreos.Rows[0]["strArchivo"].ToString();
                    strForm = dtCorreos.Rows[0]["strform"].ToString();
                    strTo = fto;
                    if (strTo == "" && HttpContext.Current.Session["$CorreoContacto"] != null)
                    {
                        strTo = HttpContext.Current.Session["$CorreoContacto"].ToString();
                    }
                    try
                    {
                        strCopias = dtCorreos.Rows[0]["strcopia"].ToString();
                    }
                    catch { }

                    try
                    {
                        strAsunto = dtCorreos.Rows[0]["strasunto"].ToString() + " " + strReserva;
                    }
                    catch
                    {
                        strAsunto = "Notificación de reserva" + " " + strReserva;
                    }

                    try
                    {
                        strCopiasOcultas = dtCorreos.Rows[0]["strcopiaoculta"].ToString();
                    }
                    catch { }

                    string shtml = SetBuscarPlantillaCorreo(strtpoplantilla);
                    
                    if (shtml != "")
                    {

                        if (clsValidaciones.GetKeyOrAdd("TpoCorreoSabre", "RSABRE").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaReservaVuelos(shtml, strReserva);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoHotelesTT", "RTT").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaReservaHTT(shtml, strReserva);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }

                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoPlanes", "RPL").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaReservaPlanes(shtml, strReserva);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoContactenos", "CTS").Equals(TpoCorreo))
                        {
                            //string[] Correo = strReserva.Split('|');
                            //strTo = Correo[0];
                            shtml = SetCargaPlantillaContactenosHotel(shtml, comentarios);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoEnvioAmigo", "AMG").Equals(TpoCorreo))
                        {

                            string[] Correo = strReserva.Split('|');
                            strTo = Correo[0];

                            shtml = SetCargaPlantillaEnvioAmigo(shtml, Correo[1], Correo[2]);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }



                        new clsEmail().EnviarMensaje(strAsunto, OperacionEmail.Email, strTo, strCopias, strCopiasOcultas, FormatMail.PlantillaHTML, strForm, shtml);
                    }
                    else
                    {
                        ExceptionHandled.Publicar("El correo no fue enviado porq no se encontro la plantilla correspondiente");
                    }

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
                cParametros.Complemento = "setCorreos libreria Page csUtilitarios";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void setCorreos(string strReserva, string strFop, string TpoCorreo, params string[] parametrosAdicionales)
        {

            clsParametros cParametros = new clsParametros();
            string strCopias = "";
            string strCopiasOcultas = "";
            string strtpoplantilla = "";
            string strForm = "";
            string strTo = "";
            string strAsunto = "";
            string strEmpresa = "";

            if (new csCache() != null)
            {
                strEmpresa = new csCache().cCache().Empresa;
            }
            else
            {
                strEmpresa = clsValidaciones.GetKeyOrAdd("idEmpresa", "3");
            }

            DataTable dtCorreos = new CsConsultasVuelos().SPConsultaTabla("SPConultaPlantillaCorreos", new string[3] { TpoCorreo, strFop, strEmpresa });
            try
            {
                if (dtCorreos != null)
                {
                    strtpoplantilla = dtCorreos.Rows[0]["strArchivo"].ToString();
                    strForm = dtCorreos.Rows[0]["strform"].ToString();
                    strTo = new csCache().cCache().Email.ToString();
                    if (strTo == "" && HttpContext.Current.Session["$CorreoContacto"] != null)
                    {
                        strTo = HttpContext.Current.Session["$CorreoContacto"].ToString();
                    }
                    try
                    {
                        strCopias = dtCorreos.Rows[0]["strcopia"].ToString();
                    }
                    catch { }

                    try
                    {
                        strAsunto = dtCorreos.Rows[0]["strasunto"].ToString() + " " + strReserva;
                    }
                    catch
                    {
                        strAsunto = "Notificación de reserva" + " " + strReserva;
                    }

                    try
                    {
                        strCopiasOcultas = dtCorreos.Rows[0]["strcopiaoculta"].ToString();
                    }
                    catch { }

                    string shtml = SetBuscarPlantillaCorreo(strtpoplantilla);

                    if (shtml != "")
                    {

                        if (clsValidaciones.GetKeyOrAdd("TpoCorreoSabre", "RSABRE").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaReservaVuelos(shtml, strReserva);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoHotelesTT", "RTT").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaReservaHTT(shtml, strReserva);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }

                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoPlanes", "RPL").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaReservaPlanes(shtml, strReserva);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoContactenos", "CTS").Equals(TpoCorreo))
                        {
                            shtml = SetCargaPlantillaContactenos(shtml);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoEnvioAmigo", "AMG").Equals(TpoCorreo))
                        {

                            string[] Correo = strReserva.Split('|');
                            strTo = Correo[0];

                            shtml = SetCargaPlantillaEnvioAmigo(shtml, Correo[1], Correo[2]);
                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoEnvioAmigoPDF", "AMGPDF").Equals(TpoCorreo))
                        {
                            //string[] Correo = strReserva.Split('|');
                            //strTo = Correo[0];
                            //string tunombre = parametrosAdicionales[0];
                            //string sunombre = parametrosAdicionales[1];
                            //string sumail = parametrosAdicionales[2];
                            //string nombrePlan = parametrosAdicionales[3];
                            //string link = parametrosAdicionales[4];
                            //strAsunto = "Información compartida por " + tunombre;
                            //shtml = SetCargaPlantillaEnvioAmigoPDF(shtml, tunombre, sunombre, nombrePlan, link);
                            //if (shtml == "")
                            //{
                            //    ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                            //    return;
                            //}
                        }//Plantilla quiero que me contacten
                        else if (clsValidaciones.GetKeyOrAdd("TpoCorreoQuieroQueMeContacten", "QMC").Equals(TpoCorreo))
                        {

                            string[] Correo = strReserva.Split('|');
                            strTo = Correo[0];
                            string nombre = parametrosAdicionales[0];
                            string correo = parametrosAdicionales[1];
                            string telefono = parametrosAdicionales[2];
                            string nombrePlan = parametrosAdicionales[3];
                            string comentarios = parametrosAdicionales[4];

                            //Guardamos el formulario en la base datos
                            csContactenos objCsContactenos = new csContactenos();
                            objCsContactenos.GuardarDatosBuzonQuieroQueMeContacten(nombre, correo, telefono, comentarios, nombrePlan);

                            //Obtenemos el html de la plantilla
                            shtml = SetCargaPlantillaQuieroQueMeContacten(shtml, nombre, correo, telefono, nombrePlan, comentarios);

                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }
                        }
                        else if (clsValidaciones.GetKeyOrAdd("TipoCorreoRegistroEmpresa", "TCRE").Equals(TpoCorreo))
                        {
                            string[] Correo = strReserva.Split('|');
                            strTo = Correo[0];
                            string razonSocial = parametrosAdicionales[0];
                            string nit = parametrosAdicionales[1];
                            string direccion = parametrosAdicionales[2];
                            string ciudad = parametrosAdicionales[3];
                            string telefono = parametrosAdicionales[4];
                            string contacto = parametrosAdicionales[5];
                            string cargo = parametrosAdicionales[6];
                            string correoElectronico = parametrosAdicionales[7];

                            //Guardamos el formulario en la base datos
                            csContactenos objCsContactenos = new csContactenos();
                            objCsContactenos.GuardarDatosBuzonRegistroEmpresa(razonSocial, nit, direccion, ciudad, telefono, contacto, cargo, correoElectronico);

                            //Obtenemos el html de la plantilla
                            shtml = SetCargaPlantillaRegistroEmpresa(shtml, razonSocial, nit, direccion, ciudad, telefono, contacto, cargo, correoElectronico);

                            if (shtml == "")
                            {
                                ExceptionHandled.Publicar("El correo no fue enviado por problemas en la creacion del html correspondiente");
                                return;
                            }

                        }
                        //Enviamos el formuario
                        new clsEmail().EnviarMensaje(strAsunto, OperacionEmail.Email, strTo, strCopias, strCopiasOcultas, FormatMail.PlantillaHTML, strForm, shtml);
                    }
                    else
                    {
                        ExceptionHandled.Publicar("El correo no fue enviado porq no se encontro la plantilla correspondiente");
                    }

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
                cParametros.Complemento = "setCorreos libreria Page csUtilitarios";
                ExceptionHandled.Publicar(cParametros);

            }


        }
        /// <summary>
        /// Plantilla quiero que me contacten.
        /// </summary>
        /// <param name="strHtml"></param>
        /// <param name="nombre"></param>
        /// <param name="correo"></param>
        /// <param name="telefono"></param>
        /// <param name="nombrePlan"></param>
        /// <returns></returns>
        public string SetCargaPlantillaRegistroEmpresa(
            string strHtml, string razonSocial, string nit, string direccion,
            string ciudad, string telefono, string contacto, string cargo, string correoElectronico)
        {
            List<string> listaParametros = new List<string>();

            try
            {
                strHtml = strHtml.Replace("@RazonSocial", razonSocial);
                strHtml = strHtml.Replace("@Nit", nit);
                strHtml = strHtml.Replace("@Direccion", direccion);
                strHtml = strHtml.Replace("@Ciudad", ciudad);
                strHtml = strHtml.Replace("@Telefono", telefono);
                strHtml = strHtml.Replace("@Contacto", contacto);
                strHtml = strHtml.Replace("@Cargo", cargo);
                strHtml = strHtml.Replace("@CorreoElectronico", correoElectronico);
            }
            catch
            {
                strHtml = "";
            }

            return strHtml;
        }

        public string SetCargaPlantillaQuieroQueMeContacten(
            string strHtml, string nombre, string correo, string telefono,
            string nombrePlan, string comentarios)
        {

            string stblsegmentos = "";
            List<string> listaParametros = new List<string>();

            try
            {
                stblsegmentos = "Nombre:&nbsp;" + nombre + "<br/>";
                stblsegmentos = stblsegmentos + "Correo:&nbsp;" + correo + "<br/>";
                stblsegmentos = stblsegmentos + "Telefono:&nbsp;" + telefono + "<br/>";
                stblsegmentos = stblsegmentos + "Nombre del plan:&nbsp;" + nombrePlan + "<br/>";
                stblsegmentos = stblsegmentos + "Comentarios:&nbsp;" + comentarios + "<br/>";
                strHtml = strHtml.Replace("||", stblsegmentos);
            }
            catch
            {
                strHtml = "";
            }

            return strHtml;
        }

        public string ConvertDataTableTableHtml(DataTable dt)
        {
            string sHtml = "";
            try
            {
                GridView gv = new GridView();
                gv.DataSource = dt;
                gv.DataBind();
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                sHtml = htw.InnerWriter.ToString();


            }
            catch { }


            return sHtml;
        }
        public string SetCargaPlantillaReservaVuelos(string strHtml, string strReserva)
        {
            string strPasajeros = "";
            string stblsegmentos = "";
            string strloc = "";
            string intFormapago = "";
            string strtextospago = "";
            DataTable dtSegmentos = new DataTable();

            try
            {

                strloc = new CsConsultasVuelos().ConsultaCodigo(strReserva, "TBLRESMASTER", "INTPROYECTO", "STRRESERVA");

                if (strloc != "")
                {
                    DataSet dsDetallereserva = new CsConsultasVuelos().EjecutaSpProcedimiento("SPConsultaDetallereservaGeneral", new string[2] { new csCache().cCache().Idioma, strloc });
                    DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPConsultaPasajerosReserva", new string[3] { new csCache().cCache().Idioma, strReserva, "1" });

                    strHtml = strHtml.Replace("Nrecord", strReserva);
                    strHtml = strHtml.Replace("Nnombre", new csCache().cCache().Nombres + " " + new csCache().cCache().Apellidos);
                    strHtml = strHtml.Replace("nloc", strloc);


                    DataTable dtPasajeros = dt.DefaultView.ToTable(false, "strNombre", "Moneda", "Total");

                    dtPasajeros.Columns["strNombre"].ColumnName = "Nombre";
                    strPasajeros = ConvertDataTableTableHtml(dtPasajeros);
                    strPasajeros = strPasajeros.Replace("<table cellspacing=" + '"' + "0" + '"' + " rules=" + '"' + "all" + '"' + " border=" + '"' + "1" + '"' + " style=" + '"' + "border-collapse:collapse;" + '"' + ">", "<table cellspacing=" + '"' + "0" + '"' + " cellpadding=" + '"' + "2" + '"' + " width=" + '"' + "100%" + '"' + " style=" + '"' + "text-align: left; font-size: 12px; margin-left: auto; margin-right: auto;border: solid 1px #ccc;" + '"' + ">");
                    strPasajeros = strPasajeros.Trim().Replace("\r", "");
                    strPasajeros = strPasajeros.Trim().Replace("\n", "");
                    strPasajeros = strPasajeros.Trim().Replace("\t", "");
                    strPasajeros = strPasajeros.Replace("<tr><th scope=" + '"' + "col" + '"' + ">Nombre</th><th scope=" + '"' + "col" + '"' + ">Moneda</th><th scope=" + '"' + "col" + '"' + ">Total</th></tr>", "<tr bgcolor=" + '"' + "gray" + '"' + "><th scope=" + '"' + "col" + '"' + ">" + "<font color=" + '"' + "White" + '"' + "><b>Reserva a nombre de</b></font>" + "</th><th scope=" + '"' + "col" + '"' + ">" + "<font color=" + '"' + "White" + '"' + "><b>Moneda</b></font>" + "</th><th scope=" + '"' + "col" + '"' + ">" + "<font color=" + '"' + "White" + '"' + "><b>Valor Total</b></font>" + "</th></tr>");


                    DataTable dtDetalle = dsDetallereserva.Tables[0];


                    intFormapago = dtDetalle.Rows[0]["intformapago"].ToString();


                    strtextospago = new CsConsultasVuelos().ConsultaCodigo(intFormapago + "'" + " AND " + " STRIDIOMA='" + new csCache().cCache().Idioma, "tblTextosFop", "strDescripcion", "intFormapago");


                    dtDetalle = dtDetalle.DefaultView.ToTable(false, "strReserva", "label101", "texto101", "label102", "texto102");


                    strHtml = strHtml.Replace("fpago", dtDetalle.Rows[0]["texto102"].ToString());
                    strHtml = strHtml.Replace("femision", dtDetalle.Rows[0]["texto101"].ToString());

                    strHtml = strHtml.Replace("stelefono", new csCache().cCache().Telefono);
                    strHtml = strHtml.Replace("sCorreo", new csCache().cCache().Email);


                    DataTable dt2 = dsDetallereserva.Tables[1];
                    DataTable dt3 = dsDetallereserva.Tables[2];

                    strHtml = strHtml.Replace("sValortotal", dt3.Rows[0]["valor"].ToString());
                    strHtml = strHtml.Replace("sMoneda", dt3.Rows[0]["moneda"].ToString());

                    if (HttpContext.Current.Session["$tsegmentos"] != null)
                    {

                        dtSegmentos = (DataTable)HttpContext.Current.Session["$tsegmentos"];

                        try
                        {
                            dtSegmentos.Columns.Add("Origen", typeof(String));
                            dtSegmentos.Columns.Add("Destino", typeof(String));
                            dtSegmentos.Columns.Add("Fecha", typeof(String));

                            dtSegmentos.Columns.Add("Hora Salida", typeof(String));
                            dtSegmentos.Columns.Add("Hora Llegada", typeof(String));
                        }
                        catch { }

                        string[] strFecha = null;
                        for (int i = 0; i < dtSegmentos.Rows.Count; i++)
                        {

                            strFecha = dtSegmentos.Rows[i]["dtmFechaSalida"].ToString().Split(' ');
                            string sfecha = strFecha[0];
                            string sHora = strFecha[1];
                            dtSegmentos.Rows[i]["Hora Salida"] = sHora;
                            dtSegmentos.Rows[i]["Fecha"] = sfecha;
                            dtSegmentos.Rows[i]["Origen"] = dtSegmentos.Rows[i]["strAeropuerto_salida"].ToString().ToUpper() + " " + dtSegmentos.Rows[i]["code_aeropuerto_Salida"].ToString();
                            dtSegmentos.Rows[i]["Destino"] = dtSegmentos.Rows[i]["strAeropuerto_llegada"].ToString().ToUpper() + " " + dtSegmentos.Rows[i]["code_aeropuerto_llegada"].ToString();

                            strFecha = dtSegmentos.Rows[i]["dtmFechallegada"].ToString().Split(' ');
                            sHora = strFecha[1];
                            dtSegmentos.Rows[i]["Hora Llegada"] = sHora;

                        }

                        dtSegmentos = dtSegmentos.DefaultView.ToTable(false, "Fecha", "strNombre_Aerolinea", "flightNumber", "Origen", "Destino", "Hora Salida", "Hora Llegada");
                        dtSegmentos.Columns["Fecha"].ColumnName = "Fecha de Inicio de Viaje";
                        dtSegmentos.Columns["strNombre_Aerolinea"].ColumnName = "Aerolínea";
                        dtSegmentos.Columns["flightNumber"].ColumnName = "Número de Vuelo";





                        stblsegmentos = "<table>";
                        for (int b = 0; b < dtSegmentos.Rows.Count; b++)
                        {

                            stblsegmentos += "<tr style=" + '"' + "background-color: #CCC;" + '"' + ">";
                            stblsegmentos += "<td style=" + '"' + "width: 180px;" + '"' + ">";
                            stblsegmentos += "<span>";
                            stblsegmentos += "Itinerario:";
                            stblsegmentos += "</span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += " <td style=" + '"' + "width: 700px;" + '"' + ">";
                            stblsegmentos += "<span>" + Convert.ToString(b + 1) + "</span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += "</tr>";


                            for (int c = 0; c < dtSegmentos.Columns.Count; c++)
                            {

                                stblsegmentos += "<tr>";

                                stblsegmentos += "<td>";
                                stblsegmentos += dtSegmentos.Columns[c].ColumnName;
                                stblsegmentos += "</td>";
                                stblsegmentos += "<td>";
                                stblsegmentos += dtSegmentos.Rows[b][c].ToString();
                                stblsegmentos += "</td>";

                                stblsegmentos += "</tr>";
                            }
                            stblsegmentos += "<tr>";
                            stblsegmentos += "<td>";
                            stblsegmentos += "<span></span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += "<td>";
                            stblsegmentos += "<span></span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += "</tr>";

                        }
                        stblsegmentos += "</table>";
                    }




                    strHtml = strHtml.Replace("||", stblsegmentos);
                    strHtml = strHtml.Replace("??", strPasajeros);

                    if (strtextospago != "")
                    {
                        strHtml = strHtml.Replace("sTextoformapago", strtextospago);
                    }
                    else
                    {
                        strHtml = strHtml.Replace("sTextoformapago", "Se ha creado la reserva descrita a continuación,  para ser pagada con débito a una cuenta bancaria. TuTiquete SA emitirá el(los) tiquete(s) y Ordenes de servicio una vez la transacción de pago haya sido realizada y verificada en bancos.");

                    }

                }

            }
            catch (Exception Ex)
            {
                ExceptionHandled.Publicar(Ex.Message);
                strHtml = "";
            }


            return strHtml;
        }
        public string SetCargaPlantillaReservaHTT(string strHtml, string strReserva)
        {
            string strPasajeros = "";
            string stblsegmentos = "";
            string strloc = "";
            string intFormapago = "";
            string strtextospago = "";
            string strNombrehotel = "";
            DataTable dtSegmentos = new DataTable();

            try
            {

                strloc = new CsConsultasVuelos().ConsultaCodigo(strReserva, "TBLRESMASTER", "INTPROYECTO", "STRRESERVA");

                if (strloc != "")
                {
                    DataSet dsDetallereserva = new CsConsultasVuelos().EjecutaSpProcedimiento("SPConsultaDetallereservaGeneral", new string[2] { new csCache().cCache().Idioma, strloc });
                    DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPConsultaPasajerosReserva", new string[3] { new csCache().cCache().Idioma, strReserva, "1" });

                    strHtml = strHtml.Replace("Nrecord", strReserva);
                    strHtml = strHtml.Replace("Nnombre", new csCache().cCache().Nombres + " " + new csCache().cCache().Apellidos);
                    strHtml = strHtml.Replace("nloc", strloc);


                    DataTable dtPasajeros = dt.DefaultView.ToTable(false, "strNombre", "Moneda", "Total");

                    dtPasajeros.Columns["strNombre"].ColumnName = "Nombre";
                    strPasajeros = ConvertDataTableTableHtml(dtPasajeros);
                    strPasajeros = strPasajeros.Replace("<table cellspacing=" + '"' + "0" + '"' + " rules=" + '"' + "all" + '"' + " border=" + '"' + "1" + '"' + " style=" + '"' + "border-collapse:collapse;" + '"' + ">", "<table cellspacing=" + '"' + "0" + '"' + " cellpadding=" + '"' + "2" + '"' + " width=" + '"' + "100%" + '"' + " style=" + '"' + "text-align: left; font-size: 12px; margin-left: auto; margin-right: auto;border: solid 1px #ccc;" + '"' + ">");
                    strPasajeros = strPasajeros.Trim().Replace("\r", "");
                    strPasajeros = strPasajeros.Trim().Replace("\n", "");
                    strPasajeros = strPasajeros.Trim().Replace("\t", "");
                    strPasajeros = strPasajeros.Replace("<tr><th scope=" + '"' + "col" + '"' + ">Nombre</th><th scope=" + '"' + "col" + '"' + ">Moneda</th><th scope=" + '"' + "col" + '"' + ">Total</th></tr>", "<tr bgcolor=" + '"' + "gray" + '"' + "><th scope=" + '"' + "col" + '"' + ">" + "<font color=" + '"' + "White" + '"' + "><b>Reserva a nombre de</b></font>" + "</th><th scope=" + '"' + "col" + '"' + ">" + "<font color=" + '"' + "White" + '"' + "><b>Moneda</b></font>" + "</th><th scope=" + '"' + "col" + '"' + ">" + "<font color=" + '"' + "White" + '"' + "><b>Valor Total</b></font>" + "</th></tr>");


                    DataTable dtDetalle = dsDetallereserva.Tables[0];


                    intFormapago = dtDetalle.Rows[0]["intformapago"].ToString();


                    strtextospago = new CsConsultasVuelos().ConsultaCodigo(intFormapago + "'" + " AND " + " STRIDIOMA='" + new csCache().cCache().Idioma, "tblTextosFop", "strDescripcion", "intFormapago");


                    dtDetalle = dtDetalle.DefaultView.ToTable(false, "strReserva", "label101", "texto101", "label102", "texto102");


                    strHtml = strHtml.Replace("fpago", dtDetalle.Rows[0]["texto102"].ToString());
                    strHtml = strHtml.Replace("femision", dtDetalle.Rows[0]["texto101"].ToString());

                    strHtml = strHtml.Replace("stelefono", new csCache().cCache().Telefono);
                    strHtml = strHtml.Replace("sCorreo", new csCache().cCache().Email);


                    DataTable dt2 = dsDetallereserva.Tables[1];
                    DataTable dt3 = dsDetallereserva.Tables[2];

                    strHtml = strHtml.Replace("sValortotal", dt3.Rows[0]["valor"].ToString());
                    strHtml = strHtml.Replace("sMoneda", dt3.Rows[0]["moneda"].ToString());

                    if (HttpContext.Current.Session["$tsegmentosHotel"] != null)
                    {
                        dtSegmentos = (DataTable)HttpContext.Current.Session["$tsegmentosHotel"];
                        strNombrehotel = dtSegmentos.Rows[0]["Name"].ToString();

                        try
                        {
                            dtSegmentos.Columns.Add("Numero de noches", typeof(String));
                        }
                        catch { }

                        dtSegmentos.Columns["intpasajeros"].ColumnName = "Cantidad huespedes";
                        dtSegmentos = dtSegmentos.DefaultView.ToTable(false, "Destination_Code", "Date_From_YMD", "Date_To_YMD", "Numero de noches", "Cantidad huespedes", "description");
                        dtSegmentos.Columns["Date_From_YMD"].ColumnName = "Fecha de Llegada";
                        dtSegmentos.Columns["Date_To_YMD"].ColumnName = "Fecha de Salida";
                        dtSegmentos.Columns["Destination_Code"].ColumnName = "Ciudad";
                        dtSegmentos.Columns["description"].ColumnName = "Observaciones";




                        DataTable dtreserva = new CsConsultasVuelos().Consultatabla("SELECT StrDescription FROM TBLCIUDADES INNER JOIN TBLCIUDADESIDIOMAS ON TBLCIUDADESIDIOMAS.INTCODE=TBLCIUDADES.INTCODE AND TBLCIUDADESIDIOMAS.STRIDIOMA='" + new csCache().cCache().Idioma + "' WHERE STRCITYCODE='" + dtSegmentos.Rows[0]["Ciudad"].ToString() + "'");
                        if (dtreserva != null)
                        {
                            if (dtreserva.Rows.Count > 0)
                            {
                                dtSegmentos.Rows[0]["Ciudad"] = dtreserva.Rows[0]["StrDescription"].ToString();
                            }
                        }

                        try
                        {
                            string[] fcha = null;
                            fcha = dtSegmentos.Rows[0]["Fecha de Llegada"].ToString().Split('/');

                            DateTime oldDate = new DateTime(Convert.ToInt32(fcha[0]), Convert.ToInt32(fcha[1]), Convert.ToInt32(fcha[2]));

                            fcha = dtSegmentos.Rows[0]["Fecha de Salida"].ToString().Split('/');

                            DateTime newDate = new DateTime(Convert.ToInt32(fcha[0]), Convert.ToInt32(fcha[1]), Convert.ToInt32(fcha[2]));

                            TimeSpan ts = newDate - oldDate;
                            int differenceInDays = ts.Days;
                            dtSegmentos.Rows[0]["Numero de noches"] = differenceInDays;
                        }
                        catch { }

                        stblsegmentos = "<table>";
                        for (int b = 0; b < dtSegmentos.Rows.Count; b++)
                        {
                            stblsegmentos += "<tr style=" + '"' + "background-color: #CCC;" + '"' + ">";
                            stblsegmentos += "<td style=" + '"' + "width: 180px;" + '"' + ">";
                            stblsegmentos += "<span>";
                            stblsegmentos += "Hotel:";
                            stblsegmentos += "</span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += " <td style=" + '"' + "width: 700px;" + '"' + ">";
                            stblsegmentos += "<span>" + strNombrehotel + "</span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += "</tr>";


                            for (int c = 0; c < dtSegmentos.Columns.Count; c++)
                            {
                                stblsegmentos += "<tr>";

                                stblsegmentos += "<td>";
                                stblsegmentos += dtSegmentos.Columns[c].ColumnName;
                                stblsegmentos += "</td>";
                                stblsegmentos += "<td>";
                                stblsegmentos += dtSegmentos.Rows[b][c].ToString();
                                stblsegmentos += "</td>";

                                stblsegmentos += "</tr>";
                            }
                            stblsegmentos += "<tr>";
                            stblsegmentos += "<td>";
                            stblsegmentos += "<span></span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += "<td>";
                            stblsegmentos += "<span></span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += "</tr>";

                        }
                        stblsegmentos += "</table>";
                    }




                    strHtml = strHtml.Replace("||", stblsegmentos);
                    strHtml = strHtml.Replace("??", strPasajeros);

                    if (strtextospago != "")
                    {
                        strHtml = strHtml.Replace("sTextoformapago", strtextospago);
                    }
                    else
                    {
                        strHtml = strHtml.Replace("sTextoformapago", "Se ha creado la reserva descrita a continuación,  para ser pagada con débito a una cuenta bancaria. TuTiquete SA emitirá el(los) tiquete(s) y Ordenes de servicio una vez la transacción de pago haya sido realizada y verificada en bancos.");

                    }

                }

            }
            catch
            {

                strHtml = "";
            }


            return strHtml;
        }
        public string SetCargaPlantillaContactenos(string strHtml)
        {

            string stblsegmentos = "";
            List<string> listaParametros = new List<string>();

            try
            {
                if (HttpContext.Current.Session["$DatosContacto"] != null)
                {

                    listaParametros = (List<string>)HttpContext.Current.Session["$DatosContacto"];

                    stblsegmentos = "Nombres:&nbsp;" + listaParametros[2] + "<br/>";
                    if (listaParametros[3] != "" && listaParametros[3] != "..")
                    {
                        stblsegmentos = stblsegmentos + "Apellido:&nbsp;" + listaParametros[3] + "<br/>";
                    }
                    if (listaParametros[5] != "" && listaParametros[5] != "..")
                    {
                        stblsegmentos = stblsegmentos + "Telefono:&nbsp;" + listaParametros[5] + "<br/>";
                    }
                    stblsegmentos = stblsegmentos + "Pais:&nbsp;" + listaParametros[7] + "<br/>";
                    stblsegmentos = stblsegmentos + "Tipo Mensaje:&nbsp;" + listaParametros[0] + "<br/>";
                    stblsegmentos = stblsegmentos + "Tema mensaje:&nbsp;" + listaParametros[1] + "<br/>";
                    stblsegmentos = stblsegmentos + "Correo:&nbsp;" + listaParametros[8] + "<br/>";
                    stblsegmentos = stblsegmentos + "Comentarios:&nbsp;" + listaParametros[6] + "<br/>";
                    strHtml = strHtml.Replace("||", stblsegmentos);
                }
            }
            catch
            {

                strHtml = "";
            }


            return strHtml;
        }
        public string SetCargaPlantillaContactenosHotel(string strHtml, string txtComentarios)
        {

            string stblsegmentos = "";
            List<string> listaParametros = new List<string>();

            try
            {
                if (HttpContext.Current.Session["$DatosContacto"] != null)
                {

                    listaParametros = (List<string>)HttpContext.Current.Session["$DatosContacto"];

                    stblsegmentos = "Nombres:&nbsp;" + listaParametros[0] + "<br/>";
                    stblsegmentos = stblsegmentos + "Apellido:&nbsp;" + listaParametros[1] + "<br/>";
                    //if (listaParametros[3] != "" && listaParametros[3] != "..")
                    //{
                    //    stblsegmentos = stblsegmentos + "Apellido:&nbsp;" + listaParametros[3] + "<br/>";
                    //}
                    //if (listaParametros[5] != "" && listaParametros[5] != "..")
                    //{
                    //    stblsegmentos = stblsegmentos + "Telefono:&nbsp;" + listaParametros[3] + "<br/>";
                    //}
                    stblsegmentos = stblsegmentos + "Telefono:&nbsp;" + listaParametros[3] + "<br/>";
                    //stblsegmentos = stblsegmentos + "Pais:&nbsp;" + listaParametros[4] + "<br/>";
                    stblsegmentos = stblsegmentos + "Tipo Mensaje:&nbsp;" + listaParametros[5] + "<br/>";
                    stblsegmentos = stblsegmentos + "Tema mensaje:&nbsp;" + listaParametros[6] + "<br/>";
                    stblsegmentos = stblsegmentos + "Correo:&nbsp;" + listaParametros[8] + "<br/>";
                    stblsegmentos = stblsegmentos + "Comentarios:&nbsp;" + txtComentarios + "<br/>";
                    strHtml = strHtml.Replace("||", stblsegmentos);
                }
            }
            catch
            {

                strHtml = "";
            }


            return strHtml;
        }
        public string SetCargaPlantillaEnvioAmigo(string strHtml, string strPlan, string user)
        {

            string strPasajeros = "";

            DataTable dtSegmentos = new DataTable();
            string strDatos = "";
            try
            {

                strHtml = strHtml.Replace("DATOSPASAJEROS", strPasajeros);

                strHtml = strHtml.Replace("Nnombre", user);

                DataTable dtdatos = new CsConsultasVuelos().SPConsultaTabla("SPConsultaAdicionalesPlan", new string[3] { strPlan, new csCache().cCache().Idioma, "0" });
                DataTable dtdatosimg = new CsConsultasVuelos().SPConsultaTabla("SPConsultaAdicionalesPlan", new string[3] { strPlan, new csCache().cCache().Idioma, "1" });

                if (dtdatosimg != null)
                {
                    if (dtdatosimg.Rows.Count > 0)
                    {
                        try
                        {
                            //string strimg = " <input type=" + '"' + "image" + '"' + " name=" + '"' + "imgLogoplan" + '"' + " id=" + '"' + "imgLogoplan" + '"' + " src=" + '"' + "http://162.248.52.194/Goocancun/SsoftContent/Images" + dtdatosimg.Rows[0][1].ToString() + '"' + " border=" + '"' + "0" + '"' + " />";
                            strHtml = strHtml.Replace("imgplan", dtdatosimg.Rows[0][1].ToString());
                        }
                        catch { }
                    }
                }


                if (dtdatos != null)
                {
                    if (dtdatos.Rows.Count > 0)
                    {

                        try
                        {

                            strHtml = strHtml.Replace("NNplan", System.Web.HttpUtility.HtmlDecode(dtdatos.Rows[1]["STRTEXTO"].ToString()));

                        }
                        catch { }

                        try
                        {

                            strHtml = strHtml.Replace("NDesc", System.Web.HttpUtility.HtmlDecode(dtdatos.Rows[0]["STRTEXTO"].ToString()));

                        }
                        catch { }

                        DataView dv = new DataView(dtdatos);
                        dv.RowFilter = "STRCODTEXTO='COND' OR STRCODTEXTO='INC' OR STRCODTEXTO='NOINC'";

                        dtdatos = dv.ToTable();

                        if (dtdatos != null)
                        {
                            if (dtdatos.Rows.Count > 0)
                            {
                                strDatos = "<table>";
                                //strDatos += "<tr><th></th>";
                                for (int b = 0; b < dtdatos.Rows.Count; b++)
                                {
                                    strDatos += "<tr><td>" + dtdatos.Rows[b]["strdescripcion"].ToString() + "</td>";
                                    strDatos += "<td>" + System.Web.HttpUtility.HtmlDecode(dtdatos.Rows[b]["strtexto"].ToString()) + "</td></tr>";
                                }
                                strDatos += "</table>";
                            }
                        }
                    }

                }
                strHtml = strHtml.Replace("DATOSPLANINCLUYE", strDatos);

            }
            catch { }


            return strHtml;
        }
        public void setOlvido(UserControl PageSource)
        {
            clsCacheControl cCacheControl = new clsCacheControl();
            clsIdioma cIdioma = new clsIdioma();
            cIdioma.LoadIdioma(csGeneralsPag.PaginaActual(), PageSource);
            string strplantillaRecupera = clsValidaciones.GetKeyOrAdd("PlantillaCorreoRecordarContraseña", "PlantillaCorreoRecordarContraseña");
            Label lblError = (Label)PageSource.FindControl("lblContraseña");
            TextBox txtUsuario = (TextBox)PageSource.FindControl("txtUsuario");
            clsParametros cParametros = new clsParametros();
            string strCopias = "";
            string strCopiasOcultas = "";
            string strtpoplantilla = "";
            string strForm = "";
            string strTo = "";
            string strAsunto = "";



            lblError.Text = string.Empty;
            clsEmail cEmail = new clsEmail();



            DataSet dsLogin = new DataSet();

            clsCache cCache = new csCache().cCache();

            if (cCache != null)
            {
                string sesion = string.Empty;

                if (txtUsuario.Text != "")
                {
                    try
                    {
                        DataTable dtLogin = new CsConsultasVuelos().SPConsultaTabla("SPValidaUsuarioFinal", new string[4] { txtUsuario.Text, clsSesiones.getAplicacion().ToString(), cCache.Empresa.ToString(), clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF") });
                        if (dtLogin.Rows.Count > 0)
                        {

                            DataTable dtCorreos = new CsConsultasVuelos().SPConsultaTabla("SPConultaPlantillaCorreos", new string[3] { "RCS", "EFE", cCache.Empresa });

                            strtpoplantilla = dtCorreos.Rows[0]["strArchivo"].ToString();
                            strForm = dtCorreos.Rows[0]["strform"].ToString();
                            strTo = new csCache().cCache().Email.ToString();
                            try
                            {
                                strCopias = dtCorreos.Rows[0]["strcopia"].ToString();
                            }
                            catch { }

                            try
                            {
                                strAsunto = dtCorreos.Rows[0]["strasunto"].ToString();
                            }
                            catch
                            {
                                strAsunto = "Recuepera Contraseña";
                            }

                            try
                            {
                                strCopiasOcultas = dtCorreos.Rows[0]["strcopiaoculta"].ToString();
                            }
                            catch { }

                            string strHtml = SetBuscarPlantillaCorreo(strplantillaRecupera);
                            strHtml = strHtml.Replace("@_USUARIO", txtUsuario.Text);
                            strHtml = strHtml.Replace("@_PASS", dtLogin.Rows[0]["strpassword"].ToString());

                            cParametros = new clsEmail().EnviarMensajeRecordar(strAsunto, txtUsuario.Text.Trim(), strCopias, strCopiasOcultas, strHtml, strForm);
                            lblError.Text = cParametros.ViewMessage[0].ToString();
                        }
                        else
                        {
                            lblError.Text = "lo sentimos pero su usuario y/o contraseña son incorrectos";
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
                        cParametros.Complemento = "Gastos ";
                        cParametros.ViewMessage.Add("Usuario no registrado");
                        cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                        ExceptionHandled.Publicar(cParametros);
                        lblError.Text = "lo sentimos pero su usuario y/o contraseña son incorrectos";
                    }
                }
                else
                {
                    lblError.Text = clsValidaciones.GetKeyOrAdd("ErrorOlvido", "Por favor ingrese una cuenta de correo");
                }
            }
        }
        public string SetCargaPlantillaReservaPlanes(string strHtml, string strReserva)
        {
            string strPasajeros = "";
            string stblsegmentos = "";
            string strloc = "";
            string intFormapago = "";
            string strtextospago = "";
            DataTable dtSegmentos = new DataTable();
            string strDatos = "";
            try
            {
                strloc = new CsConsultasVuelos().ConsultaCodigo(strReserva, "TBLRESMASTER", "INTPROYECTO", "STRRESERVA");

                if (strloc != "")
                {
                    DataSet dsDetallereserva = new CsConsultasVuelos().EjecutaSpProcedimiento("SPConsultaDetallereservaGeneral", new string[2] { new csCache().cCache().Idioma, strloc });
                    DataTable dt = new CsConsultasVuelos().SPConsultaTabla("SPConsultaPasajerosReserva", new string[3] { new csCache().cCache().Idioma, strReserva, "1" });

                    strHtml = strHtml.Replace("Nrecord", strReserva);
                    strHtml = strHtml.Replace("Nnombre", new csCache().cCache().Nombres + " " + new csCache().cCache().Apellidos);
                    strHtml = strHtml.Replace("nloc", strloc);

                    if (dt != null)
                    {
                        DataTable dtPasajeros = dt.DefaultView.ToTable(false, "strNombre", "Moneda", "Total");

                        dtPasajeros.Columns["strNombre"].ColumnName = "Nombre";
                        strPasajeros = ConvertDataTableTableHtml(dtPasajeros);
                        strPasajeros = strPasajeros.Replace("<table cellspacing=" + '"' + "0" + '"' + " rules=" + '"' + "all" + '"' + " border=" + '"' + "1" + '"' + " style=" + '"' + "border-collapse:collapse;" + '"' + ">", "<table cellspacing=" + '"' + "0" + '"' + " cellpadding=" + '"' + "2" + '"' + " style=" + '"' + "text-align: center; font-size: 12px; margin-left: auto; margin-right: auto;border: solid 1px #ccc;width:600px" + '"' + ">");
                        strPasajeros = strPasajeros.Trim().Replace("\r", "");
                        strPasajeros = strPasajeros.Trim().Replace("\n", "");
                        strPasajeros = strPasajeros.Trim().Replace("\t", "");
                        strPasajeros = strPasajeros.Replace("<tr><th scope=" + '"' + "col" + '"' + ">Nombre</th><th scope=" + '"' + "col" + '"' + ">Moneda</th><th scope=" + '"' + "col" + '"' + ">Total</th></tr>", "<tr bgcolor=" + '"' + "gray" + '"' + "><th scope=" + '"' + "col" + '"' + ">" + "<font color=" + '"' + "White" + '"' + "><b>Reserva a nombre de</b></font>" + "</th><th scope=" + '"' + "col" + '"' + ">" + "<font color=" + '"' + "White" + '"' + "><b>Moneda</b></font>" + "</th><th scope=" + '"' + "col" + '"' + ">" + "<font color=" + '"' + "White" + '"' + "><b>Valor Total</b></font>" + "</th></tr>");
                    }
                    DataTable dtDetalle = dsDetallereserva.Tables[0];


                    intFormapago = dtDetalle.Rows[0]["intformapago"].ToString();


                    strtextospago = new CsConsultasVuelos().ConsultaCodigo(intFormapago + "'" + " AND " + " STRIDIOMA='" + new csCache().cCache().Idioma, "tblTextosFop", "strDescripcion", "intFormapago");


                    dtDetalle = dtDetalle.DefaultView.ToTable(false, "strReserva", "label101", "texto101", "label102", "texto102");


                    strHtml = strHtml.Replace("fpago", dtDetalle.Rows[0]["texto102"].ToString());
                    strHtml = strHtml.Replace("femision", dtDetalle.Rows[0]["texto101"].ToString());
                    strHtml = strHtml.Replace("stelefono", new csCache().cCache().Telefono);
                    strHtml = strHtml.Replace("sCorreo", new csCache().cCache().Email);



                    DataTable dt3 = dsDetallereserva.Tables[2];

                    strHtml = strHtml.Replace("sValortotal", dt3.Rows[0]["valor"].ToString());
                    strHtml = strHtml.Replace("sMoneda", dt3.Rows[0]["moneda"].ToString());

                    strHtml = strHtml.Replace("DATOSPASAJEROS", strPasajeros);

                    DataTable dtdatos = new CsConsultasVuelos().SPConsultaTabla("SPConsultatextoplanes", new string[4] { strReserva, new csCache().cCache().Idioma, "", "3" });


                    if (dtdatos != null)
                    {
                        if (dtdatos.Rows.Count > 0)
                        {

                            try
                            {
                                DataTable dtdatosUser = new CsConsultasVuelos().SPConsultaTabla("SPConsultatextoplanes", new string[4] { strReserva, new csCache().cCache().Idioma, "NAME", "1" });
                                strHtml = strHtml.Replace("NNplan", System.Web.HttpUtility.HtmlDecode(dtdatosUser.Rows[0]["STRTEXTO"].ToString()));

                            }
                            catch { }

                            try
                            {
                                DataTable dtdatosUser = new CsConsultasVuelos().SPConsultaTabla("SPConsultatextoplanes", new string[4] { strReserva, new csCache().cCache().Idioma, "DESC", "1" });
                                strHtml = strHtml.Replace("NDesc", System.Web.HttpUtility.HtmlDecode(dtdatosUser.Rows[0]["STRTEXTO"].ToString()));

                            }
                            catch { }

                            DataView dv = new DataView(dtdatos);
                            dv.RowFilter = "STRCODTEXTO='COND' OR STRCODTEXTO='INC' OR STRCODTEXTO='NOINC'";

                            dtdatos = dv.ToTable();

                            if (dtdatos != null)
                            {
                                if (dtdatos.Rows.Count > 0)
                                {
                                    strDatos = "<table>";
                                    //strDatos += "<tr><th></th>";
                                    for (int b = 0; b < dtdatos.Rows.Count; b++)
                                    {
                                        strDatos += "<tr><td>" + dtdatos.Rows[b]["strdescripcion"].ToString() + "</td>";
                                        strDatos += "<td>" + System.Web.HttpUtility.HtmlDecode(dtdatos.Rows[b]["strtexto"].ToString()) + "</td></tr>";
                                    }
                                    strDatos += "</table>";
                                }
                            }
                        }

                    }
                    strHtml = strHtml.Replace("DATOSPLANINCLUYE", strDatos);


                    dtSegmentos = new CsConsultasVuelos().SPConsultaTabla("SPConsultatextoplanes", new string[4] { strReserva, new csCache().cCache().Idioma, "", "2" });
                    try
                    {
                        dtSegmentos.Columns["DTMFECHAINI"].ColumnName = "Fecha de Inicio de Viaje";
                        dtSegmentos.Columns["STRDURACION"].ColumnName = "Duración";
                        dtSegmentos.Columns["STRTIPOHABITACION"].ColumnName = "Tipo de habitación";
                        dtSegmentos.Columns["STRTIPOACOMODACION"].ColumnName = "Tipo de acomodación";
                        dtSegmentos.Columns["STRREGIMEN"].ColumnName = "Tipo de alimentación";
                        dtSegmentos.Columns["STROPERADOR"].ColumnName = "Hotel seleccionado";
                    }
                    catch { }

                    if (dtSegmentos != null)
                    {

                        stblsegmentos = "<table>";
                        for (int b = 0; b < dtSegmentos.Rows.Count; b++)
                        {

                            for (int c = 0; c < dtSegmentos.Columns.Count; c++)
                            {
                                stblsegmentos += "<tr>";

                                stblsegmentos += "<td>";
                                stblsegmentos += dtSegmentos.Columns[c].ColumnName + ":";
                                stblsegmentos += "</td>";
                                stblsegmentos += "<td>";
                                stblsegmentos += dtSegmentos.Rows[b][c].ToString();
                                stblsegmentos += "</td>";

                                stblsegmentos += "</tr>";
                            }
                            stblsegmentos += "<tr>";
                            stblsegmentos += "<td>";
                            stblsegmentos += "<span></span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += "<td>";
                            stblsegmentos += "<span></span>";
                            stblsegmentos += "</td>";
                            stblsegmentos += "</tr>";

                        }
                        stblsegmentos += "</table>";
                    }
                    strHtml = strHtml.Replace("DatosPlan", stblsegmentos);
                }


            }
            catch { }


            return strHtml;
        }
        public string SetMenu()
        {

            string strMenu = "";
            try
            {

                DataSet dsmenu = new DataSet();
                dsmenu = new CsConsultasVuelos().EjecutaSpProcedimiento("SPConsultaMenu", new string[4] { clsValidaciones.GetKeyOrAdd("MenuPagina", "PAG"), clsValidaciones.GetKeyOrAdd("UsuarioFinal", "UF"), new csCache().cCache().Idioma, "" });



                if (dsmenu != null)
                {
                    if (dsmenu.Tables.Count > 0)
                    {
                        if (dsmenu.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = dsmenu.Tables[0];


                            DataView dvLista = new DataView(dt);

                            dvLista.RowFilter = "nivel='0'";
                            DataTable dtlista = dvLista.ToTable();
                            if (dtlista.Rows.Count > 0)
                            {
                                strMenu = "<ul id=" + '"' + "nav" + '"' + ">";

                                foreach (DataRow drn in dtlista.Rows)
                                {

                                    strMenu = strMenu + " <li><a href='" + drn["link"] + "'>" + drn["opcion"] + " " + drn["JavaScript"] + "</a>";
                                    DataView dv = new DataView(dt);
                                    dv.RowFilter = "Nivel='" + drn["Nivel"].ToString() + "'";
                                    DataTable dtnewitems = dv.ToTable();
                                    string strSubHtml = "";
                                    strSubHtml = AddChildItem(Convert.ToInt32(drn["idmenu"].ToString()), dt, "1");
                                    if (strSubHtml != "")
                                    {
                                        strMenu = strMenu + strSubHtml;
                                    }
                                    strMenu = strMenu + "</li>";

                                }
                                strMenu = strMenu + "</ul>";
                            }

                        }
                    }
                }
            }
            catch { }

            return strMenu;

        }
        protected string AddChildItem(int miMenuItem, DataTable dtDataTable, string nivel)
        {
            string strHtml = "";
            DataTable dtCopia = dtDataTable;
            DataView dvfilter = new DataView(dtDataTable);
            dvfilter.RowFilter = "nivel='" + nivel + "' and padre='" + miMenuItem + "'";
            dtDataTable = dvfilter.ToTable();

            if (dtDataTable.Rows.Count > 0)
            {
                strHtml = "<ul><li>";

                foreach (DataRow drDataRow in dtDataTable.Rows)
                {
                    strHtml = strHtml + "<a href='" + drDataRow["link"] + "'>" + drDataRow["opcion"] + "</a>";
                    if (Convert.ToInt32(drDataRow[2]) == miMenuItem && Convert.ToInt32(drDataRow[0]) != Convert.ToInt32(drDataRow[2]))
                    {
                        string strChildrens = AddChildItem(Convert.ToInt32(drDataRow[0]), dtCopia, (Convert.ToInt32(drDataRow[1].ToString()) + 1).ToString());
                    }
                }
                strHtml = strHtml + "</li></ul>";
            }

            return strHtml;
        }
    }
}
