using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Ssoft.ManejadorExcepciones;

namespace Ssoft.Utils
{
    public static class clsControls
    {
        public static void LlenaControl(ListBox control, DataSet dataset, string datatext, string datavalue, bool blanco)
        {
            //if (controles == 0)
            //{
            control.DataSource = dataset.Tables[0].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (blanco)
            {
                control.Items.Add(new ListItem("", "0"));
                control.Items[control.Items.Count - 1].Selected = true;
            }
            //}
            //else
            //{
            //    for (int i = 0; i < controles; i++)
            //    {
            //        control.ID = 
            //    }
            //}
        }
        public static void LlenaControl(ListBox control, DataSet dataset, string datatext, string datavalue, bool adiciona, string TextAdic, string ValueAdic)
        {
            control.DataSource = dataset.Tables[0].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (adiciona)
            {
                control.Items.Add(new ListItem(TextAdic, ValueAdic));
                control.Items[control.Items.Count - 1].Selected = true;
            }
        }
        public static void LlenaControl(ListBox control, DataSet dataset, string datatext, string datavalue, bool blanco, int iPos)
        {
            control.DataSource = dataset.Tables[iPos].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (blanco)
            {
                control.Items.Add(new ListItem("", "0"));
                control.Items[control.Items.Count - 1].Selected = true;
            }
        }
        public static void LlenaControl(ListBox control, DataSet dataset, string datatext, string datavalue, bool adiciona, int iPos, string TextAdic, string ValueAdic)
        {
            control.DataSource = dataset.Tables[iPos].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (adiciona)
            {
                control.Items.Add(new ListItem(TextAdic, ValueAdic));
                control.Items[control.Items.Count - 1].Selected = true;
            }
        }
        public static void LlenaControl(ListBox control, int intValor, int intValorInicial)
        {
            control.Items.Add(new ListItem("0", "0"));

            for (int i = intValorInicial - 1; i < intValor; i++)
            {
                control.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
            }
            control.SelectedValue = "0";
        }
        public static void LlenaControl(DropDownList control, DataSet dataset, string datatext, string datavalue, bool blanco)
        {
            try
            {

                control.DataSource = dataset.Tables[0].DefaultView;
                control.DataTextField = datatext;
                control.DataValueField = datavalue;
                control.DataBind();
                if (blanco)
                {
                    control.Items.Add(new ListItem("", "0"));
                    control.Items[control.Items.Count - 1].Selected = true;
                }
            }
            catch (Exception) { }
        }
        public static void LlenaControl(DropDownList control, DataTable datatable, string datatext, string datavalue, bool blanco)
        {
            try
            {

                control.DataSource = datatable.DefaultView;
                control.DataTextField = datatext;
                control.DataValueField = datavalue;
                control.DataBind();
                if (blanco)
                {
                    control.Items.Add(new ListItem("", "0"));
                    control.Items[control.Items.Count - 1].Selected = true;
                }
            }
            catch (Exception) { }
        }
        public static void LlenaDropDownList(DropDownList control, DataTable datatable, string datatext, string datavalue, bool blanco)
        {
            try
            {

                control.DataSource = datatable.DefaultView;
                control.DataTextField = datatext;
                control.DataValueField = datavalue;
                control.DataBind();
                if (blanco)
                {
                    control.Items.Add(new ListItem("", "0"));
                    control.Items[control.Items.Count - 1].Selected = true;
                }
            }
            catch (Exception) { }
        }
        public static void LlenaControl(DropDownList control, DataSet dataset, string datatext, string datavalue, bool blanco, string sDefault)
        {
            try
            {

                control.DataSource = dataset.Tables[0].DefaultView;
                control.DataTextField = datatext;
                control.DataValueField = datavalue;
                control.DataBind();
                if (blanco)
                {
                    control.Items.Add(new ListItem("", "0"));
                }
                control.SelectedValue = sDefault;
            }
            catch (Exception) { }
        }
        public static void LlenaControl(DropDownList control, DataSet dataset, string datatext, string datavalue, bool adiciona, string TextAdic, string ValueAdic)
        {
            control.DataSource = dataset.Tables[0].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (adiciona)
            {
                control.Items.Add(new ListItem(TextAdic, ValueAdic));
                control.Items[control.Items.Count - 1].Selected = true;
            }
        }
        public static void LlenaControl(DropDownList control, DataSet dataset, string datatext, string datavalue, bool blanco, int iPos)
        {
            control.DataSource = dataset.Tables[iPos].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (blanco)
            {
                control.Items.Add(new ListItem("", "0"));
                control.Items[control.Items.Count - 1].Selected = true;
            }
        }
        public static void LlenaControl(DropDownList control, DataSet dataset, string datatext, string datavalue, bool adiciona, int iPos, string TextAdic, string ValueAdic)
        {
            control.DataSource = dataset.Tables[iPos].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (adiciona)
            {
                control.Items.Add(new ListItem(TextAdic, ValueAdic));
                control.Items[control.Items.Count - 1].Selected = true;
            }
        }
        public static void LlenaControl(DropDownList control, int intValor, int intValorInicial)
        {
            control.Items.Add(new ListItem("0", "0"));

            for (int i = intValorInicial - 1; i < intValor; i++)
            {
                control.Items.Add(new ListItem((i + 1).ToString(), (i + 1).ToString()));
            }
            control.SelectedValue = "0";
        }
        public static void LlenaControl(CheckBoxList control, DataSet dataset, string datatext, string datavalue, bool blanco, bool check)
        {
            control.DataSource = dataset.Tables[0].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (blanco)
            {
                control.Items.Add(new ListItem("", "0"));
                control.Items[control.Items.Count - 1].Selected = true;
            }
            for (int i = 0; i < dataset.Tables[0].Rows.Count; i++)
            {
                control.Items[i].Selected = check;
            }
        }
        public static void LlenaControl(BulletedList control, DataSet dataset, string datatext, string datavalue, bool blanco)
        {
            control.DataSource = dataset.Tables[0].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (blanco)
            {
                control.Items.Add(new ListItem("", "0"));
                control.Items[control.Items.Count - 1].Selected = true;
            }
        }
        public static void LlenaControl(CheckBoxList control, DataSet dataset, string datatext, string datavalue, string valuecompare, bool blanco, bool bloqueo)
        {
            control.DataSource = dataset.Tables[0].DefaultView;
            control.DataTextField = datatext;
            control.DataValueField = datavalue;
            control.DataBind();
            if (blanco)
            {
                control.Items.Add(new ListItem("", "0"));
                control.Items[control.Items.Count - 1].Selected = true;
            }
            for (int i = 0; i < dataset.Tables[1].Rows.Count; i++)
            {
                for (int pos = 0; pos < control.Items.Count; pos++)
                {
                    if (control.Items[pos].Value == dataset.Tables[1].Rows[i][valuecompare].ToString())
                        control.Items[pos].Selected = true;
                    if (bloqueo)
                    {
                        control.Items[pos].Enabled = false;
                    }
                }
            }
        }
        public static void LlenaControl(GridView control, DataSet dataset)
        {
            control.DataSource = dataset.Tables[0].DefaultView;
            control.DataBind();
        }
        public static void LlenaControl(GridView control, DataTable datatable)
        {
            control.DataSource = datatable.DefaultView;
            control.DataBind();
        }
        public static void LlenaControl(Repeater control, DataSet dataset)
        {
            control.DataSource = dataset.Tables[0].DefaultView;
            control.DataBind();
        }
        public static void LlenaControl(DataList control, DataSet dataset)
        {
            control.DataSource = dataset.Tables[0].DefaultView;
            control.DataBind();
        }
        //public static void LlenaControl(WebDataGrid control, DataSet dataset)
        //{
        //    try
        //    {
        //        control.DataSource = dataset.Tables[0].DefaultView;
        //        //control.DataBind();
        //    }
        //    catch { }
        //}
        //public static void LlenaControl(WebDataGrid control, DataTable datatable)
        //{
        //    try
        //    {
        //        control.DataSource = datatable.DefaultView;
        //        //control.DataBind();
        //    }
        //    catch { }
        //}

        //public static void LlenaControl(WebHierarchicalDataGrid control, DataSet dataset)
        //{
        //    try
        //    {
        //        control.DataSource = dataset.Tables[0].DefaultView;
        //        //control.DataBind();
        //    }
        //    catch { }
        //}
        //public static void LlenaControl(WebHierarchicalDataGrid control, DataTable datatable)
        //{
        //    try
        //    {
        //        control.DataSource = datatable.DefaultView;
        //        //control.DataBind();
        //    }
        //    catch { }
        //}
        //public static void LlenaControl(WebHierarchicalDataSource control, DataSet dataset)
        //{
        //    try
        //    {
        //        control.DataSource = dataset.Tables[0].DefaultView;
        //        control.DataBind();
        //    }
        //    catch { }
        //}
        //public static void LlenaControl(WebHierarchicalDataSource control, DataTable datatable)
        //{
        //    try
        //    {
        //        control.DataSource = datatable.DefaultView;
        //        control.DataBind();
        //    }
        //    catch { }
        //}
        public static void LlenaControl(Repeater control, DataTable datatable)
        {
            control.DataSource = datatable.DefaultView;
            control.DataBind();
        }
        public static void LlenaControl(DataList control, DataTable datatable)
        {
            control.DataSource = datatable.DefaultView;
            control.DataBind();
        }
        public static void LlenarTextoTodos(DropDownList ddl)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Value.Equals("") || ddl.Items[i].Value.Equals("0"))
                {
                    ddl.Items[i].Text = "Seleccione";
                }
            }
        }
        public static void LlenarTextoTodos(ListBox ddl)
        {
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Value.Equals("") || ddl.Items[i].Value.Equals("0"))
                {
                    ddl.Items[i].Text = "Seleccione";
                }
            }
        }
        public static void CargarControl(UserControl ucSource, DataTable dtValores)
        {
            Control ctrControl = new Control();

            if (dtValores.Columns.Count > 0)
            {
                for (int i = 0; i < dtValores.Columns.Count; i++)
                {
                    ctrControl = ucSource.FindControl(dtValores.Columns[i].ColumnName.ToString());
                    try
                    {

                        if (ctrControl != null &&
                           !string.IsNullOrEmpty(ctrControl.ToString()))
                        {
                            try
                            {
                                if (ctrControl is Label)
                                {
                                    Label lbl = (Label)ctrControl;
                                    lbl.Text = dtValores.Rows[0][i].ToString();
                                }

                                else if (ctrControl is TextBox)
                                {
                                    TextBox lbl = (TextBox)ctrControl;
                                    lbl.Text = dtValores.Rows[0][i].ToString();
                                }
                                else if (ctrControl is HiddenField)
                                {
                                    HiddenField lbl = (HiddenField)ctrControl;
                                    lbl.Value = dtValores.Rows[0][i].ToString();
                                }
                                else if (ctrControl is Image)
                                {
                                    Image lbl = (Image)ctrControl;
                                    lbl.ImageUrl = dtValores.Rows[0][i].ToString();
                                }
                                else if (ctrControl is Button)
                                {
                                    Button lbl = (Button)ctrControl;
                                    lbl.PostBackUrl = dtValores.Rows[0][i].ToString();
                                }
                                else if (ctrControl is LinkButton)
                                {
                                    LinkButton lbl = (LinkButton)ctrControl;
                                    lbl.PostBackUrl = dtValores.Rows[0][i].ToString();
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        public static void CargarControl(Page pgSource, DataTable dtValores)
        {
            Control ctrControl;

            if (dtValores.Columns.Count > 0)
            {
                for (int i = 0; i < dtValores.Columns.Count; i++)
                {
                    ctrControl = pgSource.FindControl(dtValores.Columns[i].ColumnName.ToString());
                    try
                    {
                        if (ctrControl.ToString() != null)
                        {
                            try
                            {
                                Label lbl = (Label)ctrControl;
                                lbl.Text = dtValores.Rows[0][i].ToString();
                            }
                            catch
                            {
                            }
                            try
                            {
                                TextBox lbl = (TextBox)ctrControl;
                                lbl.Text = dtValores.Rows[0][i].ToString();
                            }
                            catch
                            {
                            }
                            try
                            {
                                HiddenField lbl = (HiddenField)ctrControl;
                                lbl.Value = dtValores.Rows[0][i].ToString();
                            }
                            catch
                            {
                            }
                            try
                            {
                                Image lbl = (Image)ctrControl;
                                lbl.ImageUrl = dtValores.Rows[0][i].ToString();
                            }
                            catch
                            {
                            }
                            try
                            {
                                Button lbl = (Button)ctrControl;
                                lbl.PostBackUrl = dtValores.Rows[0][i].ToString();
                            }
                            catch
                            {
                            }
                            try
                            {
                                LinkButton lbl = (LinkButton)ctrControl;
                                lbl.PostBackUrl = dtValores.Rows[0][i].ToString();
                            }
                            catch
                            {
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        // Llenado para Idiomas
        public static void LlenaControl(DropDownList control, DataTable dtData, string datatext, string datavalue)
        {
            try
            {

                control.DataSource = dtData.DefaultView;
                control.DataTextField = datatext;
                control.DataValueField = datavalue;
                control.DataBind();
            }
            catch { }
        }
        public static void LlenaControl(RadioButtonList control, DataTable dtData, string datatext, string datavalue)
        {
            try
            {

                control.DataSource = dtData.DefaultView;
                control.DataTextField = datatext;
                control.DataValueField = datavalue;
                control.DataBind();
            }
            catch { }
        }
        public static void LlenaControl(CheckBoxList control, DataTable dtData, string datatext, string datavalue)
        {
            try
            {

                control.DataSource = dtData.DefaultView;
                control.DataTextField = datatext;
                control.DataValueField = datavalue;
                control.DataBind();
            }
            catch { }
        }
        /// <summary>
        /// Metodo que quita la seleccion de los radiobuttons de un repetidor y deja seleccionado solo el ultimo que se eligio
        /// </summary>
        /// <param name="sender">RadioButton</param>
        /// <param name="sNombreRadio">Nombre de los radios que se dejaran sin seleccionar</param>
        ///<remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-11-17
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void setSeleccionRadio(object sender, string sNombreRadio)
        {
            try
            {
                Repeater rptRepeater = ((Repeater)((RepeaterItem)((RadioButton)sender).Parent).Parent);
                setSeleccionRadio(rptRepeater, sender, sNombreRadio);
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
                cParametros.Metodo = "setSeleccionRadio";
                cParametros.Complemento = "clsControls";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        /// <summary>
        /// Metodo que quita la seleccion de los radiobuttons de un repetidor y deja seleccionado solo el ultimo que se eligio
        /// </summary>
        /// <param name="ControlRepeater">Repetidor donde se encuentra el radiobutton</param>
        /// <param name="sender">RadioButton</param>
        /// <param name="sNombreRadio">Nombre de los radios que se dejaran sin seleccionar</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-12-30
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void setSeleccionRadio(Repeater ControlRepeater, object sender, string sNombreRadio)
        {
            try
            {
                for (int i = 0; i < ControlRepeater.Items.Count; i++)
                {
                    RadioButton rbRadio = (RadioButton)ControlRepeater.Items[i].FindControl(sNombreRadio);
                    /*COMPARAMOS LOS RADIOBUTTONS DEL REPETIDOR CON EL QUE GENERO EL EVENTO*/
                    if (!sender.Equals(rbRadio))
                    {
                        rbRadio.Checked = false;
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
                cParametros.Metodo = "setSeleccionRadio";
                cParametros.Complemento = "clsControls";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        /// <summary>
        /// Metodo que quita la seleccion de los radiobuttons de un datalist y deja seleccionado solo el ultimo que se eligio
        /// </summary>
        /// <param name="ControlRepeater">Datalist donde se encuentra el radiobutton</param>
        /// <param name="sender">RadioButton</param>
        /// <param name="sNombreRadio">Nombre de los radios que se dejaran sin seleccionar</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-12-30
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void setSeleccionRadio(DataList ControlRepeater, object sender, string sNombreRadio)
        {
            try
            {
                for (int i = 0; i < ControlRepeater.Items.Count; i++)
                {
                    RadioButton rbRadio = (RadioButton)ControlRepeater.Items[i].FindControl(sNombreRadio);
                    /*COMPARAMOS LOS RADIOBUTTONS DEL REPETIDOR CON EL QUE GENERO EL EVENTO*/
                    if (!sender.Equals(rbRadio))
                    {
                        rbRadio.Checked = false;
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
                cParametros.Metodo = "setSeleccionRadio";
                cParametros.Complemento = "clsControls";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        /// <summary>
        /// Metodo que quita la seleccion de los radiobuttons de un datalist y deja seleccionado solo el ultimo que se eligio
        /// </summary>
        /// <param name="sender">RadioButton</param>
        /// <param name="sNombreRadio">Nombre de los radios que se dejaran sin seleccionar</param>
        ///<remarks>
        /// Autor:          José Faustino Posas
        /// Company:        Ssoft Colombia
        /// Fecha:          2011-12-30
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// Autor:          
        /// Fecha:         
        /// Descripción:    
        /// </remarks>
        public static void setSeleccionRadioDtl(object sender, string sNombreRadio)
        {
            try
            {
                DataList dtlRepeater = ((DataList)((DataListItem)((RadioButton)sender).Parent).Parent);
                setSeleccionRadio(dtlRepeater, sender, sNombreRadio);
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
                cParametros.Metodo = "setSeleccionRadio";
                cParametros.Complemento = "clsControls";
                cParametros.ViewMessage.Add("Su sesion ha terminado");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
    }
}
