using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Ssoft.Utils;

namespace Ssoft.ValueObjects
{
    public class VO_SabreBase
    {
        public const string FORMATO_DATETIME = "yyyy-MM-dd";
        public const string FORMATO_TIME_STAMP = "yyyy-MM-ddTHH:mm:ss";

        #region [ CONSTRUCTOR ]
        public VO_SabreBase()
        {
        }

        #endregion

        #region [ PROPIEADES ]

        public string ConversationId()
        {
            return clsSesiones.getCredentials().Conversacion;
            //return  clsValidaciones.GetKeyOrAdd("Sabre_Conversacion"]; 
        }

        public string From()
        {
            return clsSesiones.getCredentials().From;
            //return clsValidaciones.GetKeyOrAdd("Sabre_From"]; 
        }
        public string To()
        {
            return clsSesiones.getCredentials().To;
            //return  clsValidaciones.GetKeyOrAdd("Sabre_To"]; 
        }
        public string CPAId()
        {
            return clsValidaciones.GetKeyOrAdd("Sabre_CPAId"); 
        }
        public string MessageId()
        {
            return clsSesiones.getCredentials().Mensaje;
            //return clsValidaciones.GetKeyOrAdd("Sabre_Mensaje"]; 
        }
        #endregion

        #region [ DESTRUCTOR ]
        ~VO_SabreBase() { }
        #endregion
    }
}
