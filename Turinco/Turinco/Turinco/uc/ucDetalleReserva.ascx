<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucDetalleReserva.ascx.cs" Inherits="uc_ucDetalleReserva" %>


<div class="panelCompletoDetalleMiCuenta">
    <div id="masthead">
        <span class="head">
            <asp:Label ID="lblTituloSeccionPadre" Text="Detalles de la reserva" runat="server"></asp:Label>
        </span>
    </div>
    <div class="panelResultados">
        <div class="contenidoResultado">
            <div class="full" style="margin-bottom:30px;">
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Presentacion/MiCuenta.aspx" CssClass="botonBuscar botonRegresarMiCuenta" Text="<< Regresar"></asp:LinkButton>
            </div>

            <div class="full">
                <span class="vino titul">
                    <asp:Label ID="Label3" runat="server" Text="Localizador:"></asp:Label>
                </span>

                <asp:Label ID="lblLocalizador" runat="server"></asp:Label>
            </div>            

            <!-- reserva planes -->
            <asp:Repeater ID="rptReservaPlanes" runat="server">                
                <ItemTemplate>

                <div class="full">
                    <span class="vino titul">
                        Record de la reserva de
                        <asp:Label ID="lblTipoPlan" style="float:none;" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TipoPlan") %>'></asp:Label>
                        :
                        <asp:Label ID="lblRefereTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strcodigo") %>' Visible="false"></asp:Label>
                    </span>

                    <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strReserva") %>'></asp:Label>                    
                </div>

                <%-- 
                <div class="full">
                    <span class="vino titul">
                        <asp:Label ID="lblcodigoascesortext" runat="server" Text='Codigo Ascesor' />                    
                    </span>

                    <asp:Label ID="lblCodigoascesor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCodigoAscesor") %>' />
                    
                </div>
                --%>

                <div class="full" style="display:none;">
                    <span class="vino titul">
                        <asp:Label ID="lblintTipoReserva" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "intTipo") %>' Visible="false"></asp:Label>                                    
                    </span>

                    <asp:Label ID="lblEstadoReserva" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "intEstado") %>' Visible="false"></asp:Label>                
                </div> 
            
                <div class="full" style="display:none;">
                    <span class="vino titul">
                        <asp:Label ID="lbl1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label1") %>'></asp:Label>
                    </span>
                
                    <asp:Label ID="dat1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto1") %>'></asp:Label>                
                </div> 

                <div class="full" style="display:none;">
                    <span class="vino titul">
                        <asp:Label ID="lbl2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label2") %>'></asp:Label>
                    </span>

                    <asp:Label ID="dat2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto2") %>'></asp:Label>                
                </div> 

                <asp:Repeater ID="rptPlan" runat="server">
                    <ItemTemplate>
                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label1") %>'></asp:Label>
                            </span>

                            <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto1") %>'></asp:Label>                        
                        </div>                    
                    
                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label2") %>'></asp:Label>
                            </span>

                            <asp:Label ID="Label6" runat="server" Text='<%# System.Web.HttpUtility.HtmlDecode(DataBinder.Eval(Container.DataItem, "texto2").ToString()) %>'></asp:Label>                        
                        </div>
                    
                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label3") %>'></asp:Label>
                            </span>

                            <asp:Label ID="dat3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto3") %>'></asp:Label>                        
                        </div> 

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label4") %>'></asp:Label>
                            </span>

                            <asp:Label ID="dat4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto4") %>'></asp:Label>                        
                        </div> 

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label5") %>'></asp:Label>
                            </span>

                            <asp:Label ID="dat5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto5") %>'></asp:Label>                        
                        </div> 

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label6") %>'></asp:Label></strong>
                            </span>
                        
                            <asp:Label ID="dat6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto6") %>'></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label7") %>'></asp:Label>
                            </span>
                        
                            <asp:Label ID="dat7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto7") %>'></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label8") %>'></asp:Label>
                            </span>

                            <asp:Label ID="dat8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto8") %>'></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label9") %>'></asp:Label>
                            </span>
                        
                            <asp:Label ID="dat9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto9") %>'></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label10") %>'></asp:Label>
                            </span>
                        
                            <asp:Label ID="dat10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto10") %>'></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label11") %>'></asp:Label>
                            </span>

                            <asp:Label ID="txtObservaciones" runat="server" Width="600px" Rows="8" Text='<%# DataBinder.Eval(Container.DataItem, "texto11") %>'></asp:Label>
                            <asp:Label ID="dat11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto11") %>' Visible="false"></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label12") %>'></asp:Label>
                            </span>
                        
                            <asp:Label ID="dat12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto12") %>'></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label13") %>'></asp:Label>
                            </span>

                            <asp:Label ID="dat13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto13") %>'></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label14") %>'></asp:Label>
                            </span>

                            <asp:Label ID="dat14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto14") %>'></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label15") %>'></asp:Label>
                            </span>

                            <asp:Label ID="dat15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto15") %>'></asp:Label>                        
                        </div>

                        <div class="full">
                            <span class="vino titul">
                                <asp:Label ID="lbl16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label16") %>'></asp:Label>
                            </span>

                            <asp:Label ID="dat16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto16") %>'></asp:Label>                        
                        </div>
                                                                        
                        <asp:Label Visible="false" ID="lblSegmento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Segmento") %>'></asp:Label>
                        
                        <asp:Repeater ID="rptPasajeros" runat="server">
                            <HeaderTemplate>
                                <div class="table_ t_center" style="width:15%;">
                                    <h5 class="vino">
                                        <asp:Label ID="Label14" runat="server" Text="Reservar a nombre de"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label31" runat="server" Text="Tipo de pasajero"></asp:Label>
                                    </h5>
                                </div>
                                
                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label57" runat="server" Text="Genero"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center" style="width:15%;">
                                    <h5 class="vino">
                                        <asp:Label ID="Label72" runat="server" Text="Documento ident."></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label58" runat="server" Text="Fecha Nacim."></asp:Label>
                                    </h5>
                                </div>

                                <%--                                
                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label59" runat="server" Text="Nacionalidad"></asp:Label>
                                    </h5>
                                </div>
                                
                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label60" runat="server" Text="Num. Pasaporte"></asp:Label>
                                    </h5>
                                </div>
                                
                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label61" runat="server" Text="Fecha Exp. Pasaporte"></asp:Label>
                                    </h5>
                                </div>
                                
                                <div class="table_ t_center">
                                    <h5 class="vino
                                        <asp:Label ID="Label62" runat="server" Text="Pais Residencia"></asp:Label>
                                    </h5>
                                </div> 
                                --%>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label15" runat="server" Text="Moneda"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label49" runat="server" Text="Valor Total"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label44" runat="server" Text="Descuento"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label16" runat="server" Text="Valor a pagar"></asp:Label>
                                    </h5>
                                </div>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <div style="float:left; width:100%;">
                                    <div class="table_ t_center" style="width:15%;">
                                        <span class="even block">
                                            <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strNombre") %>'></asp:Label>
                                            <asp:Label ID="lblCodTarifa" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.intCodigFare") %>'></asp:Label>
                                            <asp:Label ID="lblDetalleTipoPax" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalle") %>'></asp:Label>
                                        </span>
                                    </div>   
                                    
                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="lblDescTipoPax" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalle") %>'></asp:Label>
                                            <asp:Label ID="lblTipoPax" Visible="false" runat="server" Width="100" Text='<%# DataBinder.Eval(Container,"DataItem.intTipoPax") %>'></asp:Label>
                                        </span>
                                    </div>   
                                    
                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="Label63" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Genero") %>'></asp:Label>
                                        </span>
                                    </div>   
                                    
                                    <div class="table_ t_center" style="width:15%;">
                                        <span class="even block">
                                            <asp:Label ID="Label75" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Documento") %>'></asp:Label>
                                        </span>
                                    </div>   
                                    
                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="Label64" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaNac") %>'></asp:Label>
                                        </span>
                                    </div>
                                    
                                    <%-- 
                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="Label65" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Nacionalidad") %>'></asp:Label>
                                        </span>
                                    </div>   

                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="Label66" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NumPasaporte") %>'></asp:Label>
                                        </span>
                                    </div>   

                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="Label67" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaExp") %>'></asp:Label>
                                        </span>
                                    </div>   

                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="Label68" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PaisResidencia") %>'></asp:Label>
                                        </span>
                                    </div>   
                                    --%>
                                    
                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="Label15" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>
                                        </span>
                                    </div>   
                                    
                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="txtTotal" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Neto") %>'></asp:Label>
                                            <asp:Label ID="lblTotalOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Neto") %>'></asp:Label>
                                        </span>
                                    </div>   
                                    
                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="txtDescuento" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Descuento") %>'></asp:Label>
                                            <asp:Label ID="lblDescuentoOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Descuento") %>'></asp:Label>
                                        </span>
                                    </div>   
                                    
                                    <div class="table_ t_center">
                                        <span class="even block">
                                            <asp:Label ID="txtValorPasajero" runat="server" Width="10%" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container,"DataItem.Total")).ToString("###,##0.00") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblValorOriginal" Visible="false" runat="server" Width="100" Text='<%# DataBinder.Eval(Container,"DataItem.Total") %>'></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container,"DataItem.Total")).ToString("###,##0.00") %>'></asp:Label>
                                        </span>
                                    </div>                    
                                </div>
                            </ItemTemplate>                                                    
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
                
                <asp:Repeater ID="rptValor" runat="server">
                    <ItemTemplate>
                        <div class="full" style="width:50%; margin-top:20px;">
                            <span class="vino titul" style="width:65%;">
                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label100") %>'></asp:Label>
                            </span>
                    
                            <span style="width:30%;">
                                <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container,"DataItem.texto100")).ToString("###,###,###") %>'></asp:Label>
                            </span>
                        </div>                        
                    </ItemTemplate>
                </asp:Repeater>

                <div class="full" style="width:50%; margin-top:20px;">
                    <span class="vino titul" style="width:65%;">
                        <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label101") %>'></asp:Label>
                    </span>
                    
                    <span style="width:30%;">
                        <asp:Label ID="txtFechaLimiteEmision" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto101").ToString() %>' onfocus="javascript:showcalendarG(this.id)"></asp:Label>
                        <asp:Label ID="lblFechaLimOriginal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto101").ToString() %>' Visible="false"></asp:Label>
                    </span>
                </div>

                <asp:Repeater ID="rptPasajeros" runat="server">
                    <HeaderTemplate>
                        <div class="table_ t_center" style="width:15%;">
                            <h5 class="vino">
                                <asp:Label ID="Label31" runat="server" Text="Descripcion"></asp:Label>
                            </h5>
                        </div>

                        <div class="table_ t_center" style="width:15%;">
                            <h5 class="vino">
                                <asp:Label ID="Label14" runat="server" Text="Reservar a nombre de"></asp:Label>
                            </h5>
                        </div>

                        <%-- 
                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label57" runat="server" Text="Genero"></asp:Label>
                            </h5>
                        </div>

                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label58" runat="server" Text="Fecha Nacim."></asp:Label>
                            </h5>
                        </div>

                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label59" runat="server" Text="Nacionalidad"></asp:Label>
                            </h5>
                        </div>

                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label60" runat="server" Text="Num. Pasaporte"></asp:Label>
                            </h5>
                        </div>

                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label61" runat="server" Text="Fecha Exp. Pasaporte"></asp:Label>
                            </h5>
                        </div>

                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label62" runat="server" Text="Pais Residencia"></asp:Label>
                            </h5>
                        </div>
                        --%>
                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label15" runat="server" Text="Moneda"></asp:Label>
                            </h5>
                        </div>

                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label49" runat="server" Text="Valor Total"></asp:Label>
                            </h5>
                        </div>

                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label44" runat="server" Text="Descuento"></asp:Label>
                            </h5>
                        </div>

                        <div class="table_ t_center">
                            <h5 class="vino">
                                <asp:Label ID="Label16" runat="server" Text="Valor a pagar"></asp:Label>
                            </h5>
                        </div>
                    </HeaderTemplate>
                    
                    <ItemTemplate>
                        <div style="float:left; width:100%;">
                            <div class="table_ t_center" style="width:15%;">
                                <span class="even block">
                                    <asp:Label ID="lblCodTarifa" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.intCodigFare") %>'></asp:Label>
                                    <asp:Label ID="lblTipoPax" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.intTipoPax") %>'></asp:Label>
                                    <asp:Label ID="lblDetalleTipoPax" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalle") %>'></asp:Label>
                                    <asp:Label ID="lblSTipoPax" runat="server" Width="100" Text='<%# DataBinder.Eval(Container,"DataItem.Acomodacion") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:15%;">
                                <span class="even block">
                                    <%--<asp:Label ID="lblDescTipoPax" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalle") %>'></asp:Label>--%>
                                    <asp:Label ID="Label14" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.strNombre") %>'></asp:Label>
                                </span>
                            </div>

                            <%-- 
                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:Label ID="Label63" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Genero") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:Label ID="Label64" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaNac") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:Label ID="Label65" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Nacionalidad") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:Label ID="Label66" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NumPasaporte") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:Label ID="Label67" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.FechaExp") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:Label ID="Label68" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PaisResidencia") %>'></asp:Label>
                                </span>
                            </div>
                            --%>
                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:Label ID="Label15" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:TextBox ID="txtTotal" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Neto") %>'></asp:TextBox>
                                    <asp:Label ID="lblTotalOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Neto") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:TextBox ID="txtDescuento" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Descuento") %>'></asp:TextBox>
                                    <asp:Label ID="lblDescuentoOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Descuento") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center">
                                <span class="even block">
                                    <asp:TextBox ID="txtValorPasajero" runat="server" Width="10%" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container,"DataItem.Total")).ToString("###,##0.00") %>' Visible="false"></asp:TextBox>
                                    <asp:Label ID="lblValorOriginal" Visible="false" runat="server" Width="100" Text='<%# DataBinder.Eval(Container,"DataItem.Total") %>'></asp:Label>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container,"DataItem.Total")).ToString("###,##0.00") %>'></asp:Label>
                                </span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                </ItemTemplate>
                <SeparatorTemplate>
                </SeparatorTemplate>
            </asp:Repeater>


            <!-- reserva aerea -->
            <asp:Repeater ID="rptReservaAereas" runat="server">
                <ItemTemplate>
                    <div class="full">
                        <span class="vino titul">
                            Record de reserva servicio
                            <asp:Label ID="lblTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TipoPlan") %>' style="float: none;"></asp:Label>
                            :
                        </span>

                        <asp:Label ID="lblReserva" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strReserva") %>'></asp:Label>
                        <asp:Label ID="lblRefereTipoPlan" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strcodigo") %>' Visible="false"></asp:Label>                                                                    
                        <%--
                        <asp:Label ID="lblcodigoascesortext" runat="server" Text='Codigo Ascesor' />
                        <asp:Label ID="lblCodigoascesor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "strCodigoAscesor") %>' />
                        --%>
                        <asp:Label ID="lblintTipoReserva" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "intTipo") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblEstadoReserva" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "intEstado") %>' Visible="false"></asp:Label>
                    </div>

                    <div class="full">
                        <span class="vino titul">
                            <asp:Label ID="lbl1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label1") %>'></asp:Label>
                        </span>

                        <asp:Label ID="dat1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto1") %>'></asp:Label>
                    </div>

                    <div class="full">
                        <span class="vino titul">
                            <asp:Label ID="lbl2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label2") %>'></asp:Label>
                        </span>

                        <asp:Label ID="dat2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto2") %>'></asp:Label>
                    </div>                    
                    
                    <asp:Repeater ID="rptPlan" runat="server">
                        <ItemTemplate>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label3") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto3") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label4") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto4") %>'></asp:Label>
                            </div>
                            
                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label5") %>'></asp:Label>
                                </span

                                <asp:Label ID="dat5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto5") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label6") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto6") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label7") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat7" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto7") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label8") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat8" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto8") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label9") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto9") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label10") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto10") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label11") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat11" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto11") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label12") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto12") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label13") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto13") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label14") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto14") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label15") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat15" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto15") %>'></asp:Label>
                            </div>

                            <div class="full">
                                <span class="vino titul">
                                    <asp:Label ID="lbl16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label16") %>'></asp:Label>
                                </span>

                                <asp:Label ID="dat16" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto16") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    
                    
                    <asp:Repeater ID="rptPasajeros" runat="server">
                        <HeaderTemplate>
                            <div style="float:left; width:100%;">
                                <div class="table_ t_center" style="width:20%;">
                                    <h5 class="vino">
                                        <asp:Label ID="Label14" runat="server" Text="Reservar a nombre de"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center" style="width:16%;">
                                    <h5 class="vino">
                                        <asp:Label ID="Label72" runat="server" Text="Documento ident."></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center" style="width:16%;">
                                    <h5 class="vino">
                                        <asp:Label ID="Label73" runat="server" Text="Fecha nacimiento"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center" style="width:16%;">
                                    <h5 class="vino">
                                        <asp:Label ID="Label15" runat="server" Text="Moneda"></asp:Label>
                                    </h5>
                                </div>

                                <%--
                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label34" runat="server" Text="Tarifa"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label53" runat="server" Text="Combustible"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label54" runat="server" Text="Iva"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label35" runat="server" Text="Otros impuestos"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label36" runat="server" Text="Tarifa Admin."></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label37" runat="server" Text="Iva Tarifa Admin"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label38" runat="server" Text="Fee Agencia"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label39" runat="server" Text="Iva Fee Agencia"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center">
                                    <h5 class="vino">
                                        <asp:Label ID="Label44" runat="server" Text="Descuento"></asp:Label>
                                    </h5>
                                </div>
                                --%>

                                <div class="table_ t_center" style="width:16%;">
                                    <h5 class="vino">
                                        <asp:Label ID="Label16" runat="server" Text="Total"></asp:Label>
                                    </h5>
                                </div>

                                <div class="table_ t_center" style="width:16%;">
                                    <h5 class="vino">
                                        <asp:Label ID="Label25" runat="server" Text="Número de tiquete"></asp:Label>
                                    </h5>
                                </div>
                            </div>                                    
                        </HeaderTemplate>
                        
                        <ItemTemplate>
                            <div class="table_ t_center" style="width:20%;">
                                <span class="even block">
                                    <asp:Label ID="Label14" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.strNombre") %>'></asp:Label>
                                    <asp:Label ID="lblCodTarifa" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.intCodigFare") %>'></asp:Label>
                                    <asp:Label ID="lblTipoPax" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.intTipoPax") %>'></asp:Label>
                                    <asp:Label ID="lblDetalleTipoPax" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.strDetalle") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="Label69" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Documento") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="Label70" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.FechaNac") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="Label15" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Moneda") %>'></asp:Label>
                                </span>
                            </div>

                            <%--

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtTarifa" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Tarifa") %>'></asp:Label>
                                    <asp:Label ID="lblTarifaOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Tarifa") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtCombustible" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Combustible") %>'></asp:Label>
                                    <asp:Label ID="lblCombustibleOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Combustible") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtIva" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Iva") %>'></asp:Label>
                                    <asp:Label ID="lblIvaOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Iva") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtImpuestos" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Impuestos") %>'></asp:Label>
                                    <asp:Label ID="lblImpuestosOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Impuestos") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtTA" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.TA") %>'></asp:Label>
                                    <asp:Label ID="lblTAOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.TA") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtIvaTA" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.IvaTA") %>'></asp:Label>
                                    <asp:Label ID="lblIvaTAOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.IvaTA") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtFee" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.FEE") %>'></asp:Label>
                                    <asp:Label ID="lblFeeOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.FEE") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtIvaFee" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.IvaFEE") %>'></asp:Label>
                                    <asp:Label ID="lblIvaFeeOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.IvaFEE") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtDescuento" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Descuento") %>'></asp:Label>
                                    <asp:Label ID="lblDescuentoOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Descuento") %>'></asp:Label>
                                </span>
                            </div>                                                        
                            --%>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtTotal" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Total") %>'></asp:Label>
                                    <asp:Label ID="lblTotalOriginal" Visible="false" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.Total") %>'></asp:Label>
                                </span>
                            </div>

                            <div class="table_ t_center" style="width:16%;">
                                <span class="even block">
                                    <asp:Label ID="txtNumTiquete" runat="server" Width="95%" Text='<%# DataBinder.Eval(Container,"DataItem.NoTiquete") %>'></asp:Label>
                                    <asp:Label ID="lblCodPax" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.intCodigoPax") %>' Visible="false"></asp:Label>
                                </span>
                            </div>
                        </ItemTemplate>                        
                    </asp:Repeater>

                    <asp:Repeater ID="rptValor" runat="server">
                        <ItemTemplate>
                            <div class="full" style="width:50%; margin-top:20px;">
                                <span class="vino titul" style="width:65%;">
                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label100") %>'></asp:Label>
                                </span>

                                <asp:Label ID="Label5" runat="server" Text='<%# Convert.ToDecimal(DataBinder.Eval(Container,"DataItem.texto100")).ToString("###,###,###") %>' style="width:30%;"></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <div class="full" style="width:50%; margin-top:20px;">
                        <span class="vino titul" style="width:65%;">
                            <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "label101") %>'></asp:Label>
                        </span>

                        <%--
                        <asp:TextBox ID="txtFechaLimiteEmision" runat="server" Text='<%# Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "texto101").ToString()).ToString("yyyy/MM/dd") %>' onfocus="javascript:showcalendarG(this.id)"></asp:TextBox>
                        --%>
                        <asp:Label ID="Label13" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "texto101") %>' style="width:30%;"></asp:Label>
                    </div>

                </ItemTemplate>
            </asp:Repeater>
        </div><!-- Fin inner_box_side-->
    </div>
</div>
