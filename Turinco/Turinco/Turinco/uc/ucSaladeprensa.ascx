<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSaladeprensa.ascx.cs"
    Inherits="uc_ucSaladeprensa" %>

<div class="panelResultados" style="margin-top:10px;">
    <div class="tituloResultados">
        <asp:Label ID="lblTituloSeccionPadre" Text="SALA DE PRENSA &raquo;" runat="server"></asp:Label>
    </div>
    <div class="contenidoResultado" style="margin-top:10px;">
        <div class="utilitarios">
            <ul>
                <asp:Repeater runat="server" ID="rptOfertasUl">
                    <ItemTemplate>
                        <li>
                            <asp:Repeater runat="server" ID="rptOfertasDiv">
                                <ItemTemplate>
                                    <div class="planes">
                                        <div class="imagenPlanes">
                                            <img src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen")%>' alt="" />
                                            <div class="tituloPlan">
                                                <%# DataBinder.Eval(Container,"DataItem.strTitulo") %>
                                            </div>
                                            <a href='#this' onclick='javascript:SetIdPagina("../Presentacion/SeccInformativa.aspx?CODSEC=<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>");'  > <input id='btnFormasdepago' class="verMas" type="button" runat="server"  value="Ver más" /></a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <asp:Label ID="lblError" runat="server" ForeColor="#186e9b"></asp:Label>
    </div>
</div>

<ajax:ModalPopupExtender ID="MPEEPagina" BackgroundCssClass="ui-widget-shadow" DropShadow="false"
      runat="server" TargetControlID="dummyLink" Drag="false" BehaviorID="MPEEPagina"
      OnOkScript="onOk()" OkControlID="Button2" PopupControlID="PanelPagina" />
<asp:Panel ID="PanelPagina" runat="server">
    <asp:Panel ID="Panel3" runat="server">
        <div class="ventanaSala">
            <table width="100%" cellspacing="0" cellpadding="2">
                <tr class="tituloResultados">
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" Text="DETALLE &raquo;"></asp:Label>
                        <div style="float:right;">
                            <asp:Button ID="Button2" CssClass="botonCerrar" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <iframe id="iPagina" frameborder="0" width="630px" height="170px"></iframe>
                    </td>
                </tr>
            </table>
        </div>        
    </asp:Panel>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink"
    runat="server">dummy</a> 







