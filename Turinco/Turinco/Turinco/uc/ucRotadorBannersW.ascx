<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRotadorBannersW.ascx.cs" Inherits="uc_ucRotadorBannersW" %>

<!-- slider -->
	<div id="banner-slide" class="TresCuInscribase">
        <div class="flexslider home-slider">
		  <ul class="slides">
            <asp:Repeater runat="server" ID="dtlOfertas">
                <ItemTemplate>
                    <li>
                        <%# DataBinder.Eval(Container,"DataItem.strHTML") %>
                    </li> 
                </ItemTemplate>
            </asp:Repeater>	  
		  </ul>
	    </div>
	</div>
<!-- ENDS slider -->