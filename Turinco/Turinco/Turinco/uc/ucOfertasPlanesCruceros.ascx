<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucOfertasPlanesCruceros.ascx.cs"
    Inherits="uc_OfertasPlanesCruceros" %>
<!-- resultados -->
<!-- masthead -->
<div id="masthead">
    <span class="head">Cruceros</span>
    <!-- <span class="subhead">Aquí esta su mejor opción</span> -->
</div>
<!-- ENDS masthead -->
<div class="ContentBuscadorCruceros">
    <iframe frameborder="0" height="114px" width="50%" id="rc-frame" scrolling="no" src="http://cs.cruisebase.com/cs/?skin=629&lid=es">Cruceros</iframe>
</div>
<div id="OcultoCruceros" class="ContentBloqueos">
    <div class="TituloPequeno">
        <span class="head">Nuestros bloqueos</span>
    </div>
    <div  id="ContentCruceros" class="featured portfolio-list masonry">
        <asp:Repeater ID="dtlOfertas" runat="server">
            <ItemTemplate>
                <figure style="min-height: 349px;  margin-left: -37px;  margin-right: 80px;">
                    <div class="imagenOfertaPlan">
                        <asp:Image runat="server" Visible="false" ImageUrl="~/App_Themes/Imagenes/ofertaPlan.png"
                            ID="imgOferta" />
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
                             <asp:Label CssClass="valor" ID="Label1" runat="server" Text='<%# Decimal.Parse(DataBinder.Eval(Container,"DataItem.dblPrecioDesde").ToString()).ToString("###,###.##") %>'></asp:Label>           
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
            <asp:DataList ID="dtlPaginador" runat="server" OnItemCommand="dtlPaginador_ItemCommand"
                RepeatDirection="Horizontal">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CssClass='<%# DataBinder.Eval(Container,"DataItem.Class") %>'
                        Text='<%# DataBinder.Eval(Container,"DataItem.Pagina") %>' CommandName="dlPagNoticias"
                        ID="lBIndice"></asp:LinkButton>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <asp:Button ID="Button1" CssClass="botonAtras fRight gris" CommandName="Back" runat="server"
            Text="Atrás" OnCommand="Button1_Command" Style="display: none;"></asp:Button>
    </div>
</div>

<div class="ContentUtilitarios" id="OcultoFolletos">
<div class="TituloPequeno">
        <span class="head">Promociones</span>
    </div>
    <div id="ContentFolletos">
<asp:Repeater runat="server" ID="rptSeccion">
    <ItemTemplate>
            <div id="ContenedorUtilitarios" class="ContenedorUtilitarios">
                <h4 id="Titulo" style="display:none;">
                    <asp:Label ID="strTitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>
                </h4>
                <img id="imgQuienes" runat="server" alt='<%# DataBinder.Eval(Container.DataItem, "strTitulo") %>' src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen").ToString() %>' class="iblock" style="height: 382px;" />
                <a href='../Presentacion/FolletosCruceros.aspx?CODSEC=<%# DataBinder.Eval(Container,"DataItem.intCodigo") %>&TPOMSJ=TM001&TMMSJ=93369' class="VerMas">
                        Ver más
                </a>
                <div id="descripcion" class="ContenedorDescripcionUtilitarios" style="display:none;">
                    <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                </div>
                <asp:Repeater runat="server" ID="rptSeccion">
                    <ItemTemplate>
                        <%--<asp:Label CssClass="tituloQuienes" ID="strTitulo" runat="server" Text=""><%# DataBinder.Eval(Container,"DataItem.strTitulo") %> </asp:Label>--%>
                        <asp:Label ID="strDescripcion" runat="server" Text=""><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%> </asp:Label>
                    </ItemTemplate>
                </asp:Repeater>  
            </div>           
    </ItemTemplate>
</asp:Repeater>
</div>
</div>

<script>
    //Codigo que se encarga de Ocultar el contenedor folletos
    if (document.getElementById("ContentFolletos").innerHTML == 0) {
        document.getElementById("OcultoFolletos").style.display = "none";
    }

    //Codigo que se encarga de Ocultar el contenedor Cruceros
    if (document.getElementById("ContentCruceros").innerHTML == 0) {
        document.getElementById("OcultoCruceros").style.display = "none";
    }
</script>
