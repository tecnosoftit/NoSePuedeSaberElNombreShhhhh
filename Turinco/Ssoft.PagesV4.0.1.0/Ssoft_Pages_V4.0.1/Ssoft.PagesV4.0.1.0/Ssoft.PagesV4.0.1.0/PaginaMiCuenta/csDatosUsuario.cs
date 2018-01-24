using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using SsoftQuery.Usuarios;
using Ssoft.Utils;
using SsoftQuery.Generales;
using Ssoft.Rules.Generales;

namespace Ssoft.Pages.PaginaMiCuenta
{
    public class csDatosUsuario
    {
        private static string sFormatoFecha = clsSesiones.getFormatoFecha();
        private static string sFormatoFechaBD = clsSesiones.getFormatoFechaBD();

        public void consulta_usuario(Repeater rptUsuario, string Usuario)
        {
            DataTable dtUsuarios = new DataTable();
            csConsultasUsuarios Usuarios = new csConsultasUsuarios();
            dtUsuarios = Usuarios.consulta_usuario(Usuario);
            if (dtUsuarios != null)
            {
                rptUsuario.DataSource = dtUsuarios;
                rptUsuario.DataBind();

                CargarDatosUsuario(rptUsuario);
                SeleccionarDatosUsuario(rptUsuario, dtUsuarios);
            }
        }

        public void cambiar_contrasenia(string Contrasenia, Label lblError)
        {
            clsCache cCache = new csCache().cCache();
            csConsultasUsuarios Usuarios = new csConsultasUsuarios();
            DataTable tblDatos = Usuarios.modificar_contrasenia_usuario(cCache.Contacto, Contrasenia);
            if (tblDatos != null)
            {
                int iFilasAfectadas = Convert.ToInt32(tblDatos.Rows[0][0].ToString());
                if (iFilasAfectadas > 0)
                    lblError.Text = "Tu contraseña fue modificada con éxito!";
                else
                    lblError.Text = "Tu contraseña no pudo ser modificada";
            }
            else
                lblError.Text = "Tu contraseña no pudo ser modificada";
        }

        public void CargarDatosUsuario(Repeater rptUsuario)
        {
            try
            {
                DropDownList ddlTipoIdent = null;
                RadioButtonList rblSexo = null;
                DropDownList ddlPais = null;

                DataTable dtResultadosIdent = null;
                DataTable dtResultadosGenero = null;
                DataTable dtResultadosPaises = null;
                csConsultasGenerales Generales = new csConsultasGenerales();
                csGenerales csRefere = new csGenerales();

                dtResultadosIdent = Generales.ConReferenciaTiposIdentificacion();
                dtResultadosGenero = Generales.ConReferenciaSexo();
                dtResultadosPaises = Generales.listado_paises();

                for (int i = 0; i < rptUsuario.Items.Count; i++)
                {
                    ddlTipoIdent = (DropDownList)rptUsuario.Items[i].FindControl("ddlTipoIdent");
                    if (ddlTipoIdent != null)
                    {
                        if (dtResultadosIdent != null)
                            csRefere.LlenarControlData(ddlTipoIdent, Enum_Controls.DropDownList, "intCode", "strDescripcion", true, false, null, dtResultadosIdent);
                    }

                    rblSexo = (RadioButtonList)rptUsuario.Items[i].FindControl("rblSexo");
                    if (rblSexo != null)
                    {
                        if (dtResultadosGenero != null)
                            csRefere.LlenarControlData(rblSexo, Enum_Controls.RadioButtonList, "intCode", "strDescripcion", true, false, null, dtResultadosGenero);
                    }

                    ddlPais = (DropDownList)rptUsuario.Items[i].FindControl("ddlPais");
                    if (ddlPais != null)
                    {
                        if (dtResultadosPaises != null)
                            csRefere.LlenarControlData(ddlPais, Enum_Controls.DropDownList, "intCode", "strDescripcion", true, false, null, dtResultadosPaises);
                    }
                }
            }
            catch { }
        }

        public void SeleccionarDatosUsuario(Repeater rptUsuario, DataTable tblUsuario)
        {
            try
            {
                DropDownList ddlTipoIdent = null;
                RadioButtonList rblSexo = null;
                DropDownList ddlPais = null;
                DropDownList ddlCiudad = null;
                
                for (int i = 0; i < rptUsuario.Items.Count; i++)
                {
                    ddlTipoIdent = (DropDownList)rptUsuario.Items[i].FindControl("ddlTipoIdent");
                    if (ddlTipoIdent != null)
                        ddlTipoIdent.SelectedValue = tblUsuario.Rows[i]["intTipoIdent"].ToString();

                    rblSexo = (RadioButtonList)rptUsuario.Items[i].FindControl("rblSexo");
                    if (rblSexo != null)
                        rblSexo.SelectedValue = tblUsuario.Rows[i]["intGenero"].ToString();

                    ddlPais = (DropDownList)rptUsuario.Items[i].FindControl("ddlPais");
                    if (ddlPais != null)
                        ddlPais.SelectedValue = tblUsuario.Rows[i]["intCodigoPais"].ToString();

                    ddlCiudad = (DropDownList)rptUsuario.Items[i].FindControl("ddlCiudad");
                    if (ddlCiudad != null)
                    {
                        datos_ciudades_pais(ddlCiudad, ddlPais.SelectedValue);
                        ddlCiudad.SelectedValue = tblUsuario.Rows[i]["intCiudad"].ToString();
                    }
                }
            }
            catch { }
        }

        public void datos_ciudades_pais(DropDownList ddlCiudades, string codePais)
        {
            try
            {
                DataTable dtResultados = new DataTable();
                csConsultasGenerales Ubicacion = new csConsultasGenerales();
                csGenerales csRefere = new csGenerales();
                dtResultados = Ubicacion.listado_ciudades_pais(codePais);
                if (dtResultados != null)
                    csRefere.LlenarControlData(ddlCiudades, Enum_Controls.DropDownList, "intCode", "strDescription", true, false, null, dtResultados);
            }
            catch { }
        }

        public void ciudades_pais(Repeater rptUsuario)
        {
            try
            {
                DropDownList ddlPais = null;
                DropDownList ddlCiudad = null;

                for (int i = 0; i < rptUsuario.Items.Count; i++)
                {
                    ddlPais = (DropDownList)rptUsuario.Items[i].FindControl("ddlPais");
                    if (ddlPais != null)
                    {
                        ddlCiudad = (DropDownList)rptUsuario.Items[i].FindControl("ddlCiudad");
                        if (ddlCiudad != null)
                        {
                            datos_ciudades_pais(ddlCiudad, ddlPais.SelectedValue);
                        }
                    }
                }
            }
            catch { }
        }

        public void actualizar_usuario(Repeater rptUsuario, Label lblError)
        {
            try
            {
                bool bGuardar = true;
                TextBox txtNombre= null;
                TextBox txtApellido= null;
                DropDownList ddlTipoIdent= null;
                TextBox txtIdentificacion= null;
                RadioButtonList rblSexo= null;
                TextBox txtNacimiento= null;
                TextBox txtDireccion= null;
                TextBox txtTel= null;
                TextBox txtCel= null;
                DropDownList ddlPais = null;
                DropDownList ddlCiudad = null;

                for (int i = 0; i < rptUsuario.Items.Count; i++)
                {
                    #region Validacion vacios
                    txtNombre = (TextBox)rptUsuario.Items[i].FindControl("txtNombre");
                    if (txtNombre != null)
                    {
                        if (txtNombre.Text.Trim().Equals(""))
                           bGuardar = false;
                    }

                    txtApellido = (TextBox)rptUsuario.Items[i].FindControl("txtApellido");
                    if (txtApellido != null)
                    {
                        if (txtApellido.Text.Trim().Equals(""))
                            bGuardar = false;
                    }

                    ddlTipoIdent = (DropDownList)rptUsuario.Items[i].FindControl("ddlTipoIdent");
                    if (ddlTipoIdent != null)
                    {
                        if (ddlTipoIdent.SelectedValue.Equals("") || ddlTipoIdent.SelectedValue.Equals("0"))
                            bGuardar = false;
                    }

                    txtIdentificacion = (TextBox)rptUsuario.Items[i].FindControl("txtIdentificacion");
                    if (txtIdentificacion != null)
                    {
                        if (txtIdentificacion.Text.Trim().Equals(""))
                            bGuardar = false;
                    }

                    rblSexo = (RadioButtonList)rptUsuario.Items[i].FindControl("rblSexo");
                    if (rblSexo != null)
                    {
                        if (rblSexo.SelectedValue.Equals("") || rblSexo.SelectedValue.Equals("0"))
                            bGuardar = false;
                    }

                    txtNacimiento = (TextBox)rptUsuario.Items[i].FindControl("txtNacimiento");
                    if (txtNacimiento != null)
                    {
                        if (txtNacimiento.Text.Trim().Equals(""))
                            bGuardar = false;
                    }

                    txtDireccion = (TextBox)rptUsuario.Items[i].FindControl("txtDireccion");
                    if (txtDireccion != null)
                    {
                        if (txtDireccion.Text.Trim().Equals(""))
                            bGuardar = false;
                    }

                    txtTel = (TextBox)rptUsuario.Items[i].FindControl("txtTel");
                    if (txtTel != null)
                    {
                        if (txtTel.Text.Trim().Equals(""))
                            bGuardar = false;
                    }

                    txtCel = (TextBox)rptUsuario.Items[i].FindControl("txtCel");
                    if (txtCel != null)
                    {
                        if (txtCel.Text.Trim().Equals(""))
                            bGuardar = false;
                    }

                    ddlPais = (DropDownList)rptUsuario.Items[i].FindControl("ddlPais");
                    if (ddlPais != null)
                    {
                        if (ddlPais.SelectedValue.Equals("") || ddlPais.SelectedValue.Equals("0"))
                            bGuardar = false;
                    }

                    ddlCiudad = (DropDownList)rptUsuario.Items[i].FindControl("ddlCiudad");
                    if (ddlCiudad != null)
                    {
                        if (ddlCiudad.SelectedValue.Equals("") || ddlCiudad.SelectedValue.Equals("0"))
                            bGuardar = false;
                    }
                    #endregion
                }

                if (!bGuardar)
                    lblError.Text = "Por favor completa toda la informacion";
                else
                {
                    clsCache cCache = new csCache().cCache();
                    csConsultasUsuarios Usuario = new csConsultasUsuarios();
                    string sFechaNacimiento = txtNacimiento.Text;
                    try
                    {
                        sFechaNacimiento = clsValidaciones.ConverFecha(sFechaNacimiento, sFormatoFecha, sFormatoFechaBD);
                    }
                    catch { }

                    DataTable dtResultados = Usuario.modificar_datos_usuario(cCache.Contacto, txtNombre.Text, txtApellido.Text, ddlTipoIdent.SelectedValue,
                        txtIdentificacion.Text, rblSexo.SelectedValue, sFechaNacimiento, txtDireccion.Text, txtTel.Text, txtCel.Text, ddlCiudad.SelectedValue);
                    if (dtResultados != null)
                    {
                        int iFilasAfectadas = Convert.ToInt32(dtResultados.Rows[0][0].ToString());
                        if (iFilasAfectadas > 0)
                            lblError.Text = "Tus datos fueron actualizados con éxito!";
                        else
                            lblError.Text = "Tus datos no pudieron ser actualizados";
                    }
                }
            }
            catch { }
        }
    }
}
