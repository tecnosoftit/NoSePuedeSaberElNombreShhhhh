<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucCarroCompras.ascx.cs"
    Inherits="uc_ucCarroCompras" %>

<%@ Register Src="../uc/ucBuscadorT.ascx" TagName="ucBuscador" TagPrefix="uc4" %>
<%@ Register Src="../uc/ucPopoupMensaje.ascx" TagName="ucPopoupMensaje" TagPrefix="uc6" %>
<div class="panelTabs">
    <div class="contTabs">
        <div class="tabExterior">
            <div class="tabSegundo">
                Paso 1: Confirma tu reserva
            </div>
        </div>
        <div class="tabExterior">
            <div class="tabInterior">
                Paso 2: Escoje tu forma de pago</div>
        </div>
    </div>
    <div class="panelCompletoCorporativo">
        <div class="confirmarVuelo">
            <div class="tituloFormaPago">
                1. Reserva confirmada
            </div>
            <div class="detalleFormaPago">
                Tu reserva ha sido solicitada y asignada
                <asp:Label runat="server" ID="lblLocalizador"></asp:Label>
            </div>
        </div>
        <div class="confirmarVuelo">
            <div class="tituloFormaPago">
                2. ELEGIR FORMA DE PAGO</div>
            <div class="detalleFormaPago">
                Hasta el momento haz reservado los servicios especificados a coninuacion. Puedes
                elegir un medio de pago para finalizar tu compra ó seguir comprando y posteriormente
                elegir un medio de pago. <a id="buscador" runat="server" href="">Adicionar más servicios
                    a mi compra</a>
            </div>
        </div>
        <!-- Encabezado -->
        <div class="panelResultados" style="display: none">
            <div class="tituloResultados">
                <asp:Label ID="lblTCarroCompras" runat="server" Text="CARRO DE COMPRAS - TOTAL SERVICIOS RESERVADOS"></asp:Label>
            </div>
            <div class="contenidoResultadoCarro">
                <div class="textoCarro">
                    <asp:Repeater runat="server" ID="rptSeccion">
                        <ItemTemplate>
                            <asp:Label Font-Size="Small" ID="lblDescCarro" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strDescripcion") %></asp:Label>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="valoresCarro">
                    <table width="100%">
                        <tr>
                            <td style="width: 30px;">
                                <img alt="" src="../App_Themes/Imagenes/bullet2.png" />
                            </td>
                            <td colspan="2">
                                <asp:Label ID="strTitulo" CssClass="boldTerminos" runat="server" Text="Total de la compra"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="valorTotalServicios">
                                <asp:Label ID="lblTServiciosPesos" runat="server" Text="Servicios en pesos"></asp:Label>
                            </td>
                            <td class="valorTotalServiciosPrecio">
                                <asp:Label ID="lblMonedaPeso" runat="server" Text="COP"></asp:Label>&nbsp;
                                <asp:Label ID="lblTotalPesos" runat="server" Text="2.000.000"></asp:Label>
                                <asp:Label ID="lblTotal" runat="server" Text="0" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="valorTotalServicios">
                                <asp:Label ID="lblTServiciosDolares" runat="server" Text="Servicios en dólares"></asp:Label>
                            </td>
                            <td class="valorTotalServiciosPrecio">
                                <asp:Label ID="lblMonedaDolar" runat="server" Text="USD"></asp:Label>&nbsp;
                                <asp:Label ID="lblTotalDolares" runat="server" Text="1.450"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="valorTotalServicios">
                                <asp:Label ID="lblTServiciosEuros" runat="server" Text="Servicios en euros"></asp:Label>
                            </td>
                            <td class="valorTotalServiciosPrecio">
                                <asp:Label ID="lblMonedaEuro" runat="server" Text="EUR"></asp:Label>&nbsp;
                                <asp:Label ID="lblTotalEuros" runat="server" Text="1.000"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <!-- Circuito -->
        <asp:Panel ID="pnCircuitos" runat="server">
            <div class="panelResultados">
                <div class="tituloResultadosCircuito">
                    <asp:Label ID="lblTCircuito" runat="server" Text="RESERVA DE PLAN CIRCUITO &raquo;"></asp:Label>
                </div>
                <div class="contenidoResultadoCarro">
                    <asp:Repeater ID="rptCircuitos" runat="server" OnItemCommand="rptCircuitos_ItemCommand">
                        <ItemTemplate>
                            <table width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos">
                                <tr class="tituloTabla">
                                    <td class="item">
                                        <asp:Label ID="lblTNombrePlan" runat="server" Text="Nombre del Plan:"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTDescripcion" runat="server" Text="Descripción"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTFechaServicioCir" runat="server" Text="Fecha del Servicio"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTCantidadPasajerosCir" runat="server" Text="Cantidad de pasajeros"
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="alineacionSuperior">
                                    <td class="item">
                                        <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NombrePlan") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label32" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Descripcion") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaServicio") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Cantidad") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="precioCarrito">
                                        <asp:Label ID="lblTValorCir" runat="server" Text="Valor: "></asp:Label>
                                    </td>
                                    <td colspan="3" class="precioCarrito">
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Valor") %>'></asp:Label><br />
                                        <asp:Label ID="Label9" runat="server" Text="COP" Visible="false"></asp:Label>&nbsp;
                                        <asp:Label ID="Label25" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ValorPesos") %>'></asp:Label>
                                        <asp:Label ID="lblIndiceTabla" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IndiceTablaPrincipal") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoPlan") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="recordReservaCarro">
                                            <asp:Label ID="lblTRecordReservaCir" runat="server" Text="Récord reserva: "></asp:Label>
                                            <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'></asp:Label>
                                        </div>
                                    </td>
                                    <td colspan="2" style="text-align: right">
                                        <asp:Button CssClass="quitarServicio" ID="Button3" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </asp:Panel>
        <!-- Cruceros -->
        <asp:Panel ID="pnCruceros" runat="server">
            <div class="panelResultados">
                <div class="tituloResultadosCircuito">
                    <asp:Label ID="lblTCruceros" runat="server" Text="RESERVA DE PLAN CRUCEROS &raquo;"></asp:Label>
                </div>
                <div class="contenidoResultadoCarro">
                    <asp:Repeater ID="rptCruceros" runat="server" OnItemCommand="rptCircuitos_ItemCommand">
                        <ItemTemplate>
                            <table width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos">
                                <tr class="tituloTabla">
                                    <td class="item">
                                        <asp:Label ID="lblTNombrePlanCru" runat="server" Text="Nombre del Plan"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label29" runat="server" Text="Duracion"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTDescripcionCru" runat="server" Text="Descripción"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTFechaServicioCru" runat="server" Text="Fecha del Servicio"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTCantidadPasajerosCru" runat="server" Text="Cantidad de pasajeros:"
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="alineacionSuperior">
                                    <td class="item">
                                        <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NombrePlan") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intNumeroDias") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="Label16" runat="server" Text="Días"></asp:Label>&nbsp;
                                        <asp:Label ID="Label17" runat="server" Text="/"></asp:Label>&nbsp;
                                        <asp:Label ID="Label19" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intNumeroNoches") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="Label20" runat="server" Text="Noches"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label32" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Descripcion") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label35" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaServicio") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Cantidad") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="precioCarrito">
                                        <asp:Label ID="lblTValorCru" runat="server" Text="Valor: "></asp:Label>
                                    </td>
                                    <td colspan="4" class="precioCarrito">
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Valor") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label37" runat="server" Text="COP" Visible="false"></asp:Label>&nbsp;
                                        <asp:Label ID="Label25" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ValorPesos") %>'></asp:Label>
                                        <asp:Label ID="lblIndiceTabla" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IndiceTablaPrincipal") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoPlan") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="recordReservaCarro">
                                            <asp:Label ID="lblTRecordReservaCru" runat="server" Text="Récord reserva:"></asp:Label>
                                            <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'></asp:Label>
                                        </div>
                                    </td>
                                    <td colspan="2" style="text-align: right">
                                        <asp:Button CssClass="quitarServicio" ID="Button3" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </asp:Panel>
        <!-- Asistencia -->
        <asp:Panel ID="pnAsistencia" runat="server">
            <div class="panelResultados">
                <div class="tituloResultadosCircuito">
                    <asp:Label ID="lblTAsistencia" runat="server" Text="TARJETA DE ASISTENCIA &raquo;"></asp:Label>
                </div>
                <div class="contenidoResultadoCarro">
                    <table width="100%">
                        <asp:Repeater ID="rptTarjetas" runat="server" OnItemCommand="rptCircuitos_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <asp:Label ID="lblNombrePlanAsis" runat="server" Text="Nombre del Plan:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="font-weight: bold; color: #186e9b;">
                                        <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NombrePlan") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTContinenteAsis" runat="server" Text="Continente de Destino:"></asp:Label></strong>
                                        <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ZonaGeo") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTTipoTarjetaAsis" runat="server" Text="Tipo de Tarjeta"></asp:Label>
                                        </strong>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="Label32" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TipoTarjeta") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTFechaSalidaAsis" runat="server" Text="Fecha de Salida:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaServicio") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTCantidadPasajerosAsis" runat="server" Text="Cantidad de pasajeros:"></asp:Label>
                                        </strong>
                                        <asp:Label ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.CantidadPasajeros") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTFechaRegresoAsis" runat="server" Text="Fecha de Regreso:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaRegreso") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="precioCarrito">
                                        <asp:Label ID="lblTValorAsis" runat="server" Text="Valor: "></asp:Label>
                                    </td>
                                    <td class="precioCarrito">
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Valor") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Text="COP" Visible="false"></asp:Label>&nbsp;
                                        <asp:Label ID="Label25" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ValorPesos") %>'></asp:Label>
                                        <asp:Label ID="lblIndiceTabla" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IndiceTablaPrincipal") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoPlan") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button CssClass="botonBusqueda" ID="Button3" runat="server" Text="Anular este servicio" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <hr />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <!-- Vuelos -->
        <asp:Panel ID="pnVuelos" runat="server">
            <div class="confirmarVuelo">
                <div class="subtituloDetalle">
                    tiquetes
                </div>
                <div class="resumenReserva">
                    <asp:Repeater ID="rptVuelos" runat="server" OnItemCommand="rptCircuitos_ItemCommand">
                        <ItemTemplate>
                            <table width="830" border="0" cellspacing="0" cellpadding="6" class="resumenTabla">
                                <tr class="txtAmarillo">
                                    <td class="item">
                                        <asp:Label ID="Label12" runat="server" Text="Aerolínea"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label24" runat="server" Text="Ruta"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label21" runat="server" Text="Salida"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label23" runat="server" Text="llegada"></asp:Label>
                                    </td>
                                    <td class="item2">
                                        <asp:Label ID="Label15" runat="server" Text="Cantidad"></asp:Label>
                                    </td>
                                    <td class="precioCarrito">
                                        <asp:Label ID="lblTValorVue" runat="server" Text="Valor: "></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <div style="border-top: solid 1px #666;">
                                        </div>
                                    </td>
                                </tr>
                                <tr class="alineacionSuperior">
                                    <td class="item">
                                        <asp:Label ID="Label281" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NombrePlan") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label301" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Ciudad") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label41" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaServicio") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label361" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaFinal") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Cantidad") %>'></asp:Label>
                                    </td>
                                    <td colspan="3" class="precioCarrito">
                                        <asp:Label ID="Label38" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Valor") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Text="COP" Visible="false"></asp:Label>&nbsp;
                                        <asp:Label ID="Label25" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ValorPesos") %>'></asp:Label>
                                        <asp:Label ID="lblIndiceTabla" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IndiceTablaPrincipal") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoPlan") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="recordReservaCarro">
                                            <asp:Label ID="lblTRecordVue" runat="server" Text="Récord reserva: "></asp:Label>
                                            <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'></asp:Label>
                                        </div>
                                    </td>
                                    <%-- <td colspan="2" style="text-align: right">
                                <asp:Button CssClass="quitarServicio" ID="Button31" runat="server" Text="" />
                            </td>--%>
                                </tr>
                                <td colspan="6">
                                    <div style="border-top: solid 1px #666;">
                                    </div>
                                </td>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </asp:Panel>
        <!-- Tame -->
        <asp:Panel ID="pnTame" runat="server">
            <div class="panelResultados">
                <div class="tituloResultadosCircuito">
                    <asp:Label ID="Label33" runat="server" Text="TIQUETE AEREO &raquo;"></asp:Label>
                </div>
                <div class="contenidoResultadoCarro">
                    <asp:Repeater ID="rptTame" runat="server" OnItemCommand="rptCircuitos_ItemCommand">
                        <ItemTemplate>
                            <table width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos">
                                <tr class="tituloTabla">
                                    <td class="item">
                                        <asp:Label ID="lblTAerolineaVue" runat="server" Text="Aerolínea"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTRutaVue" runat="server" Text="Ruta"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTFechaSalidaVue" runat="server" Text="Fecha de Salida"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label351" runat="server" Text="Fecha de Regreso"></asp:Label>
                                    </td>
                                    <td class="item2">
                                        <asp:Label ID="lblCantidadPasajerosVue" runat="server" Text="Cantidad de pasajeros"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="alineacionSuperior">
                                    <td class="item">
                                        <asp:Label ID="Label281" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NombrePlan") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label301" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Ciudad") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label41" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaServicio") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label361" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaFinal") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label22" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Cantidad") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="precioCarrito">
                                        <asp:Label ID="lblTValorVue" runat="server" Text="Valor: "></asp:Label>
                                    </td>
                                    <td colspan="3" class="precioCarrito">
                                        <asp:Label ID="Label38" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Valor") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Text="COP" Visible="false"></asp:Label>&nbsp;
                                        <asp:Label ID="Label25" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ValorPesos") %>'></asp:Label>
                                        <asp:Label ID="lblIndiceTabla" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IndiceTablaPrincipal") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoPlan") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="recordReservaCarro">
                                            <asp:Label ID="lblTRecordVue" runat="server" Text="Récord reserva:"></asp:Label>
                                            <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'></asp:Label>
                                        </div>
                                    </td>
                                    <td colspan="2" style="text-align: right">
                                        <asp:Button CssClass="quitarServicio" ID="Button31" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </asp:Panel>
        <!-- Casas -->
        <asp:Panel ID="pnCasas" runat="server">
            <div class="panelResultados">
                <div class="tituloResultadosCircuito">
                    <asp:Label ID="lblTApartamentos" runat="server" Text="APARTAMENTOS &raquo;"></asp:Label>
                </div>
                <div class="contenidoResultadoCarro">
                    <table width="100%">
                        <asp:Repeater ID="rptCasas" runat="server" OnItemCommand="rptCircuitos_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <asp:Label ID="lblTRecordCasa" runat="server" Text="Récord reserva:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTNombrePlanCasa" runat="server" Text="Nombre del plan:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="Label281" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NombrePlan") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTCiudadCasa" runat="server" Text="Ciudad:"></asp:Label></strong>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="Label301" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Ciudad") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTFechaInicioCasa" runat="server" Text="Fecha inicio alquiler:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label41" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaServicio") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTFechaFinCasa" runat="server" Text="Fecha fin alquiler: "></asp:Label>
                                        </strong>
                                        <asp:Label ID="Label361" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaFinal") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="precioCarrito">
                                        <asp:Label ID="lblTValorCasa" runat="server" Text="Valor: "></asp:Label>
                                    </td>
                                    <td class="precioCarrito">
                                        <asp:Label ID="Label38" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Valor") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Text="COP" Visible="false"></asp:Label>&nbsp;
                                        <asp:Label ID="Label25" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ValorPesos") %>'></asp:Label>
                                        <asp:Label ID="lblIndiceTabla" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IndiceTablaPrincipal") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoPlan") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button CssClass="botonBusqueda" ID="Button31" runat="server" Text="Anular este servicio" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 10px;">
                                        <hr />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <!-- Hoteles -->
        <asp:Panel ID="pnHoteles" runat="server">
            <div class="confirmarVuelo">
                <div class="subtituloDetalle">
                    <asp:Label ID="Label39" runat="server" Text="HOTELES  &raquo;"></asp:Label>
                </div>
                <div class="resumenReserva">
                    <asp:Repeater ID="rptHoteles" runat="server" OnItemCommand="rptCircuitos_ItemCommand">
                        <ItemTemplate>
                            <table width="830" border="0" cellspacing="0" cellpadding="6" class="resumenTabla">
                                <tr class="txtAmarillo">
                                    <td class="item">
                                        <asp:Label ID="lblTNombreHotel" runat="server" Text="Nombre del Hotel"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTCiudadHot" runat="server" Text="Ciudad"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTFechaInicioHot" runat="server" Text="Fecha de inicio viaje"></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="lblTCantidadNochesHot" runat="server" Text="Cantidad de noches"></asp:Label>
                                    </td>
                                    <td class="precioCarrito">
                                        <asp:Label ID="lblTValorCir" runat="server" Text="Valor: "></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <div style="border-top: solid 1px #666;">
                                        </div>
                                    </td>
                                </tr>
                                <tr class="alineacionSuperior">
                                    <td class="item">
                                        <asp:Label ID="Label282" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NombrePlan") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label302" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Ciudad") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label42" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaServicio") %>'></asp:Label>
                                    </td>
                                    <td class="item">
                                        <asp:Label ID="Label362" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IntNumeroNoches") %>'></asp:Label>
                                    </td>
                                    <td colspan="3" class="precioCarrito">
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Valor") %>'></asp:Label><br />
                                        <asp:Label ID="Label9" runat="server" Text="COP" Visible="false"></asp:Label>&nbsp;
                                        <asp:Label ID="Label25" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ValorPesos") %>'></asp:Label>
                                        <asp:Label ID="lblIndiceTabla" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IndiceTablaPrincipal") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoPlan") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="recordReservaCarro">
                                            <asp:Label ID="lblTRecordReservaCir" runat="server" Text="Récord reserva: "></asp:Label>
                                            <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'></asp:Label>
                                        </div>
                                    </td>
                                    <td colspan="2" style="text-align: right">
                                        <asp:Button CssClass="quitarServicio" ID="Button3" runat="server" Text="" />
                                    </td>
                                </tr>
                                <td colspan="6">
                                    <div style="border-top: solid 1px #666;">
                                    </div>
                                </td>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </asp:Panel>
        <!-- Carros -->
        <asp:Panel ID="pnCarros" runat="server">
            <div class="panelResultados">
                <div class="tituloResultadosCircuito">
                    <asp:Label ID="lblTCarrosCar" runat="server" Text="CARROS &raquo;"></asp:Label>
                </div>
                <div class="contenidoResultadoCarro">
                    <table width="100%">
                        <asp:Repeater ID="rptCarros" runat="server" OnItemCommand="rptCircuitos_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <asp:Label ID="lblTNombreRentadora" runat="server" Text="Nombre de Rentadora:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td colspan="2" style="font-weight: bold; color: #186e9b;">
                                        <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NombrePlan") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTCiudadCar" runat="server" Text="Ciudad recoge:"></asp:Label></strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Ciudad") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTTipoAuto" runat="server" Text="Tipo auto: "></asp:Label>
                                        </strong>
                                        <asp:Label ID="Label363" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TipoTarjeta") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTFechaInicioCar" runat="server" Text="Fecha de inicio viaje:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaServicio") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTFechaRegresoCar" runat="server" Text="Fecha de Regreso: "></asp:Label>
                                        </strong>
                                        <asp:Label ID="Label361" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaFinal") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="precioCarrito">
                                        <asp:Label ID="lblTValorCar" runat="server" Text="Valor: "></asp:Label>
                                    </td>
                                    <td class="precioCarrito">
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Valor") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Text="COP" Visible="false"></asp:Label>&nbsp;
                                        <asp:Label ID="Label25" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ValorPesos") %>'></asp:Label>
                                        <asp:Label ID="lblIndiceTabla" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IndiceTablaPrincipal") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoPlan") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="recordReservaCarro">
                                            <asp:Label ID="lblTRecordCar" runat="server" Text="Récord reserva:"></asp:Label>
                                            <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'></asp:Label>
                                        </div>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button CssClass="quitarServicio" ID="Button3" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <hr />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <!-- Excursiones -->
        <asp:Panel ID="pnExcursiones" runat="server">
            <div class="panelResultados">
                <div class="tituloResultadosCircuito">
                    <asp:Label ID="lblTExcursiones" runat="server" Text="RESERVA DE PLAN EXCURSIONES &raquo;"></asp:Label>
                </div>
                <div class="contenidoResultadoCarro">
                    <table width="100%">
                        <asp:Repeater ID="rptExcursiones" runat="server" OnItemCommand="rptCircuitos_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td style="width: 15%">
                                        <strong>
                                            <asp:Label ID="lblNombrePlanExc" runat="server" Text="Nombre del Plan:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="font-weight: bold; color: #186e9b;">
                                        <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NombrePlan") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTCiudadExc" runat="server" Text="Ciudad:"></asp:Label></strong>&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Ciudad") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTFechaServicioExc" runat="server" Text="Fecha del Servicio:"></asp:Label>
                                        </strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaServicio") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblTCantidadPasajerosExc" runat="server" Text="Cantidad de pasajeros:"></asp:Label>
                                        </strong>
                                        <asp:Label ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Cantidad") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="precioCarrito">
                                        <asp:Label ID="lblTValorExc" runat="server" Text="Valor: "></asp:Label>
                                    </td>
                                    <td class="precioCarrito">
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>&nbsp;
                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Valor") %>'></asp:Label>
                                        <br />
                                        <asp:Label ID="Label9" runat="server" Text="COP" Visible="false"></asp:Label>&nbsp;
                                        <asp:Label ID="Label25" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.ValorPesos") %>'></asp:Label>
                                        <asp:Label ID="lblIndiceTabla" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IndiceTablaPrincipal") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoPlan") %>'
                                            Visible="false"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="recordReservaCarro">
                                            <asp:Label ID="lblTRecordExc" runat="server" Text="Récord reserva:"></asp:Label>
                                            <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strReserva") %>'></asp:Label>
                                        </div>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button CssClass="quitarServicio" ID="Button3" runat="server" Text="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="height: 10px;">
                                        <hr />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <!-- Datos Facturacion -->
        <div class="panelResultados" style="display: none;">
            <!--- box border -->
            <div class="plb">
                <div class="prb">
                    <div class="ptb">
                        <div class="ptlc">
                            <div class="ptrc">
                                <div class="contenidoResultados">
                                    <div class="tituloResultados">
                                        <asp:Label ID="Label5" runat="server" Text="Ingrese los datos para la facturación"></asp:Label>
                                    </div>
                                    <div class="contenidoResultadoCarro">
                                        <div style="float: left; position: relative; width: 100%;">
                                            <input type="radio" name="rad1" id="Rad1" onclick="javascript:SetEsconderFacturacion('0');"
                                                checked="checked" /><label for="Rad1">Facturar a mi nombre (nombre del usuario)</label>
                                            <input type="radio" name="rad1" id="Rad2" onclick="javascript:SetEsconderFacturacion('1');" /><label
                                                for="Rad2">Facturar a nombre de otra persona</label>
                                            <br />
                                            <%--<asp:RadioButtonList ID="rblFacturar" runat="server" on AutoPostBack="true">
                        <asp:ListItem Value="Yo" Selected="True" Text=""></asp:ListItem>
                        <asp:ListItem Value="Otro"  Text=""></asp:ListItem>
                    </asp:RadioButtonList>--%>
                                        </div>
                                        <div style="float: left; position: relative; width: 100%; display: none;" id="dvFacturacion"
                                            runat="server">
                                            <table width="90%">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Tipo de Documento"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlTipoIdentificaion" runat="server" Width="85%">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Label4" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDocumento" runat="server" Text="Numero"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Width="90%" ID="txtDocumento" runat="server"></asp:TextBox>
                                                        <asp:Label ID="Label6" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="BtnV" runat="server" Enabled="true" Text="Verificar" CssClass="botonBusqueda"
                                                            OnClick="clcikBtnv" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblnuevo" runat="server" BackColor="ActiveBorder" ForeColor="blue"
                                                            BorderColor="white" Visible="false" Text="Por favor ingrese sus datos:"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblNombre" runat="server" Text="Nombre/Razón Social"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Width="90%" ID="txtnombre" runat="server"></asp:TextBox>
                                                        <asp:Label ID="Label11" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Width="90%" ID="txtapellido" runat="server"></asp:TextBox>
                                                        <asp:Label ID="Label12" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbldireccion" runat="server" Text="Dirección"></asp:Label>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox Width="90%" ID="txtdireccion" runat="server"></asp:TextBox>
                                                        <asp:Label ID="Label13" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Text="Residencia"></asp:ListItem>
                                                            <asp:ListItem Text="Comercial"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lbltelefono" runat="server" Text="Teléfono convencional"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Width="90%" ID="txttelefono" runat="server"></asp:TextBox>
                                                        <asp:Label ID="Label14" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblcelular" runat="server" Text="Celular"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Width="90%" ID="txtcelular" runat="server"></asp:TextBox>
                                                        <asp:Label ID="Label15" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblpais" runat="server" Text="Pais"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPais" runat="server" Width="85%">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="Label18" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblciudad" runat="server" Text="Ciudad"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox Width="90%" ID="txtciudad" runat="server"></asp:TextBox>
                                                        <asp:Label ID="Label21" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblmail" runat="server" Text="E-mail"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:TextBox Width="95%" ID="txtMailPersonal" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ForeColor="#186e9b" ID="Label23" runat="server" Text="Los campos marcados con (*) son obligatorios"></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="text-align: right;">
                                                        <asp:TextBox Width="90%" ID="txtClave" Visible="false" runat="server"></asp:TextBox>
                                                        <asp:TextBox Width="90%" ID="txtclaveConfir" Visible="false" runat="server"></asp:TextBox>
                                                        <ajax:UpdatePanel ID="upCrear" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Button CssClass="botonBusqueda" ID="lbCrear" runat="server" ValidationGroup="Registro"
                                                                    OnClick="lbCrear_Click" Text="Guardar Datos" Enabled="True"></asp:Button>
                                                            </ContentTemplate>
                                                        </ajax:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="alineacionCentro" colspan="4">
                                                        <ajax:UpdatePanel ID="upError" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Label ID="lblError1" runat="server" ForeColor="Maroon"></asp:Label>
                                                            </ContentTemplate>
                                                        </ajax:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="pieResultados">
            </div>
        </div>
        <!-- Formas de Pago -->
        <div class="confirmarVuelo">
            <div class="resumenReserva" id="divFormaspago" runat="server">
                <div class="formaPagoTitulo">
                    ¿CÓMO DESEAS PAGAR?</div>
                <%-- <div style="margin-left:20px; color:#5B99D1;" >
        <asp:Label ID="Label34" Font-Bold="true" runat="server" Text="Una  vez elegida una de las cuatro formas de pago, oprima el botón “Finalizar/pagar“"></asp:Label><br />
        </div>--%>
                <ajax:UpdatePanel ID="upFormasPago" runat="server">
                    <ContentTemplate>
                        <div class="tipoPago">
                            <asp:RadioButtonList ID="rblFormasPago" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblFormasPago_SelectedIndexChanged">
                                <asp:ListItem Selected="true" Text="Tarjeta de crédito"></asp:ListItem>
                                <asp:ListItem Text="Débito a cuentas bancarias"></asp:ListItem>
                                <asp:ListItem Text="Efectivo/pago en oficina"></asp:ListItem>
                                <%--<asp:ListItem Enabled="false"  Text="Que me contacte un asesor"></asp:ListItem>--%>
                            </asp:RadioButtonList>
                        </div>
                        <!-- Tarjeta de Credito -->
                        <div class="tipoTarjeta" id="DivTC" runat="server">
                            <asp:RadioButtonList ID="rblFranquicias" runat="server">
                                <asp:ListItem Selected="false" Text="Visa"></asp:ListItem>
                                <asp:ListItem Text="Diners"></asp:ListItem>
                                <asp:ListItem Text="American Express"></asp:ListItem>
                                <asp:ListItem Text="Master Card"> </asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <%--  <div style="float: right; position: relative;">
                        <img alt="" src="../App_Themes/Imagenes/pago.png" />
                    </div>--%>
                        <!-- Otros -->
                        <div style="float: left; position: relative; width: 480px; padding: 0 25px; height: 130px;
                            overflow: auto;" id="DivOtros" runat="server">
                            <asp:Label ID="lblTextoFormaPago" runat="server" Text=""></asp:Label>
                        </div>
                        <div style="float: left; position: relative; width: 400px; padding: 0 25px;">
                            <div id="Divpolizas">
                                <table width="90%">
                                    <asp:Repeater runat="server" ID="rptPolizasCarrito" Visible="false">
                                        <HeaderTemplate>
                                            <tr>
                                                <th style="width: 30%">
                                                </th>
                                                <th style="text-align: center; width: 30%">
                                                    Disponible
                                                </th>
                                                <th style="text-align: center; width: 40%">
                                                    Valor a Aplicar
                                                </th>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    Poliza:
                                                    <%#DataBinder.Eval(Container.DataItem, "polizaAlpha")%>
                                                </td>
                                                <td style="text-align: center">
                                                    <%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "valorDisponible")).ToString("C0")%>
                                                </td>
                                                <td style="text-align: center">
                                                    <asp:TextBox runat="server" ID="txtValorAplicar" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                            <table width="90%" runat="server" id="tblValoresTotales" visible="false">
                                <tr>
                                    <td style="width: 30%">
                                        <asp:Label CssClass="bold" ID="Label1" runat="server" Text="Total Polizas"></asp:Label>
                                    </td>
                                    <td style="text-align: center; width: 30%">
                                        <asp:Label runat="server" ID="lblValorTotalDisponible"></asp:Label>
                                        <asp:HiddenField runat="server" ID="HiddenValorTotalDisponible" />
                                    </td>
                                    <td style="text-align: center; width: 40%">
                                        <asp:Label runat="server" ID="lblValorTotalAplicar"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label CssClass="bold" ID="Label3" runat="server" Text="Saldo"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="text-align: center;">
                                        <asp:Label runat="server" ID="lblSaldoTotal"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:Label ForeColor="#186e9b" CssClass="bold" ID="lblMensaje" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
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
                    </ContentTemplate>
                </ajax:UpdatePanel>
                <div class="formaPagoBtn">
                    <ajax:UpdatePanel ID="uoBotonesPago" runat="server">
                        <ContentTemplate>
                            <asp:Button CssClass="botonPagar" ID="btnPagar" runat="server" Text="" OnClick="btnPagar_Click" />
                            <asp:Button CssClass="botonGuardar" ID="btnGuardar" runat="server" Text=""
                                OnClick="btnPagar_Click" Visible="false" /><br />
                            <asp:Button ID="lbtnAgregar" Visible="false" runat="server" CssClass="botonCarro"
                                OnClick="lbtnAgregar_Click" Text="Seguir comprando"></asp:Button>
                            <asp:Button ID="lbtnCancelar" runat="server" CssClass="botonCancelar" OnClick="lbtnCancelar_Click"
                                Text="Anular las reservas generadas"></asp:Button>
                        </ContentTemplate>
                    </ajax:UpdatePanel>
                </div>
            </div>
        </div>
        <%--<div class="panelResultados">
    <div class="contenidoResultadoCarro" style="text-align:center;">
        
    </div>
</div>--%>
        <!-- Seguir Comprando -->
        <div class="panelResultados" style="display: none">
            <div class="tituloResultadosAdicionar">
                <asp:Label ID="Label24" runat="server" Text="ADICIONAR MAS SERVICIOS &raquo;"></asp:Label>
            </div>
            <div class="contenidoResultadoCarro" style="text-align: center;">
                <asp:Button CssClass="botonVuelo" ID="btnBusquedaVuelos" runat="server" Text="" />
                <asp:Button CssClass="botonHotel" ID="btnBusquedaHotel" runat="server" Text="" />
                <asp:Button CssClass="botonAuto" ID="btnBusquedaAutos" runat="server" Text="" />
                <asp:Button CssClass="botonPaquete" ID="btnBusquedaPlanes" runat="server" Text="" />
            </div>
        </div>
        <!-- Opciones -->
        <table align="center" width="100%">
            <tr>
                <td align="center" class="tituloCarrito">
                    <ajax:UpdatePanel runat="server" ID="upLblError">
                        <ContentTemplate>
                            <span style="color: #F90;">
                                <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                            </span>
                        </ContentTemplate>
                    </ajax:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="strRecord" runat="server" />
        <asp:HiddenField ID="sAerolinea" runat="server" />
        <asp:HiddenField ID="iTotalBase" runat="server" />
        <asp:HiddenField ID="iTotalTarifa" runat="server" />
        <asp:HiddenField ID="iTotalIVA_Tarifa" runat="server" />
        <asp:HiddenField ID="iTotalImpuestos" runat="server" />
        <asp:HiddenField ID="iTotalImpuestoGasolina" runat="server" />
        <asp:HiddenField ID="iTotaBaselTA" runat="server" />
        <asp:HiddenField ID="iTotalIVA_TA" runat="server" />
        <asp:HiddenField ID="sRuta" runat="server" />
        <asp:HiddenField ID="sFecha" runat="server" />
        <asp:HiddenField ID="TotalCarritoSinFormato" runat="server" />
        <asp:HiddenField ID="bCreditoDispersion" runat="server" Value="False" />
        <uc6:ucPopoupMensaje ID="UcPopoupMensaje1" runat="server"></uc6:ucPopoupMensaje>
    </div>
    <div class="panelCompletoCorporativo1Pie">
    </div>
</div>
