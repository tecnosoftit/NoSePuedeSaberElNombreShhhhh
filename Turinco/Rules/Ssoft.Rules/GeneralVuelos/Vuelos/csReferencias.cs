using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web.UI.WebControls;
using Ssoft.Utils;
using Ssoft.Sql;
using Ssoft.ManejadorExcepciones;
using Ssoft.ValueObjects;
using Ssoft.Rules.Administrador;
using System.Web;

namespace Ssoft.Rules.Corporativo
{

    public class csReferencias
    {
        private const string TABLA_CC = "CentroCostos";
        private const string COLUMN_ID = "Id";

        private const string COLUMN_ID_CONTACTO = "intContacto";
        private const string COLUMN_ID_EMPRESA = "intEmpresa";
        private const string COLUMN_ID_AGENCIA = "intAgencia";
        private const string COLUMN_ID_PROPIETARIO = "intPropietario";

        protected string strConexion = default(string);
        /// <summary>
        /// Estable u obtiene es string de conexion
        /// </summary>
        public string Conexion
        {
            set { strConexion = value; }
            get { return strConexion; }
        }
        public csReferencias()
        {
            Conexion = clsSesiones.getConexion();
        }

        public DataSet csRemark(string sAgencia)
        {
            DataSet dsDatos = new DataSet();
            StringBuilder consulta = new StringBuilder();

            try
            {
                consulta.AppendLine(" SELECT     tblRelaRemark.* ");
                consulta.AppendLine(" FROM         tblRelaRemark ");
                consulta.AppendLine(" WHERE    (tblRelaRemark.intAgencia = " + sAgencia + ") ");

                DataSql dsConsulta = new DataSql();
                dsConsulta.Conexion = this.Conexion;
                dsDatos = dsConsulta.Select(consulta.ToString());
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
                cParametros.Complemento = "Referencia de Corporativo " + consulta.ToString();
                ExceptionHandled.Publicar(cParametros);
            }
            return dsDatos;
        }
     
     
        /// <summary>
        /// METODO PENDIENTE POR REVISION
        /// </summary>
        /// <param name="idContacto"></param>
        /// <returns></returns>
        public static string csRazonSocial(string idContacto)
        {
            string sContacto = string.Empty;
            try
            {
                //tblContactos otblContactos = new tblContactos();
                //otblContactos.Get(idContacto);
                //if(otblContactos.Respuesta)
                //    sContacto = otblContactos.strRazonSocial.Value;
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
                cParametros.Complemento = "Nombre del contacto ";
                ExceptionHandled.Publicar(cParametros);
            }
            return sContacto;
        }
        public static string csViajero()
        {
            string sContacto = "0";
            try
            {
                clsCache cCache = new csCache().cCache();
                sContacto = cCache.Viajero.ToString();
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return sContacto;
        }
        public static string csEmpresa()
        {
            string sContacto = "0";
            try
            {
                clsCache cCache = new csCache().cCache();
                sContacto = cCache.Empresa.ToString();
                if (sContacto.Length.Equals(0))
                    sContacto = clsValidaciones.GetKeyOrAdd("idEmpresa", "0");
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return sContacto;
        }
     
      
        public static string csContactoPadre()
        {
            string sContacto = "0";
            try
            {
                clsCache cCache = new csCache().cCache();
               
                if (sContacto.Equals("0"))
                    sContacto = csContacto();
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return sContacto;
        }
        public static string csContacto()
        {
            string sContacto = "0";
            try
            {
                clsCache cCache = new csCache().cCache();
                sContacto = cCache.Contacto.ToString();
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return sContacto;
        }
      
        public static string csAgencia(string sidContacto)
        {
            string sContacto = "0";
            try
            {
                DataSet dsData = new csContactos().ContactosNivel(sidContacto);
                if (dsData != null)
                {
                    sContacto = dsData.Tables[0].Rows[0]["intContactoAgencia"].ToString();
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return sContacto;
        }
        public static string csEmpresa(string sidContacto)
        {
            string sContacto = "0";
            try
            {
                DataSet dsData = new csContactos().ContactosNivel(sidContacto);
                if (dsData != null)
                {
                    sContacto = dsData.Tables[0].Rows[0]["intContactoEmpresa"].ToString();
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return sContacto;
        }
       
        public static string csTipoContactoRefere()
        {
            string sContacto = "0";
            try
            {
                clsCache cCache = new csCache().cCache();
                sContacto = cCache.RefereContacto.ToString();
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return sContacto;
        }
        public static string csTipoContacto()
        {
            string sContacto = string.Empty;
            try
            {
                clsCache cCache = new csCache().cCache();
                sContacto = cCache.TipoContacto.ToString();
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
                cParametros.Complemento = "Referencia de Corporativo ";
                ExceptionHandled.Publicar(cParametros);
            }
            return sContacto;
        }
       
     
     
        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="sidContacto"></param>
        /// <returns></returns>
        public static string csWeb(string sidContacto)
        {
            string sWeb = string.Empty;
            try
            {
                //tblContactos otblContactos = new tblContactos();
                //otblContactos.Get(sidContacto);
                //if (otblContactos.Respuesta)
                //{
                //    sWeb = otblContactos.strWeb.Value;
                //}
            }
            catch
            {

            }
            return sWeb;
        }
        public static VO_Credentials csCredenciales(Enum_ProveedorWebServices eProveedor)
        {
            VO_Credentials vCredentials = new VO_Credentials();
            switch (eProveedor)
            {
                case Enum_ProveedorWebServices.Sabre:
                    vCredentials = clsConfiguracionSabre.Credentials();
                    break;
            }
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
                    //        if (cCache.Corporativo[i].ProveedorWs.Equals(eProveedor))
                    //        {
                    //            vCredentials = cCache.Corporativo[i].Credentials;
                    //        }
                    //    }
                    //}
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
                cParametros.Complemento = "Actualiza agencia en la cache ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return vCredentials;
        }
      
   
        public static void csActualizaPcc(string sPseudo)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                clsCacheControl cCacheControl = new clsCacheControl();
                int iTotal = 0;
                iTotal = 0;
                try
                {
                    //if (cCache.Corporativo != null)
                    //    iTotal = cCache.Corporativo.Count;
                }
                catch { }
                //for (int i = 0; i < iTotal; i++)
                //{
                //    if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
                //    {
                //        cCache.Corporativo[i].Credentials.Pcc = sPseudo;
                //        clsSesiones.setCredentials(cCache.Corporativo[i].Credentials);
                //    }
                //}
                cCache = cCacheControl.ActualizaXML(cCache);
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
                cParametros.Complemento = "Actualiza agencia en la cache ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static void csActualizaPcc(string sPais, bool bDefault)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                clsCacheControl cCacheControl = new clsCacheControl();
                int iTotal = 0;
                iTotal = 0;
                try
                {
                    //if (cCache.Corporativo != null)
                    //    iTotal = cCache.Corporativo.Count;
                }
                catch { }
                string sPcc = string.Empty;
                int iPseudo = 0;
                string sPccPais = clsValidaciones.GetKeyOrAdd("PaisDefault", "COL");
                if (!sPais.Length.Equals(0))
                {
                    for (int i = 0; i < iTotal; i++)
                    {
                        int iConfig = 0;
                        try
                        {
                            //if (cCache.Corporativo[i].Configuracion != null)
                            //    iConfig = cCache.Corporativo[i].Configuracion.Count;
                        }
                        catch { }
                        for (int j = 0; j < iConfig; j++)
                        {
                            //if (cCache.Corporativo[i].Configuracion[j].Operador.Equals(Enum_ProveedorWebServices.Sabre))
                            //{
                            //    int iCount = cCache.Corporativo[i].Configuracion[j].lPseudos.Count;
                            //    for (int k = 0; k < iCount; k++)
                            //    {
                            //        if (cCache.Corporativo[i].Configuracion[j].lPseudos[k].IdPais.Equals(int.Parse(sPais)))
                            //        {
                            //            sPcc = cCache.Corporativo[i].Configuracion[j].lPseudos[k].Pseudo;
                            //            iPseudo = cCache.Corporativo[i].Configuracion[j].lPseudos[k].IdPseudo;
                            //            sPccPais = cCache.Corporativo[i].Configuracion[j].lPseudos[k].Pais;
                            //        }
                            //    }
                            //}
                        }
                    }
                }
                iTotal = 0;
                try
                {
                    //if (cCache.Corporativo != null)
                    //    iTotal = cCache.Corporativo.Count;
                }
                catch { }

                for (int i = 0; i < iTotal; i++)
                {
                    //if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
                    //{
                    //    if (bDefault)
                    //    {
                    //        cCache.Corporativo[i].Credentials.Pcc = cCache.Corporativo[i].Credentials.PccDefault;
                    //        cCache.Corporativo[i].Credentials.Pseudo = cCache.Corporativo[i].Credentials.PseudoDefault;
                    //        cCache.Corporativo[i].Credentials.PccPais = cCache.Corporativo[i].Credentials.PccDefaultPais;
                    //    }
                    //    else
                    //    {
                    //        if (!sPcc.Length.Equals(0))
                    //        {
                    //            cCache.Corporativo[i].Credentials.Pcc = sPcc;
                    //            cCache.Corporativo[i].Credentials.Pseudo = iPseudo;
                    //            cCache.Corporativo[i].Credentials.PccPais = sPccPais;
                    //        }
                    //        else
                    //        {
                    //            cCache.Corporativo[i].Credentials.Pcc = cCache.Corporativo[i].Credentials.PccDefault;
                    //            cCache.Corporativo[i].Credentials.Pseudo = cCache.Corporativo[i].Credentials.PseudoDefault;
                    //            cCache.Corporativo[i].Credentials.PccPais = cCache.Corporativo[i].Credentials.PccDefaultPais;
                    //        }
                    //    }
                    //    clsSesiones.setCredentials(cCache.Corporativo[i].Credentials);
                    //}
                }
                cCache = cCacheControl.ActualizaXML(cCache);
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
                cParametros.Complemento = "Actualiza agencia en la cache ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public static string csRecuperaPcc()
        {
            string sPcc = string.Empty;
            try
            {
                clsCache cCache = new csCache().cCache();
                int iTotal = 0;
                try
                {
                    //if (cCache.Corporativo != null)
                    //    iTotal = cCache.Corporativo.Count;
                }
                catch { }

                for (int i = 0; i < iTotal; i++)
                {
                    //if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
                    //{
                    //    sPcc = cCache.Corporativo[i].Credentials.Pcc;
                    //}
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
                cParametros.Complemento = "Actualiza agencia en la cache ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return sPcc;
        }
        public static string csRecuperaPccPais()
        {
            string sPcc = string.Empty;
            try
            {
                clsCache cCache = new csCache().cCache();
                int iTotal = 0;
                try
                {
                    //if (cCache.Corporativo != null)
                    //    iTotal = cCache.Corporativo.Count;
                }
                catch { }

                for (int i = 0; i < iTotal; i++)
                {
                    //if (cCache.Corporativo[i].ProveedorWs.Equals(Enum_ProveedorWebServices.Sabre))
                    //{
                    //    sPcc = cCache.Corporativo[i].Credentials.PccPais;
                    //}
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
                cParametros.Complemento = "Actualiza agencia en la cache ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
            return sPcc;
        }
       
     
    }
}
