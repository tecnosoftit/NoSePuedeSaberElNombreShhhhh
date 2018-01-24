<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucPlanes.ascx.cs" Inherits="uc_ucPlanesHome" %>
<!-- resultados -->
<!-- masthead -->
<div id="masthead">
    <div class="ContenedorPlanes">
        <span class="head" style="width: 14%; display: inline-block; position: absolute;
            margin-top: 2px;">Planes</span>
        <!-- <span class="subhead">Aquí esta su mejor opción</span> -->
        <div style="width: 80%; display: inline-block; margin-left: 135px;">
            <!-- Filtros -->
            <table class="filtrosPlanes" width="100%" cellpadding="0">
                 <tr>
            <td>
                <asp:Label ID="label2" runat="server" Text="" class="titcomp labelBuscar"></asp:Label>
                <asp:TextBox runat="server" CssClass="formaBuscar ac_input" placeholder="Búsqueda por palabra clave"
                    ID="txtFiltroTexto" Text="" style="  width: 185px; height: 12px; margin-top: 22px; margin-left: -21px;"/>
            </td>
            <td>
                <ajax:UpdatePanel ID="UpdatePanel4" runat="server" Style="width: 206px;">
                    <ContentTemplate>
                        <asp:Label ID="label45" runat="server" Text="Zona geográfica" class="titcomp labelBuscar"></asp:Label>
                        <asp:DropDownList ID="ddlZonaGeo" runat="server" AutoPostBack="true" CssClass="formaBuscarPlanes"
                            OnSelectedIndexChanged="ddlZona_SelectedIndexChanged" Style="width: 175px;">
                        </asp:DropDownList>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </td>
            <td>
                <ajax:UpdatePanel ID="UpdatePanel1" runat="server" Style="width: 189px;">
                    <ContentTemplate>
                        <asp:Label ID="Label3" runat="server" Text="País" class="titcomp labelBuscar"></asp:Label>
                        <asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="true" CssClass="formaBuscarPlanes"
                            OnSelectedIndexChanged="ddlPais_SelectedIndexChanged" Style="width: 175px;">
                        </asp:DropDownList>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </td>
            <td>
                <ajax:UpdatePanel ID="UpdatePanel3" runat="server" Style="width: 100px;">
                    <ContentTemplate>
                        <asp:Label ID="lblTCiudad" runat="server" Text="Ciudad" class="titcomp labelBuscar"></asp:Label>
                        <asp:DropDownList ID="ddlCiudad" runat="server" CssClass="formaBuscarPlanes" Style="width: 100px;">
                        </asp:DropDownList>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </td>
            <td>
                <ajax:UpdatePanel ID="UpdatePanel2" runat="server" Style="width: 135px;">
                    <ContentTemplate>
                        <asp:Label ID="Label5" runat="server" Text="Actividad" class="titcomp labelBuscar"></asp:Label>
                        <asp:DropDownList ID="ddlTipologia" runat="server" CssClass="formaBuscarPlanes" Style="width: 135px;  margin-left: 11px;">
                        </asp:DropDownList>
                    </ContentTemplate>
                </ajax:UpdatePanel>
            </td>
            <td>
                <asp:Button ID="lbBuscar" runat="server" class="link-button white" ValidationGroup="2"
                    OnClientClick="javascript:Show_Cortinilla();try{$('#ucResultadoPlanes_btnCerrar2').click()}catch(ex){};"
                    Text="Filtrar" CommandName="PlanesD" OnCommand="setCommand" Style="width: 74px;
                    height: 28px; margin-top: 23px;margin-left: 22px; font-weight: bold; border-radius: 7px;"></asp:Button>
            </td>
        </tr>
            </table>
            <!-- Fin filtros -->
        </div>
    </div>
</div>
<!-- ENDS masthead -->
<div class="featured portfolio-list masonry">
    <asp:Repeater ID="dtlOfertas" runat="server">
        <ItemTemplate>
        <figure style="min-height: 349px;">
                <div class="imagenOfertaPlan">
                     <asp:Image runat="server" Visible="false" ImageUrl="~/App_Themes/Imagenes/ofertaPlan.png" ID="imgOferta" />
                </div>
            
                <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla();" class="thumb">
                    <img alt="Promo 1" alt='<%# DataBinder.Eval(Container,"DataItem.Nombre") %>' src='<%# DataBinder.Eval(Container,"DataItem.strImagen") %>'/>
                     
                </a>
                <div class="ContendedorDescripcionPlan">
                        <h4 href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' onclick="Show_Cortinilla_Interna()">
                        <%# DataBinder.Eval(Container,"DataItem.Nombre") %>
                        </h4>
                    <h5 class="color1">
                        <span class="noneDisplay">
                            <asp:Label ID="lblTDesde" runat="server" Text="Desde "></asp:Label>
                        </span>
                        <asp:Label Visible="false" ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalleClasificacion") %>'></asp:Label>
                        <asp:Label CssClass="moneda" ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strRefereMoneda")%>'></asp:Label>           
                        <asp:Label CssClass="valor" ID="Label1" runat="server" Text='<%# Decimal.Parse(DataBinder.Eval(Container,"DataItem.dblPrecio").ToString()).ToString("###,###.##") %>'></asp:Label>           
                    </h5>
                        <div class="imagenCategoria" style="display:none;">
                            <asp:Image ID="ImageButton2" runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.urlImagenCategoria") %>' />
                        </div>
                        <a href='<%# DataBinder.Eval(Container,"DataItem.Url") %>' class="heading" onclick="Show_Cortinilla();">
                            VER MÁS
                        </a>
                         
                </div>
            </figure>
        </ItemTemplate>
    </asp:Repeater>
</div>
<div class="paginadorPaquetes1">
    <asp:Button ID="Button2" OnCommand="Button1_Command" CssClass="botonAdelante fRight gris"
        CommandName="Next" runat="server" Text="Adelante" Style="display: none;"></asp:Button>
    <div class="contenedorNumerosPaginacion fRight">
        <table>
            <tr>
                <td>
                    <asp:DataList ID="dtlPaginador" runat="server" OnItemCommand="dtlPaginador_ItemCommand"
                        RepeatDirection="Horizontal">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass='<%# DataBinder.Eval(Container,"DataItem.Class") %>'
                                Text='<%# DataBinder.Eval(Container,"DataItem.Pagina") %>' CommandName="dlPagNoticias"
                                ID="lBIndice"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>
    </div>
    <asp:Button ID="Button1" CssClass="botonAtras fRight gris" CommandName="Back" runat="server"
        Text="Atrás" OnCommand="Button1_Command" Style="display: none;"></asp:Button>
</div>
