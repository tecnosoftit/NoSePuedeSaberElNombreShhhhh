<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRegistroT.ascx.cs" Inherits="uc_ucRegistro" %>
<!--- registro -->
<ajax:UpdatePanel ID="upCrear" runat="server">
    <ContentTemplate>
        <div style="float: right; position: relative; width: 38%; display: inline-block;
            margin-top: -378px;">
            <div class="panelRegistro">
                <div class="tituloIngresar">
                    <asp:Label ID="lblDescripcion" runat="server" Text="Registre su agencia"></asp:Label>
                </div>
                <br />
                <div class="contenidoIngresar">
                    <!---- Tabla Quedama con los campos que debe quedar el formulario ---->
                    <table>
                        <tr>
                            <td style="width: 100px; text-align: left;">
                                <span class="textoLogin">Razon social</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" Text="(*) "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Rqftxtnombre" runat="server" ValidationGroup="RegistroIndex"
                                    ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                                <ajax:FilteredTextBoxExtender ID="FttxtNombre" runat="server" TargetControlID="txtNombre"
                                    ValidChars=" ,._-" FilterType="Custom,LowercaseLetters,UppercaseLetters">
                                </ajax:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; text-align: left;">
                                <asp:Label ID="lbltextoNit" runat="server" Text="Nit" CssClass="textoLogin" ToolTip="Digite su Nit sin Digito de verificación"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDocumento" runat="server" ToolTip="Digite su Nit sin Digito de verificación"
                                    Text="(*) "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RqftxtDocumento" runat="server" ValidationGroup="RegistroIndex"
                                    ControlToValidate="txtDocumento"></asp:RequiredFieldValidator>
                                <ajax:FilteredTextBoxExtender ID="FttxtDocumento" runat="server" TargetControlID="txtDocumento"
                                    FilterType="Numbers">
                                </ajax:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; text-align: left;">
                                <span class="textoLogin">Dirección</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDireccion" runat="server" Text="(*) "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RqftxtDireccion" runat="server" ValidationGroup="RegistroIndex"
                                    ControlToValidate="txtDireccion"></asp:RequiredFieldValidator>
                                <ajax:FilteredTextBoxExtender ID="FttxtDireccion" runat="server" TargetControlID="txtDireccion"
                                    FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters" ValidChars="#- _">
                                </ajax:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; text-align: left;">
                                <span class="textoLogin">Ciudad</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCiudad" runat="server" Text="(*) "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RqftxtCiudad" runat="server" ValidationGroup="RegistroIndex"
                                    ControlToValidate="txtCiudad"></asp:RequiredFieldValidator>
                                <ajax:FilteredTextBoxExtender ID="FttxtCiudad" runat="server" TargetControlID="txtCiudad"
                                    ValidChars=" " FilterType="Custom,LowercaseLetters,UppercaseLetters">
                                </ajax:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; text-align: left;">
                                <span class="textoLogin">Telefono</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTelefono" runat="server" Text="(*) "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RqftxtTelefono" runat="server" ValidationGroup="RegistroIndex"
                                    ControlToValidate="txtTelefono"></asp:RequiredFieldValidator>
                                <ajax:FilteredTextBoxExtender ID="FttxtTelefono" runat="server" TargetControlID="txtTelefono"
                                    FilterType="Numbers">
                                </ajax:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; text-align: left;">
                                <span class="textoLogin">Contacto</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPersonaContacto" runat="server" Text="(*) "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RqftxtPersonaContacto" runat="server" ValidationGroup="RegistroIndex"
                                    ControlToValidate="txtPersonaContacto"></asp:RequiredFieldValidator>
                                <ajax:FilteredTextBoxExtender ID="FttxtPersonaContacto" runat="server" TargetControlID="txtPersonaContacto"
                                    ValidChars=" " FilterType="Custom,LowercaseLetters,UppercaseLetters">
                                </ajax:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; text-align: left;">
                                <span class="textoLogin">Cargo</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCargo" runat="server" Text="(*) "></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RqftxtCargo" runat="server" ValidationGroup="RegistroIndex"
                                    ControlToValidate="txtCargo"></asp:RequiredFieldValidator>
                                <ajax:FilteredTextBoxExtender ID="FttxtCargo" runat="server" TargetControlID="txtCargo"
                                    ValidChars=" " FilterType="Custom,LowercaseLetters,UppercaseLetters">
                                </ajax:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; text-align: left;">
                                <span class="textoLogin">Email</span>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMailPersonal" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RqftxtMailPersonal" runat="server" ValidationGroup="RegistroIndex"
                                    ControlToValidate="txtMailPersonal" ErrorMessage="Los Campos Marcados con (*) son Obligatorios">(*)</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="RegistroIndex"
                                    ShowSummary="true" ShowMessageBox="true" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="RegistroIndex"
                                    ErrorMessage="Por favor Digite un Correo valido" ControlToValidate="txtMailPersonal"
                                    Text="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <ajax:UpdatePanel ID="upAcepto" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ForeColor="#186e9b" Font-Bold="true" Text="Acepto condiciones de registro."
                                            ID="chkCondicionesRegistro" runat="server" OnCheckedChanged="chkCondicionesRegistro_CheckedChanged"
                                            AutoPostBack="true" />
                                        <!-- Condiciones registro -->
                                            <ajax:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="ui-widget-shadow"
                                                DropShadow="false" runat="server" TargetControlID="dummyCondiciones" Drag="false"
                                                BehaviorID="MPEEEnvioAmigoDesc" OnOkScript="" OkControlID="btnCerrarEnv" EnableViewState="true"
                                                PopupControlID="PanelEnvioDesc" />
                                            <asp:Panel runat="server" ID="PanelEnvioDesc" Style="display: none;">
                                                    <div class="contenedorPopUpCondiciones">
                                                        <div class="condiciones">
                                                            
                                                            <div class="TextoCondiciones">
                                                                Al inscribirme en el portal, confirmo que soy un agente de viajes con Registro Nacional de Turismo y que estoy interesado en comercializar los productos de TURINCO (Turismo Internacional Colombia SAS),
                                                                que estaré atento a enviar los documentos requeridos por concretar esta inscripción y que cumplir con los procedimientos de venta que defina TURINCO.
                                                                <br />
                                                                Acepto que los datos suministrados como son Teléfono, y correo electrónico, sean utilizados por TURINCO para el envío de información comercial.
                                                            </div>
                                                        </div>
                                                        <div class="btnCerrarCondiciones">
                                                            <asp:Button ID="btnCerrarEnv" Text="Aceptar" runat="server" CausesValidation="false" CssClass="link-button florig btnCerrarCondiciones"
                                                                OnClientClick='javascript:ClosePopUp();' />
                                                        </div>
                                                    </div>
                                             </asp:Panel>
                                             <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyCondiciones"
                                                runat="server">dummy</a>
                                    </ContentTemplate>
                                </ajax:UpdatePanel>
                                <div class="contenedorBotonBuscar">
                                    <asp:Button class="link-button white" ID="btncrear" runat="server" ValidationGroup="RegistroIndex"
                                        OnClick="lbCrear_Click" Text="Guardar" Enabled="false" Style="float: right;"></asp:Button>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="alineacionCentro" colspan="2">
                                <ajax:UpdatePanel ID="upError" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblError1" runat="server" ForeColor="Maroon"></asp:Label>
                                    </ContentTemplate>
                                </ajax:UpdatePanel>
                                <asp:Label runat="server" ID="lblDetalle"></asp:Label>
                                <ajax:UpdateProgress ID="udpEsperar" runat="server">
                                    <ProgressTemplate>
                                        <div class="progressbar">
                                        </div>
                                        <img alt="" src="../App_Themes/Imagenes/loading.gif" /><br />
                                        <asp:Label ID="lblEsperar" runat="server" Text="Espere por favor..."></asp:Label>
                                    </ProgressTemplate>
                                </ajax:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <ajax:ModalPopupExtender ID="MPEMensajes" BackgroundCssClass="ui-widget-shadow" DropShadow="false"
            runat="server" TargetControlID="dummyLink" Drag="false" BehaviorID="MPEMensajes"
            OnOkScript="" OkControlID="btnCerrar" EnableViewState="true" PopupControlID="PanelMensajes" />
        <asp:Panel runat="server" ID="PanelMensajes">
            <div class="ventanaError1">
                <asp:Button ID="btnCerrar" CssClass="botonCerrarInscribase" Text="Cerrar" runat="server" Style="width: 12px;" />
                <div class="LabelVentanaError">
                    <asp:Label ID="lblError" runat="server" ForeColor="Maroon"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <%--<asp:Panel runat="server" ID="Info">
            <div class="contenedorPopUpCondiciones">
                <asp:Button ID="Button2" CssClass="btnCerrar azulClaro" Text="X" runat="server" />
                <div class="condiciones">
                    <div class="tituloCondiciones">
                        Condiciones de la reserva y tarifa
                    </div>
                    <div class="detalleVuelo3">
                        <div class="condicionesProteccion">
                            <div class="subtituloProteccion">
                            </div>
                            <div class="contenidoProteccion">
                                TURINCO, realiza actividades estrictamente como intermediario entre los usuarios
                                y las empresas que facilitan los servicios detallados. TURINCO no se responsabiliza
                                por retrasos o irregularidades que pudiesen ocurrir, tanto a las personas como a
                                sus pertenencias.
                            </div>
                            <div class="contenidoProteccion" style="margin-bottom: 20px;">
                                <ul type="disk">
                                    <li>Las aerolíneas se reservan el derecho de modificar, sin previo aviso, sus itinerarios,
                                        tarifas y condiciones. </li>
                                    <li>Las tarifas son garantizadas por las aerolíneas una vez emitido el boleto. </li>
                                    <li>Una vez emitido el boleto, éste no es endosable a otra persona.</li>
                                    <li>Las tarifas emitidas son NO REEMBOLSABLES.</li>
                                    <li>Cualquier cambio en el itinerario, una vez emitido el boleto, puede generar cargos
                                        adicionales. Debe consultarse las condiciones específicas de la aerolínea para cada
                                        tarifa.</li>
                                    <li>Precios publicados pueden variar según condiciones. Aplican restricciones.</li>
                                    <li>TURINCO no será responsable bajo ningún concepto por la pérdida, retraso en salida,
                                        llegada y conexiones de vuelo o cualquier tipo de transporte que se utilice, por
                                        cualquier razón posible; esto incluye: malas condiciones atmosféricas, catástrofes
                                        naturales, decisiones gubernamentales de los países de destino y/o conexión, huelgas
                                        y más sucesos que puedan ocurrir y que están fuera de nuestro control y responsabilidad,
                                        sea cuando el cliente esté utilizando los servicios detallados en el tour o realizando
                                        actividades no programadas en el servicio entregado. TURINCO no se responsabiliza
                                        por incumplimiento alguno de los proveedores finales, ya sea por caso fortuito,
                                        fuerza mayor o inclusive por responsabilidad de estos.</li>
                                    <li>En caso de reclamo o solicitud de reembolso por cualquier causa imputable a cualquier
                                        persona, TURINCO, en su calidad de intermediario, se limitará a cumplir con las
                                        disposiciones y políticas que al respecto tengan las empresas proveedoras finales
                                        de los servicios y estaremos en libertad de fijar un valor adicional por gastos
                                        administrativos, siempre que sea necesario.</li>
                                    <li>Al aceptar/reservar/comprar declaro que conozco y acepto las condiciones generales,
                                        términos de uso y políticas de privacidad.</li>
                                    <li>TURINCO no se responsabiliza por fraudes o robos de tarjetas de créditos, y en todo
                                        momento se reserva el derecho a ejercer las acciones pertinentes para mantener al
                                        sistema indemne de cualquier delito o contravención.</li>
                                    <li>Recomendamos que todos los viajeros contraten su propio seguro de viajes directamente
                                        en su país de origen para cubrir gastos de cancelación, reclamo de equipajes, gastos
                                        médicos u otros. TURINCO no se responsabiliza por gastos incurridos por estos conceptos.</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>--%>
        <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink"
            runat="server">dummy</a>

    </ContentTemplate>
</ajax:UpdatePanel>
