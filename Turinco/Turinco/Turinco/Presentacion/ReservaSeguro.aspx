<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReservaSeguro.aspx.cs" Inherits="Presentacion_ReservaSeguro" %>

<%@ Register Src="../uc/ucReservaSeguro.ascx" TagName="ucReservaSeguro" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc3" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>
            Nactur
        </title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <uc6:ucEncabezado ID="UcEncabezado1" runat="server" />
    </head>
   
    <body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" class="textCenter verdana">
        <form id="form1" runat="server">
            <div id="wrap" class="container textLeft">
                
                <header>
                    <uc1:ucScriptManager ID="UcScriptManager1" runat="server" />
                    <uc7:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                </header>

                <div class="encabezadoSegurosReserva blanco arial">
				    Detalle de la reserva
                </div>

                <uc2:ucReservaSeguro ID="UcReservaSeguro1" runat="server" />

                <nav class="iblock textCenter">
                    <uc3:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                </nav>

                <footer class="full iblock">
                    <asp:HiddenField ID="hdfSesionId" runat="server" />
                    <uc8:ucPiePagina ID="UcPiePagina1" runat="server" />
                </footer>
            </div>
        </form>
    </body>
</html>
