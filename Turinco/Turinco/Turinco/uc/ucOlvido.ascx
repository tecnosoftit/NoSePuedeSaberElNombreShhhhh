<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucOlvido.ascx.cs" Inherits="uc_ucIngreso" %>

<table width="100%">
    <tr>
        <td class="alineacionDerecha">
            <asp:Label CssClass="textoLogin" ID="lblTEmail" runat="server" Text="E-mail"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtUsuario" Width="200" runat="server"></asp:TextBox><asp:RequiredFieldValidator
                ID="RequiredFieldValidator2" ControlToValidate="txtUsuario"
                runat="server" ErrorMessage="(*)"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="alineacionCentro" colspan="2">
            <asp:LinkButton CssClass="btnOlvido" ID="lbOlvido" runat="server" CausesValidation="false"
                OnClick="lbOlvido_Click" Text="Enviar"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2" class="alineacionCentro">
            <asp:Label ID="lblError" CssClass="Error" runat="server" ForeColor="#186e9b"></asp:Label>
        </td>
    </tr>
</table>
