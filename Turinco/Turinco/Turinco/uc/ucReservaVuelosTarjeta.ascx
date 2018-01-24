<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucReservaVuelosTarjeta.ascx.cs" Inherits="uc_ucReservaVuelosTarjeta" %>
<%@ Register Src="ucRegistro.ascx" TagName="ucRegistro" TagPrefix="uc1" %>

<div class="iblock full reservaPaquetes reservaVuelos">
    <div class="panelLeft iblock">
        <div class="borde full iblock">
        </div>

        <div class="tituloReserva blanco arial" style="padding-top:18px;">
            <asp:Label ID="Label4" runat="server" Text=" Datos del usuario"></asp:Label>
        </div>

        <div class="datosViajero iblock full datosViajeroContacto">
            <uc1:ucRegistro ID="ucRegistro" runat="server" />
        </div>

        <div class="Separador_Reserva">
        </div>
        
        <div class="tituloReserva blanco arial" style="padding-top:18px;">
            <asp:Label ID="Label15" runat="server" Text=" Datos de los pasajeros"></asp:Label>
        </div>

        <asp:DataList ID="dtlPasajeros" runat="server" Width="100%" CssClass="tablaPasajerosReservaVuelos">
            <ItemTemplate>
                <div class="datosViajero iblock full">
                    <div class="renglonDatos iblock">
                        <div class="tipoPasajero">
                            <asp:TextBox ID="txtTipoPasajero1" Text='<%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%>' runat="server" ReadOnly="True" CssClass="labelTipoPasajeroVuelosTarjeta"></asp:TextBox>
                        </div>

                        <div class="datosObligatorios">
                            datos obligatorios (*)
                        </div>
                    </div>

                    <div class="renglonDatos iblock">
                        <div class="celdaDatos iblock">
                            <asp:Label ID="lblTPrimeroNombre" CssClass="label iblock full" runat="server" Text="Nombres (*)"></asp:Label>
                            <asp:TextBox ID="txtNombre1" CssClass="campo full iblock" runat="server" placeholder="Como figura en el documento"></asp:TextBox>
                        </div>

                        <div class="celdaDatos iblock">
                            <asp:Label ID="lblTPrimerApellido" CssClass="label iblock full" runat="server" Text="Apellidos (*)"></asp:Label>
                            <asp:TextBox ID="txtApellido1" CssClass="campo full iblock" runat="server" placeholder="Como figura en el documento"></asp:TextBox>
                        </div>

                        <div class="celdaDatos iblock">
                            <asp:Label ID="Label12" CssClass="label iblock full" runat="server" Text="Fecha de nacimiento"></asp:Label>
                            <asp:TextBox ID="txtEdad1" CssClass="campo full iblock" runat="server" placeholder="Como figura en el documento"></asp:TextBox>
                            <asp:Label ID="lblErrorFecha" ForeColor="#186e9b" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="renglonDatos iblock">
                        <div class="celdaDatos iblock">
                            <asp:Label ID="Label8" CssClass="label iblock full" runat="server" Text="Género (*)"></asp:Label>
                            <asp:DropDownList ID="ddlGenero" CssClass="campo full iblock" runat="server">
                            </asp:DropDownList>
                        </div>

                        <div class="celdaDatos iblock">
                            <asp:Label ID="Label9" CssClass="label iblock full" runat="server" Text="Tipo identificación"></asp:Label>
                            <asp:DropDownList ID="ddlTipoDoc" CssClass="campo full iblock" runat="server">
                            </asp:DropDownList>
                        </div>

                        <div class="celdaDatos iblock">
                            <asp:Label ID="Label10" CssClass="label iblock full" runat="server" Text="Identificación (*)"></asp:Label>
                            <asp:TextBox ID="txtDocumento1" CssClass="campo full iblock" runat="server" placeholder="Numero identificacion"></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <div class="noneDisplay">
                    <asp:Label ID="Label6" runat="server" Text="INFORMACIÓN DE PASAJERO"></asp:Label>
                    <asp:Label ID="lblTTrato" runat="server" Text="Trato"></asp:Label>
                    <asp:DropDownList Width="52px" ID="ddlTrato1" runat="server">
                        <asp:ListItem Text="MR"></asp:ListItem>
                        <asp:ListItem Text="MRS"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </ItemTemplate>
        </asp:DataList>

        <div class="Separador_Reserva">
        </div>

        <div class="tituloReserva blanco arial" style="padding-top:18px;">
            Formas de pago
        </div>

        <div class="datosViajero iblock full datosViajeroContacto">
            <div class="resumenReserva" id="divFormaspago" runat="server">
                <ajax:UpdatePanel ID="upFormasPago" runat="server">
                    <ContentTemplate>
                        <div class="tipoPagoVuelosTarjeta">
                            <asp:RadioButtonList ID="rblFormasPago" runat="server" RepeatDirection="Horizontal" Style="width: 550px; float: left;" AutoPostBack="true" OnSelectedIndexChanged="rblFormasPago_SelectedIndexChanged">
                            </asp:RadioButtonList>
                        </div>
                         <!-- Tarjeta de Credito -->
                            <div class="tipoTarjeta" id="DivTC" runat="server"  style="display: none">
                                <%-- <asp:RadioButtonList ID="rblFranquicias" runat="server">
                                    <asp:ListItem Selected="false" Text="Visa"></asp:ListItem>
                                    <asp:ListItem Text="Diners"></asp:ListItem>
                                    <asp:ListItem Text="American Express"></asp:ListItem>
                                    <asp:ListItem Text="Master Card"> </asp:ListItem>
                                </asp:RadioButtonList>--%>
                                <div class="logosTarjetas">
                                    <img src="../App_Themes/Imagenes/master.png" style="float: left; margin-left: 28px;
                                    margin-bottom: 18px;   margin-top: 12px;  width: 21%;" />
                                </div>
                                
                                    <br />
                                    <br />
                                    <br />
                                <div class="ContenedorTarjetas">
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblErrorTarjeta" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label141" runat="server" Text="Tarjeta (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="Número de tarjeta (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label17" runat="server" Text="Banco Emisor (*)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlFranquicia" class="listasTC" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumTarjeta" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBanco" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="Vence (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label19" runat="server" Text="Código de seguridad (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Número de cuotas (*)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlMesVencimiento" class="listasTC" runat="server">
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
                                            &nbsp;/&nbsp; <asp:DropDownList ID="txtAnioVencimiento" class="listasTC" runat="server">
                                                <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                                <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                            </asp:DropDownList><%--<asp:TextBox ID="txtAnioVencimiento" runat="server" Width="50"></asp:TextBox>--%>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodSeguridad" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCuotas" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label21" runat="server" Text="Nombre completo del tarjetahabiente (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text="Cédula del titular de la cuenta (*)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTitular" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIdentificacion" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label23" runat="server" Text="Dirección (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label24" runat="server" Text="Ciudad / país (*)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtDireccion" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPais" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label26" runat="server" Text="Teléfono horas laborales (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label38" runat="server" Text="Otro teléfono (*)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoOficina" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoOtro" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>

                                </div>
                            </div>

                            <!-- Tarjeta de Credito POL -->
                            <div class="tipoTarjeta" id="DivTCPOL" runat="server">
                                <img src="../App_Themes/Imagenes/LogosTarjetaCredito.png" style="float: left; margin-left: 28px;
                                    margin-bottom: 18px;   margin-top: 12px;  width: 25%;" />
                                    <br />
                                <table>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Label ID="lblErrorTarjetaPOL" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label140" runat="server" Text="Tarjeta (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label151" runat="server" Text="Número de tarjeta (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label190" runat="server" Text="Código de seguridad (*)"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlFranquiciaPOL" class="listasTC" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumTarjetaPOL" class="CajasTC" onkeypress="ValidaSoloNumeros()" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCodSeguridadPOL" class="CajasTC" onkeypress="ValidaSoloNumeros()" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label210" runat="server" Text="Nombre completo del tarjetahabiente (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label220" runat="server" Text="Cédula del titular de la tarjeta (*)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTitularPOL" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtIdentificacionPOL" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label180" runat="server" Text="Vence (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label200" runat="server" Text="Número de cuotas (*)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlMesVencimientoPOL" class="listasTC" runat="server">
                                                <%--<asp:ListItem Text="Enero" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="Febrero" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="Marzo" Value="03"></asp:ListItem>
                                                <asp:ListItem Text="Abril" Value="04"></asp:ListItem>
                                                <asp:ListItem Text="Mayo" Value="05"></asp:ListItem>
                                                <asp:ListItem Text="Junio" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="Julio" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="Agosto" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="Septiembre" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                            &nbsp;/&nbsp;
                                            <asp:DropDownList ID="txtAnioVencimientoPOL" class="listasTC" runat="server">
                                                <%--<asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                                <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCuotasPOL" class="CajasTC" runat="server" onkeypress="ValidaSoloNumeros()"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label260" runat="server" Text="Teléfono horas laborales (*)"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label380" runat="server" Text="Otro teléfono (*)"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoOficinaPOL" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTelefonoOtroPOL" class="CajasTC" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        <!-- Otros -->
                        <div style="float: left; width: 530px; padding: 25px; position: relative; display: none;"
                            id="DivPSE" runat="server">
                            <%--<asp:Label ID="lblTextoFormaPago" runat="server" Text=""></asp:Label>--%>
                            <p>
                                &nbsp;</p>
                            <div>
                                <p>
                                    Esta opci&oacute;n te permitir&aacute; el pago con d&eacute;bito a una cuenta bancaria
                                    (ahorros o corriente), para hacerlo debes tener habilitado en tu cuenta la opci&oacute;n
                                    de hacer pagos por internet.</p>
                            </div>
                            <p>
                                &nbsp;</p>
                        </div>
                        <div style="float: left; width: 530px; padding: 25px; position: relative; display: none"
                            id="DivEfe" runat="server">
                            <%--<asp:Label ID="Label16" runat="server" Text=""></asp:Label>--%>
                            <p>
                                &nbsp;</p>
                            <div>
                                <p>
                                    &nbsp;</p>
                                <div>
                                    <p>
                                        Puede realizar su pago en la oficina de Turinco ubicada en la Carrera 11 No.
                                        86-32 Oficina 401. en Bogot&aacute; - Colombia. Tel 2568888 o consignar en las siguientes
                                        cuentas bancarias:</p>

                                    <ul style="margin-left:67px;">
                                        <li><strong>Pago en Pesos Colombianos</strong></li>
										<li>Cuenta Corriente Bancolombia No 039 823 05525</li>
                                        <li>Cuenta Corriente Davivienda No 008 969 994 816</li>
                                        <li>Cuenta Corriente BBVA No 627 039 597</li>
                                        <li><strong>Pago en Dólares Americanos</strong></li>
                                        <li>Cuenta Corriente en dólares Corbanca No 040 025 710, por favor tener encuenta que al momento de consignar tambien se debe registrar la cuenta en pesos No. 040 023 889</li>
                                        <li><strong>Para pagar en pesos servicio reservado en dólares, antes de realizar el pago se debe consultar con Turinco la tasa de cambio a dólares que se debe utilizar</strong></li>
                                    <p style="text-align: justify;">
                                        &nbsp;</p>
                                    <p>
                                       Una vez realizada la consignación por favor enviarla por correo electrónico reservas@turinco.co</p>
                                    <p>
                                        &nbsp;</p>
                                    <p>
                                        Por favor tenga en cuenta que solamente se emitirán los tiquetes y vouchers de servicio cuando se hay recibido el pago total de la reserva.
                                    </p>
                                </div>
                                <p>
                                    &nbsp;</p>
                            </div>
                            <p>
                                &nbsp;</p>
                        </div>
                        <div style="float: left; width: 530px; padding: 25px; position: relative; display: none;"
                            id="DivContAsesor" runat="server">
                            <%--<asp:Label ID="lblTextoFormaPago" runat="server" Text=""></asp:Label>--%>
                            <p>
                                &nbsp;</p>
                            <div>
                                <p>
                                    Al elegir esta opción, tu reserva quedará solicitada, nuestros asesores confirmaran
                                    los cupos y te contactarán para definir la forma de pago
                                </p>
                            </div>
                            <p>
                                &nbsp;</p>
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
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </ContentTemplate>
            </ajax:UpdatePanel>

            <div class="contenedorCondicionesReserva iblock full">
                <img src="../App_Themes/Imagenes/warningCondiciones.jpg">

                <a href="#this" runat="server" onclick="return false" id="ltrainfo" class="linkCondiciones" style="text-decoration: underline; color: blue; margin-left: 18px;">
                    Condiciones de reserva, pago y cancelación de servicios
                </a>

                <a href="#this" runat="server" onclick="return false" id="lCondiciones" class="politicas" style="display:none;">
                    Condiciones de la reserva y tarifa
                </a>

                <ajax:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="contenedorCheck full iblock">
                            <asp:CheckBox CssClass="labelCondicionesVuelosTarjeta" ID="cbAcepto" runat="server" Text="" />
                            <asp:Label runat="server" ID="Label11" CssClass="labelAceptoVuelos" Text="Confirmo datos de los pasajeros y acepto condiciones"></asp:Label>
                        </div>
                        
                        <div class="full iblock botonesReserva">
                            <div class="contenedorbotonPlan">
                            <asp:Button CssClass="link-button florig" ID="btnPagar" runat="server" EnableTheming="True" OnClientClick="return popUp(1);" ValidationGroup="grupoValidacion" CommandName="Confirmar" OnCommand="btnReserva_Command" Text="Reservar" />
                            </div>
                            <asp:Button CssClass="btnConfirmar" ID="btnGuardar" runat="server" EnableTheming="True" OnClientClick="popUp();" ValidationGroup="grupoValidacion" CommandName="Confirmar" Text="GUARDAR RESERVA MIENTRAS REALIZO EL PAGO" OnCommand="btnReserva_Command" Visible="false"/>


                            <asp:Button ID="btnCancelar" runat="server" CssClass="LinkCancelar" Text="VOLVER A BUSCAR" CommandName="Cancelar" OnCommand="btnReserva_Command"/>
                        </div>     
                        
                        <asp:Button ID="btnCondiciones" Visible="false" runat="server" CssClass="botonCondiciones" Text="Condiciones generales"></asp:Button>
                        
                        <div class="mensajeError" id="dPanel" runat="server">
                        </div>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="panelRight iblock">
        <asp:Repeater ID="rptItinerario" runat="server">
            <ItemTemplate>
                <div class="tituloReserva blanco arial tituloCorto" style="margin-left: 0; border-bottom: 1px solid;" >
                    <asp:Label ID="Label7" runat="server" Text="Informacion de Vuelos"></asp:Label>
                </div>


                <div class="full iblock detalleVueloReserva azulOscuro" style="width:80%;">
                    <asp:Repeater ID="rptDetalle" runat="server">
                        <ItemTemplate>
                            <div class="full iblock renglonDetalleVuelosReserva renglonDetalleVuelosReservaLinea1">
                                <asp:Label ID="Label25" CssClass="fLeft bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_Salida")%>'></asp:Label>
                                <asp:Image ID="Image1" CssClass="fRight" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "urlImagenAerolinea")%>' />
                            </div>

                            <div class="full iblock renglonDetalleVuelosReserva">
                                <asp:Label ID="Label27" CssClass="fLeft bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCiudad_LLegada")%>'></asp:Label>
                            </div>
                            <div class="full iblock renglonDetalleVuelosReserva">
                                <div>
                                        <asp:Label ID="Label30" CssClass="bold iblock" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                    
                                        <asp:Label ID="Label35" CssClass="bold iblock" runat="server" Text='<%# Ssoft.Utils.clsValidaciones.ConverYMDtoDMMY(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("yyyy/MM/dd"), "-")%>'></asp:Label>
                                </div>
                            </div>
                            <div class="full iblock renglonDetalleVuelosReserva">
                                salida
                                <asp:Label CssClass="iblock" ID="Label28" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaSalida")).ToString("HH:mm:ss")%>'></asp:Label>
                                &nbsp;|&nbsp;
                                llegada:
                                <asp:Label CssClass="iblock" ID="Label32" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dtmFechaLlegada")).ToString("HH:mm:ss")%>'></asp:Label>
                            </div>

                            <div class="full iblock renglonDetalleVuelosReserva">
                                <div class="fLeft">
                                    <asp:Label ID="Label37" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ElapsedTime")%>'></asp:Label>
                                    min
                                </div>
                            </div>
                            <div class="full iblock renglonDetalleVuelosReserva">
                                <asp:Label ID="Label36" ToolTip='<%# DataBinder.Eval(Container.DataItem, "strDescripcionParadas")%>' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strParadas")%>'></asp:Label>
                            </div>
                            <div class="noneDisplay">
                                <div class="renglonTrayectoVueloTarjeta">
                                    <div class="divi195">
                                        <div class="labelItemsVueloTarjeta">
                                            Aerolinea:&nbsp;&nbsp;
                                        </div>
                                        <asp:Label CssClass="valorItemsVueloTarjeta" ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strNombre_Aerolinea")%>'></asp:Label>
                                    </div>
                                    <div class="divi195">
                                        <div class="labelItemsVueloTarjeta">
                                            Vuelo:&nbsp;&nbsp;
                                        </div>
                                        <asp:Label CssClass="valorItemsVueloTarjeta" ID="Label78" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlightNumber")%>'></asp:Label>
                                    </div>
                                    <div class="divi195">
                                        <asp:Label CssClass="labelItemsVueloTarjeta" ID="Label2" runat="server" Text="Paradas: &nbsp;&nbsp;"></asp:Label>
                                        
                                    </div>
                                    <div class="divi195">
                                        <asp:Label CssClass="labelItemsVueloTarjeta" ID="Label5" runat="server" Text="Tiempo de vuelo: &nbsp;&nbsp;"></asp:Label>
                                        
                                    </div>
                                </div>
                            </div>
                            <div class="divisorAzul300">
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>


                <div>
                    <div class="detalleTarifaVuelosTarjeta">
                        <asp:Repeater runat="server" ID="RptTiposPasajeros">
                            <ItemTemplate>
                                <asp:Repeater ID="RptTarifas" runat="server">
                                    <ItemTemplate>
                                        <div class="renglonValorPorPasajero">
                                            <div style="float: left; width: 160px; text-align: left;">
                                                <span style="float: left;margin-left: 17px; color: #28ABE3">Valor por un &nbsp; </span>
                                                <asp:Label ID="Label55" runat="server" Style="float: left; color: #28ABE3;width: 27%;">
                                                    <%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%>&nbsp;
                                                </asp:Label>
                                            </div>
                                            <a class="impuestosVuelos" href="#this" style="float: left; padding: 0; width: auto; color: #28ABE3; margin-bottom: 11px;">
                                                <span class="valorTarifaVuelosTarjeta" style="float: left; text-align: left; width: 85px;">
                                                    <asp:Label ID="Label3" runat="server">
                                                                <%# DataBinder.Eval(Container.DataItem, "strTipoMonedaTotalFare")%>&nbsp;<%#Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "IntTotalTarifaConTaXPersona")).ToString("###,###.##")%>
                                                    </asp:Label>
                                                </span>
                                                <div class="GloboImp" style="padding: 0px 0 0 10px;">
                                                    <span style="color: #034185; float: left; text-align: center;">Detalle de tarifas, impuestos
                                                        y cargos administrativos por
                                                        <%# DataBinder.Eval(Container.DataItem, "strTipoPasajero")%>
                                                    </span><span class="labelDetalleImpuestos">Tarifa </span>
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
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>


                <div class="tarifasVuelo">
                    <div class="valorTotalVuelo grisOScuro full">
                        <asp:Label ID="lblTValorTotal" runat="server" Text="Valor total a pagar" CssClass="tituloTotal grisOscuro"></asp:Label>
                    
                        <div class="iblock full textCenter">
                            <span class="monedaTotal grisOscuro">
                                <%#DataBinder.Eval(Container.DataItem, "str_Tipo_Moneda")%>
                            </span>

                            <span class="valorTotal grisOscuro">
                                <%#Convert.ToDecimal( DataBinder.Eval(Container.DataItem, "IntTotalPesos")).ToString("###,###.##")%>
                            </span>
                        </div>

                        <span class="labelImpuestosIncluidos grisOscuro full iblock textCenter">Impuestos incluidos </span>
                    
                    </div>
                    <asp:Label ID="lblPrecioTotal" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "IntTotalPesos") %>'></asp:Label>
                </div>

                <p>
                    <asp:Label ID="tituloImpuestoVuelo" CssClass="reservaTarifasNaranja" runat="server"
                        Text='<%# DataBinder.Eval(Container.DataItem, "strTextoDG")%>'></asp:Label>
                </p>
            </ItemTemplate>
        </asp:Repeater>

        <ajax:ModalPopupExtender ID="MPERecord" BackgroundCssClass="ui-widget-shadow" runat="server"
            TargetControlID="dummyLinkMPARecord" BehaviorID="MPERecord" OnOkScript="" OkControlID=""
            PopupControlID="UpdatePanelrecord" />
        <ajax:UpdatePanel ID="UpdatePanelrecord" runat="server" style="text-align: center;
            background: #fff; width: 280px; height: 100px; padding: 20px; color: #000;">
            <ContentTemplate>
                <asp:Label ID="lblUrlRedireccion" runat="server" Visible="false"></asp:Label><asp:Label
                    ID="lblTextoConfirm" runat="server"></asp:Label>&nbsp;<br />
                <asp:Label ID="lblRecord" runat="server" ForeColor="Red" Style="font-size: 20px;
                    padding: 5px; text-align: center; padding-bottom: 10px;"></asp:Label><br />
                <br />
                <br></br>
                <asp:Button ID="btnContinuar" runat="server" Text="Finalizar" class="botonBuscador"
                    Style="padding: 5px; margin: 20px;" OnClick="btnContinuar_Click" />
            </ContentTemplate>
        </ajax:UpdatePanel>
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
<ajax:ModalPopupExtender ID="MPECondiciones" BackgroundCssClass="ui-widget-shadow"
    runat="server" TargetControlID="lCondiciones" BehaviorID="MPECondiciones" OnOkScript=""
    OkControlID="btnCerrarCondiciones" PopupControlID="pnCondiciones" />
<asp:Panel runat="server" ID="pnCondiciones">
    <div class="contenedorPopUpCondiciones">
        <asp:Button ID="btnCerrarCondiciones" CssClass="btnCerrar azulClaro" Text="X" runat="server" />

        <div class="condiciones">
            <div class="tituloCondiciones">
                VER CONDICIONES DE RESERVA Y TARIFA
                <%--<  <asp:LinkButton ID="hlkCondiciones" runat="server" Text="VER CONDICIONES DE RESERVA Y TARIFA" onclick="hlkCondiciones_Click"/> --%>
            </div>
            <div class="detalleVuelo3">
                <div>
                    <%--  <asp:Panel ID="pnlcondiciones" runat="server" Visible="false">--%>
                    <asp:Label runat="server" ID="lblTPlazoLimite" Text="Plazo limite de emisión"></asp:Label><br />
                    <asp:Label runat="server" ID="lblFechaLimiteTiqueteo" ForeColor="#186e9b" Text=""></asp:Label>
                    <asp:Label ID="lblCondiciones" runat="server"></asp:Label>
                    <asp:Label ID="lblcondicionesEspecificas" runat="server"></asp:Label>
                    <asp:Label ID="lblHoraCondicion" runat="server"></asp:Label>
                    <br />
                    Las aerolíneas pueden modificar, sin previo aviso, sus tarifas y condiciones, por
                    esta razón las tarifas aéreas solo se garantizan con la emisión del tiquete. Las
                    aerolíneas pueden modificar, sin previo aviso, sus convenios con otras líneas aéreas
                    permitiendo o no la emisión de tiquetes. Una vez expedido el tiquete aéreo no se
                    puede reasignar a otra persona, consulte las excepciones en las condiciones específicas
                    de la tarifa. Esta tarifa no es reembolsable, consulte las excepciones en las condiciones
                    específicas de la tarifa. Este precio incluye una tarifa administrativa no reembolsable.
                    Las tarifas de tiquetes internacionales se liquidan a la tasa IATA vigente a la
                    fecha de expedición del tiquete aéreo. Las condiciones específicas de esta tarifa
                    se muestran a continuación tal y como son publicadas por la aerolínea en idioma
                    inglés.
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
                    <%--  </asp:Panel>     --%>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<ajax:ModalPopupExtender ID="MPEInfo" BackgroundCssClass="ui-widget-shadow" runat="server"
    TargetControlID="ltrainfo" BehaviorID="MPEInfo" OnOkScript="" OkControlID="Button2"
    PopupControlID="Info" />
<asp:Panel runat="server" ID="Info">
    <div class="contenedorPopUpCondiciones">
        <asp:Button ID="Button2" CssClass="btnCerrar azulClaro" Text="X" runat="server" />
        
        <div class="condiciones">
            <div class="tituloCondiciones" style="font-weight:bold;">
                Condiciones de la reserva y tarifa
            </div>
            <div class="detalleVuelo3">
                <div class="condicionesProteccion">
                    <div class="subtituloProteccion">                       
                    </div>
                    <br />
                    <div class="contenidoProteccion" style="margin-bottom: 20px;">
                      <ul type="disk">
                          <li>Las aerol&iacute;neas pueden modificar, sin previo aviso, sus tarifas y condiciones,
                              por esta raz&oacute;n las tarifas a&eacute;reas solo se garantizan con la emisi&oacute;n
                              del tiquete.</li><br />
                          <li>Esta reserva tiene un plazo límite de emisón, si este plazo no se cumple la reserva será anulada automáticamente.</li>
                          <li>Las aerolíneas pueden modificar, sin previo aviso, sus convenios con otras líneas aéreas permitiendo o no la emisión de tiquetes, si esto llegara a ocurrir con su reserva, uno de nuestros asesores lo contactará para ofrecerle otra alternativa.
                          </li>
                          <li>Una vez emitido el tiquete aéreo aplican las políticas de cambios y cancelaciones de la aerolínea emisora, consulte las condiciones específicas de la tarifa.  </li>
                          <li>Esta tarifa no es reembolsable, no permite cambios, consulte con un asesor las excepciones en las condiciones específicas de la tarifa. </li>
                          <li>Este precio incluye una tarifa administrativa no reembolsable. </li>
                          <li>Las tarifas de tiquetes internacionales se liquidan a la tasa IATA vigente a la
                              fecha de emisión del tiquete a&eacute;reo. </li>
                          <li><span style="font-weight: bold;"><strong>La tarifa reservada solo se garantiza con
                              la emisi&oacute;n del tiquete una vez sea recibido el pago total.</strong></span></li>
                      </ul>
                    </div>   
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLinkMPAfiliados"
    runat="server">dummy</a> <a href="#" style="display: none; visibility: hidden;" onclick="return false"
        id="dummyLinkMPARecord" runat="server">dummy</a> 