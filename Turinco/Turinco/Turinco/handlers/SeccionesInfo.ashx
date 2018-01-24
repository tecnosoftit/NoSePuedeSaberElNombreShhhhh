<%@ WebHandler Language="C#" Class="SeccionesInfo" %>

using System;
using System.Web;
using SsoftQuery.SeccionesInformativas;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
public class SeccionesInfo : IHttpHandler
{

    [DataContract]
    class Results
    {
        [DataMember]
        public string link { get; set; }
        [DataMember]
        public string urlImage { get; set; }
    }

    public void ProcessRequest(HttpContext context)
    {
        csConsultaSecciones secInformativa = new csConsultaSecciones();
        DataSet dsSeccionCarrusel = secInformativa.ConsultaSeccionesGeneral(null, null, "357", null, null, "3", null, null);

        List<Results> results = new List<Results>();
        for (int g = 0; g < dsSeccionCarrusel.Tables[0].Rows.Count; g++)
        {
            DataSet dsLinks = secInformativa.ConsultaRelacionesSecciones(dsSeccionCarrusel.Tables[0].Rows[g]["intCodigo"].ToString(), "1");

            for (int k = 0; k < dsLinks.Tables[0].Rows.Count; k++)
            {
                results.Add(new Results
                {
                    link = dsLinks.Tables[0].Rows[k]["strurl"].ToString(),
                    urlImage = dsLinks.Tables[0].Rows[k]["strImagenLink"].ToString(),
                });
            }
        }
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Results>));
        context.Response.ContentType = "application/json;charset=utf-8";
        serializer.WriteObject(context.Response.OutputStream, results);

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}