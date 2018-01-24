<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuscador.ascx.cs" Inherits="uc_ucBuscador" %>
<%@ Register Src="ucBuscadorAereo.ascx" TagName="ucBuscadorAereo" TagPrefix="uc1" %>
<%@ Register Src="ucBuscadorPlan.ascx" TagName="ucBuscadorPlan" TagPrefix="uc4" %>

<div class="buscador">
    <div id="tabs">
        <ul>
           <%-- <li><a href="#vuelos">
                <asp:Label ID="lblTVuelos" runat="server" Text="Vuelos"></asp:Label>
            </a></li>--%>
            <li><a href="#paquetes">
                <asp:Label ID="lblTPaquetes" runat="server" Text="Planes"></asp:Label>
            </a></li>
        </ul>
        <!--- vuelos -->
        <%--<div id="vuelos" class="niceform">
            <uc1:ucBuscadorAereo ID="UcBuscadorAereo1" runat="server" />
        </div>--%>
        <!--- paquetes -->
         <div id="paquetes">
            <uc4:ucBuscadorPlan ID="UcBuscadorPlan1" runat="server" />
        </div>
    </div>
</div>
