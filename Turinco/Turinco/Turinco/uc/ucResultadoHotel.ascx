<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucResultadoHotel.ascx.cs"
    Inherits="uc_ucResultadoHotel" %>
<ajax:UpdatePanel ID="upRecalculo" runat="server" class="contenedorResultadosHoteles">
    <ContentTemplate>
        <div class="listadoResultados">
            <!--- resumen de busqueda -->
            <div class="resultados">
                <div class="tituloResultados">
                    <asp:Label ID="Label1" runat="server" Text="Resumen de Búsqueda"></asp:Label>
                </div>
                <div class="contenidoResultado">
                    <div class="resumenHoteles">
                        <asp:Label CssClass="tituloResumen bold" ID="lblDestino" runat="server" Text="Destino:"></asp:Label>
                        <br />
                        <asp:Label ID="lblCiudad" runat="server"></asp:Label>
                    </div>
                    <div class="resumenHoteles">
                        <asp:Label CssClass="tituloResumen bold" ID="Label6" runat="server" Text="Fecha Ingreso:"></asp:Label>
                        <br />
                        <asp:Label ID="lblFechaSalida" runat="server"></asp:Label>
                    </div>
                    <div class="resumenHoteles">
                        <asp:Label CssClass="tituloResumen bold" ID="Label14" runat="server" Text="Fecha Salida:"></asp:Label>
                        <br />
                        <asp:Label ID="lblFechaLlegada" runat="server"></asp:Label>
                    </div>
                    <div class="resumenHoteles">
                        <asp:Label CssClass="tituloResumen bold" ID="lblHuespedes" runat="server" Text="Huespedes:"></asp:Label>
                        <br />
                        <asp:Label ID="lblAcomodacion" runat="server"></asp:Label>
                    </div>
                    <div class="nuevaBusquedaVuelos" style="display: none;">
                        <asp:Button ID="btnBusqueda" CssClass="botonBusqueda" runat="server" Text="Nueva búsqueda">
                        </asp:Button>
                    </div>
                    <div class="alineacionCentro" runat="server" id="Div1">
                        <ajax:UpdateProgress ID="udpEperar" runat="server" AssociatedUpdatePanelID="upRecalculo">
                            <ProgressTemplate>
                                <div class="progressbar">
                                </div>
                                <img alt="" src="../App_Themes/Imagenes/loading.gif" /><br />
                                <asp:Label ID="lblEsperar" runat="server" Text="Espere por favor..."></asp:Label>
                            </ProgressTemplate>
                        </ajax:UpdateProgress>
                    </div>
                    <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                    <div class="mensajeError" id="dPanel" runat="server">
                    </div>
                </div>
            </div>
            <!--- filtros -->
            <div class="panelResultadosHotel">
                <div class="tituloResultados">
                    <asp:Label ID="lblTFiltrar" runat="server" Text="Filtrar Resultados"></asp:Label>
                </div>
                <div class="contenidoResultado">
                    <!--- box border -->
                    <div class="plb">
                        <div class="contenidoResultados" style="height: 110px;">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr class="alineacionIzquierda">
                                    <td>
                                        <asp:RadioButtonList ID="chkCategoria" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                                            OnSelectedIndexChanged="chkCategoria_SelectedIndexChanged">
                                            <asp:ListItem Value="0" Selected="True">
                                                    <td colspan="5">
                                                    Todos
                                            </asp:ListItem>
                                            <asp:ListItem Value="1">
                                                    <td class="stars">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                            </asp:ListItem>
                                            <asp:ListItem Value="2">
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                            </asp:ListItem>
                                            <asp:ListItem Value="3">
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                            </asp:ListItem>
                                            <asp:ListItem Value="4">
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                                    <td>
                                                    </td>
                                            </asp:ListItem>
                                            <asp:ListItem Value="5">
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                                    <td class="stars">
                                                    </td>
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                        <br />
                                    </td>
                                </tr>
                                <tr class="alineacionIzquierda">
                                    <td>
                                        <asp:Label ID="lblZonas" runat="server" Text="Por ubicación dentro de la ciudad"></asp:Label>
                                        <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" CssClass="camposBuscador"
                                            Width="270" OnSelectedIndexChanged="ddlZona_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <br />
                                    </td>
                                </tr>
                                <tr class="alineacionIzquierda">
                                    <td>
                                        <br />
                                        <asp:Label ID="Label8" runat="server" Text="Por Nombre"></asp:Label>
                                        <asp:TextBox ID="txtnombrehotel" runat="server">
                                        </asp:TextBox>
                                        <asp:Button ID="btnenviar" CssClass="botonBuscar" runat="server" Text="Buscar" OnClick="btnFiltrar_Click" />
                                        <asp:Button ID="btnlimpiar" CssClass="botonBuscar" runat="server" Text="Borrar" OnClick="btnBorrarFiltro_Click" />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <!--- paginacion -->
            <%--<div class="panelResultadosHotel" style="overflow: auto; overflow-y: hidden;" id="paginadorHidden">
                            <table border="0" cellpadding="0" cellspacing="0" align="right" style="float: left;">
                                <tr align="right">
                                    <td>
                                        <asp:button id="btnBack" cssclass="botonBuscar" commandname="Back" runat="server"
                                            text="Anterior" oncommand="Search_Command" />
                                    </td>
                                    <td>
                                        <asp:datalist id="dlPagina" runat="server" repeatdirection="Horizontal" onitemcommand="dlPagina_ItemCommand">
                                            <headertemplate>
                                            </headertemplate>
                                            <itemtemplate>
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnPage" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.value") %>'
                                                                CssClass='<%# DataBinder.Eval(Container,"DataItem.class") %>' Text='<%# DataBinder.Eval(Container,"DataItem.text") %>' />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </itemtemplate>
                                            <footertemplate>
                                            </footertemplate>
                                        </asp:datalist>
                                    </td>
                                    <td>
                                        <asp:button id="btnNext" cssclass="botonBuscar" runat="server" text="Siguiente" commandname="Next"
                                            oncommand="Search_Command" />
                                    </td>
                                </tr>
                            </table>
                        </div>--%>
            <div class="resultados paginador">
                <div class="contenidoResultado">
                    <div class="tituloResultados">
                        <asp:Label ID="Label15" runat="server" Text="Filtrar resultados"></asp:Label>
                    </div>
                    <asp:RadioButtonList ID="RadioButtonList1" AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="chkCategoria_SelectedIndexChanged" CssClass="filtroEstrellas">
                    </asp:RadioButtonList>
                    <asp:Button ID="btnBack" CssClass="botonBuscar" Style="display: inline-block;" CommandName="Back"
                        runat="server" Text="Anterior" OnCommand="Search_Command" />
                    <asp:Repeater ID="RptPagina" runat="server" OnItemCommand="RptPagina_ItemCommand">
                        <ItemTemplate>
                            <div style="" class="NumeritosPags">
                                <asp:Button ID="btnPage" runat="server" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.value") %>'
                                    CssClass='<%# DataBinder.Eval(Container,"DataItem.class") %>' Text='<%# DataBinder.Eval(Container,"DataItem.text") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Button ID="btnNext" CssClass="botonBuscar" Style="display: inline-block; margin-left: -10px;"
                        runat="server" Text="Siguiente" CommandName="Next" OnCommand="Search_Command" />
                </div>
            </div>
            <!--- resultados -->
            <div class="box_side resultado_h" style="width: 100%; font-size: 12px; color: #5a5a5a;">
                <!--inicio box_side-->
                <div class="tituloResultados">
                    <asp:Label ID="lblTResultados" runat="server" Text="Resultados de hoteles"></asp:Label>
                </div>
                <asp:Repeater ID="rptHotel" runat="server">
                    <ItemTemplate>
                        <div class="inner_box_side">
                            <div class="ArribarptHotRes">
                            </div>
                            <div class="left">
                                <asp:Image ID="Image1" runat="server" Width="165" Height="110" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.Hotel_Photo") %>' />
                            </div>
                            <div class="right" style="width: 460px;">
                                <h4 class="bold block full t_left">
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.name") %>'
                                        Style="float: left;"></asp:Label>
                                    <span style="float: left;">-</span>
                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Zone_Text") %>'
                                        Style="float: left;"></asp:Label>
                                    <div style="float: right; margin-right: 10px; text-align: right; margin-top: -5px;">
                                        <asp:Repeater ID="RptEstrellas" runat="server">
                                            <ItemTemplate>
                                                <div class='<%# DataBinder.Eval(Container,"DataItem.style") %>'>
                                                    <%# DataBinder.Eval(Container, "DataItem.Categoria")%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </h4>
                                <div style="float: left; width: 97%; padding-right: 20px; margin-top: 8px; height: 76px;
                                    margin-bottom: 12px; overflow: auto;">
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Address") %>'></asp:Label><br />
                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Description") %>'></asp:Label>
                                </div>
                                <div class="descripcionHotelBtn">
                                    <asp:HyperLink CssClass="botonVerMasHotel" NavigateUrl='<%# DataBinder.Eval(Container,"DataItem.DetallesURL") %>'
                                        ID="HyperLink1" runat="server" Style="line-height: 15px; height: 15px; margin-top: 0;">Ver más</asp:HyperLink>
                                    <asp:Image ID="iOferta" CssClass="alignleft" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.oferta") %>'
                                        runat="server" />
                                </div>
                            </div>
                            <div style="float: left; width: 100%; margin-top: 10px; font-weight: bold; font-size: 11px;">
                                <asp:Label ID="lblTEconomico" runat="server" Text="Tarifa Neta más económica"></asp:Label>
                                <asp:Label ID="lblTTarifaNoche" runat="server" Visible="false" Text="más económica"></asp:Label>
                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Currency_Code") %>'></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AmountText") %>'></asp:Label>
                            </div>
                            <!-- rptOcupacion -->
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
                                            <%--<asp:Repeater ID="rptDesayuno" runat="server">
                                                <ItemTemplate>
                                                    <asp:Image ID="iDesayuno" CssClass="alignleft desayunoResGen" ImageUrl="~/App_Themes/Imagenes/desayuno.jpg"
                                                        runat="server" Style="position: absolute; /*margin-left: -434px; margin-top: -28px;
                                                        */" />
                                                </ItemTemplate>
                                            </asp:Repeater>--%>
                                            <asp:Repeater ID="rptDesayuno" runat="server">
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" class="DesTabRes">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Image ID="iDesayuno" CssClass="alignleft desayunoResGen" ImageUrl="~/App_Themes/Imagenes/desayuno.jpg"
                                                                runat="server" />
                                                        </td>
                                                        <td>
                                                            <%--<%# DataBinder.Eval(Container, "DataItem.Board_Text")%>--%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            <div class="PopUpShowInfo">
                                                <asp:Image ID="iOfertaPromos" runat="server" CssClass="alignleft" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.iOfertaPromosImg")%>' />
                                                <asp:Label ID="ImagenPromoText" runat="server" CssClass="PopUpHideInfo" Text='<%# DataBinder.Eval(Container, "DataItem.iOfertaPromosText")%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Button CssClass="botonBuscadorReservarHotel .link-button.white" OnClientClick="Show_Cortinilla_Interna();"
                                ID="lbReservar" runat="server" OnClick="setShorSell" Text="Reservar"></asp:Button>
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="listadoResultados">
                <!--- resultados -->
                <div class="resultados" style="display: none;">
                    <div class="panelResultados" style="text-align: right;">
                        <input type="button" class="botonBusqueda" value="Nueva búsqueda" onclick="javascript:$('#ucResultadoHotel_btnBusqueda').click();" />
                    </div>
                </div>
                <!-- Buscador Hotel -->
                <ajax:ModalPopupExtender ID="MPEEBuscadorHotel" BackgroundCssClass="ui-widget-shadow"
                    DropShadow="false" runat="server" TargetControlID="btnBusqueda" BehaviorID="MPEEBuscadorHotel"
                    OnOkScript="" OkControlID="btnCerrar2" EnableViewState="true" PopupControlID="PanelBuscador" />
                <asp:Panel runat="server" ID="PanelBuscador">
                    <div class="ventanaBuscador">
                        <asp:Label ID="Label9" runat="server" Text="Realizar Nueva Busqueda"></asp:Label>
                        <asp:Button ID="btnCerrar2" CssClass="botonCerrar" runat="server" />
                        <%-- <uc1:ucBuscadorHotel ID="ucBuscadorHotel" runat="server" /> --%>
                    </div>
                </asp:Panel>
            </div>
    </ContentTemplate>
</ajax:UpdatePanel>
