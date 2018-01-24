<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucEncabezado.ascx.cs" Inherits="uc_ucEncabeza" %>
<meta name="description" content="Encuentra tiquetes aéreos nacionales e internacionales, planes turísticos, hoteles y alquila tu carro para tus viajes con las mejores tarifas. Todo en un solo portal"/>
<link rel="shortcut icon" href="../App_Themes/Imagenes/favicon.ico" type="image/x-icon" /> 
<link href="../App_Themes/Estilos/jQuery/jquery-ui-1.8.6.custom.css" rel="stylesheet" type="text/css" />
<link href="../App_Themes/Estilos/Ssoft/estilos.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js"></script>
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
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/jquery.cookies.2.1.0.js"></script>
<script type="text/javascript" src="../App_Themes/Scripts/jQuery/slider_test.js"></script>


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