<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Documentos.aspx.cs" Inherits="Presentacion_Documentos" %>

<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc3" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucRotadorBanners.ascx" TagName="ucRotadorBanners" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucHerramientas.ascx" TagName="ucHerramientas" TagPrefix="uc4" %>
<!doctype html>
<html lang="es">
<head>
    <title>Nactur </title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="google-site-verification" content="DDB-gsEVONcq8Ow2YXzZ8bIjXJ4p14aed6RODYUA6-s" />
    <uc1:ucEncabezado ID="UcEncabezado1" runat="server" />
    <link href="../App_Themes/Estilos/Ssoft/responsiveHome.css" rel="stylesheet" type="text/css" />
</head>
<body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" class="textCenter verdana">
    <form id="form1" runat="server">
    <div class="barraResponsive">
        <img src="../App_Themes/Imagenes/imagesNactur/iconoFacebook.png" class="iconoFb iblock">
        <img src="../App_Themes/Imagenes/imagesNactur/iconoTwitter.png" class="iconoTw iblock">
        <img src="../App_Themes/Imagenes/imagesNactur/iconoInstagram.png" class="iconoIn iblock">
        <div class="botonMenuResponsive">
            <a href="javascript:menuMovil();">
                <img src="../App_Themes/Imagenes/imagesNactur/responsive_menu.png" />
            </a>
        </div>
    </div>
    <div class="container textLeft" id="wrap">
        
        <header class="home">
            <uc3:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
            <uc2:ucScriptManager ID="UcScriptManager1" runat="server" />
        </header>
        <section class="sectionDocumentos">
            <uc4:ucHerramientas ID="ucHerramientas" runat="server" />
        </section>
       
        <nav class="iblock textCenter">
            <uc7:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
        </nav>

        <footer>
            <asp:HiddenField ID="hdfSesionId" runat="server" />
            <uc6:ucPiePagina ID="UcPiePagina2" runat="server" />
        </footer>
    </div>
    </form>
</body>
</html>

