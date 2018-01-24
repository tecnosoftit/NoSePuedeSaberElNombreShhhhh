using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration;
using System.Data;
using Ssoft.ValueObjects;
using System.Web.Caching;
using System.Web.UI;
using Ssoft.ManejadorExcepciones;
using Ssoft.Email;
using Ssoft.Ssoft.ValueObjects.Hoteles;

namespace Ssoft.Utils
{
    public class clsSesiones
    {
        #region [ CONSTRUCTOR ]

        public clsSesiones() { }

        #endregion

        #region [ Sessiones ]

        public static string getSesionIDLocal()
        {
            try
            {
                if (HttpContext.Current.Session["SessionIDLocal"] == null)
                {
                    return null;
                }
                else
                {
                    setSesionIDLocal(HttpContext.Current.Session["SessionIDLocal"].ToString());
                    setSesionID(HttpContext.Current.Session["SessionIDLocal"].ToString());
                    return (string)(HttpContext.Current.Session["SessionIDLocal"]);
                }
            }
            catch { return null; }
        }
        public static void setSesionIDLocal(string sSesion)
        {
            HttpContext.Current.Session.Add("SessionIDLocal", sSesion);
        }

        public static string getSesionID()
        {
            try
            {
                if (HttpContext.Current.Session["SessionID"] == null)
                {
                    return null;
                }
                else
                {
                    setSesionID(HttpContext.Current.Session["SessionID"].ToString());
                    setSesionIDLocal(HttpContext.Current.Session["SessionID"].ToString());
                    return (string)(HttpContext.Current.Session["SessionID"]);
                }
            }
            catch { return null; }
        }
        public static void setSesionID(string sSesion)
        {
            HttpContext.Current.Session.Add("SessionID", sSesion);
        }

#region " Conexion "
        public static string getConexion()
        {
            try
            {
                if (HttpContext.Current.Session["$sConexion"] == null)
                {
                    try
                    {
                        setConexion(clsValidaciones.GetKeyOrAdd("strConexion"));
                        return clsValidaciones.GetKeyOrAdd("strConexion");
                    }
                    catch
                    {
                        return null;
                    }

                }
                else
                {
                    return (string)(HttpContext.Current.Session["$sConexion"]);
                }
            }
            catch
            {
                try
                {
                    try
                    {
                        setConexion(clsValidaciones.GetKeyOrAdd("strConexion"));
                    }
                    catch (Exception)
                    {
                        return clsValidaciones.GetKeyOrAdd("strConexion");
                    }
                    return clsValidaciones.GetKeyOrAdd("strConexion");
                }
                catch
                {
                    return null;
                }
            }
        }

        public static void setConexion(string sConexion_)
        {
            HttpContext.Current.Session.Add("$sConexion", sConexion_);
        }

        #endregion

        #region " Credenciales "
        public static VO_Credentials getCredentials()
        {
            if (HttpContext.Current.Session["$sCredential"] == null)
            {
                VO_Credentials vo_Credentials = clsConfiguracionSabre.Credentials();
                clsSesiones.setCredentials(vo_Credentials);
                return vo_Credentials;
            }
            else
            {
                return (VO_Credentials)(HttpContext.Current.Session["$sCredential"]);
            }
        }
        public static void setCredentials(VO_Credentials sCredential)
        {
            HttpContext.Current.Session.Add("$sCredential", sCredential);
        }
        //Correo
        public static Message getEmails()
        {
            if (HttpContext.Current.Session["$sEmails"] == null)
            {
                Message msEmail = new Message();
                msEmail.ReadParameters();
                clsSesiones.setEmail(msEmail);
                return msEmail;
            }
            else
            {
                return (Message)(HttpContext.Current.Session["$sEmails"]);
            }
        }
        public static void setEmail(Message sEmails)
        {
            HttpContext.Current.Session.Add("$sEmails", sEmails);
        }

        public static List<VO_Credentials> getlCredentials()
        {
            if (HttpContext.Current.Session["$slCredential"] == null) return null;
            else return (List<VO_Credentials>)(HttpContext.Current.Session["$slCredential"]);
        }

        public static void setlCredentials(List<VO_Credentials> slCredential)
        {
            HttpContext.Current.Session.Add("$slCredential", slCredential);
        }
        #endregion

        #region " Traductor "
        public static string getTraductor()
        {
            if (HttpContext.Current.Session["$sTraductor"] == null) return null;
            else return (string)(HttpContext.Current.Session["$sTraductor"]);
        }

        public static void setTraductor(string sTraductor_)
        {
            HttpContext.Current.Session.Add("$sTraductor", sTraductor_);
        }

        #endregion

        #region " Idiomas "
        public static string getIdioma()
        {
            string sIdioma = "es";

            try
            {
                if (HttpContext.Current.Session["$sIdiomaPag"] == null)
                {
                    try
                    {
                        sIdioma = clsValidaciones.GetKeyOrAdd("sIdioma","es").ToString();
                        setIdioma(sIdioma);
                    }
                    catch
                    {
                        setIdioma(sIdioma);
                    }
                }
            }
            catch
            {
                try
                {
                    sIdioma = clsValidaciones.GetKeyOrAdd("sIdioma","es").ToString();
                    setIdioma(sIdioma);
                }
                catch
                {
                    try
                    {
                        setIdioma(sIdioma);
                    }
                    catch (Exception)
                    {

                        return sIdioma;
                    }
                }
            }
            return (string)(HttpContext.Current.Session["$sIdiomaPag"]);
        }

        public static void setIdioma(string sIdiomaPag_)
        {
            HttpContext.Current.Session.Add("$sIdiomaPag", sIdiomaPag_);
        }

        #endregion

        #region " Aplicacion "
        public static int getAplicacion()
        {
            int iAplicacion = 1;
            try
            {
                if (HttpContext.Current.Session["$iAplicacionPag"] == null)
                {
                    try
                    {
                        iAplicacion = clsValidaciones.idAplicacion();
                        setAplicacion(iAplicacion);
                    }
                    catch
                    {
                        setAplicacion(iAplicacion);
                    }
                }
            }
            catch
            {
                try
                {
                    iAplicacion = clsValidaciones.idAplicacion();
                    setAplicacion(iAplicacion);
                }
                catch
                {
                    try
                    {
                        setAplicacion(iAplicacion);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            setAplicacion(clsValidaciones.ConsultaIdAplicacion());
                        }
                        catch
                        {
                            return clsValidaciones.ConsultaIdAplicacion();
                        }
                    }
                }
            }
            return (int)(HttpContext.Current.Session["$iAplicacionPag"]);
        }

        public static void setAplicacion(int iAplicacion_)
        {
            HttpContext.Current.Session.Add("$iAplicacionPag", iAplicacion_);
        }

        #endregion

        #region " Formato Fechas "
        public static string getFormatoFecha()
        {
            //string sFecha = "MM/dd/yyyy";
            //try
            //{
            //    sFecha = clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");
            //}
            //catch (Exception) { }
            //if (HttpContext.Current.Session["$sFormatoFecha"] == null)
            //{
            //    try
            //    {
            //        setFormatoFecha(sFecha);
            //    }
            //    catch (Exception)
            //    {
            //        return sFecha;
            //    }
            //}
            return clsValidaciones.GetKeyOrAdd("FormatoFecha", "MM/dd/yyyy");//(string)(HttpContext.Current.Session["$sFormatoFecha"]);
        }
        public static void setFormatoFecha(string sFormatoFecha_)
        {
            HttpContext.Current.Session.Add("$sFormatoFecha", sFormatoFecha_);
        }

        public static string getFormatoFechaBD()
        {
            //string sFecha = clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");
            //if (HttpContext.Current.Session["$sFormatoFechaBD"] == null)
            //{
            //    setFormatoFechaBD(sFecha);
            //}
            return clsValidaciones.GetKeyOrAdd("FormatoFechaBD", "yyyy/MM/dd");//(string)(HttpContext.Current.Session["$sFormatoFechaBD"]);
        }

        public static void setFormatoFechaBD(string sFormatoFechaBD_)
        {
            HttpContext.Current.Session.Add("$sFormatoFechaBD", sFormatoFechaBD_);
        }
        #endregion

        #region " Cache "
        // cache para el webservices
        public static clsCache getCacheWS()
        {
            if (HttpContext.Current.Session["$csCacheWS"] == null)
            {
                if (HttpContext.Current.Session["SessionIDLocal"] != null)
                {
                    clsCacheControl cCacheControl = new clsCacheControl();
                    clsCache cCache = cCacheControl.RecuperarXML(HttpContext.Current.Session["SessionIDLocal"].ToString());
                    if (cCache == null)
                    {
                        return null;
                    }
                    else
                    {
                        setCache(cCache);
                        return cCache;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return (clsCache)(HttpContext.Current.Session["$csCacheWS"]);
            }
        }

        public static void setCacheWS(clsCache csCacheWS_)
        {
            HttpContext.Current.Session.Add("$csCacheWS", csCacheWS_);
        }
        // Cache pagina
        public static clsCache getCache()
        {
            Page PaginaActual = (Page)HttpContext.Current.Handler;
            return getCache(PaginaActual);
        }

        public static clsCache getCache(Page PaginaActual)
        {
            clsCache cCache = new clsCache();
            clsCacheControl cCacheControl = new clsCacheControl();
            string sSesion = cCacheControl.RecuperarSesionId(PaginaActual);
            if (sSesion != null && sSesion.Length > 0)
            {
                cCache = cCacheControl.RecuperarSesion(sSesion);
                setCache(cCache);
                return cCache;
            }
            else
            {
                try
                {
                    if (HttpContext.Current.Session["SessionIDLocal"] != null)
                    {
                        sSesion = HttpContext.Current.Session["SessionIDLocal"].ToString();
                    }
                    else
                    {
                        if (PaginaActual.Request.QueryString["idSesion"] != null)
                        {
                            sSesion = PaginaActual.Request.QueryString["idSesion"].ToString();
                        }
                    }
                }
                catch { sSesion = null; }
                if (sSesion != null && sSesion.Length > 0)
                {
                    cCache = cCacheControl.RecuperarSesion(sSesion);
                    setCache(cCache);
                    return cCache;
                }
                else
                {
                    if (HttpContext.Current.Session["$csCache"] != null)
                    {
                        cCache = (clsCache)(HttpContext.Current.Session["$csCache"]);
                        setCache(cCache);
                        return cCache;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        //public static clsCache getCache()
        //{
        //    clsCacheControl cCacheControl = new clsCacheControl();
        //    clsCache cCache = new clsCache();
        //    Page PaginaActual = (Page)HttpContext.Current.Handler;
        //    if (HttpContext.Current.Session["$csCache"] == null)
        //    {
        //        if (HttpContext.Current.Session["SessionIDLocal"] != null)
        //        {
        //            cCache = cCacheControl.RecuperarXML(HttpContext.Current.Session["SessionIDLocal"].ToString());
        //            if (cCache == null)
        //            {
        //                cCache = cCacheControl.RecuperarSesion(cCacheControl.RecuperarSesionId(PaginaActual));
        //                if (cCache == null)
        //                {
        //                    try { cCache = (clsCache)PaginaActual.Cache[HttpContext.Current.Session["SessionIDLocal"].ToString()]; }
        //                    catch { return null; }
        //                    if (cCache == null)
        //                    {
        //                        return null;
        //                    }
        //                    else
        //                    {
        //                        csCache.ActualizarCache(cCache);
        //                        setCache(cCache);
        //                        return cCache;
        //                    }
        //                }
        //                else
        //                {
        //                    setCache(cCache);
        //                    return cCache;
        //                }
        //            }
        //            else
        //            {
        //                setCache(cCache);
        //                return cCache;
        //            }
        //        }
        //        else
        //        {
        //            cCache = cCacheControl.RecuperarSesion(cCacheControl.RecuperarSesionId(PaginaActual));
        //            if (cCache == null)
        //            {
        //                try { cCache = (clsCache)PaginaActual.Cache[cCacheControl.RecuperarSesionId(PaginaActual)]; }
        //                catch { return null; }
        //                if (cCache == null)
        //                {
        //                    return null;
        //                }
        //                else
        //                {
        //                    csCache.ActualizarCache(cCache);
        //                    setCache(cCache);
        //                    return cCache;
        //                }
        //            }
        //            else
        //            {
        //                setCache(cCache);
        //                return cCache;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        cCache = (clsCache)(HttpContext.Current.Session["$csCache"]);
        //        setCache(cCache);
        //        return cCache;
        //    }
        //}

        //public static clsCache getCache(Page PaginaActual)
        //{
        //    clsCache cCache = new clsCache();
        //    clsCacheControl cCacheControl = new clsCacheControl();

        //    if (HttpContext.Current.Session != null && HttpContext.Current.Session["$csCache"] == null)
        //    {
        //        if (HttpContext.Current.Session["SessionIDLocal"] != null)
        //        {
        //            cCache = cCacheControl.RecuperarXML(HttpContext.Current.Session["SessionIDLocal"].ToString());
        //            if (cCache == null)
        //            {
        //                cCache = cCacheControl.RecuperarSesion(cCacheControl.RecuperarSesionId(PaginaActual));
        //                if (cCache == null)
        //                {
        //                    try { cCache = (clsCache)PaginaActual.Cache[HttpContext.Current.Session["SessionIDLocal"].ToString()]; }
        //                    catch { return null; }
        //                    if (cCache == null)
        //                    {
        //                        return null;
        //                    }
        //                    else
        //                    {
        //                        csCache.ActualizarCache(cCache);
        //                        setCache(cCache);
        //                        return cCache;
        //                    }
        //                }
        //                else
        //                {
        //                    setCache(cCache);
        //                    return cCache;
        //                }
        //            }
        //            else
        //            {
        //                setCache(cCache);
        //                return cCache;
        //            }
        //        }
        //        else
        //        {
        //            cCache = cCacheControl.RecuperarSesion(cCacheControl.RecuperarSesionId(PaginaActual));
        //            if (cCache == null)
        //            {
        //                try { cCache = (clsCache)PaginaActual.Cache[cCacheControl.RecuperarSesionId(PaginaActual)]; }
        //                catch { return null; }
        //                if (cCache == null)
        //                {
        //                    return null;
        //                }
        //                else
        //                {
        //                    csCache.ActualizarCache(cCache);
        //                    setCache(cCache);
        //                    return cCache;
        //                }
        //            }
        //            else
        //            {
        //                setCache(cCache);
        //                return cCache;
        //            }
        //        }
        //    }/*POR REVISAR*/

        //    else
        //    {
        //        if (HttpContext.Current.Session != null)
        //        {
        //            cCache = (clsCache)(HttpContext.Current.Session["$csCache"]);
        //            setCache(cCache);
        //        }
        //        else
        //        {
        //            cCache = null;
        //        }
        //        return cCache;
        //    }
        //}

        public static void setCache(clsCache csCache_)
        {
            try
            {
                HttpContext.Current.Session.Add("$csCache", csCache_);
                HttpContext.Current.Session["SessionIDLocal"] = csCache_.SessionID;
            }
            catch { }
        }

        #endregion

        #region " Reserva "

        public static string getReserva()
        {
            if (HttpContext.Current.Session["$sReserva"] == null) return "0";
            else return (string)(HttpContext.Current.Session["$sReserva"]);
        }

        public static void setReserva(string sReserva_)
        {
            HttpContext.Current.Session.Add("$sReserva", sReserva_);
        }

        public static string getProyecto()
        {
            if (HttpContext.Current.Session["$sProyecto"] == null)
            {
                try
                {
                    clsCache cCache = new csCache().cCache();
                    if (cCache != null)
                    {
                        setProyecto(cCache.Proyecto);
                    }
                    else
                    {
                        setProyecto("0");
                    }
                }
                catch { setProyecto("0"); }
                return (string)(HttpContext.Current.Session["$sProyecto"]);
            }
            else
            {
                if (HttpContext.Current.Session["$sProyecto"].ToString().Equals("0"))
                {
                    try
                    {
                        clsCache cCache = new csCache().cCache();
                        if (cCache != null)
                        {
                            setProyecto(cCache.Proyecto);
                        }
                        else
                        {
                            setProyecto("0");
                        }
                    }
                    catch { setProyecto("0"); }
                    return (string)(HttpContext.Current.Session["$sProyecto"]);
                }
                else
                {
                    return (string)(HttpContext.Current.Session["$sProyecto"]);
                }
            }
        }

        public static void setProyecto(string sProyecto_)
        {
            HttpContext.Current.Session.Add("$sProyecto", sProyecto_);
            clsCache cCache = new csCache().cCache();
            try
            {
                if (cCache != null)
                {
                    cCache.Proyecto = sProyecto_;
                    csCache.ActualizarCache(cCache);
                }
            }
            catch { }
        }

        public static bool getImpuestos()
        {
            if (HttpContext.Current.Session["$sImpuestos"] == null)
            {
                try
                {
                    clsCache cCache = new csCache().cCache();
                    if(cCache != null)
                        setImpuestos(cCache.Impuestos);
                }
                catch { }
                return (bool)(HttpContext.Current.Session["$sImpuestos"]);
            }
            else
            {
                return (bool)(HttpContext.Current.Session["$sImpuestos"]);
            }
        }

        public static void setImpuestos(bool sImpuestos_)
        {
            HttpContext.Current.Session.Add("$sImpuestos", sImpuestos_);
        }

        public static int getNumeroPasajeros()
        {
            if (HttpContext.Current.Session["$iNumeroPasajeros"] == null) return 0;
            else return (int)(HttpContext.Current.Session["$iNumeroPasajeros"]);
        }

        public static void setNumeroPasajeros(int iNumeroPasajeros_)
        {
            HttpContext.Current.Session.Add("$iNumeroPasajeros", iNumeroPasajeros_);
        }

        #endregion

        #region " Buscadores "

        public static Enum_Priority getPriority()
        {
            if (HttpContext.Current.Session["$ePriority"] == null) return Enum_Priority.Price;
            else return (Enum_Priority)(HttpContext.Current.Session["$ePriority"]);
        }

        public static void setPriority(Enum_Priority ePriority_)
        {
            HttpContext.Current.Session.Add("$ePriority", ePriority_);
        }
        public static void setPantalleRespuestaLogin(string pantallaLogin)
        {
            HttpContext.Current.Session.Add("$pantallaLogin", pantallaLogin);
            clsCache cCache = new csCache().cCache();
            cCache.PantallaRespuestaLogin = pantallaLogin;
            clsCacheControl cCacheControl = new clsCacheControl();
            cCacheControl.ActualizaXML(cCache);
        }

        public static string getPantalleRespuestaLogin()
        {
            if (HttpContext.Current.Session["$pantallaLogin"] == null)
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    setPantalleRespuestaLogin(cCache.PantallaRespuestaLogin);
                }
                return (string)(HttpContext.Current.Session["$pantallaLogin"]);
            }
            else return (string)(HttpContext.Current.Session["$pantallaLogin"]);
        }

        public static void setAerolineaValidadora(string strAerolineaValida)
        {
            HttpContext.Current.Session.Add("$strAerolineaValida", strAerolineaValida);

            clsCache cCache = new csCache().cCache();
            cCache.AerolineaValidadora = strAerolineaValida;
            clsCacheControl cCacheControl = new clsCacheControl();
            cCacheControl.ActualizaXML(cCache);
        }

        public static string getAerolineaValidadora()
        {
            if (HttpContext.Current.Session["$strAerolineaValida"] == null)
            {
                clsCache cCache = new csCache().cCache();
                setAerolineaValidadora(cCache.AerolineaValidadora);
                return (string)(HttpContext.Current.Session["$strAerolineaValida"]);
            }
            else
            { return (string)(HttpContext.Current.Session["$strAerolineaValida"]); }
        }

        public static Enum_TipoVuelo getTipoVuelo()
        {
            if (HttpContext.Current.Session["$eTipoVuelo"] == null)
            {
                clsCache cCache = new csCache().cCache();
                setTipoVuelo(cCache.TipoVuelo);
                return (Enum_TipoVuelo)(HttpContext.Current.Session["$eTipoVuelo"]);
            }
            else
            {
                return (Enum_TipoVuelo)(HttpContext.Current.Session["$eTipoVuelo"]);
            }
        }

        public static void setTipoVuelo(Enum_TipoVuelo eTipoVuelo)
        {
            HttpContext.Current.Session.Add("$eTipoVuelo", eTipoVuelo);
        }

        #endregion

        

  

        #region " clsResultados "

        public static void setResultados(clsResultados Resultados_)
        {
            HttpContext.Current.Session.Add("$clsResulltados", Resultados_);
        }

        public static clsResultados getResultados()
        {
            if (HttpContext.Current.Session["$clsResulltados"] == null) return null;
            else return (clsResultados)(HttpContext.Current.Session["$clsResulltados"]);
        }

        public static void setParametrosError(clsParametros Parametros_)
        {
            HttpContext.Current.Session.Add("$clsParametros", Parametros_);
        }

        public static clsParametros getParametrosError()
        {
            if (HttpContext.Current.Session["$clsParametros"] == null) return null;
            else return (clsParametros)(HttpContext.Current.Session["$clsParametros"]);
        }

        #endregion

        #region " Resultados "

        public static DataSet getResultadoPlanes()
        {
            if (HttpContext.Current.Session["$dsResultadoPlanes"] == null) return null;
            else return (DataSet)(HttpContext.Current.Session["$dsResultadoPlanes"]);
        }

        public static void setResultadoPlanes(DataSet dsResultadoPlanes)
        {
            HttpContext.Current.Session.Add("$dsResultadoPlanes", dsResultadoPlanes);
        }

        public static DataSet getResultadoHotel()
        {
            DataSet dsResultadoHotel = null;
            try
            {
                if (HttpContext.Current.Session["$dsResultadoHotel"] == null)
                {
                    try
                    {
                        dsResultadoHotel = new csCacheParam().cRecuperaDsResult("dsResultadoHotel");
                    }
                    catch { }
                }
                else
                {
                    dsResultadoHotel = (DataSet)(HttpContext.Current.Session["$dsResultadoHotel"]);
                }
                if (dsResultadoHotel != null)
                    setResultadoHotel(dsResultadoHotel);
            }
            catch { }
            return dsResultadoHotel;

        }

        public static void setResultadoHotel(DataSet dsResultadoHotel_)
        {
            HttpContext.Current.Session.Add("$dsResultadoHotel", dsResultadoHotel_);
            new csCacheParam().cGuardaDsResult(dsResultadoHotel_, "dsResultadoHotel");
        }

        public static DataSet getDetalleHotelDs()
        {
            if (HttpContext.Current.Session["$dsDetalleHotel"] == null) return null;
            else return (DataSet)(HttpContext.Current.Session["$dsDetalleHotel"]);
        }

        public static void setDetalleHotelDs(DataSet dsDetalleHotel_)
        {
            HttpContext.Current.Session.Add("$dsDetalleHotel", dsDetalleHotel_);
        }

        public static DataSet getDescriptionHotel()
        {
            if (HttpContext.Current.Session["$dsDescriptionHotel"] == null) return null;
            else return (DataSet)(HttpContext.Current.Session["$dsDescriptionHotel"]);
        }

        public static void setDescriptionHotel(DataSet dsDescriptionHotel_)
        {
            HttpContext.Current.Session.Add("$dsDescriptionHotel", dsDescriptionHotel_);
        }

        public static DataSet getReservaHotel()
        {
            if (HttpContext.Current.Session["$dsReservaHotel"] == null) return null;
            else return (DataSet)(HttpContext.Current.Session["$dsReservaHotel"]);
        }

        public static void setReservaHotel(DataSet dsReservaHotel_)
        {
            HttpContext.Current.Session.Add("$dsReservaHotel", dsReservaHotel_);
        }

        public static DataSet getConfirmaHotel()
        {
            if (HttpContext.Current.Session["$dsConfirmaHotel"] == null) return null;
            else return (DataSet)(HttpContext.Current.Session["$dsConfirmaHotel"]);
        }

        public static void setConfirmaHotel(DataSet dsConfirmaHotel_)
        {
            HttpContext.Current.Session.Add("$dsConfirmaHotel", dsConfirmaHotel_);
        }

        public static string getRoomTypeCode()
        {
            if (HttpContext.Current.Session["$sRoomTypeCode"] == null) return null;
            else return (string)(HttpContext.Current.Session["$sRoomTypeCode"]);
        }

        public static void setRoomTypeCode(string sRoomTypeCode_)
        {
            HttpContext.Current.Session.Add("$sRoomTypeCode", sRoomTypeCode_);
        }

        public static string getRulesFromPrice()
        {
            if (HttpContext.Current.Session["$sRulesFromPrice"] == null) return null;
            else return (string)(HttpContext.Current.Session["$sRulesFromPrice"]);
        }

        public static void setRulesFromPrice(string sRulesFromPrice_)
        {
            HttpContext.Current.Session.Add("$sRulesFromPrice", sRulesFromPrice_);
        }



        public static void CLEAR_SESSION_PAX()
        {
            HttpContext.Current.Session.Remove("$vo_Passenger");
        }

        public static void CLEAR_SESSION_ALL()
        {
            CLEAR_SESSION_AIR();
            CLEAR_SESSION_PANTALLA_RESPUESTA_LOGIN();
            CLEAR_SESSION_AEROLINEA_VALIDADORA();
           
        }

        #endregion

        #region " Paginacion "

        public static string getPage()
        {
            if (HttpContext.Current.Session["$sPage"] == null) return null;
            else return (string)(HttpContext.Current.Session["$sPage"]);
        }

        public static void setPage(string sPage_)
        {
            HttpContext.Current.Session.Add("$sPage", sPage_);
        }

        public static string getPageMax()
        {
            if (HttpContext.Current.Session["$sPageMax"] == null) return null;
            else return (string)(HttpContext.Current.Session["$sPageMax"]);
        }

        public static void setPageMax(string sPageMax_)
        {
            HttpContext.Current.Session.Add("$sPageMax", sPageMax_);
        }

        #endregion

        #region[WEB SERVICES LOCALES]

        public static void setCiudadesIATA(List<string> lsCiudadesIATA)
        {
            HttpContext.Current.Application.Add("$Cities", lsCiudadesIATA);
        }
        public static void setAerolineas(List<string> lsCiudadesIATA)
        {
            HttpContext.Current.Application.Add("$Aerolineas", lsCiudadesIATA);
        }
        public static List<string> getAerolineas()
        {
            if (HttpContext.Current.Application["$Aerolineas"] == null) return null;
            else return (List<string>)(HttpContext.Current.Application["$Aerolineas"]);
        }
        public static List<string> getCiudadesIATA()
        {
            if (HttpContext.Current.Application["$Cities"] == null) return null;
            else return (List<string>)(HttpContext.Current.Application["$Cities"]);
        }
        public static void setCC(List<string> lsCC)
        {
            HttpContext.Current.Application.Add("$CC", lsCC);
        }
        public static List<string> getCC()
        {
            if (HttpContext.Current.Application["$CC"] == null) return null;
            else return (List<string>)(HttpContext.Current.Application["$CC"]);
        }
        #endregion

        #region[WEB SERVICES]

        public static void setSessionIdWS(string idSessionWS_)
        {
            HttpContext.Current.Application.Add("$SessionIdWS", idSessionWS_);
        }

        public static string getSessionIdWS()
        {
            if (HttpContext.Current.Application["$SessionIdWS"] == null) return null;
            else return (string)(HttpContext.Current.Application["$SessionIdWS"]);
        }
        #endregion

        #endregion

        #region [ Cierres de Sesion ]

        #region " ..PagoRedeban "

        public static void CLEAR_SESSION_PAGOREDEBAN()
        {
            HttpContext.Current.Session.Remove("$vo_PagoRedeban");
        }

        #endregion

        #region " ..Reserva "

        public static void CLEAR_SESSION_RESERVA()
        {
            HttpContext.Current.Session.Remove("$sReserva");
        }

        public static void CLEAR_SESSION_PROYECTO()
        {
            HttpContext.Current.Session.Remove("$sProyecto");
            HttpContext.Current.Session.Remove("$sImpuestos");
            HttpContext.Current.Session.Remove("$sReserva");
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    try
                    {
                        cCache.Passenger.Clear();
                    }
                    catch { }
                    cCache.Proyecto = "0";
                    cCache.Impuestos = true;
                    cCache.Reserva = null;
                    csCache.ActualizarCache(cCache);
                }
                clsSesiones.setPantalleRespuestaLogin(null);
                clsSesiones.CLEAR_SESSION_PARAMETROS_BUSQUEDA();
            }
            catch { }
        }

        public static void CLEAR_SESSION_NUMERO_PASAJEROS()
        {
            HttpContext.Current.Session.Remove("$iNumeroPasajeros");
        }

        #endregion

        #region " ..Credenciales "

        public static void CLEAR_SESSION_CREDENCIALES()
        {
            HttpContext.Current.Session.Remove("$sCredential");
            HttpContext.Current.Session.Remove("$sLCredential");
        }
        public static void CLEAR_SESSION_CORPORATIVO()
        {
            HttpContext.Current.Session.Remove("$vo_parametros");
        }

        #endregion

        #region " ..Buscadores "


        public static void CLEAR_SESSION_PRIORITY()
        {
            HttpContext.Current.Session.Remove("$ePriority");
        }

        public static void CLEAR_SESSION_PANTALLA_RESPUESTA_LOGIN()
        {
            HttpContext.Current.Session.Remove("$pantallaLogin");
        }

        public static void CLEAR_SESSION_AEROLINEA_VALIDADORA()
        {
            HttpContext.Current.Session.Remove("$strAerolineaValida");
        }
        public static void CLEAR_SESSION_PARAMETROS_BUSQUEDA()
        {
            HttpContext.Current.Session.Remove("$vo_OTA_AirLowFareSearchLLSRQ");
            //try
            //{
            //    clsCache cCache = new csCache().cCache();
            //    cCache.VoOtaAirLowFareSearchLLSRQ = null;
            //    csCache.ActualizarCache(cCache);
            //}
            //catch { }
        }

        public static void CLEAR_SESSION_TIPO_VUELO()
        {
            HttpContext.Current.Session.Remove("$eTipoVuelo");
            try
            {
                clsCache cCache = new csCache().cCache();
                cCache.TipoVuelo = Enum_TipoVuelo.Nacional;
                csCache.ActualizarCache(cCache);
            }
            catch { }
        }

        #endregion

        #region " ..Resultados "

        public static void CLEAR_SESSION_RESULTADO_HOTEL()
        {
            HttpContext.Current.Session.Remove("$dsResultadoHotel");
            new csCacheParam().cEliminaXml("dsResultadoHotel");
        }

        public static void CLEAR_SESSION_DESCRIPCION_HOTEL()
        {
            HttpContext.Current.Session.Remove("$dsDescriptionHotel");
        }

        public static void CLEAR_SESSION_RESERVA_HOTEL()
        {
            HttpContext.Current.Session.Remove("$dsReservaHotel");
        }

        public static void CLEAR_SESSION_CONFIRMA_HOTEL()
        {
            HttpContext.Current.Session.Remove("$dsConfirmaHotel");
        }

        public static void CLEAR_SESSION_ROOM_TYPE_CODE()
        {
            HttpContext.Current.Session.Remove("$sRoomTypeCode");
        }

        public static void CLEAR_SESSION_PARAMETROS_ERROR()
        {
            HttpContext.Current.Session.Remove("$clsParametros");
        }

        #endregion

        #region " ..clsResultados "

        public static void CLEAR_SESSION_CLSRESULTADOS()
        {
            HttpContext.Current.Session.Remove("$clsResulltados");
        }

        #endregion

        #region [ ..Paginacion ]

        public static void CLEAR_SESSION_SESSIONIDWS()
        {
            HttpContext.Current.Session.Remove("$SessionIdWS");
        }

        #endregion

        #region [ ..Cache ]

        public static void CLEAR_SESSION_CACHE()
        {
            HttpContext.Current.Session.Remove("$csCache");
            new clsCacheControl().RemoverSesion();
            HttpContext.Current.Session.Remove("$csCacheWS");
            try
            {
                //HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("SessionID");
                //if (cookie != null)
                //{
                HttpCookie myCookie = new HttpCookie("SessionID", null);
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookie);
                //}
            }
            catch { }
            try
            {
                //HttpCookie cookieSsoft = HttpContext.Current.Request.Cookies.Get("UserSsoft");
                //if (cookieSsoft != null)
                //{
                HttpCookie myCookieSsoft = new HttpCookie("UserSsoft", null);
                myCookieSsoft.Expires = DateTime.Now.AddDays(-1d);
                HttpContext.Current.Response.Cookies.Add(myCookieSsoft);
                //}
            }
            catch { }
        }

        #endregion

        #region [ ..Idioma ]

        public static void CLEAR_SESSION_IDIOMA()
        {
            HttpContext.Current.Session.Remove("$sIdioma");
        }

        #endregion

        #region [ ..Aplicacion ]

        public static void CLEAR_SESSION_APLICACION()
        {
            HttpContext.Current.Session.Remove("$iAplicacionPag");
        }

        #endregion

        #region [ ..Conexion ]

        public static void CLEAR_SESSION_CONEXION()
        {
            HttpContext.Current.Session.Remove("$sConexion");
        }

        #endregion

        #region [ ..MySabre ]

        public static void CLEAR_SESSION_MYSABRE()
        {
            HttpContext.Current.Session.Remove("$vAutenticationMySabre");
            HttpContext.Current.Session.Remove("$dsMySabre");
        }

        #endregion

        #endregion

        #region [ Otros ]

        public static void SetDatasetSabreAir(DataSet dsSabreAir)
        {
            HttpContext.Current.Session.Add("$dsSabreAir", dsSabreAir);
            new csCacheParam().cGuardaDsResult(dsSabreAir, "dsSabreAir");
        }
        public static DataSet GetDatasetSabreAir()
        {
            DataSet dsSabreAir = null;
            try
            {
                if (HttpContext.Current.Session["$dsSabreAir"] == null)
                {
                    try
                    {
                        dsSabreAir = new csCacheParam().cRecuperaDsResult("dsSabreAir");
                    }
                    catch { }
                }
                else
                {
                    dsSabreAir = (DataSet)(HttpContext.Current.Session["$dsSabreAir"]);
                }
                if (dsSabreAir != null)
                    SetDatasetSabreAir(dsSabreAir);
            }
            catch { }
            return dsSabreAir;
        }
        public static void SetDataTameAir(DataSet dsTameAir)
        {
            HttpContext.Current.Session.Add("$dsTameAir", dsTameAir);
        }
        public static DataSet GetDataTameAir()
        {
            DataSet dsTameAir = null;
            try
            {
                if (HttpContext.Current.Session["$dsTameAir"] != null)
                {
                    dsTameAir = (DataSet)(HttpContext.Current.Session["$dsTameAir"]);
                }
                if (dsTameAir != null)
                    SetDataTameAir(dsTameAir);
            }
            catch { }
            return dsTameAir;
        }

        public static void SetDatasetSelectSabreAir(DataSet dsSelectSabreAir)
        {
            HttpContext.Current.Session.Add("$dsSelectSabreAir", dsSelectSabreAir);
            new csCacheParam().cGuardaDsResult(dsSelectSabreAir, "dsSelectSabreAir");
        }
        public static DataSet GetDatasetSelectSabreAir()
        {
            DataSet dsSabreAir = null;
            try
            {
                if (HttpContext.Current.Session["$dsSelectSabreAir"] == null)
                {
                    try
                    {
                        dsSabreAir = new csCacheParam().cRecuperaDsResult("dsSelectSabreAir");
                    }
                    catch { }
                }
                else
                {
                    dsSabreAir = (DataSet)(HttpContext.Current.Session["$dsSelectSabreAir"]);
                }
                if (dsSabreAir != null)
                    SetDatasetSelectSabreAir(dsSabreAir);
            }
            catch { }
            return dsSabreAir;
        }

        public static void SET_USD_SABRE(string UsdTa_)
        {
            HttpContext.Current.Session.Add("$UsdTa", UsdTa_);
        }

        public static string GET_USD_SABRE()
        {
            if (HttpContext.Current.Session["$UsdTa"] == null) return null;
            else return (string)(HttpContext.Current.Session["$UsdTa"]);
        }
        public static string GET_RECORD()
        {
            if (HttpContext.Current.Session["$Record"] == null) return null;
            else return (string)(HttpContext.Current.Session["$Record"]);
        }

        public static void SET_RECORD(string Record_)
        {
            HttpContext.Current.Session.Add("$Record", Record_);
        }

        public static bool GET__AIR()
        {
            if (HttpContext.Current.Session["$bAir"] == null) return false;
            else return (bool)(HttpContext.Current.Session["$bAir"]);
        }
        public static void SET__AIR(bool bAir)
        {
            HttpContext.Current.Session.Add("$bAir", bAir);
        }

        public static DateTime GET_TICKETE()
        {
            if (HttpContext.Current.Session["$Tickete"] == null) return DateTime.Now;
            else return (DateTime)(HttpContext.Current.Session["$Tickete"]);
        }

        public static void SET_TICKETE(DateTime Tickete_)
        {
            HttpContext.Current.Session.Add("$Tickete", Tickete_);
        }

        public static void CLEAR_SESSION_DSSABREAIR()
        {
            HttpContext.Current.Session.Remove("$dsSabreAir");
            new csCacheParam().cEliminaXml("dsSabreAir");
        }

        public static void CLEAR_SESSION_AIR()
        {
            //HttpContext.Current.Session.Remove("$dsSabreAir");
            HttpContext.Current.Session.Remove("$vo_OTA_AirBookRQ");
            HttpContext.Current.Session.Remove("$vo_OTA_AirLowFareSearchLLSRQ");

            HttpContext.Current.Session.Remove("$Proceso");
            HttpContext.Current.Session.Remove("$Datos");
            HttpContext.Current.Session.Remove("$IsData");
            HttpContext.Current.Session.Remove("$Orden");
            HttpContext.Current.Session.Remove("$ListActual");
            HttpContext.Current.Session.Remove("$ListSelect");
            HttpContext.Current.Session.Remove("$ListBargainFinderActual");
            HttpContext.Current.Session.Remove("$ListBargainFinderAnterior");
            HttpContext.Current.Session.Remove("$ListBargainFinderSiguiente");
            HttpContext.Current.Session.Remove("$ListResultadosActual");
            HttpContext.Current.Session.Remove("$ListResultadosAnterior");
            HttpContext.Current.Session.Remove("$ListResultadosSiguiente");
            HttpContext.Current.Session.Remove("$IsSearch");
            HttpContext.Current.Session.Remove("$Tarifas");
            HttpContext.Current.Session.Remove("$RPH");
            HttpContext.Current.Session.Remove("$Pasajeros");
            HttpContext.Current.Session.Remove("$Impuestos");
            HttpContext.Current.Session.Remove("$ValorViaje");
            HttpContext.Current.Session.Remove("$Documentacion");
            HttpContext.Current.Session.Remove("$Tickete");
            HttpContext.Current.Session.Remove("$Record");
            HttpContext.Current.Session.Remove("$dsSelectSabreAir");
            HttpContext.Current.Session.Remove("$dsSabreAir");
            new csCacheParam().cEliminaXml("dsSabreAir");

            new csCacheParam().cEliminaXml("vo_OTA_AirLowFareSearchLLSRQ");
        }

        public static DataSet getParametrosCarList()
        {
            if (HttpContext.Current.Session["$ds_DatasetCar"] == null) return null;
            else return (DataSet)HttpContext.Current.Session["$ds_DatasetCar"];
        }

        public static void setParametrosCarList(DataSet Datos_)
        {
            HttpContext.Current.Session.Add("$ds_DatasetCar", Datos_);
        }

        public static DataSet getParametrosCarDet()
        {
            if (HttpContext.Current.Session["$ds_DatasetCarDet"] == null) return null;
            else return (DataSet)HttpContext.Current.Session["$ds_DatasetCarDet"];
        }

        public static void setParametrosCarDet(DataSet Datos_)
        {
            HttpContext.Current.Session.Add("$ds_DatasetCarDet", Datos_);
        }

        public static DateTime GET_DTMFECHA_LIMITE()
        {
            if (HttpContext.Current.Session["$FECHALIMITE"] == null) return DateTime.Now;
            else return (DateTime)(HttpContext.Current.Session["$FECHALIMITE"]);
        }
        public static void SET_DTMFECHA_LIMITE(DateTime Tickete_)
        {
            HttpContext.Current.Session.Add("$FECHALIMITE", Tickete_);
        }
        public static void SET_COMMANDARGUMENT(string Command)
        {
            HttpContext.Current.Session.Add("$CommandArgument", Command);
        }
        public static string GET_COMMANDARGUMENT()
        {
            if (HttpContext.Current.Session["$CommandArgument"] == null) return null;
            else return (string)HttpContext.Current.Session["$CommandArgument"];
        }

        public static void SetTablaBeneficios(DataTable tblBeneficios)
        {
            HttpContext.Current.Session.Add("$TablaBeneficios", tblBeneficios);
        }
        public static DataTable GetTablaBeneficios()
        {
            if (HttpContext.Current.Session["$TablaBeneficios"] == null) return null;
            else return (System.Data.DataTable)(HttpContext.Current.Session["$TablaBeneficios"]);
        }


        #endregion

        #region [ METODOS_HOTELBEDS ]

        #region " Hotel "

        public static VO_HotelValuedAvailRQ getParametrosHotel()
        {
            VO_HotelValuedAvailRQ vo_HotelValuedAvailRQ = null;
            try
            {
                if (HttpContext.Current.Session["$vo_HotelValuedAvailRQ"] == null)
                {
                    try
                    {
                        vo_HotelValuedAvailRQ = new csCacheParam().cRecuperaParamHot();
                    }
                    catch { }
                }
                else
                {
                    vo_HotelValuedAvailRQ = (VO_HotelValuedAvailRQ)(HttpContext.Current.Session["$vo_HotelValuedAvailRQ"]);
                }
                if (vo_HotelValuedAvailRQ != null)
                    setParametrosHotel(vo_HotelValuedAvailRQ);
            }
            catch { }
            return vo_HotelValuedAvailRQ;
        }

        public static void setParametrosHotel(VO_HotelValuedAvailRQ Datos_)
        {
            HttpContext.Current.Session.Add("$vo_HotelValuedAvailRQ", Datos_);
            new csCacheParam().cGuardaParamHot(Datos_);
        }

        public static VO_HotelDetailRQ getDetalleHotel()
        {
            if (HttpContext.Current.Session["$vo_HotelDetailRQ"] == null) return null;
            else return (VO_HotelDetailRQ)HttpContext.Current.Session["$vo_HotelDetailRQ"];
        }

        public static void setDetalleHotel(VO_HotelDetailRQ Datos_)
        {
            HttpContext.Current.Session.Add("$vo_HotelDetailRQ", Datos_);
        }

        public static VO_ServiceAddRQ getServiceAdd()
        {
            if (HttpContext.Current.Session["$vo_ServiceAddRQ"] == null) return null;
            else return (VO_ServiceAddRQ)HttpContext.Current.Session["$vo_ServiceAddRQ"];
        }

        public static void setServiceAdd(VO_ServiceAddRQ Datos_)
        {
            HttpContext.Current.Session.Add("$vo_ServiceAddRQ", Datos_);
        }

        public static VO_PurchaseConfirmRQ getPurchaseConfirm()
        {
            if (HttpContext.Current.Session["$vo_PurchaseConfirmRQ"] == null) return null;
            else return (VO_PurchaseConfirmRQ)HttpContext.Current.Session["$vo_PurchaseConfirmRQ"];
        }

        public static void setPurchaseConfirm(VO_PurchaseConfirmRQ Datos_)
        {
            HttpContext.Current.Session.Add("$vo_PurchaseConfirmRQ", Datos_);
        }

        public static VO_PurchaseReference getPurchaseReference()
        {
            if (HttpContext.Current.Session["$vo_PurchaseReference"] == null) return null;
            else return (VO_PurchaseReference)HttpContext.Current.Session["$vo_PurchaseReference"];
        }

        public static void setPurchaseReference(VO_PurchaseReference Datos_)
        {
            HttpContext.Current.Session.Add("$vo_PurchaseReference", Datos_);
        }

        #endregion

        #region [CLEAR SESSION]
        public static void CLEAR_SESSION_CAR()
        {
            HttpContext.Current.Session.Remove("$ds_DatasetCar");
            HttpContext.Current.Session.Remove("$ds_DatasetCarDet");
        }

        public static void CLEAR_SESSION_HOT()
        {
            HttpContext.Current.Session.Remove("$vo_HotelValuedAvailRQ");
            HttpContext.Current.Session.Remove("$vo_HotelDetailRQ");
            HttpContext.Current.Session.Remove("$vo_PurchaseReference");
            HttpContext.Current.Session.Remove("$vo_PurchaseConfirmRQ");
            HttpContext.Current.Session.Remove("$vo_ServiceAddRQ");
        }

        #endregion

        #endregion

        #region " MySabre "

        public static VO_AutenticationMySabre getAutenticationMySabre()
        {
            if (HttpContext.Current.Session["$vAutenticationMySabre"] == null) return null;
            else return (VO_AutenticationMySabre)(HttpContext.Current.Session["$vAutenticationMySabre"]);
        }

        public static void setAutenticationMySabre(VO_AutenticationMySabre Datos_)
        {
            HttpContext.Current.Session.Add("$vAutenticationMySabre", Datos_);
        }

        public static DataSet getDsMySabre()
        {
            if (HttpContext.Current.Session["$dsMySabre"] == null) return null;
            else return (DataSet)(HttpContext.Current.Session["$dsMySabre"]);
        }

        public static void setDsMySabre(DataSet Datos_)
        {
            HttpContext.Current.Session.Add("$dsMySabre", Datos_);
        }

        #endregion

        #region [ Sabre ]
        #region [ METODOS ]
        public static void SET_PARAMETRO(string sParametros)
        {
            HttpContext.Current.Session.Add("$sParametros", sParametros);
        }

        public static List<VO_OTA_AirLowFareSearchLLSRS> getResultadosBargain()
        {
            if (HttpContext.Current.Session["$lvo_OTA_AirLowFareSearchLLSRS"] == null) return null;
            else return (List<VO_OTA_AirLowFareSearchLLSRS>)(HttpContext.Current.Session["$lvo_OTA_AirLowFareSearchLLSRS"]);
        }

        public static void setResultadosBargain(List<VO_OTA_AirLowFareSearchLLSRS> lvo_ResultBargainFinderPlus)
        {
            HttpContext.Current.Session.Add("$lvo_OTA_AirLowFareSearchLLSRS", lvo_ResultBargainFinderPlus);
        }

        public static VO_OTA_AirLowFareSearchLLSRQ getParametrosAirBargain()
        {
            VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ = null;
            try
            {
                if (HttpContext.Current.Session["$vo_OTA_AirLowFareSearchLLSRQ"] == null)
                {
                    try
                    {
                        vo_OTA_AirLowFareSearchLLSRQ = new csCacheParam().cRecuperaParamAir();
                    }
                    catch { }
                }
                else
                {
                    vo_OTA_AirLowFareSearchLLSRQ = (VO_OTA_AirLowFareSearchLLSRQ)(HttpContext.Current.Session["$vo_OTA_AirLowFareSearchLLSRQ"]);
                }
                if (vo_OTA_AirLowFareSearchLLSRQ != null)
                    setParametrosAirBargain(vo_OTA_AirLowFareSearchLLSRQ);
            }
            catch { }
            return vo_OTA_AirLowFareSearchLLSRQ;
        }

        public static void setParametrosAirBargain(VO_OTA_AirLowFareSearchLLSRQ vo_OTA_AirLowFareSearchLLSRQ)
        {
            HttpContext.Current.Session.Add("$vo_OTA_AirLowFareSearchLLSRQ", vo_OTA_AirLowFareSearchLLSRQ);
            new csCacheParam().cGuardaParamAir(vo_OTA_AirLowFareSearchLLSRQ);
        }

        public static List<VO_TarifaPago> getTarifaPagoAir()
        {
            if (HttpContext.Current.Session["$lvo_TarifaPago"] == null) return null;
            else return (List<VO_TarifaPago>)(HttpContext.Current.Session["$lvo_TarifaPago"]);
        }

        public static void setTarifaPagoAir(List<VO_TarifaPago> lvo_TarifaPago)
        {
            HttpContext.Current.Session.Add("$lvo_TarifaPago", lvo_TarifaPago);
        }
        public static void setDetallesAir(List<VO_SegmentoAereo> lvo_SegmentoAereo)
        {
            HttpContext.Current.Session.Add("$lvo_SegmentoAereo", lvo_SegmentoAereo);
        }

        public static List<VO_DataTravelItineraryAddInfo> GET_LVO_DataTravelItineraryAddInfo()
        {
            List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo = null;
            try
            {
                if (HttpContext.Current.Session["$lvo_DataTravelItineraryAddInfo"] != null)
                {
                    lvo_DataTravelItineraryAddInfo = (List<VO_DataTravelItineraryAddInfo>)(HttpContext.Current.Session["$lvo_DataTravelItineraryAddInfo"]);
                    SET_LVO_DataTravelItineraryAddInfo(lvo_DataTravelItineraryAddInfo);
                }
            }
            catch { }
            return lvo_DataTravelItineraryAddInfo;
        }

        public static void SET_LVO_DataTravelItineraryAddInfo(List<VO_DataTravelItineraryAddInfo> lvo_DataTravelItineraryAddInfo)
        {
            HttpContext.Current.Session.Add("$lvo_DataTravelItineraryAddInfo", lvo_DataTravelItineraryAddInfo);
        }

        public static VO_OTA_AirBookRQ getParametrosAirHoras()
        {
            if (HttpContext.Current.Session["$vo_OTA_AirBookRQ"] == null) return null;
            else return (VO_OTA_AirBookRQ)(HttpContext.Current.Session["$vo_OTA_AirBookRQ"]);
        }
        public static void setParametrosAirHoras(VO_OTA_AirBookRQ vo_OTA_AirBookRQ)
        {
            HttpContext.Current.Session.Add("$vo_OTA_AirBookRQ", vo_OTA_AirBookRQ);
        }

        public static bool GET_LOAD_PASAJERO()
        {
            if (HttpContext.Current.Session["$bLoadPasajero"] == null) return false;
            else return (bool)(HttpContext.Current.Session["$bLoadPasajero"]);
        }

        public static void SET_LOAD_PASAJERO(bool bLoadPasajero)
        {
            HttpContext.Current.Session.Add("$bLoadPasajero", bLoadPasajero);
        }

        #endregion
       
     
        #endregion
        #region " Pasajeros "

        public static List<VO_Passenger> getPassenger()
        {
            if (HttpContext.Current.Session["$vo_Passenger"] == null)
            {
                clsCache cCache = new csCache().cCache();
                if (cCache.Passenger != null)
                {
                    setPassenger(cCache.Passenger);
                    return (List<VO_Passenger>)(HttpContext.Current.Session["$vo_Passenger"]);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return (List<VO_Passenger>)(HttpContext.Current.Session["$vo_Passenger"]);
            }

           
        }
        public static void setPassenger(List<VO_Passenger> cslPassenger_)
        {
            HttpContext.Current.Session.Add("$vo_Passenger", cslPassenger_);
        }

        #endregion
     
        #region [ DESTRUCTOR ]

        ~clsSesiones() { }

        #endregion
    }
}
