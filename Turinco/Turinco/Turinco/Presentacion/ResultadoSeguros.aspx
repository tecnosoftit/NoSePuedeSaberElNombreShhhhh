<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultadoSeguros.aspx.cs" Inherits="Presentacion_ResultadoSeguros" %>

<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc11" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucResultadoSeguros.ascx" TagName="ucResultadoSeguros" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucBuscadorSeguro.ascx" TagName="ucBuscador" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucSeguros.ascx" TagName="ucSeguros" TagPrefix="uc3" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>
            Nactur
        </title>
        <uc6:ucEncabezado ID="UcEncabezado1" runat="server" />
    </head>
    <body class="textCenter verdana">
        <form id="form1" runat="server">
            <div class="container textLeft" id="wrap">
                
                <header>
                    <uc7:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                    <uc11:ucScriptManager ID="UcScriptManager1" runat="server" />
                </header>
                <div class="buscadorInterno iblock">
                    <div class="buscador">
                        <uc1:ucBuscador ID="ucBuscador" runat="server" />
                    </div>
                </div>

                <div class="resultadoPaquetes iblock">
                    <uc3:ucSeguros ID="ucSeguros" runat="server" />
                    <uc2:ucResultadoSeguros ID="ucResultadoSeguros" runat="server" />
                </div>
                
                <nav class="iblock textCenter">
                    <uc5:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                </nav>

                <footer class="full iblock">
                    <uc8:ucPiePagina ID="UcPiePagina2" runat="server" />
                    <asp:HiddenField ID="hdfSesionId" runat="server" />
                </footer>
            </div>
        </form>
    </body>
</html>
