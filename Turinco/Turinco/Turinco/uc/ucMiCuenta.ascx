<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMiCuenta.ascx.cs" Inherits="uc_ucMiCuenta" %>
<%@ Register src="ucReservasVigentes.ascx" tagname="ucReservasVigentes" tagprefix="uc1" %>
<%@ Register src="ucDatosUsuario.ascx" tagname="ucDatosUsuario" tagprefix="uc2" %>
<%@ Register src="ucReservasHistoricas.ascx" tagname="ucReservasHistoricas" tagprefix="uc3" %>
<%@ Register src="ucCambiarContrasenia.ascx" tagname="ucCambiarContrasenia" tagprefix="uc4" %>

<div class="panelCompleto panelCompletoMiCuenta">
    <div class="panelResultados">
        <div class="contenidoResultado">
            <div id="tabs" style="float:left;">
                <ul class="t_left">
                    <li>
                        <a href="#datos" id="adatos">
                            <asp:Label ID="lblTVuelos" runat="server" Text="Mis datos"></asp:Label>
                        </a>
                    </li>
            
                    <li>
                        <a href="#vigentes" id="avigentes">
                            <asp:Label ID="Label1" runat="server" Text="Reservas Vigentes"></asp:Label>
                        </a>
                    </li>

                    <li>
                        <a href="#historicas" id="ahistoricas">
                            <asp:Label ID="Label2" runat="server" Text="Reservas Históricas"></asp:Label>
                        </a>
                    </li>

                    <li>
                        <a href="#contrasenia" id="acontrasenia">
                            <asp:Label ID="Label3" runat="server" Text="Modificar Contraseña"></asp:Label>
                        </a>
                    </li>
                </ul>
                
                <div class="contenedorMicuenta">
                    <!--- datos -->
                    <div id="datos" class="completoCuentaDetalle">
                        <uc2:ucDatosUsuario ID="ucDatosUsuario1" runat="server" />
                    </div>
                    <div id="vigentes" class="completoCuentaDetalle">
                        <uc1:ucReservasVigentes ID="ucReservasVigentes1" runat="server" />
                    </div>
                    <!--- reservas historicas -->
                    <div id="historicas" class="completoCuentaDetalle">
                        <uc3:ucReservasHistoricas ID="ucReservasHistoricas1" runat="server" />
                    </div>
                    <!--- modificacion contraseña -->
                    <div id="contrasenia" class="completoCuentaDetalle">
                        <uc4:ucCambiarContrasenia ID="ucCambiarContrasenia1" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


