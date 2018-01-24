using System;
using System.Collections.Generic;
using System.Text;
using WS_SsoftSabre.SessionValidate;
using WS_SsoftSabre.Air;

namespace WS_SsoftSabre.General
{
    public class clsSessionValidate
    {
        public bool isSesionValida(string strSesion)
        {
            MessageHeader oMensaje = clsSabreBase.SessionValidate();

            if (oMensaje != null)
            {
                Security oSeguridad = new Security();
                oSeguridad.BinarySecurityToken = strSesion;

                SessionValidateRQService servicio = new SessionValidateRQService();
                servicio.MessageHeaderValue = oMensaje;
                servicio.SecurityValue = oSeguridad;
                object oRespuesta = servicio.SessionValidateRQ(strSesion);
            }
            return true;
        }
    }
}
