<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucIngreso.ascx.cs" Inherits="uc_ucIngreso" %>

<!--- ingresar -->
<div style="float:left; position:relative; width:100%;">
<div class="panelIngresar">
    <div id="masthead">
        <span class="head">
            <asp:Label ID="lblTituloSeccionPadre" Text="Acceso para usuarios registrados" runat="server"></asp:Label>
        </span>
    </div>
    <div class="contenidoIngresar">
        <table width="100%">
            <tr>
                <td colspan="5">
                    <asp:Repeater runat="server" ID="rptSeccion">
                        <ItemTemplate>
                            <asp:Label ID="lblDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString()) %></asp:Label>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td class="alineacionDerecha">
                    <asp:Label CssClass="textoLogin" ID="lblTEmail" runat="server" Text="E-mail"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsuario" Width="200" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" ValidationGroup="Validacion_Login" ControlToValidate="txtUsuario"
                        runat="server" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                </td>
                <td class="alineacionDerecha">
                    <asp:Label CssClass="textoLogin" ID="lblTPassword" runat="server" Text="Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" Width="200" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Validacion_Login"
                        ControlToValidate="txtPassword" runat="server" ErrorMessage="(*)"></asp:RequiredFieldValidator>
                </td>
                <td class="alineacionDerecha" colspan="2">
                    <asp:Button CssClass="link-button white" ID="btnEntrar" runat="server" ValidationGroup="Validacion_Login"
                        OnClick="btnEntrar_Click" Text="Ingresar"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:LinkButton ID="lbOlvido" runat="server" CausesValidation="false"
                        OnClick="lbOlvido_Click" Text="¿Olvidó su contraseña?"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="5" class="alineacionCentro">
                    <asp:Label ID="lblError" CssClass="Error" runat="server" ForeColor="Maroon"></asp:Label>
                    <asp:Label ID="lblContraseña" CssClass="Error" runat="server" ForeColor="Maroon"></asp:Label>
                    </td>
            </tr>
            <tr>
                
            </tr>
        </table>
    </div>
</div>

</div>