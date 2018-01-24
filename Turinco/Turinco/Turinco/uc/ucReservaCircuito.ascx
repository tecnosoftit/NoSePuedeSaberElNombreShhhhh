<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucReservaCircuito.ascx.cs" Inherits="uc_ucReservaCircuito" %>
<%@ Register Src="~/uc/ucRegistro.ascx" TagName="ucRegistro" TagPrefix="uc2" %>

<div class="iblock full reservaPaquetes">
    <div class="panelLeft iblock">
        <div class="borde full iblock">
        </div>
        <div class="tituloReserva verdeOscuro arial" style="margin-top:25px;">
            <asp:Label ID="Label4" runat="server" Text="Datos del usuario"></asp:Label>
        </div>

        <div class="datosViajero iblock full datosViajeroContacto">
            <uc2:ucRegistro ID="ucRegistro" runat="server" />
        </div>
        <div class="Separador_Reserva"></div>
        <asp:Repeater ID="rptCabinas" runat="server">
            <ItemTemplate>
                <div class="noneDisplay">
                    <div>
                        INFORMACIÓN DE HABITACIÖN
                    </div>
                    <div>
                        <div>
                            <asp:Label ID="lblTCabina" runat="server" Text="Tipo de habitación:"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTipoHabitacion") %>'></asp:Label>
                            <asp:Label ID="lblConsecRes" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intConsecRes") %>'></asp:Label>
                            <asp:Label ID="lblSegmento" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intSegmento") %>'></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="lblTTipo" runat="server" Text="Acomodación:"></asp:Label>
                            <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoAcomodacion") %>'></asp:Label>
                        </div>
                    </div>
                    <div>
                        <div>
                            <asp:Label ID="lblTValor" runat="server" Text="Valor"></asp:Label>                                
                            <span>
                                <asp:Label ID="lblTMoneda" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTipoMoneda") %>'></asp:Label>
                                <asp:Label ID="Label3" runat="server" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container,"DataItem.dblValor")).ToString("###,###,###") %>'></asp:Label>
                            </span>
                        </div>
                        <div>
                            <asp:Label ID="Label5" runat="server" Text="Hotel:"></asp:Label>
                            <asp:Label ID="lblHotelCabina" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrProveedor") %>'></asp:Label>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="Label2" runat="server" Text="Cantidad de viajeros:" style="width:135px;"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strCantidadPersonas") %>'></asp:Label>
                    </div>

                    <div>
                        <asp:Label ID="lblTPorFavor" runat="server" Text="Por favor ingresa la información de los pasajeros que se hospedaran en esta habitación. Asegúrate de que los datos sean los mismos que están registrados en el documento de identificación"></asp:Label>
                    </div>  
                </div>                          
                <div class="tituloReserva blanco arial" style="padding-top:18px;">
                     <asp:Label ID="Label7" runat="server" Text=" Datos de los pasajeros"></asp:Label>
                </div>            
                <asp:Repeater ID="rptPasajeros" runat="server">
                    <ItemTemplate>
                        <div class="datosViajero iblock full">
                            
                            <div class="renglonDatos iblock full">
                                <div class="tipoPasajero">
                                    <asp:Label ID="lblTTipoPasajero" runat="server" CssClass="bold" Text="Tipo de viajero" Style="display: none;"></asp:Label>
                                    <asp:Label ID="lblTipoPax" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTipoPax") %>' CssClass="labelTipoPasajeroVuelosTarjeta"></asp:Label>
                                </div>

                                <div class="datosObligatorios">
                                    datos obligatorios (*)
                                </div>
                            </div>

                            <div class="renglonDatos iblock full">
                                <div class="celdaDatos iblock">
                                    <asp:Label ID="lblTNombres" runat="server" CssClass="label iblock full" Text="Nombres (*)"></asp:Label>
                                    <asp:TextBox ID="txtNombre" runat="server" Text="" CssClass="campo full iblock" Enabled="true" placeholder="Como figura en el documento"></asp:TextBox>
                                </div>

                                <div class="celdaDatos iblock">
                                    <asp:Label ID="lbltApellidos" runat="server" CssClass="label iblock full" Text="Apellidos (*)"></asp:Label>
                                    <asp:TextBox ID="txtApellido" runat="server" Text="" CssClass="campo full iblock" Enabled="true" placeholder="Como figura en el documento"></asp:TextBox>
                                </div>

                                <div class="celdaDatos iblock">
                                    <asp:Label ID="lblTFechaNacimiento" runat="server" CssClass="label iblock full" Text="Fecha de nacimiento"></asp:Label>
                                    <asp:TextBox ID="txtNacimiento" runat="server" Text="" CssClass="campo full iblock" Enabled="true" placeholder="Como figura en el documento"></asp:TextBox>
                                </div>                            
                            </div>

                            <div class="renglonDatos iblock full">
                                <div class="celdaDatos iblock">
                                    <asp:Label ID="lblTGenero" runat="server" CssClass="label iblock full" Text="Genero (*)"></asp:Label>
                                    <asp:DropDownList ID="ddlGenero" runat="server" CssClass="campo full iblock">
                                    </asp:DropDownList>
                                </div>

                                <div class="celdaDatos iblock">
                                    <asp:Label ID="lblTTipoDoc" runat="server" CssClass="label iblock full" Text="Tipo identificación"></asp:Label>
                                    <asp:DropDownList ID="ddlTipoIdent" runat="server" CssClass="campo full iblock" >
                                    </asp:DropDownList>
                                </div>

                                <div class="celdaDatos iblock">
                                    <asp:Label ID="lblTPasaporte" runat="server" CssClass="label iblock full" Text="Identificación"></asp:Label>
                                    <asp:TextBox ID="txtPasaporte" runat="server" Text="" CssClass="campo full iblock" Enabled="true" placeholder="Numero identificacion"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="noneDisplay">
                            <div>
                                <asp:Button Text="Seleccionar pasajero" ID="btnSeleccionPasajeros" OnClick="btnSeleccionPasajeros_Click" runat="server" />
                            </div>                                            
                            <div>
                                <asp:Label ID="lblTMoneda" runat="server" Text="Moneda"></asp:Label>
                                <asp:Label ID="lblMoneda" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTipoMoneda") %>'></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblTTarifa" runat="server" Text="Tarifa"></asp:Label>
                                <asp:Label ID="lblTarifa" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strTarifa") %>'></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblTValor" runat="server" Text="Valor con impuestos"></asp:Label>
                                <a href="#this">
                                    <asp:Label ID="lblValor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strValor") %>'></asp:Label>
                                    <div>
                                        <%# DataBinder.Eval(Container, "DataItem.strHtmlImpuestos")%>
                                    </div>
                                </a>
                                <asp:Label ID="lblidPax" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intCodigoPax") %>' Visible="false"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <div class="noneDisplay">
                    <asp:Label ID="lblTObservaciones" runat="server" Text="Requerimientos especiales: "></asp:Label>
                    <asp:TextBox ID="txtObservaciones" runat="server"></asp:TextBox>
                    <asp:Label ID="lblidPax" runat="server" Text="Los campos marcados con (*) son obligatorios"></asp:Label>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div class="Separador_Reserva"></div>

        <div class="tituloReserva verdeOscuro arial" style="padding-top: 18px;">
            Formas de pago
        </div>

        <div class="datosViajero iblock full datosViajeroContacto">
            <div id="divFormaspago" runat="server" class="resumenReserva">
                <ajax:UpdatePanel ID="upFormasPago" runat="server">
                    <ContentTemplate>
                        <div class="tipoPagoVuelosTarjeta">
                            <asp:RadioButtonList ID="rblFormasPago"  runat="server" Style="width: 550px; float: left;" AutoPostBack="true" OnSelectedIndexChanged="rblFormasPago_SelectedIndexChanged" >
                            </asp:RadioButtonList>
                        </div>

                        <!-- Tarjeta de Credito -->
                        <div class="tipoTarjeta" id="DivTC" style="display: none" runat="server">

                            <img src="../App_Themes/Imagenes/LogosTarjetaCredito.png" style="float: left; margin-left: 15px; margin-bottom: 10px;">

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
                        <div style="float: left; width: 530px; padding: 25px; position: relative; display: none;" id="DivPSE"  runat="server">
                            <p>
                                Esta opci&oacute;n le permitir&aacute; el pago con d&eacute;bito a una cuenta bancaria
                                    (ahorros o corriente), para hacerlo debe tener habilitado en su cuenta las claves para
                                    hacer pagos por internet.</p>
                            </p>
                        </div>
                        <div style="float: left; width: 530px; padding: 0px 20px; position: relative; " id="DivEfe"  runat="server">
                            <p>
                                &nbsp;
                            </p>
                            <div>
                                <p>
                                    &nbsp;
                                </p>
                                <div>
                                    <p>
                                        Puede realizar su pago en la oficina de Turinco ubicada en la Carrera 11 No.
                                        86-32 Oficina 401. en Bogot&aacute; - Colombia. Tel 2568888 o consignar en las siguientes
                                        cuentas bancarias:
                                    </p>
                                    <br />
                                    <ul style="margin-left:67px;">
                                        <li><strong>Pago en Pesos Colombianos</strong></li>
										<li>Cuenta Corriente Bancolombia No 039 823 05525</li>
                                        <li>Cuenta Corriente Davivienda No 008 969 994 816</li>
                                        <li>Cuenta Corriente BBVA No 627 039 597</li>
                                        <li><strong>Pago en Dólares Americanos</strong></li>
                                        <li>Cuenta Corriente en dólares Corbanca No 040 025 710, por favor tener encuenta que al momento de consignar tambien se debe registrar la cuenta en pesos No. 040 023 889</li>
                                        <li><strong>Para pagar en pesos servicio reservado en dólares, antes de realizar el pago se debe consultar con Turinco la tasa de cambio a dólares que se debe utilizar</strong></li>
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
                                        Por favor tenga en cuenta que solamente se emitirán los tiquetes y vouchers de servicio cuando se hay recibido el pago total de la reserva.
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
                         <div style="float: left; width: 530px; padding: 25px; position: relative; display: none;"
                                id="DivContAsesor"  runat="server">
                                <%--<asp:Label ID="lblTextoFormaPago" runat="server" Text=""></asp:Label>--%>
                                <div>
                                    <p>
                                        Al elegir esta opción, su reserva quedará solicitada, nuestros asesores confirmarán
                                    los cupos y le contactarán para definir la forma de pago.
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
            <ajax:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="mensajeError" id="dPanel" runat="server">
                    </div>
                </ContentTemplate>
            </ajax:UpdatePanel>

            <div class="contenedorCondicionesReserva iblock full">
                <img src="../App_Themes/Imagenes/warningCondiciones.jpg">

                <a href="#this" runat="server" onclick="return false" id="ltrainfo" class="politicas" style="text-decoration: underline; color: blue; margin-left: 18px;" >
                    <asp:Label ID="lblTCondiciones" runat="server" Text="Condiciones de reserva, pago y cancelación de servicios" CssClass="linkCondiciones"></asp:Label>
                </a>

                <div class="contenedorCheck full iblock">
                      <asp:CheckBox CssClass="labelCondicionesVuelosTarjeta" ID="cbAcepto" runat="server" Text="" />
                            <asp:Label runat="server" ID="Label11" CssClass="labelAceptoVuelos" Text="Confirmo datos de los pasajeros y acepto condiciones"></asp:Label>
                </div>                
            </div>

            <div class="full iblock botonesReserva">
                <div class="contenedorbotonPlan">
                <asp:Button ID="btnConfirmar" CssClass="link-button florig" runat="server" Text="Reservar" OnClientClick="Show_Cortinilla();" OnClick="btnReservar_Click" />
                </div>

                <asp:Button ID="btnCancelar" CssClass="LinkCancelar" runat="server" Text="VOLVER A BUSCAR" OnClick="btnCancelar_Click"/>
                

                <!-- Condiciones -->
                <ajax:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>
        </div>      
    </div>

    <div class="panelRight iblock">
        <div class="tituloReserva verdeOscuro arial tituloCorto" style="margin-left: 0; border-bottom: 1px solid;">
            Información del plan
        </div>

        <div class="detallePlanReserva full iblock">
            <asp:Label CssClass="bold" ID="lblConsecRes" runat="server" Text="" Visible="false"></asp:Label>

            <asp:Repeater ID="rptCircuitos" runat="server">
                <ItemTemplate>
                    <div class="nombrePaquete azulClaro full iblock">
                        <asp:Label ID="Label28" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strNombrePlan") %>'></asp:Label>
                    </div>

                    <div class="descripcionPaquete azulOscuro textJustify">
                        <asp:Label ID="Label32" runat="server" Text='<%#System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strDescripcion").ToString()) %>'></asp:Label>
                    </div>

                    <div class="Contenedor_DetallePlanFormulario">                        
                        <div>
                            <asp:Label ID="lblTCiudad" runat="server" Text="Ciudad: "></asp:Label>
                            <asp:Label ID="Label30" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrCiudad") %>'></asp:Label>
                        </div>

                        <div>
                            <asp:Label ID="Label6" runat="server" Text="Hotel(es): "></asp:Label>
                            <asp:Label ID="lblHoteles" runat="server" Text=""></asp:Label>
                        </div>
                        
                        <div>
                            <asp:Label ID="lblTDuracion" runat="server" Text="Duración: "></asp:Label>
                            <asp:Label ID="Label52" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDuracion") %>'></asp:Label>
                        </div>

                        <div>
                            <asp:Label ID="Label12" runat="server" Text="Cantidad de viajeros: "></asp:Label>
                            <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strPasajeros") %>'></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="lblTCategoria" runat="server" Text="Categoria del hotel: "></asp:Label>
                            <asp:Label ID="Label55" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrCategoria") %>'></asp:Label>
                        </div>
                        
                        <div>
                            <asp:Label ID="Label8" runat="server" Text="Tipo de habitación: "></asp:Label>
                            <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoHabitacion") %>'></asp:Label>
                        </div>
                        
                        <div>
                            <asp:Label ID="Label10" runat="server" Text="Acomodación: "></asp:Label>
                            <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrAcomodacion") %>'></asp:Label>
                        </div>

                        <div>
                            <asp:Label ID="lblTFechaInicio" runat="server" Text="Fecha Inicio: "></asp:Label>
                            <asp:Label ID="Label36" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrFechaInicial") %>'></asp:Label>
                        </div>

                        <div>
                            <asp:Label ID="lblPosicion" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PosTablaPrincipal") %>'
                                Visible="False"></asp:Label>
                            <asp:Label ID="Label56" runat="server" Text="Noches adicionales: "></asp:Label>
                            <asp:Label ID="Label57" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IntNochesAdicionales") %>'></asp:Label>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div class="ContenedorLimitePago">
            <asp:Label ID="lblTLimitePago" runat="server" Text="PLAZO LÍMITE DE PAGO: "></asp:Label>
            <asp:Label ID="lblLimitePago" runat="server" Text=""></asp:Label>
        </div>


        <div class="preciosReserva full iblock grisOscuro textCenter">
            <asp:Label ID="lblTTotal" runat="server" CssClass="full iblock label Valorpaquete" Text="VALOR PAQUETE"></asp:Label> 
                         
            <div class="precioPlan">            
                <asp:Label ID="lblTotalCarrito" runat="server" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container,"DataItem.IntValorTotal")).ToString("###,###,###") %>'></asp:Label>
                &nbsp;                        
                <asp:Label ID="lblMonedaTotal" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.StrTipoMoneda") %>'></asp:Label>
            </div>
            <span class="full iblock label" style="margin-left: 80px;">
                Incluye Impuestos
            </span>
        </div>
    </div>
</div>

<ajax:ModalPopupExtender ID="mdPCondicionesplanes" runat="server" BackgroundCssClass="ui-widget-shadow" TargetControlID="ltrainfo" PopupControlID="updCondicionesplanes" BehaviorID="mdPCondicionesplanes" CancelControlID="btnCerrarT" />
<ajax:UpdatePanel ID="updCondicionesplanes" runat="server">
    <ContentTemplate>
        <div class="contenedorPopUpCondiciones">
            <asp:Button ID="btnCerrarT" CssClass="btnCerrar azulClaro" Text="X" runat="server" OnClientClick='javascript:ClosePopUp();' />
        
            <div class="condiciones">
                <iframe src="../Presentacion/condiciones.aspx?idSession?<%=Request.QueryString["idSesion"] %>&Codigo=<%=Request.QueryString["Codigo"] %>&TipoPlan=<%=Request.QueryString["TipoPlan"] %>" style="width: 100%; height: 200px; border:0;"></iframe>
            </div>
        </div>
    </ContentTemplate>
</ajax:UpdatePanel>


            <!-- Valor Total -->
            <asp:Label ID="lblTReservaCrucero" runat="server" Text="BIENVENIDA &raquo;" Style="display: none;"></asp:Label>
            <div style="float: left; position: relative; font-size: 16px; font-weight: bold; color: #186e9b; display: none;">
                <asp:Label ID="lblTApreciado" runat="server" Text="Apreciado(a) "></asp:Label>
                <asp:Label ID="lblNombreUsuario" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblTDatos" runat="server" Text=", por favor completa tu reserva con los datos a continuación solicitados:"></asp:Label>
            </div>
            <div style="width: 100%; font-size: 11px; text-align: right; padding-top: 10px; border-top: 1px solid #000; margin-top: 20px; display:none;">
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
    <div style="text-align: center; background: #fff; width: 280px; height: 100px; padding: 20px; color: #000;">
        <ajax:UpdatePanel ID="upRecord" runat="server" style="text-align: center;">
            <ContentTemplate>
                <asp:Label ID="lblTextoConfirm" runat="server" Text="Tu reserva ha sido confirmada bajo el record:"></asp:Label>
                <br />
                <asp:Label Style="font-size: 20px; padding: 5px; text-align: center; padding-bottom: 10px;" runat="server" ID="lblRecord" ForeColor="red"></asp:Label>
            </ContentTemplate>
        </ajax:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <asp:Button runat="server" ID="btnContinuar" CssClass="link-button florig" Text="Finalizar" OnClick="btnContinuar_Click" Style="padding: 5px;width: 88px;height: 31px; margin-right: 96px;" />         
    </div>
    <div style="display: none;">
        <asp:Button ID="btnCerrar5" CssClass="btnCerrar" runat="server" />
    </div>
    <div class="panelConfirmacion" style="display:none;">
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