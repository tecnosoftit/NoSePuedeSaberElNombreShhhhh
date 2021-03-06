﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" EnableEventValidation="false" %>

<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc11" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucResultadoPlanes.ascx" TagName="ucResultadoPlanes" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucBuscador.ascx" TagName="ucBuscador" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucTimeOut.ascx" TagName="ucTimeOut" TagPrefix="uc14" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>
            Turinco
        </title>
        
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
        <uc6:ucEncabezado ID="UcEncabezado1" runat="server" />
    </head>

    <body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" class="textCenter verdana">

        <script type="text/javascript" src="../App_Themes/Scripts/Ssoft/top.js"></script>

        <form id="form1" runat="server">        
            <div class="container textLeft" id="wrap">	
                
                <header>
                    <div class="wrapper">
                        <uc14:ucTimeOut ID="ucTimeOut" runat="server" />
	                    <uc7:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                        <uc11:ucScriptManager ID="UcScriptManager1" runat="server" />
                        <nav>
                            <uc5:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                        </nav>
                    </div>
                </header>

                <div id="main" style="background:#fff;">
                     <div id="content">
                        <div class="panelIzquierdo Izquierdo">
                            <div class="buscadorInterno">
                                <uc1:ucBuscador ID="ucBuscador" runat="server" />
                            </div>  
                        </div>
                        <div class="contenedorPlanes">
                        <uc2:ucResultadoPlanes ID="ucResultadoPlanes" runat="server" />
                        </div>
                     </div>
                </div>
                
            </div>
            <footer>
                <uc8:ucPiePagina ID="UcPiePagina2" runat="server" />
                <asp:HiddenField ID="hdfSesionId" runat="server" />
            </footer>
        </form>
        <script type="text/javascript" src="../App_Themes/Scripts/Ssoft/bottom.js"></script>
    </body>
</html>