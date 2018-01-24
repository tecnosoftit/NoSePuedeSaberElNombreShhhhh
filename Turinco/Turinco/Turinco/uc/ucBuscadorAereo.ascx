<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuscadorAereo.ascx.cs"
    Inherits="uc_ucBuscadorAereo" %>
<!--- vuelos -->
<div class="contenidoBuscador">
    <asp:RadioButtonList ID="modal_vuelos" runat="server" RepeatDirection="Horizontal"
        CssClass="radioButtonList">
        <asp:ListItem Value="0" Selected="True" Text="Ida y vuelta" class="titcomp"></asp:ListItem>
        <asp:ListItem Value="1" Text="Sólo ida" class="titcomp"></asp:ListItem>
        <asp:ListItem Value="2" Text="Múltiples destinos" class="titcomp" style="display: none;"></asp:ListItem>
    </asp:RadioButtonList>
    <!--- solo ida -->
    <div id="vuelos_solo_ida">
        <div id="origen1">
            <asp:Label ID="lbl_Multi_O1" runat="server" Text="Origen" class="titcomp"></asp:Label>
            <asp:TextBox ID="txt_Multi_O1" placeholder="Ciudad de Origen" runat="server" CssClass="formaBuscar"
                onclick='javascript:PressValidacion(this)'></asp:TextBox>
            <asp:Label ID="lbl_Multi_O1E" CssClass="Error" runat="server"></asp:Label>
        </div>
        <div id="destino1">
            <asp:Label ID="lbl_Multi_D1" runat="server" Text="Destino" class="titcomp"></asp:Label>
            <asp:TextBox ID="txt_Multi_D1" placeholder="Ciudad de Destino" runat="server" CssClass="formaBuscar"
                onclick='javascript:PressValidacion(this)'></asp:TextBox>
            <asp:Label ID="lbl_Multi_D1E" CssClass="Error" runat="server"></asp:Label>
        </div>
        <div id="fechaSalida1" class="div50Buscador">
            <asp:Label ID="lblFechaMulti1" runat="server" Text="Partida" class="titcomp"></asp:Label>
            <asp:TextBox ID="txtFechaMultiO1" CssClass="formaBuscar calendario" runat="server"
                onclick='javascript:PressValidacion(this)'></asp:TextBox>
            <asp:Label ID="lblFechaMulti1E" CssClass="Error" runat="server"></asp:Label>
        </div>
        <!--- ida y vuelta -->
        <div id="vuelos_ida_vuelta" class="div50Buscador" style="margin-left: 35px;">
            <div id="fechaRegreso1" class="iblock">
                <asp:Label ID="lblTFechaRegreso" runat="server" Text="Regreso" class="titcomp"></asp:Label>
                <asp:TextBox ID="txt2VFechaMulti" CssClass="formaBuscar calendario" runat="server"
                    onclick='javascript:PressValidacion(this)'></asp:TextBox>
                <asp:Label ID="Label21" CssClass="Error" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <!--- multiples destino -->
    <div id="vuelos_multi_destinos" style="display: none;">
        <!--- destino 1 -->
        <div id="vuelos_multi_destino2">
            <div id="origen2" class="div50Buscador">
                <asp:Label ID="lbl_Multi_O2" runat="server" Text="Origen" class="titcomp"></asp:Label>
                <asp:TextBox ID="txt_Multi_O2" Text="" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_OE2" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="destino2" class="div50Buscador">
                <asp:Label ID="lbl_Multi_D2" runat="server" Text="Destino" class="titcomp"></asp:Label>
                <asp:TextBox ID="txt_Multi_D2" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_DE2" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="fechaSalidaO2" class="div50Buscador">
                <asp:Label ID="lblFechaMultiO2" runat="server" Text="Partida" class="titcomp"></asp:Label>
                <asp:TextBox ID="txtFechaMultiO2" runat="server" CssClass="formaBuscar calendario"></asp:TextBox>
                <asp:Label ID="lblFechaMultiEO2" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="horaSalidaO2" class="noneDisplay">
                <asp:Label ID="lblMultiHoraO2" runat="server" Text="Hora salida"></asp:Label>
                <asp:DropDownList ID="ddlMultiHoraO2" runat="server" CssClass="formaBuscarCombo">
                    <asp:ListItem Selected="true" Value="0" Text="Econ&#243;mica"></asp:ListItem>
                    <asp:ListItem Text="01:00" Value="01:00:00"></asp:ListItem>
                    <asp:ListItem Text="02:00" Value="02:00:00"></asp:ListItem>
                    <asp:ListItem Text="03:00" Value="03:00:00"></asp:ListItem>
                    <asp:ListItem Text="04:00" Value="04:00:00"></asp:ListItem>
                    <asp:ListItem Text="05:00" Value="05:00:00"></asp:ListItem>
                    <asp:ListItem Text="06:00" Value="06:00:00"></asp:ListItem>
                    <asp:ListItem Text="07:00" Value="07:00:00"></asp:ListItem>
                    <asp:ListItem Text="08:00" Value="08:00:00"></asp:ListItem>
                    <asp:ListItem Text="09:00" Value="09:00:00"></asp:ListItem>
                    <asp:ListItem Text="10:00" Value="10:00:00"></asp:ListItem>
                    <asp:ListItem Text="11:00" Value="11:00:00"></asp:ListItem>
                    <asp:ListItem Text="12:00" Value="12:00:00"></asp:ListItem>
                    <asp:ListItem Text="13:00" Value="13:00:00"></asp:ListItem>
                    <asp:ListItem Text="14:00" Value="14:00:00"></asp:ListItem>
                    <asp:ListItem Text="15:00" Value="15:00:00"></asp:ListItem>
                    <asp:ListItem Text="16:00" Value="16:00:00"></asp:ListItem>
                    <asp:ListItem Text="17:00" Value="17:00:00"></asp:ListItem>
                    <asp:ListItem Text="18:00" Value="18:00:00"></asp:ListItem>
                    <asp:ListItem Text="19:00" Value="19:00:00"></asp:ListItem>
                    <asp:ListItem Text="20:00" Value="20:00:00"></asp:ListItem>
                    <asp:ListItem Text="21:00" Value="21:00:00"></asp:ListItem>
                    <asp:ListItem Text="22:00" Value="22:00:00"></asp:ListItem>
                    <asp:ListItem Text="23:00" Value="23:00:00"></asp:ListItem>
                    <asp:ListItem Text="24:00" Value="24:00:00"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <!--- destino 2 -->
        <div id="vuelos_multi_destino3" style="display: none;">
            <div id="origen3" class="div50Buscador">
                <asp:Label ID="lbl_Multi_O3" runat="server" Text="Origen" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txt_Multi_O3" Text="" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_OE3" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="destino3" class="div50Buscador">
                <asp:Label ID="lbl_Multi_D3" runat="server" Text="Destino" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txt_Multi_D3" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_DE3" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="fechaSalidaO3" class="div50Buscador">
                <asp:Label ID="lblFechaMultiO3" runat="server" Text="Partida" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txtFechaMultiO3" runat="server" CssClass="formaBuscar calendario"></asp:TextBox>
                <asp:Label ID="lblFechaMultiEO3" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="horaSalidaO3" class="noneDisplay">
                <asp:Label ID="lblMultiHoraO3" runat="server" Text="Hora salida"></asp:Label>
                <asp:DropDownList ID="ddlMultiHoraO3" runat="server" CssClass="formaBuscarCombo">
                    <asp:ListItem Selected="true" Value="0" Text="Econ&#243;mica"></asp:ListItem>
                    <asp:ListItem Text="01:00" Value="01:00:00"></asp:ListItem>
                    <asp:ListItem Text="02:00" Value="02:00:00"></asp:ListItem>
                    <asp:ListItem Text="03:00" Value="03:00:00"></asp:ListItem>
                    <asp:ListItem Text="04:00" Value="04:00:00"></asp:ListItem>
                    <asp:ListItem Text="05:00" Value="05:00:00"></asp:ListItem>
                    <asp:ListItem Text="06:00" Value="06:00:00"></asp:ListItem>
                    <asp:ListItem Text="07:00" Value="07:00:00"></asp:ListItem>
                    <asp:ListItem Text="08:00" Value="08:00:00"></asp:ListItem>
                    <asp:ListItem Text="09:00" Value="09:00:00"></asp:ListItem>
                    <asp:ListItem Text="10:00" Value="10:00:00"></asp:ListItem>
                    <asp:ListItem Text="11:00" Value="11:00:00"></asp:ListItem>
                    <asp:ListItem Text="12:00" Value="12:00:00"></asp:ListItem>
                    <asp:ListItem Text="13:00" Value="13:00:00"></asp:ListItem>
                    <asp:ListItem Text="14:00" Value="14:00:00"></asp:ListItem>
                    <asp:ListItem Text="15:00" Value="15:00:00"></asp:ListItem>
                    <asp:ListItem Text="16:00" Value="16:00:00"></asp:ListItem>
                    <asp:ListItem Text="17:00" Value="17:00:00"></asp:ListItem>
                    <asp:ListItem Text="18:00" Value="18:00:00"></asp:ListItem>
                    <asp:ListItem Text="19:00" Value="19:00:00"></asp:ListItem>
                    <asp:ListItem Text="20:00" Value="20:00:00"></asp:ListItem>
                    <asp:ListItem Text="21:00" Value="21:00:00"></asp:ListItem>
                    <asp:ListItem Text="22:00" Value="22:00:00"></asp:ListItem>
                    <asp:ListItem Text="23:00" Value="23:00:00"></asp:ListItem>
                    <asp:ListItem Text="24:00" Value="24:00:00"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <!--- destino 3 -->
        <div id="vuelos_multi_destino4" style="display: none;">
            <div id="origen4" class="div50Buscador">
                <asp:Label ID="lbl_Multi_O4" runat="server" Text="Origen" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txt_Multi_O4" Text="" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_OE4" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="destino4" class="div50Buscador">
                <asp:Label ID="lbl_Multi_D4" runat="server" Text="Destino" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txt_Multi_D4" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_DE4" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="fechaSalidaO4" class="div50Buscador">
                <asp:Label ID="lblFechaMultiO4" runat="server" Text="Partida" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txtFechaMultiO4" runat="server" CssClass="formaBuscar calendario"></asp:TextBox>
                <asp:Label ID="lblFechaMultiEO4" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="horaSalidaO4" class="noneDisplay">
                <asp:Label ID="lblMultiHoraO4" runat="server" Text="Hora salida"></asp:Label>
                <asp:DropDownList ID="ddlMultiHoraO4" runat="server" CssClass="formaBuscarCombo">
                    <asp:ListItem Selected="true" Value="0" Text="Econ&#243;mica"></asp:ListItem>
                    <asp:ListItem Text="01:00" Value="01:00:00"></asp:ListItem>
                    <asp:ListItem Text="02:00" Value="02:00:00"></asp:ListItem>
                    <asp:ListItem Text="03:00" Value="03:00:00"></asp:ListItem>
                    <asp:ListItem Text="04:00" Value="04:00:00"></asp:ListItem>
                    <asp:ListItem Text="05:00" Value="05:00:00"></asp:ListItem>
                    <asp:ListItem Text="06:00" Value="06:00:00"></asp:ListItem>
                    <asp:ListItem Text="07:00" Value="07:00:00"></asp:ListItem>
                    <asp:ListItem Text="08:00" Value="08:00:00"></asp:ListItem>
                    <asp:ListItem Text="09:00" Value="09:00:00"></asp:ListItem>
                    <asp:ListItem Text="10:00" Value="10:00:00"></asp:ListItem>
                    <asp:ListItem Text="11:00" Value="11:00:00"></asp:ListItem>
                    <asp:ListItem Text="12:00" Value="12:00:00"></asp:ListItem>
                    <asp:ListItem Text="13:00" Value="13:00:00"></asp:ListItem>
                    <asp:ListItem Text="14:00" Value="14:00:00"></asp:ListItem>
                    <asp:ListItem Text="15:00" Value="15:00:00"></asp:ListItem>
                    <asp:ListItem Text="16:00" Value="16:00:00"></asp:ListItem>
                    <asp:ListItem Text="17:00" Value="17:00:00"></asp:ListItem>
                    <asp:ListItem Text="18:00" Value="18:00:00"></asp:ListItem>
                    <asp:ListItem Text="19:00" Value="19:00:00"></asp:ListItem>
                    <asp:ListItem Text="20:00" Value="20:00:00"></asp:ListItem>
                    <asp:ListItem Text="21:00" Value="21:00:00"></asp:ListItem>
                    <asp:ListItem Text="22:00" Value="22:00:00"></asp:ListItem>
                    <asp:ListItem Text="23:00" Value="23:00:00"></asp:ListItem>
                    <asp:ListItem Text="24:00" Value="24:00:00"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <!--- destino 4 -->
        <div id="vuelos_multi_destino5" style="display: none;">
            <div id="origen5" class="div50Buscador">
                <asp:Label ID="lbl_Multi_O5" runat="server" Text="Origen" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txt_Multi_O5" Text="" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_OE5" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="destino5" class="div50Buscador">
                <asp:Label ID="lbl_Multi_D5" runat="server" Text="Destino" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txt_Multi_D5" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_DE5" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="fechaSalidaO5" class="div50Buscador">
                <asp:Label ID="lblFechaMultiO5" runat="server" Text="Partida" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txtFechaMultiO5" runat="server" CssClass="formaBuscar calendario"></asp:TextBox>
                <asp:Label ID="lblFechaMultiEO5" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="horaSalidaO5" class="noneDisplay">
                <asp:Label ID="lblMultiHoraO5" runat="server" Text="Hora salida"></asp:Label>
                <asp:DropDownList ID="ddlMultiHoraO5" runat="server" CssClass="formaBuscarCombo">
                    <asp:ListItem Selected="true" Value="0" Text="Econ&#243;mica"></asp:ListItem>
                    <asp:ListItem Text="01:00" Value="01:00:00"></asp:ListItem>
                    <asp:ListItem Text="02:00" Value="02:00:00"></asp:ListItem>
                    <asp:ListItem Text="03:00" Value="03:00:00"></asp:ListItem>
                    <asp:ListItem Text="04:00" Value="04:00:00"></asp:ListItem>
                    <asp:ListItem Text="05:00" Value="05:00:00"></asp:ListItem>
                    <asp:ListItem Text="06:00" Value="06:00:00"></asp:ListItem>
                    <asp:ListItem Text="07:00" Value="07:00:00"></asp:ListItem>
                    <asp:ListItem Text="08:00" Value="08:00:00"></asp:ListItem>
                    <asp:ListItem Text="09:00" Value="09:00:00"></asp:ListItem>
                    <asp:ListItem Text="10:00" Value="10:00:00"></asp:ListItem>
                    <asp:ListItem Text="11:00" Value="11:00:00"></asp:ListItem>
                    <asp:ListItem Text="12:00" Value="12:00:00"></asp:ListItem>
                    <asp:ListItem Text="13:00" Value="13:00:00"></asp:ListItem>
                    <asp:ListItem Text="14:00" Value="14:00:00"></asp:ListItem>
                    <asp:ListItem Text="15:00" Value="15:00:00"></asp:ListItem>
                    <asp:ListItem Text="16:00" Value="16:00:00"></asp:ListItem>
                    <asp:ListItem Text="17:00" Value="17:00:00"></asp:ListItem>
                    <asp:ListItem Text="18:00" Value="18:00:00"></asp:ListItem>
                    <asp:ListItem Text="19:00" Value="19:00:00"></asp:ListItem>
                    <asp:ListItem Text="20:00" Value="20:00:00"></asp:ListItem>
                    <asp:ListItem Text="21:00" Value="21:00:00"></asp:ListItem>
                    <asp:ListItem Text="22:00" Value="22:00:00"></asp:ListItem>
                    <asp:ListItem Text="23:00" Value="23:00:00"></asp:ListItem>
                    <asp:ListItem Text="24:00" Value="24:00:00"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <!--- destino 5 -->
        <div id="vuelos_multi_destino6" style="display: none;">
            <div id="origen6" class="div50Buscador">
                <asp:Label ID="lbl_Multi_O6" runat="server" Text="Origen" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txt_Multi_O6" Text="" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_OE6" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="destino6" class="div50Buscador">
                <asp:Label ID="lbl_Multi_D6" runat="server" Text="Destino" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txt_Multi_D6" runat="server" CssClass="formaBuscar"></asp:TextBox>
                <asp:Label ID="lbl_Multi_DE6" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="fechaSalidaO6" class="div50Buscador">
                <asp:Label ID="lblFechaMultiO6" runat="server" Text="Partida" CssClass="labelBuscar"></asp:Label>
                <asp:TextBox ID="txtFechaMultiO6" runat="server" CssClass="formaBuscar calendario"></asp:TextBox>
                <asp:Label ID="lblFechaMultiEO6" CssClass="Error" runat="server"></asp:Label>
            </div>
            <div id="horaSalidaO6" class="noneDisplay">
                <asp:Label ID="lblMultiHoraO6" runat="server" Text="Hora salida"></asp:Label>
                <asp:DropDownList ID="ddlMultiHoraO6" runat="server" CssClass="formaBuscarCombo">
                    <asp:ListItem Selected="true" Value="0" Text="Econ&#243;mica"></asp:ListItem>
                    <asp:ListItem Text="01:00" Value="01:00:00"></asp:ListItem>
                    <asp:ListItem Text="02:00" Value="02:00:00"></asp:ListItem>
                    <asp:ListItem Text="03:00" Value="03:00:00"></asp:ListItem>
                    <asp:ListItem Text="04:00" Value="04:00:00"></asp:ListItem>
                    <asp:ListItem Text="05:00" Value="05:00:00"></asp:ListItem>
                    <asp:ListItem Text="06:00" Value="06:00:00"></asp:ListItem>
                    <asp:ListItem Text="07:00" Value="07:00:00"></asp:ListItem>
                    <asp:ListItem Text="08:00" Value="08:00:00"></asp:ListItem>
                    <asp:ListItem Text="09:00" Value="09:00:00"></asp:ListItem>
                    <asp:ListItem Text="10:00" Value="10:00:00"></asp:ListItem>
                    <asp:ListItem Text="11:00" Value="11:00:00"></asp:ListItem>
                    <asp:ListItem Text="12:00" Value="12:00:00"></asp:ListItem>
                    <asp:ListItem Text="13:00" Value="13:00:00"></asp:ListItem>
                    <asp:ListItem Text="14:00" Value="14:00:00"></asp:ListItem>
                    <asp:ListItem Text="15:00" Value="15:00:00"></asp:ListItem>
                    <asp:ListItem Text="16:00" Value="16:00:00"></asp:ListItem>
                    <asp:ListItem Text="17:00" Value="17:00:00"></asp:ListItem>
                    <asp:ListItem Text="18:00" Value="18:00:00"></asp:ListItem>
                    <asp:ListItem Text="19:00" Value="19:00:00"></asp:ListItem>
                    <asp:ListItem Text="20:00" Value="20:00:00"></asp:ListItem>
                    <asp:ListItem Text="21:00" Value="21:00:00"></asp:ListItem>
                    <asp:ListItem Text="22:00" Value="22:00:00"></asp:ListItem>
                    <asp:ListItem Text="23:00" Value="23:00:00"></asp:ListItem>
                    <asp:ListItem Text="24:00" Value="24:00:00"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <!--- agregar -->
        <div class="div50Buscador fRight" id="agregarOcultar1">
            <a href="javascript:mostrarOcultarComponente('1');">
                <asp:Label ID="lblAgregarVuelos" runat="server" Text="Agregar más vuelos +"></asp:Label>
            </a>
        </div>
        <!--- agregar / ocultar -->
        <div id="agregarOcultar2" style="display: none">
            <div class="div50Buscador fRight">
                <a href="javascript:mostrarOcultarComponente('1');">
                    <asp:Label ID="lblAgregarVuelos2" runat="server" Text="Agregar más vuelos +"></asp:Label>
                </a>
            </div>
            <div class="div50Buscador fLeft">
                <a href="javascript:mostrarOcultarComponente('2');">
                    <asp:Label ID="lblOcultarVuelos" runat="server" Text="Ocultar vuelos -"></asp:Label>
                </a>
            </div>
        </div>
        <!--- ocultar -->
        <div class="div50Buscador fLeft" id="agregarOcultar3" style="display: none">
            <a href="javascript:mostrarOcultarComponente('2');">
                <asp:Label ID="lblOcultarVuelos2" runat="server" Text="Ocultar vuelos -"></asp:Label>
            </a>
        </div>
    </div>
    <!--- pasajeros -->
    <div id="pasajeros" class="tablaBuscadorVuelos contenedorTablaPasajerosBuscador">
        <div class="titcomp">
            Pasajeros
        </div>
        <table width="100%" border="0" class="tablaPasajerosBuscador" id="tablaDePasajeros"
            cellspacing="0" cellpadding="0">
            <tr>
                <td class="pasajeros">
                    <asp:Label ID="lblTAdultos" runat="server" Text="Adultos" class="titcomp"></asp:Label>
                    <asp:Label ID="lblRangoAdultos" runat="server" Text=""></asp:Label>
                    <asp:DropDownList ID="ddlMultiAdultos" runat="server" CssClass="buscadorTexto">
                        <asp:ListItem Selected="true">1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="pasajeros">
                    <asp:Label ID="lblTNinos" runat="server" Text="Niños" class="titcomp"></asp:Label>
                    <asp:Label ID="lblRangoNinos" runat="server" Text=""></asp:Label>
                    <asp:DropDownList ID="ddlMultiNinios" runat="server" CssClass="buscadorTexto">
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="pasajeros">
                    <asp:Label ID="lblTInfantes" runat="server" Text="Infantes" class="titcomp"></asp:Label>
                    <asp:Label ID="lblRangoInfantes" runat="server" Text=""></asp:Label>
                    <asp:DropDownList ID="ddlMultiBebes" runat="server" CssClass="buscadorTexto">
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="pasajeros">
                </td>
                <td class="pasajeros">
                    <asp:Panel ID="PanelEdadesNinos" runat="server" CssClass="edades iblock">
                        <table align="right" id="tblEdadesNinos" cellspacing="0" cellpadding="2">
                            <tr style="display: none">
                                <td id="trMultiEdad1">
                                    <asp:Label ID="lblTAnos" runat="server" Text="Años"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiEdad1" Width="50px" runat="server" CssClass="buscadorTexto">
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiEdad2">
                                    <asp:Label ID="lblTAnos2" runat="server" Text="Años"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiEdad2" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiEdad3">
                                    <asp:Label ID="lblTAnos3" runat="server" Text="Años"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiEdad3" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiEdad4">
                                    <asp:Label ID="lblTAnos4" runat="server" Text="Años"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiEdad4" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiEdad5">
                                    <asp:Label ID="lblTAnos5" runat="server" Text="Años"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiEdad5" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiEdad6">
                                    <asp:Label ID="lblTAnos6" runat="server" Text="Años"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiEdad6" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td class="pasajeros">
                    <asp:Panel ID="PanelEdadesInfantes" runat="server" CssClass="edades iblock">
                        <table align="right" cellspacing="0" id="tblEdadesInfantes" cellpadding="2">
                            <tr style="display: none">
                                <td id="trMultiMeses1">
                                    <asp:Label ID="lblTMeses" runat="server" Text="Meses"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiMeses1" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiMeses2">
                                    <asp:Label ID="lblTMeses2" runat="server" Text="Meses"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiMeses2" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiMeses3">
                                    <asp:Label ID="lblTMeses3" runat="server" Text="Meses"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiMeses3" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiMeses4">
                                    <asp:Label ID="lblTMeses4" runat="server" Text="Meses"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiMeses4" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiMeses5">
                                    <asp:Label ID="lblTMeses5" runat="server" Text="Meses"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiMeses5" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td id="trMultiMeses6">
                                    <asp:Label ID="lblTMeses6" runat="server" Text="Meses"></asp:Label>&nbsp;
                                    <div>
                                        <asp:DropDownList ID="ddlMultiMeses6" runat="server" Width="50px" CssClass="buscadorTexto">
                                            <asp:ListItem>1</asp:ListItem>
                                            <asp:ListItem>2</asp:ListItem>
                                            <asp:ListItem>3</asp:ListItem>
                                            <asp:ListItem>4</asp:ListItem>
                                            <asp:ListItem>5</asp:ListItem>
                                            <asp:ListItem>6</asp:ListItem>
                                            <asp:ListItem>7</asp:ListItem>
                                            <asp:ListItem>8</asp:ListItem>
                                            <asp:ListItem>9</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <div class="contenedorBotonBuscar" style="float: none; margin-top: -2px;">
            <asp:Button ID="lbMultidestino" runat="server" ValidationGroup="2" class="link-button white"
                OnClientClick="return Show_Cortinilla_Validacion(1);" Text="Buscar" CommandName="Air"
                OnCommand="setCommand"></asp:Button>
        </div>
    </div>
    <!--- links opciones avanzadas -->
    <div class="tablaBuscadorVuelos">
        <%-- <a href="javascript:;" style="display:none" class="show_hide">Búsqueda avanzada</a>--%>
    </div>
    <!--- opciones avanzadas -->
    <div id="avanzadas" class="slidingDiv">
        <!--- horario  -->
        <div id="horaSalida1" class="tablaBuscadorAutos">
            <div class="titulo">
                <asp:Label ID="lblMultiHora1" runat="server" Text="Hora salida"></asp:Label>
            </div>
            <div class="forma">
                <div>
                    <asp:DropDownList ID="ddlMultiHora01" runat="server" CssClass="formaBuscarCombo">
                        <asp:ListItem Selected="true" Value="0" Text="Más econ&#243;mica"></asp:ListItem>
                        <asp:ListItem Text="01:00" Value="01:00:00"></asp:ListItem>
                        <asp:ListItem Text="02:00" Value="02:00:00"></asp:ListItem>
                        <asp:ListItem Text="03:00" Value="03:00:00"></asp:ListItem>
                        <asp:ListItem Text="04:00" Value="04:00:00"></asp:ListItem>
                        <asp:ListItem Text="05:00" Value="05:00:00"></asp:ListItem>
                        <asp:ListItem Text="06:00" Value="06:00:00"></asp:ListItem>
                        <asp:ListItem Text="07:00" Value="07:00:00"></asp:ListItem>
                        <asp:ListItem Text="08:00" Value="08:00:00"></asp:ListItem>
                        <asp:ListItem Text="09:00" Value="09:00:00"></asp:ListItem>
                        <asp:ListItem Text="10:00" Value="10:00:00"></asp:ListItem>
                        <asp:ListItem Text="11:00" Value="11:00:00"></asp:ListItem>
                        <asp:ListItem Text="12:00" Value="12:00:00"></asp:ListItem>
                        <asp:ListItem Text="13:00" Value="13:00:00"></asp:ListItem>
                        <asp:ListItem Text="14:00" Value="14:00:00"></asp:ListItem>
                        <asp:ListItem Text="15:00" Value="15:00:00"></asp:ListItem>
                        <asp:ListItem Text="16:00" Value="16:00:00"></asp:ListItem>
                        <asp:ListItem Text="17:00" Value="17:00:00"></asp:ListItem>
                        <asp:ListItem Text="18:00" Value="18:00:00"></asp:ListItem>
                        <asp:ListItem Text="19:00" Value="19:00:00"></asp:ListItem>
                        <asp:ListItem Text="20:00" Value="20:00:00"></asp:ListItem>
                        <asp:ListItem Text="21:00" Value="21:00:00"></asp:ListItem>
                        <asp:ListItem Text="22:00" Value="22:00:00"></asp:ListItem>
                        <asp:ListItem Text="23:00" Value="23:00:00"></asp:ListItem>
                        <asp:ListItem Text="24:00" Value="24:00:00"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div id="horaRegreso1" class="tablaBuscadorAutos">
            <div class="titulo">
                <asp:Label ID="lblTHoraRegreso" runat="server" Text="Hora regreso"></asp:Label><br />
            </div>
            <div class="forma">
                <div>
                    <asp:DropDownList ID="ddlMultiHoraD2" runat="server" CssClass="formaBuscarCombo">
                        <asp:ListItem Selected="true" Value="0" Text="Más econ&#243;mica"></asp:ListItem>
                        <asp:ListItem Text="01:00" Value="01:00:00"></asp:ListItem>
                        <asp:ListItem Text="02:00" Value="02:00:00"></asp:ListItem>
                        <asp:ListItem Text="03:00" Value="03:00:00"></asp:ListItem>
                        <asp:ListItem Text="04:00" Value="04:00:00"></asp:ListItem>
                        <asp:ListItem Text="05:00" Value="05:00:00"></asp:ListItem>
                        <asp:ListItem Text="06:00" Value="06:00:00"></asp:ListItem>
                        <asp:ListItem Text="07:00" Value="07:00:00"></asp:ListItem>
                        <asp:ListItem Text="08:00" Value="08:00:00"></asp:ListItem>
                        <asp:ListItem Text="09:00" Value="09:00:00"></asp:ListItem>
                        <asp:ListItem Text="10:00" Value="10:00:00"></asp:ListItem>
                        <asp:ListItem Text="11:00" Value="11:00:00"></asp:ListItem>
                        <asp:ListItem Text="12:00" Value="12:00:00"></asp:ListItem>
                        <asp:ListItem Text="13:00" Value="13:00:00"></asp:ListItem>
                        <asp:ListItem Text="14:00" Value="14:00:00"></asp:ListItem>
                        <asp:ListItem Text="15:00" Value="15:00:00"></asp:ListItem>
                        <asp:ListItem Text="16:00" Value="16:00:00"></asp:ListItem>
                        <asp:ListItem Text="17:00" Value="17:00:00"></asp:ListItem>
                        <asp:ListItem Text="18:00" Value="18:00:00"></asp:ListItem>
                        <asp:ListItem Text="19:00" Value="19:00:00"></asp:ListItem>
                        <asp:ListItem Text="20:00" Value="20:00:00"></asp:ListItem>
                        <asp:ListItem Text="21:00" Value="21:00:00"></asp:ListItem>
                        <asp:ListItem Text="22:00" Value="22:00:00"></asp:ListItem>
                        <asp:ListItem Text="23:00" Value="23:00:00"></asp:ListItem>
                        <asp:ListItem Text="24:00" Value="24:00:00"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <!--- clase  -->
        <div id="clase" class="tablaBuscadorVuelos">
            <div class="titulo">
                <asp:Label ID="Label25" runat="server" Text="Clase"></asp:Label>
            </div>
            <div class="forma">
                <div>
                    <asp:DropDownList ID="ddlClaseMulti" runat="server" CssClass="formaBuscar">
                        <asp:ListItem Selected="true" Value="-" Text="Seleccione la clase"></asp:ListItem>
                        <asp:ListItem Value="Y" Text="Econ&#243;mica"></asp:ListItem>
                        <asp:ListItem Text="Ejecutiva" Value="C"></asp:ListItem>
                        <asp:ListItem Value="F" Text="Primera"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <!--- escalas  -->
        <asp:RadioButtonList ID="rblMultiEscala" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Selected="true" Value="1" Text="Con Escalas"></asp:ListItem>
            <asp:ListItem Value="0" Text="Directos"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="warningBuscador" id="divError">
        <asp:Label ID="lblErrorGen" runat="server"></asp:Label>
    </div>
</div>

<!-- Script que selecciona la caja de texto completa  -->
<script type="text/javascript">
    $(document).ready(function () {
        $(".formaBuscar").focus(function () {
            this.select();
        });
        $(".formaBuscar").mouseup(function (e) {
            e.preventDefault();
        });
    }); 
</script>
