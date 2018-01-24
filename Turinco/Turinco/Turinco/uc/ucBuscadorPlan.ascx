<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuscadorPlan.ascx.cs"
    Inherits="uc_ucBuscadorPlan" %>
<!--- paquetes -->
<asp:UpdatePanel ID="upBuscador" runat="server">
    <ContentTemplate>
        <div class="full iblock contenidoBuscador">
            <!--- Filtro de texto -->
            <div class="div100Buscador">
                <asp:TextBox runat="server" CssClass="formaBuscar ac_input" placeholder="Búsqueda por palabra clave"
                    ID="txtFiltroTexto" Text="" />
            </div>
            <!--- zona -->
            <div class="div100Buscador">
                <ajax:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblTZona" runat="server" Text="Zona geográfica" class="titcomp labelBuscar"></asp:Label>
                        <asp:DropDownList ID="ddlZonaGeo" runat="server" AutoPostBack="true" CssClass="formaBuscar"
                            OnSelectedIndexChanged="ddlZona_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>
            <!--- pais -->
            <div class="div100Buscador">
                <ajax:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblTPais" runat="server" Text="País" class="titcomp labelBuscar"></asp:Label>
                        <asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="true" CssClass="formaBuscar"
                            OnSelectedIndexChanged="ddlPais_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>
            <!--- ciudad -->
            <div class="div100Buscador">
                <ajax:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblTCiudad" runat="server" Text="Ciudad" class="titcomp labelBuscar"></asp:Label>
                        <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="formaBuscar">
                        </asp:DropDownList>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>
            <!--- tipologia -->
            <div class="div100Buscador">
                <ajax:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblTipologia" runat="server" Text="Actividad" class="titcomp labelBuscar"></asp:Label>
                        <asp:DropDownList ID="ddlTipologia" runat="server" CssClass="formaBuscar" AutoPostBack="false">
                        </asp:DropDownList>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>
            <!--- categoria de plan-->
            <%--<div class="tablaBuscadorPlanes">
                <ajax:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblTipoPlan" runat="server" Text="Servicios incluidos"></asp:Label><br />
                        <asp:DropDownList ID="ddlTipoPlan" runat="server" CssClass="formaBuscar">
                        </asp:DropDownList>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </div>--%>
            <!--- fecha de viaje -->
            <%-- <div class="tablaBuscadorPlanes" style="display:none;">
                <asp:Label ID="lblFechaViaje" runat="server" Text="Fecha de viaje"></asp:Label><br />
                <asp:TextBox ID="txtFechaViaje" CssClass="datepicker" runat="server"></asp:TextBox>
                <asp:Label ID="lblFechaViajeE" CssClass="Error" runat="server"></asp:Label>
            </div>--%>
            <div class="contenedorBotonBuscar" style="float: none; margin-top: 2px; margin-right: 17px;">
                <asp:Button ID="lbBuscar" runat="server" class="link-button white" ValidationGroup="2"
                    OnClientClick="javascript:Show_Cortinilla();try{$('#ucResultadoPlanes_btnCerrar2').click()}catch(ex){};"
                    Text="Buscar" CommandName="PlanesC" OnCommand="setCommand"></asp:Button>
            </div>
            <%--   <ajax:CascadingDropDown ID="cddZona" runat="server" Category="Zona" Enabled="True"
            LoadingText="Cargando zonas" PromptText="Seleccione zona" ServiceMethod="GetZonas"
            ServicePath="../ServiciosLocales/Planes.asmx" TargetControlID="ddlZonaGeo">
        </ajax:CascadingDropDown>--%>
            <%--  <ajax:CascadingDropDown ID="cddPais" runat="server" Category="Pais" Enabled="True"
            LoadingText="Cargando paises"  PromptText="Seleccione pais"
            ServiceMethod="GetPaises" ServicePath="../ServiciosLocales/Planes.asmx" TargetControlID="ddlPais">
        </ajax:CascadingDropDown>
        <ajax:CascadingDropDown ID="cddCiudad" runat="server" Category="Ciudad" Enabled="True"
            LoadingText="Cargando ciudades" ParentControlID="ddlPais" PromptText="Seleccione ciudad"
            ServiceMethod="GetCiudades" ServicePath="../ServiciosLocales/Planes.asmx" TargetControlID="ddlCiudad">
        </ajax:CascadingDropDown>--%>
        </div>
        <div class="progressBarBuscador" style="text-align: center" runat="server" id="Div1">
            <ajax:UpdateProgress ID="udpEsperar" runat="server" AssociatedUpdatePanelID="upBuscador">
                <ProgressTemplate>
                    <div class="progressbar">
                    </div>
                    <img alt="" src="../App_Themes/Imagenes/loading.gif" /><br />
                    <asp:Label ID="lblEsperar" runat="server" Text="Espere por favor..."></asp:Label>
                </ProgressTemplate>
            </ajax:UpdateProgress>
        </div>
        <div class="warningBuscador" id="divError">
            <asp:Label ID="lblErrorGen" runat="server" ForeColor="#186e9b"></asp:Label>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>