
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucUtilitarios.ascx.cs" Inherits="uc_ucUtilitarios" %>
 <div id="masthead">
		    <span class="head">Flyers</span>
	    </div>
<div class="ContentUtilitarios">
<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
            <div id="ContenedorUtilitarios" class="ContenedorUtilitarios">
                <h4 id="Titulo" style="display:none;">
                    <asp:Label ID="strTitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>
                </h4>
                <img id="imgQuienes" runat="server" alt='<%# DataBinder.Eval(Container.DataItem, "strTitulo") %>' src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen").ToString() %>' class="iblock" />
                <a href='../Presentacion/DetalleUltimahora.aspx?CODSEC=<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>&TPOMSJ=TM001&TMMSJ=93369' class="VerMas">
                        Ver más
                </a>
                <div id="descripcion" class="ContenedorDescripcionUtilitarios" style="display:none;">
                    <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                </div>
                <asp:Repeater runat="server" ID="rptSeccion">
                    <ItemTemplate>
                        <%--<asp:Label CssClass="tituloQuienes" ID="strTitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>--%>
                        <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                    </ItemTemplate>
                </asp:Repeater>  
            </div>           
    </ItemTemplate>
</asp:Repeater>

<div class="paginadorPaquetes1" style="display:none;">
        <asp:Button ID="Button2" OnCommand="Button1_Command" CssClass="botonAdelante fRight gris"
            CommandName="Next" runat="server" Text="Adelante"></asp:Button>
        <div class="contenedorNumerosPaginacion fRight">
            <asp:DataList ID="dtlPaginador" runat="server" OnItemCommand="dtlPaginador_ItemCommand"
                RepeatDirection="Horizontal">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CssClass='<%# DataBinder.Eval(Container,"DataItem.Class") %>'
                        Text='<%# DataBinder.Eval(Container,"DataItem.Pagina") %>' CommandName="dlPagNoticias"
                        ID="lBIndice"></asp:LinkButton>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <asp:Button ID="Button1" CssClass="botonAtras fRight gris" CommandName="Back" runat="server"
            Text="Atrás" OnCommand="Button1_Command"></asp:Button>
    </div>

</div>
