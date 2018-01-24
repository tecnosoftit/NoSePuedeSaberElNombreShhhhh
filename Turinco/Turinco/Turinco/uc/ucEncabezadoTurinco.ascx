<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucEncabezadoTurinco.ascx.cs" Inherits="uc_ucEncabeza" %>
<meta name="description" content="Encuentra tiquetes aéreos nacionales e internacionales, planes turísticos, hoteles y alquila tu carro para tus viajes con las mejores tarifas. Todo en un solo portal"/>
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<meta http-equiv="expires" content="Tue, 01 Jan 1980 1:00:00 GMT" /> 
<meta name="keywords" content="Over Turismo Internacional, Over Turismo Internacional Colombia, expertos en viajes, Over Turinco, Turinco, tiquetes aéreos nacionales, tiquetes aéreos internacionales, paquetes turísticos nacionales, paquetes turísticos internacionales, viajes internacionales, viajes nacionales, ofertas especiales, planes turísticos, paquetes, promociones, temporada, tarifas, vacaciones, viajes, agencia, cruceros, isla margarita, hoteles, palladium, Riu, Hard Rock, Majestic, riviera maya, miami, cuba, bahía solano, zona cafetera, argentina, perú, chile, china, europa, somos mayoristas de turismo, somos agencia de viajes mayorista experta en viajes vacacionales, viajes corporativos, manejo de excursiones de quinceañeras, grupos, convenciones, viajes de incentivos para empresas, somos expertos en cruceros, pullmantur, royal Caribbean, celebrity cruises, Disney cruises, azamara cruises, carnival, norwegian, Princess cruises, costa cruceros, msc cruceros, viajes a Cancún y Riviera maya, Punta Cana, Panamá, Miami, Orlando, tierra santa, dubai, Turquía, venta de alquiler de autos, asistencias médicas, vuelos Avianca, lan, copa, jet blue, iberia, Lufthansa, aeromexico, aerolíneas argentinas, agencia de viajes en Bogotá, mayorista de turismo en bogota, agencia de viajes en cucuta, agencia de viajes en Bucaramanga, agencia de viajes en barranquilla"/>

<link href="../App_Themes/Estilos/jQuery/jquery-ui-1.8.6.custom.css" rel="stylesheet" type="text/css" />
<%--<link href="../App_Themes/Estilos/Ssoft/estilosNactur.css" rel="stylesheet" type="text/css" />--%>
<link href="../App_Themes/Estilos/Ssoft/estilosTurinco.css" rel="stylesheet" type="text/css" />
<!-- Lessgrid -->
<link href="../App_Themes/Estilos/Ssoft/lessgrid.css" rel="stylesheet" media="all"/>
<link href="../App_Themes/Estilos/Ssoft/elements.css" rel="stylesheet" media="all"/>
<link href="../App_Themes/Estilos/Ssoft/style.css" rel="stylesheet" media="all"/>

<link rel="shortcut icon" href="../App_Themes/Imagenes/favicon.ico" type="image/x-icon" />

<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery-ui-1.8.1.custom.min.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/sessvars.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/easySlider1.5.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery.fadetransition.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery.featureList-1.0.0.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery.wslide.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery.skinned-select.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/peloslideli.v1.0.1.min.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery.autocomplete.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/ui.core.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery.cookies.2.1.0.js"></script>

<script type="text/javascript" src="../App_Themes/Scripts/jQuery/swfobject.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery.dataTables.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/JQuery/jquery.jcarousel.min.js"></script>

<script type="text/javascript" src="../App_Themes/Scripts/jQuery/mostrar.js"></script>

<script type="text/javascript" src="../App_Themes/Scripts/Ssoft/Funciones.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/Ssoft/Estilo.js"></script>
<script type='text/javascript' src='../App_Themes/Scripts/Ssoft/ValidadorTarjeta.js'></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery.cookies.2.1.0.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/slider_test.js"></script>
<script src="../App_Themes/Estilos/JQuery/jsTurinco/jquery.idletimeout.js"></script>
<script src="../App_Themes/Estilos/JQuery/jsTurinco/jquery.idletimer.js"></script>

<script>
    (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
  m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

    ga('create', 'UA-61607410-1', 'auto');
    ga('send', 'pageview');

</script>


<!-- GOOGLE FONTS -->
		<link href='http://fonts.googleapis.com/css?family=Allan:700' rel='stylesheet' type='text/css'>

<!-- Flex Slider -->
		<link rel="stylesheet" href="../App_Themes/Estilos/Slider/flexslider.css" >
		<script src="../App_Themes/Estilos/JQuery/jsTurinco/jquery.flexslider-min.js"></script>

        <script>
            $(window).load(function () {
                $('.flexslider').flexslider();
            });
        </script>

<!-- ENDS Flex Slider -->

<link rel="stylesheet" href="../App_Themes/Estilos/Slider/bjqs.css" />

<!-- load jQuery and the plugin -->
<script src="../App_Themes/Scripts/Slider/bjqs-1.3.min.js"></script> 


<!-- mobile-nav -->
		<div id="mobile-nav-holder">
			<div class="wrapper">
				<ul id="mobile-nav">
					<li  class="current-menu-item"><a href="../Presentacion/index.aspx">Inicio</a></li>
					<li><a href="../Presentacion/planes.aspx?tipo=PLN">Planes</a></li>
					<li><a href="../Presentacion/Cruceros.aspx?tipo=PLN">Cruceros</a></li>
					<li><a href="../Presentacion/Ofertas.aspx?tipo=OFR">ofertas</a></li>
					<li><a href="../Presentacion/QuienesSomos.aspx">Quiénes somos</a></li>
					<li><a href="../Presentacion/Utilitarios.aspx">Folletos</a></li>
					<li><a href="../Presentacion/MiCuenta.aspx">Agentes</a></li>
					<li><a href="../Presentacion/Contactenos.aspx">Contacto</a></li>
					<li><a href="../Presentacion/Inscribase.aspx">Inscribase</a></li>
				</ul>
				<div id="nav-open"><a href="#">Menu</a></div>
			</div>
		</div>
		<!-- ENDS mobile-nav -->

<!--NECESARIO PARA LA CORTINILLA-->
<div style="display:none;" id="div_Cortinilla">
    <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Imagenes/cortinilla.gif" />
</div>

<div style="display:none;" id="div_CortinillaFlash">
    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="413" height="253">
      <param name="movie" value="../App_Themes/Imagenes/cortinilla.swf" />
      <param name="quality" value="high" />
      <%--<param name="wmode" value="transparent" />--%>
      <embed src="../App_Themes/Imagenes/cortinilla.swf" wmode="transparent" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="413" height="253"></embed>
    </object>
</div>


<div style="display:none;" id="div_CortinillaBFM">
    <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Imagenes/cortinillaBFM.gif" />
</div>

<div style="display:none;" id="div_CortinillaFlasBFM">
    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="413" height="253">
      <param name="movie" value="../App_Themes/Imagenes/cortinillaBFM.swf" />
      <param name="quality" value="high" />
      <%--<param name="wmode" value="transparent" />--%>
      <embed src="../App_Themes/Imagenes/cortinillaBFM.swf" wmode="transparent" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="413" height="253"></embed>
    </object>
</div>



<!-- CORTINILLAS INTERNAS -->
<div style="display:none;" id="div_Cortinilla_Interna">
    <asp:Image ID="Image33" runat="server" ImageUrl="~/App_Themes/Imagenes/cortinilla2.gif" />
</div>

<div style="display:none;" id="div_CortinillaFlashInterna">
    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0" width="413" height="253">
      <param name="movie" value="../App_Themes/Imagenes/cortinilla2.swf" />
      <param name="quality" value="high" />
     <%-- <param name="wmode" value="transparent" />--%>
      <embed src="../App_Themes/Imagenes/cortinilla.swf" wmode="transparent" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="413" height="253"></embed>
    </object>
</div>

<div id="galeria">
</div>
<div class="ventana2"  style="width:400px;height:200px;display:none;" id="div_CortinillaMenu"  >
    <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Imagenes/cortinilla2.gif" />
</div>

<%--<script src="../App_Themes/Estilos/JQuery/jsTurinco/custom.js"></script> --%>

        <script>
            var mobnavContainer = $("#mobile-nav");
            var mobnavTrigger = $("#nav-open");

            mobnavTrigger.click(function () {
                mobnavContainer.slideToggle();
            });
        </script>

<!--Start of Zopim Live Chat Script-->
<script type="text/javascript">
    window.$zopim || (function (d, s) {
        var z = $zopim = function (c) { z._.push(c) }, $ = z.s =
d.createElement(s), e = d.getElementsByTagName(s)[0]; z.set = function (o) {
    z.set.
_.push(o)
}; z._ = []; z.set._ = []; $.async = !0; $.setAttribute("charset", "utf-8");
        $.src = "//v2.zopim.com/?34EpkkaFbWLxbfLgTBbZjOBN5N3C4u1n"; z.t = +new Date; $.
type = "text/javascript"; e.parentNode.insertBefore($, e)
    })(document, "script");
</script>
<!--End of Zopim Live Chat Script-->

 
