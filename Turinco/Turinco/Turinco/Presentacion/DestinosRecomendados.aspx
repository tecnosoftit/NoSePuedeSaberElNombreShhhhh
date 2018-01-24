<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DestinosRecomendados.aspx.cs" Inherits="Presentacion_DestinosRecomendados" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc11" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucDestinosRecomendados.ascx" TagName="ucDestinosRecomendados" TagPrefix="uc2" %>
<%@ Register Src="~/uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>
            Nactur
        </title>

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <uc6:ucEncabezado ID="UcEncabezado1" runat="server" />
        <script type="text/javascript">
            
            function mapa() {

                var session_value = '<%=Session["Mapa"]%>';
                var map = document.getElementById("myP");
               
                if (map.innerHTML.toString().length < 2) {
                    map.innerHTML = session_value;                   
                }    

       
        }

        </script>
    </head>

    <body class="textCenter verdana">
        <form id="form1" runat="server">
            <div class="container textLeft" id="wrap">
                
                <header>
                    <uc11:ucScriptManager ID="UcScriptManager1" runat="server" />
	                <uc7:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                </header>

                <div class="encabezadoDetallePaquetes verdeOscuro arial">
				    Paquetes
                </div>

                <uc2:ucDestinosRecomendados ID="ucDestinosRecomendados" runat="server" />
                
                <nav class="iblock textCenter">
                    <uc1:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                </nav>

                <footer>
                    <asp:HiddenField ID="hdfSesionId" runat="server" />
                    <uc8:ucPiePagina ID="UcPiePagina2" runat="server" />
                </footer>         
            </div>
        </form>
    </body>
</html>
