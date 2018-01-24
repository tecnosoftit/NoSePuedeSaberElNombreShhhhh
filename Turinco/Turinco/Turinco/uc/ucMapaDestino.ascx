<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMapaDestino.ascx.cs" Inherits="uc_ucMapaDestino" %>
<ajax:UpdatePanel ID="pnlmapa" runat="server" Visible="false">
    <ContentTemplate>
        <asp:Repeater runat="server" ID="rptSeccion">
            <ItemTemplate>
                <asp:Label ID="strDescripcion" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
            </ItemTemplate>
        </asp:Repeater>
    </ContentTemplate>
</ajax:UpdatePanel>
<div id="myP" >
</div>