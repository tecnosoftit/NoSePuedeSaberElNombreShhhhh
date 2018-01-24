<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucResultadoVuelosHora.ascx.cs"
    Inherits="uc_ucResultadoVuelosHora" %>
<%@ Register Src="../uc/ucBuscadorAereo.ascx" TagName="ucBuscadorAereo" TagPrefix="uc1" %>
<div class="panelCompletoVuelos">
    <div class="panelResumen">
        <div class="resumenVuelos">
            <asp:Label ID="lblOrigen" runat="server" Text=""></asp:Label>,
            <asp:Label ID="lblDestino" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblFechaSalida" runat="server" Text=""></asp:Label>
            -
            <asp:Label ID="lblFechaLlegada" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="lblPax" runat="server" Text=""></asp:Label>
        </div>
        <div class="nuevaBusquedaVuelos">
            <asp:Button ID="btnBusqueda" CssClass="botonBusqueda" runat="server" Text="Nueva búsqueda">
            </asp:Button>
        </div>
    </div>
</div>
<asp:Repeater ID="rptSeleccion" runat="server" OnItemCommand="rptItinerario_ItemCommand">
    <HeaderTemplate>
    </HeaderTemplate>
    <ItemTemplate>
        <!--- itinerario de ida -->
        <div class="panelCompletoVuelos">
            <div class="panelResultados" style="display: none;" id="divIda" runat="server">
                <div class="tituloResultados">
                    <asp:Label ID="lblIda" Text="ITINERARIO DE IDA  &raquo;" runat="server"></asp:Label>
                </div>
                <div class="contenidoResultado">
                    <asp:Repeater runat="server" ID="RptSegmentosIda">
                        <ItemTemplate>
                            <table width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos">
                                <tr class="tituloTabla">
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="lblTAerolinea" runat="server" Text="Aerolínea"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label1" runat="server" Text="Ruta"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="lblTFechaSalida" runat="server" Text="Fecha y Hora Salida"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="lblTFechaLlegada" runat="server" Text="Fecha y Hora Llegada"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label4" runat="server" Text="Vuelo"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label24" runat="server" Text="Clase"></asp:Label>
                                    </td>
                                    <td class="item2">
                                        <asp:Label CssClass="bold" ID="Label25" runat="server" Text="Paradas"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="alineacionSuperior">
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label76" runat="server"><%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%></asp:Label><br />
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label5" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%></asp:Label>
                                        <asp:Label CssClass="bold" ID="Label6" runat="server" Text="-"></asp:Label>
                                        <asp:Label CssClass="bold" ID="Label7" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%></asp:Label><br />
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label28" runat="server"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%></asp:Label><br />
                                        <asp:Label ID="Label29" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%></asp:Label><br />
                                        <asp:Label ID="Label31" runat="server"><%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Salida")%></asp:Label><br />
                                        <asp:Label ID="Label30" runat="server"><%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label32" runat="server"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%></asp:Label><br />
                                        <asp:Label ID="Label33" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_Llegada")%></asp:Label><br />
                                        <asp:Label ID="Label34" runat="server"><%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Llegada")%></asp:Label><br />
                                        <asp:Label ID="Label35" runat="server"><%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("yyyy/MM/dd"), "-")%></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label10" runat="server"><%# DataBinder.Eval(Container.DataItem, "FlightNumber")%></asp:Label><br />
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strClase")%>'></asp:Label>
                                    </td>
                                    <td class="item2">
                                        <asp:Label CssClass="parada" ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strParadas")%>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <!--- itinerario de regreso -->
        <div class="panelCompletoVuelos">
            <div class="panelResultados" style="display: none;" id="divRegreso" runat="server">
                <div class="tituloResultados">
                    <asp:Label ID="lblRegreso" Text="ITINERARIO DE REGRESO &raquo;" runat="server"></asp:Label>
                </div>
                <div class="contenidoResultado">
                    <asp:Repeater runat="server"  ID="RptSegmentosRegreso" >
                        <ItemTemplate>
                            <table width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos">
                                <tr class="tituloTabla">
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="lblTAerolinea" runat="server" Text="Aerolínea"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label1" runat="server" Text="Ruta"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="lblTFechaSalida" runat="server" Text="Fecha y Hora Salida"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="lblTFechaLlegada" runat="server" Text="Fecha y Hora Llegada"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label4" runat="server" Text="Vuelo"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label24" runat="server" Text="Clase"></asp:Label>
                                    </td>
                                    <td class="item2">
                                        <asp:Label CssClass="bold" ID="Label25" runat="server" Text="Paradas"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="alineacionSuperior">
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label76" runat="server"><%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%></asp:Label><br />
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label5" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%></asp:Label>
                                        <asp:Label CssClass="bold" ID="Label6" runat="server" Text="-"></asp:Label>
                                        <asp:Label CssClass="bold" ID="Label7" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%></asp:Label><br />
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label28" runat="server"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%></asp:Label><br />
                                        <asp:Label ID="Label29" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%></asp:Label><br />
                                        <asp:Label ID="Label31" runat="server"><%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Salida")%></asp:Label><br />
                                        <asp:Label ID="Label30" runat="server"><%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label32" runat="server"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%></asp:Label><br />
                                        <asp:Label ID="Label33" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_Llegada")%></asp:Label><br />
                                        <asp:Label ID="Label34" runat="server"><%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Llegada")%></asp:Label><br />
                                        <asp:Label ID="Label35" runat="server"><%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("yyyy/MM/dd"), "-")%></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label10" runat="server"><%# DataBinder.Eval(Container.DataItem, "FlightNumber")%></asp:Label><br />
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strClase")%>'></asp:Label>
                                    </td>
                                    <td class="item2">
                                        <asp:Label CssClass="parada" ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strParadas")%>'></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </ItemTemplate>
    <FooterTemplate>
    </FooterTemplate>
</asp:Repeater>
<div class="tituloSelecionVuelo" id="divTituloxhora" runat="server">
    <asp:Label ID="lblTituloSelect" Text="Seleccione el itinerario de ida" runat="server"></asp:Label>
</div>
<!--- seleccion de vuelo -->
<asp:Repeater ID="rptItinerario" runat="server" OnItemCommand="rptItinerario_ItemCommand">
    <ItemTemplate>
        <div class="panelCompletoVuelos">
            <div class="panelResultados">
                <div class="tituloResultados">
                    <asp:Label ID="Label2" runat="server" Text="OPCIÓN &raquo;"></asp:Label>
                    <asp:Label ID="Label3" runat="server"><%# DataBinder.Eval(Container.DataItem, "SequenceNumber")%></asp:Label>
                </div>
                <div class="contenidoResultado">
                    <asp:Repeater runat="server"  ID="RptSegmentos">
                        <HeaderTemplate>
                            <table width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos">
                                <tr class="tituloTabla">
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="lblTAerolinea" runat="server" Text="Aerolínea"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label1" runat="server" Text="Ruta"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="lblTFechaSalida" runat="server" Text="Fecha y Hora Salida"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="lblTFechaLlegada" runat="server" Text="Fecha y Hora Llegada"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label4" runat="server" Text="Vuelo"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label CssClass="bold" ID="Label24" runat="server" Text="Clase"></asp:Label>
                                    </td>
                                    <td class="item2">
                                        <asp:Label CssClass="bold" ID="Label25" runat="server" Text="Paradas"></asp:Label>
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="alineacionSuperior">
                                <td class="item">
                                    <asp:Label CssClass="bold" ID="Label76" runat="server"><%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%></asp:Label><br />
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                </td>
                                <td class="item">
                                    <asp:Label CssClass="bold" ID="Label5" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%></asp:Label>
                                    <asp:Label CssClass="bold" ID="Label6" runat="server" Text="-"></asp:Label>
                                    <asp:Label CssClass="bold" ID="Label7" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%></asp:Label><br />
                                </td>
                                <td class="item">
                                    <asp:Label CssClass="bold" ID="Label28" runat="server"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%></asp:Label><br />
                                    <asp:Label ID="Label29" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%></asp:Label><br />
                                    <asp:Label ID="Label31" runat="server"><%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Salida")%></asp:Label><br />
                                    <asp:Label ID="Label30" runat="server"><%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%></asp:Label>
                                </td>
                                <td class="item">
                                    <asp:Label CssClass="bold" ID="Label32" runat="server"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%></asp:Label><br />
                                    <asp:Label ID="Label33" runat="server"><%# DataBinder.Eval(Container.DataItem, "strCiudad_Llegada")%></asp:Label><br />
                                    <asp:Label ID="Label34" runat="server"><%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Llegada")%></asp:Label><br />
                                    <asp:Label ID="Label35" runat="server"><%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("yyyy/MM/dd"), "-")%></asp:Label>
                                </td>
                                <td class="item">
                                    <asp:Label ID="Label10" runat="server"><%# DataBinder.Eval(Container.DataItem, "FlightNumber")%></asp:Label><br />
                                </td>
                                <td class="item">
                                    <asp:Label CssClass="bold" ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strClase")%>'></asp:Label>
                                </td>
                                <td class="item2">
                                    <asp:Label CssClass="parada" ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strParadas")%>'></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div style="width: 100%; padding: 5px; text-align: center;">
                        <asp:Button ID="btnSeleccionar" CssClass="botonBuscar" CommandName='<%# DataBinder.Eval(Container.DataItem, "RPH")%>'
                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OriginDestinationOption_Id")%>'
                            runat="server" Text="Seleccionar"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
    <FooterTemplate>
    </FooterTemplate>
</asp:Repeater>
<!--- detalle tarifas -->
<asp:Repeater ID="rptItinerarioDetalle" runat="server" OnItemCommand="rptItinerario_ItemCommand">
    <ItemTemplate>
        <div class="panelCompletoVuelos">
            <div class="panelResultados">
                <div class="tituloResultados">
                    <asp:Label ID="Label13" runat="server" Text="Detalle Tarifas"></asp:Label>
                </div>
                <div class="contenidoResultado">
                    <div class="contenidoResultados2">
                        <table width="100%" cellpadding="2" cellspacing="1">
                            <tr class="alineacionCentro">
                                <td>
                                    <asp:Label CssClass="tituloValor" ID="Label46" runat="server" Text="Valor Total de Viaje"></asp:Label>
                                    <asp:Label CssClass="tituloPrecioTotalVuelo" ID="lblTotal" runat="server"><%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalPesos")).ToString("C")%></asp:Label>
                                    <br />
                                    <asp:Label ID="tituloImpuestoVuelo" ForeColor="#186e9b" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strTextoDG")%>'></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <img src='<%# DataBinder.Eval(Container.DataItem, "imgOferta")%>' alt="" />
                                    <img src='<%# DataBinder.Eval(Container.DataItem, "imgConvenio")%>' alt="" />
                                </td>
                            </tr>
                            <tr class="textoNormal">
                                <td colspan="2">
                                    <asp:Repeater runat="server" ID="RptTiposPasajeros">
                                        <HeaderTemplate>
                                            <table width="100%" cellpadding="0" cellspacing="0" class="tablaTarifas">
                                                <tr class="tituloTabla">
                                                    <td class="item" style="width: 15%">
                                                        <asp:Label CssClass="bold" ID="Label12" runat="server" Text="Pasajero"></asp:Label>
                                                    </td>
                                                    <td class="item" style="width: 10%">
                                                        <asp:Label CssClass="bold" ID="Label9" runat="server" Text="Tarifa"></asp:Label>
                                                    </td>
                                                    <td class="item" style="width: 20%">
                                                        <asp:Label CssClass="bold" ID="Label7" runat="server" Text="Impuestos y tasas"></asp:Label>
                                                    </td>
                                                    <td class="item" style="width: 25%">
                                                        <asp:Label CssClass="bold" ID="Label18" runat="server" Text="Valor por tipo de pasajero"></asp:Label>
                                                    </td>
                                                    <td class="item" style="width: 5%">
                                                        <asp:Label CssClass="bold" ID="Label19" runat="server" Text="Cantidad"></asp:Label>
                                                    </td>
                                                    <td class="item2" style="width: 25%">
                                                        <asp:Label CssClass="bold" ID="Label8" runat="server" Text="Tarifa total por pasajero"></asp:Label>
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:Repeater ID="RptTarifas" runat="server">
                                                        <ItemTemplate>
                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="item" style="width: 15%">
                                                                        <asp:Label ID="Label55" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%></asp:Label>
                                                                        <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                    <td class="item" style="width: 10%">
                                                                        <asp:Label ID="Label3" runat="server"><%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "intBaseFare")).ToString("###,###.##")%></asp:Label>
                                                                    </td>
                                                                    <td class="item" style="width: 20%">
                                                                        <a class="tarifa" href="#">
                                                                            <asp:Label ID="lblTotalTasas" runat="server"><%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalImpuestosTasas")).ToString("###,###.##")%></asp:Label>
                                                                            <div>
                                                                                <asp:Repeater runat="server" ID="RptImpuestos">
                                                                                    <ItemTemplate>
                                                                                        <table align="left">
                                                                                            <tr>
                                                                                                <td style="width: 150px;">
                                                                                                    <strong>
                                                                                                        <%# DataBinder.Eval(Container.DataItem, "strNombre_Impuesto")%>
                                                                                                    </strong>
                                                                                                </td>
                                                                                                <td style="width: 100px; text-align: right">
                                                                                                    <%# DataBinder.Eval(Container.DataItem, "CurrencyCode")%>&nbsp;&nbsp;
                                                                                                    <%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "Amount")).ToString("C")%>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </div>
                                                                        </a>
                                                                    </td>
                                                                    <td class="item" style="width: 25%">
                                                                        <asp:Label CssClass="bold" ID="Label58" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>&nbsp;&nbsp; <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IntTotalTarifaConTaXPersona")).ToString("###,###.##")%></asp:Label>
                                                                    </td>
                                                                    <td class="item" style="width: 5%">
                                                                        <asp:Label CssClass="bold" ID="Label1" runat="server"><%# DataBinder.Eval(Container.DataItem, "intCantidad")%></asp:Label>
                                                                    </td>
                                                                    <td class="item2" style="width: 25%">
                                                                        <asp:Label CssClass="bold" ID="Label2" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>&nbsp;&nbsp; <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IntTotalTarifaConTa")).ToString("###,###.##")%></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 100%; text-align: center;">
                            <asp:Button ID="btnSeleccionar" OnClientClick="Show_Cortinilla_Interna();" CssClass="botonBuscar"
                                CommandArgument="Cotizar" runat="server" Text="Continuar"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<div class="mensajeError" id="dPanel" runat="server">
</div>
<div class="panelResultados" style="text-align: right;">
    <input type="button" class="botonBuscar" value="Nueva búsqueda" onclick="javascript:$('#ucResultadoVuelosHora_btnBusqueda').click();" />
</div>
<!-- Buscador aereo -->
<ajax:ModalPopupExtender ID="MPEEBuscadorAereo" BackgroundCssClass="ui-widget-shadow"
    DropShadow="false" runat="server" TargetControlID="btnBusqueda" BehaviorID="MPEEBuscadorAereo"
    OnOkScript="" OkControlID="btnCerrar2" EnableViewState="true" PopupControlID="PanelBuscador" />
<asp:Panel runat="server" ID="PanelBuscador">
    <div class="ventanaBuscador">
        <table width="100%" cellspacing="0" cellpadding="2">
            <tr class="tituloBuscador">
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Realizar Nueva Búsqueda"></asp:Label>
                </td>
                <td style="text-align: right">
                    <asp:Button ID="btnCerrar2" CssClass="botonCerrar" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <uc1:ucBuscadorAereo ID="ucBuscadorAereo" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
