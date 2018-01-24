<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" %>
<%@ Register Src="../uc/ucDefault.ascx" TagName="ucDefault" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
    <link href="../App_Themes/Estilos/Ssoft/estilos.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:#F5F5F5" onbeforeunload="ConfirmarCierre()" onunload="ManejadorCierre()">

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

    <form id="form1" method="post" runat="server">
        <div class="centered_div">
            <div class="imgLogin">
                <img alt="" src="../App_Themes/Imagenes/imgLogin.png" />
            </div>
            <div id="tituloLogin">
                <asp:Label ID="Label2" CssClass="t1" runat="server" Text="Ingresar al "></asp:Label>
                <asp:Label ID="Label1" CssClass="t2" runat="server" Text="Corporativo"></asp:Label>
            </div>
            <div id="login">
                <uc2:ucDefault ID="ucLogin1" runat="server" />     
            </div>
            <div class="pieLogin">
                <asp:Label ID="Label3" CssClass="t1" runat="server" Text="Derechos Reservados :: 2010 Script y dise�o por Ssoft Colombia"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
