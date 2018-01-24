<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vuelos.aspx.cs" Inherits="Presentacion_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc3" %>
<%@ Register Src="~/uc/ucBuscador.ascx" TagName="ucBuscador" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucSecInfoVuelos.ascx" TagName="ucSecInfoVuelos" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucRotadorBanners.ascx" TagName="ucRotadorBanners" TagPrefix="uc9" %>
<%@ Register Src="../uc/ucBannersInfIndex.ascx" TagName="BannersInf" TagPrefix="uc10" %>

<!doctype html>
    <html lang="es">
        <head>
            <script type="text/javascript">
                (function (i, s, o, g, r, a, m) {
                    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                        (i[r].q = i[r].q || []).push(arguments)
                    }, i[r].l =     1 * new Date(); a = s.createElement(o),
                    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
                })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

                ga('create', 'UA-2588232-1', 'tutiquete.com');
                ga('send', 'pageview');
            </script>
    
            <title>
                Nactur
            </title>
            <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
            <meta name="google-site-verification" content="DDB-gsEVONcq8Ow2YXzZ8bIjXJ4p14aed6RODYUA6-s" />
            <uc1:ucEncabezado ID="UcEncabezado1" runat="server" />
        </head>
        
        <body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" class="textCenter verdana">

            <!-- GTM -->
            <!-- Google Tag Manager -->
            <noscript>
                <iframe src="//www.googletagmanager.com/ns.html?id=GTM-KX8QZK" height="0" width="0" style="display:none;visibility:hidden"></iframe>
            </noscript>
            <script>
                (function (w, d, s, l, i) {
                    w[l] = w[l] || []; w[l].push({ 'gtm.start':
                    new Date().getTime(), event: 'gtm.js'
                }); var f = d.getElementsByTagName(s)[0],
                j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
                '//www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
                })(window, document, 'script', 'dataLayer', 'GTM-KX8QZK');
            </script>
            <!-- End Google Tag Manager -->
    
            <form id="form1" runat="server">
                <div class="container textLeft" id="wrap">
                                       
                    <header>
                        <uc2:ucScriptManager ID="UcScriptManager1" runat="server" />
                        <uc3:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                    </header>
                    
                    <div class="buscadorInterno iblock">
                        <uc4:ucBuscador runat="server" ID="ucBuscador" />
                    </div>

                    <div class="resultadoVuelos iblock">
                        <div class="encabezadoResultadoVuelos blanco arial">
                            <span id="ucResultadoVuelos_lblTResultados">
                                Ofertas áereas exclusivas
                            </span>
                        </div>


                        <div class="ofertasAereas">
                            <img src="../App_Themes/Imagenes/imagesNactur/imagenSeccionVuelos.png" class="fLeft" />

                            <ul class="listadoOfertasAereas">
                                <li>
                                    <div class="ciudades">
                                        Quito- Bogotá
                                    </div>

                                    <div class="precio">
                                        <span class="desde">
                                            Desde
                                        </span>
                                         USD 580
                                    </div>
                                </li>

                                <li>
                                    <div class="ciudades">
                                        Quito- Bogotá
                                    </div>

                                    <div class="precio">
                                        <span class="desde">
                                            Desde
                                        </span>
                                         USD 580
                                    </div>
                                </li>

                                <li>
                                    <div class="ciudades">
                                        Quito- Bogotá
                                    </div>

                                    <div class="precio">
                                        <span class="desde">
                                            Desde
                                        </span>
                                         USD 580
                                    </div>
                                </li>

                                <li>
                                    <div class="ciudades">
                                        Quito- Bogotá
                                    </div>

                                    <div class="precio">
                                        <span class="desde">
                                            Desde
                                        </span>
                                         USD 580
                                    </div>
                                </li>

                                <li>
                                    <div class="ciudades">
                                        Quito- Bogotá
                                    </div>

                                    <div class="precio">
                                        <span class="desde">
                                            Desde
                                        </span>
                                         USD 580
                                    </div>
                                </li>

                                <li>
                                    <div class="ciudades">
                                        Quito- Bogotá
                                    </div>

                                    <div class="precio">
                                        <span class="desde">
                                            Desde
                                        </span>
                                         USD 580
                                    </div>
                                </li>

                            </ul>
                        </div>

                        <%--<uc8:ucSecInfoVuelos ID="ucSecInfoVuelos1" runat="server" />--%>
                        <%--<uc10:BannersInf ID="BannersInf" runat="server" />--%>
                    </div>
                    
                    <nav class="iblock textCenter">
                        <uc7:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                    </nav>

                    <footer>
                        <asp:HiddenField ID="hdfSesionId" runat="server" />
                        <uc6:ucPiePagina ID="UcPiePagina2" runat="server" />
                    </footer>        
                </div>
            </form>
            
            <div id="ClickTaleDiv" style="display: none;">
            </div>
            
            <script type="text/javascript">
                // enable XHR wrapper
                window.ClickTaleSettings = { XHRWrapper: { Enable: true} };
            </script>
        </body>
    </html>

