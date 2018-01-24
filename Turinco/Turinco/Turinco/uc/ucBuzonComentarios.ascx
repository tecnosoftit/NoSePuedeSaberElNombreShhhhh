<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuzonComentarios.ascx.cs"
    Inherits="uc_ucBuzonComentarios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:HiddenField ID="HidenTemaMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HidenTipoMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HidenRefereTemaMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HidenStrTipoMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HidenStrTemaMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HFCliente" Value="0" runat="server" />
<div id="dvContactenos" style="padding:10px 0;">
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
            <table id="tblContactenos" cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 160px;">
                        <asp:Label CssClass="boldContacto" ID="lblNombre" runat="server" Text="Nombre"></asp:Label></td>
                    <td>
                        <asp:TextBox Width="220" ID="txtNombres" runat="server"></asp:TextBox>&nbsp;(*)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="boldContacto" ID="LblApellido" runat="server" Text="Apellido"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox Width="220" ID="txtApellidos" runat="server"></asp:TextBox>&nbsp;(*)
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="boldContacto" ID="lblEmail" runat="server" Text="E-mail"></asp:Label></td>
                    <td>
                        <asp:TextBox Width="220" ID="txtEmail" runat="server"></asp:TextBox>&nbsp;(*)
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Direccion de correo invalida" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="GrupoContactenos"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label CssClass="boldContacto" ID="lblTelefono" runat="server" Text="Teléfono"></asp:Label></td>
                    <td>
                        <asp:TextBox Width="220" ID="txtTelefono" runat="server"></asp:TextBox>&nbsp;(*)
                    </td>
                </tr>
            </table>
            <div id="dvFormulario" runat="server">
            </div>
            <table cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 160px;">
                        <asp:Label CssClass="boldContacto" ID="lblComentarios" runat="server" Text="Comentarios"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtComentarios" TextMode="MultiLine" Rows="5" runat="server" Width="220px"></asp:TextBox>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style= "padding: 20px 0 0 265px;">
                        <ajax:UpdateProgress ID="udpEperar" runat="server">
                            <ProgressTemplate>
                                <div class="progressbar">
                                </div>
                                <img alt="" src="../App_Themes/Imagenes/loading.gif" /><br />
                                <asp:Label ID="lblEsperar" runat="server" Text="Espere por favor..."></asp:Label>
                            </ProgressTemplate>
                        </ajax:UpdateProgress>
                        <asp:Button CssClass="botonBuscar" ID="lbEnviar" runat="server" Text="Comentar" OnClick="btnEnviar_Click"
                            OnClientClick="javascript:RecorrerFormContactenos();" />
                    </td>
                </tr>
                <tr>
                    <td class="alineacionCentro" colspan="2">
                        <asp:Label ID="LblError" runat="server" ForeColor="#186e9b"></asp:Label>
                    </td>
                </tr>
                <tr style="visibility: hidden">
                    <td style="text-align: right">
                        <asp:Label CssClass="boldContacto" ID="lblTipoMensaje" runat="server" Text="Tipo mensaje"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlTipoMensaje" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoMensaje_SelectedIndexChanged">
                        </asp:DropDownList>&nbsp;(*)
                    </td>
                </tr>
                <tr style="visibility: hidden">
                    <td style="text-align: right">
                        <asp:Label CssClass="boldContacto" ID="TemaMensaje" runat="server" Text="Tema mensaje"></asp:Label></td>
                    <td>
                        <asp:DropDownList ID="ddlTemamensaje" runat="server" OnSelectedIndexChanged="ddlTemamensaje_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Value="" Text="Seleccionar"></asp:ListItem>
                        </asp:DropDownList>&nbsp;(*)
                    </td>
                </tr>
                <tr style="visibility: hidden">
                    <td style="text-align: right">
                        <asp:Label CssClass="boldContacto" ID="lblpais" runat="server" Text="País"></asp:Label></td>
                    <td visible="false">
                        <asp:DropDownList ID="ddlPais" runat="server" Width="150">
                        </asp:DropDownList>&nbsp;(*)
                    </td>
                </tr>
                <tr style="visibility: hidden">
                    <td style="text-align: right">
                        <asp:Label CssClass="boldContacto" ID="lblCiudad" runat="server" Text="Ciudad"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCiudad" runat="server" Text="..."></asp:TextBox>&nbsp;(*)
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtnoticia" runat="server" Visible="false"></asp:TextBox>
</div>
