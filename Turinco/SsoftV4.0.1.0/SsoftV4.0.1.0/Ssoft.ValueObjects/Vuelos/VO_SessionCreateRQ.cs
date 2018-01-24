using System;
using System.Collections.Generic;
using System.Text;

namespace Ssoft.ValueObjects
{
    public class VO_SessionCreateRQ
    {
        private string strUsuario;
        private string strClave;
        private string strIpcc;
        private string strPcc;
        private string strDominio;

        public string StrUsuario
        {
            get { return strUsuario; }
            set { strUsuario = value; }
        }
        public string StrClave
        {
            get { return strClave; }
            set { strClave = value; }
        }
        public string StrIpcc
        {
            get { return strIpcc; }
            set { strIpcc = value; }
        }
        public string StrPcc
        {
            get { return strPcc; }
            set { strPcc = value; }
        }
        public string StrDominio
        {
            get { return strDominio; }
            set { strDominio = value; }
        }

        public VO_SessionCreateRQ(
           string strUsuario,
           string strClave,
           string strIpcc,
           string strPcc,
           string strDominio)
        {
            this.strUsuario = strUsuario;
            this.strClave = strClave;
            this.strIpcc = strIpcc;
            this.strPcc = strPcc;
            this.strDominio = strDominio;
        }
    }
}
