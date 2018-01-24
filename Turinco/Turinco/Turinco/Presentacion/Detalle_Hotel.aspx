<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" EnableEventValidation="false" %>

<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc10" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc11" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucDetalleHotel.ascx" TagName="ucDetalleHotel" TagPrefix="uc2" %>
<%@ Register Src="~/uc/ucBuscador.ascx" TagName="ucBuscador" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc8" %>


<!DOCTYPE>
<html lang="es">
    <head id="Head1" runat="server">
        <title>
            Turinco
        </title>

        <meta http-equiv="Content-Type" content="text/html;" charset="utf-8" />

        <uc6:ucEncabezado ID="UcEncabezado1" runat="server" />
    </head>

    <body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" class="textCenter verdana">
        <form id="form1" runat="server">
            <div class="container textLeft" id="wrap">
                
                <header>
                    <div class="wrapper">
                        <uc11:ucScriptManager ID="UcScriptManager1" runat="server" />
                        <uc7:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                        <nav>
                            <uc5:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                        </nav>
                    </div>
                </header>
                <div id="main" style="background:#fff;">
                    <div id="content">
                        <div class="buscadorInterno iblock">
                            <uc1:ucBuscador ID="ucBuscador" runat="server" />
                        </div>
        
                        <div class="resultadoDetalleHoteles iblock">
                            <uc2:ucDetalleHotel ID="ucDetalleHotel" runat="server" />
                        </div>
                    </div>
                </div>        
            </div>   
            <footer>
                    <asp:HiddenField ID="hdfSesionId" runat="server" />
                    <uc8:ucPiePagina ID="UcPiePagina2" runat="server" />
            </footer> 
        </form>

        <script type="text/javascript" src="../App_Themes/Scripts/Ssoft/bottom.js"></script>
    </body>
</html>
