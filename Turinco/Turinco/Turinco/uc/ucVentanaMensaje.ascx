<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVentanaMensaje.ascx.cs"
    Inherits="uc_ucVentanaMensaje" %>
    
<table width="300">
    <tr>
        <td>            
            <asp:Label CssClass="recordReserva" runat="server" ID="lblMensaje"></asp:Label>            
        </td>
    </tr>
    <tr>
        <td style="text-align:center">
            <asp:Button CssClass="botonBuscar" runat="server" ID="btnContinuar" Text="Continuar" OnClick="btnContinuar_Click" />
        </td>
    </tr>
</table>