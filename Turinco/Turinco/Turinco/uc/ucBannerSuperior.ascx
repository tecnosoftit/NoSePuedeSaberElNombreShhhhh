<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBannerSuperior.ascx.cs"
    Inherits="uc_ucBanner" %>
<div>
    <div id="logo">
        <a href="../Presentacion/index.aspx">
            <img src="../App_Themes/Imagenes/imagesNactur/logo.png" alt="Turinco">
        </a>
    </div>
<nav>
	    <ul class="list-buttons sup">
            <li><asp:LinkButton  runat="server" oncommand="Unnamed1_Command" CssClass="link-button red">Agencia</asp:LinkButton></li>			
			<li><a href="../Presentacion/Contactenos.aspx" class="link-button red">Contáctenos</a></li>
			<li><a href="../Presentacion/Inscribase.aspx" class="link-button red">Inscribase</a></li> 
		</ul>
	</nav>
</nav>
<div class="aligncenter pbx">
    <h4>
        PBX +57 1 5310152 / +57 1 2568888 
        <a class="Tel_Agencias cssToolTip">
        Otras Ciudades
        <img class="TelefonoPeque" src="../App_Themes/Imagenes/icopequeTelefono.jpg"/>
        <label class="contenedorToolDirecto">
            <img src="../App_Themes/Imagenes/Telefonos.png"/>
        </label>
        </a>
    </h4>
</div>
<%--<div class="contenidoHeader">							
	    <div class="datosContactoHeader iblock azulClaro">
		    <span class="telefono">
                <asp:Label ID="lbltelefono" runat="server" ></asp:Label>			
		    </span>									
		    <br>
		    <span class="correo">
            <asp:Label ID="lblcorreo" runat="server" ></asp:Label>
			
		    </span>
	    </div>				
				
        <img src="../App_Themes/Imagenes/imagesNactur/iconoTelefono.png" class="iconoTel iblock" alt="Telefono">

        <a href="https://www.facebook.com/pages/Nactur-National-Tours-Agencia-de-Viajes/229216860515663" target="_blank">
            <img src="../App_Themes/Imagenes/imagesNactur/iconoFacebook.png" class="iconoFb iblock" alt="Facebook" />
        </a>
        <a href="https://twitter.com/Nactur_ec" target="_blank">
            <img src="../App_Themes/Imagenes/imagesNactur/iconoTwitter.png" class="iconoTw iblock" alt="Twitter">
        </a>
        <a href="http://instagram.com/nactur" target="_blank">
            <img src="../App_Themes/Imagenes/imagesNactur/iconoInstagram.png" class="iconoIn iblock" alt="Instagram">
        </a>
    </div>--%>
<div class="loginPanel">
        <asp:Panel ID="pnLogin" runat="server">
            <ul class="superior">
                <li>
                    <a href="" class="linkSesion">
                        <asp:Label ID="Label7" runat="server" Text="Bienvenido" CssClass="linkSesion" style="display:none;"></asp:Label>
                    </a>
                </li>
                <li>
                    <asp:Label CssClass="bold" ID="lblTUsuario" runat="server" Text="Usuario: "></asp:Label>
                    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                    <br />
                    <a href="../Presentacion/micuenta.aspx" class="linkSesion">
                        <asp:Label ID="lnkLogin" runat="server" Text="Mi cuenta" CssClass="linkSesion"></asp:Label>
                    </a>
                </li>

                <li style="display:none;">
                    <a href="../Presentacion/CarroCompras.aspx" class="linkSesion">
                        <asp:Label ID="Label1" runat="server" Text="Carro de compras" CssClass="linkSesion"></asp:Label>&nbsp;
                        <asp:Label ID="lblServCarro" runat="server" Text=""></asp:Label>
                    </a>
                </li>
                <li>
                    
                </li>
                <li>
                    <asp:LinkButton ID="lbCerrarSesion" CommandName="Logout" runat="server"
                        OnCommand="setCommand" Text="Cerrar Sesion" CssClass="linkSesion"></asp:LinkButton>
                </li>
                <li style="display:none;">
                    <a href="PreguntasFrecuentes.aspx?idSesion=<%=Request.QueryString["idSesion"] %>" class="linkSesion">
                        <asp:Label ID="Label2" runat="server" Text="Servicio al Cliente" CssClass="linkSesion"></asp:Label>
                    </a>
                </li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="pnUsuario" runat="server" Visible="true">
            <table   width="100%">
                <tr style="text-align: right; display: none">
                    <td>
                        <asp:Label CssClass="bold" ID="lblTMail" runat="server" Text="E-mail "></asp:Label>
                        <asp:TextBox CssClass="login" ID="txtUsuario" Width="140" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass="bold" ID="lblTClave" runat="server" Text="Contraseña "></asp:Label>
                        <asp:TextBox CssClass="login" ID="txtPassword" Width="140" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td colspan="2" style="text-align: center">
                        <ul class="superior">
                            <li>
                                <a href="" class="linkSesion">
                                    <asp:Label ID="Label8" runat="server" Text="Bienvenido" CssClass="linkSesion"></asp:Label>
                                </a>
                            </li>                           
                            
                            <li>
                                <a href="../Presentacion/Login.aspx" onclick="GuardarSession()" class="linkSesion">
                                    <asp:Label ID="Label4" runat="server" CssClass="linkSesion" Text="Ingresa"></asp:Label>
                                </a>
                            </li>

                            <li>
                                <a href="../Presentacion/Login.aspx" onclick="GuardarSession()" class="linkSesion">
                                    <asp:Label ID="Label5" runat="server" CssClass="linkSesion" Text="Regístrate"></asp:Label>
                                </a>
                            </li>

                            <li>
                                <a href="" class="linkSesion">
                                    <asp:Label ID="Label3" runat="server" Text="Servicio al Cliente" CssClass="linkSesion"></asp:Label>
                                </a>
                            </li>
                            
                        </ul>
                        <asp:Label CssClass="errorLogin" ID="lblError" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>    
</div> 