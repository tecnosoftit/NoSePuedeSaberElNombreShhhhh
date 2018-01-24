<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPlanesHome.ascx.cs" Inherits="uc_ucPlanesHome" %>
<!-- resultados -->
<asp:Repeater ID="dtlOfertas" runat="server">
    <ItemTemplate>
        <figure style="min-height:349px;">
                <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla();" class="thumb">
                    <img class="plan" alt='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>' title='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>' src='<%# DataBinder.Eval(Container,"DataItem.strImagen") %>'/>
                </a>
            <div style="max-height: 75px;">
                <h4 href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla_Interna()">
                    <%# DataBinder.Eval(Container,"DataItem.Nombre") %>
                </h4>
                    <div class="imagenCategoria" style="display:none;">
                        <asp:Image ID="ImageButton2" runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.urlImagenCategoria") %>' />
                    </div>
                <h5 class="color1">
                    <span>
                        <asp:Label ID="lblTDesde" runat="server" Text="Desde "></asp:Label>
                    </span>
                    <asp:Label Visible="false" ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalleClasificacion") %>'></asp:Label>
                    <asp:Label CssClass="moneda" ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strRefereMoneda")%>'></asp:Label>
                    <asp:Label CssClass="valor" ID="Label1" runat="server" Text='<%# Decimal.Parse(DataBinder.Eval(Container,"DataItem.dblPrecio").ToString()).ToString("###,###.##") %>'></asp:Label>

                
                    <div class="Descrip_PlanHome" style="display:none;">
                        <span>
                            <asp:Label ID="lblAcom" runat="server" Text='<%#System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strTarifaReferencia").ToString()) %>'></asp:Label>
                            <asp:Label ID="lblCant1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intDias") %>'></asp:Label>&nbsp;
                            <asp:Label ID="lblCant1T" runat="server" Text="días / "></asp:Label>
                            <asp:Label ID="lblCant2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intNoches") %>'></asp:Label>&nbsp;
                            <asp:Label ID="lblCant2T" runat="server" Text="noches"></asp:Label>
                        </span>
                    </div>
                </h5>
                <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla();" class="heading">
                    Ver más
                </a>
                <a class="link" href=""></a>
            </div>
        </figure>
    </ItemTemplate>
</asp:Repeater>
<div class="panelResultados" style="display:none;">
    <div style="float: right; position: relative; margin-right: 20px;">
        <asp:Button ID="Button1" CssClass="botonAnterior" CommandName="Back" runat="server"
            Text="Anterior" OnCommand="Button1_Command"></asp:Button>
        &nbsp;|&nbsp;
        <asp:Button ID="Button2" OnCommand="Button1_Command" CssClass="botonSiguiente" CommandName="Next"
            runat="server" Text="Siguiente"></asp:Button>
        <table style="display: none">
            <tr>
                <td>
                    <asp:DataList ID="dtlPaginador" runat="server" OnItemCommand="dtlPaginador_ItemCommand"
                        RepeatDirection="Horizontal">
                        <ItemTemplate>
                            |
                            <asp:LinkButton runat="server" CssClass='<%# DataBinder.Eval(Container,"DataItem.Class") %>'
                                Text='<%# DataBinder.Eval(Container,"DataItem.Pagina") %>' CommandName="dlPagNoticias"
                                ID="lBIndice"></asp:LinkButton>
                            |
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </div>
    <div style="float: right; position: relative; display:none;">
        <input type="button" class="botonBusqueda" value="Nueva búsqueda" onclick="javascript:$('#ucResultadoHotel_btnBusqueda').click();" />
    </div>
</div>
<asp:Label ID="lblError" runat="server" Text=""></asp:Label>