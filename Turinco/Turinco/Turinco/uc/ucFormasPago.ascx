<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFormasPago.ascx.cs"
    Inherits="uc_ucMediosPago" %>
<%@ Register Src="../uc/ucTemasSociales.ascx" TagName="ucTemasSociales" TagPrefix="uc2" %>
<div class="contenidoDetalleDestinos">
    <ul class="tabs" style="width:424px;float:right;">
        <asp:Repeater runat="server" ID="rptTabs">
            <ItemTemplate>
                <li><a href='#<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>'>
                    <asp:Label ID="lblTVuelos" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTitulo") %>'></asp:Label>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <asp:Repeater runat="server" ID="rptSeccion">
        <ItemTemplate>
        <div class="tab_container">
            <div class="textoFormasPago">
                <asp:Label ID="lblDescripcion" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
            </div>
            <img alt="" src="../App_Themes/Imagenes/lineaDiv.png" />
          <%--  <asp:Repeater runat="server" ID="rptSeccion">
                <ItemTemplate>--%>
                    <asp:Repeater runat="server" ID="rptSeccion">
                        <ItemTemplate>
                            <div id='<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>' class="tab_content">
                                <asp:Repeater ID="rptGaleria" runat="server" OnItemCommand="rptBotones_ItemCommand">
                                    <ItemTemplate>
                                        <div style="float:left; position:relative; width:220px; text-align:center;">
                                           
                                         <asp:Image Width="217px" runat="server" ImageUrl='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagenGaleria").ToString() %>'
                                                    ID="imgGal" />
                                           
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div style="float:left; position:relative; width:100%; margin:5px 0; text-align:center;">
                                    <uc2:ucTemasSociales ID="ucTemasSociales" runat="server" Orden='<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>' />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                <%--</ItemTemplate>
            </asp:Repeater>--%>
        </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
    <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="A1"
        runat="server">dummy</a>
    <ajax:ModalPopupExtender ID="MPEEPagina" BackgroundCssClass="ui-widget-shadow" DropShadow="false"
            runat="server" TargetControlID="A1" Drag="false" BehaviorID="MPEEPagina"
            OnOkScript="onOk()" OkControlID="Button2" PopupControlID="PanelPagina"
            CancelControlID="" PopupDragHandleControlID="Panel3" />
    <asp:Panel ID="PanelPagina" runat="server" Style="display: none">
        <div class="ventanaTerminos">
            <table width="100%" cellspacing="0" cellpadding="2">
                <tr class="tituloResultados">
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        DETALLE  &raquo;
                        <div style="float:right;">
                            <asp:Button ID="Button2" CssClass="botonCerrar" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="contenidoResultados">
                        <iframe id="iPagina" frameborder="0" width="885px" height="360px"></iframe>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
