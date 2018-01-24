using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using Ssoft.Sql;
using Ssoft.Data;
using Ssoft.ManejadorExcepciones;
using Ssoft.Email;

namespace Ssoft.Utils
{
    public class clsEmail
    {
        protected string gstrConexion = string.Empty;

        public clsEmail()
        {
        }
        public string Conexion
        {
            set { this.gstrConexion = value; }
            get { return this.gstrConexion; }
        }
        public clsParametros EnviarMensaje(string pstrMensaje, string pstrSubject, OperacionEmail Operacion, string pstrTo, string pstrCC, string pstrCCO, FormatMail pmailFormat)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {

                Message objEmail = new Message();

                switch (Operacion)
                {
                    case OperacionEmail.Ambos:
                        break;

                    case OperacionEmail.Email:
                        objEmail.mTo = pstrTo;
                        objEmail.mCc = pstrCC;
                        objEmail.mCco = pstrCCO;
                        objEmail.mFrom = Message.ReadNode("MailFrom");
                        objEmail.mSubject = pstrSubject;
                        if (pmailFormat == FormatMail.HTML)
                        {
                            objEmail.MailFormat = true;
                            string sCodigoHTML = pstrMensaje;

                            objEmail.mBody = sCodigoHTML;
                        }
                        else
                        {
                            if (pmailFormat == FormatMail.PlantillaHTML)
                            {
                                objEmail.MailFormat = true;
                                StreamReader oPlantilla = new StreamReader(pstrMensaje);
                                string sCodigoHTML = oPlantilla.ReadToEnd();
                                oPlantilla.Close();
                                objEmail.mBody = sCodigoHTML;
                                //MailAttachment oAttch = new MailAttachment(pstrMensaje, MailEncoding.Base64);
                                //objEmail.Attachments.Add(oAttch);                        
                            }
                            else
                            {
                                objEmail.MailFormat = false;
                                objEmail.mBody = pstrMensaje;
                            }
                        }
                        cParametros = objEmail.Send();
                        break;

                    case OperacionEmail.InsertarBD:
                        break;
                }
            }
            catch (Exception Ex)
            {
                StringBuilder lstrMensaje = new StringBuilder();
                lstrMensaje.Append("Error Correo_ ");
                lstrMensaje.Append("Mensaje: " + pstrMensaje);
                lstrMensaje.Append("To: " + pstrTo);

                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = lstrMensaje.ToString();
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.ViewMessage.Add(lstrMensaje.ToString());
                cParametros.Sugerencia.Add("");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        public clsParametros EnviarMensaje(string pstrMensaje, string pstrSubject, OperacionEmail Operacion, string pstrTo, string pstrCC, string pstrCCO, FormatMail pmailFormat, string pstrFrom)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {

                Message objEmail = new Message();

                switch (Operacion)
                {
                    case OperacionEmail.Ambos:
                        break;

                    case OperacionEmail.Email:
                        objEmail.mTo = pstrTo;
                        objEmail.mCc = pstrCC;
                        objEmail.mCco = pstrCCO;
                        objEmail.mFrom = Message.ReadNode("MailFrom");
                        objEmail.mSubject = pstrSubject;
                        if (pmailFormat == FormatMail.HTML)
                        {
                            objEmail.MailFormat = true;
                            string sCodigoHTML = pstrMensaje;

                            objEmail.mBody = sCodigoHTML;
                        }
                        else
                        {
                            if (pmailFormat == FormatMail.PlantillaHTML)
                            {
                                objEmail.MailFormat = true;
                                StreamReader oPlantilla = new StreamReader(pstrMensaje);
                                string sCodigoHTML = oPlantilla.ReadToEnd();
                                oPlantilla.Close();
                                objEmail.mBody = sCodigoHTML;
                                //MailAttachment oAttch = new MailAttachment(pstrMensaje, MailEncoding.Base64);
                                //objEmail.Attachments.Add(oAttch);                        
                            }
                            else
                            {
                                objEmail.MailFormat = false;
                                objEmail.mBody = pstrMensaje;
                            }
                        }
                        cParametros = objEmail.Send(pstrFrom);
                        break;

                    case OperacionEmail.InsertarBD:
                        break;
                }
            }
            catch (Exception Ex)
            {
                StringBuilder lstrMensaje = new StringBuilder();
                lstrMensaje.Append("Error Correo_ ");
                lstrMensaje.Append("Mensaje: " + pstrMensaje);
                lstrMensaje.Append("To: " + pstrTo);
                lstrMensaje.Append("From: " + pstrFrom);

                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = lstrMensaje.ToString();
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.ViewMessage.Add(lstrMensaje.ToString());
                cParametros.Sugerencia.Add("");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        public clsParametros EnviarMensaje(string pstrMensaje, string pstrSubject, OperacionEmail Operacion, string pstrTo, string pstrCC, string pstrCCO, FormatMail pmailFormat, string pstrFrom, string strAdjunto)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {

                Message objEmail = new Message();

                switch (Operacion)
                {
                    case OperacionEmail.Ambos:
                        break;

                    case OperacionEmail.Email:
                        objEmail.mTo = pstrTo;
                        objEmail.mCc = pstrCC;
                        objEmail.mCco = pstrCCO;
                        objEmail.mFrom = Message.ReadNode("MailFrom");
                        objEmail.mSubject = pstrSubject;
                        if (pmailFormat == FormatMail.HTML)
                        {
                            objEmail.MailFormat = true;
                            string sCodigoHTML = pstrMensaje;

                            objEmail.mBody = sCodigoHTML;
                        }
                        else
                        {
                            if (pmailFormat == FormatMail.PlantillaHTML)
                            {
                                objEmail.MailFormat = true;
                                StreamReader oPlantilla = new StreamReader(pstrMensaje);
                                string sCodigoHTML = oPlantilla.ReadToEnd();
                                oPlantilla.Close();
                                objEmail.mBody = sCodigoHTML;
                            }
                            else
                            {
                                objEmail.MailFormat = false;
                                objEmail.mBody = pstrMensaje;
                            }
                        }
                        try
                        {
                            if (!strAdjunto.Length.Equals(0))
                            {
                                Attachment oAttch = new Attachment(strAdjunto);
                                List<Attachment> lstrAttachments = new List<Attachment>();
                                lstrAttachments.Add(oAttch);
                                objEmail.Attachments = lstrAttachments;
                            }
                        }
                        catch { }
                        cParametros = objEmail.Send(pstrFrom);
                        break;

                    case OperacionEmail.InsertarBD:
                        break;
                }
            }
            catch (Exception Ex)
            {
                StringBuilder lstrMensaje = new StringBuilder();
                lstrMensaje.Append("Error Correo_ ");
                lstrMensaje.Append("Mensaje: " + pstrMensaje);
                lstrMensaje.Append("Subject: " + pstrSubject);
                lstrMensaje.Append("To: " + pstrTo);
                lstrMensaje.Append("From: " + pstrFrom);
                lstrMensaje.Append("Adjunto: " + strAdjunto);

                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = lstrMensaje.ToString();
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.ViewMessage.Add(lstrMensaje.ToString());
                cParametros.Sugerencia.Add("");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        public clsParametros EnviarMensajeRecordar(string pstrMensaje, string pstrSubject, string pstrTo, string pstrCC, string pstrCCO,
            string strUsuario, string strPassword, string strInfo, string strEmpresa, string strTel, string strUrl)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {
                Message objEmail = new Message();

                objEmail.mTo = pstrTo;
                objEmail.mCc = pstrCC;
                objEmail.mCco = pstrCCO;
                objEmail.mFrom = Message.ReadNode("MailFrom");
                objEmail.mSubject = pstrSubject;
                objEmail.MailFormat = true;
                StreamReader oPlantilla = new StreamReader(pstrMensaje);
                string sCodigoHTML = oPlantilla.ReadToEnd();
                oPlantilla.Close();
                // para reemplazar campos en el HTML

                sCodigoHTML = sCodigoHTML.Replace("@_USUARIO", strUsuario);
                sCodigoHTML = sCodigoHTML.Replace("@_PASS", strPassword);
                sCodigoHTML = sCodigoHTML.Replace("@_URL", strUrl);
                sCodigoHTML = sCodigoHTML.Replace("@_INFO", strInfo);
                sCodigoHTML = sCodigoHTML.Replace("@_EMPRESA", strEmpresa);
                sCodigoHTML = sCodigoHTML.Replace("@_TEL", strTel);

                objEmail.mBody = sCodigoHTML;

                cParametros = objEmail.Send();
            }
            catch (Exception Ex)
            {
                StringBuilder lstrMensaje = new StringBuilder();
                lstrMensaje.Append("Error Correo_ ");
                lstrMensaje.Append("Mensaje: " + pstrMensaje);
                lstrMensaje.Append("Subject: " + pstrSubject);
                lstrMensaje.Append("To: " + pstrTo);

                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = lstrMensaje.ToString();
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.ViewMessage.Add(lstrMensaje.ToString());
                cParametros.Sugerencia.Add("");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        public clsParametros EnviarMensajeRecordar(string pstrMensaje, string pstrSubject, string pstrTo, string pstrCC, string pstrCCO,
            string strUsuario, string strPassword, string strInfo, string strEmpresa, string strTel, string strUrl, string pstrFrom)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {
                Message objEmail = new Message();

                objEmail.mTo = pstrTo;
                objEmail.mCc = pstrCC;
                objEmail.mCco = pstrCCO;
                objEmail.mFrom = Message.ReadNode("MailFrom");
                objEmail.mSubject = pstrSubject;
                objEmail.MailFormat = true;
                StreamReader oPlantilla = new StreamReader(pstrMensaje);
                string sCodigoHTML = oPlantilla.ReadToEnd();
                oPlantilla.Close();
                // para reemplazar campos en el HTML

                sCodigoHTML = sCodigoHTML.Replace("@_USUARIO", strUsuario);
                sCodigoHTML = sCodigoHTML.Replace("@_PASS", strPassword);
                sCodigoHTML = sCodigoHTML.Replace("@_URL", strUrl);
                sCodigoHTML = sCodigoHTML.Replace("@_INFO", strInfo);
                sCodigoHTML = sCodigoHTML.Replace("@_EMPRESA", strEmpresa);
                sCodigoHTML = sCodigoHTML.Replace("@_TEL", strTel);

                objEmail.mBody = sCodigoHTML;

                cParametros = objEmail.Send(pstrFrom);
            }
            catch (Exception Ex)
            {
                StringBuilder lstrMensaje = new StringBuilder();
                lstrMensaje.Append("Error Correo_ ");
                lstrMensaje.Append("Mensaje: " + pstrMensaje);
                lstrMensaje.Append("Subject: " + pstrSubject);
                lstrMensaje.Append("To: " + pstrTo);
                lstrMensaje.Append("From: " + pstrFrom);

                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = lstrMensaje.ToString();
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.ViewMessage.Add(lstrMensaje.ToString());
                cParametros.Sugerencia.Add("");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        public clsParametros EnviarMensaje(string pstrSubject, OperacionEmail Operacion, string pstrTo, string pstrCC, string pstrCCO, FormatMail pmailFormat, string pstrFrom, string sHtml)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {

                Message objEmail = new Message();

                switch (Operacion)
                {
                    case OperacionEmail.Ambos:
                        break;

                    case OperacionEmail.Email:
                        objEmail.mTo = pstrTo;
                        objEmail.mCc = pstrCC;
                        objEmail.mCco = pstrCCO;
                        objEmail.mFrom = Message.ReadNode("MailFrom");
                        objEmail.mSubject = pstrSubject;
                        if (pmailFormat == FormatMail.HTML)
                        {
                            objEmail.MailFormat = true;
                            objEmail.mBody = sHtml;
                        }
                        else
                        {
                            if (pmailFormat == FormatMail.PlantillaHTML)
                            {
                                objEmail.MailFormat = true;
                                objEmail.mBody = sHtml;

                            }
                            else
                            {
                                objEmail.MailFormat = false;
                                objEmail.mBody = sHtml;
                            }
                        }
                        cParametros = objEmail.Send(pstrFrom);
                        break;

                    case OperacionEmail.InsertarBD:
                        break;
                }
            }
            catch (Exception Ex)
            {
                StringBuilder lstrMensaje = new StringBuilder();
                lstrMensaje.Append("Error Correo_ ");
                lstrMensaje.Append("To: " + pstrTo);
                lstrMensaje.Append("From: " + pstrFrom);

                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = lstrMensaje.ToString();
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.ViewMessage.Add(lstrMensaje.ToString());
                cParametros.Sugerencia.Add("");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }

        public clsParametros EnviarMensajeRecordar(string pstrSubject, string pstrTo, string pstrCC, string pstrCCO, string strHtml, string pstrFrom)
        {
            clsParametros cParametros = new clsParametros();
            cParametros.Id = 1;
            try
            {
                Message objEmail = new Message();

                objEmail.mTo = pstrTo;
                objEmail.mCc = pstrCC;
                objEmail.mCco = pstrCCO;
                objEmail.mFrom = Message.ReadNode("MailFrom");
                objEmail.mSubject = pstrSubject;
                objEmail.MailFormat = true;
                

                objEmail.mBody = strHtml;

                cParametros = objEmail.Send(pstrFrom);
            }
            catch (Exception Ex)
            {
                StringBuilder lstrMensaje = new StringBuilder();
                lstrMensaje.Append("Error Correo_ ");
                lstrMensaje.Append("Mensaje: " + "Recuperar Contraseña");
                lstrMensaje.Append("Subject: " + pstrSubject);
                lstrMensaje.Append("To: " + pstrTo);
                lstrMensaje.Append("From: " + pstrFrom);

                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = lstrMensaje.ToString();
                cParametros.Metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
                cParametros.ViewMessage.Add(lstrMensaje.ToString());
                cParametros.Sugerencia.Add("");
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
    }
}
