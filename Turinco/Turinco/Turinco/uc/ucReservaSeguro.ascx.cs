using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ssoft.Pages.PaginaPlanes;
using System.Data;
using Ssoft.Rules.Reservas;
using Ssoft.Utils;
using Ssoft.ManejadorExcepciones;

public partial class uc_ucReservaSeguro : System.Web.UI.UserControl
{
    csReservaPlanes csResultadoPlanes = new csReservaPlanes();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            csResultadoPlanes.SetFormularioReservaCotizador(this);
           
        }
    }
    protected void EliminarConstraints(ref DataSet ds)
    {
        ds.Tables["tblTarifa"].Constraints.Clear();
        ds.Tables["tblTax"].Constraints.Clear();
        ds.Tables["tblPax"].Constraints.Clear();
        ds.Tables["tblTransac"].Constraints.Clear();
        ds.Tables["tblreserva"].Constraints.Clear();
        ds.Tables["tblHabitaciones"].Constraints.Clear();
        ds.Tables["TablaDatosVisibles"].Constraints.Clear();
        ds.Tables["CarritoCompras"].Constraints.Clear();
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        csCarrito cCar = new csCarrito("Reserva" + new csCache().cCache().SessionID, "CarritoCompras");
        cCar.LimpiarCarrito();
        clsValidaciones.RedirectPaginaIni("Planes.aspx", true);
    }
    protected void btnReservar_Click(object sender, EventArgs e)
    {
        AjaxControlToolkit.ModalPopupExtender mpterminos = (AjaxControlToolkit.ModalPopupExtender)Parent.FindControl("MPEEBanner");

        
        if (rblFormasPago.Items.Count > 0)
        {
            
                if (!cbAcepto.Checked)
                {                   
                    lblError.Text = "Por favor acepta los terminos y condicones";
                    return;
                }
                else
                {
                    clsParametros Registro = csResultadoPlanes.setCrearNoRegistro(this, ucRegistro, Enum_Login.LoginGen, false);
                    if (Registro.Id != 0)
                    {
                        csResultadoPlanes.btnReservarCotizador_Click(sender, e, this);
                        if (this.Session["$CodigoReservaPlan"] != null)
                        {
                            
                            lblRecord.Text = this.Session["$CodigoReservaPlan"].ToString();
                            if (mpterminos != null)
                            {
                                mpterminos.Hide();
                            }

                            MPEEConfirm.Show();
                        }
                    }
                }
            
        }
       

    }
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        csCarrito cCar = new csCarrito("Reserva" + new csCache().cCache().SessionID, "CarritoCompras");
        cCar.LimpiarCarrito();
        //clsValidaciones.RedirectPaginaIni("IndexInterno.aspx",true);
        clsValidaciones.RedirectPaginaIni("Index.aspx", true);
    }
    protected void rblFormasPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("EFE"))
        {
            DivTC.Style.Add("display", "none");
            DivPSE.Style.Add("display", "none");
            DivEfe.Style.Add("display", "block");
            DivContAsesor.Style.Add("display", "none");
        }
        else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("TC"))
        {
            DivTC.Style.Add("display", "block");
            DivPSE.Style.Add("display", "none");
            DivEfe.Style.Add("display", "none");
            DivContAsesor.Style.Add("display", "none");
        }
        else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("PSE"))
        {
            DivTC.Style.Add("display", "none");
            DivPSE.Style.Add("display", "block");
            DivEfe.Style.Add("display", "none");
            DivContAsesor.Style.Add("display", "none");
        }
        else if (rblFormasPago.SelectedValue.ToString().ToUpper().Equals("NAP"))
        {
            DivTC.Style.Add("display", "none");
            DivPSE.Style.Add("display", "none");
            DivEfe.Style.Add("display", "none");
            DivContAsesor.Style.Add("display", "block");
        }
    }
}