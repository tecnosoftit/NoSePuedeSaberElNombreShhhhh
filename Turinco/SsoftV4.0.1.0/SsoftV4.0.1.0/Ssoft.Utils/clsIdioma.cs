using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ssoft.Data;
using System.IO;
using System.Configuration;
using Ssoft.ManejadorExcepciones;
using Ssoft.Sql;

namespace Ssoft.Utils
{
    public class clsIdioma
    {
        private const string ETIQUETA = "strEtiqueta";
        private const string TEXT = "strLabel";
        private const string TOOLTIP = "strTooltip";
        private const string CONTROLVALUE = "strControlValue";
        private const string LINK = "strLink";
        private const string IMAGEN = "strImagen";
        private const string LINKCLIENTE = "strLinkClient";
        private const string REFERE = "strRefere";
        private const string VALUE = "strText";

        protected string strConexion = string.Empty;
        public clsIdioma()
        {
            strConexion = clsSesiones.getConexion();
        }
        public string Conexion
        {
            set { this.strConexion = value; }
            get { return this.strConexion; }
        }
        /// <summary>
        /// Metodo para idiomas generales
        /// </summary>
        /// <param name="Forma">Tipo de referencia</param>
        /// <param name="ReferenciaPaginas">Referencia</param>
        /// <returns>Dataset de resultados</returns>
       
        public DataSet dsIdioma(string Forma, string ReferenciaPaginas, string sOrden)
        {
            DataSet dstDatos = new DataSet();
            string Idioma = clsSesiones.getIdioma();
            string Aplicacion = clsSesiones.getAplicacion().ToString();

            clsSerializer cSerializer = new clsSerializer();
            string strPathXML = clsValidaciones.XMLDatasetCreaGen();
            string strArchivo = Forma + "_" + ReferenciaPaginas + sOrden + "_" + Idioma + Aplicacion + ".xml";
            string strFile = strPathXML + strArchivo;
            try
            {
                if (File.Exists(strFile))
                {
                    dstDatos = cSerializer.XMLDatasetGen(strFile);
                }
                else
                {
                    string Control = clsValidaciones.GetKeyOrAdd("Controls", "Controls");
                    StringBuilder Consulta = new StringBuilder();
                    DataSql dSql = new DataSql();
                    Consulta.AppendLine(" SELECT     tblRefereControls_1.strValor AS strControlValue, tblIdioma.strObject AS strEtiqueta, tblIdioma.strLabel, tblIdioma.strText,  ");
                    Consulta.AppendLine(" tblIdioma.strTooltip, tblIdioma.strLink, tblIdioma.strLinkClient, tblIdioma.strImagen, tblIdioma.intTipo ");
                    Consulta.AppendLine(" FROM         tblTipoRefere AS tblTipoRefere_1 INNER JOIN ");
                    Consulta.AppendLine(" tblRefereControls AS tblRefereControls_1 ON tblTipoRefere_1.intidTipoRefere = tblRefereControls_1.intidTipoRefere INNER JOIN ");
                    Consulta.AppendLine(" tblTipoRefere INNER JOIN ");
                    Consulta.AppendLine(" tblRefereControls ON tblTipoRefere.intidTipoRefere = tblRefereControls.intidTipoRefere INNER JOIN ");
                    Consulta.AppendLine(" tblRelaRefereControls ON tblRefereControls.intidRefere = tblRelaRefereControls.intidRefereControl INNER JOIN ");
                    Consulta.AppendLine(" tblIdioma ON tblRelaRefereControls.intidRefereAsociado = tblIdioma.intForma AND  ");
                    Consulta.AppendLine(" tblRefereControls.intAplicacion = tblIdioma.intAplicacion ON tblRefereControls_1.intidRefere = tblIdioma.intEtiqueta AND  ");
                    Consulta.AppendLine(" tblRefereControls_1.intAplicacion = tblIdioma.intAplicacion ");
                    Consulta.AppendLine(" WHERE     (tblTipoRefere.strTipoRefere = '" + ReferenciaPaginas + "') ");
                    Consulta.AppendLine(" AND (tblRefereControls.strIdioma = 'es') ");
                    Consulta.AppendLine(" AND (tblTipoRefere.strIdioma = 'es') ");
                    Consulta.AppendLine(" AND (tblTipoRefere_1.strIdioma = 'es') ");
                    Consulta.AppendLine(" AND (tblRefereControls_1.strIdioma = 'es') ");
                    Consulta.AppendLine(" AND (tblIdioma.strIdioma = '" + Idioma + "') ");
                    Consulta.AppendLine(" AND (tblRefereControls.strRefere = '" + Forma + "') ");
                    Consulta.AppendLine(" AND (tblIdioma.intAplicacion = " + Aplicacion + ")  ");
                    Consulta.AppendLine(" AND (tblTipoRefere_1.strTipoRefere = '" + Control + "')  ");
                    Consulta.AppendLine(" AND (tblRefereControls_1.intOrden = " + sOrden + ") ");
                    Consulta.AppendLine(" ORDER BY tblIdioma.intTipo ");
                    Consulta.AppendLine("   ");
                    Consulta.AppendLine(" SELECT     tblRefereControls_1.strValor AS strControlValue, tblIdioma.strObject AS strEtiqueta ");
                    Consulta.AppendLine(" FROM         tblTipoRefere AS tblTipoRefere_1 INNER JOIN ");
                    Consulta.AppendLine(" tblRefereControls AS tblRefereControls_1 ON tblTipoRefere_1.intidTipoRefere = tblRefereControls_1.intidTipoRefere INNER JOIN ");
                    Consulta.AppendLine(" tblTipoRefere INNER JOIN ");
                    Consulta.AppendLine(" tblRefereControls ON tblTipoRefere.intidTipoRefere = tblRefereControls.intidTipoRefere INNER JOIN ");
                    Consulta.AppendLine(" tblRelaRefereControls ON tblRefereControls.intidRefere = tblRelaRefereControls.intidRefereControl INNER JOIN ");
                    Consulta.AppendLine(" tblIdioma ON tblRelaRefereControls.intidRefereAsociado = tblIdioma.intForma AND  ");
                    Consulta.AppendLine(" tblRefereControls.intAplicacion = tblIdioma.intAplicacion ON tblRefereControls_1.intidRefere = tblIdioma.intEtiqueta AND  ");
                    Consulta.AppendLine(" tblRefereControls_1.intAplicacion = tblIdioma.intAplicacion ");
                    Consulta.AppendLine(" WHERE     (tblTipoRefere.strTipoRefere = '" + ReferenciaPaginas + "') ");
                    Consulta.AppendLine(" AND (tblRefereControls.strIdioma = 'es') ");
                    Consulta.AppendLine(" AND (tblTipoRefere.strIdioma = 'es') ");
                    Consulta.AppendLine(" AND (tblTipoRefere_1.strIdioma = 'es') ");
                    Consulta.AppendLine(" AND (tblRefereControls_1.strIdioma = 'es') ");
                    Consulta.AppendLine(" AND (tblIdioma.strIdioma = '" + Idioma + "') ");
                    Consulta.AppendLine(" AND (tblRefereControls.strRefere = '" + Forma + "') ");
                    Consulta.AppendLine(" AND (tblIdioma.intAplicacion = " + Aplicacion + ")  ");
                    Consulta.AppendLine(" AND (tblTipoRefere_1.strTipoRefere = '" + Control + "')  ");
                    Consulta.AppendLine(" AND (tblRefereControls_1.intOrden = " + sOrden + ") ");
                    Consulta.AppendLine(" GROUP BY tblRefereControls_1.strValor, tblIdioma.strObject   ");

                    dSql.Conexion = Conexion;
                    dstDatos = dSql.Select(Consulta.ToString());

                    cSerializer.DatasetXMLGen(dstDatos, strFile);
                }
            }
            catch
            {
            }
            return dstDatos;
        }
        private static Enum_Controls eControles(string sIdControls)
        {
            Enum_Controls eControls = Enum_Controls.Nulo;
            if (sIdControls.Equals(Enum_Controls.Label.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.Label;
            }
            if (sIdControls.Equals(Enum_Controls.Button.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.Button;
            }
            if (sIdControls.Equals(Enum_Controls.LinkButton.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.LinkButton;
            }
            if (sIdControls.Equals(Enum_Controls.HiperLink.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.HiperLink;
            }
            if (sIdControls.Equals(Enum_Controls.RadioButton.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.RadioButton;
            }
            if (sIdControls.Equals(Enum_Controls.ImageButton.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.ImageButton;
            }
            if (sIdControls.Equals(Enum_Controls.TextBox.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.TextBox;
            }
            if (sIdControls.Equals(Enum_Controls.CheckBox.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.CheckBox;
            }
            if (sIdControls.Equals(Enum_Controls.DataList.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.DataList;
            }
            if (sIdControls.Equals(Enum_Controls.Repeater.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.Repeater;
            }
            if (sIdControls.Equals(Enum_Controls.RadioButtonList.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.RadioButtonList;
            }
            if (sIdControls.Equals(Enum_Controls.DropDownList.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.DropDownList;
            }
            if (sIdControls.Equals(Enum_Controls.CheckBoxList.GetHashCode().ToString()))
            {
                eControls = Enum_Controls.CheckBoxList;
            }
            return eControls;
        }
        private void csControl(Control ctrControl, DataRow dtrEtiquetas)
        {
            string sText = "Etiqueta";
            string sControl = "Etiqueta_c";
            string sControlType = "Etiqueta_t";
            try
            {
                sText = dtrEtiquetas[TEXT].ToString();
                sControl = ctrControl.ClientID.ToString();
                sControlType = dtrEtiquetas[CONTROLVALUE].ToString();
            }
            catch { }
            try
            {
                Enum_Controls eControls = eControles(dtrEtiquetas[CONTROLVALUE].ToString());
                string sIdioma = clsSesiones.getIdioma();

                if (!eControls.Equals(Enum_Controls.Nulo))
                {
                    if (eControls.Equals(Enum_Controls.Label))
                    {
                        Label lbl = (Label)ctrControl;
                        lbl.Text = dtrEtiquetas[TEXT].ToString();
                        lbl.ToolTip = dtrEtiquetas[TOOLTIP].ToString();
                    }
                    else if (eControls.Equals(Enum_Controls.Literal))
                    {
                        Literal ltr = (Literal)ctrControl;
                        ltr.Text = dtrEtiquetas[TEXT].ToString();
                    }
                    else if (eControls.Equals(Enum_Controls.Button))
                    {
                        Button But = (Button)ctrControl;
                        But.Text = dtrEtiquetas[TEXT].ToString();
                        But.ToolTip = dtrEtiquetas[TOOLTIP].ToString();
                    }
                    else if (eControls.Equals(Enum_Controls.LinkButton))
                    {
                        LinkButton But = (LinkButton)ctrControl;
                        But.Text = dtrEtiquetas[TEXT].ToString();
                        But.ToolTip = dtrEtiquetas[TOOLTIP].ToString();
                    }
                    else if (eControls.Equals(Enum_Controls.HiperLink))
                    {
                        HyperLink HLnk = (HyperLink)ctrControl;
                        HLnk.Text = dtrEtiquetas[TEXT].ToString();
                        HLnk.ToolTip = dtrEtiquetas[TOOLTIP].ToString();
                        if (dtrEtiquetas[LINK].ToString() != null || dtrEtiquetas[LINK].ToString() != "")
                            HLnk.NavigateUrl = dtrEtiquetas[LINK].ToString();
                    }
                    else if (eControls.Equals(Enum_Controls.RadioButton))
                    {
                        RadioButton Rdb = (RadioButton)ctrControl;
                        Rdb.Text = dtrEtiquetas[TEXT].ToString();
                        Rdb.ToolTip = dtrEtiquetas[TOOLTIP].ToString();
                    }
                    else if (eControls.Equals(Enum_Controls.ImageButton))
                    {
                        ImageButton Img = (ImageButton)ctrControl;
                        Img.ToolTip = dtrEtiquetas[TOOLTIP].ToString();
                        if (dtrEtiquetas[IMAGEN].ToString() != null || dtrEtiquetas[IMAGEN].ToString() != "")
                        {
                            try
                            {
                                try
                                {
                                    Img.ImageUrl = clsValidaciones.ObtenerUrlImages(sIdioma) + dtrEtiquetas[IMAGEN].ToString();
                                    if (!String.IsNullOrEmpty(dtrEtiquetas[LINKCLIENTE].ToString()))
                                        Img.OnClientClick = "window.open('" + dtrEtiquetas[LINKCLIENTE].ToString() + "');return false;";
                                    if (!String.IsNullOrEmpty(dtrEtiquetas[LINK].ToString()))
                                        Img.PostBackUrl = dtrEtiquetas[LINK].ToString();
                                }
                                catch { Img.ImageUrl = clsValidaciones.ObtenerUrlImages(sIdioma) + dtrEtiquetas[IMAGEN].ToString(); }
                            }
                            catch { Img.ImageUrl = clsValidaciones.ObtenerUrlImages() + dtrEtiquetas[IMAGEN].ToString(); }
                        }
                    }
                    else if (eControls.Equals(Enum_Controls.TextBox))
                    {
                        TextBox Tbox = (TextBox)ctrControl;
                        if (dtrEtiquetas[TEXT].ToString() != null || dtrEtiquetas[TEXT].ToString() != "")
                            Tbox.Text = dtrEtiquetas[TEXT].ToString();
                        Tbox.ToolTip = dtrEtiquetas[TOOLTIP].ToString();
                    }
                    else if (eControls.Equals(Enum_Controls.CheckBox))
                    {
                        CheckBox check = (CheckBox)ctrControl;
                        if (dtrEtiquetas[TEXT].ToString() != null || dtrEtiquetas[TEXT].ToString() != "")
                            check.Text = dtrEtiquetas[TEXT].ToString();
                        check.ToolTip = dtrEtiquetas[TOOLTIP].ToString();
                    }
                    else if (eControls.Equals(Enum_Controls.RadioButtonList))
                    {
                        CheckBox check = (CheckBox)ctrControl;
                        if (dtrEtiquetas[TEXT].ToString() != null || dtrEtiquetas[TEXT].ToString() != "")
                            check.Text = dtrEtiquetas[TEXT].ToString();
                        check.ToolTip = dtrEtiquetas[TOOLTIP].ToString();
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
                cParametros.Complemento = "Cambio de idioma al control " + sControl + " Text " + sText + "  Type " + sControlType;
                ExceptionHandled.Publicar(cParametros);
            }
        }
        private void csControl(Control ctrControl, DataTable dtData)
        {
            string sControl = "Etiqueta_c";
            try
            {
                sControl = ctrControl.ClientID.ToString();
            }
            catch { }
            try
            {
                Enum_Controls eControls = eControles(dtData.Rows[0][CONTROLVALUE].ToString());
                if (!eControls.Equals(Enum_Controls.Nulo))
                {
                    if (eControls.Equals(Enum_Controls.RadioButtonList))
                    {
                        RadioButtonList rbtList = (RadioButtonList)ctrControl;
                        clsControls.LlenaControl(rbtList, dtData, TEXT, VALUE);
                    }
                    else if (eControls.Equals(Enum_Controls.DropDownList))
                    {
                        DropDownList drbList = (DropDownList)ctrControl;
                        clsControls.LlenaControl(drbList, dtData, TEXT, VALUE);
                    }
                    if (eControls.Equals(Enum_Controls.CheckBoxList))
                    {
                        CheckBoxList chkList = (CheckBoxList)ctrControl;
                        clsControls.LlenaControl(chkList, dtData, TEXT, VALUE);
                    }
                    //if (eControls.Equals(Enum_Controls.Repeater))
                    //{
                    //    Repeater rptList = (Repeater)ctrControl;
                    //    clsControls.LlenaControl(chkList, dtData, TEXT, VALUE);
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
                cParametros.Complemento = "Cambio de idioma al control " + sControl;
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void LoadIdioma(string Forma, Page PageSource)
        {
            DataSet dsIdiomaForma = new DataSet();

            Control ctrControl;
            string ReferenciaPaginas = clsValidaciones.GetKeyOrAdd("Paginas", "Paginas");

            dsIdiomaForma = dsIdioma(Forma, ReferenciaPaginas, "0");
            try
            {
                if (dsIdiomaForma.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtrEtiquetas in dsIdiomaForma.Tables[0].Rows)
                    {
                        if (Convert.IsDBNull(dtrEtiquetas[ETIQUETA]) == false)
                        {
                            ctrControl = PageSource.Form.FindControl(dtrEtiquetas[ETIQUETA].ToString());
                            if (ctrControl.ToString() != "")
                            {
                                csControl(ctrControl, dtrEtiquetas);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            dsIdiomaForma = dsIdioma(Forma, ReferenciaPaginas, "1");
            try
            {
                if (dsIdiomaForma.Tables[0].Rows.Count > 0)
                {
                    if (dsIdiomaForma.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dtrControls in dsIdiomaForma.Tables[1].Rows)
                        {
                            string sWhere = ETIQUETA + " = '" + dtrControls[ETIQUETA].ToString() + "' ";
                            //DataRow[] drData = dstDatos.Tables[0].Select(sWhere);
                            DataView vistaDatos = new DataView(dsIdiomaForma.Tables[0]);
                            vistaDatos.RowFilter = sWhere;
                            /*pasamos los datos filtrados a un DataTable*/
                            DataTable dtTablaFiltrada = vistaDatos.ToTable();
                            ctrControl = PageSource.Form.FindControl(dtrControls[ETIQUETA].ToString());
                            if (ctrControl.ToString() != "")
                            {
                                csControl(ctrControl, dtTablaFiltrada);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public void LoadIdioma(string Forma, UserControl UsercontrolSource)
        {
            DataSet dsIdiomaForma = new DataSet();

            Control ctrControl;
            string ReferenciaPaginas = clsValidaciones.GetKeyOrAdd("userControls", "userControls");

            dsIdiomaForma = dsIdioma(Forma, ReferenciaPaginas, "0");
            try
            {
                if (dsIdiomaForma.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dtrEtiquetas in dsIdiomaForma.Tables[0].Rows)
                    {
                        if (Convert.IsDBNull(dtrEtiquetas[ETIQUETA]) == false)
                        {
                            ctrControl = UsercontrolSource.FindControl(dtrEtiquetas[ETIQUETA].ToString());
                            if (ctrControl != null && ctrControl.ToString() != "")
                            {
                                csControl(ctrControl, dtrEtiquetas);
                            }
                        }
                    }
                }
            }
            catch 
            {
            }
            dsIdiomaForma = dsIdioma(Forma, ReferenciaPaginas, "1");
            try
            {
                if (dsIdiomaForma.Tables[0].Rows.Count > 0)
                {
                    if (dsIdiomaForma.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dtrControls in dsIdiomaForma.Tables[1].Rows)
                        {
                            string sWhere = ETIQUETA + " = '" + dtrControls[ETIQUETA].ToString() + "' ";
                            //DataRow[] drData = dstDatos.Tables[0].Select(sWhere);
                            DataView vistaDatos = new DataView(dsIdiomaForma.Tables[0]);
                            vistaDatos.RowFilter = sWhere;
                            /*pasamos los datos filtrados a un DataTable*/
                            DataTable dtTablaFiltrada = vistaDatos.ToTable();
                            ctrControl = UsercontrolSource.FindControl(dtrControls[ETIQUETA].ToString());
                            if (ctrControl != null && ctrControl.ToString() != "")
                            {
                                csControl(ctrControl, dtTablaFiltrada);
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        /*METODO UTILIZADO PARA CAMBAIR EL IDIOMA DE LA PAGINA, ACTUALIZA LA SESION IDIOMA Y LA CACHE EN LA VARIABLE IDIOMA*/
        public void CambiarIdioma(string sIdioma)
        {
            try
            {
                clsCacheControl cCacheControl = new clsCacheControl();
                clsCache cCache = new csCache().cCache();
                clsSesiones.setIdioma(sIdioma);

                cCache.Idioma = sIdioma;
                cCacheControl.ActualizaXML(cCache);
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
                cParametros.Complemento = "Cambio de idioma a " + sIdioma;
                ExceptionHandled.Publicar(cParametros);
            }
        }
    }
}
