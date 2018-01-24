<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDetallePlan.ascx.cs"
    Inherits="uc_ucDetallePlan" %>
<%@ Register Src="ucTarifasCircuitosMulti.ascx" TagName="ucTarifasCircuitosMulti"
    TagPrefix="uc10" %>
<%--<%@ Register Src="ucTarifasRotativosEdades.ascx" TagName="ucTarifasRotativosEdades" TagPrefix="uc9" %>--%>
<%@ Register Src="ucTarifasRotativosMulti.ascx" TagName="ucTarifasRotativosMulti"
    TagPrefix="uc8" %>
<%--<%@ Register Src="ucEnvioAmigoDesc.ascx" TagName="ucEnvioAmigo" TagPrefix="uc7" %>--%>
<%@ Register Src="ucTarifasRotativos.ascx" TagName="ucTarifasRotativos" TagPrefix="uc5" %>
<%@ Register Src="ucTarifasCircuitos.ascx" TagName="ucTarifasCircuitos" TagPrefix="uc4" %>
<%--<%@ Register Src="ucEnvioAmigo.ascx" TagName="ucEnvioAmigo" TagPrefix="uc2" %>--%>

<div id="project-content">
    <div class="project-heading">
        <h1>
            <asp:Label ID="strNombrePlan" runat="server" Text=""></asp:Label>
        </h1>
        <div class="clearfix">
        </div>
    </div>
    <div class="setp GaleriaDetalle">
        <!-- slider -->
        <div class="project-slider">
            <div class="flexslider">
                <ul class="slides">
                    <asp:Repeater ID="rptGaleria" runat="server">
                        <ItemTemplate>
                            <li>
                                <%--<a href='<%# DataBinder.Eval(Container,"DataItem.strNombreImagen") %>'>--%>
                                <asp:Image runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.strNombreImagen") %>'
                                    ID="imgGal" CssClass="imagen fLeft" />
                                <%--</a>--%>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <!-- ENDS slider -->
    </div>
    <div class="unCu asidedet">
        <!-- sidebar -->
        <aside id="sidebar">
                    <div id="popup" class="popup">
                        <a onclick="closeDialog('popup');" class="close"></a>
                        <div>
                            <div class="ImagenGrande">
		        		        <asp:Image ID="strImagen" onclick="openDialog();" runat="server" CssClass="imagen fLeft" style="width: 700px;height:346px;"/>
	        		        </div>
                        </div>
                    </div>
	        		<div>   
                        <asp:Image ID="strImagen1" runat="server" CssClass="imagen fLeft" style="  width: 250px; height: 85px; margin-left: 99px;"/>
                    </div>
	        	</aside>
        <div class="clearfix">
        </div>
        <div class="valorFinal iblock">
            <div class="ColorDetallePlan">
                <span>
                    <asp:Label ID="Label2" runat="server" Text="Desde "></asp:Label>
                </span>
                <asp:Label ID="strRefereTipoMoneda" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strRefereTipoMoneda") %>'></asp:Label>
                <asp:Label ID="dblPrecio" class="ValorPlan" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.dblPrecio") %>'></asp:Label>
            </div>
            </br>
            <asp:Label CssClass="referencia" ID="strTarifaReferencia" runat="server" Text=""></asp:Label>
            </br>
            <asp:Label ID="strDetalleDuracion" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalleDuracion") %>'></asp:Label>
            </br>
            <asp:Image ID="imgCategoria" runat="server" />
        </div>
        <div class="imagenCategoria">
            <asp:Image ID="ImageButton2" runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.urlImagenCategoria") %>' />
        </div>
        <!-- ENDS sidebar -->
    </div>
    <div class="iblock contenedorDatosPaquetes" style="display: none;">
        <div class="precios full iblock grisOscuro">
            <asp:Label ID="strTarifaCuotas" runat="server" Visible="false" Text=""></asp:Label>
            <asp:Label ID="lblTDesde" runat="server" Text="VALOR FINAL" CssClass="labelValorFinal iblock"></asp:Label>
            <div class="difiera iblock">
                DIFIERE EL PAGO CON TU TARJETA
            </div>
            <div class="iblock cuotas">
                <div class="valorCuota iblock full">
                    <asp:Label ID="lblTarifaCuotas" runat="server" Text='<%#System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container,"DataItem.strTarifaCuotas").ToString()) %>'></asp:Label>
                </div>
            </div>
        </div>
        <div class="detalle azulOscuro iblock full">
            <asp:Label ID="lblurl" runat="server"></asp:Label>
        </div>
        <div class="galeria full iblock">
            <%--<asp:Repeater ID="rptGaleria" runat="server">
                <ItemTemplate>
                    <a href='<%# DataBinder.Eval(Container,"DataItem.strNombreImagen") %>'>
                        <asp:Image runat="server" ImageUrl='<%# DataBinder.Eval(Container,"DataItem.strNombreImagen") %>'
                            ID="imgGal" CssClass="imagen fLeft" />
                    </a>
                </ItemTemplate>
            </asp:Repeater>--%>
        </div>
    </div>
    <div id="page-content-fullPlan">
        <h1 class="h-margin">
            Descripción</h1>
        <script>
            $(function () {
                $("#tabsPaquetes").tabs();
            });
        </script>
        <div id="tabsPaquetes">
            <ul class="tabs">
                <li><a href="#tabs-1"><span>Descripción</span> </a></li>
                <li><a id="Oculto" href="#tabs-2"><span>Itinerario</span> </a></li>
                <li><a href="#tabs-3"><span>Tenga en cuenta</span> </a></li>
                <li><a href="#tabs-4"><span>Claúsulas</span> </a></li>
                <li><a href="#tabs-5" class="bcolor2"><span>Cotizar</span> </a></li>
            </ul>
            <div id="tabs-1">
                <ul>
                    <li>
                        
                        <asp:Label ID="strDescripcion" runat="server"></asp:Label>
                    </li>
                    <%--<li>
				    <h3>
					    Clasificación: 
				    </h3>
								
				    <p>
					    Cultural, Historia, Vacaciones 
					    <br>
					    La única capital de Latinoamérica frente al mar, Lima es un lugar grandioso para descubrir.
				    </p>
			    </li>--%>
                    <li>
                        <h3>
                            <asp:Label ID="lblTQueIncluye" runat="server" Text="Incluye:"></asp:Label>
                        </h3>
                        <asp:Label ID="strIncluye" runat="server"></asp:Label>
                    </li>
                    <li>
                        <h3>
                            <asp:Label ID="lblTQueNoIncluye" runat="server" Text="No incluye:"></asp:Label>
                        </h3>
                        <asp:Label ID="strNoIncluye" runat="server"></asp:Label>
                    </li>
                </ul>
            </div>
            <div id="tabs-2">
                <h2>
                    <asp:Label ID="Label1" runat="server" Text="Itinerario"></asp:Label>
                </h2>
                <asp:Image ID="imgITinerario" runat="server"/>
                <asp:Label ID="lblItinerario" runat="server"></asp:Label>
                <asp:Label ID="lblVerMasTexto" runat="server"></asp:Label>
            </div>
            <div id="tabs-3">
                <li>
                    <h3>
                        <asp:Label ID="lblTEnCuenta" runat="server" Text="Toma en cuenta:"></asp:Label>
                    </h3>
                    <br />
                    <asp:Label ID="strEncuenta" runat="server" Text="Label"></asp:Label>
                </li>
                <asp:Label ID="lblDescBreve" runat="server" Style="display: none;"></asp:Label>&nbsp;
            </div>
            <div id="tabs-4">
                <h2>
                    <asp:Label ID="lblTCondiciones" runat="server" Text="Condiciones y restricciones"></asp:Label>
                </h2>
                <asp:Label ID="strRestriccion" runat="server"></asp:Label>&nbsp;
                <p onclick="EsconderDetalle(getElementById('VerMas').id,this.children[0]);" class="block">
                    
                </p>
                <div class="CotenedorClausulas">
									<p style="text-align: justify;">
                                        <strong>Claúsula de Responsabilidad </strong>
										
										<ul> Over Turismo Internancional Colombia SAS (OVER Turinco)  con registro Nacional de turismo 10832 se hace responsable ante los usuarios por la total prestación y calidad de los servicios adquiridos, actuando como intermediario de los hoteles, compañías de transporte y demás servicios ofrecidos por terceros en cada uno de los programas, sin que haya procedencia de reclamaciones posteriores al viaje por la calidad de servicio de los mismos.</ul>

                                        <ul> Over Turinco, sus operadores y agentes declaran que actúan como intermediarios de los hoteles, compañías de transporte y otros, declinando por lo tanto toda responsabilidad por accidentes, huelgas, asonadas, terremoto, huracanes, sobreventas aéreas, terrestres y/o hoteleras y cualquier otro caso debidamente comprobado que pudiera ocurrir durante el viaje.</ul>

										<ul> En el evento de que algunas de las anteriores circunstancias ocurrieran durante el viaje, Over Turinco se reserva el derecho de hacer los cambios necesarios en itinerarios, fechas de viaje, hoteles, transportes y otros servicios, para garantizar el éxito del programa vendido, teniendo en cuenta siempre las cláusulas de responsabilidad y de cancelación de cada proveedor, sin que haya procedencia de reclamaciones posteriores al viaje.</ul>
										<ul> El Itinerario puede variar o modificarse por cambios de horarios en vuelos internacionales y/o nacionales, horario de trenes, buses, navegación en río y/o lago o por condiciones climatológicas o por razones que están fuera de nuestro control (fallas técnicas de aviones, etc.). En tal caso se buscará siempre ofrecer la mejor alternativa para los pasajeros garantizando el éxito de la excursión, los No Show causados por los mismos serán cubiertos por el pasajero.</ul>

										<ul>Es responsabilidad de cada viajero, ir provisto de su pasaporte o documento de identidad preciso, vigente y dotado de todos los visados y/o requisitos necesarios para el programa elegido, declinando las agencias prestatarias de servicios y operadores, toda responsabilidad, en el caso de ser rechazada por alguna autoridad la entrada a un país, por carecer de alguno de los requisitos que se precisen o defecto en el pasaporte, o simplemente por disposición unilateral de la autoridad migratoria quien decida no permitir el ingreso a su país de dicho visitante, aun teniendo la documentación en regla; los gastos se generen por No Show será asumidos en su totalidad por cuenta del pasajero, aplicándose en estas circunstancias las condiciones establecidas para la cancelación y rechazo voluntario de los servicios.</ul>

										<ul>El equipaje y cualquier objeto que el turista lleve consigo esta bajo su custodia y por lo tanto el operador y sus organizadores no se hacen responsables por los mismos.</ul>

<ul>En caso de cancelación de algún programa por parte de Over Turinco, se hará responsable ofreciendo opciones de viaje o reembolsando la totalidad del dinero según sea el caso.</ul>

<ul>En caso de cancelación de algún programa por parte del pasajero, se le aplicarán las políticas de cancelación que estén establecidas de acuerdo al plan y al proveedor de cada servicio. </ul> 

<ul>Para  otros planes, cruceros, destinos y eventos especiales, aplicarán las condiciones de cada caso, que serán informadas en la confirmación de los servicios.</ul>

<ul>OVER Turinco se acoge en su integridad a la ley 300 de 1996 y a sus posteriores reformas.</ul>

<ul><strong>OVER Turinco rechaza la explotación, la pornografía, el turismo sexual y demás formas de abuso sexual con menores; contribuye al cumplimiento de la Ley 679 de 2001, Ley 1329 y Ley 1336 de 2009..</strong></ul></p>
                </div>
                <div id="VerMas" style="display: none;">
                    <p>
                        <asp:Label ID="lblMas" runat="server" Text="No hay mas información"></asp:Label>
                    </p>
                </div>
            </div>
            <div id="tabs-5" style="min-height: 430px;">
                <asp:Label ID="lblDescCarro" runat="server" Text=""></asp:Label>
                <!-- tarifas circuitos normal -->
                <asp:Panel ID="pSalidas" runat="server">
                    <uc4:ucTarifasCircuitos ID="UcTarifasCircuitos1" runat="server" />
                </asp:Panel>
                <!-- tarifas rotativos normal -->
                <asp:Panel ID="pRotativos" runat="server">
                    <uc5:ucTarifasRotativos ID="UcTarifasRotativos1" runat="server" />
                </asp:Panel>
                <!-- tarifas rotativos Multihotel -->
                <asp:Panel ID="pRotativosMulti" runat="server">
                    <uc8:ucTarifasRotativosMulti ID="UcTarifasRotativosMulti1" runat="server" />
                </asp:Panel>
                <!-- tarifas circuitos Multihotel -->
                <asp:Panel ID="pSalidasMulti" runat="server">
                    <uc10:ucTarifasCircuitosMulti ID="UcTarifasCircuitosMulti1" runat="server" />
                </asp:Panel>
                <!-- tarifas rotativos Edades -->
                <%--<asp:Panel ID="pRotativosEdades" runat="server">
                <uc9:ucTarifasRotativosEdades ID="UcTarifasRotativosEdades1" runat="server" />
            </asp:Panel>--%>
                <div style="text-align: center">
                    <asp:Label ID="lblError" runat="server" ForeColor="Maroon"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</div>
<!-- related -->
<div class="related-projects">
    <div class="related-heading">
        <h4>
            Planes relacionados</h4>
    </div>
    <div class="related-list">
        <figure>
								<a href="../Presentacion/planes.aspx?tipo=OFR" class="thumb"><img src="../App_Themes/Imagenes/imagesTurinco/dummies/featured-1.jpg" alt="Alt text" /></a>
								<h4>Plan</h4>
		        				<h5 class="color1">Desde USD 500</h5>
			        			
							</figure>
        <figure>
								<a href="../Presentacion/planes.aspx?tipo=OFR" class="thumb"><img src="../App_Themes/Imagenes/imagesTurinco/dummies/featured-1.jpg" alt="Alt text" /></a>
								<h4>Plan</h4>
		        				<h5 class="color1">Desde USD 500</h5>
			        			
							</figure>
        <figure>
								<a href="../Presentacion/planes.aspx?tipo=OFR" class="thumb"><img src="../App_Themes/Imagenes/imagesTurinco/dummies/featured-1.jpg" alt="Alt text" /></a>
								<h4>Plan</h4>
		        				<h5 class="color1">Desde USD 500</h5>
			        			
							</figure>
    </div>
    <div class="clearfix">
    </div>
</div>
<!-- ENDS related -->
<!-- Enviar a un amigo Descripcion -->
<ajax:ModalPopupExtender ID="MPEEEnvioAmigoDesc" BackgroundCssClass="ui-widget-shadow"
    DropShadow="false" runat="server" TargetControlID="dummyLinkEnvio" Drag="false"
    BehaviorID="MPEEEnvioAmigoDesc" OnOkScript="" OkControlID="btnCerrarEnv" EnableViewState="true"
    PopupControlID="PanelEnvioDesc" />
<ajax:UpdatePanel runat="server" ID="PanelEnvioDesc" Style="display: none;">
    <ContentTemplate>
        <div class="contenedorPopUpCondiciones">
            <asp:Button ID="btnCerrarEnv" Text="X" runat="server" CausesValidation="false" CssClass="btnCerrar azulClaro"
                OnClientClick='javascript:ClosePopUp();' />
            <div class="condiciones">
                <div class="boldTerminos">
                    Enviar tour por email
                </div>
                <div class="div100 renglonEnvio">
                    <asp:Label ID="lblTNombreEnvia" runat="server" Text="Nombre de quien envia:"></asp:Label>
                    <asp:TextBox ID="txtTuNombre" runat="server" CssClass="formaBuscar"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FtbNombre" runat="server" TargetControlID="txtTuNombre"
                        FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters" ValidChars=" ">
                    </ajax:FilteredTextBoxExtender>
                </div>
                <div class="div100 renglonEnvio">
                    <asp:Label ID="lblTNombreEnvio" runat="server" Text="Nombre a quien envia:"></asp:Label>
                    <asp:TextBox ID="txtSuNombre" runat="server" CssClass="formaBuscar"></asp:TextBox>
                    <ajax:FilteredTextBoxExtender ID="FtbSuNombre" runat="server" TargetControlID="txtSuNombre"
                        FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters" ValidChars=" ">
                    </ajax:FilteredTextBoxExtender>
                </div>
                <div class="div100 renglonEnvio">
                    <asp:Label ID="lblTEmailEnvio" runat="server" Text="Email a quien envia:"></asp:Label>
                    <asp:TextBox ID="txtSuEmail" runat="server" CssClass="formaBuscar"></asp:TextBox>
                    <div class="error">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSuEmail"
                            ErrorMessage="Por favor Digite un correo valido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="div100 renglonEnvio">
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                    <asp:Button CssClass="btnAzul" ID="lbEnviar" runat="server" CausesValidation="false"
                        OnClick="lbEnviar_Click" Text="Enviar"></asp:Button>
                </div>
            </div>
        </div>
    </ContentTemplate>
</ajax:UpdatePanel>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLinkEnvio"
    runat="server">dummy</a> <a href="#" style="display: none; visibility: hidden;" onclick="return false"
        id="dummyLink1" runat="server">dummy</a> <a href="#" style="display: none; visibility: hidden;"
            onclick="return false" id="dummyLink2" runat="server">dummy</a> <a href="#" style="display: none;
                visibility: hidden;" onclick="return false" id="dummyLink3" runat="server">dummy</a>
<a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink4"
    runat="server">dummy</a> <a href="#" style="display: none; visibility: hidden;" onclick="return false"
        id="dummyLink5" runat="server">dummy</a> 

<script>
    //Codigo que se encarga de Ocultar el itinerario
    if (document.getElementById("ucDetallePlan_lblItinerario").innerHTML == "") {
        document.getElementById("Oculto").style.display = "none";
    }

    var url = $("#ucDetallePlan_strImagen").attr("src")
    if ( url == "") {
        document.getElementById("popup").style.display = "none";
     }

</script>

<script type="text/javascript">
    function openDialog() {
        $('#overlay').fadeIn('fast', function () {
            $('#popup').css('display', 'block');
            $('#popup').animate({ 'left': '50%' }, 500);
        });
    }

    function closeDialog(id) {
        $('#' + id).css('position', 'absolute');
        $('#' + id).animate({ 'left': '150%' }, 500, function () {
            $('#' + id).css('position', 'fixed');
            $('#' + id).css('left', '100%');
            $('#' + id).css('display', 'none');
            $('#overlay').fadeOut('fast');
        });
    }
</script>