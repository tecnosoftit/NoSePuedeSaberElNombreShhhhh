<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPoliticasdePrivacidad.ascx.cs" Inherits="uc_ucPoliticasdePrivacidad" %>

<div id="masthead">
        <span class="head">
            <asp:Label ID="lblTituloSeccionPadre" Text="Politicas de privacidad" runat="server"></asp:Label>
        </span>
    </div>
    <br />
<asp:Repeater runat="server" ID="rptSeccion" >
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td>
                    <asp:Label CssClass="textoTerminos" ID="lblDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Repeater runat="server" ID="rptSeccion" >
            <ItemTemplate>
                    <div>
                        <h4 style="margin-left:25px;">
                            <asp:Label ID="strTitulo" CssClass="boldTerminos" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTitulo") %>'></asp:Label>
                        </h4>
                    </div>
                    <br />
                        <td>
                            <asp:Label ID="strDescripcion" CssClass="textoTerminos" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
                        </td>
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>
