<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTemasSociales.ascx.cs"
    Inherits="uc_ucTemasSociales" %>
<asp:Repeater ID="rptSeccion" runat="server">
    <ItemTemplate>
        <a href='#this' class="botonSeccion" onclick='javascript:SetIdPagina("../Presentacion/SeccInformativa.aspx?CODSEC=<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>");'>
            <%# DataBinder.Eval(Container,"DataItem.strTitulo") %>
        </a>
    </ItemTemplate>
</asp:Repeater>
