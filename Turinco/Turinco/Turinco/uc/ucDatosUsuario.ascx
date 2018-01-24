<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDatosUsuario.ascx.cs" Inherits="uc_ucDatosUsuario" %>

<table width="100%" cellpadding="3">
    <asp:Repeater ID="rptUsuario" runat="server">
        <ItemTemplate>
            <tr>
                <td style="width: 80px">
                    <strong>
                        <span>
                            Nombre
                        </span>                        
                    </strong>
                </td>

                <td style="width: 146px">
                    <asp:TextBox ID="txtNombre" runat="server" class="cajasMicuenta" Text='<%# DataBinder.Eval(Container.DataItem, "strNombre")%>'></asp:TextBox>
                </td>

                <td style="width: 142px">
                    <strong>
                        <span>
                            Apellidos
                        </span>
                    </strong>
                </td>

                <td style="width: 183px">
                    <asp:TextBox ID="txtApellido" runat="server" class="cajasMicuenta"  Text='<%# DataBinder.Eval(Container.DataItem, "strApellido")%>'></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <strong>
                        Género
                    </strong>
                </td>

                <td>
                    <asp:RadioButtonList ID="rblSexo" runat="server">
                    </asp:RadioButtonList>
                </td>

                <td>
                    <strong>
                        Fecha de nacimiento                            
                    </strong>
                </td>

                <td>
                    <asp:TextBox ID="txtNacimiento" runat="server" class="cajasMicuenta" Text='<%# DataBinder.Eval(Container.DataItem, "dtmFechaNac")%>'></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <strong>
                        Tipo de identificación
                    </strong>
                </td>

                <td>
                    <asp:DropDownList ID="ddlTipoIdent"  runat="server" style="width: 206px; margin-left: 0px;">
                    </asp:DropDownList>
                </td>

                <td>
                    <strong>
                        Identificación
                    </strong>
                </td>

                <td>
                    <asp:TextBox ID="txtIdentificacion" runat="server" class="cajasMicuenta" Text='<%# DataBinder.Eval(Container.DataItem, "strIdentificacion")%>'></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <strong>
                        Teléfono
                    </strong>
                </td>

                <td>
                    <asp:TextBox ID="txtTel" class="cajasMicuenta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strTelefono")%>'></asp:TextBox>
                </td>

                <td>
                    <strong>
                        Celular
                    </strong>
                </td>

                <td>
                    <asp:TextBox ID="txtCel" class="cajasMicuenta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCelular")%>'></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <strong>
                        País
                    </strong>
                </td>

                <td>
                    <asp:DropDownList runat="server" ID="ddlPais" style="width: 206px; margin-left: 0px;" AutoPostBack="true" OnSelectedIndexChanged="ConsultaCiudadesPais">
                    </asp:DropDownList>
                </td>

                <td>
                    <strong>
                        Ciudad
                    </strong>
                </td>

                <td>
                    <asp:DropDownList runat="server" style="width: 206px; margin-left: 0px;" ID="ddlCiudad">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <strong>
                        Dirección
                    </strong>
                </td>

                <td>
                    <asp:TextBox ID="txtDireccion" class="cajasMicuenta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strUbicacion")%>'></asp:TextBox>
                </td>

                <td>
                    <strong>
                        E-Mail
                    </strong>
                </td>

                <td>
                    <asp:Label ID="Label12" runat="server" class="cajasMicuenta" Text='<%# DataBinder.Eval(Container.DataItem, "strEmail")%>'></asp:Label>
                </td>
            </tr>
            
            <asp:Label ID="lblCodePlan" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "intUsuario")%>'></asp:Label>
            <%--
            <div class="rotuloCodigoMoneda">
                Tipo usuario
            </div>
            <div class="cajaCodigoMoneda">
                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TipoUsuario")%>'></asp:Label>
            </div>
            --%>
        </ItemTemplate>
    </asp:Repeater>
    
    <tr>
        <td colspan="3">
        </td>
        <td>
            <div class="contenedorBotonBuscar">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" class="link-button white" />
            </div>
        </td>
    </tr>

    <tr>
        <td colspan="4">
            <asp:Label ID="lblError" ForeColor="black" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>

<%--<div class="right bk_white">
    <div class="box_r">
        <span class="vino titul full t_left clear block">ELEGIDOS</span>
        <asp:Panel ID="pnlpasajeros" runat="server" Style="display: block; float: left; width: 80%;
            margin-left: 10%;">
            <asp:Repeater ID="rptpasajeros" runat="server">
                <ItemTemplate>
                    <div class="clear">
                        <span class="left">
                            <asp:Label ID="lbltpousuario" runat="server" CssClass="tipoUsuarioPax" Visible="false">
                                <%#DataBinder.Eval(Container.DataItem, "strtipouser")%>
                            </asp:Label>
                            <asp:Button ID="btnSeleccionarPasajero" runat="server"
                                CssClass="btn white" Text="seleccionar" Style="float: left; margin: 0;" Visible="false" />
                            <asp:Label ID="lblid" runat="server" Style="display: none;">
                                <%#DataBinder.Eval(Container.DataItem, "intUsuario")%>
                            </asp:Label>
                            <%#DataBinder.Eval(Container.DataItem, "strNombre")%>&nbsp;&nbsp;<%#DataBinder.Eval(Container.DataItem, "strApellido")%><asp:Label ID="lblGenero" runat="server" Style="display: none;"><%#DataBinder.Eval(Container.DataItem, "intGenero")%></asp:Label>
                            <asp:Label ID="lbldtmFechanac" runat="server" Style="display: none;"><%#DataBinder.Eval(Container.DataItem, "dtmFechanac")%></asp:Label>
                            <asp:Label ID="lblinttipoident" runat="server" Style="display: none;"><%#DataBinder.Eval(Container.DataItem, "inttipoident")%></asp:Label>
                            <asp:Label ID="lblIdentificacion" runat="server" Style="display: none;"><%#DataBinder.Eval(Container.DataItem, "stridentificacion")%></asp:Label>
                        </span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Label ID="lbliditemseleccionado" runat="server" Style="display: none; float: left;"></asp:Label>
            <asp:Label ID="lbltexto" runat="server" CssClass="notaAfiliados"></asp:Label>
            <div class="clear">
                <asp:Button runat="server" ID="btnAgregarAfiliado" CssClass="block white agregar btn left cursor" Text="AGREGAR" />
            </div>
        </asp:Panel>
    </div>
</div>--%>




<%--<ajax:ModalPopupExtender ID="MPAfiliados" runat="server" TargetControlID="btnAgregarAfiliado" PopupControlID="udpReserva"
BackgroundCssClass="ui-widget-shadow" OkControlID="btncerrar">
</ajax:ModalPopupExtender>
<asp:Panel ID="udpReserva" runat="server">
    <div>
        <asp:Button ID="btncerrar" runat="server" CssClass="botonCerrarVuelosTarjeta" />
    </div>
    <div class="panelPasajeros">
        <asp:Panel ID="pnlNuevosAfiliados" runat="server" Style="width: 100%;">
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
         
            <asp:Button CssClass="block white right cursor btn" ID="lbCrear" runat="server"
                OnClick="lbCrear_Click" Text="GUARDAR" ValidationGroup="Registro"></asp:Button>
        </asp:Panel>
    </div>
</asp:Panel>
--%>
<%--<ajax:ModalPopupExtender ID="MPAfiliados" BackgroundCssClass="ui-widget-shadow" runat="server"
    TargetControlID="btnRegistrar" BehaviorID="MPAfiliados" OnOkScript=""
    OkControlID="btncerrar" PopupControlID="udpReserva" />
<asp:Panel ID="udpReserva" runat="server">
   <div>
        <asp:Button ID="btncerrar" runat="server" CssClass="botonCerrarVuelosTarjeta" />
    </div>--%>
    <%-- <div class="panelPasajeros">
        <asp:Panel ID="pnlNuevosAfiliados" runat="server" Style="width: 100%;">
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
    </div>--%>
<%--</asp:Panel>--%>
