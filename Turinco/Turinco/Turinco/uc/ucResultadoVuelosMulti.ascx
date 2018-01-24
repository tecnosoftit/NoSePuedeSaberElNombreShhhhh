<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucResultadoVuelosMulti.ascx.cs" Inherits="uc_ucResultadoVuelosMulti" %>



<div class="encabezadoResultadoVuelos blanco arial">
    <asp:Label ID="lblTResultados" runat="server" Text="Resumen búsqueda en vuelos"></asp:Label>
</div>

<div class="noneDisplay">
    <asp:Label ID="lblOrigen" runat="server" Text=""></asp:Label>, &nbsp;A&nbsp;
    <asp:Label ID="lblDestino" runat="server" Text=""></asp:Label>
    <span class="fechaTrayecto">
        <asp:Label ID="lblFechaSalida" runat="server" Text=""></asp:Label>
        -
        <asp:Label ID="lblFechaLlegada" runat="server" Text=""></asp:Label>
    </span>
</div>

<div class="resultados">
    <div class="resumenVuelos">
        <div id="content-slider">
        </div>

        <div class="tituloFiltros">
            <div class="aerolinea">
                <asp:Label ID="Label4" runat="server" Text="Aerolínea"></asp:Label>
            </div>
            <div class="directo">
                <asp:Label ID="Label10" runat="server" Text="Sin Paradas"></asp:Label>
            </div>
            <div class="parada1">
                <asp:Label ID="Label14" runat="server" Text="1 Parada"></asp:Label>
            </div>
            <div class="parada2">
                <asp:Label ID="Label17" runat="server" Text="2+ Paradas"></asp:Label>
            </div>
        </div>

        <div id="content-scroll">
            <div id="content-holder" class="contenidoFiltros">
                <asp:Repeater runat="server" ID="dtFiltroAir" OnItemCommand="dtlAir_ItemCommand">
                    <ItemTemplate>
                        <div class="columnaAerolinea">
                            <div class="aerolinea">
                                <asp:ImageButton ID="imgSeleccionar" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>'
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "strMarketingAirline")%>'
                                    ToolTip='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>' runat="server">
                                </asp:ImageButton>
                            </div>
                            <div class="directo">
                                <asp:Label ID="lblParadas" runat="server"><%#DataBinder.Eval(Container.DataItem, "StopQuantity")%></asp:Label>
                            </div>
                            <div class="parada1">
                                <asp:Label ID="lblParada1" runat="server"><%#DataBinder.Eval(Container.DataItem, "StopQuantity_1")%></asp:Label>
                            </div>
                            <div class="parada2">
                                <asp:Label ID="lblParada2" runat="server"><%#DataBinder.Eval(Container.DataItem, "StopQuantity_2")%></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
            <div class="alineacionCentro" runat="server" id="Div1">
                <ajax:UpdateProgress ID="udpEperar" runat="server">
                    <ProgressTemplate>
                        <div class="progressbar">
                        </div>
                        <img alt="" src="../App_Themes/Imagenes/loading.gif" /><br />
                        <asp:Label ID="lblEsperar" runat="server" Text="Espere por favor..."></asp:Label>
                    </ProgressTemplate>
                </ajax:UpdateProgress>
            </div>
        </div>
    </div>
</div>


<div class="tituloResultadoVuelos blanco arial noneDisplay">
    <asp:Label ID="lblTResultadosVuelos" runat="server" Text="Resultado vuelos"></asp:Label>
</div>

<div class="resultados">
    <asp:Repeater ID="rptItinerario" runat="server" OnItemCommand="rptItinerario_ItemCommand">
        <ItemTemplate>
            <div class="vuelos">
                <div class="tituloOpcion">
                    Opción <asp:Label ID="lblSequence" runat="server" ></asp:Label>
                </div>

                <div class="detalleVuelo">
                    <div class="tablasTrayectosVuelos">
                        <asp:DataList runat="server" ID="dtlSegmentos">
                            <ItemTemplate>
                                <table width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos azulOscuro" style="font-size: 12px">
                                    <tr>
                                        <td>
                                            <div class="div30px">                                                
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div285px">
                                                <asp:Label CssClass="bold" ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>
                                                <asp:Label ID="Label30" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div220px">
                                                <asp:Label CssClass="bold" ID="Label27" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%>'></asp:Label>
                                                <asp:Label ID="Label35" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div70">
                                            </div>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td>
                                            <div class="div30px">                                                
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div285px">
                                                <asp:Label ID="Label31" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Salida")%>'></asp:Label>
                                                <asp:Label ID="Label29" runat="server" CssClass="noneDisplay" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div220px">
                                                <asp:Label ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Llegada")%>'></asp:Label>
                                                <asp:Label ID="Label33" CssClass="noneDisplay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Llegada")%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <div class="div30px">
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div285px">
                                                Aerolinea:
                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>'></asp:Label>
                                                &nbsp;|&nbsp; 
                                                
                                                Vuelo:
                                                <asp:Label ID="Label78" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%>'></asp:Label>
                                            </div>
                                        </td>

                                        <td colspan="2">
                                            <div>
                                                Tipo de avión:
                                                <asp:Label ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strEquipment")%>'></asp:Label>
                                                &nbsp;|&nbsp; 
                                                Clase:
                                                <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strClase")%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>

                                    <tr class="filaFechasVuelo">
                                        <td>
                                            <div class="div30px">
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div285px">
                                                Salida:
                                                <asp:Label ID="Label28" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%>'></asp:Label>
                                                &nbsp;|&nbsp; 
                                                Llegada:
                                                <asp:Label ID="Label32" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%>'></asp:Label>
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div220px">
                                                <asp:Label ID="Label5" runat="server" Text="Tiempo de vuelo" CssClass="noneDisplay"></asp:Label>
                                                <asp:Label ID="Label37" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ElapsedTime")%>'></asp:Label>
                                                &nbsp;

                                                <div class="iblock fRight cantidadParadas">
                                                    <asp:Label CssClass='<%# DataBinder.Eval(Container.DataItem, "strEstiloParada")%>' ID="Label36" ToolTip='<%# DataBinder.Eval(Container.DataItem, "strDescripcionParadas")%>' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strParadas")%>'></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Text="Paradas"></asp:Label>
                                                </div>
                                            </div>
                                        </td>

                                        <td>
                                            <div class="div70">
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                            </div>
                                        </td>
                                    </tr>

                                    
                                    <tr class="noneDisplay">
                                        <td>
                                        </td>
                                        <td colspan="4" class="division">
                                            <h4 class="trigger">
                                                <a href="javascript:;">Detalles del Vuelo </a>
                                            </h4>
                                            <div class="toggle_container">
                                                
                                                <br />
                                                
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>

                    
                </div>
                <!-- Pasajeros -->
                <div class="tarifasVuelo" style="width:75%;">
                    <div class="detalleTarifa grisOscuro">
                        <asp:Repeater runat="server" ID="RptTiposPasajeros">
                            <ItemTemplate>
                                <asp:Label ID="lblValorSinImp" runat="server" CssClass="noneDisplay"></asp:Label>

                                <asp:Repeater ID="RptTarifas" runat="server">
                                    <ItemTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr class="tituloTarifa">
                                                <td style="font-size:10px; color: #fff; font-weight: bold;">
                                                    Valor por
                                                    <asp:Label ID="Label55" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%></asp:Label>                                                    
                                                </td>

                                                <td>
                                                    <div class="toggle_container">
                                                        Imp. y Cargos
                                                        <asp:Label ID="lblTotalTasas" runat="server"><%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalImpuestosTasas")).ToString("###,###.##")%></asp:Label>
                                                        Valor por tipo de pasajero<br />
                                                        <asp:Label CssClass="bold" ID="Label58" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>&nbsp;&nbsp; <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IntTotalTarifaConTaXPersona")).ToString("###,###.##")%></asp:Label>
                                                    </div>
                                                    <a class="impuestosVuelos" href="#this"><span class="valorTarifa">
                                                        <asp:Label ID="Label3" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>&nbsp;<%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IntTotalTarifaConTaXPersona")).ToString("###,###.##")%></asp:Label>
                                                    </span>
                                                        <div class="GloboImp">
                                                            <span style="color: #034185; float: left; text-align: center;">Detalle de tarifas, impuestos
                                                                y cargos administrativos por
                                                                <%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%></span> <span class="labelDetalleImpuestos">
                                                                    Tarifa</span>
                                                            <asp:Label ID="Label13" runat="server" CssClass="valorlDetalleImpuestos">		
			                                                        <%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>
				                                                        &nbsp;
			                                                        <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "intBaseFare")).ToString("###,###.##")%>
                                                            </asp:Label>
                                                            <asp:Repeater runat="server" ID="RptImpuestos">
                                                                <ItemTemplate>
                                                                    <span class="labelDetalleImpuestos">
                                                                        <%# DataBinder.Eval(Container.DataItem, "strNombre_Impuesto")%>
                                                                    </span><span class="valorlDetalleImpuestos">
                                                                        <%# DataBinder.Eval(Container.DataItem, "CurrencyCode")%>&nbsp;&nbsp;
                                                                        <%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "Amount")).ToString("C")%>
                                                                    </span>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Precios -->
                    <div class="valorTotalVuelo grisOScuro">
                        <asp:Label CssClass="tituloTotal grisOscuro" ID="lblTValorTotal" runat="server" Text="Valor total a pagar"></asp:Label>
                    
                        <div class="iblock full">
                            <span class="monedaTotal grisOscuro">
                                <%#DataBinder.Eval(Container.DataItem, "str_Tipo_Moneda")%>
                            </span>

                            <span class="valorTotal grisOscuro">
                                <%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalPesos")).ToString("###,###.##")%>                                    
                            </span>                            
                        </div>

                        <span class="labelImpuestosIncluidos grisOscuro">
                            Todos los impuestos incluidos
                        </span>
                    </div>

                    <div class="seleccionarVuelos">
                        <asp:Button ID="btnSeleccionar" CssClass="btnAzul" OnClientClick="Show_Cortinilla_Interna();" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "SequenceNumber")%>'  runat="server" Text="Seleccionar"></asp:Button>
                        <asp:Label ID="tituloImpuestoVuelo" ForeColor="red" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strTextoDG")%>'></asp:Label>
                    </div>

                    <div class="">
                        <%--  <asp:DataList Width="100%" runat="server" ID="dtlTiposPasajeros" CellPadding="0" CellSpacing="0" BorderWidth="0" >
                            <ItemTemplate>
                                <asp:DataList Width="100%" ID="dtlTarifas" runat="server" CellPadding="0" CellSpacing="0"
                                    BorderWidth="0">
                                    <ItemTemplate>
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr class="tituloTarifa">
                                                <td>
                                                    Tarifa por 1 
                                                    <asp:Label ID="Label55" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%></asp:Label>
                                                    <br />
                                                    <span class="valorTarifa">
                                                        <asp:Label ID="Label3" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>&nbsp;<%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "intBaseFare")).ToString("###,###.##")%></asp:Label>
                                                        
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <h4 class="trigger">
                                                        <a href="javascript:;">
                                                            Ver detalle
                                                        </a>
                                                    </h4>
                                                    <div class="toggle_container">
                                                        Imp. y Cargos <asp:Label ID="lblTotalTasas" runat="server"><%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalImpuestosTasas")).ToString("###,###.##")%></asp:Label>
                                                        Valor por tipo de pasajero<br />
                                                        <asp:Label CssClass="bold" ID="Label58" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>&nbsp;&nbsp; <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IntTotalTarifaConTaXPersona")).ToString("###,###.##")%></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ItemTemplate>
                        </asp:DataList>--%>
                    </div>
                </div>
                <div class="no_home t_center noneDisplay" style="float: left;">
                    <span class="white t_center block">Encuentra <a id="aqui" href="" target="_blank"
                        class="yellow cursor">aquí </a>las mejores tarifas en nuestro Club Top Flight.<br>
                        En Club Top Flight: <span class="yellow">$<asp:Label ID="lblValorSinImp" runat="server"></asp:Label></span>
                    </span>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="mensajeError" id="dPanel" runat="server">
</div>
<ajax:ModalPopupExtender ID="MPEEAdicionales" BackgroundCssClass="ui-widget-shadow"
    DropShadow="false" runat="server" TargetControlID="dummyLink4" Drag="false" BehaviorID="MPEEAdicionales"
    OnOkScript="" OkControlID="btnCerrarAdicionales" EnableViewState="true" PopupControlID="PanelAdicionales" />
<asp:Panel runat="server" ID="PanelAdicionales">
    <div class="ventana4">
        <table width="100%" cellspacing="0" cellpadding="2">
            <tr class="tituloPopUp">
                <td>
                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                </td>
                <td style="text-align: right">
                    <asp:Button ID="btnCerrarAdicionales" CssClass="botonCerrar" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink4"
    runat="server">dummy>dummy</a>