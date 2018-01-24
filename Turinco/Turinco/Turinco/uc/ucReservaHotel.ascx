<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucReservaHotel.ascx.cs"
    Inherits="uc_ucReservaHotel" %>
<%@ Register Src="ucVentanaConfirmacion.ascx" TagName="ucVentanaConfirmacion" TagPrefix="uc1" %>
<%@ Register Src="../uc/ucPopoupMensaje.ascx" TagName="ucPopoupMensaje" TagPrefix="uc6" %>
<%--<%@ Register Src="../uc/ucRegistro.ascx" TagName="ucRegistro" TagPrefix="uc15" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:HiddenField ID="HFCliente" Value="0" runat="server" />
<asp:HiddenField ID="HidenRefereTemaMensaje" Value="0" runat="server" />
<div class="panelTabs">
    <%--<label id="ddlTipoMensaje" runat="server" style="display: none;">
    </label>
    <label id="ddlTemamensaje" runat="server" style="display: none;">
    </label>
    <asp:Panel id="txtComentarios" style="display: none;">
    </aps:panel>--%>
    <label id="ddlPais" runat="server" style="display: none;">
    </label>
    <div class="panelCompletoCorporativo" style="padding-bottom: 20px; padding-top: 20px;">
        <div class="panelIzquierdoReservaVuelos nuevoHoteles">
            <div class="panelDerechoReservaVuelos">
                <div class="reservaTituloVuelosTarjeta">
                    <asp:Label ID="lblTResumenHotel" runat="server" Text="Información del hotel"></asp:Label>
                </div>
                <div class="reservaResumen300">
                    <div style="float: left; width: 130px;">
                        <asp:Image ID="iImagen" runat="server" class="imagen" ImageUrl="" />
                    </div>
                    <div style="float: right; width: 100%; padding-left: 20px; box-sizing: border-box;
                        font-weight: bold;">
                        <asp:Label ID="lblNombre" runat="server" CssClass="vino t_left"></asp:Label>
                    </div>
                    <div style="float: right; width: 100%; position: relative; box-sizing: border-box;
                        padding-left: 20px; margin-top: 10px; font-weight: normal;">
                        <asp:Label ID="Label1" runat="server" Text="Categoria"></asp:Label>
                        <asp:Repeater ID="RptEstrellas" runat="server">
                            <ItemTemplate>
                                <div style="display: inline-block;" class='<%# DataBinder.Eval(Container,"DataItem.style") %>'>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div style="width: 100%; margin-top: 10px; float: left; text-align: justify; padding-left: 20px;
                        padding-bottom: 20px; box-sizing: border-box;">
                        <asp:Label ID="lblDireccion" runat="server"></asp:Label><br />
                        <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
                        <div class="PrecioCotzd">
                            <asp:Label ID="Label1502" Text="Precio Total" CssClass="TituloPrice1" runat="server">Precio Total</asp:Label>
                            <br />
                            <br />
                            <div class="PriceDivgen">
                                <asp:Label ID="lblMoneda" runat="server" CssClass="Price112"></asp:Label>&nbsp;
                                <asp:Label ID="lblPrecioTotal" runat="server" CssClass="Price112"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="dvFormulario" runat="server">
                </div>
                <div class="reservaTituloVuelosTarjeta">
                    <asp:Label ID="Label2" runat="server" Text="Resumen de la reserva"></asp:Label>
                </div>
                <div class="reservaResumen300" style="width: 275px; padding: 0 10px 0 15px; font-size: 12px;">
                    <asp:Repeater ID="rptHabitaciones" runat="server">
                        <ItemTemplate>
                            <!-- Datos de la habitación -->
                            <div class="clear block">
                                <asp:Label ID="Label14" CssClass="bold" runat="server" Text="Tipo de habitación"></asp:Label>
                                <asp:Label ID="lblTipoHabitacion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.tipoHabitacion")%>'></asp:Label>
                            </div>
                            <div class="clear block">
                                <asp:Label ID="lblRegimenT" CssClass="bold" runat="server" Text="Tipo de alimentación"></asp:Label>
                                <asp:Label ID="lblRegimen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Regimen")%>'></asp:Label>
                            </div>
                            <div class="clear block">
                                <asp:Label ID="Label15" CssClass="bold" runat="server" Text="Check In"></asp:Label>
                                <asp:Label ID="lblCheckIn" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.checkIn")%>'></asp:Label>
                            </div>
                            <div class="clear block">
                                <asp:Label ID="Label18" CssClass="bold" runat="server" Text="Check Out"></asp:Label>
                                <asp:Label ID="lblCheckOut" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.checkOut")%>'></asp:Label>
                            </div>
                            <div class="clear block">
                                <asp:Label ID="Label19" CssClass="bold" runat="server" Text="Estado"></asp:Label>
                                <asp:Label ID="lblEstado" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.estado")%>'></asp:Label>
                            </div>
                            <div class="clear block">
                                <asp:Label ID="Label20" CssClass="bold" runat="server" Text="Adulto(s) / Menor(es)"></asp:Label>
                                <asp:Label ID="lblNumHuespedes" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.numHuespedes")%>'></asp:Label>
                            </div>
                            <div class="clear block">
                            </div>
                            <!-- Penalizacion de pago -->
                            <asp:Repeater ID="RptPenalizacion" runat="server">
                                <HeaderTemplate>
                                    <div style="display: none;">
                                        <asp:Label ID="lblCurrPenT" runat="server" Text="Moneda"></asp:Label>
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div style="margin: 10px 0; border-bottom: 1px solid #ccc; border-top: 1px solid #ccc;
                                        padding: 5px; display: none;">
                                        <div class="clear block">
                                            <asp:Label ID="lblFecIniPenT" runat="server" CssClass="bold" Text="Fecha límite para cancelar"></asp:Label>
                                            <asp:Label ID="lblFecIniPen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DateTimeFrom_YMD")%>'></asp:Label>
                                        </div>
                                        <div class="clear block">
                                            <asp:Label ID="lblValPenT" runat="server" CssClass="bold" Text="Valor penalidad por cancelación"></asp:Label>
                                            <asp:Label ID="lblValPen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Cancellation_TotalAmountText")%>'></asp:Label>
                                            <asp:Label ID="lblCurrPen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Currency_Code")%>'></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="clear block" style="margin-bottom: 10px;">
                        <asp:Label ID="lblObservaciones" CssClass="bold" runat="server" Style="text-align: justify;
                            width: 100%; float: left;"></asp:Label>
                    </div>
                    <div class="clear block" style="margin-bottom: 10px;">
                        <asp:Label ID="lblCondicones" runat="server" Style="text-align: justify; width: 100%;
                            float: left;"></asp:Label>
                    </div>
                    <div class="clear block" style="text-align: right; margin-top: 20px;">
                        <asp:Label ID="lblTfechaPago" CssClass="bold vino" runat="server" Text="PLAZO LÍMITE DE PAGO: "></asp:Label>
                        <asp:Label ID="lblFechaPago" CssClass="bold" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="divisorAzul300 ">
                </div>
            </div>
            <div class="datosDePasajeros datosDePasajeros1">
                <%--<div class="renglonFormulario">
                    Por favor ingresa los datos de contacto del Usuario.
                </div>--%>
                <asp:UpdatePanel ID="panelreserva" runat="server">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="jsn" runat="server">
                            <ContentTemplate>
                                <div class="titulo">
                                    <asp:Label ID="Label5" runat="server" Text="Datos del Usuario"></asp:Label>
                                </div>
                                <div class="reservaRegistroVuelosTarjeta">
                                    <table cellpadding="2" cellspacing="1" class="tablaPasajeros">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="renglonFormulario">
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label CssClass="textoLogin" ID="lblIdUsuario" Visible="false" runat="server"
                                                                Text="0"></asp:Label>
                                                            <asp:Label ID="Label13" runat="server" Text="Nombre (*)" CssClass="label iblock full"></asp:Label>
                                                            <asp:TextBox ID="txtNombre" Width="80%" CssClass="campo iblock" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="Label16" runat="server" Text="Apellido (*)" CssClass="label iblock full"></asp:Label>
                                                            <asp:TextBox ID="txtApellido" Width="80%" CssClass="campo iblock" runat="server"
                                                                Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="renglonFormulario">
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="Label25" runat="server" Text="Cédula (*)" CssClass="label iblock full"></asp:Label>
                                                            <asp:TextBox ID="txtDocumento" Text="10" CssClass="campo iblock" Width="80%" runat="server"
                                                                Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="lblTEmailRegistro" runat="server" CssClass="labelDatosVuelosTarjeta"
                                                                Text="Correo electrónico (*)"></asp:Label>
                                                            <asp:TextBox ID="txtMailPersonal" runat="server" Enabled="false"></asp:TextBox>
                                                            <asp:Label ID="lblErrorMail" runat="server" CssClass="Error" ForeColor="Maroon" Text=""></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="renglonFormulario">
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="lblTelf" runat="server" Visible="true" CssClass="labelDatosVuelosTarjeta"
                                                                Text="Teléfono fijo"></asp:Label>
                                                            <asp:TextBox ID="txtTelefono" Text="" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="lblCiudadR" runat="server" Visible="true" Text="Ciudad (*)" CssClass="labelDatosVuelosTarjeta"></asp:Label>
                                                            <asp:TextBox ID="txtCiudad" Visible="true" Text="" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="lblTCelular" Visible="true" runat="server" Text="Celular (*)" CssClass="labelDatosVuelosTarjeta"></asp:Label>
                                                            <asp:TextBox ID="txtCelular" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <%--<uc15:ucRegistro ID="ucRegistro" runat="server" />--%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:Repeater ID="rptDatosReserva" runat="server">
                <ItemTemplate>
                    <div class="reservaRegistroVuelosTarjeta" style="font-size: 12px;">
                        <div class="datosDePasajeros datosDePasajeros3">
                            <div class="titulo">
                                <asp:Label ID="Label5" runat="server" Text="Datos del Huesped"></asp:Label>
                            </div>
                            <div class="CounterHabs">
                                <asp:Label ID="lblhab_Counter" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="renglonFormulario">
                                <div class="celdaFormulario">
                                    <asp:Label ID="lblTPrimeroNombre" runat="server" Text="Nombres(*)" CssClass="labelDatosVuelosTarjeta"></asp:Label>
                                    <asp:TextBox ID="txtNombre1" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FttxtNombre1" runat="server" FilterType="LowercaseLetters,UppercaseLetters"
                                        TargetControlID="txtNombre1">
                                    </ajax:FilteredTextBoxExtender>
                                </div>
                                <div class="celdaFormulario">
                                    <asp:Label ID="lblTPrimerApellido" runat="server" Text="Apellidos(*)" CssClass="labelDatosVuelosTarjeta"></asp:Label>
                                    <asp:TextBox ID="txtApellido1" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FttxtApellido1" runat="server" FilterType="LowercaseLetters,UppercaseLetters"
                                        TargetControlID="txtApellido1">
                                    </ajax:FilteredTextBoxExtender>
                                </div>
                                <div class="celdaFormulario">
                                    <asp:Label ID="Label12" runat="server" Text="Fecha de nacimiento(*)" CssClass="labelDatosVuelosTarjeta"></asp:Label>
                                    <asp:TextBox ID="txtEdad1" runat="server"></asp:TextBox>
                                    <asp:Label ID="lblErrorFecha" ForeColor="#186e9b" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblErrorEdad123" ForeColor="red" runat="server" CssClass="StyleErrorDate"
                                        Text=""></asp:Label>
                                    <ajax:FilteredTextBoxExtender ID="FttxtEdad1" runat="server" FilterType="Custom,Numbers"
                                        ValidChars="/" TargetControlID="txtEdad1">
                                    </ajax:FilteredTextBoxExtender>
                                </div>
                            </div>
                            <div class="renglonFormulario">
                                <div class="celdaFormulario">
                                    <asp:Label ID="Label4" runat="server" Text="Género(*)" CssClass="labelDatosVuelosTarjeta"></asp:Label>
                                    <asp:DropDownList ID="ddlGenero" runat="server">
                                        <%--<asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Femenino" Value="F"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="celdaFormulario">
                                    <asp:Label ID="Label6" runat="server" Text="Tipo de Documento(*)" CssClass="labelDatosVuelosTarjeta"></asp:Label>
                                    <asp:DropDownList ID="ddlTipoDoc" runat="server">
                                        <asp:ListItem Text="C.C." Value="1"></asp:ListItem>
                                        <asp:ListItem Text="C.E." Value="2"></asp:ListItem>
                                        <asp:ListItem Text="T.I." Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Pasaporte" Value="PS"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="celdaFormulario">
                                    <asp:Label ID="Label10" runat="server" Text="No de identificación(*)" CssClass="labelDatosVuelosTarjeta "></asp:Label>
                                    <asp:TextBox ID="txtDocumento1" runat="server"></asp:TextBox>
                                    <ajax:FilteredTextBoxExtender ID="FttxtDocumento1" runat="server" FilterType="Numbers,LowercaseLetters,UppercaseLetters"
                                        TargetControlID="txtDocumento1">
                                    </ajax:FilteredTextBoxExtender>
                                </div>
                                <div style="display: none;">
                                    <div class="left adulto" style="display: none;">
                                        <asp:Label ID="lblTTipoPasajero" runat="server" Text="Tipo pasajero"></asp:Label>
                                        <%-- <asp:TextBox ID="txtTipoPasajero1" Text='<%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%>'
                                            runat="server" ReadOnly="True" CssClass="labelTipoPasajeroVuelosTarjeta"></asp:TextBox>--%>
                                    </div>
                                    <div style="display: none;">
                                        <asp:Label ID="lblTTrato" runat="server" Text="Trato"></asp:Label>
                                        <asp:DropDownList Width="52px" ID="ddlTrato1" runat="server">
                                            <asp:ListItem Text="MR"></asp:ListItem>
                                            <asp:ListItem Text="MRS"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label ID="lblError1" runat="server" Text=""></asp:Label>
            <%-- <div class="datosDePasajeros datosDePasajeros1">
                <div class="renglonFormulario">
                    Por favor ingresa los datos de contacto del Usuario.
                </div>
                <asp:UpdatePanel ID="panelreserva" runat="server">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="jsn" runat="server">
                            <ContentTemplate>
                                <div class="reservaRegistroVuelosTarjeta">
                                    <table cellpadding="2" cellspacing="1" class="tablaPasajeros">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <div class="renglonFormulario">
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="lblTEmailRegistro" runat="server" CssClass="labelDatosVuelosTarjeta"
                                                                Text="Correo electrónico (*)"></asp:Label>
                                                            <asp:TextBox ID="txtMailPersonal" runat="server"></asp:TextBox>
                                                            <asp:Label ID="lblErrorMail" runat="server" CssClass="Error" ForeColor="Maroon" Text=""></asp:Label>
                                                        </div>
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="lblTelf" runat="server" Visible="true" CssClass="labelDatosVuelosTarjeta"
                                                                Text="Teléfono fijo"></asp:Label>
                                                            <asp:TextBox ID="txtTelefono" Text="" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="renglonFormulario">
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="lblCiudadR" runat="server" Visible="true" Text="Ciudad (*)" CssClass="labelDatosVuelosTarjeta"></asp:Label>
                                                            <asp:TextBox ID="txtCiudad" Visible="true" Text="" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="celdaFormulario" style="width: 49%;">
                                                            <asp:Label ID="lblTCelular" Visible="true" runat="server" Text="Celular (*)" CssClass="labelDatosVuelosTarjeta"></asp:Label>
                                                            <asp:TextBox ID="txtCelular" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>--%>
            <div class="datosDePasajeros datosDePasajeros2">
                <!-- Formas de Pago -->
                <tr>
                    <td>
                        <div class="titulo">
                            Condiciones de reserva
                        </div>
                        <asp:Label CssClass="bold" ID="lblGarantia" runat="server"></asp:Label>
                        <asp:Repeater ID="RptPenalizacionGara" runat="server">
                            <ItemTemplate>
                                <div style="margin: 10px 0; border-bottom: 1px solid #ccc; border-top: 1px solid #ccc;
                                    padding: 5px;">
                                    <div class="CounterHabs2">
                                        <asp:Label ID="lblhab_Counter" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="clear block">
                                        <asp:Label ID="lblTxtFechaLimiteT" runat="server" CssClass="bold" Text="Si cancelas después de las "></asp:Label>
                                        <asp:Label ID="lblTxtHoraLimite" runat="server"></asp:Label>
                                        <asp:Label ID="lblTxt1" runat="server" CssClass="bold" Text=" del "></asp:Label>
                                        <asp:Label ID="lblTxtFechaLimite" runat="server"></asp:Label>
                                        <asp:Label ID="LbTxtl2" runat="server" CssClass="bold" Text=" se aplicarán unos gastos de: "></asp:Label>
                                        <asp:Label ID="lbllblTxtPlataLimite" runat="server"></asp:Label>
                                        <asp:Label ID="lbllblTxtCurrencyLimite" runat="server"></asp:Label>
                                    </div>
                                    <p class="TextoAdverHusos">
                                        Las horas y fechas se calculan en base al horario local en el país de destino.
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <br />
                <div class="tarifasquemadas">
                    <div class="titulo">
                        Condiciones del servicio
                    </div>
                    <ul>
                        <li>Tarifas sujetas a cambio sin previo aviso y disponibilidad.</li>
                        <li>Mientras el pago de su reserva es procesado la tarifa puede cambiar.</li>
                        <li>Se entiende que un usuario que reserva un hotel de los aquí enunciados conoce y
                            comprende las distintas categorías y características de los hoteles ofrecidos y
                            las acepta, por tanto no se aceptan reclamos por este concepto.</li>
                        <li>Se entiende que el usuario que reserva un hotel es una persona mayor de 18 años
                            de edad o más y tiene capacidad legal para crear un contrato legalmente vinculante
                            con www.turinco.co, garantiza que toda la información ingresada durante el proceso
                            de reserva es correcta y acepta la responsabilidad financiera de las reservas realizadas
                            bajo su nombre.</li>
                        <li>Las cancelaciones de reservas que se realizan posteriores a la fecha límite indicada
                            al momento de reservar, generan penalidades que deben ser asumidas por el usuario.</li>
                        <li>El uso de nuestro sitio web implica la aceptación completa sin exclusión, de las
                            condiciones contenidas en estas claúsulas y es una prueba del consentimiento expreso
                            para contratar el servicio reservado.</li>
                        <li>La tarifa reservada solo se garantiza con la emisión del voucher de servicio una
                            vez se haya recibido el pago.</li>
                    </ul>
                    <div id="divFormaspago" runat="server">
                        <br />
                        <div class="titulo">
                            Formas de pago
                        </div>
                        <ajax:UpdatePanel ID="upFormasPago" runat="server">
                            <ContentTemplate>
                                <div class="tipoPagoVuelosTarjeta">
                                    <asp:RadioButtonList ID="rblFormasPago" runat="server" Style="width: 550px; float: left;"
                                        AutoPostBack="true" OnSelectedIndexChanged="rblFormasPago_SelectedIndexChanged">
                                    </asp:RadioButtonList>
                                </div>
                                <!-- Tarjeta de Credito -->
                                <div class="tipoTarjeta" id="DivTC" style="display: none" runat="server">
                                    <img src="../App_Themes/Imagenes/LogosTarjetaCredito.png" style="float: left; margin-left: 15px;
                                        margin-bottom: 10px;" />
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
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
                                                    &nbsp;/&nbsp;
                                                    <asp:TextBox ID="txtAnioVencimiento" runat="server" Style="width: 50px;" MaxLength="4"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtTelefonoOtro" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- Otros -->
                                <div style="float: left; width: 530px; padding: 25px; position: relative; display: none;"
                                    id="DivPSE" runat="server">
                                    <p>
                                        Esta opci&oacute;n te permitir&aacute; el pago con d&eacute;bito a una cuenta bancaria
                                        (ahorros o corriente), para hacerlo debes tener habilitado en tu cuenta la opci&oacute;n
                                        de hacer pagos por internet.</p>
                                    </p>
                                </div>
                                <div style="float: left; width: 530px; padding: 25px; position: relative; display: none"
                                    id="DivEfe" runat="server">
                                    <div>
                                        <p>
                                            &nbsp;
                                        </p>
                                        <div>
                                            <p>
                                                Puede realizar su pago en la oficina de Turinco ubicada en la Carrera 11 No. 86-32
                                                Oficina 401. en Bogot&aacute; - Colombia. Tel 2568888 o consignar en las siguientes
                                                cuentas bancarias:
                                            </p>
                                            <br />
                                            <ul style="margin-left: 67px;">
                                                <li><strong>Pago en Pesos Colombianos</strong></li>
                                                <li>Cuenta Corriente Bancolombia No 039 823 05525</li>
                                                <li>Cuenta Corriente Davivienda No 008 969 994 816</li>
                                                <li>Cuenta Corriente BBVA No 627 039 597</li>
                                                <li><strong>Pago en Dólares Americanos</strong></li>
                                                <li>Cuenta Corriente en dólares Corbanca No 040 025 710, por favor tener encuenta que
                                                    al momento de consignar tambien se debe registrar la cuenta en pesos No. 040 023
                                                    889</li>
                                                <li><strong>Para pagar en pesos servicio reservado en dólares, antes de realizar el
                                                    pago se debe consultar con Turinco la tasa de cambio a dólares que se debe utilizar</strong></li>
                                            </ul>
                                            <p style="text-align: justify;">
                                                &nbsp;
                                            </p>
                                            <p>
                                                Una vez realizada la consignación por favor enviarla por correo electrónico reservas@turinco.co</p>
                                            <p>
                                                &nbsp;
                                            </p>
                                            <p>
                                                Por favor tenga en cuenta que solamente se emitirán los tiquetes y vouchers de servicio
                                                cuando se hay recibido el pago total de la reserva.
                                            </p>
                                        </div>
                                        <p>
                                            &nbsp;
                                        </p>
                                    </div>
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
                    <div class="contenedorCondicionesVuelosTarjeta">
                        <img src="../App_Themes/Imagenes/warningCondiciones.jpg" style="width: 40px; float: left;">
                        <%--<a href="#this" runat="server" onclick="return false" id="lCondiciones" style="float: left;
                        width: 490px; margin-left: 10px; margin-bottom: 10px; font-size: 14px;">Confirmo
                        datos de los pasajeros y acepto condiciones. </a>--%>
                        <asp:CheckBox ID="cbAceptar" runat="server" CssClass="labelCondicionesVuelosTarjeta  Lblcheckhotelescond"
                            OnClick="return ShowModalPopup();" Text="Confirmo datos de los pasajeros y acepto los términos y condiciones de reserva y tarifas" />
                        <%--<asp:Label ID="lblAcepto" AssociatedControlID="cbAceptar" runat="server" Text="Confirmo datos de los pasajeros y acepto los términos y condiciones de reserva y tarifas"
                        Style="float: left; width: 490px; margin-left: 20px;"></asp:Label>--%>
                        <div style="float: left; width: 100%; margin-top: 20px;">
                            <asp:Button ID="btnFinalizar" runat="server" OnClientClick="popUp();" CssClass="btnSiguienteVuelosTarjeta "
                                Text="Finalizar" CommandName="Confirmar" OnCommand="setCommand" Style="float: right;"
                                ValidationGroup="reservahotel" />
                            <asp:Button ID="btncancelar" runat="server" CssClass="btnCancelar" Style="float: left;
                                color: #FF9600; margin-top: 5px;" Text="<< Buscar más hoteles" OnClick="btnCancelar_Click" />
                        </div>
                    </div>
                    <ajax:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <%--
                        <asp:Button CssClass="btn white" ID="btnPagar" runat="server" EnableTheming="True" OnClientClick="popUp();" ValidationGroup="grupoValidacion" CommandName="Confirmar" OnCommand="btnReserva_Command" Text="RESERVAR Y PAGAR EN LÍNEA" />
                        <asp:Button CssClass="btn white" ID="btnGuardar" runat="server" EnableTheming="True" OnClientClick="popUp();" ValidationGroup="grupoValidacion" CommandName="Confirmar" Text="GUARDAR RESERVA MIENTRAS REALIZO EL PAGO" OnCommand="btnReserva_Command" Visible="false" />
                            --%>
                            <div class="mensajeError" id="dPanel" runat="server">
                            </div>
                        </ContentTemplate>
                    </ajax:UpdatePanel>
                </div>
            </div>
        </div>
        <!-- Resumen Pago -->
        <div class="confirmacionVuelo1">
            <div class="contenidoResultado">
                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                    <tr style="display: none">
                        <td>
                            <asp:Label ID="lblAdicional" CssClass="bold" runat="server" Text="Costos adicionales"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="display: none">
                            <asp:Repeater ID="RptAdicional" runat="server">
                                <HeaderTemplate>
                                    <table cellspacing="0" cellpadding="5" width="500" class="bordeTabla">
                                        <tr style="font-weight: bold; text-align: center">
                                            <td style="width: 300px;" align="left">
                                                <asp:Label ID="lblDetalleT" runat="server" Text="Detalle"></asp:Label>
                                            </td>
                                            <td style="width: 100px;">
                                                <asp:Label ID="lblCurrencyT" runat="server" Text="Moneda"></asp:Label>
                                            </td>
                                            <td style="width: 100px;">
                                                <asp:Label ID="lblValT" runat="server" Text="Costo"></asp:Label>
                                            </td>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr style="text-align: center">
                                        <td>
                                            <asp:Label ID="lblDetalle" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCurrency" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Currency_Code")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblVal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AmountText")%>'></asp:Label>
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
                        <td>
                            <asp:Label CssClass="bold" ID="Label3" runat="server" Text="Servicios prepago en efectivo en dolares, aplica el 2% de fee bancario"></asp:Label>
                        </td>
                    </tr>
                    <tr class="nomostrarportextios">
                        <td>
                            <asp:Label CssClass="bold" ID="lblGarantia3" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" class="bordeTablaPrecioHotel"
                                runat="server" id="tblInfoAdicional">
                                <tr>
                                    <td class="alineacionIzquierda" style="width: 150px">
                                        <strong>
                                            <asp:Label ID="Label7" runat="server" Text="Vat Number"></asp:Label></strong>
                                    </td>
                                    <td class="tituloPrecios5">
                                        <asp:Label ID="lblVatNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="alineacionIzquierda" style="width: 150px">
                                        <strong>
                                            <asp:Label ID="Label8" runat="server" Text="No Incoming Office"></asp:Label></strong>
                                    </td>
                                    <td class="tituloPrecios5">
                                        <asp:Label ID="lblIconming" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="alineacionIzquierda" style="width: 150px">
                                        <strong>
                                            <asp:Label ID="Label9" runat="server" Text="Loc Number"></asp:Label></strong>
                                    </td>
                                    <td class="tituloPrecios5">
                                        <asp:Label ID="lblLocNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblError" runat="server" ForeColor="#186e9b" Text="Para continuar, debe aceptar las condiciones e ingresar los datos de número de la tarjeta de crédito."
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</div>
<asp:Panel ID="pError" runat="server">
</asp:Panel>
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
<asp:HiddenField ID="bCreditoDispersion" runat="server" Value="False" />
<!-- Confirmacion -->
<ajax:ModalPopupExtender ID="MPEEConfirm" BackgroundCssClass="ui-widget-shadow" runat="server"
    TargetControlID="dummyLink5" BehaviorID="MPEEConfirm" OnOkScript="" OkControlID="btnCerrar5"
    PopupControlID="Panel2" />
<asp:Panel runat="server" ID="Panel2">
    <div style="text-align: center; background: #fff; width: 280px; height: 100px; padding: 20px;
        color: #000; z-index: 1000000000000000000000;">
        <ajax:UpdatePanel ID="upRecord" runat="server" style="text-align: center;">
            <ContentTemplate>
                <asp:Label ID="lblTextoConfirm" runat="server" Text="Tu reserva ha sido confirmada bajo el record:"></asp:Label>
                <br />
                <asp:Label ForeColor="red" runat="server" ID="lblRecord" Style="font-size: 20px;
                    padding: 5px; text-align: center; padding-bottom: 10px;"></asp:Label>
            </ContentTemplate>
        </ajax:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <asp:Button runat="server" ID="btnContinuar" CssClass="botonBuscador" Text="Finalizar"
            OnClick="btnContinuar_Click" Style="padding: 5px; margin: 20px;" />
        <asp:Button runat="server" ID="btnCancelarReserva" CssClass="botonBuscador" Text="Cancelar"
            Style="display: none;" OnClick="btnCancelarResrva_Click" />
    </div>
    <div style="display: none;">
        <asp:Button ID="btnCerrar5" CssClass="btnCerrar" runat="server" />
    </div>
    <div class="tituloCorporativoIndex" style="display: none;">
        <asp:Label ID="Label11" runat="server" Text="CONFIRMACION DE PAGO &raquo;"></asp:Label>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink5"
    runat="server">dummy</a>
<!-- Fallo proceso reserva -->
<ajax:ModalPopupExtender ID="MPEEFallo" BackgroundCssClass="ui-widget-shadow" runat="server"
    TargetControlID="dummyLink5" BehaviorID="MPEEFallo" OnOkScript="" OkControlID="btnCerrar5"
    PopupControlID="Panel3" />
<asp:Panel runat="server" ID="Panel3">
    <div style="text-align: center; background: #fff; width: 280px; height: 100px; padding: 20px;
        color: #000;">
        <ajax:UpdatePanel ID="UpdatePanel3" runat="server" style="text-align: center;">
            <ContentTemplate>
                <asp:Label ID="Label27" runat="server" Text="Debido a que la solicitud se encuentra dentro del plazo límite, un asesor se contactara con usted para continuar el proceso de reserva"></asp:Label>
            </ContentTemplate>
        </ajax:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <asp:Button runat="server" ID="Button1" CssClass="botonBuscador" Text="Aceptar" OnClick="btnContinuar_Click"
            Style="padding: 5px; margin: 20px;" />
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="A1"
    runat="server">dummy</a>
<!-- Reserva -->
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
<ajax:ModalPopupExtender ID="MPerrores" BackgroundCssClass="ui-widget-shadow" runat="server"
    TargetControlID="dummyLink6" BehaviorID="MPerrores" OnOkScript="" OkControlID="btnClose"
    CancelControlID="btnOk" PopupControlID="PnelErrores" />
<asp:Panel runat="server" ID="PnelErrores">
    <div style="display: none;">
        <asp:Button ID="btnClose" CssClass="btnCerrar" runat="server" />
    </div>
    <div class="panelConfirmacion">
        <div class="tituloCorporativoIndex">
            <asp:Label ID="Label29" runat="server" Text=""></asp:Label>
        </div>
        <div class="contenidoContactenos">
            <ajax:UpdatePanel ID="Updatepanel2" runat="server" style="text-align: center;">
                <ContentTemplate>
                    <asp:Label ID="lblErrores" runat="server" Text=""></asp:Label>
                </ContentTemplate>
            </ajax:UpdatePanel>
            <br />
            <asp:Button ID="btnOk" runat="server" Text="OK" />
        </div>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink6"
    runat="server">dummy</a>
<%-- Nuevo PopUp Condiciones --%>
<asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
<cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
    PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
    <div class="CondicionesHoteles">
        <div class="header titulocondihotel">
            Condiciones de la reseva
        </div>
        <div class="tarifasdesdewebserv">
            <script>
                //if ($("#ucReservaHotel_cbAceptar").is(":checked")) {
                // $('#ucReservaHotel_lblGarantia').html();
                //}
            </script>
            <%--<asp:Label CssClass="bold" ID="lblGarantia2" runat="server"></asp:Label>--%>
        </div>
        <div class="tarifasquemadas">
            <ul>
                <li>Tarifas sujetas a cambio sin previo aviso y disponibilidad.</li>
                <li>Mientras el pago de su reserva es procesado la tarifa puede cambiar.</li>
                <li>Se entiende que un usuario que reserva un hotel de los aquí enunciados conoce y
                    comprende las distintas categorías y características de los hoteles ofrecidos y
                    las acepta, por tanto no se aceptan reclamos por este concepto.</li>
                <li>Se entiende que el usuario que reserva un hotel es una persona mayor de 18 años
                    de edad o más y tiene capacidad legal para crear un contrato legalmente vinculante
                    con www.turinco.co, garantiza que toda la información ingresada durante el proceso
                    de reserva es correcta y acepta la responsabilidad financiera de las reservas realizadas
                    bajo su nombre.</li>
                <li>Las cancelaciones de reservas que se realizan posteriores a la fecha límite indicada
                    al momento de reservar, generan penalidades que deben ser asumidas por el usuario.</li>
                <li>El uso de nuestro sitio web implica la aceptación completa sin exclusión, de las
                    condiciones contenidas en estas claúsulas y es una prueba del consentimiento expreso
                    para contratar el servicio reservado.</li>
                <li>La tarifa reservada solo se garantiza con la emisión del voucher de servicio una
                    vez se haya recibido el pago.</li>
            </ul>
        </div>
        <asp:Button ID="btnHide" runat="server" Text="Aceptar" OnClientClick="return HideModalPopup()"
            CssClass="BotonAceptarCondicionesHoteles" />
    </div>
</asp:Panel>
<%--<script>
    //$("#ucReservaHotel_cbAceptar").click(function () {
        if ($("#ucReservaHotel_cbAceptar").is(":checked")) {
             
             $("#ucReservaHotel_cbAceptar").prop("checked", true);
            
            return ShowModalPopup();
            //$("#ucReservaHotel_cbAceptar").prop('checked', true);
        }
    //});
    
</script>--%>
<%--<script type="text/javascript">
    function ShowModalPopup() {
        $find("mpe").show();
        $("#ucReservaHotel_cbAceptar").prop("checked", "true");
        return false;
    }
    function HideModalPopup() {
        $find("mpe").hide();
        $("#ucReservaHotel_cbAceptar").prop("checked", "true");
        return false;
    }
</script>--%>
<%--<script>
    $("#ucReservaHotel_cbAceptar").on("click", function (e) {

        ShowModalPopup();

    })
</script>--%>
<script>
    function myFunc() {
        if ($('#ucReservaHotel_lblRecord') != "") {
            $('#ucReservaHotel_Panel1').hide();
        }
    }
</script>
