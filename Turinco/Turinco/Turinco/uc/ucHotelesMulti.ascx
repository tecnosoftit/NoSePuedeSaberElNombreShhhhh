<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucHotelesMulti.ascx.cs" Inherits="uc_ucHotelesMulti" %>

<table width="610" border="0" cellspacing="0" cellpadding="3" style="background-color:#FFF;">
    <asp:Repeater ID="rptHoteles" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <div style="float:left; position:relative; padding:3px;">
                        <img alt="" height="150" width="150" runat="server" id="Imagen" src='<%# DataBinder.Eval(Container,"DataItem.Imagen") %>'/>    
                    </div>
                    <asp:Label CssClass="bold" ID="Nombre" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>'></asp:Label>
                    <br />
                    <asp:Label ID="Descripcion" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.Descripcion").ToString()) %>'></asp:Label>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
</table>