<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFormularioContacto.ascx.cs" Inherits="uc_ucFormularioContacto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:HiddenField ID="HidenTemaMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HidenTipoMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HidenRefereTemaMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HidenStrTipoMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HidenStrTemaMensaje" Value="0" runat="server" />
<asp:HiddenField ID="HFCliente" Value="0" runat="server" />

<div id="dvContactenos">
    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
        <ContentTemplate>
            <table id="tblContactenos" cellpadding="2" cellspacing="0">
                <tr>
                    <td colspan="2" style="width:100%;">
                        <asp:Label  ID="lblNombre" runat="server" Text="Nombres" class="TitulosFormulario"></asp:Label>
                        <asp:TextBox ID="txtNombres" runat="server" class="CajasFormulario"></asp:TextBox>
                    </td>
                </tr> 

                <tr>
                    <td colspan="2" style="width:100%;">
                        <asp:Label ID="lblpais" runat="server" Text="País" class="TitulosFormulario"></asp:Label>
                        <asp:DropDownList ID="ddlPais" runat="server" class="CajasFormulario" style="height: 22px!important;margin-left: 0px; width: 66%!important;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width:100%;" >
                        <asp:Label ID="lblTelefono" runat="server" Text="Teléfono" class="TitulosFormulario"></asp:Label>
                        <asp:TextBox ID="txtTelefono" runat="server" class="CajasFormulario"></asp:TextBox>
                    </td>
                </tr>
                <td style="text-align: left">
                        <asp:Label CssClass="boldContacto" ID="lblCiudad" runat="server" Text="Ciudad"></asp:Label>
                     </td>
                    <td>
                        <asp:TextBox ID="txtCiudad" runat="server" Text=".."></asp:TextBox>&nbsp;<span style="color:#1D5B9E">(*)</span>
                    </td>             
                <tr style="display:none;">
                    <td style="text-align: left">
                        <asp:Label CssClass="boldContacto" ID="LblApellido" runat="server" Text="Apellido"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtApellidos" runat="server" Text=".."></asp:TextBox>&nbsp;<span style="color:#1D5B9E">(*)</span>
                    </td>
                </tr>

                <tr>
                    <td colspan="2" style="width:100%;" >
                        <asp:Label ID="lblTipoMensaje" runat="server" Text="Tipo mensaje" class="TitulosFormulario"></asp:Label>
                        <asp:DropDownList ID="ddlTipoMensaje" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoMensaje_SelectedIndexChanged" class="CajasFormulario" style="height: 22px!important;margin-left: 0px; width: 66%!important;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr> 
                    <td colspan="2" style="width:100%;" >
                        <asp:Label ID="TemaMensaje" runat="server" Text="Tema del mensaje" class="TitulosFormulario"></asp:Label>
                        <asp:DropDownList ID="ddlTemamensaje" runat="server" OnSelectedIndexChanged="ddlTemamensaje_SelectedIndexChanged" AutoPostBack="true" style="height: 22px!important;margin-left: 0px; width: 66%!important;">
                            <asp:ListItem Value="" Text="Seleccionar" class="CajasFormulario"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>                      
            
                <tr>
                    <td colspan="2" style="width:100%;">
                        <asp:Label ID="lblEmail" runat="server" Text="E-mail" class="TitulosFormulario"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" class="CajasFormulario"></asp:TextBox>                        
                    </td>

                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Direccion de correo invalida" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            ValidationGroup="GrupoContactenos"></asp:RegularExpressionValidator>
                    </td>
                </tr>
              
            </table>                                        
            <div id="dvFormulario" runat="server">
            </div>

            <table cellpadding="2" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2">
                        <asp:Label CssClass="boldContacto" ID="lblComentarios" runat="server" Text="Mensaje"></asp:Label>
                        <asp:TextBox ID="txtComentarios" TextMode="MultiLine" Rows="5" runat="server" Width="100%" Height="60px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">
                        <ajax:UpdateProgress ID="udpEperar" runat="server">
                            <ProgressTemplate>
                                <div class="progressbar">
                                </div>
                                <img alt="" src="../App_Themes/Imagenes/loading.gif" /><br />
                                <asp:Label ID="lblEsperar" runat="server" Text="Espere por favor..."></asp:Label>
                            </ProgressTemplate>
                        </ajax:UpdateProgress>
                    </td>
                </tr>
                <tr class="contenedorBotonEnviar">
                    <td colspan="2">
                        <asp:Button CssClass="link-button" ID="lbEnviar" runat="server" Text="Enviar" OnClick="btnEnviar_Click" OnClientClick="javascript:RecorrerFormContactenos();" />
                    </td>
                </tr>
                <tr>
                    <td class="alineacionCentro" colspan="2">
                        <asp:Label ID="LblError" runat="server" ForeColor="#186e9b"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="alineacionCentro" runat="server" id="Div1">
    </div>
</div>