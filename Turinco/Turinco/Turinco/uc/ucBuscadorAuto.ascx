<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuscadorAuto.ascx.cs" Inherits="uc_ucBuscadorAuto" %>
    <!--- autos -->
    <div class="contenidoBuscador">
            <!--- ciudad recoge -->
            <div class="tablaBuscadorVuelos">
                <asp:Label ID="lblTCiudadRecoge" runat="server" Text="Ciudad Recoge"></asp:Label>
                <br />
                <asp:TextBox ID="txtCarCiudRecoge" CssClass="formaBuscar" runat="server"></asp:TextBox>
                <asp:Label ID="lblCarCiudRecogeE" CssClass="Error" runat="server"></asp:Label>
            </div>
            <!--- fecha recoge -->
            <div class="tablaBuscadorAutos">
                <asp:Label ID="lblTFechaRecoge" runat="server" Text="Fecha Recoge"></asp:Label><br />
                <asp:TextBox ID="txtCarFechaRecoge" CssClass="datepicker" runat="server"></asp:TextBox>
                <asp:Label ID="lblCarFechaRecogeE" CssClass="Error" runat="server"></asp:Label>
            </div>
            <!--- hora recoge -->
            <div class="tablaBuscadorAutos">
                <asp:Label ID="lblTHoraRecoge" runat="server" Text="Hora Recoge"></asp:Label><br />
                <asp:DropDownList ID="ddlCarHoraRecoge" runat="server" CssClass="formaBuscar">
                    <asp:ListItem Selected="True" Text="07:00 am" Value="07:00:00"></asp:ListItem>
                    <asp:ListItem Text="08:00 am" Value="08:00:00"></asp:ListItem>
                    <asp:ListItem Text="09:00 am" Value="09:00:00"></asp:ListItem>
                    <asp:ListItem Text="10:00 am" Value="10:00:00"></asp:ListItem>
                    <asp:ListItem Text="11:00 am" Value="11:00:00"></asp:ListItem>
                    <asp:ListItem Text="12:00 m" Value="12:00:00"></asp:ListItem>
                    <asp:ListItem Text="01:00 pm" Value="13:00:00"></asp:ListItem>
                    <asp:ListItem Text="02:00 pm" Value="14:00:00"></asp:ListItem>
                    <asp:ListItem Text="03:00 pm" Value="15:00:00"></asp:ListItem>
                    <asp:ListItem Text="04:00 pm" Value="16:00:00"></asp:ListItem>
                    <asp:ListItem Text="05:00 pm" Value="17:00:00"></asp:ListItem>
                    <asp:ListItem Text="06:00 pm" Value="18:00:00"></asp:ListItem>
                    <asp:ListItem Text="07:00 pm" Value="19:00:00"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <!--- ciudad entrega -->
            <div class="tablaBuscadorVuelos">
                <asp:Label ID="lblTCiudadEntrega" runat="server" Text="Ciudad Entrega"></asp:Label>
                <br />
                <asp:TextBox ID="txtCarCiudEntrega" CssClass="formaBuscar" runat="server"></asp:TextBox>
                <asp:Label ID="lblCarCiudEntregaE" CssClass="Error" runat="server"></asp:Label>
            </div>
            <!--- fecha entrega -->
            <div class="tablaBuscadorAutos">
                <asp:Label ID="lblTFechaEntrega" runat="server" Text="Fecha Entrega"></asp:Label><br />
                <asp:TextBox ID="txt2CFechaEntrega" CssClass="datepicker" runat="server"></asp:TextBox>
                <asp:Label ID="lblCarFechaEntregaE" CssClass="Error" runat="server"></asp:Label>
            </div>
            <!--- hora entrega -->
            <div class="tablaBuscadorAutos">
                <asp:Label ID="lblTHoraEntrega" runat="server" Text="Hora Entrega"></asp:Label><br />
                <asp:DropDownList ID="ddlCarHoraEntrega" runat="server" CssClass="formaBuscar">
                    <asp:ListItem Selected="True" Text="07:00 am" Value="07:00:00"></asp:ListItem>
                    <asp:ListItem Text="08:00 am" Value="08:00:00"></asp:ListItem>
                    <asp:ListItem Text="09:00 am" Value="09:00:00"></asp:ListItem>
                    <asp:ListItem Text="10:00 am" Value="10:00:00"></asp:ListItem>
                    <asp:ListItem Text="11:00 am" Value="11:00:00"></asp:ListItem>
                    <asp:ListItem Text="12:00 m" Value="12:00:00"></asp:ListItem>
                    <asp:ListItem Text="01:00 pm" Value="13:00:00"></asp:ListItem>
                    <asp:ListItem Text="02:00 pm" Value="14:00:00"></asp:ListItem>
                    <asp:ListItem Text="03:00 pm" Value="15:00:00"></asp:ListItem>
                    <asp:ListItem Text="04:00 pm" Value="16:00:00"></asp:ListItem>
                    <asp:ListItem Text="05:00 pm" Value="17:00:00"></asp:ListItem>
                    <asp:ListItem Text="06:00 pm" Value="18:00:00"></asp:ListItem>
                    <asp:ListItem Text="07:00 pm" Value="19:00:00"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <!--- rentadora -->
            <div class="tablaBuscadorVuelos">
                <asp:Label ID="lblTRentadora" runat="server" Text="Rentadora"></asp:Label><br />
                <asp:DropDownList ID="ddlRentadora" runat="server" CssClass="formaBuscar">
                </asp:DropDownList>
            </div>
            <!--- tipo auto -->
            <div class="tablaBuscadorVuelos">
                <asp:Label ID="lblTTipoAuto" runat="server" Text="Tipo de Auto"></asp:Label><br />
                <asp:DropDownList ID="ddlTipoAuto" runat="server" CssClass="formaBuscar">
                </asp:DropDownList>
            </div>
            <ajax:CascadingDropDown ID="cddTipoAuto" runat="server" Category="Auto" Enabled="True"
                LoadingText="Cargando Tipos Autos" PromptText="Todos los Autos" ServiceMethod="GetAutos"
                ServicePath="../ServiciosLocales/Planes.asmx" TargetControlID="ddlTipoAuto">
            </ajax:CascadingDropDown>
            <ajax:CascadingDropDown ID="cddRentadora" runat="server" Category="Auto" Enabled="True"
                LoadingText="Cargando Rentadoras" PromptText="Todas las Rentadoras" ServiceMethod="GetRentadora"
                ServicePath="../ServiciosLocales/Planes.asmx" TargetControlID="ddlRentadora">
            </ajax:CascadingDropDown>
            <div class="tablaBuscadorVuelos" style="text-align: right">
                <asp:Button ID="lbCar" CssClass="botonBuscador" runat="server" Text="Buscar autos"  CommandName="Car" OnCommand="setCommand" OnClientClick="Show_Cortinilla()">
                </asp:Button>
            </div>
    </div>
    <div>    
        <asp:Label ID="lblErrorGen" runat="server" ForeColor="red"></asp:Label>
    </div>
