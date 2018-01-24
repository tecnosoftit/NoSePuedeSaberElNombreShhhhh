<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHerramientas.ascx.cs" Inherits="uc_ucHerramientas" %>

<input id="btnRegresar" class="btnAzul" onclick="javascript:history.back();" type="button" value="<< Regresar" />

<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
        <asp:Repeater runat="server" ID="rptlinks">
            <ItemTemplate>
                <asp:Label ID="strDescripcion" runat="server" Text=""><iframe  id=iframe scrolling='auto' src='<%# DataBinder.Eval(Container, "DataItem.strurl")%>' frameBorder=0  width=100% height=700></iframe></asp:Label>
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>
