<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice"
    EnableEventValidation="false" %>


<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc3" %>
<%@ Register Src="../uc/ucBuscador.ascx" TagName="ucBuscador" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucRotadorBanners.ascx" TagName="ucRotadorBanners" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucPlanesHome.ascx" TagName="ucPlanesHome" TagPrefix="uc9" %>
<%@ Register Src="../uc/ucSeccionInformativaHome.ascx" TagName="ucSeccionInformativaHome"
    TagPrefix="uc10" %>
<%--<%@ Register Src="../uc/ucBannersInfIndex.ascx" TagName="ucBannersInf" TagPrefix="uc10" %>--%>
<%--<%@ Register Src="../uc/ucServiciosCIndex.ascx" TagName="ucServiciosCIndex" TagPrefix="uc11" %>--%>
<%@ Register Src="../uc/ucRotadorBannersW.ascx" TagName="ucRotadorBannersW" TagPrefix="uc12" %>
<%@ Register Src="../uc/ucSecInfoHoteles.ascx" TagName="ucSecInfoHoteles" TagPrefix="uc13" %>
<%@ Register Src="../uc/ucTimeOut.ascx" TagName="ucTimeOut" TagPrefix="uc14" %>
<!doctype html>
<html lang="es">
<head>
    <title>Turinco </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="google-site-verification" content="DDB-gsEVONcq8Ow2YXzZ8bIjXJ4p14aed6RODYUA6-s" />
    <uc1:ucEncabezado ID="UcEncabezado1" runat="server" />

    <link href="../App_Themes/Estilos/Ssoft/responsiveHome.css" rel="stylesheet" type="text/css" />
</head>
<body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" lang="es">
    <form id="form1" runat="server">
    <%--<div class="barraResponsive">
                <img src="../App_Themes/Imagenes/imagesNactur/iconoFacebook.png" class="iconoFb iblock">
                <img src="../App_Themes/Imagenes/imagesNactur/iconoTwitter.png" class="iconoTw iblock">
                <img src="../App_Themes/Imagenes/imagesNactur/iconoInstagram.png" class="iconoIn iblock">
                <div class="botonMenuResponsive">
                    <a href="javascript:menuMovil();">
                        <img src="../App_Themes/Imagenes/imagesNactur/responsive_menu.png" />
                    </a>
                </div>
            </div>--%>
    <div class="container textLeft" id="wrap">
        <header>
                    <div class="wrapper">
                        <uc14:ucTimeOut ID="ucTimeOut" runat="server" />
                        <uc3:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                        <uc2:ucScriptManager ID="UcScriptManager1" runat="server" />   
                        <nav>
                            <uc7:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                        </nav>
                    </div>    
                </header>
        <div id="main">
            <!-- social -->
            <div id="social-bar">
                <ul>
                    <li><a href="https://www.facebook.com/turinco.co?fref=ts" target="_blank" title="Hazte fan">
                        <img src="../App_Themes/Imagenes/imagesTurinco/social/facebook_32.png" alt="Facebook" />
                    </a></li>
                    <li><a href="https://twitter.com/TURINCO" target="_blank" title="Sigue mis tweets">
                        <img src="../App_Themes/Imagenes/imagesTurinco/social/twitter_32.png" alt="Twitter" />
                    </a></li>
                    <li><a href="http://www.pinterest.com/turinco/" target="_blank" title="Pinterest">
                        <img src="../App_Themes/Imagenes/imagesTurinco/social/pinterest_32.png" height="32"
                            width="32" alt="Pinterest" />
                    </a></li>
                </ul>
            </div>
            <!-- ENDS social -->
            <div id="content" style="float: left;">
                <div class="ContentSuperior">
                        <div class="buscadorHome unCu">
                        <uc4:ucBuscador ID="ucBuscador" runat="server" />
                    </div>
                    <div class="contenedorRotadorHome">
                        <div class="rotadorHome">
                            <uc8:ucRotadorBanners ID="ucRotadorBanners1" runat="server" />
                        </div>
                    </div>
                </div>                
                <div id="page-content-full">
                    <div class="headline">
                        <span>OFERTAS</span>
                    </div>
                </div>
                <div class="featured" style="position: relative; height: 785px;">
                    <uc9:ucPlanesHome ID="ucPlanesHome" runat="server" />
                </div>
                <div id="page-content-full2">
                    <uc10:ucSeccionInformativaHome ID="ucSeccionInformativaHome1" runat="server" />
                </div>
                <div class="page-content-full">
                    <!-- Headline -->
                    <div class="headline">
                        MANUALES
                    </div>
                    <!-- ENDS Headline -->
                </div>
                <object data="../App_Themes/Imagenes/imagesTurinco/coverflow/index.html" type="text/html"
                    width="100%" height="420px">
                </object>
                <div class="clearfix">
                </div>
                <!-- ENDS home-gallery -->
            </div>
        </div>
    </div>
    <footer>
               <uc6:ucPiePagina ID="UcPiePagina2" runat="server" />
            </footer>
    </form>
</body>
</html>
