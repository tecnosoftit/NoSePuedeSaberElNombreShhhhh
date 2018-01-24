<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucOfertasPlanes.ascx.cs"
    Inherits="uc_ucPlanesHome" %>
<!-- resultados -->
<!-- masthead -->
<div id="masthead">
    <span class="head">Ofertas</span>
    <!-- <span class="subhead">Aquí esta su mejor opción</span> -->
</div>
<!-- ENDS masthead -->
<div class="featured portfolio-list masonry">
    <asp:Repeater ID="dtlOfertas" runat="server">
        <ItemTemplate>
            <figure style="min-height: 349px;">
                <div class="imagenOfertaPlan">
                    <asp:Image runat="server" Visible="false" ImageUrl="~/App_Themes/Imagenes/ofertaPlan.png"
                        ID="imgOferta" />
                </div>
            
                <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla();" class="thumb">
                       <img alt="Promo 1" alt='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>' src='<%# DataBinder.Eval(Container,"DataItem.strImagen") %>'/>                                    </a>
                <div class="ContendedorDescripcionPlan">
                    <h4 href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla_Interna()">
                           <%# DataBinder.Eval(Container,"DataItem.Nombre") %>
                    </h4>
                    <h5 class="color1">
                            <span class="noneDisplay">
                            <asp:Label ID="lblTDesde" runat="server" Text="Desde "></asp:Label>
                            </span>
                            <asp:Label Visible="false" ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalleClasificacion") %>'></asp:Label>
                            <asp:Label CssClass="moneda" ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strRefereMoneda")%>'></asp:Label>           
                            <asp:Label CssClass="valor" ID="Label1" runat="server" Text='<%# Decimal.Parse(DataBinder.Eval(Container,"DataItem.dblPrecio").ToString()).ToString("###,###.##") %>'></asp:Label>           
                            </h5>
                               <div class="imagenCategoria" style="display:none;">
                               <asp:Image ID="ImageButton2" runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.urlImagenCategoria") %>' />
                               </div>

                            <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' class="heading" onclick="Show_Cortinilla();">
                               VER MÁS
                            </a>
                 </div>
             </figure>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="paginadorPaquetes1">
    <asp:Button ID="Button2" OnCommand="Button1_Command" CssClass="botonAdelante fRight gris"
        CommandName="Next" runat="server" Text="Adelante" Style="display: none;"></asp:Button>
    <div class="contenedorNumerosPaginacion fRight">
        <table>
            <tr>
                <td>
                    <asp:DataList ID="dtlPaginador" runat="server" OnItemCommand="dtlPaginador_ItemCommand"
                        RepeatDirection="Horizontal">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass='<%# DataBinder.Eval(Container,"DataItem.Class") %>'
                                Text='<%# DataBinder.Eval(Container,"DataItem.Pagina") %>' CommandName="dlPagNoticias"
                                ID="lBIndice"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="Button1" CommandName="Back" runat="server" Text="Atrás" OnCommand="Button1_Command"
        Style="display: none;"></asp:Button>
</div>
