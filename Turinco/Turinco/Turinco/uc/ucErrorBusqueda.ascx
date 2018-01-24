<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucErrorBusqueda.ascx.cs" Inherits="uc_ucErrorBusqueda" %>
<%@ Register Src="ucBuscador.ascx" TagName="ucBuscador" TagPrefix="uc1" %>


<div class="ofertasHome">
    <div class="tituloOfertas">
        <img class="bordeOfertas" src="../App_Themes/Imagenes/imagesNactur/bordeBarraOfertas.jpg">
        <asp:Label ID="lblTituloSeccionPadre" Text="Resultados Vuelos" runat="server"></asp:Label>
    </div>


    <div class="mensajeError" id="dPanel" runat="server">  
    </div>
</div>
