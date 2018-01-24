<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucReservaVuelosTarjeta2.ascx.cs"
    Inherits="uc_ucReservaVuelosTarjeta2" %>
<%@ Register Src="ucRegistro.ascx" TagName="ucRegistro" TagPrefix="uc1" %>
<div class="panelTabs">
    <div class="panelCompletoCorporativo">
        <div class="panelIzquierdoReservaVuelos">
            <div class="franjaAmarillaInicio">
            </div>
            <div class="reservaTituloVuelosTarjeta">
                <asp:Label ID="Label6" runat="server" Text="INFORMACIÓN DE PASAJERO"></asp:Label>
            </div>
            <div class="reservaDatosPasajero">
                <asp:DataList ID="dtlPasajeros" runat="server" Width="100%">
                    <ItemTemplate>
                        <div class="renglonDatosVuelosTarjeta">
                            <div class="tipoPasajeroVuelosTarjeta">
                                <asp:TextBox ID="txtTipoPasajero1" Text='<%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%>'
                                    runat="server" ReadOnly="True" CssClass="labelTipoPasajeroVuelosTarjeta"></asp:TextBox>
                            </div>
                            <div class="datosPasajeroVuelosTarjeta">
                                <div class="lineaDatosVuelosTarjeta">
                                    <div class="celdaDatosVuelosTarjeta">
                                        <asp:Label ID="lblTPrimeroNombre" CssClass="labelDatosVuelosTarjeta" runat="server"
                                            Text="Nombres (*)"></asp:Label>
                                        <asp:TextBox ID="txtNombre1" Width="90%" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="celdaDatosVuelosTarjeta">
                                        <asp:Label ID="lblTPrimerApellido" CssClass="labelDatosVuelosTarjeta" runat="server"
                                            Text="Apellidos (*)"></asp:Label>
                                        <asp:TextBox ID="txtApellido1" Width="90%" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="celdaDatosVuelosTarjeta">
                                        <asp:Label ID="Label12" CssClass="labelDatosVuelosTarjeta" runat="server" Text="Fecha nacimiento"></asp:Label>
                                        <asp:TextBox ID="txtEdad1" Width="80%" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblErrorFecha" ForeColor="#186e9b" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="lineaDatosVuelosTarjeta">
                                    <div class="celdaDatosVuelosTarjeta">
                                        <asp:Label ID="Label8" CssClass="labelDatosVuelosTarjeta" runat="server" Text="Género (*)"></asp:Label>
                                        <div class="my-skinnable-select" style="width: 90%; margin-top: 5px; height: 26px;
                                            border: solid 1px #abadb3;">
                                            <asp:DropDownList Width="90%" ID="ddlGenero" runat="server">
                                                <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                                                <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="celdaDatosVuelosTarjeta">
                                        <asp:Label ID="Label9" CssClass="labelDatosVuelosTarjeta" runat="server" Text="Tipo documento"></asp:Label>
                                        <div class="my-skinnable-select" style="margin-top: 5px; height: 26px; border: solid 1px #abadb3;">
                                            <asp:DropDownList Width="90%" ID="ddlTipoDoc" runat="server">
                                                <asp:ListItem Text="C.C." Value="CC"></asp:ListItem>
                                                <asp:ListItem Text="C.E." Value="CE"></asp:ListItem>
                                                <asp:ListItem Text="T.I." Value="TI"></asp:ListItem>
                                                <asp:ListItem Text="Pasaporte" Value="PS"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="celdaDatosVuelosTarjeta">
                                        <asp:Label ID="Label10" CssClass="labelDatosVuelosTarjeta" runat="server" Text="No.documento (*)"></asp:Label>
                                        <asp:TextBox ID="txtDocumento1" Width="80%" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table width="674" border="0" cellspacing="0" cellpadding="5">
                            <tr class="txt12azul">
                                <td style="display: none">
                                    <asp:Label ID="lblTTrato" runat="server" Text="Trato"></asp:Label>
                                </td>
                            </tr>
                            <td>
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
                        </table>
                    </ItemTemplate>
                </asp:DataList>
                <span style="font-size: 13px; font-weight: bold; color: #186e9b">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </span>
            </div>
            <div class="divisorAzul580">
            </div>
            <!--- datos del pasajero -->
            <div class="reservaTituloVuelosTarjeta">
                <asp:Label ID="Label4" runat="server" Text=" DATOS DE CONTACTO"></asp:Label>
            </div>
            <div class="reservaRegistroVuelosTarjeta">
                <uc1:ucRegistro ID="ucRegistro" runat="server" />
            </div>
            <div class="divisorAzul580">
            </div>
            <div class="reservaDetalle">
                <!-- Formas de Pago -->
                <div class="resumenReserva" id="divFormaspago" runat="server">
                    <div class="reservaTituloVuelosTarjeta">
                        FORMA DE PAGO
                    </div>
                    <ajax:UpdatePanel ID="upFormasPago" runat="server">
                        <ContentTemplate>
                            <div class="tipoPago">
                                <asp:RadioButtonList ID="rblFormasPago" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblFormasPago_SelectedIndexChanged">
                                    <asp:ListItem Selected="true" Text="Tarjeta de crédito"></asp:ListItem>
                                    <asp:ListItem Text="Débito a cuentas bancarias"></asp:ListItem>
                                    <asp:ListItem Text="Efectivo/pago en oficina"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <!-- Tarjeta de Credito -->
                            <div class="tipoTarjeta" id="DivTC" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="Tarjeta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="Número de tarjeta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="Banco Emisor"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlFranquicia" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumTarjeta" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBanco" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="Vence"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="Código de seguridad"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Número de cuotas"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlMesVencimiento" runat="server">
                                                <asp:ListItem Text="Enero" Value="Enero"></asp:ListItem>
                                                <asp:ListItem Text="Febrero" Value="Febrero"></asp:ListItem>
                                                <asp:ListItem Text="Marzo" Value="Marzo"></asp:ListItem>
                                                <asp:ListItem Text="Abril" Value="Abril"></asp:ListItem>
                                                <asp:ListItem Text="Mayo" Value="Mayo"></asp:ListItem>
                                                <asp:ListItem Text="Junio" Value="Junio"></asp:ListItem>
                                                <asp:ListItem Text="Julio" Value="Julio"></asp:ListItem>
                                                <asp:ListItem Text="Agosto" Value="Agosto"></asp:ListItem>
                                                <asp:ListItem Text="Septiembre" Value="Septiembre"></asp:ListItem>
                                                <asp:ListItem Text="Octubre" Value="Octubre"></asp:ListItem>
                                                <asp:ListItem Text="Noviembre" Value="Noviembre"></asp:ListItem>
                                                <asp:ListItem Text="Diciembre" Value="Diciembre"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;/&nbsp;<asp:TextBox ID="txtAnioVencimiento" runat="server" Width="50"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodSeguridad" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCuotas" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text="Titular de la tarjeta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text="Cédula del titular de la tarjeta"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTitular" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIdentificacion" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label23" runat="server" Text="Dirección"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text="Ciudad / país"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPais" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" Text="Teléfono horas laborales"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label38" runat="server" Text="Otro teléfono"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoOficina" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoOtro" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- Otros -->
                            <div style="float: left; position: relative; width: 480px; padding: 0 25px; height: 130px;
                                overflow: auto;" id="DivOtros" runat="server">
                                <asp:Label ID="lblTextoFormaPago" runat="server" Text=""></asp:Label>
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
                </div>
            </div>
        </div>
        <!-- Fin panelIzquierdoReservaVuelos -->
        <div class="panelDerechoReservaVuelos">
            <div class="franjaAmarillaInicio">
            </div>
            <asp:Repeater ID="rptItinerarioBFM" runat="server" OnItemCommand="rptItinerario_ItemCommand"
                EnableViewState="true">
                <ItemTemplate>
                    <div class="vuelos">
                        <div class="detalleVuelo">
                            <asp:Repeater runat="server" ID="RptSegmentosIda" EnableViewState="true">
                                <ItemTemplate>
                                    <table id="tblIda" runat="server" width="100%" cellpadding="2" cellspacing="1" class="tablaVuelos"
                                        style="font-size: 11px">
                                        <tr style="position: relative">
                                            <td>
                                                <asp:Image ID="ImgIda" runat="server" Visible="false" />
                                            </td>
                                            <td>
                                                <asp:Label CssClass="bold" ID="lblIda" runat="server" Text="IDA" Visible="false"></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ID="lblCiudadSalida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ForeColor="Blue" ID="lblCiudadSalidaCod" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "strDepartureAirport")%>'></asp:Label>
                                                <asp:Label CssClass="bold" ID="lblOdeId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "OriginDestinationOption_Id")%>'></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Image ID="iNext" runat="server" Visible="false" />
                                                <asp:Label CssClass="bold" ID="lblCiudadLlegada" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%>'></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ForeColor="Blue" ID="lblCiudadLlegadaCod" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "strArrivalAirport")%>'></asp:Label>
                                            </td>
                                            <td align="top">
                                                <asp:Label ID="lblFechaSalida" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton runat="server" GroupName="Sel" ID="rbtnSel" />
                                            </td>
                                            <td valign="top" style="padding-top: 4px">
                                                <asp:Label CssClass="bold" ID="lblNameAir" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>'></asp:Label>
                                                <asp:Label CssClass="bold" ID="lblMarketingAirline" runat="server" Visible="false"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "strMarketingAirline")%>'></asp:Label>
                                                salida:
                                                <asp:Label CssClass="bold" ID="lblHourDeparture" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%>'></asp:Label>
                                                <asp:Label CssClass="bold" ID="lblHourTotal" runat="server" Visible="false" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida"))%>'></asp:Label>
                                                &nbsp;|&nbsp; llegada:
                                                <asp:Label CssClass="bold" ID="lblHourArrival" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%>'></asp:Label>
                                            </td>
                                            <td style="width: 69px;">
                                                <asp:Label ID="lblTimeFly" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ElapsedTime")%>'></asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <ajax:UpdatePanel ID="upModalIda" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lblHederIda" runat="server" Text='<%# Eval("strParadas") %>'
                                                            CommandArgument='<%# ConcatColumns(Eval("FlightNumber"),Eval("OriginDestinationOption_Id"),"I")%>'
                                                            OnCommand="lnkCustDetails_Click"></asp:LinkButton>
                                                    </ContentTemplate>
                                                </ajax:UpdatePanel>
                                            </td>
                                            <td style="width: 60px; float: left;">
                                                <asp:Image ID="ImgAir" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                            </td>
                                        </tr>
                                        <tr id="trIda" runat="server" visible="false">
                                            <td colspan="5">
                                                <hr />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label CssClass="bold" ID="lblFly" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Repeater runat="server" ID="RptSegmentosReg" OnItemDataBound="RptSegmentosReg_ItemDataBound"
                                EnableViewState="true">
                                <ItemTemplate>
                                    <table id="tblVuelta" runat="server" width="100%" cellpadding="2" cellspacing="1"
                                        class="tablaVuelos" style="font-size: 11px">
                                        <tr style="position: relative">
                                            <td>
                                                <asp:Image ID="ImgVuelta" runat="server" Visible="false" />
                                            </td>
                                            <td>
                                                <asp:Label CssClass="bold" ID="lblVuelta" runat="server" Text="Vuelta" Visible="false"></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ID="lblCiudadSalida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ForeColor="Blue" ID="lblCiudadSalidaCod" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "strDepartureAirport")%>'></asp:Label>
                                                <asp:Label CssClass="bold" ID="lblOdeId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "OriginDestinationOption_Id")%>'></asp:Label>
                                            </td>
                                            <td colspan="2">
                                                <asp:Image ID="iNext" runat="server" Visible="false" />
                                                <asp:Label CssClass="bold" ID="lblCiudadLlegada" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%>'></asp:Label>&nbsp
                                                <asp:Label CssClass="bold" ForeColor="Blue" ID="lblCiudadLlegadaCod" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "strArrivalAirport")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaSalida" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton runat="server" GroupName="Sel" ID="rbtnSel" />
                                            </td>
                                            <td>
                                                salida:
                                                <asp:Label CssClass="bold" ID="lblHourDeparture" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%>'></asp:Label>
                                                <asp:Label CssClass="bold" ID="lblHourTotal" runat="server" Visible="false" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida"))%>'></asp:Label>
                                                &nbsp;|&nbsp; llegada:
                                                <asp:Label CssClass="bold" ID="lblHourArrival" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%>'></asp:Label>
                                            </td>
                                            <td style="width: 69px;">
                                                <asp:Label ID="lblTimeFly" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ElapsedTime")%>'></asp:Label>&nbsp;
                                            </td>
                                            <td>
                                                <ajax:UpdatePanel ID="upModal" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="lblHeder" runat="server" Text='<%# Eval("strParadas") %>' CommandArgument='<%# ConcatColumns(Eval("FlightNumber"),Eval("OriginDestinationOption_Id"),"R")%>'
                                                            OnCommand="lnkCustDetails_Click"></asp:LinkButton>
                                                    </ContentTemplate>
                                                </ajax:UpdatePanel>
                                            </td>
                                            <td style="width: 60px; float: left;">
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Label CssClass="bold" ID="lblFly" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="tarifasVuelo">
                            <!-- Precios -->
                            <div class="valorTotalVuelo">
                                <asp:Label CssClass="tituloValor" ID="lblTValorTotal" runat="server" Text="Valor total a pagar"></asp:Label>
                                <br />
                                <span style="font-size: 10px;">Todos los impuestos incluidos </span>
                                <br />
                                <asp:Label CssClass="tituloValor" ID="lblTotalOriginal" Visible="false" runat="server"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "IntTotalPesos")%>'></asp:Label>
                                <asp:Label CssClass="tituloValor" ID="Label52" runat="server" Text=""><%--<%#DataBinder.Eval(Container.DataItem, "str_Tipo_Moneda")%>--%>
                        &nbsp;<%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalPesos")).ToString("###,###.##")%></asp:Label><br />
                                <asp:Label CssClass="tituloValor" Visible="False" ID="lblAerolinea" runat="server"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "strMarketingAirline")%>'></asp:Label>
                            </div>
                            <!-- Pasajeros -->
                            <div class="detalleTarifa">
                                <asp:Repeater runat="server" ID="RptTiposPasajeros">
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
                                                                    <asp:DataList runat="server" ID="RptImpuestos">
                                                                        <ItemTemplate>
                                                                            <span class="labelDetalleImpuestos">
                                                                                <%# DataBinder.Eval(Container.DataItem, "strNombre_Impuesto")%>
                                                                            </span><span class="valorlDetalleImpuestos">
                                                                                <%# DataBinder.Eval(Container.DataItem, "CurrencyCode")%>&nbsp;&nbsp;
                                                                                <%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "Amount")).ToString("C")%>
                                                                            </span>
                                                                        </ItemTemplate>
                                                                    </asp:DataList>
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
                            <div class="valorTotalVuelo">
                                <asp:Button ID="btnSeleccionar" CssClass="botonBuscador" OnClientClick="Show_Cortinilla_Interna();"
                                    runat="server" Text="Seleccionar"></asp:Button>
                                <br />
                                <asp:Label ID="tituloImpuestoVuelo" ForeColor="red" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strTextoDG")%>'></asp:Label>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <!-- Fin panelDerechoReservaVuelos -->
    </div>
    <div class="panelCompletoCorporativo1Pie">
    </div>
</div>
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
<%--Fin Gabo--%>
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