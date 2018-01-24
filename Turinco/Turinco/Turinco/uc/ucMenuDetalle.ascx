<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMenuDetalle.ascx.cs" Inherits="uc_ucMenuDetalle" %>
<ajax:UpdatePanel ID="pnSubdetalles" runat="server">
    <ContentTemplate>
        <div class="menuDetalle" style="width:600px;">
            <asp:Repeater runat="server" ID="rptSeccion">
                <ItemTemplate>
                 <asp:Label ID="strDescripcion" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
                    <asp:Repeater runat="server" ID="rptSeccion">
                        <ItemTemplate>
                            <div class="items">
                            <asp:Label ID="strDescripcion" runat="server" Text='<%#System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
                                <asp:Repeater runat="server" ID="rptSeccion">
                                    <ItemTemplate>
                                        <div class="items">
                                            <asp:Label ID="strDescripcion" runat="server" Text='<%#System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                           </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="contenidoMenuDetalle">
            <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label>
        </div>
    </ContentTemplate>
</ajax:UpdatePanel>