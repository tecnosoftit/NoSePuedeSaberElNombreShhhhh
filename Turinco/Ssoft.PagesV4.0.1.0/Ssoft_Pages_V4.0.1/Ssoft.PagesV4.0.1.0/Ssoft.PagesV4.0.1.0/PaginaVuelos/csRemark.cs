using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Configuration;
using Ssoft.Utils;
using Ssoft.Rules.Corporativo;
using Ssoft.Rules.Pagina;
using Ssoft.ManejadorExcepciones;

namespace Ssoft.Pages
{
    public class csRemark : TemplateControl
    {
        public const string TABLA_RELAREMARK = "tblRelaRemark";

        private const string COLUMN_CODIGO = "intCodigo";
        private const string COLUMN_APLICACION = "intAplicacion";
        private const string COLUMN_AGENCIA = "intAgencia";
        private const string COLUMN_WS = "intWS";
        private const string COLUMN_TIPO_REMARK = "intTipoRemark";
        private const string COLUMN_DESCRIPCION = "strDescripcion";
        private const string COLUMN_TEXTO = "strTexto";
        private const string COLUMN_VARIABLE = "intVariable";
        private const string COLUMN_COMANDO = "strComando";
        private const string COLUMN_ACTIVO = "bitActivo";
        private const string COLUMN_ORDEN = "intOrden";

        protected string strConexion = default(string);
        /// <summary>
        /// Estable u obtiene es string de conexion
        /// </summary>
        public string Conexion
        {
            set { strConexion = value; }
            get { return strConexion; }
        }
        public csRemark()
        {
            strConexion = clsSesiones.getConexion();
        }
        public void setCargar(UserControl PageSource)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    DataSet dsData = new DataSet();

                    string sAgencia = csReferencias.csEmpresa();
                    csReferencias cReferencia = new csReferencias();

                    dsData = cReferencia.csRemark(sAgencia);
                    setFormulario(PageSource, dsData);
                    setHabilitaEditar(PageSource, false);
                }
                else
                {
                    csGeneralsPag.FinSesion();
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
                cParametros.Complemento = "Gastos ";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
                new csCache().setError(PageSource, cParametros);
            }
        }
        public void setCommand(UserControl PageSource, object source, CommandEventArgs e)
        {
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    switch (e.CommandName)
                    {
                        case "Edit":
                            setEditar(PageSource);
                            break;


                        case "Cancel":
                            setCargar(PageSource);
                            break;

                        case "Back":
                            setRegresar(PageSource);
                            break;

                        case "New":
                            setNuevo(PageSource);
                            break;
                    }
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
                cParametros.Complemento = "Galeria general";
                ExceptionHandled.Publicar(cParametros);
            }
        }
        private void setFormulario(UserControl PageSource, DataSet dsData)
        {
            try
            {
                
                Repeater rptRefere = (Repeater)PageSource.FindControl("rptRefere");

                clsControls.LlenaControl(rptRefere, dsData);
                Label lblSeccion = (Label)PageSource.FindControl("lblSeccion");
                setCargarCombos(rptRefere);
                lblSeccion.Text = "Remark";
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
        }
        private void setNuevo(UserControl PageSource)
        {
            Label lblError = (Label)PageSource.FindControl("lblError");
            lblError.Text = string.Empty;

            DataSet dsData = new DataSet();
            dsData = setRegistros(1);
            setFormulario(PageSource, dsData);
            setEditar(PageSource);
        }
        private void setEditar(UserControl PageSource)
        {
            Label lblError = (Label)PageSource.FindControl("lblError");
            lblError.Text = string.Empty;

            Repeater rptRefere = (Repeater)PageSource.FindControl("rptRefere");
            setHabilitaText(rptRefere, false);
            setHabilitaEditar(PageSource, true);
        }
      
        private void setRegresar(UserControl PageSource)
        {
            string sSesion = string.Empty;
            clsCache cCache = new csCache().cCache();
            if (cCache != null)
            {
                sSesion = cCache.SessionID.ToString();
            }
            clsValidaciones.RedirectPagina("Index.aspx");
        }
        private void setHabilitaText(Repeater rptRefere, bool bEditar)
        {
            int iTotal = rptRefere.Items.Count;

            for (int i = 0; i < iTotal; i++)
            {
                TextBox txtDescripcion = (TextBox)rptRefere.Items[i].FindControl("txtDescripcion");
                TextBox txtTexto = (TextBox)rptRefere.Items[i].FindControl("txtTexto");
                DropDownList ddlVariable = (DropDownList)rptRefere.Items[i].FindControl("ddlVariable");
                DropDownList ddlWs = (DropDownList)rptRefere.Items[i].FindControl("ddlWs");
                DropDownList ddlTipoRemark = (DropDownList)rptRefere.Items[i].FindControl("ddlTipoRemark");
                CheckBox chkActivo = (CheckBox)rptRefere.Items[i].FindControl("chkActivo");
                txtDescripcion.ReadOnly = bEditar;
                txtTexto.ReadOnly = bEditar;
                ddlVariable.Enabled = !bEditar;
                ddlWs.Enabled = !bEditar;
                ddlTipoRemark.Enabled = !bEditar;
                chkActivo.Enabled = !bEditar;
            }
        }
        private void setHabilitaEditar(UserControl PageSource, bool bEditar)
        {
            Button btnNuevo = (Button)PageSource.FindControl("btnNuevo");
            Button btnEditar = (Button)PageSource.FindControl("btnEditar");
            Button btnGuardar = (Button)PageSource.FindControl("btnGuardar");
            Button btnCancelar = (Button)PageSource.FindControl("btnCancelar");
            Button btnRegresar = (Button)PageSource.FindControl("btnRegresar");

            btnNuevo.Enabled = !bEditar;
            btnEditar.Enabled = !bEditar;
            btnGuardar.Enabled = bEditar;
            btnCancelar.Enabled = bEditar;
            btnRegresar.Enabled = !bEditar;
        }

        /// <summary>
        /// metodo pendiente por revision
        /// </summary>
        /// <param name="rptRefere"></param>
        private void setCargarCombos(Repeater rptRefere)
        {
            DataSet dsDataWs = new DataSet();
            DataSet dsDataTipoR = new DataSet();
            DataSet dsDataRemark = new DataSet();
            

            //string sRefereWs = clsValidaciones.GetKeyOrAdd("WebService", "WS");
            //string sRefereTipoRemark = clsValidaciones.GetKeyOrAdd("TipoRemark", "TipoRemark");
            //string sVariablesRemarks = clsValidaciones.GetKeyOrAdd("VariablesRemarks", "VariablesRemarks");

            //tblRefere otblRefere = new tblRefere();
            //dsDataWs = otblRefere.GetTipoRefere(sRefereWs, true);
            //dsDataTipoR = otblRefere.GetTipoRefere(sRefereTipoRemark);
            //dsDataRemark = otblRefere.GetTipoRefere(sVariablesRemarks);

            //int iTotal = rptRefere.Items.Count;

            //for (int i = 0; i < iTotal; i++)
            //{
            //    DropDownList ddlWs = (DropDownList)rptRefere.Items[i].FindControl("ddlWs");
            //    DropDownList ddlTipoRemark = (DropDownList)rptRefere.Items[i].FindControl("ddlTipoRemark");
            //    DropDownList ddlVariable = (DropDownList)rptRefere.Items[i].FindControl("ddlVariable");
            //    HiddenField intWS = (HiddenField)rptRefere.Items[i].FindControl("intWS");
            //    HiddenField intTipoRemark = (HiddenField)rptRefere.Items[i].FindControl("intTipoRemark");
            //    HiddenField intVariable = (HiddenField)rptRefere.Items[i].FindControl("intVariable");
            //    clsControls.LlenaControl(ddlWs, dsDataWs, "strDetalle", "intIdRefere", false, 0);
            //    clsControls.LlenaControl(ddlTipoRemark, dsDataTipoR, "strDetalle", "intIdRefere", false, 0);
            //    clsControls.LlenaControl(ddlVariable, dsDataRemark, "strDetalle", "intIdRefere", false, 0);
            //    ddlWs.SelectedValue = intWS.Value;
            //    ddlTipoRemark.SelectedValue = intTipoRemark.Value;
            //    ddlVariable.SelectedValue = intVariable.Value;
            //}
        }
        private DataSet setRegistros(int iRegistros)
        {
            DataTable dtRefereCorp = new DataTable(TABLA_RELAREMARK);
            DataSet dsData = new DataSet();

            dtRefereCorp.Columns.Add(COLUMN_CODIGO, typeof(int));
            dtRefereCorp.Columns.Add(COLUMN_APLICACION, typeof(int));
            dtRefereCorp.Columns.Add(COLUMN_AGENCIA, typeof(int));
            dtRefereCorp.Columns.Add(COLUMN_WS, typeof(int));
            dtRefereCorp.Columns.Add(COLUMN_TIPO_REMARK, typeof(int));
            dtRefereCorp.Columns.Add(COLUMN_DESCRIPCION, typeof(string));
            dtRefereCorp.Columns.Add(COLUMN_TEXTO, typeof(string));
            dtRefereCorp.Columns.Add(COLUMN_VARIABLE, typeof(int));
            dtRefereCorp.Columns.Add(COLUMN_COMANDO, typeof(string));
            dtRefereCorp.Columns.Add(COLUMN_ACTIVO, typeof(bool));
            dtRefereCorp.Columns.Add(COLUMN_ORDEN, typeof(int));
            try
            {
                string sEmpresa = csReferencias.csEmpresa();

                for (int i = 0; i < iRegistros; i++)
                {
                    DataRow fila = dtRefereCorp.NewRow();

                    fila[COLUMN_CODIGO] = 0;
                    fila[COLUMN_APLICACION] = 1;
                    fila[COLUMN_AGENCIA] = sEmpresa;
                    fila[COLUMN_WS] = 0;
                    fila[COLUMN_TIPO_REMARK] = 0;                   
                    fila[COLUMN_VARIABLE] = 0;
                    fila[COLUMN_ACTIVO] = true;
                    fila[COLUMN_ORDEN] = 0;
                    dtRefereCorp.Rows.Add(fila);
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
            dsData.Tables.Add(dtRefereCorp);
            return dsData;
        }
    }
}
