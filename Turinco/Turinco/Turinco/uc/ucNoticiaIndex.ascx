<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucNoticiaIndex.ascx.cs"
    Inherits="uc_ucNoticiaIndex" %>
<div class="tituloNoticias">
    <asp:Label ID="Label2" runat="server" Text="NOTICIAS &raquo;"></asp:Label>
</div>
<div class="contenidoNoticias">
    <asp:Repeater runat="server" ID="rptSeccion">
        <ItemTemplate>
            <img alt="" src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen").ToString() %>' />
            <asp:Label ID="Label1" CssClass="titulo" runat="server"><%# DataBinder.Eval(Container,"DataItem.strTitulo") %></asp:Label>
            <asp:Label ID="Label3" runat="server"><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%></asp:Label>
            <div style="width:100%; text-align:right;">
                <a href='../Presentacion/Noticia.aspx?CODSEC=<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>&TPOMSJ=TM001&TMMSJ=93369'
                    class="botonBusqueda">Ver más</a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
