<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCondiciones.ascx.cs"
    Inherits="class_uc_ucCondiciones" %>
<div>
    <table style="width:100%; font-size:12px;">
        <tr>
            <td>
                <span class="boldTerminos">
                    Condiciones de reserva
                </span>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnCondicionesTraslados" runat="server">
                    <asp:Repeater ID="rptCondicionesTraslados" runat="server">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="font-style:italic">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcondiciones" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.StrRestricciones").ToString()) %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="pnCondicionesCircuitos" runat="server">
                    <asp:Repeater ID="rptCondicionesCircuitos" runat="server">
                        <HeaderTemplate>
                            <strong><asp:Label ID="lblTCircuitos" runat="server" Text="Circuitos"></asp:Label></strong>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="font-style:italic">
                                    - <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrNombrePlan") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrRestricciones") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="pnCondicionesExcursiones" runat="server">
                    <asp:Repeater ID="rptCondicionesExcursiones" runat="server">
                        <HeaderTemplate>
                            <strong><asp:Label ID="lblTExcursiones" runat="server" Text="Excursiones"></asp:Label></strong>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="font-style:italic">
                                    - <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrNombrePlan") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrRestricciones") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="pnCondicionesSouvenirs" runat="server">
                    <asp:Repeater ID="rptCondicionesSouvenirs" runat="server">
                        <HeaderTemplate>
                            <strong><asp:Label ID="lblTSouvenirs" runat="server" Text="Souvenirs"></asp:Label></strong>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td style="font-style:italic">
                                    - <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrNombrePlan") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrRestricciones") %>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </asp:Panel>                    
            </td>
        </tr>
    </table>
</div>
