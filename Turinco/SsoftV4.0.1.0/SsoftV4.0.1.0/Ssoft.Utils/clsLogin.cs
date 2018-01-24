using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Ssoft.ManejadorExcepciones;
using Ssoft.Sql;

namespace Ssoft.Utils
{
    public class clsLogin
    {
        protected string strConexion = string.Empty;

        public clsLogin()
        {
            strConexion = clsSesiones.getConexion();
        }

        public string Conexion
        {
            set { this.strConexion = value; }
            get { return this.strConexion; }
        }

        public DataSet LoginWS(string strUser, string strPass)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     intEmpresa, intEmpresaPadre, strRazonSocial, strEmail, strPassword, strUsername " +
                          " FROM       tblEmpresas " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strUsername = '" + strUser + "') ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }


        public DataSet Login(string strPass, string strEmal)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad, tblcontactos.strCodigoExterno " +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strEmail = '" + strEmal + "') AND " +
                          " (tblRefere_1.intNivel = 0) AND (tblRefere_2.intNivel = 0) ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }
        public DataSet LoginSabre(string strPass, string strEmal)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, strUsername, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad, strCiudad As Ciudad, tblcontactos.strCodigoExterno " +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strUsername = '" + strEmal + "') AND " +
                          " (tblRefere_1.intNivel = 0) AND (tblRefere_2.intNivel = 0) AND " +
                          " (tblRefere.strIdioma = N'es') AND (tblRefere_2.strIdioma = N'es') AND (tblRefere_1.strIdioma = N'es') AND (tblRefere_2.intAplicacion = 1) AND  " +
                          " (tblRefere_1.intAplicacion = 1) AND (tblRefere.intAplicacion = 1) ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }
        public DataSet Login(string strPass, string strEmal, string strAgencia)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad, tblcontactos.strCodigoExterno " +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strEmail = '" + strEmal + "') AND (strIdentificacion = '" + strAgencia + "') AND " +
                          " (tblRefere_1.intNivel = 0) AND (tblRefere_2.intNivel = 0) ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet Login(string strPass, string strEmal, int Aplicacion)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad, tblcontactos.strCodigoExterno " +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strEmail = '" + strEmal + "') AND " +
                          " (tblRefere_1.intNivel = 0) AND (tblRefere_2.intNivel = 0)  AND (intAplicacion = " + Aplicacion + ") ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet LoginSinCiudad(string strPass, string strEmal)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad " +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strEmail = '" + strEmal + "')";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet LoginSinCiudad(string strPass, string strEmal, string Aplicacion)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad, tblcontactos.intTipoIdentifica as TipoIdentificacion , tblcontactos.strCodigoExterno" +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strEmail = '" + strEmal + "') AND (tblcontactos.intAplicacion = " + Aplicacion + ") ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet LoginSinCiudad(string strPass, string strEmal, string Aplicacion, string sIdioma)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad, tblcontactos.intTipoIdentifica as TipoIdentificacion, tblcontactos.strCodigoExterno " +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strEmail = '" + strEmal + "') AND (tblcontactos.intAplicacion = " + Aplicacion + ") " +
                          " AND (dbo.tblRefere.strIdioma = '" + sIdioma + "') AND (tblRefere_2.strIdioma = '" + sIdioma + "') AND (tblRefere_1.strIdioma = '" + sIdioma + "')";


                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }
        public DataSet LoginAdmin(string strPass, string strEmal, string Permiso)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad, dbo.tblcontactos.strCodigoExterno " +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_3 ON tblcontactos.intTipo = tblRefere_3.intidRefere " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strEmail = '" + strEmal + "') AND " +
                    //" (tblRefere_1.intNivel = 0) AND (tblRefere_2.intNivel = 0) AND ( tblRefere_3.strrefere='" + Permiso + "') ";
                          " (tblRefere_3.strrefere='" + Permiso + "')";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet LoginAdmin(string strPass, string strEmal, string Permiso, string Aplicacion)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad, dbo.tblcontactos.strCodigoExterno " +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_3 ON tblcontactos.intTipo = tblRefere_3.intidRefere " +
                          " WHERE      (strPassword = '" + strPass + "') AND (strEmail = '" + strEmal + "') AND " +
                    //" (tblRefere_1.intNivel = 0) AND (tblRefere_2.intNivel = 0) AND ( tblRefere_3.strrefere='" + Permiso + "') ";
                          " (tblRefere_3.strrefere='" + Permiso + "') AND (tblcontactos.intaplicacion=" + Aplicacion + ")";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet LoginAdmin(string strEmal, string Permiso)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT    tblcontactos.intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strRazonSocial, strEmail, strPassword, " +
                          " strNivel, strIdentificacion, strEmpresa, strNombre, strApellido, " +
                          " strUbicacion As strDireccion, strTelefono, strCelular,  dbo.tblRefere.strDetalle As strPais, " +
                          " tblRefere_1.strDetalle AS strEstado, tblRefere_2.strDetalle AS strCiudad, tblcontactos.strCodigoExterno " +
                          " FROM       tblRefere RIGHT OUTER JOIN tblcontactos ON tblRefere.intidRefere = tblcontactos.intPais LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_2 ON tblcontactos.intCiudad = tblRefere_2.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_1 ON tblcontactos.intEstado = tblRefere_1.intidRefere LEFT OUTER JOIN " +
                          " tblRefere AS tblRefere_3 ON tblcontactos.intTipo = tblRefere_3.intidRefere " +
                          " WHERE  (strEmail = '" + strEmal + "') AND " +
                    //" (tblRefere_1.intNivel = 0) AND (tblRefere_2.intNivel = 0) AND ( tblRefere_3.strrefere='" + Permiso + "') ";
                          " (tblRefere_3.strrefere='" + Permiso + "') ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet Login(string strEmal)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strEmail, strPassword " +
                          " FROM       tblcontactos " +
                          " WHERE      (strEmail = '" + strEmal + "') ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet Login(string strEmal, int Aplicacion)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     intAplicacion, intContacto, strNombre + ' ' + strApellido As Nombre, strEmail, strPassword " +
                          " FROM       tblcontactos " +
                          " WHERE      (strEmail = '" + strEmal + "') AND (intAplicacion = " + Aplicacion + ") ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet LoginExterno(string strUsuario, bool bCodigo)
        {
            StringBuilder strConsulta = new StringBuilder();
            DataSet dstDatos = new DataSet();
            string sIdioma = clsSesiones.getIdioma();
            try
            {
                strConsulta.AppendLine("EXEC SPCONSULTAUSUARIO "+strUsuario);
                dstDatos = new DataSql().Select(strConsulta.ToString());
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
            }
            return dstDatos;
        }

        public DataSet LoginExternoToken(string strUsuario)
        {
            StringBuilder strConsulta = new StringBuilder();
            DataSet dstDatos = new DataSet();
            string sIdioma = clsSesiones.getIdioma();
            try
            {
                strConsulta.AppendLine("                SELECT     dbo.tblcontactos.intAplicacion, dbo.tblcontactos.intContacto, dbo.tblcontactos.intContactoPadre, dbo.tblcontactos.intTipo,");
                strConsulta.AppendLine("                      dbo.tblcontactos.intTratamiento, dbo.tblcontactos.intTipoIdentifica, dbo.tblcontactos.strIdentificacion, dbo.tblcontactos.strNombre,");
                strConsulta.AppendLine("                      dbo.tblcontactos.strApellido, dbo.tblcontactos.strRazonSocial, dbo.tblcontactos.intUNegocio, dbo.tblcontactos.dtmFechaIngreso,");
                strConsulta.AppendLine("                      convert(varchar, dbo.tblcontactos.dtmFechaNac, 111) AS dtmFechaNac, dbo.tblcontactos.strUbicacion, dbo.tblcontactos.intPais, dbo.tblcontactos.intEstado, dbo.tblcontactos.intCiudad,");
                strConsulta.AppendLine("                      dbo.tblcontactos.strZona, dbo.tblcontactos.strTelefono, dbo.tblcontactos.strCelular, dbo.tblcontactos.strEmail, dbo.tblcontactos.strEmailAdicional,");
                strConsulta.AppendLine("                      dbo.tblcontactos.strPassword, dbo.tblcontactos.intResponsable, dbo.tblcontactos.strCargo, dbo.tblcontactos.strWeb,");
                strConsulta.AppendLine("                      dbo.tblcontactos.strObservaciones, dbo.tblcontactos.bitReserva, dbo.tblcontactos.strUsername, dbo.tblcontactos.strMapa, dbo.tblcontactos.strImagen,");
                strConsulta.AppendLine("                      dbo.tblcontactos.intSexo, dbo.tblcontactos.bitActivo, dbo.tblcontactos.strEmpresa, dbo.tblcontactos.strNivel, dbo.tblcontactos.dtmCFecha,");
                strConsulta.AppendLine("                      dbo.tblcontactos.strCiudad AS strciudadtext, dbo.tblcontactos.strFax, dbo.tblcontactos.strIata, dbo.tblcontactos.intTipoProveedor,");
                strConsulta.AppendLine("                      dbo.tblcontactos.dtmVigenciaIni, dbo.tblcontactos.dtmVigenciaFin, dbo.tblcontactos.bitHabilitaAcceso, dbo.tblcontactos.dtmMFecha,");
                strConsulta.AppendLine("                      dbo.tblcontactos.intContactoUser, dbo.tblcontactos.intGrupoUser, dbo.tblcontactos.strSnapCode, tblrefere_4.strRefere AS referetipocontacto,");
                strConsulta.AppendLine("                      tblrefere_4.strDetalle AS tipocontacto, tblrefere_1.strDetalle AS strciudad, tblrefere_2.strDetalle AS strestado, tblrefere_3.strDetalle AS strpais,");
                strConsulta.AppendLine("                      tblcontactos_1.intContacto AS intempresa, dbo.tblcontactos.intEmpresa AS intcomunidad, tblcontactos_1.intContactoPadre AS intagencia,");
                strConsulta.AppendLine("                      tblcontactos_2.intContactoPadre AS intpropietario, dbo.tblRefere.strRefere AS referetipoidentificacion, dbo.tblRefere.strDetalle AS detalleidentificacion,");
                strConsulta.AppendLine("                      dbo.tblRefere.strIdioma, dbo.tblRefere.intidRefere AS idreferetipoidentificacion, dbo.tblcontactos.strCodigoExterno");
                strConsulta.AppendLine("FROM         dbo.tblRefere INNER JOIN");

                strConsulta.AppendLine("                dbo.tblRefere AS tblrefere_4 INNER JOIN ");
                strConsulta.AppendLine("     dbo.tblcontactos ON tblrefere_4.intidRefere = dbo.tblcontactos.intTipo AND tblrefere_4.intAplicacion = dbo.tblcontactos.intAplicacion INNER JOIN ");
                strConsulta.AppendLine(" dbo.tblRefere AS tblrefere_1 ON dbo.tblcontactos.intCiudad = tblrefere_1.intidRefere AND dbo.tblcontactos.intAplicacion = tblrefere_1.intAplicacion INNER JOIN ");
                strConsulta.AppendLine(" dbo.tblRefere AS tblrefere_2 ON dbo.tblcontactos.intEstado = tblrefere_2.intidRefere AND dbo.tblcontactos.intAplicacion = tblrefere_2.intAplicacion INNER JOIN ");
                strConsulta.AppendLine(" dbo.tblRefere AS tblrefere_3 ON dbo.tblcontactos.intPais = tblrefere_3.intidRefere AND dbo.tblcontactos.intAplicacion = tblrefere_3.intAplicacion ON ");
                strConsulta.AppendLine(" dbo.tblRefere.intAplicacion = dbo.tblcontactos.intAplicacion AND dbo.tblRefere.intidRefere = dbo.tblcontactos.intTipoIdentifica LEFT OUTER JOIN ");
                strConsulta.AppendLine(" dbo.tblcontactos AS tblcontactos_2 INNER JOIN dbo.tblcontactos AS tblcontactos_1 ON tblcontactos_2.intContacto = tblcontactos_1.intContactoPadre ON ");
                strConsulta.AppendLine(" dbo.tblcontactos.intContactoPadre = tblcontactos_1.intContacto");

                strConsulta.AppendLine("WHERE     (tblrefere_4.strIdioma = 'es')");
                strConsulta.AppendLine("AND (tblrefere_1.strIdioma = '" + sIdioma + "')");
                strConsulta.AppendLine("AND (tblrefere_2.strIdioma = '" + sIdioma + "') ");
                strConsulta.AppendLine("AND (tblrefere_3.strIdioma = '" + sIdioma + "') ");
                strConsulta.AppendLine("AND (dbo.tblRefere.strIdioma = '" + sIdioma + "') ");
                strConsulta.AppendLine("AND (dbo.tblcontactos.bitActivo = 1)");

                strConsulta.AppendLine("AND (dbo.tblcontactos.strIdentificacion = '" + strUsuario + "')");

                dstDatos = new DataSql().Select(strConsulta.ToString());
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
            }
            return dstDatos;
        }
        public DataSet Menu(string strNivel)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT      intAplicacion, intIdMenu, intPos, strOpcion, intNivel, intIdMenuPadre, strNiveles, intNivel1, intNivel2, intNivel3, intNivel4, strLink, strImagen, strNivel,  " +
                          " strToolTip, strValue, intOrden, bitActivo " +
                          " FROM       tblmenutree " +
                          " WHERE      (strNivel LIKE '%" + strNivel + "%') AND (bitActivo = 1) " +
                          " ORDER BY intNivel, strNiveles ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet Opcion(string strPagina)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     intIdPermiso, strPagina, strProceso, strRead, strUpdate, strInsert, strDelete, strLink, strImagen " +
                          " FROM       tblpermiso " +
                          " WHERE      (strPagina ='" + strPagina + "') ";

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }

        public DataSet Nivel(string strNivel)
        {
            string pstrSql;
            DataSet dstDatos = new DataSet();
            try
            {
                pstrSql = " SELECT     intAplicacion, inGroupUser, strGrupo, strOpcion " +
                          " FROM       tblgroupuser ";
                if (strNivel != "0" && strNivel != "1")
                {
                    pstrSql = pstrSql + " WHERE      (inGroupUser =" + strNivel + ") ";
                }

                dstDatos = new DataSql().Select(pstrSql);
            }
            catch (Exception ex)
            {
                ExceptionHandled.Publicar(ex);
                return null;
                //throw new Exception(ex.Message);
            }
            return dstDatos;
        }
    }
}

