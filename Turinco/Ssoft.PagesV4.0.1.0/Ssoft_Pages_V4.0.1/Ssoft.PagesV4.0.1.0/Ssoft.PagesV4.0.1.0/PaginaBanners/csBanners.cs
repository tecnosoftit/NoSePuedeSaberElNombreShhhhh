using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ssoft.Utils;
using System.Web.UI;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SsoftQuery.Banners;

namespace Ssoft.Pages.PaginaBanners
{
    public class csBanners
    {
        /// <summary>
        /// Metodo que consulta los banners y los llena en la pagina dependiendo de las validaciones aplicadas
        /// </summary>
        /// <param name="PageSource"></param>
        /// <param name="iPos"></param>
        /// <remarks>
        /// Autor:          Juan Camilo Diaz
        /// Company:        Ssoft Colombia
        /// Fecha:          2013-12-27
        /// -------------------
        /// Control de Cambios
        /// -------------------
        /// </remarks>
        public void setLlenarBanners(UserControl PageSource, int iPos)
        {
            DataSet dsBanners = new DataSet();
            try
            {
                string PaginaActual = csGeneralsPag.PaginaActual();
                clsCache cCache = new csCache().cCache();

                csConsultasBanners cConsBanners = new csConsultasBanners();
                dsBanners = cConsBanners.ConsultaBanners(PaginaActual, cCache.Empresa);
                
                if (dsBanners != null && dsBanners.Tables.Count > 0)
                {
                    DataTable dtBannersFiltro = dsBanners.Tables[0];
                    dtBannersFiltro = setHTMLBanner(dtBannersFiltro);

                    DataView dvFiltro = new DataView(dtBannersFiltro);
                    #region Pos 0
                    if (iPos == 0)
                    {
                        HtmlGenericControl banner3 = (HtmlGenericControl)PageSource.FindControl("banner3");
                        HtmlGenericControl banner3_1 = (HtmlGenericControl)PageSource.FindControl("banner3_1");

                        dvFiltro.RowFilter = "intUbicacion = '3'";
                        dvFiltro.Table = dvFiltro.ToTable();
                        dvFiltro.RowFilter = "intOrden = 1";
                        if (dvFiltro.ToTable().Rows.Count > 0)
                        {
                            banner3.InnerHtml = dvFiltro.ToTable().Rows[0]["strHTML"].ToString();
                        }
                        if (banner3_1 != null)
                        {
                            dvFiltro.Table = dsBanners.Tables[0];
                            dvFiltro.RowFilter = "intUbicacion = '3'";
                            dvFiltro.Table = dvFiltro.ToTable();
                            dvFiltro.RowFilter = "intOrden = 2";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                banner3_1.InnerHtml = dvFiltro.ToTable().Rows[0]["strHTML"].ToString();
                            }
                        }

                    }
                    #endregion
                    #region Pos 1
                    if (iPos == 1)
                    {
                        HtmlGenericControl banner1 = (HtmlGenericControl)PageSource.FindControl("banner1");
                        HtmlGenericControl banner2 = (HtmlGenericControl)PageSource.FindControl("banner2");
                        HtmlGenericControl banner3 = (HtmlGenericControl)PageSource.FindControl("banner3");
                        HtmlGenericControl banner4 = (HtmlGenericControl)PageSource.FindControl("banner4");
                        HtmlGenericControl banner5 = (HtmlGenericControl)PageSource.FindControl("banner5");
                        HtmlGenericControl banner6 = (HtmlGenericControl)PageSource.FindControl("banner6");
                        if (banner1 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = '1'";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                                banner1.InnerHtml = dvFiltro.ToTable().Rows[0]["strHTML"].ToString().Replace("/>", "alt='" + dvFiltro.ToTable().Rows[0]["strTitulo"].ToString() + "' />");
                        }
                        if (banner2 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = '2'";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                                banner2.InnerHtml = dvFiltro.ToTable().Rows[0]["strHTML"].ToString().Replace("/>", "alt='" + dvFiltro.ToTable().Rows[0]["strTitulo"].ToString() + "' />");
                        }
                        if (banner3 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = '3'";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                                banner3.InnerHtml = dvFiltro.ToTable().Rows[0]["strHTML"].ToString().Replace("/>", "alt='" + dvFiltro.ToTable().Rows[0]["strTitulo"].ToString() + "' />");
                        }
                        if (banner4 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = '4'";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                                banner4.InnerHtml = dvFiltro.ToTable().Rows[0]["strHTML"].ToString().Replace("/>", "alt='" + dvFiltro.ToTable().Rows[0]["strTitulo"].ToString() + "' />");
                        }
                        if (banner5 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = '5'";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                                banner5.InnerHtml = dvFiltro.ToTable().Rows[0]["strHTML"].ToString().Replace("/>", "alt='" + dvFiltro.ToTable().Rows[0]["strTitulo"].ToString() + "' />");
                        }
                        if (banner6 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = '6'";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                                banner6.InnerHtml = dvFiltro.ToTable().Rows[0]["strHTML"].ToString().Replace("/>", "alt='" + dvFiltro.ToTable().Rows[0]["strTitulo"].ToString() + "' />");
                        }
                    }
                    #endregion
                    #region Pos 2
                    if (iPos == 2)
                    {
                        HtmlGenericControl bannerInterno1 = (HtmlGenericControl)PageSource.FindControl("bannerInterno1");
                        HtmlGenericControl bannerInterno2 = (HtmlGenericControl)PageSource.FindControl("bannerInterno2");
                        HtmlGenericControl bannerInterno3 = (HtmlGenericControl)PageSource.FindControl("bannerInterno3");
                        HtmlGenericControl bannerInterno4 = (HtmlGenericControl)PageSource.FindControl("bannerInterno4");
                        HtmlGenericControl bannerInterno5 = (HtmlGenericControl)PageSource.FindControl("bannerInterno5");
                        HtmlGenericControl bannerInterno6 = (HtmlGenericControl)PageSource.FindControl("bannerInterno6");
                        HtmlGenericControl bannerInterno7 = (HtmlGenericControl)PageSource.FindControl("bannerInterno7");

                        if (bannerInterno1 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 1";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                }
                                bannerInterno1.InnerHtml = sbBanners.ToString();
                            }
                        }
                        if (bannerInterno2 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 2";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString().Replace("/>", "alt='" + dvFiltro.ToTable().Rows[i]["strTitulo"].ToString() + "' />"));
                                }
                                bannerInterno2.InnerHtml = sbBanners.ToString();
                            }
                        }
                        if (bannerInterno3 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 3";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                }
                                bannerInterno3.InnerHtml = sbBanners.ToString();
                            }
                        }
                        if (bannerInterno4 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 4";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                }
                                bannerInterno4.InnerHtml = sbBanners.ToString();
                            }
                        }
                        if (bannerInterno5 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 5";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                }
                                bannerInterno5.InnerHtml = sbBanners.ToString();
                            }
                        }
                        if (bannerInterno6 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 6";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                }
                                bannerInterno6.InnerHtml = sbBanners.ToString();
                            }
                        }
                        if (bannerInterno7 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 7";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                }
                                bannerInterno7.InnerHtml = sbBanners.ToString();
                            }
                        }
                    }
                    #endregion
                    #region Pos 3
                    if (iPos == 3)
                    {
                        HtmlGenericControl bannerInf = (HtmlGenericControl)PageSource.FindControl("bannerInf");

                        dvFiltro.RowFilter = "intUbicacion = 1";
                        dvFiltro.Sort = "intOrden";
                        DataTable dtBanners = dvFiltro.ToTable();
                        try
                        {
                            /*randomizamos los resultados*/
                            int iNumBanners = Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NBannersInfHome", "3"));
                            dtBanners = new Utils.Utils().dtRandom(dtBanners, iNumBanners, false);
                        }
                        catch (Exception) { }
                        if (dtBanners.Rows.Count > 0)
                        {
                            StringBuilder sbBanners = new StringBuilder();
                            for (int i = 0; i < dtBanners.Rows.Count; i++)
                            {
                                sbBanners.AppendLine(dtBanners.Rows[i]["strHTML"].ToString());
                            }
                            bannerInf.InnerHtml = sbBanners.ToString();
                        }
                    }
                    #endregion
                    #region Pos 4
                    if (iPos == 4)
                    {
                        HtmlGenericControl dvBannersCarrusel = (HtmlGenericControl)PageSource.FindControl("dvBannersCarrusel");

                        dvFiltro.RowFilter = "intUbicacion = 1";
                        DataTable dtBanners = dvFiltro.ToTable();

                        if (dtBanners.Rows.Count > 0)
                        {
                            try
                            {
                                /*randomizamos los resultados*/
                                dtBanners = new Utils.Utils().dtRandom(dtBanners, dtBanners.Rows.Count, false);
                                StringBuilder sbBanners = new StringBuilder();
                                sbBanners.AppendLine("<ul>");
                                for (int i = 0; i < dtBanners.Rows.Count; i++)
                                {
                                    sbBanners.AppendLine("<li>");
                                    sbBanners.AppendLine(dtBanners.Rows[i]["strHTML"].ToString());
                                    sbBanners.AppendLine("</li>");
                                }
                                sbBanners.AppendLine("</ul>");
                                sbBanners.AppendLine("<div class=\"botonera\">");
                                sbBanners.AppendLine("    <a href=\"#\" class=\"prevBtn\"></a>");
                                sbBanners.AppendLine("    <a href=\"#\" class=\"pauseBtn\"></a>");
                                sbBanners.AppendLine("    <a href=\"#\" class=\"playBtn\"></a>");
                                sbBanners.AppendLine("    <a href=\"#\" class=\"nextBtn\"></a>");
                                sbBanners.AppendLine("</div>");
                                dvBannersCarrusel.InnerHtml = sbBanners.ToString();
                            }
                            catch (Exception) { }
                        }
                    }
                    #endregion
                    #region Pos 5
                    if (iPos == 5)
                    {
                        HtmlGenericControl bannerInf2 = (HtmlGenericControl)PageSource.FindControl("bannerInf2");
                        HtmlGenericControl bannerInf3 = (HtmlGenericControl)PageSource.FindControl("bannerInf3");

                        if (bannerInf2 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 2";
                            DataTable dtBanners = dvFiltro.ToTable();
                            try
                            {
                                /*randomizamos los resultados*/
                                dtBanners = new Utils.Utils().dtRandom(dtBanners, 1, false);
                            }
                            catch (Exception) { }
                            if (dtBanners.Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dtBanners.Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dtBanners.Rows[i]["strHTML"].ToString());
                                }
                                bannerInf2.InnerHtml = sbBanners.ToString();
                            }
                        }
                        if (bannerInf3 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 3";
                            DataTable dtBanners = dvFiltro.ToTable();
                            try
                            {
                                /*randomizamos los resultados*/
                                dtBanners = new Utils.Utils().dtRandom(dtBanners, 1, false);
                            }
                            catch (Exception) { }
                            if (dtBanners.Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dtBanners.Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dtBanners.Rows[i]["strHTML"].ToString());
                                }
                                bannerInf3.InnerHtml = sbBanners.ToString();
                            }
                        }
                    }
                    #endregion
                    #region Pos 6
                    if (iPos == 6)
                    {
                        Repeater dtlOfertas = (Repeater)PageSource.FindControl("dtlOfertas");

                        dvFiltro.RowFilter = "intUbicacion = 1";
                        DataTable dtBanners = dvFiltro.ToTable();

                        try
                        {
                            /*randomizamos los resultados*/
                            dtBanners = new Utils.Utils().dtRandom(dtBanners, dtBanners.Rows.Count, false);
                        }
                        catch (Exception) { }

                        foreach (DataRow dr in dtBanners.Rows)
                        {
                            dr["strHTML"] = dr["strHTML"].ToString().Replace("/>", "alt='" + dr["strTitulo"].ToString() + "' />");
                        }

                        if (dtlOfertas != null)
                        {
                            dtlOfertas.DataSource = dtBanners;
                            dtlOfertas.DataBind();
                        }
                    }
                    #endregion
                    #region Pos 7
                    if (iPos == 7)
                    {
                        HtmlGenericControl bannerHome = (HtmlGenericControl)PageSource.FindControl("bannerHome");

                        if (bannerHome != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 2";
                            DataTable dtBanners = dvFiltro.ToTable();
                            try
                            {
                                dtBanners = new Utils.Utils().dtRandom(dtBanners, dtBanners.Rows.Count, false);
                            }
                            catch (Exception) { }
                            if (dtBanners.Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dtBanners.Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dtBanners.Rows[i]["strHTML"].ToString());
                                }
                                bannerHome.InnerHtml = sbBanners.ToString();
                            }
                        }
                    }
                    #endregion
                    #region Pos 8
                    if (iPos == 8)
                    {

                        Repeater bannerInf5 = (Repeater)PageSource.FindControl("bannerInf5");
                        dvFiltro.RowFilter = "intUbicacion = 5";
                        DataTable dtBanners = dvFiltro.ToTable();

                        try
                        {
                            csGeneralsPag.DividirTablaCarrusel(dtBanners, int.Parse(clsValidaciones.GetKeyOrAdd("carrusel", "4")), bannerInf5);
                        }
                        catch (Exception) { }
                    }
                    #endregion
                    #region Pos 9
                    if (iPos == 9)
                    {
                        Repeater rptBanners = (Repeater)PageSource.FindControl("rptBanners");

                        if (rptBanners != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 1";
                            DataTable dtBanners = dvFiltro.ToTable();
                            try
                            {
                                dtBanners = new Utils.Utils().dtRandom(dtBanners, Convert.ToInt32(clsValidaciones.GetKeyOrAdd("NumBannersHome", "4")), false);
                            }
                            catch (Exception) { }
                            if (dtBanners.Rows.Count > 0)
                            {
                                rptBanners.DataSource = dtBanners;
                                rptBanners.DataBind();
                            }
                        }
                    }
                    #endregion
                    #region Pos 10
                    if (iPos == 10)
                    {
                        HtmlGenericControl bannerInterno1 = (HtmlGenericControl)PageSource.FindControl("bannerInterno1");
                        HtmlGenericControl bannerInterno2 = (HtmlGenericControl)PageSource.FindControl("bannerInterno2");
                        HtmlGenericControl bannerInterno3 = (HtmlGenericControl)PageSource.FindControl("bannerInterno3");

                        if (bannerInterno1 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 1";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                }
                                bannerInterno1.InnerHtml = sbBanners.ToString();
                            }
                        }
                        if (bannerInterno2 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 2";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                    sbBanners.AppendLine("<br />" + (dvFiltro.ToTable().Rows[i]["strDescripcion"].ToString()));
                                }
                                bannerInterno2.InnerHtml = sbBanners.ToString();

                            }
                        }
                        if (bannerInterno3 != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 3";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                    sbBanners.AppendLine("<br />" + (dvFiltro.ToTable().Rows[i]["strDescripcion"].ToString()));
                                }
                                bannerInterno3.InnerHtml = sbBanners.ToString();
                            }
                        }
                    }
                    #endregion
                    #region Pos 11
                    if (iPos == 11)
                    {
                        HtmlGenericControl banner = (HtmlGenericControl)PageSource.FindControl("bannerColumnaIzquierda");
                        
                        if (banner != null)
                        {
                            dvFiltro.RowFilter = "intUbicacion = 11";
                            dvFiltro.Sort = "intOrden";
                            if (dvFiltro.ToTable().Rows.Count > 0)
                            {
                                StringBuilder sbBanners = new StringBuilder();
                                for (int i = 0; i < dvFiltro.ToTable().Rows.Count; i++)
                                {
                                    sbBanners.AppendLine(dvFiltro.ToTable().Rows[i]["strHTML"].ToString());
                                }
                                banner.InnerHtml = sbBanners.ToString();
                            }
                        }                        
                    }
                    #endregion
                    #region Pos 12
                    if (iPos == 12)
                    {
                        Repeater dtlOfertas = (Repeater)PageSource.FindControl("dtlOfertas");

                        dvFiltro.RowFilter = "intUbicacion = 1";
                        DataTable dtBanners = dvFiltro.ToTable();

                        foreach (DataRow dr in dtBanners.Rows)
                        {
                            dr["strHTML"] = dr["strHTML"].ToString().Replace("/>", "alt='" + dr["strTitulo"].ToString() + "' />");
                        }

                        if (dtlOfertas != null)
                        {
                            dtlOfertas.DataSource = dtBanners;
                            dtlOfertas.DataBind();
                        }
                    }
                    #endregion
                }
            }
            catch { }
        }

        public DataTable setHTMLBanner(DataTable tblBanner)
        {
            try
            {
                tblBanner.Columns.Add("strHTML");
                StringBuilder sbHTML = new StringBuilder();
                for(int i = 0; i< tblBanner.Rows.Count; i++)
                {
                    if (tblBanner.Rows[i]["strCode"].ToString().Equals(
                        clsValidaciones.GetKeyOrAdd("sTipoComportamientoBannerRedirect", "REDIRECT")))
                    {
                        if (tblBanner.Rows[i]["strLinkUrl"].ToString().Trim() != "")
                        {
                            sbHTML.AppendLine("<a href='" + tblBanner.Rows[i]["strLinkUrl"].ToString() + "'>");
                            sbHTML.AppendLine("<img src='" + clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + tblBanner.Rows[i]["strImagen"].ToString() + "' />");
                            sbHTML.AppendLine("</a>");
                        }
                        else
                        {
                            sbHTML.AppendLine("<img src='" + clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + tblBanner.Rows[i]["strImagen"].ToString() + "' />");
                        }
                    }
                    if (tblBanner.Rows[i]["strCode"].ToString().Equals(
                       clsValidaciones.GetKeyOrAdd("sTipoComportamientoBannerNuevaPestaña", "NEWPEST")))
                    {
                        if (tblBanner.Rows[i]["strLinkUrl"].ToString().Trim() != "")
                        {
                            sbHTML.AppendLine("<a href='" + tblBanner.Rows[i]["strLinkUrl"].ToString() + "' target='_blank'>");
                            sbHTML.AppendLine("<img src='" + clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + tblBanner.Rows[i]["strImagen"].ToString() + "' />");
                            sbHTML.AppendLine("</a>");
                        }
                        else
                        {
                            sbHTML.AppendLine("<img src='" + clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + tblBanner.Rows[i]["strImagen"].ToString() + "' />");
                        }
                    }
                    if (tblBanner.Rows[i]["strCode"].ToString().Equals(
                      clsValidaciones.GetKeyOrAdd("sTipoComportamientoBannerNuevaVentana", "NEWVENT")))
                    {
                        if (tblBanner.Rows[i]["strLinkUrl"].ToString().Trim() != "")
                        {
                            sbHTML.AppendLine("<a href=\"#\" onclick=\"window.open('" + tblBanner.Rows[i]["strLinkUrl"].ToString() + "','mywindow', 'width=700,height=700'); return false;\">");
                            sbHTML.AppendLine("<img src='" + clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + tblBanner.Rows[i]["strImagen"].ToString() + "' />");
                            sbHTML.AppendLine("</a>");
                        }
                        else
                        {
                            sbHTML.AppendLine("<img src='" + clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + tblBanner.Rows[i]["strImagen"].ToString() + "' />");
                        }
                    }
                    tblBanner.Rows[i]["strHTML"] = sbHTML.ToString();
                    sbHTML.Clear();
                }
            }
            catch { }
            return tblBanner;
        }
    }
}
