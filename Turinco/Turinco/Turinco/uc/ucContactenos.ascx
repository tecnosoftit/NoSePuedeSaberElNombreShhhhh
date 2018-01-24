<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucContactenos.ascx.cs"    Inherits="uc_ucContactenos" %>
<%@ Register Src="ucFormularioContacto.ascx" TagName="ucFormularioContacto" TagPrefix="uc1" %>

<div>
    <div id="banner1" runat="server" class="full iblock img"></div>

    <!-- masthead -->
	    <div id="masthead">
		    <span class="head">Contáctenos</span>
	    </div>
	<!-- ENDS masthead -->

    <!-- page content -->
	    <div class="ContentFormulario" id="page-content">
            <asp:Repeater runat="server" ID="rptSeccion">
                <ItemTemplate>
                        <div class="noneDisplay">
                            <img width="75" id="imgImagen" runat="server" alt='<%# DataBinder.Eval(Container.DataItem, "strTitulo") %>' src='<%# Ssoft.Utils.clsValidaciones.GetKeyOrAdd("RUTA_IMAGENES_PLANESW", "") + DataBinder.Eval(Container,"DataItem.strImagen") %>' />
                        </div>
                        <div class="textoContacto">
                            <asp:Label runat="server" ID="lblDescripcion"><%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container, "DataItem.strDescripcion").ToString())%></asp:Label>
                        </div>
                        <div class="noneDisplay">
                            <asp:Label runat="server" ID="Label2"><%# DataBinder.Eval(Container,"DataItem.strTitulo") %></asp:Label>
                        </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="formulario">
                <uc1:ucFormularioContacto ID="ucFormularioContacto" runat="server" />
            </div>
            
        </div>

    <div style="display:none;">
        <asp:Label ID="lblTextoContactenos" runat="server" CssClass="textoEncabezadoContacto"></asp:Label>
    </div>
    <!-- sidebar -->
	        	<aside id="sidebar" class="ContentMapa">
	        		<div class="block">
                        
                        <div class="contenedorInformacion1">
		        		    <h4>Oficina Mayorista </h4>
		        		
		        		    <ul class="address-block">
		        			    <li class="address">Carrera 11 No. 86-32 Of. 401 </li>
		        			    <li class="phone">PBX +571 2568888</li></br>
		        			    <li class="mobile">312 432 7815</li>
		        			    <li class="email">
		        			    <a href="mailto:servicioalcliente@turinco.co">servicioalcliente@turinco.co</a></li>
		        		    </ul>
                        </div>
                        </br>
                        <div class="contenedorInformacion1">
		        		    <h4>Agencia de Viajes</h4>
		        		
		        		    <ul class="address-block">		        			
		        			    <li class="address"> Calle 37 No. 8-23  </li>
		        			    <li class="phone">PBX +571 2320407</li></br>
		        			    <li class="mobile">313 831 0815</li>
		        			    <li class="email">
		        			    <a href="mailto:servicioalcliente37@turinco.co">servicioalcliente37@turinco.co</a>
		        			    </li>
		        		    </ul>
                        </div>
	        		</div>
                    <div  id="map-holder">
                        <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3976.591360889194!2d-74.04936592252432!3d4.666713448236787!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8e3f9a8b0199491b%3A0xbc0deb97ccc9c350!2sCarrera+11+%23+86-32%2C+Bogot%C3%A1%2C+Cundinamarca%2C+Colombia!5e0!3m2!1ses!2s!4v1424903115638" width="500" height="400" frameborder="0" style="border:0"></iframe>
                    </div>
	        	</aside>
	        	<div class="clearfix"></div>
				<!-- ENDS sidebar -->
</div>
