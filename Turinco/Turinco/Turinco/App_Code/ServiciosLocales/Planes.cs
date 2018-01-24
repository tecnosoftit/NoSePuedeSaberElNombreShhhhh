using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Data;
using AjaxControlToolkit;
using Ssoft.Rules.Generales;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using Ssoft.Rules;
using Ssoft.Rules.WebServices;
using Ssoft.Utils;
using Ssoft.Rules.Interface;
using Ssoft.Rules.Administrador;
using Ssoft.Rules.Pagina;
using SsoftQuery.Vuelos;

/// <summary>
/// Descripción breve de Planes
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class Planes : System.Web.Services.WebService
{

    public Planes()
    {

    }

    #region [METODOS]

 
 

    #region [Metodos Estaticos]
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// Autor:
    /// Fecha Creacion:
    /// Ultima Modificacion
    /// Autor ultima Modificacion
    private static string Conexion()
    {
        return ConfigurationManager.AppSettings["strConexion"];
    }

    private static string GetKey(string strKey)
    {
        return ConfigurationManager.AppSettings[strKey];
    }

    #endregion


   
    #endregion    

}

