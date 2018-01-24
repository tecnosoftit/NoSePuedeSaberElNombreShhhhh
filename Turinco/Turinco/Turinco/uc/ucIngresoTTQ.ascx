<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucIngresoTTQ.ascx.cs"
    Inherits="uc_ucIngresoTTQ" %>
<!--- ingresar -->
<div class="pop_up">
    <div class="login_box popUpSinLogin">
        <div style="display: none;">
            <div class="full t_center logo_box">
                <img src="../App_Themes/Imagenes/logo_login.jpg" alt="Login" />
            </div>
            <div class="left" style="display: none;">
                <asp:Repeater runat="server" ID="rptSeccion">
                    <ItemTemplate>
                        <asp:Label ID="lblDescripcion" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strDescripcion") %></asp:Label>
                    </ItemTemplate>
                </asp:Repeater>
                <span class="yellow block">
                    <asp:Label ID="lblDescripcion" runat="server" Text="INGRESAR"></asp:Label>
                </span>
                <div class="clear campo">
                    <label class="block left white">
                        <asp:Label ID="lblTEmail" runat="server" Text="Email:"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtUsuario" CssClass="text right" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Validacion_Login"
                        ControlToValidate="txtUsuario" runat="server" ErrorMessage="El campo MAIL es obligatorio"
                        ForeColor="#ffffff" Font-Size="10px" Style="margin-top: 5px; float: right;"></asp:RequiredFieldValidator>
                </div>
                <div class="clear ">
                    <label class="block left white">
                        <asp:Label ID="lblTPassword" runat="server" Text="Contraseña:"></asp:Label>
                    </label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="text right" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Validacion_Login"
                        ControlToValidate="txtPassword" runat="server" ErrorMessage="El campo CONTRASEÑA es obligatorio"
                        ForeColor="#ffffff" Font-Size="10px" Style="margin-top: 5px; float: right;"></asp:RequiredFieldValidator>
                </div>
                <asp:Label ID="lblError" runat="server" Style="float: right;"></asp:Label>
                <asp:LinkButton CssClass="right white olvidaste cursor" ID="lbOlvido" runat="server"
                    CausesValidation="false" OnClick="lbOlvido_Click" Text="¿Olvidaste tu contraseña?"></asp:LinkButton>
                <asp:Button CssClass="right cursor" ID="btnEntrar" runat="server" ValidationGroup="Validacion_Login"
                    OnClick="btnEntrar_Click" Text="Ingresar"></asp:Button>
            </div>
            <div class="right">
                <span class="yellow block">BENEFICIOS</span>
                <ul class="white beneficios">
                    <li class="pad_left">Encuentra excelentes tarifas en tiquetes.</li>
                    <li class="pad_left">Comparte todos los beneficios del club con 4 elegidos.</li>
                    <li class="pad_left">Escoge un destino y sorpréndete de las ofertas. </li>
                    <li class="pad_left">Dinos cuando deseas viajar y cuanto quieres gastar y te ayudaremos
                        a buscar las mejores opciones</li>
                </ul>                
            </div>
            <div class="inscribete" style="display: none;">
                <div class="contenido_inscribete">
                    <label class="block left white" style="position: absolute; margin: 0px -23px;">
                        <asp:Label ID="LabelInscribete" runat="server" Text="INSCRIBETE"></asp:Label>
                    </label>
                    <asp:TextBox ID="TextUsuario" CssClass="text right" runat="server" Style="position: absolute;
                        margin: 0px 78px;"></asp:TextBox>
                    <asp:Button CssClass="right cursor" ID="btnEnviar_inscribete" runat="server" OnClick="btnEnviar_Click"
                        Text="ENVIAR" Style="margin: 0px"></asp:Button>
                </div>
            </div>
        </div>

        <asp:Button runat="server" ID="btnRegistro" Text="" class="BotonVerMas" OnClick="btnRegistro_Click"></asp:Button>
    </div>
</div>
