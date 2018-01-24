<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pantallaAgradecimiento.aspx.cs"
    Inherits="Presentacion_indice" EnableEventValidation="false" %>

<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc3" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc7" %>
<!doctype html>
<html lang="es">
<head>
    <title>Turinco</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="google-site-verification" content="DDB-gsEVONcq8Ow2YXzZ8bIjXJ4p14aed6RODYUA6-s" />
    <uc1:ucEncabezado ID="UcEncabezado1" runat="server" />
    <link href="../App_Themes/Estilos/Ssoft/responsiveHome.css" rel="stylesheet" type="text/css" />
</head>
<body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()">
    <form id="form1" runat="server">
    <div class="container textLeft" id="wrap">
        <header>
            <div class="wrapper">
                <uc2:ucScriptManager ID="UcScriptManager1" runat="server" />
                <uc3:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />
                <nav>
                    <uc7:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                </nav>
            </div>
        </header>
        <div id="main">
            <!-- social -->
            <div id="social-bar">
                <ul>
                    <li><a href="https://www.facebook.com/turinco.co?fref=ts" target="_blank" title="Hazte fan">
                        <img src="../App_Themes/Imagenes/imagesTurinco/social/facebook_32.png" alt="Facebook" />
                    </a></li>
                    <li><a href="https://twitter.com/TURINCO" target="_blank" title="Sigue mis tweets">
                        <img src="../App_Themes/Imagenes/imagesTurinco/social/twitter_32.png" alt="Twitter" />
                    </a></li>
                    <li><a href="http://www.pinterest.com/turinco/" target="_blank" title="Pinterest">
                        <img src="../App_Themes/Imagenes/imagesTurinco/social/pinterest_32.png" height="32"
                            width="32" alt="Pinterest" />
                    </a></li>
                </ul>
            </div>
            <!-- ENDS social -->
            <div class="agradecimientos" id="content" style="float: left;">
                <div class="tituloAgradecimientos" id="masthead">
                         <span class="head">
                            Gracias por su compra                        
                         </span>
                </div>
                <div class="detalleAgradecimientos">
                    <h4>
                        A continuacion un resumen de su transaccion.
                    </h4>  
                    <br />                  
                    <table>
                        <tr>
                            <td>
                                <h5>Codigo reserva:</h5>                        
                            </td>                        
                            <td>
                                <asp:Label ID="lblCodTransaccion" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5>Valor:</h5>                        
                            </td>                        
                            <td>
                                <asp:Label ID="lblValor" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5>Moneda:</h5>                        
                            </td>                        
                            <td>
                                <asp:Label ID="lblMoneda" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5>Estado de pago:</h5>                        
                            </td>                        
                            <td>
                                <asp:Label ID="lblEstado" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </table>                        
                </div>
            </div>
        </div>
    </div>
            <footer>
                <asp:HiddenField ID="hdfSesionId" runat="server" />
                <uc6:ucPiePagina ID="UcPiePagina2" runat="server" />
            </footer>
    </form>
    <div id="ClickTaleDiv" style="display: none;">
    </div>
    <script type="text/javascript">
        // enable XHR wrapper
        window.ClickTaleSettings = { XHRWrapper: { Enable: true} };
    </script>
</body>
</html>
