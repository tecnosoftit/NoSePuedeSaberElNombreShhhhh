<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucRotadorBanners.ascx.cs" Inherits="uc_ucRotadorBanners" %>

<!-- slider -->
	<div id="banner-slide" class="TresCu flexRigth">
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