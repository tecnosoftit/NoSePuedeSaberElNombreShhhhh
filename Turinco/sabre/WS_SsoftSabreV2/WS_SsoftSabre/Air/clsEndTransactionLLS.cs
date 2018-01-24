using System;
using System.Collections.Generic;
using System.Text;
using EndTransactionRQ = WS_SsoftSabre.EndTransactionRQ;
using System.Configuration;
using Ssoft.ManejadorExcepciones;
using Ssoft.ValueObjects;
using Ssoft.Rules.WebServices;
using Ssoft.Utils;
using WS_SsoftSabre.Utilidades;

namespace WS_SsoftSabre.Air
{
    public class clsEndTransactionLLS
    {
        //private int iPos;
        string Session_ = string.Empty;
        Ssoft.ValueObjects.VO_Credentials objvo_Credentials;

        public clsEndTransactionLLS()
        {
            Session_ = AutenticacionSabre.GET_SabreSession();
        }

        public EndTransactionRQ.EndTransactionRS _Sabre_GuardarReserva()
        {
            objvo_Credentials = Ssoft.Utils.clsSesiones.getCredentials();
            EndTransactionRQ.EndTransactionRS EndTransactionResultado_ = new EndTransactionRQ.EndTransactionRS();
            clsParametros cParametros = new clsParametros();
            StringBuilder consulta = new StringBuilder();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;

            try
            {
                EndTransactionRQ.MessageHeader Mensaje_ = clsSabreBase.__ISabre_EndTransactionLLSUpdated();

                if (Mensaje_ != null)
                {
                    EndTransactionRQ.Security Seguridad_ = new EndTransactionRQ.Security();
                    Seguridad_.BinarySecurityToken = Session_;

                    EndTransactionRQ.EndTransactionRQ EndTransaction_ = new EndTransactionRQ.EndTransactionRQ();
                    EndTransactionRQ.EndTransactionRQPOS EndTransactionPos_ = new EndTransactionRQ.EndTransactionRQPOS();
                    EndTransactionRQ.EndTransactionRQPOSSource EndTransactionSource_ = new EndTransactionRQ.EndTransactionRQPOSSource();

                    EndTransactionSource_.PseudoCityCode = objvo_Credentials.Pcc;//ConfigurationManager.AppSettings["Sabre_Ipcc"];
                    EndTransactionPos_.Source = EndTransactionSource_;
                    EndTransaction_.POS = EndTransactionPos_;

                    EndTransactionRQ.EndTransactionRQEndTransaction EndTransactionEnd_ = new EndTransactionRQ.EndTransactionRQEndTransaction();
                    EndTransactionRQ.EndTransactionRQUpdatedBy EndTransaction_Update_ = new EndTransactionRQ.EndTransactionRQUpdatedBy();
                    EndTransactionRQ.EndTransactionRQUpdatedByTPA_Extensions EndTransaction_UpdateTPA_ = new EndTransactionRQ.EndTransactionRQUpdatedByTPA_Extensions();
                    EndTransactionRQ.EndTransactionRQUpdatedByTPA_ExtensionsAccess EndTransaction_UpdateTPAAccess_ = new EndTransactionRQ.EndTransactionRQUpdatedByTPA_ExtensionsAccess();
                    EndTransactionRQ.EndTransactionRQUpdatedByTPA_ExtensionsAccessAccessPerson EndTransaction_UpdateTPAAccessPerson_ = new EndTransactionRQ.EndTransactionRQUpdatedByTPA_ExtensionsAccessAccessPerson();

                    EndTransaction_UpdateTPAAccessPerson_.GivenName = "WEB";
                    EndTransaction_UpdateTPAAccess_.AccessPerson = EndTransaction_UpdateTPAAccessPerson_;
                    EndTransaction_UpdateTPA_.Access = EndTransaction_UpdateTPAAccess_;
                    EndTransaction_Update_.TPA_Extensions = EndTransaction_UpdateTPA_;
                    EndTransaction_.UpdatedBy = EndTransaction_Update_;
                    EndTransactionEnd_.Ind = true;
                    EndTransactionEnd_.IndSpecified = true;
                    /*PARA CONFIGURAR SI SE ENVIA CORREO DE NOTIFICACION*/
                    string sCorreo;
                    try { sCorreo = ConfigurationManager.AppSettings["Sabre_VirtuallyThere"].ToString(); }
                    catch { sCorreo = "True"; }
                    if (sCorreo.Equals("True"))
                    {
                        EndTransactionRQ.EndTransactionRQEndTransactionSendEmail oSendEmail = new EndTransactionRQ.EndTransactionRQEndTransactionSendEmail();
                        oSendEmail.Ind = true;
                        oSendEmail.IndSpecified = true;
                        EndTransactionEnd_.SendEmail = oSendEmail;
                    }
                    EndTransaction_.EndTransaction = EndTransactionEnd_;
                    EndTransaction_.Version = clsSabreBase.SABRE_VERSION_ENDTRANSACTION;
                    EndTransactionRQ.EndTransactionService EndTransactionServicio_ = new EndTransactionRQ.EndTransactionService();
                    EndTransactionServicio_.MessageHeaderValue = Mensaje_;
                    EndTransactionServicio_.SecurityValue = Seguridad_;

                    try
                    {
                        string sConvenio = csVuelos.csConvenio();
                        if (!sConvenio.Length.Equals(0))
                        {
                            string Comando_ = "WPAC*" + sConvenio + "¥RQ";
                            Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno(Comando_);
                        }
                    }
                    catch { }
                    EndTransactionServicio_.Url = objvo_Credentials.UrlWebServices;

                    EndTransactionResultado_ = EndTransactionServicio_.EndTransactionRQ(EndTransaction_);
                    //string sComando = "PQ";
                    //string sVenta = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
                    if (EndTransactionResultado_.Errors != null)
                    {
                        cParametros.Id = 0;
                        cParametros.TipoLog = Enum_Error.Log;
                        cParametros.Code = EndTransactionResultado_.Errors.Error.ErrorCode;
                        cParametros.Message = EndTransactionResultado_.Errors.Error.ErrorMessage;
                        cParametros.Severity = EndTransactionResultado_.Errors.Error.Severity;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Metodo = "_Sabre_GuardarReserva";
                        cParametros.Complemento = "HostCommand: " + EndTransactionResultado_.TPA_Extensions.HostCommand;
                        cParametros.ViewMessage.Add("La reserva no pudo ser confirmada");
                        cParametros.Sugerencia.Add("Por favor intente de nuevo");
                        consulta.AppendLine("Credenciales: ");
                        if (objvo_Credentials != null)
                        {
                            consulta.AppendLine("User: " + objvo_Credentials.User);
                            consulta.AppendLine("Password: " + objvo_Credentials.Password);
                            consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                            consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                            consulta.AppendLine("QNumber: " + objvo_Credentials.QNumber);
                            consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                            consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                        }
                        cParametros.TargetSite = consulta.ToString();
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        cParametros.Id = 1;
                        cParametros.TipoLog = Enum_Error.Transac;
                        cParametros.Tipo = clsTipoError.WebServices;
                        cParametros.Metodo = "_Sabre_GuardarReserva";
                        cParametros.Complemento = "HostCommand: " + EndTransactionResultado_.TPA_Extensions.HostCommand;
                        try
                        {
                            if (EndTransactionResultado_.UniqueID != null)
                            {
                                cParametros.Message = EndTransactionResultado_.UniqueID.ID;
                            }
                        }
                        catch { }
                        consulta.AppendLine("Credenciales: ");
                        try
                        {
                            if (objvo_Credentials != null)
                            {
                                consulta.AppendLine("User: " + objvo_Credentials.User);
                                consulta.AppendLine("Password: " + objvo_Credentials.Password);
                                consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                                consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                                consulta.AppendLine("QNumber: " + objvo_Credentials.QNumber);
                                consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                                consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                            }
                        }
                        catch { }
                        cParametros.TargetSite = consulta.ToString();
                        ExceptionHandled.Publicar(cParametros);
                    }
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message;
                cParametros.StackTrace = Ex.StackTrace;
                cParametros.Source = Ex.Source;
                cParametros.TargetSite = Ex.TargetSite.ToString();
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Metodo = "_Sabre_GuardarReserva";
                cParametros.Tipo = clsTipoError.WebServices;
                consulta.AppendLine("Credenciales: ");
                if (objvo_Credentials != null)
                {
                    consulta.AppendLine("User: " + objvo_Credentials.User);
                    consulta.AppendLine("Password: " + objvo_Credentials.Password);
                    consulta.AppendLine("Ipcc: " + objvo_Credentials.Ipcc);
                    consulta.AppendLine("Pcc: " + objvo_Credentials.Pcc);
                    consulta.AppendLine("Dominio: " + objvo_Credentials.Dominio);
                    consulta.AppendLine("Url Sabre: " + objvo_Credentials.UrlWebServices);
                }
                cParametros.TargetSite = consulta.ToString();
                ExceptionHandled.Publicar(cParametros);
            }
            return EndTransactionResultado_;
        }
        public clsParametros _GuardarReserva()
        {
            clsParametros cParametros = new clsParametros();
            cParametros.TipoWs = Enum_ProveedorWebServices.Sabre;
            cParametros.Id = 1;
            try
            {
                EndTransactionRQ.EndTransactionRS EndTransactionResultado_ = _Sabre_GuardarReserva();

                if (EndTransactionResultado_ != null)
                {
                    if (EndTransactionResultado_.UniqueID != null)
                    {
                        cParametros.DatoAdic = EndTransactionResultado_.UniqueID.ID;

                        cParametros.Id = 1;
                        cParametros.Message = "Reserva Exitosa";
                        cParametros.Tipo = clsTipoError.Aplication;
                        cParametros.Severity = clsSeveridad.Moderada;
                        cParametros.Metodo = "Guardar reserva";
                        cParametros.Complemento = "Record: " + cParametros.DatoAdic;
                        ExceptionHandled.Publicar(cParametros);
                    }
                    else
                    {
                        EndTransactionRQ.EndTransactionRSErrorsError Error_ = EndTransactionResultado_.Errors.Error;
                        EndTransactionRQ.EndTransactionRSErrorsErrorErrorInfo ErrorInfo_ = Error_.ErrorInfo;

                        if (EndTransactionResultado_.Errors != null)
                        {
                            cParametros.Id = 0;
                            cParametros.Code = EndTransactionResultado_.Errors.Error.ErrorCode;
                            cParametros.Info = EndTransactionResultado_.Errors.Error.ErrorInfo.Message;
                            cParametros.DatoAdic = EndTransactionResultado_.Errors.Error.ErrorInfo.Message;
                            cParametros.Message = EndTransactionResultado_.Errors.Error.ErrorMessage;
                            cParametros.Severity = EndTransactionResultado_.Errors.Error.Severity;
                            cParametros.Tipo = clsTipoError.WebServices;
                            cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                            cParametros.Complemento = "";
                            cParametros.ViewMessage.Add("");
                            cParametros.Sugerencia.Add("");
                            cParametros.Message = EndTransactionResultado_.Errors.Error.ErrorMessage;
                            ExceptionHandled.Publicar(cParametros);
                        }
                    }
                }
                else
                {
                    cParametros.Id = 0;
                    cParametros.Message = "Unable to connect to the remote server";
                    cParametros.DatoAdic = "Unable to connect to the remote server";
                    cParametros.Complemento = "Unable to connect";
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.DatoAdic = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.Metodo = Ex.TargetSite.Name;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "";
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }

        public Boolean Cancelar_Reserva(String str_Record)
        {
            return true;
        }
        /// <summary>
        /// Metodo para abrir la reserva a traves de comando
        /// </summary>
        /// <param name="str_Record">record</param>
        /// <returns>Respuesta del Command para verificacion</returns>
        /// <remarks>
        /// Autor:  Faustino Posas
        /// Fecha:  2011-10-07
        /// -- Control de cambios --
        /// Descripcion:        
        /// Fecha:          
        /// Responsable:    
        /// </remarks>
        public string Abrir_ReservaCommand(String str_Record)
        {
            string sReponse = "Error";
            try
            {
                string sComando = "*" + str_Record;
                sReponse = Negocios_WebServiceSabreCommand._EjecutarComando(sComando);
            }
            catch { }
            return sReponse;
        }
        public Boolean _Cancelar_Reserva(String str_Record)
        {
            return true;
        }
        public clsParametros _CerrarReserva(ref String Record_)
        {
            #region [ CERRAR RESERVA ]
            string Return_ = "XXXXXX";
            string ReturnTotal_ = "YYYYYY";
            string NotComplete_ = "ZZZZZZ";
            clsParametros cParametros = new clsParametros();
            objvo_Credentials = clsSesiones.getCredentials();
            bool bReservaNormal = true;
            cParametros.DatoAdic = Record_;
            List<VO_SabreErrors> SabreErrors_ = WS_SsoftSabre.Utilidades.clsValidacionesVuelos._SabreErrors();
            int iCount = 0;
            while (!Ssoft.Utils.clsValidaciones.IS_ALPHABETIC(cParametros.DatoAdic))
            {
                cParametros = _GuardarReserva();

                if (cParametros.Message.Contains("Unable to connect"))
                {
                    if (iCount < 3)
                    {
                        cParametros.DatoAdic = Record_;
                        iCount++;
                    }
                    else
                    {
                        cParametros.DatoAdic = NotComplete_;
                    }
                }
                else
                {
                    if (cParametros.DatoAdic == null) cParametros.DatoAdic = String.Empty;

                    if (cParametros.DatoAdic.Length != 6 || !Ssoft.Utils.clsValidaciones.IS_ALPHABETIC(cParametros.DatoAdic))
                    {
                        for (int i = 0; i < SabreErrors_.Count; i++)
                        {
                            if (cParametros.DatoAdic.Trim().CompareTo(SabreErrors_[i].Error_) == 0)
                            {
                                #region [ FILTRAR ]

                                if (SabreErrors_[i].Solucion_[0].CompareTo("RETURN") == 0) cParametros.DatoAdic = Return_;
                                else if (SabreErrors_[i].Solucion_[0].CompareTo("RETURNCOMPLETE") == 0) cParametros.DatoAdic = ReturnTotal_;
                                else
                                {
                                    foreach (string Solucion_ in SabreErrors_[i].Solucion_)
                                    {
                                        if (SabreErrors_[i].Error_.Contains("NEED ADDRESS - USE W-"))
                                        {
                                            Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno(Solucion_ + objvo_Credentials.Agencia_Nombre);
                                        }
                                        else
                                        {
                                            if (SabreErrors_[i].Error_.Contains("NEED PHONE FIELD - USE 9"))
                                            {
                                                Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno(Solucion_ + objvo_Credentials.Agencia_Telefono);
                                            }
                                            else
                                            {
                                                if (SabreErrors_[i].Error_.Contains("INFANT DETAILS REQUIRED IN SSR - ENTER 3INFT/..."))
                                                {
                                                    Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno(Solucion_);
                                                    bReservaNormal = false;
                                                }
                                                else
                                                {
                                                    Negocios_WebServiceSabreCommand._EjecutarComandoSinRetorno(Solucion_);
                                                }
                                            }
                                        }
                                        System.Threading.Thread.Sleep(50);
                                    }
                                    if (bReservaNormal)
                                    {
                                        cParametros = _GuardarReserva();
                                    }
                                    else
                                    {
                                        string sComando = "*P6";
                                        cParametros = Negocios_WebServiceSabreCommand._EjecutarComandoGen(sComando);
                                        cParametros.Complemento = "Command de reserva: " + cParametros.Message;
                                        ExceptionHandled.Publicar(cParametros);
                                        cParametros.DatoAdic = clsValidacionesVuelos.setResultComado(cParametros.Message.ToString(), 1, 27, 6);
                                    }
                                    if (cParametros.DatoAdic == null) cParametros.DatoAdic = String.Empty;

                                    if (cParametros.DatoAdic.Length != 6 || !Ssoft.Utils.clsValidaciones.IS_ALPHABETIC(cParametros.DatoAdic))
                                    {
                                        if (iCount < 3)
                                        {
                                            iCount++;
                                            i = -1;
                                        }
                                        else
                                        {
                                            cParametros.DatoAdic = Return_;
                                            i = SabreErrors_.Count;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                #endregion
                            }
                            if (i.CompareTo(SabreErrors_.Count - 1) == 0) cParametros.DatoAdic = NotComplete_;
                        }
                    }
                }
            }
            #endregion
            Record_ = cParametros.DatoAdic;
            if ((cParametros.DatoAdic == Return_) || (cParametros.DatoAdic == ReturnTotal_) || (cParametros.DatoAdic == NotComplete_))
            {
                cParametros.Data = "La reserva no se pudo confirmar";
                Record_ = cParametros.Data;
            }
            return cParametros;
        }

    }
}
