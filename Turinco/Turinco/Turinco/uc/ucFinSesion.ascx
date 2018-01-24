<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucFinSesion.ascx.cs" Inherits="ucFinSesion" %>

<div id="content">
    <div class="mensajeError" id="dPanel" runat="server">
    </div>
    <br />
    <div>
        <asp:Button ID="btnRegresar" Text="Regresar el Inicio" CssClass="botonBuscar" runat="server" OnClick="btnRegresar_Click" />
        <asp:HiddenField ID="hdfUrl" runat="server" />
    </div>
</div>