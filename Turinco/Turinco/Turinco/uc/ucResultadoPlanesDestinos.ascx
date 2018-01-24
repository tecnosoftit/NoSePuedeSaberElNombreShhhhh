<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucResultadoPlanesDestinos.ascx.cs" Inherits="uc_ucResultadoPlanesDestinos" %>
<%@ Register Src="../uc/ucBuscadorPlan.ascx" TagName="ucBuscadorPlan" TagPrefix="uc1" %>

<asp:Label ID="lblDescripcion" runat="server"></asp:Label>
<!-- resultados -->
<asp:Repeater ID="dtlOfertas" runat="server" OnItemCommand="dtlOfertas_ItemCommand1" OnItemDataBound="dlPlanes_ItemDataBound">
    <ItemTemplate>
        <div class="panelOfertas">   
            <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla();" class="iblock">
                <img alt='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>' src='<%# DataBinder.Eval(Container,"DataItem.strImagen") %>' class="imagen" />
            </a>
            <div class="descripcion">
                <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla_Interna()" class="tituloPlanes azulClaro full">
                    <%# DataBinder.Eval(Container,"DataItem.Nombre") %>                    
                </a>
                
                <div class="textoPlanes azulOscuro full textJustify">
                    <asp:Label ID="lblCant1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intDias") %>'></asp:Label>&nbsp;
                    <asp:Label ID="lblCant1T" runat="server" Text="días / "></asp:Label>
                    <asp:Label ID="lblCant2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intNoches") %>'></asp:Label>&nbsp;
                    <asp:Label ID="lblCant2T" runat="server" Text="noches"></asp:Label>
                    
                    <br />
                    
                   <%-- <asp:Label ID="lblAcom" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strTarifaReferencia").ToString()) %>'></asp:Label>--%>

                    <br />
                    <asp:Label ID="Label1" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
                    
                    

                    <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla_Interna()" class="btnAzul">
                        VER DETALLES
                    </a>
                </div>

                <div class="imagenCategoria">
                    <asp:Image ID="ImageButton2" runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.urlImagenCategoria") %>' />
                </div>
            </div>
            <div class="precioPlan full iblock grisOscuro">
                <span class="labelValorFinal iblock">
                    <asp:Label ID="lblTDesde" runat="server" Text="DESDE"></asp:Label>
                </span>

                <div class="valorFinal iblock">
                    <asp:Label Visible="false" ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalleClasificacion") %>'></asp:Label>
                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strRefereMoneda") + " " + Decimal.Parse(DataBinder.Eval(Container,"DataItem.dblPrecio").ToString()).ToString("###,###.##") %>'></asp:Label>
                </div>

                <div class="difiera iblock">
					DIFIERE EL PAGO CON TU TARJETA
				</div>

                <div class="iblock cuotas cuotasDestinosRecomendados">
					<div class="valorCuota iblock full">
						<asp:Label  ID="lblTarifaCuotas" runat="server" Text='<%#System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strTarifaCuotas").ToString()) %>'></asp:Label>  
					</div>
								
					
				</div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

<div class="paginadorPaquetes full iblock">
    <asp:Button ID="Button2" OnCommand="Button1_Command" CssClass="botonAdelante fRight gris" CommandName="Next" runat="server" Text="Adelante"></asp:Button>
    

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

    <asp:Button ID="Button1" CssClass="botonAtras fRight gris" CommandName="Back" runat="server" Text="Atrás" OnCommand="Button1_Command"></asp:Button>
    
    
    <div class="noneDisplay">
        <input type="button" class="botonBusqueda" value="Nueva búsqueda" onclick="javascript:$('#ucResultadoHotel_btnBusqueda').click();" />
    </div>
</div>
<asp:Label ID="lblError" Style="display: none;" runat="server" Text=""></asp:Label>

