<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucServicios.ascx.cs" Inherits="ucServicios" %>

<div class="panelResultados">
    <div class="tituloResultados">
        <asp:Label ID="lblTUltimasNoticias" runat="server" Text="SERVICIOS COMPLEMENTARIOS &raquo;"></asp:Label>
    </div>
    <div class="contenidoResultado">
        <div style="text-align: right; float: right; margin-right: 10px;">
            <asp:DataList ID="dtlPaginador" runat="server" OnItemCommand="dtlPaginadorRec_ItemCommand"
                RepeatDirection="Horizontal">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CssClass='<%# DataBinder.Eval(Container,"DataItem.Class") %>'
                        Text='<%# DataBinder.Eval(Container,"DataItem.Pagina") %>' CommandName="dlPagNoticias"
                        ID="lBIndice"></asp:LinkButton>&nbsp;
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <asp:Repeater runat="server" ID="rptSeccion">
        <ItemTemplate>
            <asp:Repeater runat="server" ID="rptSeccion">
                <ItemTemplate>
                    <div class="panelOfertas">
                        <div class="imagen">
                            <img class="plan" alt="" src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen") %>' />
                        </div>
                        <div class="descripcion">
                            <div class="tituloPlanes">
                                <%# DataBinder.Eval(Container.DataItem, "strTitulo") %>
                            </div>
                            <div class="textoPlanes" style="border-bottom:0;">
                                <asp:Label ID="lblDescripcion" runat="server"><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString()) %></asp:Label>
                            </div>
                        </div>
                        <div class="precioPlan">
                            <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>' Visible="false"></asp:Label>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
</div>
