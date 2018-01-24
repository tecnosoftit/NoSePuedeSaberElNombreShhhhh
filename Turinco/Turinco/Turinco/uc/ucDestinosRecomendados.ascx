<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDestinosRecomendados.ascx.cs"
    Inherits="uc_ucDestinosRecomendados" %>
<%@ Register Src="ucMenuDetalle.ascx" TagName="ucMenuDetalle" TagPrefix="uc2" %>
<%@ Register Src="ucResultadoPlanesDestinos.ascx" TagName="ucResultadoPlanesDestinos"
    TagPrefix="uc1" %>
<%@ Register Src="ucMapaDestino.ascx" TagName="ucMapaDestino" TagPrefix="uc3" %>
<div class="banners" id="banner1" runat="server">
</div>
<div class="selectRecomendados">
    <asp:Label ID="lblTdestinos" runat="server" Text="Selecciona el destino de tu interés: "></asp:Label>
    <asp:DropDownList ID="ddlDestinos" runat="server" OnSelectedIndexChanged="ddlDestinos_SelectedIndexChanged"
        AutoPostBack="true">
    </asp:DropDownList>
</div>
<div class="iblock full detallePaquetes">
    <asp:Repeater runat="server" ID="rptSeccion">
        <ItemTemplate>
            <img id="imgImagen" class="fLeft imagen" runat="server" alt='<%# DataBinder.Eval(Container.DataItem, "strTitulo") %>' src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen") %>' />
            <div class="iblock contenedorDatosPaquetes datosDestinosRecomendados">
                <div class="titulo azulClaro bold full iblock">
                    <asp:Label ID="lblTitulo" runat="server" Text=""><%# DataBinder.Eval(Container.DataItem, "strTitulo") %></asp:Label>
                </div>
                <div class="detalle azulOscuro iblock full">
                    <asp:Label ID="lblDesctipcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem, "strDescripcion").ToString())%></asp:Label>
                </div>
            </div>
            <script>
                $(function () {
                    $("#tabsPaquetes").tabs();
                });
            </script>
            <div id="tabsPaquetes" class="iblock full">
                <ul class="iblock full arial">
                    <li><a href="#planes">
                        <asp:Label ID="lblTVuelos" runat="server" Text="Tours"></asp:Label>
                    </a></li>
                    <li><a href="#sugerencias">
                        <asp:Label ID="Label1" runat="server" Text="Sugerencias"></asp:Label>
                    </a></li>
                    <li><a href="#datos">
                        <asp:Label ID="Label2" runat="server" Text="Datos Útiles"></asp:Label>
                    </a></li>
                    <li><a href="#mapa" onclick="javascript:mapa();">
                        <asp:Label ID="Label4" runat="server" Text="Mapas"></asp:Label>
                    </a></li>
                    <li><a href="#galeriadestino">
                        <asp:Label ID="Label5" runat="server" Text="Documentación"></asp:Label>
                    </a></li>
                </ul>
                <div id="planes" class="planesRecomendados">
                    <uc1:ucResultadoPlanesDestinos ID="UcResultadoPlanesDestinos" runat="server" />
                </div>
                <div id="sugerencias">
                    <uc2:ucMenuDetalle ID="UcMenuSubDetalle1" runat="server" TipoSeccion="SP_DESTINOS_DESTACADOS"
                        Nivel="1" Orden="3" />
                </div>
                <div id="datos">
                    <uc2:ucMenuDetalle ID="UcMenuDetalle1" runat="server" TipoSeccion="SP_DESTINOS_DESTACADOS"
                        Nivel="1" Orden="1" />
                </div>
                <div id="mapa">
                    <uc3:ucMapaDestino ID="UcMapaDestino1" runat="server" TipoSeccion="SP_DESTINOS_DESTACADOS"
                        Nivel="1" Orden="4" />
                </div>
                <div id="galeriadestino">
                    <uc2:ucMenuDetalle ID="UcMenuDetalle2" runat="server" TipoSeccion="SP_DESTINOS_DESTACADOS"
                        Nivel="1" Orden="2" />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
