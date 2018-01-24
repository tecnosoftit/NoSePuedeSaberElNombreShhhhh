<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucLoginCorporativo.ascx.cs" Inherits="uc_ucLogin" %>
<%@ Register Src="ucRegistroT.ascx" TagName="ucRegistroT" TagPrefix="uc2" %>
<%@ Register Src="../uc/ucRotadorBannersW.ascx" TagName="ucRotadorBannersW" TagPrefix="uc12" %>

<div>
        <div class="contenedorRotadorInscribase">
          <div class="rotadorInscribase">
              <uc12:ucRotadorBannersW ID="ucRotadorBannersW1" runat="server" />
          </div>
        </div>
        <uc2:ucRegistroT ID="UcRegistroT1" runat="server" />
</div>

