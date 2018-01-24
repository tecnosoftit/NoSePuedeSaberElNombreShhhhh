<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuscadorSeguro.ascx.cs" Inherits="uc_ucBuscadorSeguro" %>
<!--- seguros -->

<div class="tabSeguros">
    <img src="../App_Themes/Imagenes/imagesNactur/iconoSeguros.png" />

    <div class="labelSeguros">
        SEGUROS
    </div>

    <div class="contenidoBuscador">
        <div id="origen1" class="div100Buscador">
            <asp:Label ID="lbl_Multi_O1" runat="server" Text="Destino" CssClass="labelBuscar"></asp:Label>
            <asp:DropDownList ID="ddlZonaGeo" runat="server" CssClass="formaBuscar">
            </asp:DropDownList>
        </div>     
        <div id="fechaSalida" class="div100Buscador">
            <asp:Label ID="lblFechaSeguro" runat="server" Text="Fecha Salida" CssClass="labelBuscar"></asp:Label>
            <asp:TextBox ID="txtFechaSalidaTarjetas" CssClass="datepicker" runat="server"></asp:TextBox>
            <asp:Label ID="lblFechaViajeE" CssClass="Error" runat="server"></asp:Label>
        </div>
        <div id="fechaRegreso" class="div100Buscador">
            <asp:Label ID="Label1" runat="server" Text="Fecha Regreso" CssClass="labelBuscar"></asp:Label>
            <asp:TextBox ID="txt2TFechaRegresoTarjetas" CssClass="datepicker" runat="server"></asp:TextBox>
            <asp:Label ID="Label2" CssClass="Error" runat="server"></asp:Label>
        </div>

        <div class="div100Buscador">
            <asp:Label ID="Label3" runat="server" Text="Cantidad de personas" CssClass="labelBuscar"></asp:Label>            
            <ajax:UpdatePanel ID="upPax" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlCantidadPax" runat="server" OnSelectedIndexChanged="ddlCantidadPax_Selected" AutoPostBack="true">
                        <asp:ListItem Text="1"></asp:ListItem>
                        <asp:ListItem Text="2"></asp:ListItem>
                        <asp:ListItem Text="3"></asp:ListItem>
                        <asp:ListItem Text="4"></asp:ListItem>
                        <asp:ListItem Text="5"></asp:ListItem>
                        <asp:ListItem Text="6"></asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
            </ajax:UpdatePanel>
        </div>

        
        <ajax:UpdatePanel ID="upEdades" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="rptEdadPax" runat="server">                                                                                    
                    <ItemTemplate>
                        <div class="div100Buscador">
                            <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strPax") %>' CssClass="labelBuscar"></asp:Label>
                            <asp:TextBox ID="txtNacimientoFecha" runat="server"></asp:TextBox>                    
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </ajax:UpdatePanel>
        
        <div class="tablaBuscadorVuelos" style="text-align: right">
            <asp:Button ID="lbBuscar" runat="server" CssClass="botonBuscador" OnClientClick="Show_CortinillaTab(5);" Text=""
                   CommandName="Seguros" OnCommand="setCommand"></asp:Button><br /><br />
            <div class="warningBuscador" id="divError">
                <asp:Label ID="lblErrorGen" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</div>  