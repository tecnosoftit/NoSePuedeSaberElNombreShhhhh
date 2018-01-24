using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;

public class AutenticacionSabre
{
    private static bool isConected;
    private static string sSession;

    #region [ CONSTRUCTOR ]

    public AutenticacionSabre() { isConected = false; sSession = null; }

    #endregion

    #region [ METODOS ]
    public static bool IsConected
    {
        get
        {
            if (sSession != null)
                isConected = true;
            return isConected;
        }
    }
    public static string GET_SabreSession()
    {
        if (HttpContext.Current.Session["SesionSabre"] == null)
        {
            sSession = null;
            sSession = Negocios_WebServiceSession._CrearSesion();
            SET_SabreSession(sSession);
            return sSession;
        }
        else return HttpContext.Current.Session["SesionSabre"].ToString();
    }
    public static string GET_SabreSessionValida()
    {
        if (HttpContext.Current.Session["SesionSabre"] != null)
            return HttpContext.Current.Session["SesionSabre"].ToString();
        else
            return null;
    }
    public static void SET_SabreSession(string Session_)
    {
        HttpContext.Current.Session.Add("SesionSabre", Session_);
        HttpContext.Current.Session.Timeout = 10;
    }

    #endregion

    #region [ DESTRUCTOR ]

    ~AutenticacionSabre() { }

    #endregion
}
