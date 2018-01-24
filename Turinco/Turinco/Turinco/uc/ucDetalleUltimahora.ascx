<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDetalleUltimahora.ascx.cs" Inherits="uc_ucDetalleUltimahora" %>
<div class="contenidoNoticiasDetalle">

    <asp:Repeater runat="server" ID="rptSeccion">
        <ItemTemplate>
            <div id="masthead">
                <asp:Label ID="lblTUltimasNoticias" class="head" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTitulo") %>'></asp:Label>
            </div>
            <a class="Regresar" href="../Presentacion/Utilitarios.aspx" class="VerMas">
                Ver más folletos
            </a> 
            <%--<asp:Label ID="lblFecha" runat="server" CssClass="fechaNoticiaDetalle" Text='<%# DataBinder.Eval(Container,"DataItem.dtmFecha") %>'></asp:Label>--%>
            <div class="fotoDetalleUltima">
                <img alt="" src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen") %>' />
            </div>
             <asp:Label ID="lblDescripcion" runat="server" CssClass="descripcionDetalleUltimaHora"><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString()) %></asp:Label>
           <%-- <asp:Repeater ID="rptSeccion" runat="server">
                <ItemTemplate>
                    <asp:Label ID="strDescripcion" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDescripcion") %>'></asp:Label>
                </ItemTemplate>
            </asp:Repeater>--%>
        </ItemTemplate>
    </asp:Repeater>
</div>
