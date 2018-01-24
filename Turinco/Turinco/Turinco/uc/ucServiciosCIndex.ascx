<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucServiciosCIndex.ascx.cs"
    Inherits="uc_ucServiciosCIndex" %>

<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
        <div class="servicio">
            <div class="imagenServicio">
                <img width="90" height="80" alt="" src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen") %>' />
            </div>
            <div class="descripcion">
                <asp:Label ID="Label4" runat="server"><%#  System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString()) %></asp:Label>
            </div>
            <a href="../Presentacion/ServiciosComplementarios.aspx?Detalles=true&idSesion=<%=Request.QueryString["idSesion"] %>">
                Ver más
            </a>
        </div>
    </ItemTemplate>
</asp:Repeater>