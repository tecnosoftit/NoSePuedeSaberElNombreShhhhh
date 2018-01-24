<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" %>

<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc11" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucReservaHotel.ascx" TagName="ucReservaHotel" TagPrefix="uc2" %>
<%@ Register Src="~/uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc1" %>

<!DOCTYPE>
<html lang="es">
    <head id="Head1" runat="server">
        <script type="text/javascript" language="JavaScript">
            function Registro(i) {

                var Panel1 = document.getElementById("ucReservaHotel_pnlpasajeros");
                var Panel2 = document.getElementById("ucReservaHotel_pnlNuevosAfiliados");

                if (i = 0) {
                    Panel1.setAttribute('style', 'display:block;');
                    Panel2.setAttribute('style', 'display:none;');
                }
                else if (i = 1) {
                    Panel2.setAttribute('style', 'display:block;');
                    Panel1.setAttribute('style', 'display:none;');
                }
                else {
                    Panel1.setAttribute('style', 'display:block;');
                    Panel2.setAttribute('style', 'display:none;');
                }
            }        
        </script>

        <title>
            Turinco
        </title>

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <uc6:ucEncabezado ID="UcEncabezado1" runat="server" />
    </head>

    <body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()" class="textCenter verdana">
        <form id="form1" runat="server">
            <div id="wrap" class="container textLeft">
                
                <header>
                    <div class="wrapper">
                        <uc11:ucScriptManager ID="UcScriptManager1" runat="server" />
                        <uc7:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                        <nav>
                            <uc1:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                        </nav>
                    </div>
                </header>
            
                <div id="main">
                    <div id="content">
                        <div class="encabezadoReservaHoteles blanco arial">
				            Formulario de reserva
                        </div>
                          
                        <uc2:ucReservaHotel ID="ucReservaHotel" runat="server" /> 
                    </div>      
                </div>                    
                      
                      
            </div> 
             <%--<footer>
                    <asp:HiddenField ID="hdfSesionId" runat="server" />
                    <uc8:ucPiePagina ID="UcPiePagina2" runat="server" />
                </footer>  --%>
        </form>
    </body>
</html>
