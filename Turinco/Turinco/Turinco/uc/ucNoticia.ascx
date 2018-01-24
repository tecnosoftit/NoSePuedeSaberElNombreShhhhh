<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNoticia.ascx.cs" Inherits="uc_ucNoticia" %>

<%--<%@ Register Src="ucEnvioAmigo.ascx" TagName="ucEnvioAmigo" TagPrefix="uc2" %>
<%@ Register Src="ucEnvioAmigoDesc.ascx" TagName="ucEnvioAmigo" TagPrefix="uc8" %>--%>

<div class="tituloNoticias">
    <asp:Label ID="lblTUltimasNoticias" runat="server" Text="NOTICIAS &raquo;"></asp:Label>
</div>
<div class="contenidoNoticiasDetalle">
    <asp:Repeater runat="server" ID="rptSeccion">
        <ItemTemplate>
            <div class="fotoNoticias">
                <img alt="" src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen").ToString() %>' />
            </div>
            <asp:Label CssClass="tituloNoticiaDetalle" ID="lblTitulo" runat="server"><%# DataBinder.Eval(Container,"DataItem.strTitulo") %></asp:Label>
            <asp:Label ID="lblDescripcion" runat="server"><%# DataBinder.Eval(Container,"DataItem.strDescripcion") %></asp:Label>
            <asp:Label ID="lblFecha" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dtmFecha") %>'></asp:Label>
            <asp:Repeater ID="rptSeccion" runat="server">
                <ItemTemplate>
                    <asp:Label ID="strDescripcion" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDescripcion") %>'></asp:Label>
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="compartirNoticia" style="display:none">
    <input id="btn1" type="button" class="botonEnviar" runat="server" onclick='javascript:SetEnviarDesc();'/>
</div>

<!-- Enviar a un amigo -->
<ajax:ModalPopupExtender ID="MPEEEnvioAmigo" BackgroundCssClass="ui-widget-shadow"
    DropShadow="false" runat="server" TargetControlID="dummyLink2" Drag="false" BehaviorID="MPEEEnvioAmigo"
    OnOkScript="" OkControlID="btnCerrar1" EnableViewState="true" PopupControlID="PanelEnvio" />
<asp:Panel runat="server" ID="PanelEnvio">
     <div class="ventana2">
        <table width="100%" cellspacing="0" cellpadding="2">
            <tr class="tituloResultados">
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTEnviarAmigo" runat="server" Text="ENVIAR A UN AMIGO &raquo;"></asp:Label>
                    <div style="float:right;">
                        <asp:Button ID="btnCerrar1" CssClass="botonCerrar" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%--<uc2:ucEnvioAmigo ID="ucEnvioAmigo" runat="server" />--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink2"
    runat="server">dummy</a> 
    
<!-- Enviar a un amigo Descripcion -->
<ajax:ModalPopupExtender ID="MPEEEnvioAmigoDesc" BackgroundCssClass="ui-widget-shadow"
    DropShadow="false" runat="server" TargetControlID="dummyLink5" Drag="false" BehaviorID="MPEEEnvioAmigoDesc"
    OnOkScript="" OkControlID="btnCerrarEnv" EnableViewState="true" PopupControlID="PanelEnvioDesc" />
<asp:Panel runat="server" ID="PanelEnvioDesc">
     <div class="ventana2">
        <table width="100%" cellspacing="0" cellpadding="2">
            <tr class="tituloResultados">
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="ENVIAR A UN AMIGO &raquo;"></asp:Label>
                    <div style="float:right;">
                        <asp:Button ID="btnCerrarEnv" CssClass="botonCerrar" runat="server" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%--<uc8:ucEnvioAmigo ID="ucEnvioAmigo1" runat="server" />--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink5"
    runat="server">dummy</a> 

