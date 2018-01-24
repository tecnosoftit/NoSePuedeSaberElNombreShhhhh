<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRotadorBannerBuscador.ascx.cs" Inherits="uc_ucRotadorBannerBuscador" %>

<div class="bannersDerecha"> 
    <div class="bannerDer">
       <div id="bannerInterno2" runat="server"></div>   
    </div>
</div>
<script type="text/javascript">
    if (document.getElementById("ucBuscador_ucRotadorBannerBuscador_bannerInterno2").innerHTML == 0) {
        $('#ucBuscador_ucRotadorBannerBuscador_bannerInterno2').append('<img id=\'imgTemp\' src=\' \' class=\'tempImg\' />')
    }
</script>
