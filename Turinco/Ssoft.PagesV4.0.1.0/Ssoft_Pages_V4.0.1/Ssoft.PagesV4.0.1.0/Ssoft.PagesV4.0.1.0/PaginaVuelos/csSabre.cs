using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using Ssoft.Utils;
using Ssoft.ValueObjects;
using Ssoft.ManejadorExcepciones;

namespace Ssoft.Pages
{
    public class csSabre : TemplateControl
    {
        public void setTicket(UserControl PageSource)
        {
            clsParametros cParametros = new clsParametros();
            clsResultados cResultados = new clsResultados();
            try
            {
                    string[] sValue = csValue(PageSource);
                    if (!sValue[0].Equals("0"))
                    {
                        cResultados = csWebServices.TicketReservaSabre(sValue[0].ToString());
                        if (!cResultados.Error.Id.Equals(0))
                        {

                        }
                    }
            }
            catch (Exception Ex)
            {
                cParametros.Id = 0;
                cParametros.Message = Ex.Message.ToString();
                cParametros.Source = Ex.Source.ToString();
                cParametros.Tipo = clsTipoError.Library;
                cParametros.Severity = clsSeveridad.Moderada;
                cParametros.StackTrace = Ex.StackTrace.ToString();
                cParametros.Complemento = "Modificacion Usuarios ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                new csCache().setError(PageSource, cParametros);
            }
        }
        private string[] csValue(UserControl PageSource)
        {
            string[] sValue = new string[1];
            try
            {
                if (PageSource.Request.QueryString["Record"] != null)
                {
                    sValue[0] = PageSource.Request.QueryString["Record"].ToString();
                }
                else
                {
                    sValue[0] = "0";
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
            return sValue;
        }
    }
}
