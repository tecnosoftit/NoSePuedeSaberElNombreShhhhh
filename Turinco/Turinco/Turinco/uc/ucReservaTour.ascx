<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucReservaTour.ascx.cs"
    Inherits="uc_ucReservaTour" %>
<%@ Register Src="~/uc/ucRegistro.ascx" TagName="ucRegistro" TagPrefix="uc2" %>
<div class="panelTabs">
    <div class="panelCompletoCorporativo" style="padding-bottom: 20px; padding-top: 20px;">
        <div class="panelIzquierdoReservaVuelos">
            <div class="franjaAmarillaInicio">
            </div>
            <div class="reservaTituloVuelosTarjeta">
                <asp:Label ID="Label4" runat="server" Text="DATOS DEL AGENTE QUE RESERVA"></asp:Label>
            </div>
            <div class="reservaRegistroVuelosTarjeta">
                <uc2:ucRegistro ID="ucRegistro" runat="server" />
            </div>
            <div class="divisorAzul580">
            </div>
            <div class="reservaTituloVuelosTarjeta">
                <asp:Label ID="Label7" runat="server" Text="Información de los Viajeros"></asp:Label>
            </div>
            <div class="reservaRegistroVuelosTarjeta" style="font-size: 12px;">
                <table>
                    <asp:Repeater ID="rptCabinas" runat="server">
                        <ItemTemplate>
                            <tr style="display: none">
                                <td>
                                    <asp:Label CssClass="bold" ID="Label33" runat="server" Text="Idioma"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="ddlIdioma" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    <asp:Label ID="lblConsecRes" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intConsecRes") %>'></asp:Label>
                                    <asp:Label ID="lblSegmento" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intSegmento") %>'></asp:Label>
                                    <asp:Label CssClass="bold" ID="Label56" runat="server" Text="Hora(*)"></asp:Label>
                                    <asp:DropDownList ID="ddlHora" runat="server">
                                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                        <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                        <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                        <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                        <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                        <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                        <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                        <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                        <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                        <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox Width="50%" ID="txtHoraServicio" runat="server" Text="" Visible="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label CssClass="bold" ID="Label20" runat="server" Text="Minutos"></asp:Label>
                                    <asp:DropDownList ID="ddlMinuto" runat="server">
                                        <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="40" Value="40"></asp:ListItem>
                                        <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                            <td colspan="2">
                                    <asp:Label CssClass="bold" ID="Label2" runat="server" Text="Cantidad de viajeros:"></asp:Label>
                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strCantidadPersonas") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    <asp:Label CssClass="bold" ID="Label5" runat="server" Text="Lugar donde inicia el servicio(*)"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="rblLugarInicio" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rblLugar_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Aeropuerto" Value="Aeropuerto"></asp:ListItem>
                                        <asp:ListItem Text="Otro" Value="Otro"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <asp:Panel ID="pDatosVuelo" runat="server" Visible="false">
                                <tr>
                                    <td colspan="4">
                                        <asp:Label CssClass="bold" ID="Label13" runat="server" Text="Datos del vuelo"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label14" runat="server" Text="Aerolinea"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAerolinea" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label15" runat="server" Text="Origen"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOrigen" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label16" runat="server" Text="Hora de salida"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHoraSalida" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label17" runat="server" Text="Numero de vuelo"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVuelo" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label18" runat="server" Text="Destino"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDestino" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label19" runat="server" Text="Hora de llegada"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHoraLlegada" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr style="display: none">
                                <asp:Panel ID="pDirInicio" runat="server" Visible="false">
                                    <td>
                                        <asp:Label CssClass="bold" ID="Label1" runat="server" Text="Dirección"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox Width="80%" ID="txtDirInicio" runat="server" Text=""></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    <asp:Label CssClass="bold" ID="Label3" runat="server" Text="Lugar donde finaliza el servicio(*)"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:RadioButtonList ID="rblLugarFin" runat="server" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rblLugar_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Aeropuerto" Value="Aeropuerto"></asp:ListItem>
                                        <%--<asp:ListItem Text="Puente aéreo" Value="Puente aéreo"></asp:ListItem>--%>
                                        <asp:ListItem Text="Otro" Value="Otro"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <asp:Panel ID="pDatosVueloFin" runat="server" Visible="false">
                                <tr>
                                    <td colspan="4">
                                        <asp:Label CssClass="bold" ID="Label21" runat="server" Text="Datos del vuelo"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label22" runat="server" Text="Aerolinea"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAerolineaFin" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label23" runat="server" Text="Origen"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOrigenFin" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label24" runat="server" Text="Hora de salida"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHoraSalidaFin" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label25" runat="server" Text="Numero de vuelo"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVueloFin" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label26" runat="server" Text="Destino"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDestinoFin" runat="server" Text=""></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label CssClass="bold" ID="Label27" runat="server" Text="Hora de llegada"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHoraLlegadaFin" runat="server" Text=""></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr style="display: none">
                                <asp:Panel ID="pDirFin" runat="server" Visible="false">
                                    <td>
                                        <asp:Label CssClass="bold" ID="Label6" runat="server" Text="Dirección"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox Width="80%" ID="txtDirFin" runat="server" Text=""></asp:TextBox>
                                    </td>
                                </asp:Panel>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblTPorFavor" runat="server" Text="Por favor ingrese la información de el(los) viajero(s). Asegurese que el nombre es el mismo que aparece registrado en el documento de identificación"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Repeater ID="rptPasajeros" runat="server">
                                        <HeaderTemplate>
                                            <table width="100%">
                                                <tr class="tituloTablaCircuito">
                                                    <td>
                                                        <asp:Label ID="lblTTipoPasajero" runat="server" CssClass="bold" Text="Tipo de viajero"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTNombres" runat="server" CssClass="bold" Text="Apellidos y Nombres (*)"></asp:Label>
                                                    </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblTGenero" runat="server" CssClass="bold" Text="Genero"></asp:Label>
                                                    </td>
                                                    <td style="display: none">
                                                        <asp:Label ID="lblTFechaNacimiento" runat="server" CssClass="bold" Text="Fecha de nacimiento"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTMoneda" runat="server" CssClass="bold" Text="Moneda"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTTarifa" runat="server" CssClass="bold" Text="Tarifa"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTValor" runat="server" CssClass="bold" Text="Valor con impuestos"></asp:Label>
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr style="text-align: left">
                                                <td>
                                                    <asp:Label ID="lblTipoPax" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTipoPax") %>'></asp:Label>
                                                </td>
                                                <td style="width: 40%">
                                                    <asp:TextBox ID="txtNombre" Style="width: 85%" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrNombre") %>'></asp:TextBox>
                                                </td>
                                                <td style="display: none">
                                                    <asp:DropDownList ID="ddlGenero" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="display: none">
                                                    <asp:TextBox ID="txtNacimiento" runat="server" Text=""></asp:TextBox><br />
                                                    <asp:Label ID="Label7" runat="server" Text="mm/dd/aaaa" ForeColor="#186e9b"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMoneda" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTipoMoneda") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTarifa" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTarifa") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <a class="valorTotal" href="#this">
                                                        <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strValor") %>'></asp:Label>
                                                        <div>
                                                            <%# DataBinder.Eval(Container, "DataItem.strHtmlImpuestos")%>
                                                        </div>
                                                    </a>
                                                    <asp:Label ID="lblidPax" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intCodigoPax") %>'
                                                        Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td style="display: none">
                                    <asp:Label ID="Label8" runat="server" Text="Notificar reserva a:" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td colspan="4" style="display: none">
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 10%">
                                                <asp:Label CssClass="bold" ID="Label11" runat="server" Text="Nombre" Visible="false"></asp:Label>
                                            </td>
                                            <td style="width: 30%">
                                                <asp:TextBox Width="90%" ID="txtNombreContacto" runat="server" Text="" Visible="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label CssClass="bold" ID="Label9" runat="server" Text="Teléfono" Visible="false"></asp:Label>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox Width="70%" ID="txtTelContacto" runat="server" Text="" Visible="false"></asp:TextBox>
                                            </td>
                                            <td style="width: 10%">
                                                <asp:Label CssClass="bold" ID="Label10" runat="server" Text="Empresa" Visible="false"></asp:Label>
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox Width="90%" ID="txtEmpresaContacto" runat="server" Text="" Visible="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label CssClass="bold" ID="lblTObservaciones" runat="server" Text="Observaciones: "></asp:Label>
                                    <asp:TextBox ID="txtObservaciones" runat="server" Width="500"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: right; color: #186e9b; font-weight: bold;">
                                    <asp:Label CssClass="bold" ID="Label12" runat="server" Text="Los campos marcados con (*) son obligatorios"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div class="divisorAzul580">
            </div>
            <div id="divFormaspago" runat="server" class="resumenReserva">
                <div class="reservaTituloVuelosTarjeta">
                    FORMA DE PAGO
                </div>
                <ajax:UpdatePanel ID="upFormasPago" runat="server">
                    <ContentTemplate>
                        <div class="tipoPagoVuelosTarjeta">
                            <asp:RadioButtonList ID="rblFormasPago" runat="server" Style="width: 550px; float: left;">
                            </asp:RadioButtonList>
                        </div>
                        <!-- Tarjeta de Credito -->
                        <div class="tipoTarjeta" id="DivTC" style="display: none">
                            <img src="../App_Themes/Imagenes/LogosTarjetaCredito.png" style="float: left; margin-left: 15px;
                                margin-bottom: 10px;">
                            <table>
                                <tbody>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblErrorTarjeta" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="Tarjeta(*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label15" runat="server" Text="Número de tarjeta(*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="Banco Emisor(*)"></asp:Label>
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
                                            <asp:Label ID="Label18" runat="server" Text="vence(*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="Código de seguridad(*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Número de cuotas(*)"></asp:Label>
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
                                            &nbsp;/&nbsp;<asp:DropDownList ID="txtAnioVencimiento" runat="server">
                                                <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                                <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                            </asp:DropDownList>
                                            <%--<asp:TextBox ID="txtAnioVencimiento" runat="server" Width="50px"></asp:TextBox>--%>
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
                                            <asp:Label ID="Label21" runat="server" Text="Titular de la Tarjeta(*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text="Cédula del titular de la tarjeta(*)"></asp:Label>
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
                                            <asp:Label ID="Label23" runat="server" Text="Dirección(*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text="Ciudad / País(*)"></asp:Label>
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
                                            <asp:Label ID="Label26" runat="server" Text="Teléfono horas laborales(*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label38" runat="server" Text="Otro teléfono(*)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoOficina" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoOtro" runat="server" CssClass="block full text_in"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <!-- Otros -->
                        <div style="float: left; width: 530px; padding: 25px; position: relative; display: none;"
                            id="DivPSE">
                            <p>
                                Esta opci&oacute;n te permitir&aacute; el pago con d&eacute;bito a una cuenta bancaria
                                (ahorros o corriente), para hacerlo debes tener habilitado en tu cuenta la opci&oacute;n
                                de hacer pagos por internet.</p>
                            </p>
                        </div>
                        <div style="float: left; width: 530px; padding: 25px; position: relative; display: none;"
                            id="DivEfe">
                            <p>
                                &nbsp;
                            </p>
                            <div>
                                <p>
                                    &nbsp;
                                </p>
                                <div>
                                    <p>
                                        Puedes realizar tu pago en la oficina de&nbsp;TuTiquete&nbsp;ubicada en la Calle
                                        27 # 4-49 piso 4. en Bogot&aacute; - Colombia. Tel 2439903 o consignar en las siguientes
                                        cuentas bancarias:
                                    </p>
                                    <strong>
                                        <img src="../imagenes/Planes/bancolombiapng.png" alt="" width="50" height="50" />
                                    </strong>
                                    <br />
                                    <ul>
                                        <li>Cuenta Corriente Tutiquete S.A., No. 690 299479-20.&nbsp; </li>
                                        <li>Usa formato Recaudo Nacional, en la casilla convenio escribe: 900113658-6 y en la
                                            casilla concepto o referencia: tu n&uacute;mero de c&eacute;dula </li>
                                    </ul>
                                    <div>
                                        <strong>
                                            <img src="../imagenes/Planes/daviviendapng.png" alt="" width="50" height="50" />
                                        </strong>
                                    </div>
                                    <div>
                                        <ul>
                                            <li>Cuenta Corriente&nbsp;a nombre de Tutiquete S.A. No. 4574699940-18 </li>
                                        </ul>
                                    </div>
                                    <p style="text-align: justify;">
                                        &nbsp;
                                    </p>
                                    <p>
                                        Una vez realizada la consignaci&oacute;n, env&iacute;ala v&iacute;a fax al&nbsp;No.
                                        751 8095 en Bogota, o por correo electr&oacute;nico a: centralreservas@tutiquete.com</p>
                                    <p>
                                        &nbsp;
                                    </p>
                                    <p>
                                        Por favor ten en cuenta que&nbsp;los&nbsp;tiquete(s) y vouchers u &oacute;rdenes
                                        de servicio se emiten una vez se haya&nbsp; recibido el pago.
                                    </p>
                                </div>
                                <p>
                                    &nbsp;
                                </p>
                            </div>
                            <p>
                                &nbsp;
                            </p>
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
            <ajax:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="mensajeError" id="dPanel" runat="server">
                    </div>
                </ContentTemplate>
            </ajax:UpdatePanel>
            <div class="contenedorCondicionesVuelosTarjeta">
                <img src="../App_Themes/Imagenes/warningCondiciones.jpg" style="width: 40px; float: left;">
                <a href="#this" runat="server" onclick="return false" id="ltrainfo" style="float: left;
                    width: 490px; margin-left: 10px; margin-bottom: 5px; font-size: 14px;">
                    <asp:Label ID="lblTCondiciones" runat="server" Text="Condiciones de la reserva y cancelacón de servicios"
                        CssClass="linkCondiciones"></asp:Label>
                </a>
                <div class="contenedorCheck">
                    <asp:CheckBox ID="cbAcepto" runat="server" Text="Acepto condiciones de reserva y cancelación de servicios" />
                </div>
                <div style="float: left; width: 100%; margin-top: 20px;">
                    <asp:Button ID="btnConfirmar" CssClass="btnSiguienteVuelosTarjeta" runat="server"
                        Text="RESERVAR" OnClientClick="popUp();" OnClick="btnReservar_Click" Enabled="False"
                        Style="float: right;" />
                    <asp:Button ID="btnCancelar" CssClass="btnCancelar " runat="server" Text="<< Buscar más planes"
                        OnClick="btnCancelar_Click" Style="float: left; color: #FF9600; margin-top: 5px;" />
                </div>
                <!-- Condiciones -->
                <ajax:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>
        </div>
        <div class="panelDerechoReservaVuelos">
            <div class="franjaAmarillaInicio">
            </div>
            <div class="reservaTituloVuelosTarjeta">
                INFORMACIÓN DEL PLAN
            </div>
            <div class="reservaResumen300" style="width: 275px; padding: 0 10px 0 15px; font-size: 12px;">
                <asp:Label CssClass="bold" ID="lblConsecRes" runat="server" Text="" Visible="false"></asp:Label>
                <table>
                <asp:Repeater ID="rptCircuitos" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 15%">
                                <asp:Label CssClass="bold" ID="lblTNombrePlan" runat="server" Text="Nombre del plan"></asp:Label>
                            </td>
                            <td class="valorTotalReserva" style="width: 45%">
                                <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strNombrePlan") %>'></asp:Label>
                            </td>
                            <td style="width: 15%">
                                <asp:Label CssClass="bold" ID="lblTCiudad" runat="server" Text="Ciudad" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 25%">
                                <asp:Label ID="Label30" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.StrCiudad") %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label CssClass="bold" ID="lblTDescripcion" runat="server" Text="Descripción"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="Label32" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label CssClass="bold" ID="lblTDuracion" runat="server" Text="Duración"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label52" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDuracion") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label CssClass="bold" ID="lblTCategoria" runat="server" Text="Categoria del hotel"
                                    Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label55" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrCategoria") %>'
                                    Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label CssClass="bold" ID="lblTFechaInicio" runat="server" Text="Fecha servicio"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrFechaInicial") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPosicion" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PosTablaPrincipal") %>'
                                    Visible="False"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </table>
                <div class="clear block" style="text-align: right; margin-top: 20px;">
                    <asp:Label ID="lblTLimitePago" runat="server" Text="PLAZO LÍMITE DE PAGO: "></asp:Label>
                    <asp:Label ID="lblLimitePago" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="divisorAzul300 ">
            </div>
            <div class="reservaResumen300" style="width: 275px; padding: 0 10px 20px 15px; font-size: 12px;">
                <div style="width: 100%; float: left;">
                    <div style="float: left; color: #0D486A">
                        <asp:Label ID="lblTTotal" runat="server" Text="VALOR TOTAL A PAGAR:"></asp:Label>
                    </div>
                    <div style="float: left; color: Blue; margin-left: 10px;">
                        <asp:Label ID="lblMonedaTotal" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoMoneda") %>'></asp:Label>&nbsp;
                        <asp:Label ID="lblTotalCarrito" runat="server" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container,"DataItem.IntValorTotal")).ToString("###,###,###") %>'></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<ajax:ModalPopupExtender ID="mdPCondicionesplanes" runat="server" BackgroundCssClass="ui-widget-shadow"
    TargetControlID="ltrainfo" PopupControlID="updCondicionesplanes" BehaviorID="mdPCondicionesplanes"
    CancelControlID="btnCerrarT" />
<ajax:UpdatePanel ID="updCondicionesplanes" runat="server">
    <ContentTemplate>
        <div>
            <asp:Button ID="btnCerrarT" CssClass="botonCerrarVuelosTarjeta" runat="server" />
        </div>
        <div class="condiciones100" style="padding-top: 30px;">
            <iframe src="../Presentacion/condiciones.aspx?idSession?<%=Request.QueryString["idSesion"] %>&Codigo=<%=Request.QueryString["Codigo"] %>&TipoPlan=<%=Request.QueryString["TipoPlan"] %>"
                style="width: 100%; height: 100px"></iframe>
        </div>
    </ContentTemplate>
</ajax:UpdatePanel>
<!-- Valor Total -->
<asp:Label ID="lblTReservaCrucero" runat="server" Text="BIENVENIDA &raquo;" Style="display: none;"></asp:Label>
<div style="float: left; position: relative; font-size: 16px; font-weight: bold;
    color: #186e9b; display: none;">
    <asp:Label ID="lblTApreciado" runat="server" Text="Apreciado(a) "></asp:Label>
    <asp:Label ID="lblNombreUsuario" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblTDatos" runat="server" Text=", por favor completa tu reserva con los datos a continuación solicitados:"></asp:Label>
</div>
<div style="width: 100%; font-size: 11px; text-align: right; padding-top: 10px; border-top: 1px solid #000;
    margin-top: 20px; display: none;">
    <asp:Label ID="lblTextPrecioSuscripcion" runat="server"></asp:Label>
    <asp:Label ID="lblPrecioSuscripcion" runat="server"></asp:Label>
</div>
<ajax:UpdatePanel ID="UpdatePanel1" runat="server" style="text-align: center; float: left;">
    <ContentTemplate>
        <asp:Label ID="lblUrlRedireccion" runat="server" Visible="false"></asp:Label>
    </ContentTemplate>
</ajax:UpdatePanel>
<!-- Confirmacion -->
<ajax:ModalPopupExtender ID="MPEEConfirm" BackgroundCssClass="ui-widget-shadow" runat="server"
    TargetControlID="dummyLink5" BehaviorID="MPEEConfirm" OnOkScript="" OkControlID="btnCerrar5"
    PopupControlID="Panel2" />
<asp:Panel runat="server" ID="Panel2">
    <div style="text-align: center; background: #fff; width: 280px; height: 100px; padding: 20px;
        color: #000;">
        <ajax:UpdatePanel ID="upRecord" runat="server" style="text-align: center;">
            <ContentTemplate>
                <asp:Label ID="lblTextoConfirm" runat="server" Text="Tu reserva ha sido confirmada bajo el record:"></asp:Label>
                <br />
                <asp:Label Style="font-size: 20px; padding: 5px; text-align: center; padding-bottom: 10px;"
                    runat="server" ID="lblRecord" ForeColor="red"></asp:Label>
            </ContentTemplate>
        </ajax:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <asp:Button runat="server" ID="btnContinuar" CssClass="botonBuscador" Text="Finalizar"
            OnClick="btnContinuar_Click" Style="padding: 5px; margin: 20px;" />
    </div>
    <div style="display: none;">
        <asp:Button ID="btnCerrar5" CssClass="btnCerrar" runat="server" />
    </div>
    <div class="panelConfirmacion" style="display: none;">
        <div class="tituloCorporativoIndex">
            <asp:Label ID="Label1" runat="server" Text="CONFIRMACION DE PAGO &raquo;"></asp:Label>
        </div>
        <%--
        y el localizador
        <asp:Label CssClass="recordReserva" runat="server" ID="lblProyecto"></asp:Label>
        --%>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink5"
    runat="server">dummy</a>
<!------Seleccion de pasajeros--->
<ajax:ModalPopupExtender ID="MPAfiliados" BackgroundCssClass="ui-widget-shadow" runat="server"
    TargetControlID="dummyLinkMPAfiliados" BehaviorID="MPAfiliados" OnOkScript=""
    OkControlID="btncerrar" PopupControlID="udpReserva" />
<asp:Panel ID="udpReserva" runat="server">
    <div>
        <asp:Button ID="btncerrar" runat="server" CssClass="botonCerrarVuelosTarjeta" />
    </div>
    <div class="panelPasajeros">
        <asp:Panel ID="pnlpasajeros" runat="server" Style="display: block; float: left; width: 80%;
            margin-left: 10%;">
            <asp:Repeater ID="rptpasajeros" runat="server">
                <ItemTemplate>
                    <div style="width: 100%; color: #000; line-height: 26px; margin-bottom: 10px; float: left;">
                        <asp:Label ID="lbltpousuario" runat="server" CssClass="tipoUsuarioPax"><%#DataBinder.Eval(Container.DataItem, "strtipouser")%></asp:Label>
                        <asp:Button ID="btnSeleccionarPasajero" runat="server" OnClick="btnseleccionar_Click"
                            CssClass="btn white" Text="seleccionar" Style="float: left; margin: 0;" />
                        <asp:Label ID="lblid" runat="server" Style="display: none;">
                                <%#DataBinder.Eval(Container.DataItem, "intUsuario")%>
                        </asp:Label>
                        <asp:Label ID="lblNombre" runat="server" Style="float: left; text-transform: uppercase;
                            margin-left: 30px; width: 250px;">
                                <span style="color:#84021C">Nombre: </span>
                                <%#DataBinder.Eval(Container.DataItem, "strNombre")%>
                        </asp:Label>
                        <asp:Label ID="lblApellido" runat="server" Style="float: left; text-transform: uppercase;
                            margin-left: 30px; width: 250px;">
                                <span style="color:#84021C">Apellido: </span>
                                <%#DataBinder.Eval(Container.DataItem, "strApellido")%>
                        </asp:Label>
                        <asp:Label ID="lblGenero" runat="server" Style="display: none;"><%#DataBinder.Eval(Container.DataItem, "intGenero")%></asp:Label>
                        <asp:Label ID="lbldtmFechanac" runat="server" Style="display: none;"><%#DataBinder.Eval(Container.DataItem, "dtmFechanac")%></asp:Label>
                        <asp:Label ID="lblinttipoident" runat="server" Style="display: none;"><%#DataBinder.Eval(Container.DataItem, "inttipoident")%></asp:Label>
                        <asp:Label ID="lblIdentificacion" runat="server" Style="display: none;"><%#DataBinder.Eval(Container.DataItem, "stridentificacion")%></asp:Label>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label ID="lbliditemseleccionado" runat="server" Style="display: none; float: left;"></asp:Label>
            <asp:Label ID="lbltexto" runat="server" CssClass="notaAfiliados"></asp:Label>
            <div id="btnRegistrar" runat="server" style="float: left;">
                <a href="#" onclick="Registro(1)" class="botonBuscador" style="line-height: 26px;
                    color: #84021C; background: #CCC;">Registrar Nuevo</a>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlNuevosAfiliados" runat="server" Style="display: none; float: left;
            width: 100%;">
            <span class="vino" style="float: left; width: 100%;">
                <asp:Label ID="lblDescripcion" runat="server" Text="REGISTRO DE AFILIADOS" CssClass="tituloNuevoAfiliado"></asp:Label>
            </span><span class="notaAfiliados">
                <asp:Label ID="lblNota" runat="server" Text="Por favor ten en cuenta que los 'elegidos' no podrán eliminarse una vez creados. <br> Todos los Datos Son obligatorios"></asp:Label>
            </span>
            <div class="lineaAfiliados">
                <div class="celdaAfiliados">
                    <asp:Label ID="lblTNombre" runat="server" Text="NOMBRE" CssClass="block full labelAfiliados"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="full text_in"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVTNombre" runat="server" ControlToValidate="txtNombre"
                        ValidationGroup="Registro" InitialValue="" ErrorMessage="Nombre" Text="*Ingrese el Nombre"
                        CssClass="block full labelAfiliados"></asp:RequiredFieldValidator>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="LowercaseLetters,UppercaseLetters"
                        TargetControlID="txtNombre">
                    </ajax:FilteredTextBoxExtender>
                </div>
                <div class="celdaAfiliados">
                    <asp:Label ID="lblTApellido" runat="server" Text="APELLIDO" CssClass="block full labelAfiliados"></asp:Label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="full text_in"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="Registro" InitialValue="" ID="RFVTApellido"
                        runat="server" ControlToValidate="txtApellido" ErrorMessage="Apellido" Text="*Ingrese el Apellido"
                        CssClass="block full labelAfiliados"></asp:RequiredFieldValidator>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="LowercaseLetters,UppercaseLetters"
                        TargetControlID="txtApellido">
                    </ajax:FilteredTextBoxExtender>
                </div>
                <div class="celdaAfiliados">
                    <asp:Label ID="lblTFechaNacimiento" runat="server" Text="Fecha de Nacimiento" CssClass="block full labelAfiliados"></asp:Label>
                    <asp:TextBox ID="txtEdad1" runat="server" CssClass="block text_in left_normal"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEdad1"
                        ValidationGroup="Registro" InitialValue="" ErrorMessage="Identificación" Text="*Ingrese la Fecha de Nacimiento"
                        CssClass="block full labelAfiliados"></asp:RequiredFieldValidator>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,Numbers"
                        InvalidChars="'" ValidChars="/" TargetControlID="txtEdad1">
                    </ajax:FilteredTextBoxExtender>
                </div>
            </div>
            <div class="lineaAfiliados">
                <div class="celdaAfiliados">
                    <asp:Label ID="lblTpoDocumento" runat="server" Text="Tipo de Documento" CssClass="block full labelAfiliados"></asp:Label>
                    <asp:DropDownList ID="ddlTpoDocumentoR" runat="server" CssClass="full text_in">
                    </asp:DropDownList>
                </div>
                <div class="celdaAfiliados">
                    <asp:Label ID="lblTIdentificacion" runat="server" Text="IDENTIFICACIÓN" CssClass="block full labelAfiliados"></asp:Label>
                    <asp:TextBox ID="txtDocumentosid" runat="server" CssClass="full text_in"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVTIdentificacion" runat="server" ControlToValidate="txtDocumentosid"
                        ValidationGroup="Registro" InitialValue="" ErrorMessage="Identificación" Text="*Ingrese la identificación"
                        CssClass="block full labelAfiliados"></asp:RequiredFieldValidator>
                    <ajax:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers,LowercaseLetters,UppercaseLetters"
                        TargetControlID="txtDocumentosid">
                    </ajax:FilteredTextBoxExtender>
                </div>
                <div class="celdaAfiliados">
                    <asp:Label ID="lblTGenero" runat="server" Text="Genero" CssClass="block full labelAfiliados"></asp:Label>
                    <asp:DropDownList ID="ddlGeneroR" runat="server" CssClass="full text_in">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="lineaAfiliados">
                <div class="celdaAfiliados">
                    <asp:Label ID="lblTEmailRegistro" runat="server" Text="MAIL" CssClass="block full labelAfiliados"></asp:Label>
                    <asp:TextBox ID="txtMailPersonal" runat="server" CssClass="full text_in"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ErrorMessage="Email Invalido" Font-Bold="False" Text="Ingrese el Mail" CssClass="block full labelAfiliados"
                        ControlToValidate="txtMailPersonal" Font-Underline="False" ValidationGroup="Registro">
                    </asp:RegularExpressionValidator>
                    <asp:Label ID="lblErrorMail" runat="server"></asp:Label>
                </div>
                <div class="celdaAfiliados">
                    <asp:Label ID="lblTTelefonos" runat="server" Text="Telefono" CssClass="block full labelAfiliados"></asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="full text_in"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="Validanumeros" runat="server" FilterType="Numbers"
                        TargetControlID="txtTelefono">
                    </ajax:FilteredTextBoxExtender>
                </div>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="true"
                ValidationGroup="Registro" HeaderText="Los campos Marcados con (*) son Obligatorios"
                DisplayMode="SingleParagraph" Font-Size="11px" ForeColor="#000000" CssClass="ErrorRegistro" />
            <asp:Button CssClass="block white right cursor btn" ID="lbCrear" runat="server" ValidationGroup="Registro"
                OnClick="lbCrear_Click" Text="GUARDAR"></asp:Button>
        </asp:Panel>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLinkMPAfiliados"
    runat="server">dummy</a> <a href="#" style="display: none; visibility: hidden;" onclick="return false"
        id="dummyLinkMPARecord" runat="server">dummy</a>
<!------Valores para enviar a pagos--->
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
<!------Modal que levanta la cortinilla --->
<%--
<ajax:ModalPopupExtender ID="MPEEReserva" BackgroundCssClass="ui-widget-shadow" runat="server"
    TargetControlID="dummyLink4" BehaviorID="MPEEReserva" OnOkScript="" OkControlID="btnCerrar4"
    PopupControlID="Panel1" />
<asp:Panel runat="server" ID="Panel1">
    <div style="display: none;">
        <asp:Button ID="btnCerrar4" CssClass="btnCerrar" runat="server" />
    </div>
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="../App_Themes/Imagenes/TopFlight/cortinillaGeneral.gif" />
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink4"
    runat="server">dummy</a>--%>