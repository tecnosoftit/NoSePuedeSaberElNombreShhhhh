<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" %>

<%--<%@ Register Src="../uc/ucScriptManager.ascx" TagName="ucScriptManager" TagPrefix="uc11" %>
<%@ Register Src="../uc/ucPiePagina.ascx" TagName="ucPiePagina" TagPrefix="uc8" %>
<%@ Register Src="../uc/ucBannerSuperior.ascx" TagName="ucBannerSuperior" TagPrefix="uc7" %>
<%@ Register Src="../uc/ucEncabezado.ascx" TagName="ucEncabezado" TagPrefix="uc6" %>
<%@ Register Src="../uc/ucMenuInferior.ascx" TagName="ucMenuInferior" TagPrefix="uc4" %>--%>
<%@ Register Src="../uc/ucDetalleTour.ascx" TagName="ucDetallePlan" TagPrefix="uc2" %>
<<%--%@ Register Src="~/uc/ucMenuSuperior.ascx" TagName="ucMenuSuperior" TagPrefix="uc1" %>--%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript">

        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
  m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-2588232-1', 'tutiquete.com');
        ga('send', 'pageview');


</script>
    <title>Tu Tiquete</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

  <%--  <uc6:ucEncabezado ID="UcEncabezado1" runat="server" />--%>
</head>

<body onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()">

<!-- GTM -->
    <!-- Google Tag Manager -->
    <noscript><iframe src="//www.googletagmanager.com/ns.html?id=GTM-KX8QZK"
    height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
    <script>        (function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({ 'gtm.start':
    new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
    j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
    '//www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-KX8QZK');</script>
    <!-- End Google Tag Manager -->

<script type="text/javascript" src="../App_Themes/Scripts/Ssoft/top.js"></script>
<form id="form1" runat="server">
    <div id="wrap">
	    <header>
           <%-- <uc11:ucScriptManager ID="UcScriptManager1" runat="server" />
	        <uc7:ucBannerSuperior ID="UcBannerSuperior1" runat="server" />--%>
	    </header>

        <nav>
           <%-- <uc1:ucMenuSuperior ID="ucMenuSuperior1" runat="server" />--%>
        </nav>

	    <div id="main">
	        <div class="panelDetallePlan">
                <uc2:ucDetallePlan ID="ucDetallePlan" runat="server" />
            </div>
            
         <%--   <uc4:ucMenuInferior ID="ucMenuInferior" runat="server" />--%>
	    </div>
    </div>

    <footer>
       <%-- <asp:HiddenField ID="hdfSesionId" runat="server" />
        <uc8:ucPiePagina ID="UcPiePagina2" runat="server" />--%>
    </footer>
</form>
  <script type="text/javascript" src="../App_Themes/Scripts/Ssoft/bottom.js"></script>
</body>
</html>