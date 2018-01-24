<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucResultadoPlanes.ascx.cs" Inherits="uc_ucResultadoPlanes" %>

<div class="encabezadoResultadoVuelos blanco arial" style="width: 553px;">
        <asp:Label ID="lblTCarroCompras" runat="server" Text="Resultados de la busqueda"></asp:Label>
</div>

<div class="paginadorPaquetes1" style="display:none;">
    <asp:Button ID="Button4" OnCommand="Button1_Command" CssClass="botonAdelante fRight gris" CommandName="Next" runat="server" Text="Adelante" style="display:none;"></asp:Button>
    
    <div class="contenedorNumerosPaginacion fRight">
        <asp:DataList ID="dtlPaginadorinf" runat="server" OnItemCommand="dtlPaginador_ItemCommand" RepeatDirection="Horizontal">
            <ItemTemplate>
                <asp:LinkButton runat="server" CssClass='<%# DataBinder.Eval(Container,"DataItem.Class") %>' Text='<%# DataBinder.Eval(Container,"DataItem.Pagina") %>' CommandName="dlPagNoticias" ID="lBIndice"></asp:LinkButton>
            </ItemTemplate>
        </asp:DataList>
    </div>

    <asp:Button ID="Button3" CssClass="botonAtras fRight gris" CommandName="Back" runat="server" Text="Atrás" OnCommand="Button1_Command" style="display:none;"></asp:Button>
</div>

<div class="featured portfolio-list masonry" style="margin-top:0;">
    <!-- resultados -->
    <asp:Repeater ID="dtlOfertas" runat="server" OnItemCommand="dtlOfertas_ItemCommand1" OnItemDataBound="dlPlanes_ItemDataBound">
        <ItemTemplate>
            <figure style="padding-top: 11px; width: 43%; margin-left: -34px; display: inline-block; padding-right: 63px; min-height: 380px;">
                <div class="imagenOfertaPlan">
                            <asp:Image runat="server" Visible="false" ImageUrl="~/App_Themes/Imagenes/ofertaPlan.png" ID="imgOferta" />
                </div>
                <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla();" class="thumb">
                    <asp:Image class="imagen" runat="server" AlternateText='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>' ToolTip='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>'  ImageUrl='<%# DataBinder.Eval(Container,"DataItem.strImagen") %>' />
                </a>

                <div class="ContendedorDescripcionPlan">
                     <h4 href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla_Interna()" class="azulClaro bold">
                            <%# DataBinder.Eval(Container,"DataItem.Nombre") %>
                     </h4>
                    <h5 class="color1" style="display:none;">
                       <%-- <asp:Label ID="lblAcom" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strTarifaReferencia").ToString()) %>'></asp:Label>--%>
                        <span>
                            <asp:Label ID="lblCant1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intDias") %>'></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblCant1T" runat="server" Text="días / "></asp:Label>
                            <asp:Label ID="lblCant2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intNoches") %>'></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblCant2T" runat="server" Text="noches"></asp:Label>
                        </span>

                        <asp:Label ID="Label1" style="color:#ccc; display:none;" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
                    </h5>
                </div>

                <div class="precios full iblock grisOscuro">
                    <h5 class="labelValorFinal iblock color1">
                        <asp:Label ID="lblTDesde" runat="server" Text="Desde"></asp:Label>
                    </h5>

                    <h5 class="valorFinal iblock color1">
                        <asp:Label Visible="false" ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalleClasificacion") %>'></asp:Label>
                        <asp:Label ID="Label6" runat="server" Text='<%# Decimal.Parse(DataBinder.Eval(Container,"DataItem.dblPrecio").ToString()).ToString("###,###.##") + " " + DataBinder.Eval(Container,"DataItem.strRefereMoneda") %>'></asp:Label>
                    </h5>

                    <div class="difiera iblock" style="display:none;">
						DIFIERE EL PAGO CON TU TARJETA
					</div>
							
					<div class="iblock cuotas">
						<div class="valorCuota iblock full">
                          <asp:Label  ID="lblTarifaCuotas" runat="server" Text='<%#System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strTarifaCuotas").ToString()) %>'></asp:Label>  
						</div>
								
						
					</div>	
                </div>
                <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla_Interna()" class="heading">
                        Ver más
                </a>

                <div style="display:none;">
                    <asp:Image ID="ImageButton2" runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.urlImagenCategoria") %>' />
                </div>
            </figure>
        </ItemTemplate>
    </asp:Repeater>
</div>


<div class="paginadorPaquetes1">
    <asp:Button ID="Button2" OnCommand="Button1_Command" CssClass="botonAdelante fRight gris" CommandName="Next" runat="server" Text="Adelante" style="display:none;"></asp:Button>

    <div class="contenedorNumerosPaginacion fRight" style="margin-right: 423px; margin-top: -40px;">
        <asp:DataList ID="dtlPaginador" runat="server" OnItemCommand="dtlPaginador_ItemCommand" RepeatDirection="Horizontal">
            <ItemTemplate>
                <asp:LinkButton runat="server" CssClass='<%# DataBinder.Eval(Container,"DataItem.Class") %>' Text='<%# DataBinder.Eval(Container,"DataItem.Pagina") %>' CommandName="dlPagNoticias" ID="lBIndice"></asp:LinkButton>
            </ItemTemplate>
        </asp:DataList>
    </div>

    <asp:Button ID="Button1" CssClass="botonAtras fRight gris" CommandName="Back" runat="server" Text="Atrás" OnCommand="Button1_Command" style="display:none;"></asp:Button>
</div>

<!--- resumen busqueda -->
<div class="panelResultados" style="display:none;">
    <div class="tituloResultados">
        <asp:Label ID="Label1" runat="server" Text="CRITERIOS DE BÚSQUEDA &raquo;"></asp:Label>
    </div>
    <div class="contenidoResultado">
        <div class="resumenPlanes">
            <span class="tituloResumen">
                <asp:Label ID="lblTPais" runat="server" Text="País: "></asp:Label>
            </span>
            <asp:Label ID="lblPais" runat="server"></asp:Label>
        </div>                
        <div class="resumenPlanes">
            <span class="tituloResumen">
                <asp:Label ID="lblTCiudad" runat="server" Text="Ciudad: "></asp:Label>
            </span>
            <asp:Label ID="lblCiudad" runat="server"></asp:Label>
        </div>
        <%--<div class="resumenPlanes">
            <span class="tituloResumen">
                <asp:Label ID="lblTFecRecoge" runat="server" Text="Fecha de viaje: "></asp:Label>
            </span>
            <asp:Label ID="Label2" runat="server" Text="">
                <%= (this.ucBuscadorPlan.FindControl("txtFechaViaje") as TextBox).Text%>
            </asp:Label>
        </div>--%>
        <div class="nuevaBusquedaVuelos">
            <asp:Button ID="btnBusqueda" CssClass="botonBusqueda" runat="server" Text="Nueva búsqueda"></asp:Button>
        </div>
    </div>
</div>

<asp:Label ID="lblDescripcion" runat="server"></asp:Label>
<asp:Label ID="lblError" runat="server" Text=""></asp:Label>


   

