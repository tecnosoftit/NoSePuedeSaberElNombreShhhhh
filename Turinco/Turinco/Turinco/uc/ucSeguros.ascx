<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSeguros.ascx.cs" Inherits="uc_ucSeguros" %>

<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
        <div class="encabezadoSeguros blanco arial">
            <asp:Label ID="strTitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>
        </div>

        <asp:Label ID="strDescripcion" runat="server" Text="" CssClass="descripcionSeguros arial">
            <%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%>
        </asp:Label>
        
        
        <%--
            <asp:Image runat="server" ImageUrl='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen").ToString() %>' ID="imgSeguros" />
            <img id="imgSeguros" runat="server" alt='<%# DataBinder.Eval(Container.DataItem, "strTitulo") %>' src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen").ToString() %>' />
        --%>


        <asp:Repeater runat="server" ID="rptSeccion">
            <ItemTemplate>
                <asp:Label CssClass="tituloSeguros arial" ID="strTitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>
                <asp:Label ID="strDescripcion" runat="server" Text="" CssClass="descripcionSeguros arial">
                    <%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%>
                </asp:Label>
            </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
</asp:Repeater>
