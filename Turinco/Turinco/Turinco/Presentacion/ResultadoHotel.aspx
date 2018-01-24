<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" EnableEventValidation="false" %>
<%@ Register Src="../uc/ucEncabezadoTurinco.ascx" TagName="ucEncabezado" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc3" %>
<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucResultadoHotel.ascx" TagName="ucResultadoHotel" TagPrefix="uc5" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc7" %>
<%@ Register Src="~/uc/ucBuscador.ascx" TagName="ucBuscador" TagPrefix="uc8" %>

<!DOCTYPE html>

<html lang="es">
    <head runat="server">
        <title>
            Turinco
        </title>

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

        <uc1:ucEncabezado ID="UcEncabezado1" runat="server" />
    </head>

    <body class="textCenter verdana">
        <form id="form1" runat="server">
            <div class="container textLeft" id="wrap">
                <header>
                    <div class="wrapper">
                        <uc2:ucScriptManager ID="UcScriptManager1" runat="server" />
                        <uc3:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />   
                        <nav>
                            <uc4:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />
                        </nav>
                    </div>
                </header>

                <div id="main"> 
                    <div id="content">
                    <div class="buscadorInterno iblock buscadorInternoFiltro Izquierdo">
                        <div class="panelIzquierdo ">
                            <div class="buscadorInterno">
                                <uc8:ucBuscador ID="ucBuscador" runat="server" />
                            </div>
                        </div>
    
                        <%--<div class="filtro azulOscuro">
                            <h3 class="azul">
                                Filtrar hotel por
                            </h3>
                            <h4 class="azul">
                                Estrellas
                            </h4>
                            <%--<p class="PFiltroPrecio">
                                <asp:RadioButtonList ID="chkCategoria" AutoPostBack="true" runat="server" RepeatDirection="Vertical"
                                    OnSelectedIndexChanged="chkCategoria_SelectedIndexChanged" CssClass="filtroEstrellas">
                                </asp:RadioButtonList>
                            </p>--%>
                            <h4>
                                Nombre del hotel
                            </h4>
                            <%--<div>
                                <asp:TextBox ID="txtNombreHotel" runat="server"></asp:TextBox>
                                <asp:Button ID="btnFiltrar" runat="server" Text="Aplicar" OnClick="btnFiltrar_Click" CssClass="btnAzul" />
                                <asp:Button ID="btnQuitar" runat="server" Visible="false" Text="Quitar" OnClick="btnFiltrar_Click" />
                            </div>--%>
                        </div>
                        <div class="resultadoHoteles Derecho" style="background:#fff;" >
                            <uc5:ucResultadoHotel ID="resultadoshotel" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
                
                <footer>
                    <uc7:ucPiePagina ID="UcPiePagina2" runat="server" />                    
                    <asp:HiddenField ID="hdfSesionId" runat="server" />
                </footer>
            </div>      
        </form>
    </body>
</html>
