<%@ Control Language="C#" ClassName="ucDetalle_Hotel" CodeFile="~/Uc/ucDetalleHotel.ascx.cs"
    Inherits="Uc_ucDetalleHotel" %>
<div class="side_right auto" style="margin: 0 auto;">
    <div class="box_side" style="width: 100%">
        <div class="tituloSeccionNoticias" style="width: auto;">
            <asp:Label ID="lblTDetalleHotel" runat="server" Text="Detalle Del Hotel" Style="display: none;"></asp:Label>
            <asp:Label CssClass="bold" ID="lblNombre" runat="server"></asp:Label>
            <span style="float: right; margin-right: 55px; margin-left: 40px;">
                <%-- <asp:Repeater ID="RptEstrellas" runat="server">
                    <ItemTemplate>
                        <div style="display: inline-block;" class='<%# DataBinder.Eval(Container,"DataItem.style") %>'>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>--%>
                <asp:DataList ID="dlEstrellas" runat="server" RepeatDirection="Horizontal">
                    <HeaderTemplate>
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <td class='<%# DataBinder.Eval(Container,"DataItem.style") %>'>
                        </td>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tr> </table>
                    </FooterTemplate>
                </asp:DataList>
            </span>
        </div>
        <input id="btnRegresar" class="botonBuscador back" onclick="javascript:history.back();"
            type="button" value="<< Regresar" />
        <div class="inner_box_side" style="padding: 25px;">
            <div class="ArribarptHotDetP">
            </div>
            <div class="full left_normal">
                <div class="resultadoImg">
                    <asp:Image ID="iImagen" runat="server" Style="width: 200px; border-width: 0px; height: 150px;" />
                </div>
                <div class="detalleHotelDescripcion">
                    <div class="bold full left_normal" style="margin-bottom: 5px; display: none;">
                        <span style="font-size: 14px;">Desde</span>
                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AmountText") %>'></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Currency_Code") %>'></asp:Label>
                        <asp:Label ID="lbltotalView" runat="server"></asp:Label>
                        <span>
                            <asp:Label ID="lblMonedaView" runat="server"></asp:Label></span> <span>- Precio total</span>
                    </div>
                    <div class="bold full left_normal" style="margin-bottom: 5px;">
                        <asp:Label ID="lblDireccion" runat="server"></asp:Label>
                        <asp:Label ID="lblTelefono" runat="server"></asp:Label>
                    </div>
                    <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
                </div>
            </div>
            <div class="full left_normal" style="margin-top: 20px;">
                <asp:Repeater ID="rptOcupacion" runat="server">
                    <ItemTemplate>
                        <div class="tituloTablaHotel" style="text-align: center;">
                            <asp:Label CssClass="bold" ID="Label10" runat="server" Text="Habitación"></asp:Label>
                            <asp:Label CssClass="bold" ID="lblidHabitacion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                            - Adultos
                            <%# DataBinder.Eval(Container, "DataItem.AdultCount") %>
                            / Niños
                            <%# DataBinder.Eval(Container, "DataItem.ChildCount") %>
                            <%# DataBinder.Eval(Container, "DataItem.RoomCountText") %>
                        </div>
                        <div class="tituloTablaHotel" style="margin-top: 15px;">
                            <div class="titulo1">
                                Tipo de habitación
                            </div>
                            <div class="titulo2">
                                Moneda
                            </div>
                            <div class="titulo2">
                                Tarifa total
                            </div>
                        </div>
                        <asp:Repeater ID="rptTiposHabitacion" runat="server">
                            <ItemTemplate>
                                <div class="lineaHotel">
                                    <div class="campo1">
                                        <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RoomType_Text")%>'></asp:Label>
                                    </div>
                                    <div class="campo2">
                                        <asp:Label ID="lblMoneda" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Currency_Code")%>'></asp:Label>
                                    </div>
                                    <div class="campo2">
                                        <asp:Label ID="lblDesde" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AmountText")%>'></asp:Label>
                                    </div>
                                    <div class="campo2">
                                        <asp:RadioButton ID="rbtSeleccion" runat="server" OnCheckedChanged="rbtSeleccion_CheckedChanged"
                                            AutoPostBack="true" />
                                        <asp:Label ID="lblCodCategoria" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.HotelRoom_Id")%>'
                                            Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <asp:Button CssClass="botonBuscador" ID="bReservar" runat="server" OnClick="setShorSell"
                Text="Reservar"></asp:Button>
        </div>
    </div>
    <!--- Galeria -->
    <div class="box_side" style="width: 100%;">
        <div class="tituloSeccionNoticias">
            Galería de fotos
        </div>
        <div class="inner_box_side" style="padding: 25px;">
            <div class="ArribarptHotDet">
            </div>
            <div class="galeriaHotel">
                <asp:Repeater ID="RptGaleria" runat="server">
                    <ItemTemplate>
                        <div style="display: inline-block; border: 1px dotted gray; padding: 5px; font-size: 10px;
                            margin-bottom: 5px;">
                            <asp:Image ID="iGaleria" runat="server" Width="130" Height="90" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.ImagePath") %>'
                                CssClass="block" />
                            <asp:Label ID="lblGaleria" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Name") %>'
                                CssClass="blocK"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <!--- instalaciones -->
    <div class="box_side" style="width: 100%;">
        <div class="tituloSeccionNoticias">
            Instalaciones y servicios del hotel
        </div>
        <div class="inner_box_side" style="padding: 25px;">
            <div class="ArribarptHotDet">
            </div>
            <asp:Repeater ID="rptComodidadesH" runat="server" OnItemDataBound="Facilidades_ItemDataBound">
                <ItemTemplate>
                    <div class="instalacioneshoteles">
                        <asp:Image ID="iFlecha" CssClass="alignleft" ImageUrl="../App_Themes/Imagenes/flechita.gif"
                            runat="server" />&nbsp;
                        <asp:Label ID="lblComodidades" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Facilidad") %>'></asp:Label></div>
                    <!--<asp:Label ID="Label1" runat="server" Text='$'></asp:Label>-->
                    <asp:Label ID="lblValue" ForeColor="red" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Value") %>'></asp:Label>
                </ItemTemplate>
            </asp:Repeater>
            <p style="color:Red;">
                (*) Algunos servicios serán abonados en el establecimiento.</p>
        </div>
    </div>
    <!--- comodidades -->
    <div class="box_side" style="width: 100%;">
        <div class="tituloSeccionNoticias">
            Equipamento de habitaciones
        </div>
        <div class="inner_box_side" style="padding: 25px;">
            <div class="ArribarptHotDet">
            </div>
            <asp:Repeater ID="rptComodidadesR" runat="server" OnItemDataBound="Facilidades_ItemDataBound">
                <ItemTemplate>
                    <div class="instalacioneshoteles">
                        <asp:Image ID="iFlecha" CssClass="alignleft" ImageUrl="../App_Themes/Imagenes/flechita.gif"
                            runat="server" />&nbsp;
                        <asp:Label ID="lblComodidades" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Facilidad") %>'></asp:Label></div>
                    <!--<asp:Label ID="Label1" runat="server" Text='$'></asp:Label>-->
                    <asp:Label ID="lblValue" ForeColor="red" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Value") %>'></asp:Label>
                </ItemTemplate>
            </asp:Repeater>
            <p style="color:Red;">
                (*) Algunos servicios serán abonados en el establecimiento.</p>
        </div>
    </div>
    <div class="box_side" style="width: 100%;">
        <asp:UpdatePanel ID="udpUbicacion" runat="server">
            <ContentTemplate>
                <div class="tituloSeccionNoticias">
                    <asp:Label ID="Label3" runat="server" Text="Ubicación"></asp:Label>
                </div>
                <div class="inner_box_side" style="padding: 25px;">
                    <div class="ArribarptHotDet">
                    </div>
                    <asp:Panel ID="pMapa" runat="server">
                    </asp:Panel>
                    <asp:Label ID="lblMapa" runat="server"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<%--
<div class="contenedorBlanco">
    <div class="detalleContenido">
        <div class="detallePanelLeftHotel">

            <!--Tabla con los detalles de las habitaciones-->
            <div class="contenedorCafe">
                
                <!--end cod  -->
            </div>
            <asp:Label ID="lblError" runat="server"></asp:Label>
            
            <!--Mapa de ubicación-->
        </div>
        <!--Panel derecho donde va la Galería y servicios-->
        <div class="detallePanelRightHotel">
            <!--Galería de imágenes-->
            <!--- galeria -->
            <div class="detalleTituloDescripcion" style="display: none;">
                <asp:Label ID="lblTGaleria" runat="server" Text="Galería"></asp:Label>
            </div>
            <!--Sección servicios-->
            <div class="detalleTituloDescripcion" style="display: none">
                <asp:Label Style="display: none;" ID="lblComodidadesH" runat="server" Text="Servicios"></asp:Label>
                <asp:Label Style="color: #04417F; font-weight: bold; margin-bottom: 10px;" ID="Label1"
                    runat="server" Text="Servicios"></asp:Label>
            </div>
            <div class="detalleServiciosHotel">
                <asp:Repeater ID="RptComodidadesH" runat="server" OnItemDataBound="Facilidades_ItemDataBound">
                    <ItemTemplate>
                        <div class="uldetalleServiciosHotel">
                            <asp:Image ID="iFlecha" CssClass="alignleft" ImageUrl="../App_Themes/Imagenes/images/icnBulletChulo.png"
                                runat="server" />&nbsp;
                            <asp:Label ID="lblComodidades" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Facilidad") %>'></asp:Label>
                            <asp:Label ID="lblValue" ForeColor="red" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Value") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="detallePie">
        </div>
    </div>
</div>

--%>

<script>

</script>