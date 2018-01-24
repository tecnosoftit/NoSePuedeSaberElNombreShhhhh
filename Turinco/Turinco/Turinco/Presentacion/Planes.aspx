<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Planes.aspx.cs" Inherits="Presentacion_Planes" %>

<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc3" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucPlanes.ascx" TagName="ucPlanes" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucTimeOut.ascx" TagName="ucTimeOut" TagPrefix="uc14" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Turinco</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <uc1:ucEncabezado ID="UcEncabezado1" runat="server" />
</head>

<body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" lang="es">
    <form id="form1" runat="server">
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
					        <li>
                                <a href="https://www.facebook.com/turinco.co?fref=ts" target="_blank"  title="Hazte fan">
                                    <img src="../App_Themes/Imagenes/imagesTurinco/social/facebook_32.png"  alt="Facebook" />
                                </a>
                            </li>
					        <li>
                                <a href="https://twitter.com/TURINCO" target="_blank" title="Sigue mis tweets">
                                    <img src="../App_Themes/Imagenes/imagesTurinco/social/twitter_32.png"  alt="Twitter" />
                                </a>
                            </li>
					        <li>
                                <a href="http://www.pinterest.com/turinco/" target="_blank" title="Pinterest">
                                    <img src="../App_Themes/Imagenes/imagesTurinco/social/pinterest_32.png" height="32" width="32" alt="Pinterest" />
                                </a>
                            </li>
				        </ul>
			        </div>
		    <!-- ENDS social -->  
            <div id="content">        
                    <uc8:ucPlanes ID="ucPlanes" runat="server" />
            </div>
        </div>
    </div>
    <footer>
         <uc6:ucPiePagina ID="UcPiePagina2" runat="server" />
         <asp:HiddenField ID="hdfSesionId" runat="server" />
    </footer>
    </form>
    <script type="text/javascript" src="../App_Themes/Scripts/Ssoft/bottom.js"></script>
</body>
</html>
