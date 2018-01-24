/*
===========================================================================================
Descripción: Componente corporativo que provee la funcionalidad para el
acceso a datos sobre repositorios relacionales como son MSSQLServer.
Autor: José Faustino Posas
URL: http://www.ssoftcolombia.com
Email: fposas@ssoftcolombia.com
Fecha: 2011-08-15
Version:    5.0.0.0
===========================================================================================
*/
using System;
using System.Collections.Generic;

using System.Text;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Xml;
using Ssoft.ValueObjects;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;

namespace Ssoft.Email
{
    public class Message
    {
        private string _strFrom;
        private string _strTo;
        private string _strCc;
        private string _strCco;
        private string _strbody;
        private string _strSubject;
        private string _strServerName;
        private bool _strMailFormat;
        private List<Attachment> _strAttachments;

        private string _strPort;
        private string _strAdjuntos;
        protected ArrayList arlAttachments;

        private string _strSendPort;
        private string _strPassword;

        /// <summary>
        /// Persona o remitente del mensaje.
        /// </summary>
        public string mFrom
        {
            set { this._strFrom = value; }
            get { return this._strFrom; }
        }

        /// <summary>
        /// Persona o destinatario a quien se le enviará la notificación.
        /// </summary>
        public string mTo
        {
            set { this._strTo = value; }
            get { return this._strTo; }
        }
        /// <summary>
        /// Persona o destinatario a quien se le enviará copia de la notificación.
        /// </summary>
        public string mCc
        {
            set { this._strCc = value; }
            get { return this._strCc; }
        }
        /// <summary>
        /// Persona o destinatario a quien se le enviará copia oculta la notificación.
        /// </summary>
        public string mCco
        {
            set { this._strCco = value; }
            get { return this._strCco; }
        }

        /// <summary>
        /// Cuerpo o contenido del mensaje o notificación.
        /// </summary>
        public string mBody
        {
            set { this._strbody = value; }
            get { return this._strbody; }
        }

        /// <summary>
        /// Asunto del mensaje.
        /// </summary>
        public string mSubject
        {
            set { this._strSubject = value; }
            get { return this._strSubject; }
        }
        public string mSendPort
        {
            set { this._strSendPort = value; }
            get { return this._strSendPort; }
        }
        public string mServerName
        {
            set { this._strServerName = value; }
            get { return this._strServerName; }
        }
        public string mPort
        {
            set { this._strPort = value; }
            get { return this._strPort; }
        }
        public string mPassword
        {
            set { this._strPassword = value; }
            get { return this._strPassword; }
        }

        private string mAdjuntos
        {
            set { this._strAdjuntos = value; }
            get { return this._strAdjuntos; }
        }

        ///<summary>
        ///Constructor de la clase Message. Inicialmente construye los 
        ///valores iniciales de la configuración del envío del mensaje.
        ///</summary>
        public bool MailFormat
        {
            get { return _strMailFormat; }
            set { _strMailFormat = value; }
        }
        public List<Attachment> Attachments
        {
            get { return _strAttachments; }
            set { _strAttachments = value; }
        }
        public Message()
        {
            this.ReadConfig();
        }
        public string[] CorreosLista(string sCorreos)
        {
            string[] slCorreos = clsValidaciones.Lista(sCorreos, " ");
            try
            {
                if (sCorreos.Contains(";"))
                {
                    slCorreos = clsValidaciones.Lista(sCorreos, ";");
                }
                else
                {
                    if (sCorreos.Contains(","))
                    {
                        slCorreos = clsValidaciones.Lista(sCorreos, ",");
                    }
                }
            }
            catch { }
            return slCorreos;
        }
        /// <summary>
        /// Realizar la notifiación vía Email
        /// </summary>
        public clsParametros Send()
        {
            //Realizar la notificación vía Email.
            clsParametros cParametros = new clsParametros();
            MailMessage lobjemail = new MailMessage();
            string _strSendPort = Message.ReadNode("SendPort");
            string _strPassword = Message.ReadNode("Password");
            try
            {
                MailAddress maFrom = new MailAddress(mFrom);
                //validar las entradas de las propiedades.
                this.Validate();
                //MailAttachment attachment = null; 
                lobjemail.From = maFrom;
                lobjemail.Subject = mSubject;
                lobjemail.Body = mBody;

                // Destinatarios de correo
                string[] slTo = CorreosLista(mTo);
                string[] slCc = CorreosLista(mCc);
                string[] slBcc = CorreosLista(mCco);

                for (int i = 0; i < slTo.Length; i++)
                {
                    if (slTo[i].Length > 0)
                        lobjemail.To.Add(slTo[i]);
                }

                for (int i = 0; i < slCc.Length; i++)
                {
                    if (slCc[i].Length > 0)
                        lobjemail.CC.Add(slCc[i]);
                }

                for (int i = 0; i < slBcc.Length; i++)
                {
                    if (slBcc[i].Length > 0)
                        lobjemail.Bcc.Add(slBcc[i]);
                }

                SmtpClient _smtpCliente = new SmtpClient();
                NetworkCredential _NetworkCredential = new NetworkCredential();

                _NetworkCredential.Password = _strPassword;
                _NetworkCredential.UserName = mFrom;

                _smtpCliente.Host = _strServerName;
                _smtpCliente.Port = int.Parse(_strPort); ;
                _smtpCliente.Credentials = _NetworkCredential;

                lobjemail.IsBodyHtml = MailFormat;

                if (Attachments != null && Attachments.Count > 0)
                {
                    for (int i = 0; i < Attachments.Count; i++)
                    {
                        lobjemail.Attachments.Add(Attachments[i]);
                    }
                }
                if (lobjemail.From.Address.Length.Equals(0))
                {
                    MailAddress maFromTemp = new MailAddress(clsValidaciones.GetKeyOrAdd("strEmailEnvio", "info@ssoftcolombia.com"));
                    lobjemail.From = maFromTemp;
                }
                _smtpCliente.Send(lobjemail);
                cParametros.Id = 1;
                cParametros.ViewMessage.Add("Correo enviado satisfactoriamente");
                cParametros.Sugerencia.Add("");

                // Seguimiento de correo
                if (clsValidaciones.GetKeyOrAdd("LogCorreo", "False").ToUpper().Equals("TRUE"))
                {
                    cParametros.Message = "Log de Correo";
                    cParametros.Complemento = "Envio de correo, objeto enviado lobjemail... - Datos: To_: " + lobjemail.To + " CC_: " + lobjemail.CC + " From_: " + lobjemail.From + " Subject_: " + lobjemail.Subject;
                    cParametros.Data = "Coneccion de Email configurada: ServerName_: " + _strServerName + " Port_: " + _strPort + " PortSend_: " + _strSendPort + " UserName_: " + mFrom + " Password_: " + _strPassword + " Body_: " + _strbody;
                    ExceptionHandled.Publicar(cParametros);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.ViewMessage.Add("Error al enviar el correo " + Ex.Message.ToString());
                cParametros.Sugerencia.Add("");
                cParametros.Complemento = "Envio de correo, objeto enviado lobjemail... - Datos: To_: " + lobjemail.To + " CC_: " + lobjemail.CC + " From_: " + lobjemail.From + " Subject_: " + lobjemail.Subject;
                cParametros.Data = "Coneccion de Email configurada: ServerName_: " + _strServerName + " Port_: " + _strPort + " PortSend_: " + _strSendPort + " UserName_: " + mFrom + " Password_: " + _strPassword + " Body_: " + _strbody;
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        /// <summary>
        /// Realiza la consulta de los valores fíjos de los
        /// parámetros que se requieren para el envío del mensaje.
        /// </summary>
        /// <summary>
        /// Realizar la notifiación vía Email
        /// </summary>
        public clsParametros Send(string strFrom)
        {
            //Realizar la notificación vía Email.
            clsParametros cParametros = new clsParametros();
            MailMessage lobjemail = new MailMessage();
            string _strSendPort = Message.ReadNode("SendPort");
            string _strPassword = Message.ReadNode("Password");
            try
            {
                MailAddress maFrom = new MailAddress(strFrom);
                //validar las entradas de las propiedades.
                this.Validate();
                //MailAttachment attachment = null; 
                lobjemail.From = maFrom;
                lobjemail.Subject = mSubject;
                lobjemail.Body = mBody;

                // Destinatarios de correo
                string[] slTo = CorreosLista(mTo);
                string[] slCc = CorreosLista(mCc);
                string[] slBcc = CorreosLista(mCco);

                for (int i = 0; i < slTo.Length; i++)
                {
                    if (slTo[i].Length > 0)
                        lobjemail.To.Add(slTo[i]);
                }

                for (int i = 0; i < slCc.Length; i++)
                {
                    if (slCc[i].Length > 0)
                        lobjemail.CC.Add(slCc[i]);
                }

                for (int i = 0; i < slBcc.Length; i++)
                {
                    if (slBcc[i].Length > 0)
                        lobjemail.Bcc.Add(slBcc[i]);
                }

                SmtpClient _smtpCliente = new SmtpClient();
                NetworkCredential _NetworkCredential = new NetworkCredential();

                _NetworkCredential.Password = _strPassword;
                _NetworkCredential.UserName = mFrom;

                _smtpCliente.Host = _strServerName;
                _smtpCliente.Port = int.Parse(_strPort); ;
                _smtpCliente.Credentials = _NetworkCredential;

                lobjemail.IsBodyHtml = MailFormat;

                if (Attachments != null && Attachments.Count > 0)
                {
                    for (int i = 0; i < Attachments.Count; i++)
                    {
                        lobjemail.Attachments.Add(Attachments[i]);
                    }
                }
                if (lobjemail.From.Address.Length.Equals(0))
                {
                    MailAddress maFromTemp = new MailAddress(clsValidaciones.GetKeyOrAdd("strEmailEnvio", "info@ssoftcolombia.com"));
                    lobjemail.From = maFromTemp;
                }
                _smtpCliente.Send(lobjemail);

                cParametros.Id = 1;
                cParametros.ViewMessage.Add("Correo enviado satisfactoriamente");
                cParametros.Sugerencia.Add("");
                // Seguimiento de correo
                if (clsValidaciones.GetKeyOrAdd("LogCorreo", "False").ToUpper().Equals("TRUE"))
                {
                    cParametros.Message = "Log de Correo";
                    cParametros.Complemento = "Envio de correo, objeto enviado lobjemail... - Datos: To_: " + lobjemail.To + " CC_: " + lobjemail.CC + " From_: " + lobjemail.From + " Subject_: " + lobjemail.Subject;
                    cParametros.Data = "Coneccion de Email configurada: ServerName_: " + _strServerName + " Port_: " + _strPort + " PortSend_: " + _strSendPort + " UserName_: " + mFrom + " Password_: " + _strPassword + " Body_: " + _strbody;
                    ExceptionHandled.Publicar(cParametros);
                }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Alta;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.ViewMessage.Add("Error al enviar el correo " + Ex.Message.ToString());
                cParametros.Sugerencia.Add("");
                cParametros.Complemento = "Envio de correo, objeto enviado lobjemail... - Datos: To_: " + lobjemail.To + " CC_: " + lobjemail.CC + " From_: " + lobjemail.From + " Subject_: " + lobjemail.Subject;
                cParametros.Data = "Coneccion de Email configurada: ServerName_: " + _strServerName + " Port_: " + _strPort + " PortSend_: " + _strSendPort + " UserName_: " + mFrom + " Password_: " + _strPassword + " Body_: " + _strbody;
                ExceptionHandled.Publicar(cParametros);
            }
            return cParametros;
        }
        /// <summary>
        /// Realiza la consulta de los valores fíjos de los
        /// parámetros que se requieren para el envío del mensaje.
        /// </summary>
        private void ReadConfig()
        {
            _strServerName = this.ReadElement("ServerName");
            _strPort = this.ReadElement("Port");
            _strSendPort = this.ReadElement("SendPort");
            _strPassword = this.ReadElement("Password");
            _strFrom = this.ReadElement("MailFrom");
            _strSubject = this.ReadElement("MailSubject");
        }
        public void ReadParameters()
        {
            ReadConfig();
        }

        /// <summary>
        /// Lee un Nodo o elemento del archivo de configuración EmailConfig.xml
        /// </summary>
        /// <param name="pvstrElemento">Nombre del Tag o elemento.</param>
        /// <returns>Valor del Tag leído.</returns>
        public static string ReadNode(string pvstrElemento)
        {
            string _strReturn = string.Empty;
            Message m = new Message();

            try { _strReturn = m.ReadElement(pvstrElemento); }
            catch { }

            return _strReturn;
        }

        /// <summary>
        /// Realiza la lectura de un tag del documento EmailConfig.xml
        /// </summary>
        /// <param name="pvstrElemento">Nombre del tag.</param>
        /// <returns>Valor del tag leído.</returns>
        private string ReadElement(string pvstrElemento)
        {
            XmlDocument xmldoc = new XmlDocument();
            string sRuta = clsValidaciones.XMLTempCrea();
            try { xmldoc.Load(sRuta + "EmailConfig.xml"); }
            catch { xmldoc.Load(@"c:\EmailConfig.xml"); }

            XmlNodeList elemList = xmldoc.GetElementsByTagName(pvstrElemento);
            return elemList[0].InnerText;
        }

        /// <summary>
        /// Realiza la validación de los parámetros que se requieren
        /// para poder realizar el envío o notificación electrónica.
        /// </summary>
        /// <param name="pvstrProperty">Nombre de la propiedad.</param>
        /// <param name="pvstrArgument">Valor o contenido de la propiedad.</param>
        private void ValidateArgument(string pvstrProperty, string pvstrArgument)
        {
            if (pvstrArgument.Trim().Length == 0)
                throw new ArgumentException("El valor de la propiedad \"" + pvstrProperty + "\" se encuentra vacío.");
        }

        /// <summary>
        /// Valida uno a uno las propiedades que se requieren para
        /// la validación.
        /// </summary>
        private void Validate()
        {
            try
            {
                this.ValidateArgument("To", mTo);
                this.ValidateArgument("MailSubject", mSubject);
                this.ValidateArgument("MailFrom", mFrom);
                this.ValidateArgument("Body", mBody);
            }
            catch { }
        }

        private void ValidateAll()
        {
            try
            {
                this.ValidateArgument("To", mTo);
                this.ValidateArgument("MailSubject", mSubject);
                this.ValidateArgument("MailFrom", mFrom);
                this.ValidateArgument("Body", mBody);
                this.ValidateArgument("ServerName", mServerName);
                this.ValidateArgument("Port", mPort);
                this.ValidateArgument("SendPort", mSendPort);
                this.ValidateArgument("Password", mPassword);
            }
            catch { }
        }
    }
}
