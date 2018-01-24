using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Ssoft.Utils
{
    [Obsolete]
    public class clsMensajes
    {
        private string sMensaje = string.Empty;
        private string sComplemento = string.Empty;
        private string sTipoMensaje = string.Empty;
        private string sSugerencia = string.Empty;
        private string sSource = string.Empty;
        private string sStackTrace = string.Empty;
        private string sDatoAdic = string.Empty;
        private string sDatoAdicArr = string.Empty;
        private int iMensaje = 0;

        public string Mensaje
        {
            get { return sMensaje; }
            set { sMensaje = value; }
        }

        public string Complemento
        {
            get { return sComplemento; }
            set { sComplemento = value; }
        }

        public string TipoMensaje
        {
            get { return sTipoMensaje; }
            set { sTipoMensaje = value; }
        }

        public string Sugerencia
        {
            get { return sSugerencia; }
            set { sSugerencia = value; }
        }

        public string Source
        {
            get { return sSource; }
            set { sSource = value; }
        }

        public string StackTrace
        {
            get { return sStackTrace; }
            set { sStackTrace = value; }
        }

        public int id
        {
            get { return iMensaje; }
            set { iMensaje = value; }
        }

        public string DatoAdic
        {
            get { return sDatoAdic; }
            set { sDatoAdic = value; }
        }

        public string DatoAdicArr
        {
            get { return sDatoAdicArr; }
            set { sDatoAdicArr = value; }
        }

        public clsMensajes()
        {
        }
    }

    public class clsMensajesError
    {
        public clsMensajesError()
        {
        }
        public clsMensajes GetMensaje(clsMensajes CodigoMensaje)
        {
            try
            {
                switch (CodigoMensaje.id)
                {
                    case 0:
                        CodigoMensaje.Mensaje = "Error al guardar los datos, por favor verificar";
                        CodigoMensaje.TipoMensaje = "General";
                        CodigoMensaje.Sugerencia = "Comunicarse con el administardor";
                        break;
                    case 1:
                        CodigoMensaje.Mensaje = "Se guardaron los datos exitosamente";
                        CodigoMensaje.TipoMensaje = "Base de datos";
                        CodigoMensaje.Sugerencia = string.Empty;
                        break;
                    case 2:
                        CodigoMensaje.Mensaje = "Error de Procedimiento";
                        CodigoMensaje.TipoMensaje = "General";
                        CodigoMensaje.Sugerencia = "Comunicarse con el administrador";
                        break;
                }
                return CodigoMensaje;
            }
            catch (Exception ex)
            {
                CodigoMensaje.Complemento = CodigoMensaje.Complemento.ToString() + " Error en mensaje " + ex.Message.ToString();
                CodigoMensaje.Source = CodigoMensaje.Source.ToString() + " Error en mensaje " + ex.Source.ToString();
                CodigoMensaje.StackTrace = CodigoMensaje.StackTrace.ToString() + " Error en mensaje " + ex.StackTrace.ToString();
                CodigoMensaje.TipoMensaje = CodigoMensaje.TipoMensaje.ToString() + " Error en mensaje General ";
                return CodigoMensaje;
            }
        }

        public string[] GetMensajeError(string[] CodigoMensaje)
        {
            string sMensaje = string.Empty;
            string sComplemento = string.Empty;
            string sTipoMensaje = string.Empty;
            string sSugerencia = string.Empty;
            string sSource = string.Empty;
            string sStackTrace = string.Empty;
            int iMensaje = int.Parse(CodigoMensaje[0].ToString());
            string[] sRespuesta = new string[7];
            sTipoMensaje = "General";
            sSugerencia = "Comunicarse con el administardor";

            try
            {
                switch (int.Parse(CodigoMensaje[0].ToString()))
                {
                    case 0:
                        sMensaje = "Error al guardar los datos, por favor verificar";
                        sComplemento = CodigoMensaje[1].ToString();
                        sTipoMensaje = "General";
                        sSugerencia = "Comunicarse con el administardor";
                        sSource = CodigoMensaje[2].ToString();
                        sStackTrace = CodigoMensaje[3].ToString();
                        break;
                    case 1:
                        sMensaje = "Se guardaron los datos exitosamente";
                        sComplemento = CodigoMensaje[1].ToString();
                        sTipoMensaje = "Base de datos";
                        sSugerencia = "";
                        sSource = CodigoMensaje[2].ToString();
                        sStackTrace = CodigoMensaje[3].ToString();
                        break;
                    case 2:
                        sMensaje = "Error de Procedimiento";
                        sComplemento = CodigoMensaje[1].ToString();
                        sTipoMensaje = "General";
                        sSugerencia = "Comunicarse con el administrador";
                        sSource = CodigoMensaje[2].ToString();
                        sStackTrace = CodigoMensaje[3].ToString();
                        break;
                    case 3:
                        sMensaje = "";
                        sComplemento = CodigoMensaje[1].ToString();
                        sTipoMensaje = "";
                        sSugerencia = "";
                        sSource = CodigoMensaje[2].ToString();
                        sStackTrace = CodigoMensaje[3].ToString();
                        break;
                    case 4:
                        sMensaje = "";
                        sComplemento = CodigoMensaje[1].ToString();
                        sTipoMensaje = "";
                        sSugerencia = "";
                        sSource = CodigoMensaje[2].ToString();
                        sStackTrace = CodigoMensaje[3].ToString();
                        break;
                }
                sRespuesta[0] = sMensaje;
                sRespuesta[1] = sComplemento;
                sRespuesta[2] = sTipoMensaje;
                sRespuesta[3] = sSugerencia;
                sRespuesta[4] = sSource;
                sRespuesta[5] = sStackTrace;
                sRespuesta[6] = iMensaje.ToString();
                return sRespuesta;
            }
            catch (Exception ex)
            {
                sRespuesta[0] = sMensaje;
                sRespuesta[1] = ex.ToString();
                sRespuesta[2] = sTipoMensaje;
                sRespuesta[3] = sSugerencia;
                sRespuesta[4] = sSource;
                sRespuesta[5] = sStackTrace;
                sRespuesta[6] = iMensaje.ToString();
                return sRespuesta;
            }
        }
    }

    public class clsPantallaError
    {
        public clsPantallaError()
        {
        }
        private Table getMensaje(string sMensaje)
        {
            string sMensaje1 = string.Empty;
            string sMensaje2 = string.Empty;
            string sTelefono = string.Empty;
            string sRutaImagenes = clsValidaciones.ObtenerUrlImages();
            string sImagen = sRutaImagenes + "sinResultados.jpg";

            try
            {
                if (sMensaje.Length == 0)
                {
                    sMensaje1 = "No se encontraron resultados para su busqueda.";
                    sMensaje2 = "Por favor intente otra vez.";
                }
                else
                {
                    if (sMensaje.IndexOf("|").Equals(-1))
                    {
                        sMensaje1 = sMensaje;
                    }
                    else
                    {
                        Utils cUtil = new Utils();
                        string[] sValorArr = clsValidaciones.Lista(sMensaje, "|");
                        if (sValorArr.Length > 1)
                        {
                            sMensaje1 = sValorArr[0].ToString();
                            sMensaje2 = sValorArr[1].ToString();
                        }
                        else
                        {
                            if (sValorArr.Length > 0)
                            {
                                sMensaje1 = sValorArr[0].ToString();
                            }
                            else
                            {
                                sMensaje1 = "No se encontraron resultados para su busqueda.";
                                sMensaje2 = "Por favor intente otra vez.";
                            }
                        }
                    }
                }
            }
            catch
            {
                sMensaje1 = "No se encontraron resultados para su busqueda.";
                sMensaje2 = "Por favor intente otra vez.";
            }

            try
            {
                sTelefono = clsValidaciones.GetKeyOrAdd("Agencia_Telefono", "");
            }
            catch { }

            Table tResultados = new Table();

            TableRow trSuperior = new TableRow();
            TableCell tcSup1 = new TableCell();
            TableCell tcSup2 = new TableCell();
            Label lSup2 = new Label();
            TableCell tcSup3 = new TableCell();

            TableRow trResultado = new TableRow();
            TableCell tcResult1 = new TableCell();
            TableCell tcResult2 = new TableCell();
            TableCell tcResult3 = new TableCell();

            TableRow trInferior = new TableRow();
            TableCell tcInf1 = new TableCell();
            TableCell tcInf2 = new TableCell();
            TableCell tcInf3 = new TableCell();

            Label lDatos;

            tResultados.Rows.Add(getEspacio(30));
            StringBuilder SinResultados = new StringBuilder();

            SinResultados.AppendLine(" <table border='0' cellpadding='0' cellspacing='0'>    ");
            SinResultados.AppendLine(" <tr class='alineacionSuperior'>  ");
            SinResultados.AppendLine(" <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>  ");
            SinResultados.AppendLine(" <td colspan='2'> ");
            SinResultados.AppendLine(" <div class='callcenter'> ");
            SinResultados.AppendLine(" <span style='color:#F60; font-size:15px;'> ");
            SinResultados.AppendLine(" Llama ya a nuestro callcenter&nbsp;&nbsp;&nbsp;&nbsp; ");
            SinResultados.AppendLine(" </span> ");
            SinResultados.AppendLine(" <br /> ");
            SinResultados.AppendLine(" <strong>" + sTelefono + "</strong>&nbsp; ");
            SinResultados.AppendLine(" </div> ");
            SinResultados.AppendLine(" </td> ");
            SinResultados.AppendLine(" </tr> ");
            SinResultados.AppendLine(" <tr> ");
            SinResultados.AppendLine(" <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>  ");
            SinResultados.AppendLine(" <td> ");
            SinResultados.AppendLine(" <img alt='' src='" + sImagen + "' /> ");
            SinResultados.AppendLine(" </td> ");
            SinResultados.AppendLine(" <td align='center'> ");
            SinResultados.AppendLine(" <span style='color:#036; text-align:center; font-size:26px;'> ");
            SinResultados.AppendLine(" " + sMensaje1 + " ");
            SinResultados.AppendLine(" <br /> ");
            SinResultados.AppendLine(" " + sMensaje2 + " ");
            SinResultados.AppendLine(" </span> ");
            SinResultados.AppendLine(" </td> ");
            SinResultados.AppendLine(" </tr> ");
            SinResultados.AppendLine(" </table> ");

            #region [SUPERIOR]
            tcSup1.CssClass = "Resultado_Encabezado_1";
            tcSup2.CssClass = "Resultado_Encabezado_2_3_4";
            tcSup3.CssClass = "Resultado_Encabezado_7";

            lSup2.CssClass = "tituloRuta";

            lSup2.Text = SinResultados.ToString();
            tcSup2.Controls.Add(lSup2);
            tcSup2.Width = new Unit(100, UnitType.Percentage);
            trSuperior.Cells.Add(tcSup1);
            trSuperior.Cells.Add(tcSup2);
            trSuperior.Cells.Add(tcSup3);

            tResultados.Rows.Add(trSuperior);

            #endregion

            trResultado = new TableRow();
            tcResult1 = new TableCell();
            tcResult2 = new TableCell();
            tcResult3 = new TableCell();
            lDatos = new Label();

            tcResult1.CssClass = "resultado_1";
            tcResult2.CssClass = "resultado_2";
            tcResult3.CssClass = "resultado_8";

            tcResult1.Width = new Unit(9);
            lDatos.CssClass = "tituloRuta";
            lDatos.Text = string.Empty;
            tcResult2.Controls.Add(lDatos);

            trResultado.Cells.Add(tcResult1);
            trResultado.Cells.Add(tcResult2);
            trResultado.Cells.Add(tcResult3);

            tResultados.Rows.Add(trResultado);


            #region[INFERIOR]

            tcInf1.CssClass = "resultados_Inf_1";
            tcInf2.CssClass = "resultados_Inf_2";
            tcInf3.CssClass = "resultados_Inf_3";

            trInferior.Cells.Add(tcInf1);
            trInferior.Cells.Add(tcInf2);
            trInferior.Cells.Add(tcInf3);

            tResultados.Rows.Add(trInferior);

            #endregion

            tResultados.CssClass = "textoNormal";
            tResultados.CellPadding = 0;
            tResultados.CellSpacing = 0;
            tResultados.Width = new Unit(100, UnitType.Percentage);
            tResultados.BorderStyle = BorderStyle.None;

            return tResultados;
        }
        private Table getMensajeReserva(string sRecord)
        {
            string sRutaImagenes = clsValidaciones.ObtenerUrlImages();
            string sImagen = sRutaImagenes + "sinResultados.jpg";
            Table tResultados = new Table();

            TableRow trSuperior = new TableRow();
            TableCell tcSup1 = new TableCell();
            TableCell tcSup2 = new TableCell();
            Label lSup2 = new Label();
            TableCell tcSup3 = new TableCell();

            TableRow trResultado = new TableRow();
            TableCell tcResult1 = new TableCell();
            TableCell tcResult2 = new TableCell();
            TableCell tcResult3 = new TableCell();

            TableRow trInferior = new TableRow();
            TableCell tcInf1 = new TableCell();
            TableCell tcInf2 = new TableCell();
            TableCell tcInf3 = new TableCell();

            Label lDatos;

            tResultados.Rows.Add(getEspacio(30));
            StringBuilder SinResultados = new StringBuilder();

            SinResultados.AppendLine(" <table border='0' cellpadding='0' cellspacing='0'>    ");
            SinResultados.AppendLine(" <tr class='alineacionSuperior'>  ");
            SinResultados.AppendLine(" <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>  ");
            SinResultados.AppendLine(" <td> ");
            SinResultados.AppendLine(" <img alt='' src='" + sImagen + "' /> ");
            SinResultados.AppendLine(" </td> ");
            SinResultados.AppendLine(" <td align='center'> ");
            SinResultados.AppendLine(" <span style='color:#036; text-align:center; font-size:26px;'> ");
            SinResultados.AppendLine(" Su reserva ha sido confirmada con el record de agencia número " + sRecord + " ");
            SinResultados.AppendLine(" <br /> ");
            SinResultados.AppendLine(" <br /> ");
            SinResultados.AppendLine(" Por ser una tarifa promocional, un asesor nuestro lo contactará en breves minutos para gestionar el pago y garantizar su boleto ");
            SinResultados.AppendLine(" </span> ");
            SinResultados.AppendLine(" </td> ");
            SinResultados.AppendLine(" </tr> ");
            SinResultados.AppendLine(" </table> ");

            #region [SUPERIOR]
            tcSup1.CssClass = "Resultado_Encabezado_1";
            tcSup2.CssClass = "Resultado_Encabezado_2_3_4";
            tcSup3.CssClass = "Resultado_Encabezado_7";

            lSup2.CssClass = "tituloRuta";

            lSup2.Text = SinResultados.ToString();
            tcSup2.Controls.Add(lSup2);
            tcSup2.Width = new Unit(100, UnitType.Percentage);
            trSuperior.Cells.Add(tcSup1);
            trSuperior.Cells.Add(tcSup2);
            trSuperior.Cells.Add(tcSup3);

            tResultados.Rows.Add(trSuperior);

            #endregion

            trResultado = new TableRow();
            tcResult1 = new TableCell();
            tcResult2 = new TableCell();
            tcResult3 = new TableCell();
            lDatos = new Label();

            tcResult1.CssClass = "resultado_1";
            tcResult2.CssClass = "resultado_2";
            tcResult3.CssClass = "resultado_8";

            tcResult1.Width = new Unit(9);
            lDatos.CssClass = "tituloRuta";
            lDatos.Text = string.Empty;
            tcResult2.Controls.Add(lDatos);

            trResultado.Cells.Add(tcResult1);
            trResultado.Cells.Add(tcResult2);
            trResultado.Cells.Add(tcResult3);

            tResultados.Rows.Add(trResultado);


            #region[INFERIOR]

            tcInf1.CssClass = "resultados_Inf_1";
            tcInf2.CssClass = "resultados_Inf_2";
            tcInf3.CssClass = "resultados_Inf_3";

            trInferior.Cells.Add(tcInf1);
            trInferior.Cells.Add(tcInf2);
            trInferior.Cells.Add(tcInf3);

            tResultados.Rows.Add(trInferior);

            #endregion

            tResultados.CssClass = "textoNormal";
            tResultados.CellPadding = 0;
            tResultados.CellSpacing = 0;
            tResultados.Width = new Unit(100, UnitType.Percentage);
            tResultados.BorderStyle = BorderStyle.None;

            return tResultados;
        }
        private TableRow getEspacio(int iHeight)
        {
            TableRow trEspacio;
            TableCell tcEspacio;

            trEspacio = new TableRow();
            tcEspacio = new TableCell();

            tcEspacio.CssClass = "divisionSpacer";
            trEspacio.CssClass = "divisionSpacer";

            if (!iHeight.Equals(0))
            {
                trEspacio.Height = new Unit(iHeight);
            }
            trEspacio.Cells.Add(tcEspacio);

            return trEspacio;
        }
        public void getPantalla(string sMensaje, Panel cPanel)
        {
            Table tResultados;

            cPanel.Controls.Clear();
            tResultados = new Table();

            tResultados = getMensaje(sMensaje);
            cPanel.Controls.Add(tResultados);
        }
        public void getReserva(string sReserva, Panel cPanel)
        {
            Table tResultados;

            cPanel.Controls.Clear();
            tResultados = new Table();

            tResultados = getMensajeReserva(sReserva);
            cPanel.Controls.Add(tResultados);
        }
    }
}
