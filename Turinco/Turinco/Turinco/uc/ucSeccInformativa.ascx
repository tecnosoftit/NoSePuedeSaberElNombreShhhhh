<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSeccInformativa.ascx.cs"
    Inherits="uc_ucSeccInformativa" %>
<div style="font-family:Trebuchet MS; font-size:12px; color:#5a5a5a">
<asp:Repeater ID="rptSeccion" runat="server">
    <ItemTemplate>
    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strtitulo") %>'></asp:Label>
        <asp:Label ID="strDescripcion" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
        <asp:Repeater runat="server" ID="rptSeccion">
            <ItemTemplate>
                <br />
                <asp:Label CssClass="bold" ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTitulo") %>'></asp:Label>
                <br />
                <div class="imagenSubdetalle" style="display: none;">
                    <asp:Image runat="server" ImageUrl='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen").ToString() %>'
                        ID="imgQuienes" />
                </div>
                <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                <br />
                <br />
                <asp:Repeater runat="server" ID="rptSeccion">
                    <ItemTemplate>
                        <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>
</div>