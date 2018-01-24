using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Ssoft.ManejadorExcepciones;
using System.Data;
using Ssoft.Utils;
using System.Web.UI;
using SsoftQuery.SeccionesInformativas;
using Ssoft.DataNet;
using System.Web.UI.HtmlControls;

namespace Ssoft.Pages.PaginaSeccionesInformativas
{
    public class csSeccionesInformativas
    {

        public void CargarSeccionInformativa(
                           UserControl PageSource,
                           Enum_Tipo_Seccion_Publicacion tipoSeccionPublicacion,
                           Enum_Tipo_Plantilla_Seccion tipoPlantillaSeccion,
                           string nivel,
                           string codigoPadre,
                           string posicion,
                           int? iPagina,
                           string codigoSeccion,
                           int? cantidadFilas,
                           string campoOrden,
                           params Dictionary<string, string>[] parametrosAdicionales)
        {
            String refereSeccionPublicacion = null;
            String refereSeccionInformativa = null;
            String codigo = codigoSeccion;
            csConsultaSecciones secInformativa = new csConsultaSecciones();
            Repeater rptSeccion = PageSource.FindControl("rptSeccion") as Repeater;

            csGeneralsPag.Idioma(PageSource);


            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    #region [SECCION PUBLICACION]
                    switch (tipoSeccionPublicacion)
                    {
                        case Enum_Tipo_Seccion_Publicacion.SP_AEROLINEAS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionAerolineas", "SP_AEROLINEAS");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_BOLETIN:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionBoletin", "SP_BOLETIN");
                            if (PageSource.Request.QueryString["SECPUB"] != null)
                            {
                                refereSeccionPublicacion = PageSource.Request.QueryString["SECPUB"];
                            }
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_CONDICIONES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionCondiciones", "SP_CONDICIONES");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_CONDICIONES_REGISTRO:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionCondicionesRegistro", "SP_CONDICIONES_REGISTRO");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_CONTACTENOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionTexcoContacto", "SP_CONTACTENOS");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_DESTINOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionDestinos", "SP_DESTINO");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_DOCUMENTACION:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionDocumentacion", "SP_DOCUMENTACION");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_GRUPOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionGrupos", "SP_GRUPOS");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_HOTELES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionHoteles", "SP_HOTELES");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_INF_CARROCOMPRAS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionCarroCompras", "SP_INF_CARROCOMPRAS");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_MEDIOS_PAGO:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionMediosPago", "SP_MEDIOS_PAGO");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_QUIENES_SOMOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionQuienes", "SP_QUIENES_SOMOS");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_RECOMENDACIONES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionRecomendaciones", "SP_RECOMENDACIONES");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_ENCABEZADO:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionEncabezado", "SP_ENCABEZADO");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_TESTIMONIOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionTestimonios", "SP_TESTIMONIOS_DE_CLIENTES");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_CONGRESOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionCongresos", "SP_CONGRESOS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_SALIDAS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionSalidas", "SP_SALIDAS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_CRUCEROS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionCruceros", "SP_CRUCEROS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_TURISMO_ESP:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionTurismo", "SP_TURISMO");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_NOTICIAS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionNoticias", "SP_NOTICIAS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_TEXTOS_LEGALES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionTextosLegales", "SP_TEXTOS_LEGALES");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_CLAUSULA_RESPONSABILIDAD:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionClausula", "SP_CLAUSULA_RESPONSABILIDAD");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_PREGUNTASFRECUENTES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPreguntas", "SP_PF_PAGINA");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_IMAGENES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionImagenes", "SP_IMAGENES");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_BIENVENIDA:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionBienvenida", "SP_BIENVENIDO");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_ACERCA_BOG:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionAcercaBogota", "SP_ACERCA_BOG");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_MARCABLANCA:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionMarcaBlanca", "SP_MARCABLANCA");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_ACTIVIDADES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionActividades", "SP_ACTIVIDADES");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_COMO_COMPRAR:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionComoComprar", "SP_COMO_COMPRAR");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_PREGUNTAS_FRECUENTES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionPreguntasFrecuentes", "SP_PREGUNTAS_FRECUENTES");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_SERVICIOS_PARA_AGENCIAS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionServiciosparaAgencias", "SP_SERVICIOS_PARA_AGENCIAS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_SERVICIOS_PARA_PROVEEDORES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionServiciosparaProveedores", "SP_SERVICIOS_PARA_PROVEEDORES");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_FORMAS_DE_PAGO:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionFormasdePago", "SP_FORMAS_DE_PAGO");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_EXPERIENCIAS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionExperiencias", "SP_EXPERIENCIAS");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_OLAS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionOlas", "SP_OLAS");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_SERVICIOS_CORPORATIVOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionServiciosCorporativos", "SP_SERVICIOS_CORPORATIVOS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_PROVEEDORES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionProveedores", "SP_PROVEEDORES");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_AYUDA:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionAyuda", "SP_AYUDA");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_ASESOR:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionAsesor", "SP_ASESOR");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_CERTIFICACIONES:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionCertificaciones", "SP_CERTIFICACIONES");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_CHAT:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionChat", "SP_CHAT");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_CONTRA:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionContra", "SP_CONTRA");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_FACEBOOK:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionFacebook", "SP_FACEBOOK");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_TWITTER:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionTwitter", "SP_TWITTER");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_MAPA:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionMapa", "SP_MAPA");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_COMENTARIOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionComentarios", "SP_COMENTARIOS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_COMPRAR_EMPRESA:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionComprar", "SP_COMPRAR_EMPRESA");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_VIAJAR_SEGURO:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionViajarSeguro", "SP_VIAJAR_SEGURO");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_PLANESDEAHORRO:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionPlanesAhorro", "SP_PLANESDEAHORRO");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_REVISTA_VAMOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionRevistaVamos", "SP_REVISTA_VAMOS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_NUESTROS_ALIADOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionNuestrosAliados", "SP_NUESTROS_ALIADOS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_HOTEL:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionHotel", "SP_HOTEL");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_AUTOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionAutos", "SP_AUTOS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_PLANTILLA_1:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionSeccion1", "SP_PLANTILLA_1");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_PLANTILLA_2:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionSeccion2", "SP_PLANTILLA_2");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_PLANTILLA_3:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionSeccion3", "SP_PLANTILLA_3");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_PLANTILLA_4:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionSeccion4", "SP_PLANTILLA_4");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_OFERTAS_DYSNEY:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionDisney", "s035");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_CONTACTO_SERVICIO:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionContacto", "SP_CONTACTO_SERVICIO");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_BENEFICIOS_CORPORATIVOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionBeneficiosCorporativo", "SP_BENEFICIOS_CORPORATIVOS");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_TEXTO_LOGIN:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionTextoLogin", "SP_TEXTO_LOGIN");
                            break;

                        case Enum_Tipo_Seccion_Publicacion.SP_SALA_PRENSA:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionSaladeprensa", "SP_SALA_PRENSA");
                            break;
                        case Enum_Tipo_Seccion_Publicacion.SP_DESTINOS_DESTACADOS:
                            refereSeccionPublicacion = clsValidaciones.GetKeyOrAdd("SeccionPublicacionSaladeprensa", "SP_DESTINOS_DESTACADOS");
                            break;
                        default:
                            break;
                    }
                    #endregion
                    #region [SECCION INFORMATIVA]
                    //switch (seccionInformativa)
                    //{
                    //    case Enum_Seccion_Informativa.AEROLINEAS:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionAerolineas", "AEROLINEAS");
                    //        break;
                    //    case Enum_Seccion_Informativa.BOLETIN:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionBoletin", "BOLETIN");
                    //        break;
                    //    case Enum_Seccion_Informativa.CONDICIONES:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionCondiciones", "CONDICIONES");
                    //        break;
                    //    case Enum_Seccion_Informativa.CONDICIONES_REGISTRO:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionCondicionesRegistro", "CONDICIONES_REGISTRO");
                    //        break;
                    //    case Enum_Seccion_Informativa.CONTACTENOS:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionTexcoContacto", "CONTACTENOS");
                    //        break;
                    //    case Enum_Seccion_Informativa.DESTINOS:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionDestinos", "DESTINOS");
                    //        break;
                    //    case Enum_Seccion_Informativa.DOCUMENTACION:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionDocumentacion", "DOCUMENTACION");
                    //        break;
                    //    case Enum_Seccion_Informativa.GRUPOS:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionGrupos", "GRUPOS");
                    //        break;
                    //    case Enum_Seccion_Informativa.HOTELES:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionHoteles", "HOTELES");
                    //        break;
                    //    case Enum_Seccion_Informativa.INF_CARROCOMPRAS:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionCarroCompras", "INF_CARROCOMPRAS");
                    //        break;
                    //    case Enum_Seccion_Informativa.MEDIOS_PAGO:
                    //        break;
                    //    case Enum_Seccion_Informativa.QUIENES_SOMOS:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionQuienes", "QUIENES_SOMOS");
                    //        break;
                    //    case Enum_Seccion_Informativa.RECOMENDACIONES:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionRecomendaciones", "RECOMENDACIONES");
                    //        break;
                    //    case Enum_Seccion_Informativa.NINGUNA:
                    //        break;
                    //    case Enum_Seccion_Informativa.LOGIN:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionLogin", "LOGIN");
                    //        break;
                    //    case Enum_Seccion_Informativa.TESTIMONIOS:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionTestimonios", "TESTIMONIOS");
                    //        break;
                    //    case Enum_Seccion_Informativa.PAUTE:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionPaute", "PAUTE");
                    //        break;
                    //    case Enum_Seccion_Informativa.NOTICIAS:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionNoticias", "NOTICIAS");
                    //        break;
                    //    case Enum_Seccion_Informativa.MARCABLANCA:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionMarcaBlcanca", "MARCABLANCA");
                    //        break;

                    //    case Enum_Seccion_Informativa.DEMOS_ALIACORP:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionDemos_Aliacorp", "DEMOS_ALIACORP");
                    //        break;
                    //    case Enum_Seccion_Informativa.SERVICIOS_CORPORATIVOS:
                    //        refereSeccionInformativa = clsValidaciones.GetKeyOrAdd("SeccionDemos_Servicios_Corporativos", "SERVICIOS_CORPORATIVOS");
                    //        break;

                    //    default:
                    //        break;
                    //}
                    #endregion
                    Label lblTitulo = PageSource.FindControl("lblTitulo") as Label;

                    switch (tipoPlantillaSeccion)
                    {
                        case Enum_Tipo_Plantilla_Seccion.PlantillaUno:
                            #region [PLANTILLA GENERAL]

                            DataSet dsSecciones = secInformativa.ConsultaSeccionesGeneral(
                                             codigo,
                                             refereSeccionPublicacion,
                                             codigoPadre,
                                             nivel,
                                             posicion,
                                            cCache.Empresa, cantidadFilas.ToString(), campoOrden);

                            if (dsSecciones != null && dsSecciones.Tables.Count != 0 && dsSecciones.Tables[0].Rows.Count != 0)
                            {
                                DataTable dtSeccionInf = dsSecciones.Tables[0];
                                string sTipoSeccion = clsValidaciones.GetKeyOrAdd("SEC_TIPOARCHIVOHTML", "SEC_TIPOARCHIVOHTML");
                                //if (dtSeccionInf.Rows[0]["strRefereTipoSeccion"].ToString().Equals(sTipoSeccion))
                                //{
                                //    ActualizaHtmlArchivo(dtSeccionInf, PageSource);
                                //}
                                try
                                {
                                    if (lblTitulo != null)
                                    {
                                        DataSet dsDetalles = null;
                                        if (int.Parse(nivel) > 0)
                                        {
                                            dsDetalles = secInformativa.ConsultaSeccionesGeneral(
                                                   dtSeccionInf.Rows[0]["intPadre"].ToString(),
                                                   null, null, null, null, null, null, null);
                                        }
                                        else
                                        {
                                            dsDetalles = secInformativa.ConsultaSeccionesGeneral(
                                                   codigo,
                                                   null, null, null, null, null, null, null);
                                        }
                                        if (dsDetalles != null)
                                        {
                                            lblTitulo.Text = dsDetalles.Tables[0].Rows[0]["strTitulo"].ToString();
                                        }
                                    }
                                }
                                catch { }
                                if (rptSeccion != null)
                                {
                                    // aca se verifica si el tipo de seccion es archivohtml, para reemplazar el strHtml y luego se llena normal
                                    rptSeccion.DataSource = dtSeccionInf;
                                    rptSeccion.DataBind();

                                    bool bEntra = true;

                                    for (int s = 0; s < rptSeccion.Items.Count; s++)
                                    {
                                        Repeater rptDetalle = rptSeccion.Items[s].FindControl("rptSeccion") as Repeater;
                                        Repeater rptlinks = rptSeccion.Items[s].FindControl("rptlinks") as Repeater;
                                        DataList dtlPaginador = rptSeccion.Items[s].FindControl("dtlPaginador") as DataList;

                                        /*cuando hay un repetidor de detalles.*/
                                        if (rptDetalle != null)
                                        {
                                            DataSet dsDetalles = secInformativa.ConsultaSeccionesGeneral(
                                                   null,
                                                   refereSeccionPublicacion,
                                                   dtSeccionInf.Rows[s]["intCodigo"].ToString(),
                                                 (Convert.ToInt32(dtSeccionInf.Rows[s]["intNivel"].ToString()) + 1).ToString(),
                                                   null,
                                                   cCache.Empresa, cantidadFilas.ToString(), campoOrden);


                                            if (dsDetalles != null &&
                                                dsDetalles.Tables.Count != 0)
                                            {

                                                DataTable dtDetalle = dsDetalles.Tables[0];
                                                int iCantidad = 0;
                                                /*cuando hay paginador*/
                                                if (dtlPaginador != null)
                                                {
                                                    iCantidad = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumeroNoticiasPagina", "4"));
                                                    csGeneralsPag.Paginar(rptDetalle, dtDetalle, iPagina ?? 0, dtlPaginador, iCantidad);
                                                }
                                                else
                                                {
                                                    #region Excluir
                                                    //string excluir = null;
                                                    //string incluir = null;
                                                    //string galery = null;
                                                    //foreach (Dictionary<string, string> dict in parametrosAdicionales)
                                                    //{
                                                    //    if (dict.ContainsKey("EXCLUIR"))
                                                    //    {
                                                    //        excluir = dict["EXCLUIR"];
                                                    //    }
                                                    //    if (dict.ContainsKey("INCLUIR"))
                                                    //    {
                                                    //        incluir = dict["INCLUIR"];
                                                    //    }
                                                    //    if (dict.ContainsKey("GALERY"))
                                                    //    {
                                                    //        galery = dict["GALERY"];
                                                    //    }
                                                    //}
                                                    //if (excluir != null)
                                                    //{
                                                    //    DataView vistaFiltrada = new DataView(dtDetalle);
                                                    //    vistaFiltrada.RowFilter = "strTitulo not like '*" + excluir + "*'";
                                                    //    dtDetalle = vistaFiltrada.ToTable();
                                                    //}
                                                    //else if (incluir != null)
                                                    //{
                                                    //    DataView vistaFiltrada = new DataView(dtDetalle);
                                                    //    vistaFiltrada.RowFilter = "strTitulo like '*" + incluir + "*'";
                                                    //    dtDetalle = vistaFiltrada.ToTable();
                                                    //}
                                                    //else if (galery != null)
                                                    //{
                                                    //    bEntra = false;
                                                    //    if (galery.Equals("1"))
                                                    //    {
                                                    //        int iTotalPaginas = dtDetalle.Rows.Count;
                                                    //        int iPaginacion = iPagina ?? 0;
                                                    //        int iPaginas = iTotalPaginas / iPaginacion;
                                                    //        int iPaginaCompara = iPaginas * iPaginacion;
                                                    //        if (iTotalPaginas > iPaginaCompara)
                                                    //            iPaginas++;

                                                    //        DataTable dtPaginacion = clsDataNet.dtPaginacionStyle(iPaginas, "pagedemo _current", "pagedemo");
                                                    //        rptDetalle.DataSource = dtPaginacion;
                                                    //        rptDetalle.DataBind();

                                                    //        for (int g = 0; g < rptDetalle.Items.Count; g++)
                                                    //        {
                                                    //            int iRowPagina = g * iPaginacion;
                                                    //            int iMaxRows = iPaginacion * (g + 1);
                                                    //            if (iMaxRows > iTotalPaginas)
                                                    //                iMaxRows = iTotalPaginas;
                                                    //            Repeater rptGalery = rptDetalle.Items[g].FindControl("rptSeccion") as Repeater;
                                                    //            if (rptGalery != null)
                                                    //            {
                                                    //                DataTable dtGalery = clsDataNet.dtPaginacionDetalle(iRowPagina, iMaxRows, dtDetalle);
                                                    //                rptGalery.DataSource = dtGalery;
                                                    //                rptGalery.DataBind();
                                                    //            }
                                                    //        }
                                                    //    }
                                                    //}
                                                    #endregion
                                                    if (bEntra)
                                                    {
                                                        rptDetalle.DataSource = dtDetalle;
                                                        rptDetalle.DataBind();
                                                    }
                                                }

                                                for (int c = 0; c < rptDetalle.Items.Count; c++)
                                                {
                                                    Label lblVideo = rptDetalle.Items[c].FindControl("lblVideo") as Label;

                                                    /*si contiene el video*/
                                                    if (dtDetalle.Rows[c]["strTitulo"].ToString().ToUpper().Contains("VIDEO"))
                                                    {
                                                        /*asignamos el html del video*/
                                                        if (lblVideo != null)
                                                        {
                                                            lblVideo.Text = dtDetalle.Rows[c]["strHtml"].ToString();
                                                            continue;
                                                        }
                                                    }

                                                    Repeater rptGaleria = rptDetalle.Items[c].FindControl("rptGaleria") as Repeater;

                                                    if (rptGaleria != null)
                                                    {
                                                        DataSet dsGaleria = secInformativa.ConsultaRelacionesSecciones(dtDetalle.Rows[c]["intCodigo"].ToString(), "0");

                                                        if (dsGaleria != null &&
                                                            dsGaleria.Tables.Count != 0)
                                                        {
                                                            DataTable dtGaleria = dsGaleria.Tables[0];
                                                            rptGaleria.DataSource = dtGaleria;
                                                            rptGaleria.DataBind();
                                                        }
                                                    }
                                                }
                                                if (bEntra)
                                                {
                                                    for (int d = 0; d < dtDetalle.Rows.Count; d++)
                                                    {                                                       
                                                        Repeater rptSubDetalle = rptDetalle.Items[d].FindControl("rptSeccion") as Repeater;
                                                        Repeater rptlinksSub = rptDetalle.Items[d].FindControl("rptlinks") as Repeater;
                                                        DataList dtlPaginadorSub = rptDetalle.Items[d].FindControl("dtlPaginador") as DataList;
                                                        DataSet dsSubDetalles = new DataSet();
                                                        if (rptSubDetalle != null)
                                                        {
                                                            if (iCantidad == null)
                                                                iCantidad = 0;
                                                            if (iPagina == null)
                                                                iPagina = 0;

                                                           
                                                            dsSubDetalles = secInformativa.ConsultaSeccionesGeneral(
                                                                    null,
                                                                    refereSeccionPublicacion,
                                                                    dtDetalle.Rows[Convert.ToInt32((iCantidad * iPagina) + d)]["intCodigo"].ToString(),
                                                                (Convert.ToInt32(dtDetalle.Rows[Convert.ToInt32((iCantidad * iPagina) + d)]["intNivel"].ToString()) + 1).ToString(),
                                                                    null,
                                                                    cCache.Empresa, null, null);
                                                           

                                                            if (dsSubDetalles != null &&
                                                                dsSubDetalles.Tables.Count != 0)
                                                            {
                                                                DataTable dtSubDetalle = dsSubDetalles.Tables[0];

                                                                /*cuando hay paginador*/
                                                                if (dtlPaginadorSub != null)
                                                                {
                                                                    iCantidad = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumeroNoticiasPagina", "4"));
                                                                    csGeneralsPag.Paginar(rptSubDetalle, dtSubDetalle, iPagina ?? 0, dtlPaginador, iCantidad);
                                                                }
                                                                else
                                                                {
                                                                    rptSubDetalle.DataSource = dtSubDetalle;
                                                                    rptSubDetalle.DataBind();
                                                                }
                                                                
                                                                /*si hay reptidor de galerias*/
                                                                if (rptSubDetalle != null && rptSubDetalle.Items.Count != 0)
                                                                {
                                                                    for (int g = 0; g < rptSubDetalle.Items.Count; g++)
                                                                    {
                                                                        Repeater rptGaleria = rptSubDetalle.Items[g].FindControl("rptGaleria") as Repeater;
                                                                        Label lblVideo = rptSubDetalle.Items[g].FindControl("lblVideo") as Label;

                                                                        /*si contiene el video*/
                                                                        if (dtSubDetalle.Rows[g]["strTitulo"].ToString().Contains("Video"))
                                                                        {
                                                                            /*asignamos el html del video*/
                                                                            if (lblVideo != null)
                                                                            {
                                                                                lblVideo.Text = dtSubDetalle.Rows[g]["strHtml"].ToString();
                                                                                continue;
                                                                            }
                                                                        }

                                                                        if (rptGaleria != null)
                                                                        {
                                                                            DataSet dsGaleria = secInformativa.ConsultaRelacionesSecciones(dtSubDetalle.Rows[g]["intCodigo"].ToString(), "0");

                                                                            if (dsGaleria != null &&
                                                                                dsGaleria.Tables.Count != 0)
                                                                            {
                                                                                DataTable dtGaleria = dsGaleria.Tables[0];
                                                                                rptGaleria.DataSource = dtGaleria;
                                                                                rptGaleria.DataBind();
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                        if (bEntra)
                                        {
                                            for (int g = 0; g < rptSeccion.Items.Count; g++)
                                            {
                                                Repeater rptGaleria = rptSeccion.Items[g].FindControl("rptGaleria") as Repeater;

                                                if (rptGaleria != null)
                                                {
                                                    Boolean randomGaleria = false;
                                                    int numeroRandom = default(int);

                                                    foreach (Dictionary<string, string> dic in parametrosAdicionales)
                                                    {
                                                        if (dic.ContainsKey("RandomGaleria"))
                                                        {
                                                            randomGaleria = true;
                                                            numeroRandom = Convert.ToInt32(dic["RandomGaleria"]);
                                                        }
                                                    }

                                                    DataSet dsGaleria = secInformativa.ConsultaRelacionesSecciones(dtSeccionInf.Rows[g]["intCodigo"].ToString(), "0");

                                                    if (dsGaleria != null &&
                                                        dsGaleria.Tables.Count != 0)
                                                    {
                                                        DataTable dtGaleria = dsGaleria.Tables[0];

                                                        DataView dvGaleria = new DataView(dtGaleria);
                                                        dvGaleria.RowFilter = "bitActivo = True";
                                                        dtGaleria = dvGaleria.ToTable();
                                                        /*si, llega el parametro "RandomGaleria"*/
                                                        if (randomGaleria)
                                                        {
                                                            dtGaleria = new Utils.Utils().dtRandom(dtGaleria, numeroRandom, false);
                                                        }

                                                        rptGaleria.DataSource = dtGaleria;
                                                        rptGaleria.DataBind();
                                                    }
                                                }
                                            }
                                            /*cuando hay un repetidor de links.*/
                                            if (rptlinks != null)
                                            {
                                                DataSet dsLinks = secInformativa.ConsultaRelacionesSecciones(dtSeccionInf.Rows[s]["intCodigo"].ToString(), "1");
                                                if (dsLinks != null &&
                                                    dsLinks.Tables.Count != 0)
                                                {
                                                    DataTable dtlinks = dsLinks.Tables[0];
                                                    /*cuando hay paginador*/
                                                    if (dtlPaginador != null)
                                                    {
                                                        int iCantidad = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumeroNoticiasPagina", "4"));
                                                        csGeneralsPag.Paginar(rptlinks, dtlinks, iPagina ?? 0, dtlPaginador, iCantidad);
                                                    }
                                                    else
                                                    {
                                                        rptlinks.DataSource = dtlinks;
                                                        rptlinks.DataBind();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                Repeater rptTabs = PageSource.FindControl("rptTabs") as Repeater;
                                if (rptTabs != null && nivel != null)
                                {
                                    DataSet dsSubDetalles = secInformativa.ConsultaSeccionesGeneral(
                                        null,
                                        refereSeccionPublicacion,
                                        null,
                                        "1",
                                        null,
                                        cCache.Empresa, null, null);

                                    if (dsSubDetalles != null)
                                    {
                                        try
                                        {
                                            if (rptTabs != null)
                                            {
                                                rptTabs.DataSource = dsSubDetalles.Tables[0];
                                                rptTabs.DataBind();
                                            }
                                        }
                                        catch { }
                                    }
                                }
                            }
                            else
                            {
                                if (lblTitulo != null)
                                {

                                    DataSet dsDetalles = secInformativa.ConsultaSeccionesGeneral(
                                        codigo,
                                        null, null, null, null, null, null, null);

                                    if (dsDetalles != null)
                                    {
                                        lblTitulo.Text = dsDetalles.Tables[0].Rows[0]["strTitulo"].ToString();
                                    }
                                }
                            }

                            #endregion
                            break;
                        case Enum_Tipo_Plantilla_Seccion.PlantillaContactoServicio:
                            #region [PLANTILLA CONTACTO SERVICIO]

                            //dsSecciones = secInformativa.ConsultaSeccionesGeneral(codigo, refereSeccionPublicacion, codigoPadre, nivel,
                            //    posicion, cCache.Empresa, cantidadFilas.ToString(), campoOrden);

                            //if (dsSecciones != null &&
                            //                    dsSecciones.Tables.Count != 0 && dsSecciones.Tables[0].Rows.Count != 0)
                            //{
                            //    DataTable dtSeccionInf = dsSecciones.Tables[0];
                            //    string sTipoSeccion = clsValidaciones.GetKeyOrAdd("SEC_TIPOARCHIVOHTML", "SEC_TIPOARCHIVOHTML");
                            //    //if (dtSeccionInf.Rows[0]["strRefereTipoSeccion"].ToString().Equals(sTipoSeccion))
                            //    //{
                            //    //    ActualizaHtmlArchivo(dtSeccionInf, PageSource);
                            //    //}
                            //    try
                            //    {
                            //        if (lblTitulo != null)
                            //        {
                            //            DataSet dsDetalles = null;
                            //            if (int.Parse(nivel) > 0)
                            //            {
                            //                dsDetalles = secInformativa.ConsultaSeccionesGeneral(
                            //                       dtSeccionInf.Rows[0]["intPadre"].ToString(),
                            //                       null, null, null, null, null, null, null);
                            //            }
                            //            else
                            //            {
                            //                dsDetalles = secInformativa.ConsultaSeccionesGeneral(
                            //                       codigo,
                            //                       null, null, null, null, null, null, null);
                            //            }
                            //            if (dsDetalles != null)
                            //            {
                            //                lblTitulo.Text = dsDetalles.Tables[0].Rows[0]["strTitulo"].ToString();
                            //            }
                            //        }
                            //    }
                            //    catch { }

                            //    if (rptSeccion != null)
                            //    {
                            //        string sTemaMsj = "0";
                            //        string[] sValue = csValue(PageSource);
                            //        if (sValue[7] != null)
                            //        { sTemaMsj = sValue[7]; }

                            //        DataView dvFiltro = new DataView(dtSeccionInf);
                            //        dvFiltro.RowFilter = "intTemaMensaje=" + sTemaMsj;
                            //        dtSeccionInf = dvFiltro.ToTable();
                            //        // aca se verifica si el tipo de seccion es archivohtml, para reemplazar el strHtml y luego se llena normal
                            //        rptSeccion.DataSource = dtSeccionInf;
                            //        rptSeccion.DataBind();

                            //        bool bEntra = true;

                            //        for (int s = 0; s < rptSeccion.Items.Count; s++)
                            //        {
                            //            Repeater rptDetalle = rptSeccion.Items[s].FindControl("rptSeccion") as Repeater;
                            //            Repeater rptlinks = rptSeccion.Items[s].FindControl("rptlinks") as Repeater;
                            //            DataList dtlPaginador = rptSeccion.Items[s].FindControl("dtlPaginador") as DataList;

                            //            /*cuando hay un repetidor de detalles.*/
                            //            if (rptDetalle != null)
                            //            {
                            //                DataSet dsDetalles = secInformativa.ConsultaSeccionesGeneral(
                            //                       null,
                            //                       refereSeccionPublicacion,
                            //                       null,
                            //                       clsSesiones.getIdioma(),
                            //                       dtSeccionInf.Rows[s]["intCodigo"].ToString(),
                            //                     (Convert.ToInt32(dtSeccionInf.Rows[s]["intNivel"].ToString()) + 1).ToString(),
                            //                       null,
                            //                       refereCategoria,
                            //                       clsSesiones.getAplicacion().ToString(), null, null, cantidadFilas, campoOrden);

                            //                if (dsDetalles != null &&
                            //                    dsDetalles.Tables.Count != 0)
                            //                {

                            //                    DataTable dtDetalle = dsDetalles.Tables[0];
                            //                    int iCantidad = 0;
                            //                    /*cuando hay paginador*/
                            //                    if (dtlPaginador != null)
                            //                    {
                            //                        iCantidad = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumeroNoticiasPagina", "4"));
                            //                        csGeneralsPag.Paginar(rptDetalle, dtDetalle, iPagina ?? 0, dtlPaginador, iCantidad);
                            //                    }
                            //                    else
                            //                    {
                            //                        string excluir = null;
                            //                        string incluir = null;
                            //                        string galery = null;
                            //                        foreach (Dictionary<string, string> dict in parametrosAdicionales)
                            //                        {
                            //                            if (dict.ContainsKey("EXCLUIR"))
                            //                            {
                            //                                excluir = dict["EXCLUIR"];
                            //                            }
                            //                            if (dict.ContainsKey("INCLUIR"))
                            //                            {
                            //                                incluir = dict["INCLUIR"];
                            //                            }
                            //                            if (dict.ContainsKey("GALERY"))
                            //                            {
                            //                                galery = dict["GALERY"];
                            //                            }
                            //                        }
                            //                        if (excluir != null)
                            //                        {
                            //                            DataView vistaFiltrada = new DataView(dtDetalle);
                            //                            vistaFiltrada.RowFilter = "strTitulo not like '*" + excluir + "*'";
                            //                            dtDetalle = vistaFiltrada.ToTable();
                            //                        }
                            //                        else if (incluir != null)
                            //                        {
                            //                            DataView vistaFiltrada = new DataView(dtDetalle);
                            //                            vistaFiltrada.RowFilter = "strTitulo like '*" + incluir + "*'";
                            //                            dtDetalle = vistaFiltrada.ToTable();
                            //                        }
                            //                        else if (galery != null)
                            //                        {
                            //                            bEntra = false;
                            //                            if (galery.Equals("1"))
                            //                            {
                            //                                int iTotalPaginas = dtDetalle.Rows.Count;
                            //                                int iPaginacion = iPagina ?? 0;
                            //                                int iPaginas = iTotalPaginas / iPaginacion;
                            //                                int iPaginaCompara = iPaginas * iPaginacion;
                            //                                if (iTotalPaginas > iPaginaCompara)
                            //                                    iPaginas++;

                            //                                DataTable dtPaginacion = clsDataNet.dtPaginacionStyle(iPaginas, "pagedemo _current", "pagedemo");
                            //                                rptDetalle.DataSource = dtPaginacion;
                            //                                rptDetalle.DataBind();

                            //                                for (int g = 0; g < rptDetalle.Items.Count; g++)
                            //                                {
                            //                                    int iRowPagina = g * iPaginacion;
                            //                                    int iMaxRows = iPaginacion * (g + 1);
                            //                                    if (iMaxRows > iTotalPaginas)
                            //                                        iMaxRows = iTotalPaginas;
                            //                                    Repeater rptGalery = rptDetalle.Items[g].FindControl("rptSeccion") as Repeater;
                            //                                    if (rptGalery != null)
                            //                                    {
                            //                                        DataTable dtGalery = clsDataNet.dtPaginacionDetalle(iRowPagina, iMaxRows, dtDetalle);
                            //                                        rptGalery.DataSource = dtGalery;
                            //                                        rptGalery.DataBind();
                            //                                    }
                            //                                }
                            //                            }
                            //                        }
                            //                        if (bEntra)
                            //                        {
                            //                            rptDetalle.DataSource = dtDetalle;
                            //                            rptDetalle.DataBind();
                            //                        }
                            //                    }

                            //                    for (int c = 0; c < rptDetalle.Items.Count; c++)
                            //                    {
                            //                        Label lblVideo = rptDetalle.Items[c].FindControl("lblVideo") as Label;

                            //                        /*si contiene el video*/
                            //                        if (dtDetalle.Rows[c]["strTitulo"].ToString().ToUpper().Contains("VIDEO"))
                            //                        {
                            //                            /*asignamos el html del video*/
                            //                            if (lblVideo != null)
                            //                            {
                            //                                lblVideo.Text = dtDetalle.Rows[c]["strHtml"].ToString();
                            //                                continue;
                            //                            }
                            //                        }
                            //                    }
                            //                    if (bEntra)
                            //                    {
                            //                        for (int d = 0; d < dtDetalle.Rows.Count; d++)
                            //                        {
                            //                            Repeater rptTabs = PageSource.FindControl("rptTabs") as Repeater;
                            //                            Repeater rptSubDetalle = rptDetalle.Items[d].FindControl("rptSeccion") as Repeater;
                            //                            Repeater rptlinksSub = rptDetalle.Items[d].FindControl("rptlinks") as Repeater;
                            //                            DataList dtlPaginadorSub = rptDetalle.Items[d].FindControl("dtlPaginador") as DataList;
                            //                            DataSet dsSubDetalles = new DataSet();
                            //                            if (rptSubDetalle != null)
                            //                            {
                            //                                if (iCantidad == null)
                            //                                    iCantidad = 0;
                            //                                if (iPagina == null)
                            //                                    iPagina = 0;

                            //                                if (rptTabs != null && nivel != null)
                            //                                {

                            //                                    dsSubDetalles = secInformativa.ConsultaSeccionesGeneral(
                            //                                      null,
                            //                                      refereSeccionPublicacion,
                            //                                      null,
                            //                                      clsSesiones.getIdioma(),
                            //                                      dtDetalle.Rows[Convert.ToInt32((iCantidad * iPagina) + d)]["intPadre"].ToString(),
                            //                                    (Convert.ToInt32(dtDetalle.Rows[Convert.ToInt32((iCantidad * iPagina) + d)]["intNivel"].ToString())).ToString(),
                            //                                      null,
                            //                                      refereCategoria,
                            //                                      clsSesiones.getAplicacion().ToString(), null, null);

                            //                                }
                            //                                else
                            //                                {
                            //                                    dsSubDetalles = secInformativa.ConsultaSeccionesGeneral(
                            //                                         null,
                            //                                         refereSeccionPublicacion,
                            //                                         null,
                            //                                         clsSesiones.getIdioma(),
                            //                                         dtDetalle.Rows[Convert.ToInt32((iCantidad * iPagina) + d)]["intCodigo"].ToString(),
                            //                                       (Convert.ToInt32(dtDetalle.Rows[Convert.ToInt32((iCantidad * iPagina) + d)]["intNivel"].ToString()) + 1).ToString(),
                            //                                         null,
                            //                                         refereCategoria,
                            //                                         clsSesiones.getAplicacion().ToString(), null, null);
                            //                                }

                            //                                if (dsSubDetalles != null &&
                            //                                    dsSubDetalles.Tables.Count != 0)
                            //                                {
                            //                                    DataTable dtSubDetalle = dsSubDetalles.Tables[0];

                            //                                    /*cuando hay paginador*/
                            //                                    if (dtlPaginadorSub != null)
                            //                                    {
                            //                                        iCantidad = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumeroNoticiasPagina", "4"));
                            //                                        csGeneralsPag.Paginar(rptSubDetalle, dtSubDetalle, iPagina ?? 0, dtlPaginador, iCantidad);
                            //                                    }
                            //                                    else
                            //                                    {
                            //                                        rptSubDetalle.DataSource = dtSubDetalle;
                            //                                        rptSubDetalle.DataBind();
                            //                                    }
                            //                                    try
                            //                                    {
                            //                                        if (rptTabs != null)
                            //                                        {
                            //                                            rptTabs.DataSource = dtSubDetalle;
                            //                                            rptTabs.DataBind();
                            //                                        }
                            //                                    }
                            //                                    catch { }
                            //                                    /*si hay reptidor de galerias*/
                            //                                    if (rptSubDetalle != null && rptSubDetalle.Items.Count != 0)
                            //                                    {
                            //                                        for (int g = 0; g < rptSubDetalle.Items.Count; g++)
                            //                                        {
                            //                                            Repeater rptGaleria = rptSubDetalle.Items[g].FindControl("rptGaleria") as Repeater;
                            //                                            Label lblVideo = rptSubDetalle.Items[g].FindControl("lblVideo") as Label;

                            //                                            /*si contiene el video*/
                            //                                            if (dtSubDetalle.Rows[g]["strTitulo"].ToString().Contains("Video"))
                            //                                            {
                            //                                                /*asignamos el html del video*/
                            //                                                if (lblVideo != null)
                            //                                                {
                            //                                                    lblVideo.Text = dtSubDetalle.Rows[g]["strHtml"].ToString();
                            //                                                    continue;
                            //                                                }
                            //                                            }


                            //                                            if (rptGaleria != null)
                            //                                            {
                            //                                                DataSet dsGaleria = secInformativa.ConsultaGaleria(dtSubDetalle.Rows[g]["intCodigo"].ToString());

                            //                                                if (dsGaleria != null &&
                            //                                                    dsGaleria.Tables.Count != 0)
                            //                                                {
                            //                                                    DataTable dtGaleria = dsGaleria.Tables[0];
                            //                                                    rptGaleria.DataSource = dtGaleria;
                            //                                                    rptGaleria.DataBind();
                            //                                                }
                            //                                            }
                            //                                        }
                            //                                    }
                            //                                }
                            //                            }

                            //                        }
                            //                    }
                            //                }
                            //            }
                            //            if (bEntra)
                            //            {
                            //                for (int g = 0; g < rptSeccion.Items.Count; g++)
                            //                {
                            //                    Repeater rptGaleria = rptSeccion.Items[g].FindControl("rptGaleria") as Repeater;

                            //                    if (rptGaleria != null)
                            //                    {
                            //                        Boolean randomGaleria = false;
                            //                        int numeroRandom = default(int);

                            //                        foreach (Dictionary<string, string> dic in parametrosAdicionales)
                            //                        {
                            //                            if (dic.ContainsKey("RandomGaleria"))
                            //                            {
                            //                                randomGaleria = true;
                            //                                numeroRandom = Convert.ToInt32(dic["RandomGaleria"]);
                            //                            }

                            //                        }

                            //                        DataSet dsGaleria = secInformativa.ConsultaGaleria(dtSeccionInf.Rows[g]["intCodigo"].ToString());

                            //                        if (dsGaleria != null &&
                            //                            dsGaleria.Tables.Count != 0)
                            //                        {
                            //                            DataTable dtGaleria = dsGaleria.Tables[0];

                            //                            DataView dvGaleria = new DataView(dtGaleria);
                            //                            dvGaleria.RowFilter = "bitActivo = True";
                            //                            dtGaleria = dvGaleria.ToTable();
                            //                            /*si, llega el parametro "RandomGaleria"*/
                            //                            if (randomGaleria)
                            //                            {
                            //                                dtGaleria = new Utils.Utils().dtRandom(dtGaleria, numeroRandom, false);
                            //                            }

                            //                            rptGaleria.DataSource = dtGaleria;
                            //                            rptGaleria.DataBind();
                            //                        }
                            //                    }
                            //                }
                            //                /*cuando hay un repetidor de links.*/
                            //                if (rptlinks != null)
                            //                {
                            //                    DataSet dsLinks = secInformativa.ConsultaLinks(dtSeccionInf.Rows[s]["intCodigo"].ToString(),
                            //                        dtSeccionInf.Rows[s]["intAplicacion"].ToString());
                            //                    if (dsLinks != null &&
                            //                        dsLinks.Tables.Count != 0)
                            //                    {
                            //                        DataTable dtlinks = dsLinks.Tables[0];
                            //                        /*cuando hay paginador*/
                            //                        if (dtlPaginador != null)
                            //                        {
                            //                            int iCantidad = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumeroNoticiasPagina", "4"));
                            //                            csGeneralsPag.Paginar(rptlinks, dtlinks, iPagina ?? 0, dtlPaginador, iCantidad);
                            //                        }
                            //                        else
                            //                        {
                            //                            rptlinks.DataSource = dtlinks;
                            //                            rptlinks.DataBind();
                            //                        }
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    if (lblTitulo != null)
                            //    {
                            //        tblSeccionInf otblSeccionInf = new tblSeccionInf();
                            //        otblSeccionInf.Get(codigo);
                            //        if (otblSeccionInf.Respuesta)
                            //        {
                            //            lblTitulo.Text = otblSeccionInf.strTitulo.Value;
                            //        }
                            //    }
                            //}
                            #endregion
                            break;
                        case Enum_Tipo_Plantilla_Seccion.PlantillaRegistro:
                            #region [PLANTILLA REGISTRO]

                            DataSet dsSeccionRegistro = secInformativa.ConsultaSeccionesGeneral(null, refereSeccionPublicacion, codigoPadre, nivel,
                                           posicion, cCache.Empresa, null, null);

                            Label lblDescripcionSeccion = PageSource.FindControl("lblDescripcionSeccion") as Label;
                            Label lblDescripcionDetalle = PageSource.FindControl("lblDescripcionDetalle") as Label;
                            Label lblDetalle = PageSource.FindControl("lblDetalle") as Label;

                            if (dsSeccionRegistro != null && dsSeccionRegistro.Tables.Count != 0)
                            {
                                if (lblDescripcionSeccion != null)
                                {
                                    lblDescripcionSeccion.Text = dsSeccionRegistro.Tables[0].Rows[0]["strDescripcion"].ToString();
                                }
                                if (lblDescripcionDetalle != null && dsSeccionRegistro.Tables[0].Rows.Count > 1)
                                {
                                    lblDescripcionDetalle.Text = dsSeccionRegistro.Tables[0].Rows[1]["strDescripcion"].ToString();
                                }
                                if (lblDetalle != null && dsSeccionRegistro.Tables[0].Rows.Count > 2)
                                {
                                    lblDetalle.Text = dsSeccionRegistro.Tables[0].Rows[2]["strDescripcion"].ToString();
                                }
                            }
                            #endregion
                            break;
                        case Enum_Tipo_Plantilla_Seccion.PlantillaContacto:
                            #region [PLANTILLA PlantillaContacto]
                            /*si hay parametros por url los tomamos*/
                            if (nivel.Equals("0"))
                            {
                                if (!string.IsNullOrEmpty(PageSource.Request.QueryString["SECPUB"]))
                                {
                                    refereSeccionPublicacion = PageSource.Request.QueryString["SECPUB"];
                                }
                            }
                            else
                            {
                                nivel = "0";
                            }
                            DataSet dsSeccionContactenos = secInformativa.ConsultaSeccionesGeneral(null, refereSeccionPublicacion, codigoPadre, nivel,
                                           posicion, cCache.Empresa, null, null);

                            if (dsSeccionContactenos != null && dsSeccionContactenos.Tables.Count != 0)
                            {
                                if (rptSeccion != null)
                                {
                                    rptSeccion.DataSource = dsSeccionContactenos.Tables[0];
                                    rptSeccion.DataBind();
                                }
                                if (lblTitulo != null)
                                {
                                    lblTitulo.Text = dsSeccionContactenos.Tables[0].Rows[0]["strTitulo"].ToString();
                                }

                            }
                            #endregion
                            break;
                        case Enum_Tipo_Plantilla_Seccion.PlantillaDestino:
                            #region [PLANTILLA PlantillaDestino]
                            //DataSet dsSeccionDestino = secInformativa.ConsultaSeccionesGeneral(
                            //               codigoSeccion,
                            //               refereSeccionPublicacion,
                            //               refereSeccionInformativa,
                            //               clsSesiones.getIdioma(),
                            //               codigoPadre,
                            //               nivel,
                            //               posicion,
                            //               refereCategoria,
                            //               clsSesiones.getAplicacion().ToString(), null, null);

                            //if (dsSeccionDestino != null && dsSeccionDestino.Tables.Count != 0)
                            //{
                            //    DataTable dtSeccionDestino = dsSeccionDestino.Tables[0];
                            //    if (rptSeccion != null)
                            //    {
                            //        rptSeccion.DataSource = dtSeccionDestino;
                            //        rptSeccion.DataBind();

                            //        if (dtSeccionDestino != null && dtSeccionDestino.Rows.Count != 0)
                            //        {
                            //            Repeater rptGaleria = rptSeccion.Items[0].FindControl("rptGaleria") as Repeater;
                            //            if (rptGaleria != null)
                            //            {
                            //                DataSet dsGaleria = secInformativa.ConsultaGaleria(dtSeccionDestino.Rows[0]["intCodigo"].ToString(),
                            //                    dtSeccionDestino.Rows[0]["intAplicacion"].ToString());

                            //                if (dsGaleria != null &&
                            //                    dsGaleria.Tables.Count != 0)
                            //                {
                            //                    DataTable dtGaleria = dsGaleria.Tables[0];
                            //                    rptGaleria.DataSource = dtGaleria;
                            //                    rptGaleria.DataBind();
                            //                }
                            //            }
                            //        }
                            //    }

                            //    DataSet dsDetallesDestino = secInformativa.ConsultaSeccionesGeneral(
                            //            null,
                            //            refereSeccionPublicacion,
                            //            refereSeccionInformativa,
                            //            clsSesiones.getIdioma(),
                            //            dtSeccionDestino.Rows[0]["intCodigo"].ToString(),
                            //            null,
                            //            null,
                            //            refereCategoria,
                            //            clsSesiones.getAplicacion().ToString(), null, null);

                            //    if (dsDetallesDestino != null && dsDetallesDestino.Tables.Count != 0)
                            //    {
                            //        DataTable dtDetallesDestino = dsDetallesDestino.Tables[0];

                            //        for (int c = 0; c < rptSeccion.Items.Count; c++)
                            //        {
                            //            Label lblVideo = rptSeccion.Items[c].FindControl("lblVideo") as Label;
                            //            Label lblMapa = rptSeccion.Items[c].FindControl("lblMapa") as Label;
                            //            Label lblDescripcionDatosUtiles = rptSeccion.Items[c].FindControl("lblDescripcionDatosUtiles") as Label;
                            //            Button imgButton = rptSeccion.Items[c].FindControl("imgGaleria") as Button;

                            //            foreach (DataRow fila in dtDetallesDestino.Rows)
                            //            {

                            //                /*si contiene el video*/
                            //                if (fila["strTitulo"].ToString().Contains("Video"))
                            //                {
                            //                    /*asignamos el html del video*/
                            //                    if (lblVideo != null)
                            //                    {
                            //                        lblVideo.Text = fila["strHtml"].ToString();
                            //                        continue;
                            //                    }
                            //                }
                            //                /*si contiene el mapa*/
                            //                if (fila["strTitulo"].ToString().Contains("Mapa"))
                            //                {
                            //                    /*asignamos el html del video*/
                            //                    if (lblMapa != null)
                            //                    {
                            //                        lblMapa.Text = fila["strHtml"].ToString();
                            //                        continue;
                            //                    }
                            //                }
                            //                /*si contiene el datos utiles*/
                            //                if (fila["strTitulo"].ToString().Contains("Datos"))
                            //                {
                            //                    /*asignamos la descriptcion de los datos utiles*/
                            //                    if (lblDescripcionDatosUtiles != null)
                            //                    {
                            //                        lblDescripcionDatosUtiles.Text = fila["strDescripcion"].ToString();
                            //                        continue;
                            //                    }
                            //                }


                            //                if (imgButton != null)
                            //                {   /*si contiene Galeria de Fotos*/
                            //                    if (fila["strTitulo"].ToString().Contains("Galeria"))
                            //                    {/*encontramos el boton de la galeria y asignamos el id en el javascript*/
                            //                        imgButton.OnClientClick = "javascript:AbrirGaleria(" + fila["intCodigo"].ToString() + ");";
                            //                        continue;
                            //                    }
                            //                }

                            //                /*si contiene Otros detalles*/
                            //                if (fila["strTitulo"].ToString().Contains("Otros"))
                            //                {
                            //                    //Label lblVideo = rptSeccion.Items[c].FindControl("lblVideo") as Label;
                            //                    Repeater rptSubDetalle = rptSeccion.Items[c].FindControl("rptSubDetalle") as Repeater;

                            //                    /*subdetalles*/
                            //                    DataSet dsSubDetallesDestino = secInformativa.ConsultaSeccionesGeneral(
                            //                                       null,
                            //                                       refereSeccionPublicacion,
                            //                                       refereSeccionInformativa,
                            //                                       clsSesiones.getIdioma(),
                            //                                       dtDetallesDestino.Rows[3]["intCodigo"].ToString(),
                            //                                       null,
                            //                                       null,
                            //                                       refereCategoria,
                            //                                       clsSesiones.getAplicacion().ToString(), null, null);


                            //                    if (dsSubDetallesDestino != null &&
                            //                        rptSubDetalle != null &&
                            //                        dsSubDetallesDestino.Tables.Count != 0)
                            //                    {
                            //                        rptSubDetalle.DataSource = dsSubDetallesDestino.Tables[0];
                            //                        rptSubDetalle.DataBind();
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            #endregion
                            break;
                        case Enum_Tipo_Plantilla_Seccion.PlantillaCarruselHome:
                            #region [PLANTILLA PlantillaCarrusel]

                            DataSet dsSeccionCarrusel = secInformativa.ConsultaSeccionesGeneral(codigoSeccion, refereSeccionPublicacion,
                                           codigoPadre, nivel, posicion, cCache.Empresa, null, null);

                            if (dsSeccionCarrusel != null && dsSeccionCarrusel.Tables.Count != 0)
                            {
                                DataTable dtSeccionCarrusel = dsSeccionCarrusel.Tables[0];
                                Repeater rptOfertasUl = PageSource.FindControl("rptOfertasUl") as Repeater;

                                csGeneralsPag.DividirTablaCarrusel(dtSeccionCarrusel,
                                    Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumDestinosCarruselHome", "2")), rptOfertasUl);

                                DataTable dt = new DataTable();
                                Repeater rptlinks = (Repeater)rptOfertasUl.Items[0].FindControl("rptlinks");

                                {
                                    if (rptlinks != null)
                                    {
                                        for (int g = 0; g < dtSeccionCarrusel.Rows.Count; g++)
                                        {
                                            DataSet dsLinks = secInformativa.ConsultaRelacionesSecciones(dtSeccionCarrusel.Rows[g]["intCodigo"].ToString(), "1");

                                            if (dsLinks != null &&
                                                dsLinks.Tables.Count != 0)
                                            {
                                                new Utils.Utils().AddRows(dt, dsLinks.Tables[0], g.ToString());

                                                if (dtSeccionCarrusel.Rows.Count == g + 1)
                                                {
                                                    DataTable dtlinks = new DataTable();
                                                    dtlinks = dt.Copy();
                                                    rptlinks.DataSource = dtlinks;
                                                    rptlinks.DataBind();
                                                    dt.Rows.Clear();

                                                    csGeneralsPag.DividirTablaLinks(dtlinks,
                                                    Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumDestinosCarruselLinks", "2")), rptOfertasUl);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            break;
                        case Enum_Tipo_Plantilla_Seccion.PlantillaDestinoDinamico:
                            #region [PLANTILLA PlantillaDestino]

                            //DataSet dsSeccionDestinoDinam = secInformativa.ConsultaSeccionesGeneral(
                            //               codigoSeccion,
                            //               refereSeccionPublicacion,
                            //               refereSeccionInformativa,
                            //               clsSesiones.getIdioma(),
                            //               codigoPadre,
                            //               nivel,
                            //               posicion,
                            //               refereCategoria,
                            //               clsSesiones.getAplicacion().ToString(), null, null);

                            //if (dsSeccionDestinoDinam != null && dsSeccionDestinoDinam.Tables.Count != 0)
                            //{
                            //    DataTable dtSeccionDestino = dsSeccionDestinoDinam.Tables[0];
                            //    //Label lblPaisDestino = (Label)PageSource.FindControl("lblPaisDestino");
                            //    //if (lblPaisDestino != null)
                            //    //    lblPaisDestino.Text = dtSeccionDestino.Rows[0]["intCategoria"].ToString();
                            //    if (rptSeccion != null)
                            //    {
                            //        rptSeccion.DataSource = dtSeccionDestino;
                            //        rptSeccion.DataBind();

                            //        if (dtSeccionDestino != null && dtSeccionDestino.Rows.Count != 0)
                            //        {
                            //            Repeater rptGaleria = rptSeccion.Items[0].FindControl("rptGaleria") as Repeater;

                            //            if (rptGaleria != null)
                            //            {
                            //                DataSet dsGaleria = secInformativa.ConsultaGaleria(dtSeccionDestino.Rows[0]["intCodigo"].ToString(),
                            //                    dtSeccionDestino.Rows[0]["intAplicacion"].ToString());

                            //                if (dsGaleria != null &&
                            //                    dsGaleria.Tables.Count != 0)
                            //                {
                            //                    DataTable dtGaleria = dsGaleria.Tables[0];
                            //                    rptGaleria.DataSource = dtGaleria;
                            //                    rptGaleria.DataBind();
                            //                }
                            //            }
                            //        }

                            //        DataSet dsDetallesDestino = secInformativa.ConsultaSeccionesGeneral(
                            //                null,
                            //                refereSeccionPublicacion,
                            //                refereSeccionInformativa,
                            //                clsSesiones.getIdioma(),
                            //                dtSeccionDestino.Rows[0]["intCodigo"].ToString(),
                            //                null,
                            //                null,
                            //                refereCategoria,
                            //                clsSesiones.getAplicacion().ToString(), null, null);

                            //        if (dsDetallesDestino != null && dsDetallesDestino.Tables.Count != 0)
                            //        {
                            //            DataTable dtDetallesDestino = dsDetallesDestino.Tables[0];

                            //            for (int c = 0; c < rptSeccion.Items.Count; c++)
                            //            {
                            //                Label lblVideo = rptSeccion.Items[c].FindControl("lblVideo") as Label;
                            //                Label lblMapa = rptSeccion.Items[c].FindControl("lblMapa") as Label;
                            //                Label lblDescripcionDatosUtiles = rptSeccion.Items[c].FindControl("lblDescripcionDatosUtiles") as Label;
                            //                Label lblSugerencias = rptSeccion.Items[c].FindControl("lblSugerencias") as Label;
                            //                Label lblDescripcion = rptSeccion.Items[c].FindControl("lblDescripcion") as Label;
                            //                Label lblTPestana1 = rptSeccion.Items[c].FindControl("lblTPestana1") as Label;
                            //                Label lblTPestana2 = rptSeccion.Items[c].FindControl("lblTPestana2") as Label;
                            //                Label lblTPestana3 = rptSeccion.Items[c].FindControl("lblTPestana3") as Label;


                            //                Button imgButton = rptSeccion.Items[c].FindControl("imgGaleria") as Button;

                            //                foreach (DataRow fila in dtDetallesDestino.Rows)
                            //                {
                            //                    /*si contiene el mapa*/
                            //                    if (fila["intOrden"].ToString().Equals("1"))
                            //                    {
                            //                        /*asignamos el html del video*/
                            //                        if (lblMapa != null)
                            //                        {
                            //                            lblMapa.Text = fila["strHtml"].ToString();
                            //                            continue;
                            //                        }
                            //                    }
                            //                    if (fila["intOrden"].ToString().Equals("2"))
                            //                    {
                            //                        /*asignamos la descriptcion de los datos utiles*/
                            //                        if (lblDescripcionDatosUtiles != null)
                            //                        {
                            //                            lblDescripcion.Text = fila["strDescripcion"].ToString();
                            //                            lblTPestana1.Text = fila["strTitulo"].ToString();
                            //                            continue;
                            //                        }
                            //                    }
                            //                    /*si contiene el datos utiles*/
                            //                    if (fila["intOrden"].ToString().Equals("3"))
                            //                    {
                            //                        /*asignamos la descriptcion de los datos utiles*/
                            //                        if (lblDescripcionDatosUtiles != null)
                            //                        {
                            //                            lblDescripcionDatosUtiles.Text = fila["strDescripcion"].ToString();
                            //                            lblTPestana2.Text = fila["strTitulo"].ToString();
                            //                            continue;
                            //                        }
                            //                    }
                            //                    /*si contiene el datos utiles*/
                            //                    if (fila["intOrden"].ToString().Equals("4"))
                            //                    {
                            //                        /*asignamos la descriptcion de los datos utiles*/
                            //                        if (lblDescripcionDatosUtiles != null)
                            //                        {
                            //                            lblSugerencias.Text = fila["strDescripcion"].ToString();
                            //                            lblTPestana3.Text = fila["strTitulo"].ToString();
                            //                            continue;
                            //                        }
                            //                    }

                            //                    /*si contiene Otros detalles*/
                            //                    if (fila["strTitulo"].ToString().Contains("Otros"))
                            //                    {
                            //                        //Label lblVideo = rptSeccion.Items[c].FindControl("lblVideo") as Label;
                            //                        Repeater rptSubDetalle = rptSeccion.Items[c].FindControl("rptSubDetalle") as Repeater;

                            //                        /*subdetalles*/
                            //                        DataSet dsSubDetallesDestino = secInformativa.ConsultaSeccionesGeneral(
                            //                                           null,
                            //                                           refereSeccionPublicacion,
                            //                                           refereSeccionInformativa,
                            //                                           clsSesiones.getIdioma(),
                            //                                           dtDetallesDestino.Rows[3]["intCodigo"].ToString(),
                            //                                           null,
                            //                                           null,
                            //                                           refereCategoria,
                            //                                           clsSesiones.getAplicacion().ToString(), null, null);


                            //                        if (dsSubDetallesDestino != null &&
                            //                            rptSubDetalle != null &&
                            //                            dsSubDetallesDestino.Tables.Count != 0)
                            //                        {
                            //                            rptSubDetalle.DataSource = dsSubDetallesDestino.Tables[0];
                            //                            rptSubDetalle.DataBind();
                            //                        }
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }
                            //}
                            #endregion
                            break;
                        case Enum_Tipo_Plantilla_Seccion.PlantillaFechasCategoria:
                            #region [PLANTILLA PlantillaFechasCategoria]
                            //DataSet dsSeccionFechasCateg = secInformativa.ConsultaSeccionesGeneral(
                            //               null,
                            //               refereSeccionPublicacion,
                            //               refereSeccionInformativa,
                            //               clsSesiones.getIdioma(),
                            //               codigoPadre,
                            //               nivel,
                            //               posicion,
                            //               null,
                            //               clsSesiones.getAplicacion().ToString(), null, null);

                            //Repeater rptAnios = PageSource.FindControl("rptAnios") as Repeater;

                            ////DataSet dsSeccionFechasCateg = secInformativa.ConsultaSeccionesGeneral(
                            ////               null,
                            ////               refereSeccionPublicacion,
                            ////               refereSeccionInformativa,
                            ////               clsSesiones.getIdioma(),
                            ////               codigoPadre,
                            ////               nivel,
                            ////               posicion,
                            ////               refereCategoria,
                            ////               clsSesiones.getAplicacion().ToString(), null, null);

                            //if (dsSeccionFechasCateg != null &&
                            //    dsSeccionFechasCateg.Tables.Count != 0)
                            //{
                            //    int sYear = DateTime.Now.Year;
                            //    int sMonth = 0;
                            //    try
                            //    {
                            //        if (HttpContext.Current.Request.QueryString["Year"] != null)
                            //        {
                            //            sYear = int.Parse(HttpContext.Current.Request.QueryString["Year"].ToString());
                            //        }
                            //    }
                            //    catch { }
                            //    foreach (Dictionary<string, string> param in parametrosAdicionales)
                            //    {
                            //        if (param.ContainsKey("Anio"))
                            //        {
                            //            sYear = int.Parse(param["Anio"]);
                            //        }
                            //        if (param.ContainsKey("Month"))
                            //        {
                            //            sMonth = int.Parse(param["Month"]);
                            //        }
                            //    }
                            //    //string sWhere = " anio=" + sYear.ToString();
                            //    if (sYear.Equals(0))
                            //    {
                            //        try
                            //        {
                            //            if (rptAnios != null)
                            //            {
                            //                foreach (RepeaterItem item in rptAnios.Items)
                            //                {
                            //                    Button btnAnio = item.FindControl("btnAnio") as Button;
                            //                    if (btnAnio.CssClass.Equals("botonBuscarSelected"))
                            //                    {
                            //                        sYear = int.Parse(btnAnio.Text);
                            //                        break;
                            //                    }
                            //                }
                            //            }
                            //        }
                            //        catch { }
                            //    }
                            //    string sWhere = string.Empty;
                            //    //if (sMonth > 0)
                            //    //{
                            //    //    sWhere += " and mes=" + sMonth.ToString();
                            //    //}
                            //    if (sMonth > 0)
                            //    {
                            //        sWhere += "mes=" + sMonth.ToString();
                            //    }
                            //    if (refereCategoria != null)
                            //    {
                            //        if (!refereCategoria.Equals("0"))
                            //        {
                            //            if (sWhere.Length > 0)
                            //            {
                            //                sWhere += " and strRefereCategoria='" + refereCategoria + "'";
                            //            }
                            //            else
                            //            {
                            //                sWhere += "strRefereCategoria='" + refereCategoria + "'";
                            //            }
                            //        }
                            //    }

                            //    DataTable dteccionFechasCateg = clsDataNet.dsDataWhere(sWhere, dsSeccionFechasCateg.Tables[0]);

                            //    if (rptSeccion != null)
                            //    {
                            //        bool random = false;
                            //        int numeroRandom = 0;

                            //        foreach (Dictionary<string, string> dic in parametrosAdicionales)
                            //        {
                            //            if (dic.ContainsKey("Random"))
                            //            {
                            //                random = true;
                            //                numeroRandom = Convert.ToInt32(dic["Random"]);
                            //            }
                            //        }
                            //        if (random)
                            //        {
                            //            dteccionFechasCateg = new Utils.Utils().dtRandom(dteccionFechasCateg, numeroRandom, false);
                            //        }

                            //        rptSeccion.DataSource = dteccionFechasCateg;
                            //        rptSeccion.DataBind();
                            //    }

                            //    DropDownList ddlCategoria =
                            //        PageSource.FindControl("ddlCategoria") as DropDownList;

                            //    if (ddlCategoria != null &&
                            //        !PageSource.IsPostBack)
                            //    {
                            //        /*Llenamos la categoria, primero hacemos el distinct.*/
                            //        DataSet dsCategoria = secInformativa.ConsultaSeccionesGeneral(
                            //             null,
                            //             refereSeccionPublicacion,
                            //             refereSeccionInformativa,
                            //             clsSesiones.getIdioma(),
                            //             codigoPadre,
                            //             nivel,
                            //             posicion,
                            //             null,
                            //             clsSesiones.getAplicacion().ToString(), null, null);

                            //        if (dsCategoria != null &&
                            //            dsCategoria.Tables.Count != 0)
                            //        {
                            //            DataView dtCategoria = new DataView(dsCategoria.Tables[0]);
                            //            ddlCategoria.DataSource = dtCategoria.ToTable(true, "intCategoria", "strCategoria", "strRefereCategoria");
                            //            ddlCategoria.DataTextField = "strCategoria";
                            //            ddlCategoria.DataValueField = "strRefereCategoria";
                            //            ddlCategoria.DataBind();
                            //            /*si viene la referencia la seleccionamos*/
                            //            if (!string.IsNullOrEmpty(refereCategoria))
                            //            {
                            //                ddlCategoria.SelectedValue = refereCategoria;
                            //            }
                            //        }
                            //    }

                            //    if (rptSeccion != null)
                            //    {
                            //        for (int i = 0; i < rptSeccion.Items.Count; i++)
                            //        {
                            //            Repeater rptLinks = rptSeccion.Items[i].FindControl("rptLinks") as Repeater;
                            //            if (rptLinks != null)
                            //            {
                            //                DataSet links = ConsultarLinks(dteccionFechasCateg.Rows[i]["intCodigo"].ToString());
                            //                if (links != null && links.Tables.Count != 0)
                            //                {
                            //                    rptLinks.DataSource = links.Tables[0];
                            //                    rptLinks.DataBind();
                            //                }
                            //            }
                            //        }

                            //    }
                            //    /*Llenamos los años, primero hacemos el distinct.*/
                            //    try
                            //    {
                            //        if (rptAnios != null &&
                            //            !PageSource.IsPostBack)
                            //        {
                            //            DataView dtvAnio =
                            //                new DataView(dsSeccionFechasCateg.Tables[0]);
                            //            rptAnios.DataSource = dtvAnio.ToTable(true, "Anio");
                            //            rptAnios.DataBind();

                            //        }
                            //    }
                            //    catch { }
                            //    /*recorremos el repetidor de años y buscamos el año actual*/
                            //    try
                            //    {
                            //        if (rptAnios != null)
                            //        {
                            //            foreach (RepeaterItem item in rptAnios.Items)
                            //            {
                            //                Button btnAnio = item.FindControl("btnAnio") as Button;
                            //                if (btnAnio.CommandArgument.Equals(sYear.ToString()))
                            //                {
                            //                    btnAnio.CssClass = "botonBuscarSelected";
                            //                }
                            //                else
                            //                {
                            //                    btnAnio.CssClass = "botonBuscar";
                            //                }
                            //            }
                            //        }
                            //    }
                            //    catch { }
                            //    /*Llenamos los meses, primero hacemos el distinct.*/
                            //    try
                            //    {
                            //        Repeater rptMes =
                            //            PageSource.FindControl("rptMeses") as Repeater;

                            //        DataTable dtMes = new DataTable("dtMes");
                            //        dtMes.Columns.Add("strMes");
                            //        dtMes.Columns.Add("intMes");
                            //        dtMes.Rows.Add("Enero", "1");
                            //        dtMes.Rows.Add("Febrero", "2");
                            //        dtMes.Rows.Add("marzo", "3");
                            //        dtMes.Rows.Add("Abril", "4");
                            //        dtMes.Rows.Add("Mayo", "5");
                            //        dtMes.Rows.Add("Junio", "6");
                            //        dtMes.Rows.Add("Julio", "7");
                            //        dtMes.Rows.Add("Agosto", "8");
                            //        dtMes.Rows.Add("Septiembre", "9");
                            //        dtMes.Rows.Add("Octubre", "10");
                            //        dtMes.Rows.Add("Noviembre", "11");
                            //        dtMes.Rows.Add("Diciembre", "12");

                            //        if (rptMes != null)
                            //        {

                            //            rptMes.DataSource = dtMes;
                            //            rptMes.DataBind();

                            //            for (int c = 0; c < rptMes.Items.Count; c++)
                            //            {
                            //                Repeater rptDetalles = rptMes.Items[c].FindControl("rptSeccion") as Repeater;
                            //                /*Filtramos por mes*/
                            //                DataView dtvMes =
                            //                    new DataView(dteccionFechasCateg);
                            //                dtvMes.RowFilter = "Mes = " + (c + 1).ToString() + "AND ANIO = " + sYear.ToString();

                            //                DataTable dtMesFiltrado = dtvMes.ToTable();
                            //                rptDetalles.DataSource = dtMesFiltrado;
                            //                rptDetalles.DataBind();

                            //                if (rptDetalles != null)
                            //                {
                            //                    for (int i = 0; i < rptDetalles.Items.Count; i++)
                            //                    {
                            //                        Repeater rptLinks = rptDetalles.Items[i].FindControl("rptLinks") as Repeater;
                            //                        if (rptLinks != null)
                            //                        {
                            //                            DataSet links = ConsultarLinks(dtMesFiltrado.Rows[i]["intCodigo"].ToString());
                            //                            if (links != null && links.Tables.Count != 0)
                            //                            {
                            //                                rptLinks.DataSource = links.Tables[0];
                            //                                rptLinks.DataBind();
                            //                            }
                            //                        }
                            //                    }
                            //                }
                            //            }
                            //        }
                            //    }
                            //    catch { }
                            //}

                            #endregion
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    csGeneralsPag.FinSesion();
                }
            }
            catch (Exception Ex)
            {
                clsParametros cparametros = new clsParametros();
                cparametros.Tipo = clsTipoError.DataBase;
                cparametros.Metodo = System.Reflection.MethodInfo.GetCurrentMethod().Name;
                cparametros.Message = Ex.Message;
                if (refereSeccionPublicacion != null)
                {
                    cparametros.Complemento = "Secc. Publicacion " + refereSeccionPublicacion;
                }
                if (refereSeccionInformativa != null)
                {
                    cparametros.Complemento += "  Secc. Informativa " + refereSeccionInformativa;
                }
                cparametros.Id = 0;
                ExceptionHandled.Publicar(cparametros);
            }
        }

        public void setCargar(UserControl PageSource, string CommandArgument)
        {
            csGeneralsPag.Idioma(PageSource);

            try
            {
                Label lblTitulo = PageSource.FindControl("lblTitulo") as Label;

                if (lblTitulo != null)
                {
                    if (PageSource.Request.QueryString["origen"] != null &&
                        PageSource.Request.QueryString["origen"].Equals("WebChek"))
                    {
                        lblTitulo.Text = "Web Checkin";
                    }
                }

                HtmlGenericControl cPanel = (HtmlGenericControl)PageSource.FindControl("cPanel");
                StringBuilder dPanel = new StringBuilder();
                dPanel.AppendLine(" <iframe  id=iframe scrolling='auto' src='" + CommandArgument + "' frameBorder=0  width=100% height=700></iframe>");
                cPanel.Controls.Clear();
                cPanel.InnerHtml = dPanel.ToString();
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
                cParametros.Complemento = "setCargar ";
                cParametros.ViewMessage.Add("No se ha podido vcargar la Pagina");
                cParametros.Sugerencia.Add("Por favor confirme con el administrador");
                ExceptionHandled.Publicar(cParametros);
            }
        }
        public void Load_Destinos(UserControl PageSource)
        {
            csGeneralsPag.Idioma(PageSource);

            clsParametros cParametros = new clsParametros();
            try
            {
                clsCache cCache = new csCache().cCache();
                if (cCache != null)
                {
                    DropDownList ddlDestinos = (DropDownList)PageSource.FindControl("ddlDestinos");
                    Repeater rptDestinos = (Repeater)PageSource.FindControl("rptDestinos");

                    DataSet dt_Recomendaciones = new csConsultaSecciones().ConsultaSeccionesGeneral(null, Utils.Enum_Tipo_Seccion_Publicacion.SP_DESTINOS_DESTACADOS.ToString(),null,"0", null, null, null, null);
                

                    if (dt_Recomendaciones != null && dt_Recomendaciones.Tables[0].Rows.Count > 0)
                    {
                        if (rptDestinos != null)
                        {
                            rptDestinos.DataSource = dt_Recomendaciones;
                            rptDestinos.DataBind();
                        }
                        if (ddlDestinos != null)
                        {
                            DataView dvOrden = new DataView(dt_Recomendaciones.Tables[0]);
                            dvOrden.Sort = "strTitulo ASC";
                            DataTable dtOrden = dvOrden.ToTable();
                            DataSet dsOrden = new DataSet();
                            dsOrden.Tables.Add(dtOrden);
                            clsControls.LlenaControl(ddlDestinos, dsOrden, "strTitulo", "CodigoCategoria", true);                           
                        }
                       
                    }
                }
                else
                {
                    csGeneralsPag.FinSesion();
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
                cParametros.Complemento = "Load_Destinos";
                ExceptionHandled.Publicar(cParametros);
            }
        }
    }
}
