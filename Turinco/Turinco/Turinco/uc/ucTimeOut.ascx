<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucTimeOut.ascx.cs" Inherits="uc_ucTimeOut" %>

<div id="dialog" title="La sesion expiro!">
	        <p class="mensajeTimeOut">
		        <span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 50px 0;"></span>
                    <asp:Label ID="lblTextoTimeOut" runat="server" CssClass="mensajeTimeOut"></asp:Label>
		        <span id="dialog-countdown" style="font-weight:bold; display:none;"></span>
	        </p>

            
            <a class="botonAceptarTime" href="index.aspx">
                <input value="Aceptar" type="button" class="link-button blue"/>
            </a>
</div>

<script>
    //Time Out

    $("#dialog").dialog({
        autoOpen: false,
        modal: true,
        width: 400,
        height: 200,
        closeOnEscape: false,
        draggable: false,
        resizable: false,
        buttons: {
            'Aceptar': function () {
                // fire whatever the configured onTimeout callback is.
                // using .call(this) keeps the default behavior of "this" being the warning
                // element (the dialog in this case) inside the callback.
                //$.idleTimeout.options.onTimeout.call(this);
                window.location = "index.aspx";
            }
        }
    });

    // cache a reference to the countdown element so we don't have to query the DOM for it on each ping.
    var $countdown = $("#dialog-countdown");

    // start the idle timer plugin
    $.idleTimeout('#dialog', 'div.ui-dialog-buttonpane button:first', {
        idleAfter: 300,
        pollingInterval: 2,
        onTimeout: function () {
            //alert("Hola");
            //window.location = "index.aspx";
        },
        onIdle: function () {
            $(this).dialog("open");
        },
        onCountdown: function (counter) {
            $countdown.html(counter); // update the counter
        }
    });

    //Fin Time Out
</script>