<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucReservaVuelos.ascx.cs"
    Inherits="uc_ucReservaVuelos" %>
<%@ Register Src="ucRegistro.ascx" TagName="ucRegistro" TagPrefix="uc1" %>
<div class="panelTabs">
    <div class="contTabs">
        <div class="tabExterior">
            <div class="tabInterior">
                Paso 1: Confirma tu reserva
            </div>
        </div>
        <div class="tabExterior">
            <div class="tabSegundo">
                Paso 2: Escoje tu forma de pago</div>
        </div>
    </div>
    <div class="panelCompletoCorporativo">
        <div class="reservaDetalle">
            <!--- datos del pasajero -->
            <div class="reservaTitulo">
                1.
                <asp:Label ID="Label4" runat="server" Text=" DATOS DE CONTACTO &raquo;"></asp:Label>
            </div>
            <div class="reservaRegistro">
                <uc1:ucRegistro ID="ucRegistro" runat="server" />
            </div>
            <div class="reservaTitulo">
                2.
                <asp:Label ID="Label6" runat="server" Text=" DATOS DEL PASAJERO &raquo;"></asp:Label>
            </div>
            <div class="reservaDatosPasajero">
                <asp:DataList ID="dtlPasajeros" runat="server" Width="100%">
                    <ItemTemplate>
                        <table width="674" border="0" cellspacing="0" cellpadding="5">
                            <tr class="txt12azul">
                                <td>
                                    <asp:Label ID="lblTTipoPasajero" runat="server" Text="Pasajero"></asp:Label>
                                </td>
                                <td style="display: none">
                                    <asp:Label ID="lblTTrato" runat="server" Text="Trato"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTPrimeroNombre" runat="server" Text="Nombres (*)"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTPrimerApellido" runat="server" Text="Apellidos (*)"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Género (*)"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Text="Tipo documento"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="No.documento  (*)"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label12" runat="server" Text="Fecha nacimiento"></asp:Label>
                                </td>
                            </tr>
                            <td>
                                <asp:TextBox Width="49px" ID="txtTipoPasajero1" Text='<%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%>'
                                    runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td style="display: none">
                                >
                                <div>
                                    <asp:DropDownList Width="52px" ID="ddlTrato1" runat="server">
                                        <asp:ListItem Text="MR"></asp:ListItem>
                                        <asp:ListItem Text="MRS"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre1" Width="90px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtApellido1" Width="90px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <div class="my-skinnable-select">
                                    <asp:DropDownList Width="99px" ID="ddlGenero" runat="server">                                      
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div class="my-skinnable-select">
                                    <asp:DropDownList Width="52px" ID="ddlTipoDoc" runat="server">                                       
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocumento1" Width="71px" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEdad1" Width="76px" runat="server"></asp:TextBox>
                                <asp:Label ID="lblErrorFecha" ForeColor="#186e9b" runat="server" Text=""></asp:Label>
                            </td>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                <span style="font-size: 13px; font-weight: bold; color: #186e9b">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </span>
            </div>
            <asp:Repeater ID="rptItinerario" runat="server">
                <ItemTemplate>
                    <%--Aqui debe ir el cuadrito de la derecha chikito--%>
                    <div class="reservaTarifas">
                        <p>
                            <asp:Label CssClass="tituloValor" ID="lblTValorTotal" runat="server" Text="Valor total de viaje"></asp:Label>
                            <br />
                            <asp:Label CssClass="reservaTarifasPrecio" ID="Label52" runat="server" Text=""><%#DataBinder.Eval(Container.DataItem, "str_Tipo_Moneda")%>&nbsp;
                    <%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalPesos")).ToString("###,###.##")%></asp:Label>
                            <br />
                            <span style="font-size: 10px;">Impuestos incluidos </span>
                            <br />
                            <asp:Label ID="tituloImpuestoVuelo" CssClass="reservaTarifasNaranja" runat="server"
                                Text='<%# DataBinder.Eval(Container.DataItem, "strTextoDG")%>'></asp:Label>
                        </p>
                        <div class="detalleTarifa" style="padding: 5px">
                            <asp:Repeater  runat="server" ID="RptTiposPasajeros">
                                <ItemTemplate>
                                    <asp:Repeater ID="RptTarifas" runat="server">
                                        <ItemTemplate>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr class="tituloTarifa">
                                                    <td>
                                                        Valor total por
                                                        <asp:Label ID="Label55" runat="server"><%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
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
                                                            <div class="GloboImp" style="padding: 0px 0 0 10px;">
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
                        <asp:CheckBox CssClass="bold" Font-Size="X-Small" ID="cbAcepto" runat="server" ForeColor="#ff9900"
                            Text="Confirmo datos de los pasajeros y acepto los términos y condiciones de reserva y tarifas" />
                        <!--detalletarifa-->
                        <asp:Button ID="btnCondiciones" Visible="false" runat="server" CssClass="botonCondiciones"
                            Text="Condiciones generales"></asp:Button>
                        <br />
                        <asp:Button ID="btnConfirmar" runat="server" CssClass="btnSiguiente" ToolTip="Debe aceptar los términos y condiciones para continuar"
                            EnableTheming="True" OnClientClick="popUp();" ValidationGroup="grupoValidacion"
                            CommandName="Confirmar" OnCommand="btnReserva_Command" Text="Siguiente"></asp:Button>
                        <br />
                        <asp:Button ID="btnCancelar" runat="server" CssClass="btnCancelar" Text="Cancelar solicitud / Regresar "
                            CommandName="Cancelar" OnCommand="btnReserva_Command" />
                        <div class="mensajeError" id="dPanel" runat="server">
                        </div>
                    </div>
                    <%--Fin de Aqui debe ir el cuadrito de la derecha chikito--%>
                    <div class="reservaTitulo">
                        3.
                        <asp:Label ID="Label7" runat="server" Text=" RESUMEN ITINERARIO &raquo;"></asp:Label></div>
                    <div class="reservaResumen">
                        <div class="detalleVuelo">
                            <asp:Repeater ID="rptDetalle" runat="server">
                                <ItemTemplate>
                                    <table width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos">
                                        <tr>
                                            <td class="logoAerolinea">
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                            </td>
                                            <td>
                                                <asp:Label CssClass="bold" ID="Label25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="Label30" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label CssClass="bold" ID="Label27" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%>'></asp:Label>
                                                <br />
                                                <asp:Label ID="Label35" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                            </td>
                                            <td class="alineacionCentro">
                                                <asp:Label CssClass="bold" ID="Label2" runat="server" Text="Paradas"></asp:Label><br />
                                                <asp:Label CssClass='<%# DataBinder.Eval(Container.DataItem, "strEstiloParada")%>'
                                                    ID="Label36" ToolTip='<%# DataBinder.Eval(Container.DataItem, "strDescripcionParadas")%>'
                                                    runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strParadas")%>'></asp:Label>
                                            </td>
                                            <td class="alineacionCentro">
                                                <asp:Label CssClass="bold" ID="Label5" runat="server" Text="Tiempo de vuelo"></asp:Label><br />
                                                <asp:Label ID="Label37" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ElapsedTime")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="datosAerolinea">
                                            <td>
                                            </td>
                                            <td colspan="4">
                                                Aerolinea:
                                                <asp:Label CssClass="bold" ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>'></asp:Label>
                                                &nbsp;|&nbsp; Vuelo:
                                                <asp:Label CssClass="bold" ID="Label78" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%>'></asp:Label>
                                                <br />
                                                Hora de salida:
                                                <asp:Label CssClass="bold" ID="Label28" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%>'></asp:Label>
                                                &nbsp;|&nbsp; Hora de llegada:
                                                <asp:Label CssClass="bold" ID="Label32" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td colspan="4" class="division">
                                                <h4 class="trigger">
                                                    <a href="javascript:;">Detalles del Vuelo </a>
                                                </h4>
                                                <div class="toggle_container">
                                                    Aeropuerto salida:
                                                    <asp:Label ID="Label31" CssClass="bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Salida")%>'></asp:Label>
                                                    <strong>de</strong>
                                                    <asp:Label ID="Label29" runat="server" CssClass="bold" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>
                                                    <br />
                                                    Aeropuerto llegada:
                                                    <asp:Label CssClass="bold" ID="Label34" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strAeropuerto_Llegada")%>'></asp:Label>
                                                    <strong>de</strong>
                                                    <asp:Label ID="Label33" CssClass="bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Llegada")%>'></asp:Label>
                                                    <br />
                                                    Tipo de avión:
                                                    <asp:Label CssClass="bold" ID="Label16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strEquipment")%>'></asp:Label>
                                                    &nbsp;|&nbsp; Clase:
                                                    <asp:Label CssClass="bold" ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strClase")%>'></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="reservaResumen">
                        <div class="tituloCondiciones">
                            VER CONDICIONES DE RESERVA Y TARIFA
                        </div>
                        <div class="detalleVuelo3">
                            <div class="condiciones">
                                <asp:Label runat="server" ID="lblTPlazoLimite" Text="Plazo limite de emisión"></asp:Label><br />
                                <asp:Label runat="server" ID="lblFechaLimiteTiqueteo" ForeColor="#186e9b" Text=""></asp:Label>
                                <asp:Label ID="lblCondiciones" runat="server"></asp:Label>
                                <asp:Label ID="lblcondicionesEspecificas" runat="server"></asp:Label>
                                <asp:Label ID="lblHoraCondicion" runat="server"></asp:Label>
                                <br />
                                <ajax:UpdatePanel ID="pnlUpdate" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="hlkSabre" runat="server" Text="Ver más" OnClick="hlkSabre_Click"></asp:LinkButton>
                                        <asp:Label ID="lblPnl" runat="server" Visible="false" Text=""></asp:Label>
                                        <ajax:CollapsiblePanelExtender ID="pnlExtendere" runat="server" AutoCollapse="false"
                                            AutoExpand="true" CollapsedText="Ocultar" ExpandedText="Ver mas" TextLabelID="lblPnl"
                                            CollapsedSize="0" CollapseControlID="hlkSabre" ExpandControlID="hlkSabre" ExpandedSize="50"
                                            TargetControlID="pnlSabre">
                                        </ajax:CollapsiblePanelExtender>
                                        <asp:Panel ID="pnlSabre" runat="server" Width="100%" Height="20%">
                                            <asp:Label ID="lblCondicionesSabre" runat="server" Font-Bold="true"></asp:Label>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajax:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="panelCompletoCorporativo1Pie">
    </div>
</div>
<ajax:ModalPopupExtender ID="MPEEReserva" BackgroundCssClass="ui-widget-shadow" runat="server"
    TargetControlID="dummyLink4" BehaviorID="MPEEReserva" OnOkScript="" OkControlID="btnCerrar4"
    PopupControlID="Panel1" />
<asp:Panel runat="server" ID="Panel1">
    <div style="display: none;">
        <asp:Button ID="btnCerrar4" CssClass="btnCerrar" runat="server" />
    </div>
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Imagenes/cortinillaGeneral.gif" />
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink4"
    runat="server">dummy</a> 