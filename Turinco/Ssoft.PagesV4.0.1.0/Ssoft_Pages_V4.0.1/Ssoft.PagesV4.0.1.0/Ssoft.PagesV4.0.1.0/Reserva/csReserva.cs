using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSOFT.ENCRYPT;
using System.Web.UI;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using System.Web.UI.WebControls;
using SsoftQuery.Vuelos;
using System.Data;
using Ssoft.ValueObjects;
using Ssoft.Rules.Reservas;
using System.Web;
using SsoftQuery.Reserva;
using System.Net;

namespace Ssoft.Pages.Reserva
{
     
    public class csReserva
    {
        #region Atributos
        private const string USUARIOID = "usuarioId=";
        private const string REFVENTA = "refVenta=";
        public const string VALORADICIONAL = "valorAdicional=";
        private const string DESCRIPCION = "descripcion=";
        public const string VALOR = "valor=";
        public const string IVA = "iva=";
        private const string BASEDEVOLUCIONIVA = "baseDevolucionIva=";
        public const string EMAILCOMPRADOR = "emailComprador=";
        public const string MONEDA = "moneda=";
        private const string NOMBRECOMPRADOR = "nombreComprador=";
        private const string DOCUMENTOIDENTIFICACION = "documentoIdentificacion=";
        private const string TIPODOCUMENTOIDENTIFICACION = "tipoDocumentoIdentificacion=";
        public const string EXTRA1 = "extra1=";
        public const string EXTRA2 = "extra2=";
        private const string PRUEBA = "prueba=";
        public const string TARIFAADMINISTRATIVA = "tarifaAdministrativa=";
        public const string IVATARIFAADMINISTRATIVA = "ivaTarifaAdministrativa=";
        public const string BASEDEVOLUCIONTARIFAADMINISTRATIVA = "baseDevolucionTarifaAdministrativa=";
        public const string NOMBRE_USUARIO = "NombreUsuario=";
        public const string EXTRA3 = "extra3=";
        public const string EXTRA4 = "extra4=";
        public const string EXTRA5 = "extra5=";
        public const string PLANTILLA = "plantilla=";
        private const string CIUDAD_ENVIO = "ciudadEnvio=";
        private const string DIR_ENVIO = "direccionEnvio=";
        private const string USERPACIFICARD = "UsuarioID=";
        private const string CODOPERACION = "CodigoOperacion=";
        private const string CODMONEDA = "CodigoMoneda=";
        private const string MONTO = "Monto=";
        private const string RESERVADO1 = "Reservado1=";
        private const string RESERVADO2 = "Reservado2=";
        private const string RESERVADO3 = "Reservado3=";
        private const string NOMBRECOMPRADORPC = "NombreComprador=";
        private const string PAGINACONFIRMACION = "PaginaConfirmacion=";
        private const string DETALLE = "Detalle=";
        public const string AEROLINEA = "aerolinea=";
        #endregion
        private static string sCaracterDecimal = clsValidaciones.GetKeyOrAdd("CaracterDecimal", ",");

        public string setInsertarTarjeta(UserControl PageSource, clsCache cCache)
        {
            clsParametros cParam = new clsParametros();
            string strvalida = string.Empty;
            try
            {

                bool bValida = true;
                string bProyecto = string.Empty;
                DropDownList ddlFranquicia = (DropDownList)PageSource.FindControl("ddlFranquicia");
                TextBox txtNumTarjeta = (TextBox)PageSource.FindControl("txtNumTarjeta");
                TextBox txtBanco = (TextBox)PageSource.FindControl("txtBanco");
                DropDownList ddlMesVencimiento = (DropDownList)PageSource.FindControl("ddlMesVencimiento");
                DropDownList txtAnioVencimiento = (DropDownList)PageSource.FindControl("txtAnioVencimiento");
                TextBox txtCodSeguridad = (TextBox)PageSource.FindControl("txtCodSeguridad");
                TextBox txtCuotas = (TextBox)PageSource.FindControl("txtCuotas");
                TextBox txtTitular = (TextBox)PageSource.FindControl("txtTitular");
                TextBox txtIdentificacion = (TextBox)PageSource.FindControl("txtIdentificacion");
                TextBox txtDireccion = (TextBox)PageSource.FindControl("txtDireccion");
                TextBox txtPais = (TextBox)PageSource.FindControl("txtPais");
                TextBox txtTelefonoOficina = (TextBox)PageSource.FindControl("txtTelefonoOficina");
                TextBox txtTelefonoOtro = (TextBox)PageSource.FindControl("txtTelefonoOtro");

                string[] tblTarjetas;
                tblTarjetas = new string[17];
                tblTarjetas[0] = clsSesiones.getProyecto();
                if (txtNumTarjeta != null)
                    tblTarjetas[1] = "'" + txtNumTarjeta.Text + "'";
                else
                    bValida = false;

                if (ddlFranquicia != null)
                {
                    tblTarjetas[2] = new CsConsultasVuelos().ConsultaCodigo(ddlFranquicia.SelectedValue, "tblTccFranquicia", "intIdFranquicia", "strcodFranquicia");
                }

                if (txtBanco != null)
                    tblTarjetas[3] = "'" + txtBanco.Text + "'";

                if (txtPais != null)
                    tblTarjetas[4] = "'" + txtPais.Text + "'";

                if (txtCodSeguridad != null)
                    tblTarjetas[5] = "'" + txtCodSeguridad.Text + "'";

                if (ddlMesVencimiento != null && txtAnioVencimiento != null)
                    tblTarjetas[6] = "'" + ddlMesVencimiento.SelectedValue + "/" + txtAnioVencimiento.SelectedValue + "'";

                if (txtTitular != null)
                    tblTarjetas[7] = "'" + txtTitular.Text + "'";

                if (txtIdentificacion != null)
                    tblTarjetas[8] = "'" + txtIdentificacion.Text + "'";

                if (txtDireccion != null)
                    tblTarjetas[9] = "'" + txtDireccion.Text + "'";

                if (txtTelefonoOficina != null)
                    tblTarjetas[10] = "'" + txtTelefonoOficina.Text + "'";

                if (txtTelefonoOtro != null)
                    tblTarjetas[11] = "'" + txtTelefonoOtro.Text + "'";

                tblTarjetas[12] = "'" + cCache.Email + "'";

                if (txtCuotas != null)
                    tblTarjetas[13] = txtCuotas.Text;

                tblTarjetas[14] = cCache.Contacto;

                string StrFecha = DateTime.Now.ToString().Replace("p.m.", "").Replace("a.m.", "");

                int bfecha = StrFecha.IndexOf(" ");

                if (bfecha != -1)
                {
                    StrFecha = StrFecha.Substring(0, bfecha);
                    try
                    {
                        StrFecha = clsValidaciones.ConverFecha(StrFecha, clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy"), clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd"));
                    }
                    catch
                    {
                        StrFecha = clsValidaciones.ConverFecha(StrFecha, Enum_FormatoFecha.MDY);
                    }
                }

                tblTarjetas[15] = "'" + StrFecha + "'";
                tblTarjetas[16] = "'" + StrFecha + "'";

                if (bValida)
                {
                    bProyecto = new CsConsultasVuelos().EjecutarSPConsulta("SPInsertarTarjeta", tblTarjetas);
                    if (bProyecto != "0" && bProyecto != "")
                    {
                        strvalida = "OK";
                        string parametrosOk = string.Empty;
                        parametrosOk = "Se insertaron los Datos de tarjeta son: ";
                        for (int i = 0; i < tblTarjetas.Length; i++)
                        {
                            parametrosOk += tblTarjetas[i] + "|";
                        }
                        parametrosOk += "-----------------------------------";
                        ExceptionHandled.Publicar("Datos de tarjeta:" + parametrosOk);
                    }
                    else
                    {
                        string parametros = string.Empty;
                        parametros = "No se pudieron insertar los Datos de tarjeta campturados son: SPInsertarTarjeta ";
                        for (int i = 0; i < tblTarjetas.Length; i++)
                        {
                            parametros += tblTarjetas[i] + "|";
                        }
                        parametros += "-----------------------------------";

                        ExceptionHandled.Publicar(parametros);
                        strvalida = "No se pudo Guardar los datos de tarjeta";
                    }
                }
                else
                {
                    ExceptionHandled.Publicar("Nose pudo realizar la insercion pues no se encontro el control que contiene el numero de la tarjeta");
                }
            }
            catch (Exception Ex)
            {
                cParam.Id = 0;
                cParam.ViewMessage.Add("No se pudo insertar los datos de tarjeta");
                cParam.Sugerencia.Add("Ha ocurrido un error procesando su solicitud");
                cParam.Tipo = clsTipoError.Library;
                cParam.Ex = Ex;
                cParam.Message = Ex.Message;
                ExceptionHandled.Publicar(cParam);
                strvalida = "Ha ocurrido un error procesando su solicitud";
            }
            return strvalida;
        }

        /// <summary>
        /// Metodo del llamado del pago para el WS de POL
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="cCache"></param>
        /// <param name="sRecord"></param>
        /// <returns></returns>
        /// /// <remarks>
        /// Autor:      Camilo Diaz
        /// Company:    Ssoft Colombia
        /// Fecha: 
        /// -------------------
        /// Control de Cambios
        /// -------------------   
        /// </remarks>
        public string setSolicitarAutorizacionPOLWS(UserControl PageSource, clsCache cCache, string sRecord)
        {
            clsParametros cParam = new clsParametros();
            string sResp = "";
            try
            {
                VO_Credentials vo_Credenciales = clsCredenciales.Credenciales(Enum_ProveedorWebServices.POL);
                PagosOnlineWS.PagosOnLineWS wsLocal = new PagosOnlineWS.PagosOnLineWS();
                //wsLocal.ConsultarMediosPago("1", "2");
                string sIP = PageSource.Request.UserHostAddress;
                //string IPHost = Dns.GetHostName();
                //string sIP = Dns.GetHostByName(IPHost).AddressList[0].ToString();
                string sUserAgent = PageSource.Request.UserAgent;
                HttpCookie hCookie = new HttpCookie("CookiePagoRef" + sRecord, sRecord);
                bool bValida = true;

                string bProyecto = string.Empty;
                DropDownList ddlFranquicia = (DropDownList)PageSource.FindControl("ddlFranquiciaPOL");
                TextBox txtNumTarjeta = (TextBox)PageSource.FindControl("txtNumTarjetaPOL");
                //TextBox txtBanco = (TextBox)PageSource.FindControl("txtBancoPOL");
                DropDownList ddlMesVencimiento = (DropDownList)PageSource.FindControl("ddlMesVencimientoPOL");
                DropDownList txtAnioVencimiento = (DropDownList)PageSource.FindControl("txtAnioVencimientoPOL");
                TextBox txtCodSeguridad = (TextBox)PageSource.FindControl("txtCodSeguridadPOL");
                TextBox txtCuotas = (TextBox)PageSource.FindControl("txtCuotasPOL");
                TextBox txtTitular = (TextBox)PageSource.FindControl("txtTitularPOL");
                TextBox txtIdentificacion = (TextBox)PageSource.FindControl("txtIdentificacionPOL");
                //TextBox txtDireccion = (TextBox)PageSource.FindControl("txtDireccionPOL");
                //TextBox txtPais = (TextBox)PageSource.FindControl("txtPaisPOL");
                TextBox txtTelefonoOficina = (TextBox)PageSource.FindControl("txtTelefonoOficinaPOL");
                TextBox txtTelefonoOtro = (TextBox)PageSource.FindControl("txtTelefonoOtroPOL");
                Label lblError = (Label)PageSource.FindControl("lblError");
                HiddenField strRecord = (HiddenField)PageSource.FindControl("strRecord");
                HiddenField sRuta = (HiddenField)PageSource.FindControl("sRuta");
                HiddenField sFecha = (HiddenField)PageSource.FindControl("sFecha");
                HiddenField TotalCarritoSinFormato = (HiddenField)PageSource.FindControl("TotalCarritoSinFormato");
                RadioButtonList rblFranquicias = (RadioButtonList)PageSource.FindControl("rblFranquicias");
                HiddenField iTotaBaselTA = (HiddenField)PageSource.FindControl("iTotaBaselTA");
                HiddenField iTotalIVA_TA = (HiddenField)PageSource.FindControl("iTotalIVA_TA");
                HiddenField iTotalBase = (HiddenField)PageSource.FindControl("iTotalBase");
                HiddenField iTotalImpuestoGasolina = (HiddenField)PageSource.FindControl("iTotalImpuestoGasolina");
                HiddenField iTotalImpuestos = (HiddenField)PageSource.FindControl("iTotalImpuestos");
                HiddenField iTotalIVA_Tarifa = (HiddenField)PageSource.FindControl("iTotalIVA_Tarifa");
                HiddenField sAerolinea = (HiddenField)PageSource.FindControl("sAerolinea");
                Label lblTextoConfirm = (Label)PageSource.FindControl("lblTextoConfirm");
                string sMonedaLocal = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                string sPaisPago = clsValidaciones.GetKeyOrAdd("sPaisPagoPOLWS", "CO");
                string sCodVisa = clsValidaciones.GetKeyOrAdd("sCodMedioVisa", "10");
                string sCodAmex = clsValidaciones.GetKeyOrAdd("sCodMedioAmex", "12");
                string sCodDiners = clsValidaciones.GetKeyOrAdd("sCodMedioVisaDiners", "22");
                string sMedioPago = "0";
                string sAerolineaVal = "";
                decimal iBaseTA = 0;
                decimal iBaseDevolucion = 0;
                decimal iValor = 0;
                decimal iIva = 0;
                decimal iValorAdicional = 0;
                decimal iTA = 0;
                decimal iIvaTA = 0;
                int iCuotas = 0;
                try
                {
                    iBaseTA = Convert.ToDecimal(iTotaBaselTA.Value.Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal));

                    iIvaTA = Convert.ToDecimal(iTotalIVA_TA.Value.Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal));

                    iTA = iBaseTA + iIvaTA;

                    iBaseDevolucion = Convert.ToDecimal(iTotalBase.Value.Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal)) +
                        Convert.ToDecimal(iTotalImpuestoGasolina.Value.Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal));

                    iIva = Convert.ToDecimal(iTotalIVA_Tarifa.Value.Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal));

                    iValor = iBaseDevolucion + iIva;

                    iValorAdicional = Convert.ToDecimal(iTotalImpuestos.Value.Replace(",", sCaracterDecimal).Replace(".", sCaracterDecimal));

                    iCuotas = Convert.ToInt32(txtCuotas.Text);

                    if (ddlFranquicia != null)
                    {
                        if (ddlFranquicia.SelectedItem.Text.ToUpper().Contains("VISA"))
                            sMedioPago = sCodVisa;
                        if (ddlFranquicia.SelectedItem.Text.ToUpper().Contains("AMERICAN"))
                            sMedioPago = sCodAmex;
                        if (ddlFranquicia.SelectedItem.Text.ToUpper().Contains("DINERS"))
                            sMedioPago = sCodDiners;
                    }

                    sAerolineaVal = clsSesiones.getAerolineaValidadora();
                }
                catch { }
                wsLocal.ValidarAerolineas(vo_Credenciales.User);
                string[] sRespuestaSolicitud = wsLocal.SolicitarAutorizacionConDispersion(vo_Credenciales.User, sRecord, clsValidaciones.GetKeyOrAdd("sDescripcionPago", "Venta de tiquete aereo"),
                    iValor, iIva, iBaseDevolucion, iValorAdicional, iTA, iIvaTA, iBaseTA, sMonedaLocal, iCuotas, cCache.Nombres + " " + cCache.Apellidos, txtIdentificacion.Text,
                    cCache.User, txtTelefonoOtro.Text, txtTelefonoOficina.Text, sPaisPago, "", "", sPaisPago, "", "", sIP, sMedioPago, sAerolineaVal, "", "", txtNumTarjeta.Text,
                    txtTitular.Text, txtAnioVencimiento.SelectedValue + "/" + ddlMesVencimiento.SelectedValue, txtCodSeguridad.Text,
                    "CookiePagoRef" + sRecord, sUserAgent, false, true);


                if (sRespuestaSolicitud != null && sRespuestaSolicitud.Length > 0)
                {
                    if (sRespuestaSolicitud.Length >= 8)
                    {
                        string[] sCodRespuesta = sRespuestaSolicitud[2].Split('|');
                        string[] sRespuesta = sRespuestaSolicitud[3].Split('|');
                        string[] sAutorizacionReserva = sRespuestaSolicitud[4].Split('|');
                        string[] sAutorizacionTA = sRespuestaSolicitud[5].Split('|');
                        if (sAutorizacionReserva.Length > 1 && sAutorizacionTA.Length > 1)
                        {
                            sResp = sCodRespuesta[1];
                            if (sAutorizacionReserva[1] != null && sAutorizacionReserva[1] != "" && sAutorizacionReserva[1] != "0" &&
                                sAutorizacionTA[1] != null && sAutorizacionTA[1] != "" && sAutorizacionTA[1] != "0"
                                && sRespuesta[1].ToUpper().Contains("APROBADA"))
                            {
                                lblTextoConfirm.Text = "Tu pago fue aprobado, el codigo de tu reserva es el siguiente:";
                            }
                            else
                            {
                                if (((sAutorizacionReserva[1] == null || sAutorizacionReserva[1] == "" || sAutorizacionReserva[1] == "0") &&
                                    (sAutorizacionTA[1] != null && sAutorizacionTA[1] != "" && sAutorizacionTA[1] != "0")) ||
                                    ((sAutorizacionReserva[1] != null && sAutorizacionReserva[1] != "" && sAutorizacionReserva[1] != "0") &&
                                    (sAutorizacionTA[1] == null || sAutorizacionTA[1] == "" || sAutorizacionTA[1] == "0")))
                                {
                                    lblTextoConfirm.Text = "Tu pago fue parcialmente aprobado, por favor comunicate con la agencia usando el siguiente codigo de reserva para finalizar el proceso de pago";
                                }
                                else
                                {
                                    if (sRespuesta[1].ToUpper().Contains("RECHAZ") || sRespuesta[1].ToUpper().Contains("DECLIN"))
                                    {
                                        lblTextoConfirm.Text = "Tu pago ha sido rechazado,  por favor comunicate con la agencia usando el siguiente codigo de reserva para realizar el pago con otro medio";
                                    }
                                    else
                                    {
                                        if (sRespuesta[1].ToUpper().Contains("VALIDACION"))
                                        {
                                            lblTextoConfirm.Text = "Tu pago se encuentra en validacion por parte de la entidad financiera, por favor contacta a la agencia para verificar el estado final de tu pago";
                                        }
                                        else
                                        {
                                            string[] sDescRespuesta = sRespuestaSolicitud[3].Split('|');
                                            string[] sDescError = sRespuestaSolicitud[7].Split('|');
                                            StringBuilder sbRespuesta = new StringBuilder();
                                            sbRespuesta.AppendLine("Tu pago fue procesado con el siguiente resultado: ");
                                            sbRespuesta.AppendLine("Respuesta: " + sDescRespuesta[1]);
                                            sbRespuesta.AppendLine("Error: " + sDescError[1]);
                                            sbRespuesta.AppendLine("Por favor comunicate con la agencia para realizar el pago de tu reserva usando el siguiente record:");
                                            lblTextoConfirm.Text = sbRespuesta.ToString();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            lblTextoConfirm.Text = "Tu pago no pudo ser procesado, por favor comunicate con la agencia con el siguiente record para poder efectuar el pago de tu reserva";
                        }
                    }
                    else
                    {
                        lblTextoConfirm.Text = "Tu pago no pudo ser procesado, por favor comunicate con la agencia con el siguiente record para poder efectuar el pago de tu reserva";
                    }
                }
                else
                {
                    if (lblTextoConfirm != null)
                        lblTextoConfirm.Text = "Tu pago no pudo ser procesado, por favor comunicate con la agencia con el siguiente record para poder efectuar el pago de tu reserva";
                }
                //if (bValida)
                //{
                //    bProyecto = new CsConsultasVuelos().EjecutarSPConsulta("SPInsertarTarjeta", tblTarjetas);
                //    if (bProyecto != "0" && bProyecto != "")
                //    {
                //        strvalida = "OK";
                //        string parametrosOk = string.Empty;
                //        parametrosOk = "Se insertaron los Datos de tarjeta son: ";
                //        for (int i = 0; i < tblTarjetas.Length; i++)
                //        {
                //            parametrosOk += tblTarjetas[i] + "|";
                //        }
                //        parametrosOk += "-----------------------------------";
                //        ExceptionHandled.Publicar("Datos de tarjeta:" + parametrosOk);
                //    }
                //    else
                //    {
                //        string parametros = string.Empty;
                //        parametros = "No se pudieron insertar los Datos de tarjeta campturados son: SPInsertarTarjeta ";
                //        for (int i = 0; i < tblTarjetas.Length; i++)
                //        {
                //            parametros += tblTarjetas[i] + "|";
                //        }
                //        parametros += "-----------------------------------";

                //        ExceptionHandled.Publicar(parametros);
                //        strvalida = "No se pudo Guardar los datos de tarjeta";
                //    }
                //}
                //else
                //{
                //    ExceptionHandled.Publicar("Nose pudo realizar la insercion pues no se encontro el control que contiene el numero de la tarjeta");
                //}
            }
            catch (Exception Ex)
            {
                cParam.Id = 0;
                cParam.ViewMessage.Add("No se pudo enviar el pago al WS de POL");
                cParam.Sugerencia.Add("Ha ocurrido un error procesando su solicitud");
                cParam.Tipo = clsTipoError.Library;
                cParam.Ex = Ex;
                cParam.Message = Ex.Message;
                ExceptionHandled.Publicar(cParam);
            }
            return sResp;
        }

        public void setTotalReserva(UserControl PageSource)
        {
            Label lblRecord = (Label)PageSource.FindControl("lblRecord");
            HiddenField iTotalBase = (HiddenField)PageSource.FindControl("iTotalBase");
            HiddenField iTotalTarifa = (HiddenField)PageSource.FindControl("iTotalTarifa");
            HiddenField iTotalIVA_Tarifa = (HiddenField)PageSource.FindControl("iTotalIVA_Tarifa");
            HiddenField iTotalImpuestos = (HiddenField)PageSource.FindControl("iTotalImpuestos");
            HiddenField iTotalImpuestoGasolina = (HiddenField)PageSource.FindControl("iTotalImpuestoGasolina");
            HiddenField iTotaBaselTA = (HiddenField)PageSource.FindControl("iTotaBaselTA");
            HiddenField iTotalIVA_TA = (HiddenField)PageSource.FindControl("iTotalIVA_TA");
            HiddenField iTotalBaseDecimal = (HiddenField)PageSource.FindControl("iTotalBaseDecimal");
            HiddenField iTotalIVA_TarifaDecimal = (HiddenField)PageSource.FindControl("iTotalIVA_TarifaDecimal");
            HiddenField iTotalImpuestosDecimal = (HiddenField)PageSource.FindControl("iTotalImpuestosDecimal");
            HiddenField iTotalImpuestoGasolinaDecimal = (HiddenField)PageSource.FindControl("iTotalImpuestoGasolinaDecimal");
            HiddenField iTotaBaselTADecimal = (HiddenField)PageSource.FindControl("iTotaBaselTADecimal");
            HiddenField iTotalIVA_TADecimal = (HiddenField)PageSource.FindControl("iTotalIVA_TADecimal");
            HiddenField TotalCarritoSinFormato = (HiddenField)PageSource.FindControl("TotalCarritoSinFormato");

            DataSet dsData = null;
            DataTable tblDatosTarifa = null;
            DataTable tblDatosTax = null;

            if (lblRecord != null)
            {
                if (lblRecord.Text != "")
                {
                    string sProyecto = new CsConsultasVuelos().ConsultaCodigo(lblRecord.Text, "TBLRESMASTER", "INTPROYECTO", "STRRESERVA");
                    dsData = new CsConsultasVuelos().ConsultarReserva(Convert.ToInt32(sProyecto));

                    if (dsData != null)
                    {

                        tblDatosTarifa = dsData.Tables["tblPax"];
                        tblDatosTax = dsData.Tables["tblFareTax"];

                        iTotalBase.Value = "0";
                        iTotalTarifa.Value = "0";
                        iTotalIVA_Tarifa.Value = "0";
                        iTotalImpuestos.Value = "0";
                        iTotalImpuestoGasolina.Value = "0";
                        iTotaBaselTA.Value = "0";
                        iTotalIVA_TA.Value = "0";

                        if (iTotalImpuestosDecimal != null)
                            iTotalImpuestosDecimal.Value = "0";
                        if (iTotalImpuestoGasolinaDecimal != null)
                            iTotalImpuestoGasolinaDecimal.Value = "0";
                        if (iTotalIVA_TarifaDecimal != null)
                            iTotalIVA_TarifaDecimal.Value = "0";
                        if (iTotalBaseDecimal != null)
                            iTotalBaseDecimal.Value = "0";
                        if (iTotaBaselTADecimal != null)
                            iTotaBaselTADecimal.Value = "0";
                        if (iTotalIVA_TADecimal != null)
                            iTotalIVA_TADecimal.Value = "0";


                        string intTipoPax = "intTipoPax";
                        string dblValor = "dblValor";
                        string dblTotal = "dblTotal";
                        string strMoneda = "intMoneda";
                        string dblTax = "dblTax";

                        Utils.Utils utilidades = new Utils.Utils();
                        DataColumn dcintTA = new DataColumn("intTA");
                        DataColumn dcintITA = new DataColumn("intITA");
                        DataColumn dcintTotalImpuestos = new DataColumn("intTotalImpuestos");
                        DataRowCollection drcTiposPax = tblDatosTarifa.Rows;

                        string[] strCampos = new string[4];
                        int iContadorCampos = 0;
                        strCampos[iContadorCampos++] = "intTipoPax";
                        strCampos[iContadorCampos++] = "strcode";
                        strCampos[iContadorCampos++] = "dblValorTax";
                        strCampos[iContadorCampos++] = "strtipoPax";
                        DataTable dtDesctincTaxesXPax = utilidades.SelectDistinct(strCampos, tblDatosTax, null);

                        foreach (DataRow drTipoPax in drcTiposPax)
                        {
                            Label lblError = (Label)PageSource.FindControl("lblError");

                            int iTarifa = 0;
                            int.TryParse(drTipoPax[dblValor].ToString(), out iTarifa);
                            if (iTotalBaseDecimal != null)
                            {
                                try
                                {
                                    iTotalBaseDecimal.Value = drTipoPax[dblValor].ToString();
                                }
                                catch (Exception) { }
                            }


                            int iTotalPasajero = 0;
                            int.TryParse(drTipoPax[dblTotal].ToString(), out iTotalPasajero);


                            string sTipoPasajero = String.Empty;
                            string sMoneda = drTipoPax[strMoneda].ToString();

                            DataRow[] drTaxesXPax = dtDesctincTaxesXPax.Select("intTipoPax=" + drTipoPax[intTipoPax]);

                            foreach (DataRow drTaxXPax in drTaxesXPax)
                            {
                                int iImpuestos = 0;
                                decimal dImpuestos = 0;
                                string sCodigoTax = drTaxXPax["strcode"].ToString();
                                string sValorTax = drTaxXPax["dblValorTax"].ToString();

                                dImpuestos = Convert.ToDecimal(sValorTax.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal));
                                int.TryParse(sValorTax, out iImpuestos);
                                sTipoPasajero = "Tarifa para 1 " + drTaxXPax["strtipoPax"].ToString();

                                if (isOtroImpuesto(PageSource, sCodigoTax, iImpuestos))
                                {
                                    iTotalImpuestos.Value = Convert.ToString(int.Parse(iTotalImpuestos.Value) + iImpuestos);
                                }
                                if (isOtroImpuestoDecimal(PageSource, sCodigoTax, dImpuestos))
                                {
                                    if (iTotalImpuestosDecimal != null)
                                    {
                                        try
                                        {
                                            iTotalImpuestosDecimal.Value = Convert.ToString(Convert.ToDecimal(iTotalImpuestos.Value.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal)) + dImpuestos);
                                        }
                                        catch (Exception) { }
                                    }
                                }
                            }
                            iTotalBase.Value = Convert.ToString(int.Parse(iTotalBase.Value) + iTarifa);
                            iTotalTarifa.Value = Convert.ToString(int.Parse(iTotalTarifa.Value) + iTotalPasajero);

                            if (TotalCarritoSinFormato != null)
                                TotalCarritoSinFormato.Value = iTotalTarifa.Value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="sReserva"></param>
        public void setPagarReserva(UserControl PageSource, string sReserva)
        {
            clsCache cCache = new csCache().cCache();
            Label lblError = (Label)PageSource.FindControl("lblError");
            if (cCache != null)
            {
                try
                {
                    clsIdioma cIdioma = new clsIdioma();
                    cIdioma.LoadIdioma(csGeneralsPag.PaginaActual(), PageSource);
                    string sTC = clsValidaciones.GetKeyOrAdd("TarjetaCredito", "TC");
                    string sTD = clsValidaciones.GetKeyOrAdd("TarjetaDebito", "TD");
                    string sEFE = clsValidaciones.GetKeyOrAdd("Efectivo", "EFE");
                    string sTCP = clsValidaciones.GetKeyOrAdd("TarjetaCreditoSD", "TCP");
                    string sNAP = clsValidaciones.GetKeyOrAdd("FormaPagoNA", "NAP");

                    RadioButtonList rblFormasPago = (RadioButtonList)PageSource.FindControl("rblFormasPago");
                    RadioButtonList rblFranquicias = (RadioButtonList)PageSource.FindControl("rblFranquicias");


                    if (rblFormasPago.SelectedItem.Value.Equals(sEFE) || rblFormasPago.SelectedItem.Value.Equals(sNAP))
                    {
                        setInsertarFormaPago(rblFormasPago.SelectedItem.Value, PageSource, "air", sReserva);
                    }

                }
                catch { }
                //if (csGeneralsPag.ValidaFacturacion(cCache))
                //{
                //    
                //   
                //    HiddenField bCreditoDispersion = (HiddenField)PageSource.FindControl("bCreditoDispersion");
                //    bool CreditoDispersion = default(bool);

                //   
                //    try
                //    {
                //        if (bCreditoDispersion != null)
                //            CreditoDispersion = Convert.ToBoolean(bCreditoDispersion.Value);
                //    }
                //    catch { CreditoDispersion = false; }

                //   

                //    if (rblFormasPago.SelectedItem.Value.Equals(sTC) && CreditoDispersion)
                //    {
                //        if (rblFranquicias != null)
                //        {
                //            if (rblFranquicias.SelectedItem != null)
                //            {
                //                EnviarPagoDispersion(PageSource);
                //            }
                //            else
                //            {
                //                lblError.Text = "Por favor seleccione la franquicia con la cual desea realizar el pago";
                //            }
                //        }
                //    }
                //    else
                //    {
                //        if (rblFormasPago.SelectedItem.Value.Equals(sTC) && !CreditoDispersion)
                //        {
                //            if (rblFranquicias != null)
                //            {
                //                if (rblFranquicias.SelectedItem != null)
                //                {
                //                    EnviarValoresCompleto(sTCP, PageSource);
                //                }
                //                else
                //                {
                //                    lblError.Text = "Por favor seleccione la franquicia con la cual desea realizar el pago";
                //                }
                //            }
                //        }
                //        else
                //        {
                //            if (rblFormasPago.SelectedItem.Value.Equals(sTD))
                //            {
                //                EnviarValoresCompletoReserva(sTD, PageSource, sReserva);
                //            }
                //            else
                //            {
                //                if (rblFormasPago.SelectedItem.Value.Equals(sEFE) || rblFormasPago.SelectedItem.Value.Equals(sNAP) ||
                //                    rblFormasPago.SelectedItem.Value.Equals(sFIN) || rblFormasPago.SelectedItem.Value.Equals(sDOM))
                //                {
                //                    EnviarPagoEfectivo(rblFormasPago.SelectedItem.Value, PageSource, sReserva);
                //                }
                //                /*Forma pago Poliza*/
                //                else if (rblFormasPago.SelectedItem.Value.Equals(sPoliza))
                //                {
                //                    /*si el valor del carro de compras es menor o igual al valor de la poliza*/

                //                    Decimal valorTotalCarroCompras = default(decimal);
                //                    if (SetVerificarValorAplicar(PageSource, out valorTotalCarroCompras))
                //                    {
                //                        clsResultados csResultados = SetPagarConPoliza(PageSource);

                //                        if (csResultados.Error.Id == 1)
                //                        {
                //                            EnviarPagoEfectivo(sPoliza, PageSource);
                //                        }
                //                        else if (csResultados.Error.Id == 0)
                //                        {
                //                            if (lblError != null)
                //                                lblError.Text = "Ha ocurrido un error procesando la peticion, por favor pongase en contacto con la agencia";
                //                            clsParametros cParametros = new clsParametros();
                //                            cParametros.Id = 0;
                //                            cParametros.Complemento = "setPagar";
                //                            cParametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                //                            cParametros.ValidaInfo = false;
                //                            cParametros.Code = "950";
                //                            ExceptionHandled.Publicar(cParametros);
                //                            try
                //                            {
                //                                new csCache().setError(PageSource, cParametros);
                //                            }
                //                            catch { }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    lblError.Text = "Por favor guardar datos de facturación";
                //}
            }
        }
        //public string ObtenerUrlAgradecimiento(clsCache cCache, string sFormaPago, int iValor, string sTipoServicio, string sDetalleFOP)
        //{
        //    string sUrlAgradecimiento = "";
        //    try
        //    {
        //        VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
        //        csConsultasReserva cPagos = new csConsultasReserva();
        //        DataTable dtResultados = null;
        //        if (vo_OTA_AirLowFareSearchLLSRQ != null)
        //        {
        //            string sTipoDestinoInternacional = clsValidaciones.GetKeyOrAdd("TpoDestinoInternacional", "INAL");
        //            string sTipoDestinoNacional = clsValidaciones.GetKeyOrAdd("TpoDestinoNacional", "NAL");
        //            if (sTipoServicio == "" || sTipoServicio == null || sTipoServicio == "0")
        //                sTipoServicio = clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR");
        //            switch (vo_OTA_AirLowFareSearchLLSRQ.ETipoVuelo)
        //            {
        //                case Enum_TipoVuelo.Nacional:
        //                    switch (vo_OTA_AirLowFareSearchLLSRQ.ETipoTrayecto)
        //                    {
        //                        case Enum_TipoTrayecto.Ida:
        //                            dtResultados = cPagos.consulta_pagina_agradecimiento(sTipoDestinoNacional,
        //                                clsValidaciones.GetKeyOrAdd("sTipoTrayectoOneWay", "O"), sTipoServicio, sFormaPago, cCache.Empresa);
        //                            break;
        //                        case Enum_TipoTrayecto.IdaRegreso:
        //                            dtResultados = cPagos.consulta_pagina_agradecimiento(sTipoDestinoNacional,
        //                                clsValidaciones.GetKeyOrAdd("sTipoTrayectoRoundTrip", "R"), sTipoServicio, sFormaPago, cCache.Empresa);
        //                            break;
        //                    }
        //                    break;
        //                case Enum_TipoVuelo.Internacional:
        //                    dtResultados = cPagos.consulta_pagina_agradecimiento(sTipoDestinoInternacional, "", sTipoServicio, sFormaPago, cCache.Empresa);
        //                    if (dtResultados != null)
        //                    {
        //                        DataView dvFiltro = new DataView(dtResultados);
        //                        dvFiltro.RowFilter = "dblValorInicial <= " + iValor + " AND dblValorFinal >= " + iValor;
        //                        dtResultados = dvFiltro.ToTable();
        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }

        //            if (dtResultados != null)
        //            {
        //                sUrlAgradecimiento = dtResultados.Rows[0]["strURL"].ToString();
        //            }
        //        }
        //        else
        //        {
        //            dtResultados = cPagos.consulta_pagina_agradecimiento("", "", sTipoServicio, sFormaPago, cCache.Empresa);
        //            if (dtResultados != null)
        //            {
        //                if (iValor != null && iValor != 0)
        //                {
        //                    DataView dvFiltro = new DataView(dtResultados);
        //                    dvFiltro.RowFilter = "dblValorInicial <= " + iValor + " AND dblValorFinal >= " + iValor;
        //                    dtResultados = dvFiltro.ToTable();
        //                }
        //                sUrlAgradecimiento = dtResultados.Rows[0]["strURL"].ToString();
        //            }
        //        }
        //        csCarrito cCar = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
        //        DataSet dsData = cCar.GetDsReservas();
        //        DataTable tblCarrito = dsData.Tables["CarritoCompras"];
        //        if (tblCarrito.Rows.Count > 0)
        //        {
        //            if (sFormaPago != clsValidaciones.GetKeyOrAdd("PSE", "PSE")) 
        //                sUrlAgradecimiento += "?Ruta=" + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["strCiudad"].ToString() +
        //                    "&Itinerario=" + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["strOrigen"].ToString() + " - " + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["strDestino"].ToString() +
        //                    "&Record=" + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["strReserva"].ToString() +
        //                    "&Valor=" + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["intValorTotal"].ToString() +
        //                    "&IdCliente=" + cCache.Contacto + "&CodFormaPago=" + sFormaPago + "&DetFormaPago=" + sDetalleFOP;
        //        }
        //    }
        //    catch { }
        //    return sUrlAgradecimiento; 
        //}
        public string ObtenerUrlAgradecimiento(clsCache cCache, string sFormaPago, string sDetalleFOP, string sEstadoPago, string sMoneda)//, string sTipoServicio, string sDetalleFOP, string sEstadoPago)
        {
            string sUrlAgradecimiento = "";
            try
            {
                VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = clsSesiones.getParametrosAirBargain();
                csConsultasReserva cPagos = new csConsultasReserva();
                DataTable dtResultados = null;

                string sTipoDestinoInternacional = clsValidaciones.GetKeyOrAdd("TpoDestinoInternacional", "INAL");
                string sTipoDestinoNacional = clsValidaciones.GetKeyOrAdd("TpoDestinoNacional", "NAL");

                dtResultados = cPagos.consulta_pagina_agradecimiento("", "", "", sFormaPago, cCache.Empresa, "");

                if (dtResultados != null)
                {
                    sUrlAgradecimiento = dtResultados.Rows[0]["strURL"].ToString();
                }

                if (dtResultados != null)
                {
                    csCarrito cCar = new csCarrito("Reserva" + cCache.SessionID, "CarritoCompras");
                    DataSet dsData = cCar.GetDsReservas();
                    DataTable tblCarrito = dsData.Tables["CarritoCompras"];
                    if (tblCarrito.Rows.Count > 0)
                    {
                        if (sFormaPago != clsValidaciones.GetKeyOrAdd("PSE", "PSE"))
                            sUrlAgradecimiento += "?Ruta=" + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["strCiudad"].ToString() +
                                "&Itinerario=" + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["strOrigen"].ToString() + " - " + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["strDestino"].ToString() +
                                "&Record=" + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["strReserva"].ToString() +
                                "&Valor=" + tblCarrito.Rows[tblCarrito.Rows.Count - 1]["intValorTotal"].ToString() +
                                "&IdCliente=" + cCache.Contacto + "&CodFormaPago=" + sFormaPago + "&DetFormaPago=" + sDetalleFOP +
                                "&EstadoPago=" + sEstadoPago + "&Moneda=" + sMoneda;
                    }
                }
            }
            catch { }
            return sUrlAgradecimiento;
        }
        public void setInsertarFormaPago(string FormaPago, UserControl PageSource,string strServicio,string strReserva)
        {
            ExceptionHandled.Publicar("Inicio cambio forma de pago");
           
            try
            {
                 DataTable dtFOP = new CsConsultasVuelos().SPConsultaTabla("SPConsultaFOPEmpresa", new string[3] { new csCache().cCache().Empresa, new csCache().cCache().Idioma, strServicio });
            
                DataView dv =new DataView(dtFOP);
                dv.RowFilter = "strcodigo='" + FormaPago + "'";
                dtFOP = dv.ToTable();

                string strFormaPago = dtFOP.Rows[0]["intidfop"].ToString();

                string strEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP");
                strEstadoPago=new CsConsultasVuelos().ConsultaCodigo(strEstadoPago,"tblEstadosPago","intCodigo","strCode");

                bool bValida = new csReservas().CambiarEstadoPagoReserva(strReserva, Convert.ToInt16(strEstadoPago), Convert.ToInt16(strFormaPago));
                DataTable dt = new CsConsultasVuelos().Consultatabla("SELECT INTCODIGO FROM TBLRESMASTER WHERE STRRESERVA='" + strReserva + "' AND INTFORMAPAGO='" + strFormaPago + "' AND INTESTADOPAGO='" + strEstadoPago + "'");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        
                    }
                    else
                    {
                        ExceptionHandled.Publicar("No se pudo actualizar el estado de pago ni la forma de pago estadopago=" + strEstadoPago + " y la forma pago es" + strFormaPago + " formapago=" + FormaPago);
                        dt = new CsConsultasVuelos().Consultatabla("UPDATE tblresmaster SET  INTFORMAPAGO='" + strFormaPago + "' ,INTESTADOPAGO='" + strEstadoPago + "' WHERE STRRESERVA='" + strReserva + "'");
                    }
                }
                else
                {
                    ExceptionHandled.Publicar("No se pudo actualizar el estado de pago ni la forma de pago estadopago=" + strEstadoPago + " y la forma pago es" + strFormaPago + " formapago=" + FormaPago);
                    dt=new CsConsultasVuelos().Consultatabla("UPDATE tblresmaster SET  INTFORMAPAGO='"+strFormaPago+"' ,INTESTADOPAGO='"+strEstadoPago+"' WHERE STRRESERVA='"+strReserva +"'");
                }

                DataTable dtValida = new CsConsultasVuelos().Consultatabla("SELECT INTCODIGO FROM TBLRESMASTER WHERE STRRESERVA='" + strReserva + "' AND (INTFORMAPAGO = '0'  OR  INTESTADOPAGO='0')");
                try
                {
                    if (dtValida.Rows.Count > 0)
                    {
                        strFormaPago = new CsConsultasVuelos().ConsultaCodigo(FormaPago, "TBLFOP", "intidfop", "strcodigo");
                        dt = new CsConsultasVuelos().Consultatabla("UPDATE tblresmaster SET  INTFORMAPAGO='" + strFormaPago + "' ,INTESTADOPAGO='1' WHERE STRRESERVA='" + strReserva + "'");
                    }
                }
                catch { }

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
                cParametros.Complemento = "Metodo setInsertarFormaPago Error ";
                ExceptionHandled.Publicar(cParametros);
                
            }

           
            
        }
        private bool isOtroImpuesto(UserControl PageSource, string strCodigo, int intValor)
        {
            HiddenField iTotalBase = (HiddenField)PageSource.FindControl("iTotalBase");
            HiddenField iTotalTarifa = (HiddenField)PageSource.FindControl("iTotalTarifa");
            HiddenField iTotalIVA_Tarifa = (HiddenField)PageSource.FindControl("iTotalIVA_Tarifa");
            HiddenField iTotalImpuestos = (HiddenField)PageSource.FindControl("iTotalImpuestos");
            HiddenField iTotalImpuestoGasolina = (HiddenField)PageSource.FindControl("iTotalImpuestoGasolina");
            HiddenField iTotaBaselTA = (HiddenField)PageSource.FindControl("iTotaBaselTA");
            HiddenField iTotalIVA_TA = (HiddenField)PageSource.FindControl("iTotalIVA_TA");

            bool bEncontrado = false;
            try
            {
                if (!String.IsNullOrEmpty(strCodigo))
                {
                    strCodigo = strCodigo.Trim();
                    if (strCodigo.Length >= 2)
                    {
                        if (strCodigo.Substring(0, 2).Equals("YS"))// || strCodigo.Equals("IvaHotel"))
                        {
                            iTotalIVA_Tarifa.Value = Convert.ToString(int.Parse(iTotalIVA_Tarifa.Value) + intValor);
                        }
                        else
                        {
                            if (strCodigo != "IvaHotel")
                            {
                                if (strCodigo.Equals("TA") || strCodigo.Equals("TAN") || strCodigo.Equals("TAI"))
                                {
                                    iTotaBaselTA.Value = Convert.ToString(int.Parse(iTotaBaselTA.Value) + intValor);
                                }
                                else
                                {
                                    if (strCodigo.Equals("ITA") || strCodigo.Equals("ITAN") || strCodigo.Equals("ITAI"))
                                    {
                                        iTotalIVA_TA.Value = Convert.ToString(int.Parse(iTotalIVA_TA.Value) + intValor);
                                    }
                                    else
                                    {
                                        if (strCodigo.Substring(0, 2).Equals("YQ"))
                                        {
                                            iTotalImpuestoGasolina.Value = Convert.ToString(int.Parse(iTotalImpuestoGasolina.Value) + intValor);
                                        }
                                        else
                                        {
                                            if (strCodigo.Equals("ADFE"))
                                            {
                                                iTotaBaselTA.Value = Convert.ToString(int.Parse(iTotaBaselTA.Value) + intValor);
                                            }
                                            else
                                            {
                                                if (strCodigo.Equals("IVAADFE"))
                                                {
                                                    iTotalIVA_TA.Value = Convert.ToString(int.Parse(iTotalIVA_TA.Value) + intValor);
                                                }
                                                else
                                                {
                                                    if (strCodigo.Length >= 3)
                                                    {
                                                        if (strCodigo.Substring(0, 3).Equals("FEE"))
                                                        {
                                                            iTotaBaselTA.Value = Convert.ToString(int.Parse(iTotaBaselTA.Value) + intValor);
                                                        }
                                                        else
                                                        {
                                                            if (strCodigo.Length >= 4)
                                                            {
                                                                if (strCodigo.Substring(0, 4).Equals("IFEE"))
                                                                {
                                                                    iTotalIVA_TA.Value = Convert.ToString(int.Parse(iTotalIVA_TA.Value) + intValor);
                                                                }
                                                                else
                                                                {
                                                                    bEncontrado = true;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                bEncontrado = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        bEncontrado = true;
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
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "isOtroImpuesto";
                ExceptionHandled.Publicar(cParametros);
            }
            return bEncontrado;
        }
        private bool isOtroImpuestoDecimal(UserControl PageSource, string strCodigo, decimal dValor)
        {

            HiddenField iTotalIVA_TarifaDecimal = (HiddenField)PageSource.FindControl("iTotalIVA_TarifaDecimal");
            HiddenField iTotalImpuestosDecimal = (HiddenField)PageSource.FindControl("iTotalImpuestos");
            HiddenField iTotalImpuestoGasolinaDecimal = (HiddenField)PageSource.FindControl("iTotalImpuestoGasolina");
            HiddenField iTotaBaselTADecimal = (HiddenField)PageSource.FindControl("iTotaBaselTADecimal");
            HiddenField iTotalIVA_TADecimal = (HiddenField)PageSource.FindControl("iTotalIVA_TADecimal");

            bool bEncontrado = false;
            try
            {
                if (!String.IsNullOrEmpty(strCodigo))
                {
                    strCodigo = strCodigo.Trim();
                    if (strCodigo.Length >= 2)
                    {
                        if (strCodigo.Substring(0, 2).Equals("YS") || strCodigo.Equals("IvaHotel"))
                        {
                            if (iTotalIVA_TarifaDecimal != null)
                            {
                                try
                                {
                                    iTotalIVA_TarifaDecimal.Value = Convert.ToString(Convert.ToDecimal(iTotalIVA_TarifaDecimal.Value.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal)) + dValor);
                                }
                                catch (Exception) { }
                            }
                        }
                        else
                        {
                            if (strCodigo.Equals("TA"))
                            {
                                if (iTotaBaselTADecimal != null)
                                {
                                    try
                                    {
                                        iTotaBaselTADecimal.Value = Convert.ToString(Convert.ToDecimal(iTotaBaselTADecimal.Value.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal)) + dValor);
                                    }
                                    catch (Exception) { }
                                }
                            }
                            else
                            {
                                if (strCodigo.Equals("ITA"))
                                {
                                    if (iTotalIVA_TADecimal != null)
                                    {
                                        try
                                        {
                                            iTotalIVA_TADecimal.Value = Convert.ToString(Convert.ToDecimal(iTotalIVA_TADecimal.Value.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal)) + dValor);
                                        }
                                        catch (Exception) { }
                                    }
                                }
                                else
                                {
                                    if (strCodigo.Substring(0, 2).Equals("YQ"))
                                    {
                                        if (iTotalImpuestoGasolinaDecimal != null)
                                        {
                                            try
                                            {
                                                iTotalImpuestoGasolinaDecimal.Value = Convert.ToString(Convert.ToDecimal(iTotalImpuestoGasolinaDecimal.Value.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal)) + dValor);
                                            }
                                            catch (Exception) { }
                                        }
                                    }
                                    else
                                    {
                                        if (strCodigo.Equals("ADFE"))
                                        {
                                            if (iTotaBaselTADecimal != null)
                                            {
                                                try
                                                {
                                                    iTotaBaselTADecimal.Value = Convert.ToString(Convert.ToDecimal(iTotaBaselTADecimal.Value.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal)) + dValor);
                                                }
                                                catch (Exception) { }
                                            }
                                        }
                                        else
                                        {
                                            if (strCodigo.Equals("IVAADFE"))
                                            {
                                                if (iTotalIVA_TADecimal != null)
                                                {
                                                    try
                                                    {
                                                        iTotalIVA_TADecimal.Value = Convert.ToString(Convert.ToDecimal(iTotalIVA_TADecimal.Value.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal)) + dValor);
                                                    }
                                                    catch (Exception) { }
                                                }
                                            }
                                            else
                                            {
                                                if (strCodigo.Length >= 3)
                                                {
                                                    if (strCodigo.Substring(0, 3).Equals("FEE"))
                                                    {
                                                        if (iTotaBaselTADecimal != null)
                                                        {
                                                            try
                                                            {
                                                                iTotaBaselTADecimal.Value = Convert.ToString(Convert.ToDecimal(iTotaBaselTADecimal.Value.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal)) + dValor);
                                                            }
                                                            catch (Exception) { }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (strCodigo.Length >= 4)
                                                        {
                                                            if (strCodigo.Substring(0, 4).Equals("IFEE"))
                                                            {
                                                                if (iTotalIVA_TADecimal != null)
                                                                {
                                                                    try
                                                                    {
                                                                        iTotalIVA_TADecimal.Value = Convert.ToString(Convert.ToDecimal(iTotalIVA_TADecimal.Value.Replace(".", sCaracterDecimal).Replace(",", sCaracterDecimal)) + dValor);
                                                                    }
                                                                    catch (Exception) { }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                bEncontrado = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            bEncontrado = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    bEncontrado = true;
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
            catch (Exception Ex)
            {
                clsParametros cParametros = new clsParametros();
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Metodo = "isOtroImpuesto";
                ExceptionHandled.Publicar(cParametros);
            }
            return bEncontrado;
        }
        public void EnviarValoresCompleto(string FormaPago, UserControl PageSource, string strServicio, string DetalleFormaPago)
        {
            VO_Credentials vo_Credenciales = clsCredenciales.Credenciales(Enum_ProveedorWebServices.POL);
            if (vo_Credenciales != null)
            {

                Label lblError = (Label)PageSource.FindControl("lblError");
                HiddenField iTotalBase = (HiddenField)PageSource.FindControl("iTotalBase");
                HiddenField iTotalIVA_Tarifa = (HiddenField)PageSource.FindControl("iTotalIVA_Tarifa");
                HiddenField strRecord = (HiddenField)PageSource.FindControl("strRecord");
                HiddenField sRuta = (HiddenField)PageSource.FindControl("sRuta");
                HiddenField sFecha = (HiddenField)PageSource.FindControl("sFecha");
                HiddenField TotalCarritoSinFormato = (HiddenField)PageSource.FindControl("TotalCarritoSinFormato");
                RadioButtonList rblFranquicias = (RadioButtonList)PageSource.FindControl("rblFranquicias");
                clsCache cCache = new csCache().cCache();
                clsCacheControl cCacheControl = new clsCacheControl();
                string sSesion = cCacheControl.RecuperarSesionId((Page)HttpContext.Current.Handler);
                csCarrito csCarCompUnion = new csCarrito("Reserva" + sSesion, "CarritoCompras");


                string URL_PAGO = vo_Credenciales.UrlWebServices;
                string sUsuaioPOL = vo_Credenciales.User;
                string sPOL_Prueba = vo_Credenciales.Prueba;
                string strDescripcion = " Venta pagina " + cCache.Empresa;
                string sExtra2 = vo_Credenciales.Extra2;
                string sCiudadEnvio = vo_Credenciales.CiudadEnvio;
                string sDirEnvio = vo_Credenciales.DireccionEnvio;
                int iValor = 0;
                string sTipoPago = "";
                if (FormaPago.Equals(clsValidaciones.GetKeyOrAdd("TarjetaCreditoSD", "TCP")))
                {
                    if (rblFranquicias != null)
                        sTipoPago = rblFranquicias.SelectedItem.Value;
                }
                else if (FormaPago.Equals("PSE"))
                {
                    sTipoPago = vo_Credenciales.PlantillaPse;
                }
                else
                {
                    if (rblFranquicias != null)
                        sTipoPago = vo_Credenciales.PlantillaPse;
                }
                iValor = Convert.ToInt32(clsValidaciones.getDecimalNotRound(TotalCarritoSinFormato.Value));
                string sMonedaLocal = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                //----------- Validacion de la url de agradecimiento               
                //string sExtra2Aux = ObtenerUrlAgradecimiento(cCache, FormaPago, iValor, "", DetalleFormaPago);
                //string sExtra2Aux = "http://66.165.137.173/Pagina/Presentacion/Index.aspx";
                //if (sExtra2Aux.Length > 0)
                    //sExtra2 = sExtra2Aux;
                //--------------------------------------------------

                string sUrlPago =
                URL_PAGO + USUARIOID + sUsuaioPOL +
                "&" + REFVENTA + strRecord.Value +
                "&" + DESCRIPCION + strDescripcion +
                "&" + VALOR + iValor +
                "&" + VALORADICIONAL + "0" +
                "&" + IVA + iTotalIVA_Tarifa.Value +
                "&" + BASEDEVOLUCIONIVA + iTotalBase.Value +
                "&" + MONEDA + sMonedaLocal +
                "&" + NOMBRECOMPRADOR + "" +
                "&" + TIPODOCUMENTOIDENTIFICACION + "1" +
                "&" + DOCUMENTOIDENTIFICACION + cCache.Identificacion +
                "&" + PRUEBA + sPOL_Prueba +
                "&" + TARIFAADMINISTRATIVA + "0" +
                "&" + IVATARIFAADMINISTRATIVA + "0" +
                "&" + EXTRA1 + strRecord.Value +
                "&" + NOMBRE_USUARIO + cCache.Nombre +
                "&" + EMAILCOMPRADOR + cCache.User +
                "&" + EXTRA2 + sExtra2 +
                "&" + EXTRA3 + sRuta.Value +
                "&" + EXTRA4 + sFecha.Value +
                "&" + EXTRA5 + "0" +
                "&" + PLANTILLA + sTipoPago +
                "&" + BASEDEVOLUCIONTARIFAADMINISTRATIVA + "0" +
                "&" + DIR_ENVIO + sDirEnvio +
                "&" + CIUDAD_ENVIO + sCiudadEnvio;


                if (strServicio.Trim().ToUpper().Equals("AIR"))
                {
                    setInsertarFormaPago(FormaPago, PageSource, strServicio, strRecord.Value);
                    new csUtilitarios().setCorreos(strRecord.Value, FormaPago, "RSABRE");
                }
                else if (strServicio.Trim().ToUpper().Equals("PLANES"))
                {
                    setInsertarFormaPagoPlanes(FormaPago, strRecord.Value);
                }
                else if (strServicio.Trim().ToUpper().Equals("HOTELES"))
                {
                    setInsertarFormaPago(FormaPago, PageSource, "HINTER", strRecord.Value);
                    new csUtilitarios().setCorreos(strRecord.Value, "PSE", "RTT");
                }

                csCarCompUnion.LimpiarCarrito();
                Negocios_WebServiceSession._CerrarSesion();
                clsValidaciones.RedirectPagina(sUrlPago);

            }
        }
        public void setInsertarFormaPagoPlanes(string FormaPago, string strReserva)
        {
            DataTable dtFOP = new DataTable();

            try
            {

                string strFormaPago = new CsConsultasVuelos().ConsultaCodigo(FormaPago, "TBLFOP", "INTIDFOP", "STRCODIGO");
                string strEstadoPago = clsValidaciones.GetKeyOrAdd("EstadoPagoPendiente", "PP");
                strEstadoPago = new CsConsultasVuelos().ConsultaCodigo(strEstadoPago, "tblEstadosPago", "intCodigo", "strCode");

                bool bValida = new csReservas().CambiarEstadoPagoReserva(strReserva, Convert.ToInt16(strEstadoPago), Convert.ToInt16(strFormaPago));

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
                cParametros.Complemento = "Metodo setInsertarFormaPago Error ";

            }



        }
        public bool setPagoPacificard(UserControl PageSource)
        {
            bool bRespuesta = true;           
            Label lblError = (Label)PageSource.FindControl("lblError");
            Label lblErrorVentana = (Label)PageSource.FindControl("lblErrorVentana");           
            clsIdioma cIdioma = new clsIdioma();
            cIdioma.LoadIdioma(csGeneralsPag.PaginaActual(), PageSource);
            HiddenField bCreditoDispersion = (HiddenField)PageSource.FindControl("bCreditoDispersion");
            bool CreditoDispersion = default(bool);

            string sTC = clsValidaciones.GetKeyOrAdd("TarjetaCredito", "TC");
            string sTD = clsValidaciones.GetKeyOrAdd("TarjetaDebito", "TD");
            string sEFE = clsValidaciones.GetKeyOrAdd("Efectivo", "EFE");
            string sTCP = clsValidaciones.GetKeyOrAdd("TarjetaCreditoSD", "TCP");
            string sNAP = clsValidaciones.GetKeyOrAdd("FormaPagoNA", "NAP");
            string sPoliza = clsValidaciones.GetKeyOrAdd("Poliza", "Poliza");
            string sBancoGuayaquil = clsValidaciones.GetKeyOrAdd("FormaPagoBG", "FPBG");
            try
            {
                if (bCreditoDispersion != null)
                    CreditoDispersion = Convert.ToBoolean(bCreditoDispersion.Value);
            }
            catch
            {
                CreditoDispersion = false;
            }

            RadioButtonList rblFormasPago = (RadioButtonList)PageSource.FindControl("rblFormasPago");
            RadioButtonList rblFranquicias = (RadioButtonList)PageSource.FindControl("rblFranquicias");

            if (rblFormasPago.SelectedItem.Value.Equals(sTC) && CreditoDispersion)
            {
                EnviarValoresPagoPacificardEmision(sTC, PageSource);
            }
            else
            {
                if (rblFormasPago.SelectedItem.Value.Equals(sTC) && !CreditoDispersion)
                {
                    //if (rblFranquicias != null)
                    //{
                    //    if (rblFranquicias.SelectedItem != null)
                    //    {
                    //        if (rblFranquicias.SelectedItem.Value.Equals(clsValidaciones.GetKeyOrAdd("PagosMaster", "PagosMaster")) ||
                    //            rblFranquicias.SelectedItem.Value.Equals(clsValidaciones.GetKeyOrAdd("PagosVisa", "PagosVisa")))
                                EnviarValoresPagoPacificard(sTC, PageSource);
                        //}
                        //else
                        //{
                        //    lblErrorVentana.Text = "Por favor seleccione la franquicia con la cual desea realizar el pago";
                        //    bRespuesta = false;
                        //}
                    //}
                }                
            }            
            return bRespuesta;
        }

        /// <summary>
        /// Metodo que arma la url para invocacion de pagos de pacificard
        /// </summary>
        /// <param name="FormaPago">referencia de forma de pago</param>
        /// <param name="PageSource">Usercontrol</param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          13-11-2014
        /// -------------------
        /// Control de Cambios
        /// -------------------       
        /// </remarks>
        protected void EnviarValoresPagoPacificard(string FormaPago, UserControl PageSource)
        {
            VO_Credentials vo_Credenciales = clsCredenciales.Credenciales(Enum_ProveedorWebServices.POL);
            if (vo_Credenciales != null)
            {
                Label lblError = (Label)PageSource.FindControl("lblError");
                HiddenField iTotalBase = (HiddenField)PageSource.FindControl("iTotalBase");
                HiddenField iTotalIVA_Tarifa = (HiddenField)PageSource.FindControl("iTotalIVA_Tarifa");
                HiddenField strRecord = (HiddenField)PageSource.FindControl("strRecord");
                HiddenField sRuta = (HiddenField)PageSource.FindControl("sRuta");
                HiddenField sFecha = (HiddenField)PageSource.FindControl("sFecha");
                HiddenField TotalCarritoSinFormato = (HiddenField)PageSource.FindControl("TotalCarritoSinFormato");
                RadioButtonList rblFranquicias = (RadioButtonList)PageSource.FindControl("rblFranquicias");
                clsCache cCache = new csCache().cCache();
                clsCacheControl cCacheControl = new clsCacheControl();
                string sSesion = cCacheControl.RecuperarSesionId((Page)HttpContext.Current.Handler);
                csCarrito csCarCompUnion = new csCarrito("Reserva" + sSesion, "CarritoCompras");
                DataTable TablaPlanes = csCarCompUnion.RecuperarTabla();

                string sDetallePago = getTextoDetallePago(TablaPlanes);
                
                string URL_PAGO = vo_Credenciales.UrlWebServices;
                string sUsuario = vo_Credenciales.User;             
                string sExtra2 = vo_Credenciales.Extra2;
                decimal iValor = 0;
                iValor = clsValidaciones.getDecimalNotRound(TotalCarritoSinFormato.Value);
                string sMonedaLocal = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                string sProyecto = clsSesiones.getProyecto();

                string sUrlPago =
                URL_PAGO + USERPACIFICARD + sUsuario +
                "&" + CODOPERACION + sProyecto +//strRecord.Value +
                "&" + CODMONEDA + sMonedaLocal +
                "&" + MONTO + iValor +
                "&" + RESERVADO1 + iValor +//iTotalIVA_Tarifa.Value +
                "&" + RESERVADO2 + "0" +
                "&" + RESERVADO3 + clsSesiones.getIdioma() +//iTotalBase.Value +               
                "&" + DETALLE + sDetallePago +
                "&" + NOMBRECOMPRADORPC + cCache.Nombre +
                "&" + EMAILCOMPRADOR + cCache.User +
                "&" + PAGINACONFIRMACION + sExtra2;
               
                csCarCompUnion.LimpiarCarrito();
                Negocios_WebServiceSession._CerrarSesion();
                clsValidaciones.RedirectPagina(sUrlPago);
            }
        }
        /// <summary>
        /// metodo qu eenvia el valor a pagar a pacificard enviando tambien el codigo de la aerolinea
        /// </summary>
        /// <param name="FormaPago"></param>
        /// <param name="PageSource"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          13-11-2014
        /// -------------------
        /// Control de Cambios
        /// -------------------          
        /// </remarks>
        protected void EnviarValoresPagoPacificardEmision(string FormaPago, UserControl PageSource)
        {
            VO_Credentials vo_Credenciales = clsCredenciales.Credenciales(Enum_ProveedorWebServices.POL);
            if (vo_Credenciales != null)
            {
                Label lblError = (Label)PageSource.FindControl("lblError");
                HiddenField iTotalBase = (HiddenField)PageSource.FindControl("iTotalBase");
                HiddenField iTotalIVA_Tarifa = (HiddenField)PageSource.FindControl("iTotalIVA_Tarifa");
                HiddenField strRecord = (HiddenField)PageSource.FindControl("strRecord");
                HiddenField sRuta = (HiddenField)PageSource.FindControl("sRuta");
                HiddenField sFecha = (HiddenField)PageSource.FindControl("sFecha");
                HiddenField TotalCarritoSinFormato = (HiddenField)PageSource.FindControl("TotalCarritoSinFormato");
                HiddenField sAerolinea = (HiddenField)PageSource.FindControl("sAerolinea");
                RadioButtonList rblFranquicias = (RadioButtonList)PageSource.FindControl("rblFranquicias");
                clsCache cCache = new csCache().cCache();
                clsCacheControl cCacheControl = new clsCacheControl();
                string sSesion = cCacheControl.RecuperarSesionId((Page)HttpContext.Current.Handler);
                csCarrito csCarCompUnion = new csCarrito("Reserva" + sSesion, "CarritoCompras");
                DataTable TablaPlanes = csCarCompUnion.RecuperarTabla();

                string sDetallePago = getTextoDetallePago(TablaPlanes);
               
                string URL_PAGO = vo_Credenciales.UrlWebServices;
                string sUsuario = vo_Credenciales.User;               
                string sExtra2 = vo_Credenciales.Extra2;               
                decimal iValor = 0;
                iValor = clsValidaciones.getDecimalNotRound(TotalCarritoSinFormato.Value);
                string sMonedaLocal = clsValidaciones.GetKeyOrAdd("MonedaLocal", "COP");
                string sProyecto = clsSesiones.getProyecto();

                string sUrlPago =
                URL_PAGO + USERPACIFICARD + sUsuario +
                "&" + CODOPERACION + strRecord.Value +
                "&" + CODMONEDA + sMonedaLocal +
                "&" + MONTO + iValor +
                "&" + RESERVADO1 + iValor +//iTotalIVA_Tarifa.Value +
                "&" + RESERVADO2 + "0" +
                "&" + RESERVADO3 + clsSesiones.getIdioma() +//iTotalBase.Value +               
                "&" + DETALLE + sDetallePago +
                "&" + NOMBRECOMPRADORPC + cCache.Nombre +
                "&" + EMAILCOMPRADOR + cCache.User +
                "&" + PAGINACONFIRMACION + sExtra2 +
                "&" + AEROLINEA + sAerolinea.Value;
                
                csCarCompUnion.LimpiarCarrito();
                Negocios_WebServiceSession._CerrarSesion();
                ExceptionHandled.Publicar("Url de pagos:  --- " + sUrlPago + " ---");
                clsValidaciones.RedirectPagina(sUrlPago);
            }
        }

        /// <summary>
        /// Metodo que genera un texto con los records de reserva de todos los servicios
        /// </summary>
        /// <param name="TablaPrincipal">Tabla de reservas</param>
        /// <returns>texto de descripcion</returns>
        /// /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          13-11-2014
        /// -------------------
        /// Control de Cambios
        /// -------------------       
        /// </remarks>        
        public string getTextoDetallePago(DataTable TablaPrincipal)
        {
            string sTexto = "";
            try
            {
                int i = 0;
                while (i < TablaPrincipal.Rows.Count)
                {
                    if (TablaPrincipal.Rows[i]["StrTipoPlan"].ToString().Equals(clsValidaciones.GetKeyOrAdd("Aereo_WSTame", "AIR_TAME")) ||
                        TablaPrincipal.Rows[i]["StrTipoPlan"].ToString().Equals(clsValidaciones.GetKeyOrAdd("Aereo_WS", "AIR")))
                    {
                        sTexto = "Reserva vuelo codigo: ";
                    }
                    else
                    {
                        if (TablaPrincipal.Rows[i]["StrTipoPlan"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TIPOPLANCARWS", "CAR")))
                        {
                            sTexto = "Reserva auto codigo: ";
                        }
                        else
                        {
                            if (TablaPrincipal.Rows[i]["StrTipoPlan"].ToString().Equals(clsValidaciones.GetKeyOrAdd("HOT_WS", "HOT")) ||
                                TablaPrincipal.Rows[i]["StrTipoPlan"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoHotelesHotelBeds", "HOTBED")) ||
                                TablaPrincipal.Rows[i]["StrTipoPlan"].ToString().Equals(clsValidaciones.GetKeyOrAdd("TipoHotelesTotalTrip", "TotalTrip")))
                            {
                                sTexto = "Reserva hotel codigo: ";
                            }
                            else
                            {
                                sTexto = "Reserva tour codigo: ";
                            }

                        }
                    }
                    if (i == TablaPrincipal.Rows.Count - 1)
                        sTexto = sTexto + TablaPrincipal.Rows[i]["strReserva"].ToString();
                    else
                        sTexto = sTexto + TablaPrincipal.Rows[i]["strReserva"].ToString() + "; ";

                    i++;
                }
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
                cParametros.Metodo = "getTextoDetalleDiners";
                cParametros.Complemento = "Texto de detalle de diners";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return sTexto;
        }
    }
}

