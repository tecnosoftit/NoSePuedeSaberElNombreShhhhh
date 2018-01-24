<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuscadorT.ascx.cs" Inherits="uc_ucBuscador" %>
<%@ Register Src="ucBuscadorAereo.ascx" TagName="ucBuscadorAereo" TagPrefix="uc1" %>
<div class="buscador">
    <div id="tabs">
        <ul>
            <li><a href="#vuelos">
                <asp:Label ID="lblTVuelos" runat="server" Text="Vuelos"></asp:Label>
            </a></li>
        </ul>
        <!--- vuelos -->
        <div id="vuelos" class="niceform">
            <uc1:ucBuscadorAereo ID="UcBuscadorAereo1" runat="server" />
        </div>
    </div>
</div>
