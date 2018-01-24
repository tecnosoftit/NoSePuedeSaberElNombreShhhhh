<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDocumento.ascx.cs" Inherits="uc_ucDocumento" %>

<div class="panelResultados">
    <div class="tituloResultados">
        <div style="float:right; position:relative;">
            <input id="btnRegresar" class="botonBuscar" onclick="javascript:history.back();" type="button" value="Regresar" />
        </div>
        <asp:Label ID="lblTituloDestino" runat="server" Text="HERRAMIENTAS DE VIAJE &raquo;"></asp:Label>
    </div>
    <div class="contenidoResultado">
        <div class="contentPanel" id="cPanel" runat="server">
        </div> 
    </div>
</div>