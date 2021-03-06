﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" %>

<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc11" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucBannerSuperiorOff.ascx" TagName="ucBannerSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucEncabezadoPrint.ascx" TagName="ucEncabezado" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucReservaVuelosTarjeta.ascx" TagName="ucReservaVuelos" TagPrefix="uc2" %>
<%--<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc1" %>--%>
<%@ Register Src="../uc/ucTimeOut.ascx" TagName="ucTimeOut" TagPrefix="uc14" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>
            Turinco
        </title>
        
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />    
 
        <uc6:ucEncabezado ID="UcEncabezado1" runat="server" />
        
        <script type="text/javascript" language="JavaScript">
//            function popUp() {
//                $find('MPEEReserva').show();
//            }        
        </script>
    </head>

    <body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" class="textCenter verdana">
        <!-- End Google Tag Manager -->

        <script type="text/javascript" src="../App_Themes/Scripts/Ssoft/top.js"></script>
        
        <form id="form1" runat="server">
            <div id="wrap" class="container textLeft">
                
                <header>
                    <div class="wrapper">
                        <uc14:ucTimeOut ID="ucTimeOut" runat="server" />
                        <uc11:ucScriptManager ID="UcScriptManager1" runat="server" />
	                    <uc7:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                        <nav>
                             <%--<uc1:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />--%>
                        </nav>
                    </div>
                </header>
                <div id="main">
                    <div id="content">  
                        <div id="masthead">
				            <span class="head">Formulario de reserva</span>
                        </div>
                        <uc2:ucReservaVuelos ID="ucReservaVuelos" runat="server" />
                    </div>
                </div>	  
            </div>
            <footer>
                    <asp:HiddenField ID="hdfSesionId" runat="server" />
                    <uc8:ucPiePagina ID="UcPiePagina2" runat="server" />
                </footer>
        </form>
    </body>
</html>
