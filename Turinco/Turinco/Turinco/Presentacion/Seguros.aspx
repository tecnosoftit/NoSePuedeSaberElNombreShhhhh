<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" EnableEventValidation="false" %>

<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc3" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucSeguros.ascx" TagName="ucSeguros" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucBuscadorSeguro.ascx" TagName="ucBuscadorSeguro" TagPrefix="uc8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>
            Nactur
        </title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <uc1:ucEncabezado ID="UcEncabezado1" runat="server" />
    </head>

    <body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" id="quienes" class="textCenter verdana">
        <form id="form1" runat="server">
            <div class="container textLeft" id="wrap">
                
	            <header>
                    <uc2:ucScriptManager ID="UcScriptManager1" runat="server" />
	                <uc3:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
	            </header>

                <div class="buscadorInterno iblock">
                    <div class="buscador">
                        <uc8:ucBuscadorSeguro ID="ucBuscadorSeguro" runat="server" />
                    </div>
                </div>
                
                <div class="resultadoPaquetes iblock">
                    <uc5:ucSeguros ID="ucSeguros" runat="server" />
                </div>
	            
                <nav class="iblock textCenter">
                    <uc4:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                </nav>

                <footer>
                    <asp:HiddenField ID="hdfSesionId" runat="server" />
                    <uc7:ucPiePagina ID="UcPiePagina2" runat="server" />
                </footer>
            </div>    
        </form>
    </body>
</html>
