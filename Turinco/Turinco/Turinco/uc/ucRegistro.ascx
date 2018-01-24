<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRegistro.ascx.cs" Inherits="uc_ucRegistro" %>
<!--- registro -->
<table cellpadding="2" cellspacing="1" class="tablaContactoPasajeros" width="100%">
  <%--  <tr style="display: none;">
        <td>
            <asp:Label CssClass="textoLogin" ID="lblIdUsuario" runat="server" Text="0"></asp:Label>
            <asp:Label CssClass="textoLogin" ID="lblTNombre" runat="server" Text="Nombre"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtNombre" Width="80%" runat="server"></asp:TextBox>
            <asp:Label ID="Label13" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
        </td>
        <td>
            <asp:Label CssClass="textoLogin" ID="lblTApellido" runat="server" Text="Apellido"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtApellido" Width="80%" runat="server"></asp:TextBox>
            <asp:Label ID="Label16" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlTipoIdentificaion" runat="server" Width="80%">
                <asp:ListItem Value="cc"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:TextBox ID="txtDocumento" Text="10" Width="80%" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:TextBox ID="txtDireccion" Text="cl1" Width="80%" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:DropDownList ID="ddlPais" runat="server" Width="80%">
                <asp:ListItem Value="col"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Label CssClass="textoLogin" ID="lblTPasswordNuevo" runat="server" Text="Contraseña"></asp:Label>
            <asp:Label ID="Label19" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
            <br />
            <asp:TextBox ID="txtClave" Width="80%" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Label CssClass="textoLogin" ID="lblTConfirmarPassword" runat="server" Text="Confirmar contraseña"></asp:Label>
            <asp:Label ID="Label18" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
            <br />
            <asp:TextBox ID="txtclaveConfir" Width="80%" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Label ID="lblErrorContrasena" runat="server" CssClass="Error" ForeColor="Maroon"
                Text=""></asp:Label>
        </td>
        <td>
            <div style="float: right;">
                <ajax:UpdatePanel ID="upCrear" runat="server">
                    <ContentTemplate>
                        <asp:Button CssClass="botonBuscar" ID="lbCrear" runat="server" ValidationGroup="Registro"
                            OnClick="lbCrear_Click" Text="Crear cuenta" Enabled="false"></asp:Button>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>
            <ajax:UpdatePanel ID="upAcepto" runat="server">
                <ContentTemplate>
                    <asp:CheckBox ForeColor="#186e9b" Font-Bold="true" Text="Acepto condiciones de registro."
                        ID="chkCondicionesRegistro" runat="server" OnCheckedChanged="chkCondicionesRegistro_CheckedChanged"
                        AutoPostBack="true" />
                </ContentTemplate>
            </ajax:UpdatePanel>
        </td>
        <td>
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
    <tr class="txt12azul">
        <td>
            <asp:Label ID="lblTEmailRegistro" runat="server" Text="Correo electrónico (*)" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtMailPersonal" Width="84%" CssClass="campo iblock" runat="server" Enabled="false" placeholder="Correo frecuente"></asp:TextBox>
            <asp:Label ID="lblErrorMail" runat="server" CssClass="Error" ForeColor="Maroon" Text=""></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblTCelular" Visible="true" runat="server" Text="Celular (*)" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtCelular" Visible="true" Width="80%" CssClass="campo iblock" Enabled="false" 
                runat="server"  placeholder="Numero celular"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblCiudadR" runat="server" Visible="true" Text="Ciudad" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtCiudad" Visible="true" Text="" Width="80%" CssClass="campo iblock" Enabled="false" 
                runat="server" autocomplete="on"  placeholder="Ciudad residencia"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblTelf" runat="server" Visible="true" Text="Telefono Fijo" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtTelefono" Width="80%" Visible="true" CssClass="campo iblock" Enabled="false"
                Text="" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <div id="Div2" class="alineacionCentro">
                <div id="Div1" style="display: none;">
                    <div class="progressbar">
                    </div>
                    <img alt="" src="http://www.tutiquete.com/MBIF/App_Themes/Imagenes/loading.gif" /><br />
                    <span id="lblEsperar">Espere por favor...</span>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <span style="font-size: 13px; font-weight: bold; color: #186e9b">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </span>
        </td>
    </tr>--%>
    <tr class="txt12azul">
         <td>
            <asp:Label CssClass="textoLogin" ID="lblIdUsuario" Visible="false" runat="server" Text="0"></asp:Label>
            <asp:Label ID="Label1" runat="server" Text="Nombre (*)" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtNombre" Width="80%" CssClass="campo iblock"  runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="Label2" runat="server" Text="Apellido (*)" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtApellido" Width="80%" CssClass="campo iblock"  runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblTEmailRegistro" runat="server" Text="Correo electrónico (*)" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtMailPersonal" Width="84%" CssClass="campo iblock" runat="server" Enabled="false" placeholder="Correo frecuente"></asp:TextBox>
            <asp:Label ID="lblErrorMail" runat="server" CssClass="Error" ForeColor="Maroon" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCiudadR" runat="server" Visible="true" Text="Ciudad" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtCiudad" Visible="true" Text="" Width="80%" CssClass="campo iblock" Enabled="false" 
                runat="server" autocomplete="on"  placeholder="Ciudad residencia"></asp:TextBox>
        </td>        
        <td>
             <asp:Label ID="Label3" runat="server" Text="Cédula (*)" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtDocumento" Text="10" CssClass="campo iblock"  Width="80%" runat="server"></asp:TextBox>
        </td>
        <td>
            <asp:Label ID="lblTelf" runat="server" Visible="true" Text="Telefono Fijo" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtTelefono" Width="80%" Visible="true" CssClass="campo iblock" Enabled="false"
                Text="" runat="server"></asp:TextBox>
        </td>    
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblTCelular" Visible="true" runat="server" Text="Celular (*)" CssClass="label iblock full"></asp:Label>
            <asp:TextBox ID="txtCelular" Visible="true" Width="80%" CssClass="campo iblock" Enabled="false" 
                runat="server"  placeholder="Numero celular"></asp:TextBox>
        </td>
         <td style="display: none;">
            <asp:DropDownList ID="ddlTipoIdentificaion" runat="server" Width="80%">
                <asp:ListItem Value="cc"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td  style="display: none;">
            <asp:TextBox ID="txtDireccion" Text="cl1" Width="80%" runat="server"></asp:TextBox>
        </td>
        <td  style="display: none;">
            <asp:DropDownList ID="ddlPais" runat="server" Width="80%">
                <asp:ListItem Value="col"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="display: none;">
            <asp:Label CssClass="textoLogin" ID="lblTPasswordNuevo" runat="server" Text="Contraseña"></asp:Label>
            <asp:Label ID="Label19" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
            <br />
            <asp:TextBox ID="txtClave" Width="80%" runat="server"></asp:TextBox>
        </td>
        <td style="display: none;">
            <asp:Label CssClass="textoLogin" ID="lblTConfirmarPassword" runat="server" Text="Confirmar contraseña"></asp:Label>
            <asp:Label ID="Label18" runat="server" CssClass="textoLogin" Text="(*)"></asp:Label>
            <br />
            <asp:TextBox ID="txtclaveConfir" Width="80%" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Label ID="lblErrorContrasena" runat="server" CssClass="Error" ForeColor="Maroon"
                Text=""></asp:Label>
        </td>
        <td style="display: none;">
            <div style="float: right;">
                <ajax:UpdatePanel ID="upCrear" runat="server">
                    <ContentTemplate>
                        <asp:Button CssClass="botonBuscar" ID="lbCrear" runat="server" ValidationGroup="Registro"
                            OnClick="lbCrear_Click" Text="Crear cuenta" Enabled="false"></asp:Button>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>
            <ajax:UpdatePanel ID="upAcepto" runat="server">
                <ContentTemplate>
                    <asp:CheckBox ForeColor="#186e9b" Font-Bold="true" Text="Acepto condiciones de registro."
                        ID="chkCondicionesRegistro" runat="server" OnCheckedChanged="chkCondicionesRegistro_CheckedChanged"
                        AutoPostBack="true" />
                </ContentTemplate>
            </ajax:UpdatePanel>
        </td>
        <td  style="display: none;">
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
    <tr>
        <td>
            <div id="Div2" class="alineacionCentro">
                <div id="Div1" style="display: none;">
                    <div class="progressbar">
                    </div>
                    <img alt="" src="http://www.tutiquete.com/MBIF/App_Themes/Imagenes/loading.gif" /><br />
                    <span id="lblEsperar">Espere por favor...</span>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <span style="font-size: 13px; font-weight: bold; color: #186e9b">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
</table>
