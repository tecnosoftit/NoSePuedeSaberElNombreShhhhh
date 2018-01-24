using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;
using Ssoft.ValueObjects;
using Ssoft.Rules.Corporativo;
using Ssoft.Rules.Reservas;
using Ssoft.Rules.Administrador;
using Ssoft.Rules.WebServices;
using Ssoft.Rules.Pagina;
using WS_SsoftSabre.Air;
using Ssoft.Rules.Generales;
using SsoftQuery.Vuelos;

namespace Ssoft.Pages
{
    public static class csWebServices
    {
        private const string sCommandTR = "WPQY";
        private const string sCommandTM = "WPNCS";
        private const string sCommandTA = "WP";
        private const string strReserva = "strReserva";
        private const string intTipoPlan = "intTipoPlan";
        private const string intNivel = "intNivel";
        private const string strLocalizadorExt = "strLocalizadorExt";

        public static clsResultados SubirReserva(string sReserva, Enum_ProveedorWebServices eWebServices)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            csReservas csRes = new csReservas();
            cParametros.Id = 1;
            try
            {
                switch (eWebServices)
                {
                    case Enum_ProveedorWebServices.Sabre:
                        cResultados = SubirReservaSabre(sReserva, "0", "0");
                        break;                   
                    default:
                        break;
                }
                try
                {
                    // Verifica si trae resultados
                    if (!cResultados.Error.Id.Equals(0))
                    {
                        cParametros = csRes.GuardaReservaGen(cResultados.dsResultados);
                        if (cParametros.Id.Equals(0))
                        {
                            cParametros.ViewMessage = new List<string>();
                            cParametros.Sugerencia = new List<string>();

                            cParametros.ViewMessage.Add("La reserva no se subio, o ya se encuentar cargada");
                            cParametros.Sugerencia.Add("Por favor revise");

                            cResultados.Error = cParametros;
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
                    cParametros.Complemento = "guardar reserva ";
                    ExceptionHandled.Publicar(cParametros);
                    cResultados.Error = cParametros;
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
                cParametros.Complemento = "WebServices ";
                ExceptionHandled.Publicar(cParametros);
                cResultados.Error = cParametros;
            }
            return cResultados;
        }
        /// <summary>
        /// Proceso para subir reservas generales  - Subir Reserva - SRSABRE
        /// </summary>
        /// <param name="sReserva">Codigo de la reserva</param>
        /// <param name="eWebServices">Tipo de webservices, enumerado</param>
        /// <param name="sProyecto">Codigo del proyecto</param>
        /// <param name="sContacto">Codigo del contacto</param>
        /// <returns>Clase de errores, clsResultados</returns>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-09-13
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static clsResultados SubirReserva(string sReserva, Enum_ProveedorWebServices eWebServices, string sProyecto, string sContacto)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            csReservas csRes = new csReservas();
            cParametros.Id = 1;
            try
            {
                switch (eWebServices)
                {
                    case Enum_ProveedorWebServices.Sabre:
                        cResultados = SubirReservaSabre(sReserva, "0", "0", sProyecto, sContacto);
                        break;

                   

                    default:
                        break;
                }
                try
                {
                    // Verifica si trae resultados
                    if (!cResultados.Error.Id.Equals(0))
                    {
                        cParametros = csRes.GuardaReservaGen(cResultados.dsResultados);
                        if (cParametros.Id.Equals(0))
                        {
                            cParametros.ViewMessage = new List<string>();
                            cParametros.Sugerencia = new List<string>();

                            cParametros.ViewMessage.Add("La reserva no se subio, o ya se encuentar cargada");
                            cParametros.Sugerencia.Add("Por favor revise");

                            cResultados.Error = cParametros;
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
                    cParametros.Complemento = "guardar reserva ";
                    ExceptionHandled.Publicar(cParametros);
                    cResultados.Error = cParametros;
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
                cParametros.Complemento = "WebServices ";
                ExceptionHandled.Publicar(cParametros);
                cResultados.Error = cParametros;
            }
            return cResultados;
        }
        public static clsParametros CancelarReserva(List<string> sReserva, Enum_ProveedorWebServices eWebServices, string sMotivoCancelacion)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {
                switch (eWebServices)
                {
                    case Enum_ProveedorWebServices.Sabre:
                        cParametros = CancelarReservaSabre(sReserva);
                        break;

                   

                    default:
                        break;
                }
                if (!cParametros.Id.Equals(0))
                {
                    ModificarEstadoReserva(sReserva[0], sMotivoCancelacion);
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
                cParametros.Complemento = "WebServices ";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }

             
       
        /// <summary>
        /// Metodo para subir reservas de sabre - Subir Reserva - SRSABRE
        /// </summary>
        /// <param name="sReserva">Codigo de reserva a subir</param>
        /// <param name="sTA">Valor de la TA</param>
        /// <param name="sITA">Valor del Iva de la TA</param>
        /// <param name="sProyecto">Codigo del proyecto a la cual se va a asociar la reserva, si es0, se inserta como nueva</param>
        /// <param name="sContacto">Codigo del contacto al cual se le asociara la reserva</param>
        /// <returns>Clase de errores, clsResultados</returns>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2012-09-13
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static clsResultados SubirReservaSabre(string sReserva, string sTA, string sITA, string sProyecto, string sContacto)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            try
            {
                cParametros.Id = 1;
                Negocios_WebService_OTA_AirLowFareSearch cReserva = new Negocios_WebService_OTA_AirLowFareSearch();
                cResultados = cReserva.getSubirReserva(sReserva, sTA, sITA, sProyecto, sContacto);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "WebServices ";
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        public static clsResultados SubirReservaSabre(string sReserva, string sTA, string sITA)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            try
            {
                cParametros.Id = 1;
                Negocios_WebService_OTA_AirLowFareSearch cReserva = new Negocios_WebService_OTA_AirLowFareSearch();
                cResultados = cReserva.getSubirReserva(sReserva, sTA, sITA);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "WebServices ";
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
        public static clsResultados TicketReservaSabre(string sReserva)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            try
            {
                cParametros.Id = 1;
                Negocios_WebService_OTA_AirLowFareSearch cReserva = new Negocios_WebService_OTA_AirLowFareSearch();
                cResultados = cReserva.GetDsBusquedaRecordSabreAir(sReserva);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "WebServices Tickets";
                cResultados.Error = cParametros;
                ExceptionHandled.Publicar(cParametros);
            }
            return cResultados;
        }
      
      
    


        private static clsParametros CancelarReservaSabre(List<string> sReserva)
        {
            clsParametros cParametros = new clsParametros();
            try
            {
                Negocios_WebService_OTA_AirLowFareSearch cCancel = new Negocios_WebService_OTA_AirLowFareSearch();

                cParametros = cCancel.GetCancelRecordSabre(sReserva[0]);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "WebServices ";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
       
     
        private static Enum_TipoRemark eTipoRemark(string sIdRemark)
        {
            Enum_TipoRemark eRemark = Enum_TipoRemark.Simple;
            //tblRefere otbleRefere = new tblRefere();

            //otbleRefere.Get(sIdRemark);
            if (sIdRemark !="")
            {
                string sRemark ="1";
                if (sRemark.Equals(Enum_TipoRemark.Compuesto.GetHashCode().ToString()))
                {
                    eRemark = Enum_TipoRemark.Compuesto;
                }
                if (sRemark.Equals(Enum_TipoRemark.Direccion.GetHashCode().ToString()))
                {
                    eRemark = Enum_TipoRemark.Direccion;
                }
                if (sRemark.Equals(Enum_TipoRemark.DireccionCliente.GetHashCode().ToString()))
                {
                    eRemark = Enum_TipoRemark.DireccionCliente;
                }
                if (sRemark.Equals(Enum_TipoRemark.Grupo.GetHashCode().ToString()))
                {
                    eRemark = Enum_TipoRemark.Grupo;
                }
                if (sRemark.Equals(Enum_TipoRemark.Historico.GetHashCode().ToString()))
                {
                    eRemark = Enum_TipoRemark.Historico;
                }
                if (sRemark.Equals(Enum_TipoRemark.Impresion.GetHashCode().ToString()))
                {
                    eRemark = Enum_TipoRemark.Impresion;
                }
                if (sRemark.Equals(Enum_TipoRemark.Libre.GetHashCode().ToString()))
                {
                    eRemark = Enum_TipoRemark.Libre;
                }
                if (sRemark.Equals(Enum_TipoRemark.Oculto.GetHashCode().ToString()))
                {
                    eRemark = Enum_TipoRemark.Oculto;
                }
                if (sRemark.Equals(Enum_TipoRemark.Simple.GetHashCode().ToString()))
                {
                    eRemark = Enum_TipoRemark.Simple;
                }
            }
            return eRemark;
        }
        public static Enum_ProveedorWebServices eProveedorWebServices(string sIdWs)
        {
            Enum_ProveedorWebServices eWs = Enum_ProveedorWebServices.Sabre;

            if (sIdWs.Equals(Enum_ProveedorWebServices.Sabre.GetHashCode().ToString()))
            {
                eWs = Enum_ProveedorWebServices.Sabre;
            }
            if (sIdWs.Equals(Enum_ProveedorWebServices.Cotelco.GetHashCode().ToString()))
            {
                eWs = Enum_ProveedorWebServices.Cotelco;
            }         
            if (sIdWs.Equals(Enum_ProveedorWebServices.Planes.GetHashCode().ToString()))
            {
                eWs = Enum_ProveedorWebServices.Planes;
            }
            if (sIdWs.Equals(Enum_ProveedorWebServices.Tame.GetHashCode().ToString()))
            {
                eWs = Enum_ProveedorWebServices.Tame;
            }
            if (sIdWs.Equals(Enum_ProveedorWebServices.TotalTrip.GetHashCode().ToString()))
            {
                eWs = Enum_ProveedorWebServices.TotalTrip;
            }
            return eWs;
        }
        public static string RecuperarSnapCode()
        {
            string sSnapCode = null;
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    //if (cCache.Corporativo != null)
                    //{
                    //    int iTotal = cCache.Corporativo.Count;
                    //    for (int i = 0; i < iTotal; i++)
                    //    {
                    //        if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
                    //        {
                    //            if (cCache.Corporativo[i].Convenio != null)
                    //            {
                    //                int iTotalConv = cCache.Corporativo[i].Convenio.Count;
                    //                for (int j = 0; j < iTotalConv; j++)
                    //                {
                    //                    if (cCache.Corporativo[i].Convenio[j].Tipo.Equals(Enum_WebServices.SabreAir))
                    //                    {
                    //                        if (cCache.Corporativo[i].Convenio[j].OperaConvenio != null)
                    //                        {
                    //                            int iTotalOperaConv = cCache.Corporativo[i].Convenio[j].OperaConvenio.Count;
                    //                            for (int k = 0; k < iTotalOperaConv; k++)
                    //                            {
                    //                                if (cCache.Corporativo[i].Convenio[j].OperaConvenio[k].Operador.Equals("SnapCode"))
                    //                                {
                    //                                    sSnapCode = cCache.Corporativo[i].Convenio[j].OperaConvenio[k].Convenio;
                    //                                }
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            catch 
            {
            }
            return sSnapCode;
        }
        private static void RemarksCache(List<VO_Remarks> lRemark, clsCache cCache)
        {
            clsRemark cRemark = new clsRemark();
            List<clsRemark> clRemark = new List<clsRemark>();
            //clsCorporativo cCorporativo = new clsCorporativo();
            //List<clsCorporativo> clCorporativo = new List<clsCorporativo>();

            //if (cCache.Corporativo != null)
            //{
            //    int iTotal = cCache.Corporativo.Count;
            //    for (int i = 0; i < iTotal; i++)
            //    {
            //        if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
            //        {
            //            if (cCache.Corporativo[i].Remark != null)
            //            {
            //                int iTotalRemark = cCache.Corporativo[i].Remark.Count;
            //                for (int j = 0; j < iTotalRemark; j++)
            //                {
            //                    if (cCache.Corporativo[i].Remark[j].Operador.Equals(Enum_ProveedorWebServices.Sabre))
            //                    {
            //                        cCache.Corporativo[i].Remark[j].Remark = lRemark;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                cRemark.Operador = Enum_ProveedorWebServices.Sabre;
            //                cRemark.Remark = lRemark;
            //                clRemark.Add(cRemark);
            //                cCorporativo.Remark = clRemark;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    cRemark.Operador = Enum_ProveedorWebServices.Sabre;
            //    cRemark.Remark = lRemark;
            //    clRemark.Add(cRemark);
            //    cCorporativo.Remark = clRemark;
            //    clCorporativo.Add(cCorporativo);
            //    cCache.Corporativo = clCorporativo;
            //}
            csCache.ActualizarCache(cCache);
        }
        public static List<VO_Remarks> RecuperaRemark(clsCache cCache)
        {
            List<VO_Remarks> clRemark = new List<VO_Remarks>();
            try
            {
                //if (cCache.Corporativo != null)
                //{
                //    int iTotal = cCache.Corporativo.Count;
                //    for (int i = 0; i < iTotal; i++)
                //    {
                //        if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
                //        {
                //            if (cCache.Corporativo[i].Remark != null)
                //            {
                //                int iTotalRemark = cCache.Corporativo[i].Remark.Count;
                //                for (int j = 0; j < iTotalRemark; j++)
                //                {
                //                    if (cCache.Corporativo[i].Remark[j].Operador.Equals(Enum_ProveedorWebServices.Sabre))
                //                    {
                //                        clRemark = cCache.Corporativo[i].Remark[j].Remark;
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
            }
            catch 
            {
            }
            return clRemark;
        }
      
      
       
     
        private static clsParametros Remarks(string sRemark, string sComando, clsCache cCache)
        {
            ///pendiente por una revision mas detallada
            clsParametros cParametros = new clsParametros();
            try
            {
                cParametros.DatoAdic = sComando;
                string sTipoVariable = clsValidaciones.GetKeyOrAdd("VariablesRemarks", "VariablesRemarks");
                string sSV = clsValidaciones.GetKeyOrAdd("SV", "SV");
                string sMNE = clsValidaciones.GetKeyOrAdd("MNE", "MNE");
                string sSC = clsValidaciones.GetKeyOrAdd("SC", "SC");
                string sCC1 = clsValidaciones.GetKeyOrAdd("CC1", "CC1");
                string sCC2 = clsValidaciones.GetKeyOrAdd("CC2", "CC2");
                string sCC3 = clsValidaciones.GetKeyOrAdd("CC3", "CC3");
                string sCC4 = clsValidaciones.GetKeyOrAdd("CC4", "CC4");
                string sCC5 = clsValidaciones.GetKeyOrAdd("CC5", "CC5");
                string sCC = clsValidaciones.GetKeyOrAdd("CC", "CC");
                string sDP = clsValidaciones.GetKeyOrAdd("DP", "DP");
                string sAE = clsValidaciones.GetKeyOrAdd("AE", "AE");
                string sPR = clsValidaciones.GetKeyOrAdd("PR", "PR");
                string sCAE = clsValidaciones.GetKeyOrAdd("CAE", "CAE");
                string sPRC = clsValidaciones.GetKeyOrAdd("PRC", "PRC");
                string sPRA = clsValidaciones.GetKeyOrAdd("PRA", "PRA");
                string sTR = clsValidaciones.GetKeyOrAdd("TR", "TR");
                string sTM = clsValidaciones.GetKeyOrAdd("TM", "TM");
                string sTA = clsValidaciones.GetKeyOrAdd("TA", "TA");

                cParametros.Id = 1;
                if (sRemark.Equals(sSV))
                {
                    cParametros.DatoAdic = sComando;
                }
            
                if (sRemark.Equals(sSC))
                {
                    string sCodeSC = RecuperarSnapCode();
                    if (sCodeSC != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeSC;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sCC1))
                {
                    string sCodeC1 ="";
                    if (sCodeC1 != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeC1;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sCC2))
                {
                    string sCodeC2 = "";
                    if (sCodeC2 != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeC2;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sCC3))
                {
                    string sCodeC3 = "";
                    if (sCodeC3 != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeC3;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sCC4))
                {
                    string sCodeC4 = "";
                    if (sCodeC4 != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeC4;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sCC5))
                {
                    string sCodeC5 = "";
                    if (sCodeC5 != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeC5;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sCC))
                {
                    string sCodeC = "";
                    if (sCodeC != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeC;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sDP))
                {
                    cParametros.DatoAdic = sComando;
                    string sCodeDP = "";
                    if (sCodeDP != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeDP;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sAE))
                {
                    string sCodeAE = "";
                    if (sCodeAE != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeAE;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sCAE))
                {
                    string sCodeAE = "";
                    if (sCodeAE != null)
                    {
                        cParametros.DatoAdic = sComando + sCodeAE;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sPR))
                {
                    cParametros.DatoAdic = sComando + clsSesiones.getProyecto();
                }
                if (sRemark.Equals(sPRC))
                {
                    string sCodeAE = "";
                    if (sCodeAE != null)
                    {
                        cParametros.DatoAdic = sComando + clsSesiones.getProyecto() + "-" + sCodeAE;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
                if (sRemark.Equals(sPRA))
                {
                    string sCodeAE = "";
                    if (sCodeAE != null)
                    {
                        cParametros.DatoAdic = sComando + clsSesiones.getProyecto() + "-" + sCodeAE;
                    }
                    else
                    {
                        cParametros.Id = 0;
                    }
                }
               
               
               
            }
            catch 
            {
            }
            return cParametros;
        }
        private static clsParametros ModificarEstadoReserva(string Reserva, string sMotivoCancelacion)
        {
            clsParametros cParametros = new clsParametros();
          
            try
            {
                cParametros.Id = 1;
     
                string intestado = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("EstadoReservaCancelada", "HX"),"TBLESTADOS_RESERVA","INTCODE","STRCODE");

                if (intestado !="")
                    new csReservas().CambiarEstadoReserva(Reserva, int.Parse(intestado), sMotivoCancelacion);
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Error Guardando en la Base de Datos";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        private static clsParametros ModificarEstadoProyecto(string Proyecto)
        {
            clsParametros cParametros = new clsParametros();
           
            try
            {
                cParametros.Id = 1;               
                string intestado = new CsConsultasVuelos().ConsultaCodigo(clsValidaciones.GetKeyOrAdd("EstadoProyectoCancelada", "CL"),"TBLESTADOS_RESERVA","INTCODE","STRCODE");

                if (intestado != "")
                {
                    csReservas cReservas = new csReservas();
                    cReservas.CambiarEstadoProyecto(Proyecto, int.Parse(intestado));
                    cReservas.CambiarEstadoSolicitud(Proyecto, 0, 0);
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
                cParametros.Complemento = "Error Guardando en la Base de Datos";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
      
    }
}
