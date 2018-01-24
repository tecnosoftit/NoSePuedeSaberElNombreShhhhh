<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucVentanaConfirmacion.ascx.cs"
    Inherits="uc_ucVentanaConfirmacion" %>
    
<asp:Label ID="lblTReserva" runat="server" Text="Tu reserva ha sido solicitada bajo el codigo de reserva"></asp:Label>
<asp:Label CssClass="recordReserva" runat="server" ID="lblRecord"></asp:Label>&nbsp;
y el localizador
<asp:Label CssClass="recordReserva" runat="server" ID="lblProyecto"></asp:Label>

<div style="margin:0 auto;">
    <asp:Button CssClass="botonSeccion" runat="server" ID="btnOfertas" Text="Reservar vuelos" OnClick="btnOfertas_Click" />
    <asp:Button CssClass="botonSeccion" runat="server" ID="btnTour" Text="Reservar tour complementario" OnClick="btnTour_Click" />
    <asp:Button CssClass="botonSeccion" runat="server" ID="btnContinuar" Text="Continuar" OnClick="btnContinuar_Click" />
    <asp:Button CssClass="botonSeccion" runat="server" ID="btnPoliticas" Text="Continuar" OnClick="btnPoliticas_Click" Visible="false" />
</div>