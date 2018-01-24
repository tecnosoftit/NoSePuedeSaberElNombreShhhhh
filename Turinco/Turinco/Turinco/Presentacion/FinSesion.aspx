<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" %>


<%@ Register Src="../uc/ucFinSesion.ascx" TagName="ucFinSesion" TagPrefix="uc2" %>

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

    <form id="form1" runat="server">
        <div class="centered_contenedor">
	        <div id="contenedor">
	            <div id="box" style="margin-top:20px;">
                    <!--- box border -->
                    <div id="lb">
                    <div id="rb">
                    <div id="bb"><div id="blc"><div id="brc">
                    <div id="tb"><div id="tlc"><div id="trc">
                    
                        <div id="content">
                            <div class="panelCompleto">
                                <div class="resultados">
                                  
                                    <uc2:ucFinSesion ID="ucFinSesion1" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div></div></div></div>
                    </div></div></div></div>
                </div>
                
                <asp:HiddenField ID="hdfSesionId" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
