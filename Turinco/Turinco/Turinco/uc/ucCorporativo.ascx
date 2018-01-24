<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCorporativo.ascx.cs"
    Inherits="uc_SeccionCorporativo" %>

<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
        <div class="panelResultados">
            <div class="imagenQuienes">
                <img src='<%# DataBinder.Eval(Container,"DataItem.strImagen")%>' alt="" />
            </div>
            <div class="textoQuienes">
                <asp:Label CssClass="tituloQuienes" ID="strTitulo" runat="server" Text="Viajes Corporativos"></asp:Label>
                <div class="descripcionQuienes">
                    <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                </div>    
            </div>
            <div class="lineaQuienes"></div>
        </div>
        <asp:Repeater runat="server" ID="rptSeccion">
            <ItemTemplate>
                <div class="panelResultados">
                    <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>