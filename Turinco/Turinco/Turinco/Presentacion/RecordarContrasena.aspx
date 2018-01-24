<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecordarContrasena.aspx.cs" Inherits="Presentacion_indice"
    EnableEventValidation="false" %>


<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc3" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucRecordarContrasena.ascx" TagName="ucLogin" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucRotadorBannersW.ascx" TagName="ucRotadorBannersW" TagPrefix="uc12" %>
<%@ Register Src="../uc/ucSecInfoHoteles.ascx" TagName="ucSecInfoHoteles" TagPrefix="uc13" %>
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
                 <uc4:ucLogin ID="ucLogin" runat="server" />
            </div>
        </div>
    </div>
    <footer>
               <uc6:ucPiePagina ID="UcPiePagina2" runat="server" />
            </footer>
    </form>
</body>
</html>
