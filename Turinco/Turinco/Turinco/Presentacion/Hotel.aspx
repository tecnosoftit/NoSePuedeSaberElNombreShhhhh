<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Presentacion_indice" %>

<%@ Register Src="../uc/ucHotel.ascx" TagName="ucHotel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">

    <title>Tu Tiquete</title><meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        html, body, form{background-image:none; background-color:#FFF;padding:0; margin:0; border:0; font-family:Trebuchet MS; font-size:12px; color:#000;}
        ul{ list-style:disc; padding-left:10px;}
        ol{ list-style:decimal; padding-left:10px;}
        strong { font-weight:bold; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:ucHotel ID="ucHotel" runat="server" />        
    </form>
</body>
</html>