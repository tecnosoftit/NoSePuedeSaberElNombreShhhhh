<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDetalleVuelo.ascx.cs" Inherits="uc_ucDetalleVuelo" %>

<!--- itinerario -->
<div class="completo">
    <!--- box border -->
    <div class="plb">
    <div class="prb">
    <div class="pbb"><div class="pblc"><div class="pbrc">
    <div class="ptb"><div class="ptlc"><div class="ptrc">
    
    <div class="contenidoCompleto">
        <asp:Label CssClass="tituloPromociones" ID="lblTItinerario" runat="server" Text="Itinerario Seleccionado"></asp:Label>
        <br /><br />
        <table width="890" cellpadding="2" cellspacing="1" class="bordeTabla">
            <tr>
                <td><asp:Label CssClass="bold" ID="lblTOrigen" runat="server" Text="Origen"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="lblTFechaSalida" runat="server" Text="Fecha Salida"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="lblTHoraSalida" runat="server" Text="Hora Salida"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="lblTAdultos" runat="server" Text="Adultos"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="lblTNinos" runat="server" Text="Niños"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="lblTInfantes" runat="server" Text="Infantes"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblOrigen" runat="server" Text="Bogota (BOG)"></asp:Label></td>
                <td><asp:Label ID="lblFechaSalida" runat="server" Text="Jue, 21 Ago 09"></asp:Label></td>
                <td><asp:Label ID="lblHoraSalida" runat="server" Text="1:00 PM"></asp:Label></td>
                <td><asp:Label ID="lblAdultos" runat="server" Text="2"></asp:Label></td>
                <td><asp:Label ID="lblNinos" runat="server" Text="1"></asp:Label></td>
                <td><asp:Label ID="lblInfantes" runat="server" Text="0"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label CssClass="bold" ID="lblTDestino" runat="server" Text="Destino"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="lblTFechaRegreso" runat="server" Text="Fecha Regreso"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="lblTHoraRegreso" runat="server" Text="Hora Regreso"></asp:Label></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblDestino" runat="server" Text="Cali (CAL )"></asp:Label></td>
                <td><asp:Label ID="lblFechaRegreso" runat="server" Text="Jue, 21 Ago 09"></asp:Label></td>
                <td><asp:Label ID="lblHoraRegreso" runat="server" Text="2:00 PM"></asp:Label></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    
                </td>
                <td colspan="3" class="alineacionDerecha">
                    <asp:Button ID="ibBuscartarjeta" CssClass="botonBuscar" runat="server" Text="Regresar a resultados"></asp:Button>
                </td>
            </tr>
        </table>    
    </div>
    
    <!--- end of box border -->
    </div></div></div></div>
    </div></div></div></div>
</div>

<!--- detalle vuelo -->
<div class="completo">
    <!--- box border -->
    <div class="plb">
    <div class="prb">
    <div class="pbb"><div class="pblc"><div class="pbrc">
    <div class="ptb"><div class="ptlc"><div class="ptrc">
    
    <div class="contenidoCompleto">
        <asp:Label CssClass="tituloPromociones" ID="Label2" runat="server" Text="Detalle del Vuelo"></asp:Label>
        <br /><br />
        <table width="890" cellpadding="2" cellspacing="1" class="bordeTabla">
            <tr class="tituloTabla">
                <td class="alineacionCentro"><asp:Label CssClass="bold" ID="Label12" runat="server" Text="Aerolinea"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="Label21" runat="server" Text="Fecha y Hora Salida"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="Label23" runat="server" Text="Fecha y Hora Llegada"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="Label24" runat="server" Text="Tiempo de Vuelo"></asp:Label></td>
            </tr>                          
            <tr class="alineacionSuperior">
                <td class="alineacionCentro">
                    <asp:Label CssClass="bold" ID="Label25" runat="server" Text="Bogota"></asp:Label>
                    <asp:Label CssClass="bold" ID="Label26" runat="server" Text="-"></asp:Label>
                    <asp:Label CssClass="bold" ID="Label27" runat="server" Text="Cali"></asp:Label><br />
                    <asp:Label CssClass="bold" ID="Label76" runat="server" Text="Avianca"></asp:Label><br />
                    <asp:Label ID="Label77" runat="server" Text="Vuelo: "></asp:Label>
                    <asp:Label ID="Label78" runat="server" Text="01256"></asp:Label><br />
                    <asp:Image ID="Image1" runat="server" ImageUrl="../App_Themes/Imagenes/Airline/AV.gif" />
                </td>
                <td>
                    <asp:Label CssClass="bold" ID="Label28" runat="server" Text="12:20 PM"></asp:Label><br />
                    <asp:Label ID="Label29" runat="server" Text="Bogota (BOG)"></asp:Label><br />
                    <asp:Label ID="Label31" runat="server" Text="(Jose Maria Cordova)"></asp:Label><br />
                    <asp:Label ID="Label30" runat="server" Text="Jue, 21 Ago 09"></asp:Label>
                </td>
                <td>
                    <asp:Label CssClass="bold" ID="Label32" runat="server" Text="12:20 PM"></asp:Label><br />
                    <asp:Label ID="Label33" runat="server" Text="Bogota (BOG)"></asp:Label><br />
                    <asp:Label ID="Label34" runat="server" Text="(Jose Maria Cordova)"></asp:Label><br />
                    <asp:Label ID="Label35" runat="server" Text="Jue, 21 Ago 09"></asp:Label>
                </td>
                <td class="alineacionCentro">
                    <asp:Label CssClass="bold" ID="Label36" runat="server" Text="00:58"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td colspan="4"><hr class="divisionVuelos" /></td>
            </tr>
            <tr class="alineacionSuperior">
                <td class="alineacionCentro">
                    <asp:Label CssClass="bold" ID="Label38" runat="server" Text="Bogota - Medellin"></asp:Label><br />
                    <asp:Label ID="Label39" runat="server" Text="Vuelo: 9546"></asp:Label><br />
                    <asp:Label ID="Label40" runat="server" Text="Avianca"></asp:Label><br />
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Imagenes/Airline/AV.gif" />
                </td>
                <td>
                    <asp:Label ID="Label41" runat="server" Text="Lun, 13 Abr 2009"></asp:Label><br />
                    <asp:Label ID="Label42" runat="server" Text="6:00 am"></asp:Label><br />
                    <asp:Label ID="Label43" runat="server" Text="Bogota, Colombia"></asp:Label><br />
                    <asp:Label ID="Label44" runat="server" Text="(El Dorado)"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label45" runat="server" Text="Lun, 13 Abr 2009"></asp:Label><br />
                    <asp:Label ID="Label46" runat="server" Text="6:00 am"></asp:Label><br />
                    <asp:Label ID="Label47" runat="server" Text="Medellin, Colombia"></asp:Label><br />
                    <asp:Label ID="Label48" runat="server" Text="(Jose Maria Cordova)"></asp:Label>
                </td>
                <td class="alineacionCentro">
                    <asp:Label ID="Label49" runat="server" Text="00:58"></asp:Label><br />
                    <asp:Label ID="Label50" runat="server" Text="Paradas 0"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    
    <!--- end of box border -->
    </div></div></div></div>
    </div></div></div></div>
</div>

<!--- detalle tarifa -->
<div class="completo">
    <!--- box border -->
    <div class="plb">
    <div class="prb">
    <div class="pbb"><div class="pblc"><div class="pbrc">
    <div class="ptb"><div class="ptlc"><div class="ptrc">
    
    <div class="contenidoCompleto">
        <asp:Label CssClass="tituloPromociones" ID="Label15" runat="server" Text="Detalle del Vuelo"></asp:Label>
        <br /><br />
        <table width="890" cellpadding="2" cellspacing="1" class="bordeVuelos">
            <tr class="alineacionCentro">
                <td colspan="3">
                    <asp:Label CssClass="tituloValor" ID="Label51" runat="server" Text="Valor Total de Viaje"></asp:Label>
                    <asp:Label CssClass="tituloPrecioTotalVuelo" ID="Label52" runat="server" Text="COP 1.520.000"></asp:Label> 
                </td>
            </tr>
            <tr>
                <td>
                    <table width="200" cellpadding="0" cellspacing="0">
                        <tr>
                            <td><asp:Label ID="Label55" runat="server" Text="Tarifa 1 Adulto"></asp:Label></td>
                            <td class="alineacionDerecha"><asp:Label ID="Label56" runat="server" Text="310.000"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <a class="tarifa" href="#">
                                    <asp:Label ID="Label54" runat="server" Text="Impuestos y Tarifas"></asp:Label>
                                    <span>
                                        <table align="left">
                                            <tr>
                                                <td><strong><asp:Label ID="Label66" runat="server" Text="Impuesto"></asp:Label></strong></td>
                                                <td><asp:Label ID="Label67" runat="server" Text="COP 25.000"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong><asp:Label ID="Label68" runat="server" Text="Impuesto Combustible"></asp:Label></strong></td>
                                                <td><asp:Label ID="Label69" runat="server" Text="COP 50.000"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong><asp:Label ID="Label70" runat="server" Text="Otros"></asp:Label></strong></td>
                                                <td><asp:Label ID="Label82" runat="server" Text="COP 5.000"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td><strong><asp:Label ID="Label83" runat="server" Text="Tarifa Administrativa"></asp:Label></strong></td>
                                                <td><asp:Label ID="Label84" runat="server" Text="COP 23.000"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </span>
                                </a>
                            </td>
                            <td class="alineacionDerecha">
                                <asp:Label ID="Label85" runat="server" Text="5.652.000"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label CssClass="bold" ID="Label57" runat="server" Text="Tarifa Total"></asp:Label></td>
                            <td class="alineacionDerecha"><asp:Label CssClass="tarifaTotalPersona" ID="Label58" runat="server" Text="320.000"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="200" cellpadding="0" cellspacing="0">
                        <tr>
                            <td><asp:Label ID="Label59" runat="server" Text="Tarifa 1 Niño"></asp:Label></td>
                            <td class="alineacionDerecha"><asp:Label ID="Label60" runat="server" Text="310.000"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label61" runat="server" Text="Impuestos y Tarifas"></asp:Label></td>
                            <td class="alineacionDerecha"><asp:Label ID="Label62" runat="server" Text="10.000"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label63" runat="server" Text="Tarifa Total"></asp:Label></td>
                            <td class="alineacionDerecha"><asp:Label CssClass="tarifaTotalPersona" ID="Label64" runat="server" Text="320.000"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="200" cellpadding="0" cellspacing="0">
                        <tr>
                            <td><asp:Label ID="Label65" runat="server" Text="Tarifa 1 Infante"></asp:Label></td>
                            <td class="alineacionDerecha"><asp:Label ID="Label71" runat="server" Text="310.000"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label72" runat="server" Text="Impuestos y Tarifas"></asp:Label></td>
                            <td class="alineacionDerecha"><asp:Label ID="Label73" runat="server" Text="10.000"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="Label74" runat="server" Text="Tarifa Total"></asp:Label></td>
                            <td class="alineacionDerecha"><asp:Label CssClass="tarifaTotalPersona" ID="Label75" runat="server" Text="320.000"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    <!--- end of box border -->
    </div></div></div></div>
    </div></div></div></div>
</div>

<!--- condiciones de tarifa -->
<div class="completo">
    <!--- box border -->
    <div class="plb">
    <div class="prb">
    <div class="pbb"><div class="pblc"><div class="pbrc">
    <div class="ptb"><div class="ptlc"><div class="ptrc">
    
    <div class="contenidoCompleto">
        <asp:Label CssClass="tituloPromociones" ID="Label16" runat="server" Text="Condiciones de Vuelo"></asp:Label>
        <br /><br />
        <asp:Label ID="Label17" runat="server" Text="condiciones"></asp:Label>
    </div>
    
    <!--- end of box border -->
    </div></div></div></div>
    </div></div></div></div>
</div>

<!--- datos pasajeros -->
<div class="completo">
    <!--- box border -->
    <div class="plb">
    <div class="prb">
    <div class="pbb"><div class="pblc"><div class="pbrc">
    <div class="ptb"><div class="ptlc"><div class="ptrc">
    
    <div class="contenidoCompleto">
        <asp:Label CssClass="tituloPromociones" ID="Label22" runat="server" Text="Datos de los pasajeros"></asp:Label>
        <br /><br />
        <asp:Label ID="Label37" runat="server" Text="condiciones"></asp:Label>
        <table width="890" cellpadding="2" cellspacing="1" class="bordeTabla">
            <tr class="tituloTabla">
                <td><asp:Label CssClass="bold" ID="Label53" runat="server" Text="Tipo Pasajero"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="Label79" runat="server" Text="Trato"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="Label80" runat="server" Text="Edad"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="Label81" runat="server" Text="Nombres"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="Label110" runat="server" Text="Apellidos"></asp:Label></td>
                <td><asp:Label CssClass="bold" ID="Label111" runat="server" Text="Numero Viajero Frecuente"></asp:Label></td>
            </tr>                          
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList Width="80" ID="DropDownList1" runat="server">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox Width="50" ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Width="180" ID="TextBox4" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox Width="180" ID="TextBox5" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    
    <!--- end of box border -->
    </div></div></div></div>
    </div></div></div></div>
</div>