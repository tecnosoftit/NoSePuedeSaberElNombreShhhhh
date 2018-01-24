<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuscador.ascx.cs" Inherits="uc_ucBuscador" %>
<%@ Register Src="ucBuscadorAereo.ascx" TagName="ucBuscadorAereo" TagPrefix="uc1" %>
<%@ Register Src="ucBuscadorHotel.ascx" TagName="ucBuscadorHotel" TagPrefix="uc2" %>
<%@ Register Src="ucBuscadorCrucero.ascx" TagName="ucBuscadorCrucero" TagPrefix="uc3" %>
<%@ Register Src="ucBuscadorPlan.ascx" TagName="ucBuscadorPlan" TagPrefix="uc4" %>
<%@ Register Src="ucBuscadorAuto.ascx" TagName="ucBuscadorAuto" TagPrefix="uc5" %>
<%@ Register Src="ucRotadorBannerBuscador.ascx" TagName="ucRotadorBannerBuscador" TagPrefix="uc6" %>


<div>
    <div id="tabs" class="tabs">
        <ul>
            <li class="pestanaVuelos">
                <a href="#vuelos" class="bcolor1">
                    <img src="../App_Themes/Imagenes/imagesTurinco/icovuelos.png" />
                    <asp:Label ID="lblTVuelos" runat="server"></asp:Label>
                </a>
            </li>
            <li class="pestanaHoteles" style="opacity: 1;">
                <a href="#hoteles" class="bcolor2" style="cursor:pointer;">
                    <img src="../App_Themes/Imagenes/imagesTurinco/icohotel.png" />
                    <asp:Label ID="lblTHoteles" runat="server"></asp:Label>
                </a>
            </li>
            <li class="pestanaPaquetes">
                <a href="#paquetes" class="bcolor3">
                    <img src="../App_Themes/Imagenes/imagesTurinco/icoplanes.png" />
                    <asp:Label ID="lblTPaquetes" runat="server"></asp:Label>
                </a>
            </li>
            <li class="pestanaCruceros">
                <a href="#cruceros" class="bcolor4">
                    <img src="../App_Themes/Imagenes/imagesTurinco/icocrucero.png" />
                    <asp:Label ID="lblTCruceros" runat="server"></asp:Label>
                </a>
            </li>
            
            <li class="pestanaAutos" style="opacity: 1;display:none;">               
                <a href="#autos" onclick='javascript:Redirect()' class="bcolor5" style="cursor:pointer;">
                    <img src="../App_Themes/Imagenes/imagesTurinco/icoautos.png" alt="" />
                    <asp:Label ID="lblTAutos" runat="server" style="color: whitesmoke;"></asp:Label>
                </a>
            </li>
            <li class="pestanaExcurciones">
                <a href="#excurciones" class="bcolor6">
                    <img src="../App_Themes/Imagenes/imagesTurinco/icoescurciones.png" />
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </a>
            </li>
            <li class="pestanaAsistencia">
                <a href="#asistencia" class="bcolor0">
                    <img src="../App_Themes/Imagenes/imagesTurinco/icoasistencia.png" />
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </a>
            </li>

        </ul>
        
        <!--- vuelos -->
        <div id="vuelos" class="bcolor1" style="display:block; box-shadow: 3px 3px 5px;">
            <uc1:ucBuscadorAereo ID="UcBuscadorAereo1" runat="server" />
        </div>
        <!--- hoteles -->
         <div id="hoteles" class="bcolor2" style="display:block; box-shadow: 3px 3px 5px;">
           <%--<img src="../App_Themes/Imagenes/imagesTurinco/estamosCargando.png" style="width:100%;"/>--%>
             <uc2:ucBuscadorHotel ID="ucBuscadorHotel1" runat="server" />
        </div>
        <!--- paquetes -->
         <div id="paquetes" class="bcolor3" style="display:block; box-shadow: 3px 3px 5px; height: 380px;">
            <uc4:ucBuscadorPlan ID="UcBuscadorPlan1" runat="server" />
        </div>
        <!--- cruceros -->
         <div id="cruceros" class="bcolor4" style="display:block; box-shadow: 3px 3px 5px;">
            <%--<uc3:ucBuscadorCrucero ID="ucBuscadorCrucero1" runat="server" />--%>
            <%--<img src="../App_Themes/Imagenes/imagesTurinco/estamosCargando.png" style="width:100%;"/>--%>
            <iframe frameborder="0" height="auto" width="100%" id="rc-frame" scrolling="no" src="http://cs.cruisebase.com/cs/?skin=629&lid=es" style="width: 360px; margin-left: -10px;">Cruceros</iframe>
             <div style="height:220px;">
                <uc6:ucRotadorBannerBuscador ID="ucRotadorBannerBuscador" runat="server" />
             </div>
            
        </div>
        <!--- autos -->
         <div id="autos" class="bcolor5" style="display:none; box-shadow: 3px 3px 5px;">
            <img src="../App_Themes/Imagenes/imagesTurinco/estamosCargando.png" style="width:100%;"/>
            <%--<uc5:ucBuscadorAuto ID="ucBuscadorAuto1" runat="server" />--%>
        </div>
        <!--- Excurciones -->
         <div id="excurciones" class="bcolor6" style="display:block; box-shadow: 3px 3px 5px;">
            <img src="../App_Themes/Imagenes/imagesTurinco/estamosCargando.png" style="width:100%;"/>
            <%--<uc2:ucBuscadorHotel ID="ucBuscadorHotel1" runat="server" />--%>
        </div>
        <!--- Excurciones -->
         <div id="asistencia" class="bcolor0" style="display:block; box-shadow: 3px 3px 5px;">
            <img src="../App_Themes/Imagenes/imagesTurinco/estamosCargandoNegro.png" style="width:100%;"/>
            <%--<uc2:ucBuscadorHotel ID="ucBuscadorHotel1" runat="server" />--%>
        </div>

        <div class="tarifasDescuentos">
            TARIFAS CON DESCUENTOS ESPECIALES
            <br />
            (Tercera Edad, discapacitados, estudiantes)
            <br />
            <a href="javascript:openChat();">
                Haga clic aquí
            </a>            
        </div>
    </div>
</div>
<script type="text/javascript">
    function Redirect() {
        window.open('../Presentacion/carros.aspx');
    }

    function RedirectHotel() {
        window.open('../Presentacion/Hoteles.aspx');
    }

    function openChat() {
        $(".zopim").toggle();
    }
 
</script>