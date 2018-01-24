<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBannerSuperior.ascx.cs" Inherits="uc_ucBanner" %>

<%@ Register Src="../uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc1" %>
<div class="botonesDerecha">
    <div class="contacto">
        <a href="../Presentacion/Contactenos.aspx" target="_blank">
            <img alt="" src="../App_Themes/Imagenes/contacto.png" />
        </a>
    </div>
    <div class="chat">
        <a href="https://livechat.boldchat.com/aid/6204590666656341798/bc.chat?cwdid=5726033846606487700" target="_blank" onclick="window.open((window.pageViewer && pageViewer.link || function(link){return link;})(this.href + (this.href.indexOf('?')>=0 ? '&amp;' : '?') + 'url=' + escape(document.location.href)), 'Chat1832357175445864370', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=640,height=480');return false;">
            <img alt="" src="../App_Themes/Imagenes/chat.png" />
        </a>
    </div>
    <div class="unete">
        <a href="http://www.facebook.com/pages/TuTiquete-Oficial/144775865619988" target="_blank">
            <img alt="" src="../App_Themes/Imagenes/fbUnete.png" />
        </a>
    </div>
</div>

<div id="bannerSuperior" class="2">
    <div class="loginPanel">
        <asp:Panel ID="pnLogin" runat="server">
            <ul class="superior">
                <li>
                    <asp:Label CssClass="bold" ID="lblTUsuario" runat="server" Text="Usuario: "></asp:Label>
                    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                </li>
                <li style="display:none;">
                    <a href="../Presentacion/CarroCompras.aspx" class="linkSesion" >
                        <asp:Label ID="Label1" runat="server" Text="Carro de compras" CssClass="linkSesion" ></asp:Label>&nbsp;
                        <asp:Label ID="lblServCarro" runat="server" Text=""></asp:Label>
                    </a>
                </li>
                <li>
                    <a href="../Presentacion/Login.aspx" class="linkSesion" >
                        <asp:Label ID="lnkLogin" runat="server" Text="Mi cuenta" CssClass="linkSesion" ></asp:Label>
                    </a>
                </li>
                <li>
                    <a href="../Presentacion/Index.aspx?ParamHtm=Logout"  onclick="GuardarSession()" class="linkSesion">
                        <asp:Label ID="Label2" runat="server" Text="Desconectar" CssClass="linkSesion"></asp:Label>
                    </a>
                </li>
            </ul>
        </asp:Panel>
        <asp:Panel ID="pnUsuario" runat="server" Visible="true">
            <table width="100%">
                <tr style="text-align:right; display:none">
                    <td>
                        <asp:Label CssClass="bold" ID="lblTMail" runat="server" Text="E-mail "></asp:Label>
                        <asp:TextBox CssClass="login" ID="txtUsuario" Width="140" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label CssClass="bold" ID="lblTClave" runat="server" Text="Contraseña "></asp:Label>
                        <asp:TextBox CssClass="login" ID="txtPassword" Width="140" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <ul class="superior">
                            <li>
                                <a href="http://www.facebook.com/pages/TuTiquete-Oficial/144775865619988" class="linkSesion" target="_blank">
                                    <asp:Label ID="Label6" runat="server" CssClass="linkSesion" Text="Ingresar a Facebook"></asp:Label>
                                </a>
                            </li>
                            <li>
                                <a href="../Presentacion/Login.aspx" onclick="GuardarSession()" class="linkSesion">
                                    <asp:Label ID="Label5" runat="server" CssClass="linkSesion" Text="Regístrate"></asp:Label>
                                </a>
                            </li>
                            <li>
                                <a href="../Presentacion/Login.aspx" onclick="GuardarSession()" class="linkSesion">
                                    <asp:Label ID="Label4" runat="server" CssClass="linkSesion" Text="Ingresa"></asp:Label>
                                </a>
                            </li>
                            <li>
                                <a href="../Presentacion/Login.aspx" onclick="GuardarSession()" class="linkSesion">
                                    <asp:Label ID="Label3" runat="server" CssClass="linkSesion" Text="Perdí mi contraseña" ></asp:Label>
                                </a> 
                            </li>
                            <%--<li class="idioma">
                                <asp:Label ID="Label7" runat="server" CssClass="linkSesion" Text="Idioma" ></asp:Label>
                                <asp:DropDownList ID="ddlIdioma" runat="server">
                                    <asp:ListItem Selected="true" Value="0" Text="Español"></asp:ListItem>
                                    <asp:ListItem Text="01:00" Value="Ingles"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox CssClass="google" MaxLength="25" ID="txtBuscar" runat="server"></asp:TextBox>
                            </li>--%>
                        </ul>
                        <asp:Label CssClass="errorLogin" ID="lblError" runat="server"></asp:Label>  
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="logo">
        <a href="../Presentacion/Index.aspx">
            <img alt="" src="../App_Themes/Imagenes/logo.png" />
        </a>
    </div>
    <div class="opcionesBanner">
        <img alt="" src="../App_Themes/Imagenes/phone.png" />
    </div>
    <uc1:ucMenuSuperior ID="UcMenuSuperior1" runat="server" />
</div>
