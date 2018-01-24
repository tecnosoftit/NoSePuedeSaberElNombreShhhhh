<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuscadorCrucero.ascx.cs" Inherits="uc_ucBuscadorCrucero" %>
    <!--- Cruceros -->
    <div class="contenidoBuscador">
        <div class="full iblock textRight">
            <asp:Button ID="lbBuscar" runat="server" CssClass="botonBuscador" OnClientClick="Show_Cortinilla();" Text="" CommandName="Crucer" OnCommand="setCommand"></asp:Button>
        </div>
    </div>

    <div>    
        <asp:Label ID="lblErrorGen" runat="server" ForeColor="red"></asp:Label>
    </div>
