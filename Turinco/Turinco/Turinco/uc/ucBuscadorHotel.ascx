<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuscadorHotel.ascx.cs" Inherits="uc_ucBuscadorHotel" %>
<!--- Hotel -->
<div class="contenidoBuscador hotel_f">
    <div class="tablaBuscadorHoteles trayectos" style="display:none;">
        <asp:RadioButtonList ID="modal_hotel" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="0"  Text="Nacionales"></asp:ListItem>
            <asp:ListItem Value="1" Selected="True" Text="Internacionales"></asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <!--- destino  -->
    <div class="div100Buscador">
        <div class="titulo">
            <asp:Label ID="lblCiudadH" runat="server" Text="Ciudad"></asp:Label>
        </div>
        <div class="forma" id="hotel_internal">
            <asp:TextBox ID="txtCiudadDestino" CssClass="formaBuscar" runat="server"></asp:TextBox>
        </div>
        <div class="forma" id="hotel_nal" style="display:none;">
            <div>
                <asp:DropDownList ID="ddlCiudades" runat="server" CssClass="formaBuscarCombo">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="div50Buscador">
        <div class="titulo">
            <asp:Label ID="lblTFechaIngresoH" runat="server" Text="Fecha ingreso"></asp:Label>
        </div>
        <div class="forma">
            <asp:TextBox ID="txtFechaIngreso" CssClass="datepicker formaBuscar" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="div50Buscador">
        <div class="titulo">
            <asp:Label ID="lblTNochesH" runat="server" Text="Noches"></asp:Label>
        </div>
        <div class="forma">
            <ajax:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:DropDownList ID="cmbNoches" runat="server" CssClass="formaBuscar">
                            <asp:ListItem Value="1" Selected="True">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Value="8">8</asp:ListItem>
                            <asp:ListItem Value="9">9</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                            <asp:ListItem Value="24">24</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="26">26</asp:ListItem>
                            <asp:ListItem Value="27">27</asp:ListItem>
                            <asp:ListItem Value="28">28</asp:ListItem>
                            <asp:ListItem Value="29">29</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </ContentTemplate>
            </ajax:UpdatePanel>
        </div>
    </div>
    <div class="div50Buscador">
        <div class="titulo">
            <asp:Label ID="lblTFechaSalidaH" runat="server" Text="Fecha salida"></asp:Label>
        </div>
        <div class="forma">
            <ajax:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:TextBox ID="txt2HFechaSalida" runat="server" CssClass="datepicker formaBuscar" Enabled="true"></asp:TextBox>
                    <br />
                    <div class="alineacionCentro" runat="server" id="Div2">
                        <ajax:UpdateProgress ID="udpEsperar" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
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
    </div>

    <div class="tablaBuscadorVuelos" id="pasajeros" style="width:100%;">
        <div class="habitacionColumna">
            <table style="float:left;">
                <thead>
                    <tr>
                        <td>
                            <asp:Label ID="lblTHabitacion" runat="server" Text="Habitación"></asp:Label>
                        </td>
                    </tr>
                </thead>
                <tr>
                    <td>
                        <div style="text-align:center;">
                            <asp:DropDownList ID="cmbHabitaciones" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="huespedesColumna">
            <!--tabla habitaciones hoteles-->
            <table id="tablaHabitacionesHoteles">
                <thead>
                    <tr>
                        <td id="Adultos" style="text-align:center;">
                            Adultos
                        </td>
                        <td id="Ninos" style="text-align:center;">
                            Niños
                        </td>
                        <td id="EdadUno" style="display: none; text-align:center;">
                            Edad uno
                        </td>
                        <td id="EdadDos" style="display: none; text-align:center;">
                            Edad dos
                        </td>
                    </tr>
                </thead>
                <!--habitacion 1-->
                <tr class="fila" style="display: none">
                    <td class="columnaAdultos">
                        <div style="text-align: center;">
                            <asp:DropDownList ID="cmbAdultos1" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaNinos" style="text-align: center;">
                        <div>
                            <asp:DropDownList ID="cmbNiños1" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad1" style="display: none; text-align:center;">
                        <div>
                            <asp:DropDownList ID="ddlEdad11" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad2" style="display: none; text-align:center;">
                        <div>
                            <asp:DropDownList ID="ddlEdad12" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <!--habitacion 2-->
                <tr class="fila" style="display: none">
                    <td class="columnaAdultos">
                        <div>
                            <asp:DropDownList ID="cmbAdultos2" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaNinos">
                        <div>
                            <asp:DropDownList ID="cmbNiños2" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad1" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad21" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad2" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad22" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <!--habitacion 3-->
                <tr class="fila" style="display: none">
                    <td class="columnaAdultos">
                        <div>
                            <asp:DropDownList ID="cmbAdultos3" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaNinos">
                        <div>
                            <asp:DropDownList ID="cmbNiños3" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad1" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad31" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad2" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad32" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <!--habitacion 4-->
                <tr class="fila" style="display: none">
                    <td class="columnaAdultos">
                        <div>
                            <asp:DropDownList ID="cmbAdultos4" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaNinos">
                        <div>
                            <asp:DropDownList ID="cmbNiños4" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad1" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad41" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad2" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad42" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <!--habitacion 5-->
                <tr class="fila" style="display: none">
                    <td class="columnaAdultos">
                        <div>
                            <asp:DropDownList ID="cmbAdultos5" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaNinos">
                        <div>
                            <asp:DropDownList ID="cmbNiños5" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad1" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad51" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad2" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad52" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <!--habitacion 6-->
                <tr class="fila" style="display: none">
                    <td class="columnaAdultos">
                        <div>
                            <asp:DropDownList ID="cmbAdultos6" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaNinos">
                        <div>
                            <asp:DropDownList ID="cmbNiños6" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad1" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad61" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad2" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad62" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <!--habitacion 7-->
                <tr class="fila" style="display: none">
                    <td class="columnaAdultos">
                        <div>
                            <asp:DropDownList ID="cmbAdultos7" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaNinos">
                        <div>
                            <asp:DropDownList ID="cmbNiños7" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad1" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad71" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad2" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad72" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <!--habitacion 8-->
                <tr class="fila" style="display: none">
                    <td class="columnaAdultos">
                        <div>
                            <asp:DropDownList ID="cmbAdultos8" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaNinos">
                        <div>
                            <asp:DropDownList ID="cmbNiños8" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad1" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad81" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad2" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad82" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <!--habitacion 9-->
                <tr class="fila" style="display: none">
                    <td class="columnaAdultos">
                        <div>
                            <asp:DropDownList ID="cmbAdultos9" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaNinos">
                        <div>
                            <asp:DropDownList ID="cmbNiños9" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad1" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad91" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td class="columnaEdad2" style="display: none">
                        <div>
                            <asp:DropDownList ID="ddlEdad92" runat="server">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="tablaBuscadorVuelos" style="display: none">
        <asp:Label ID="lblTNombreHotel" runat="server" Text="Nombre del hotel"></asp:Label><br />
        <asp:TextBox ID="txtNombreHotel" runat="server" Width="265" CssClass="formaBuscar"></asp:TextBox>
    </div>
    <div class="tablaBuscadorVuelos" style="display: none">
        <asp:Label ID="lblTCategoriaH" runat="server" Text="Categoria"></asp:Label><br />
        <asp:DropDownList ID="cmbClasificacion" runat="server" CssClass="camposBuscador"
            Width="270">
            <asp:ListItem Value="" Text="Mostrar Todas"></asp:ListItem>
            <asp:ListItem Value="1EST" Text="1 estrella"></asp:ListItem>
            <asp:ListItem Value="2EST" Text="2 estrellas"></asp:ListItem>
            <asp:ListItem Value="3EST" Text="3 estrellas"></asp:ListItem>
            <asp:ListItem Value="4EST" Text="4 estrellas"></asp:ListItem>
            <asp:ListItem Value="5EST" Text="5 estrellas"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="tablaBuscadorBoton">
        <asp:Button ID="lbBuscar" runat="server" CssClass="botonBuscador" OnClientClick="Show_Cortinilla();"
            Text="Buscar" ValidationGroup="buscadorHoteles" CommandName="Hoteles" OnCommand="setCommand" style="margin-top:5px;">
        </asp:Button>
    </div>
</div>
<div class="warningBuscador" id="divError">
    <asp:Label ID="lblErrorGen" runat="server" ForeColor="#186e9b"></asp:Label>
</div>
