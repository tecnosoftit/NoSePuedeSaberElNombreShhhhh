i = 0;
(function ($) {
    $.fn.extend({
        validarTarjetaCredito: function () {
            return this.each(function () {
                $(this).keyup(function (e) {
                    if(i === 0)
                    {
                        $(this).after("<div id='tipoTarjeta'></div>");
                        i++;
                    }
                    var num = $(this).val().toString();
                    var charCount = num.length;                    
                    if(charCount > 0)
                    {
                        var franquicia = $("[id*='ddlFranquiciaPOL'] :selected").text().toUpperCase();                        
                        /* VALIDACION DE TIPO */
                        if (charCount == 1) {
                            if (num == "4" && franquicia.indexOf("VISA") != -1) {
                                $("#tipoTarjeta").html("VISA");
                                $(this).removeClass("lblError");
                                $("#tipoTarjeta").html("");
                            }
                            else
                            {
                                if(franquicia.indexOf("DIN") == -1 && franquicia.indexOf("AME") == -1)
                                {
                                    $(this).addClass("lblError");
                                    $("#tipoTarjeta").html("FRANQUICIA INCORRECTA");
                                }
                            }
                        }
                        if (charCount == 2) 
                        {
                            if ((num == "34" || num == "37") && franquicia.indexOf("AME") != -1) {
                                $("#tipoTarjeta").html("AMEX");
                                $(this).removeClass("lblError");
                                $("#tipoTarjeta").html("");
                            } 
                            else
                            {
                                if ((num == "36" || num == "38" || num == "39") && franquicia.indexOf("DIN") != -1) 
                                {
                                    $("#tipoTarjeta").html("DINERS");
                                    $(this).removeClass("lblError");
                                    $("#tipoTarjeta").html("");
                                }
                                else 
                                {
                                    if(franquicia.indexOf("VISA") == -1)
                                    {
                                        $(this).addClass("lblError");
                                        $("#tipoTarjeta").html("FRANQUICIA INCORRECTA");
                                    }
                                }
                            } 
                           
//                             if (num == "51" || num == "52" || num == "53" || num == "54"|| num == "55") 
//                            {
//                                $("#tipoTarjeta").html("MASTER CARD");
//                            }
//                             else 
//                             {
//                             
//                             }
                        }
                        if (charCount == 3) 
                        {
                            if ((num == "300" || num == "305" || num == "309") && franquicia.indexOf("DIN") != -1) 
                            {
                                $("#tipoTarjeta").html("DINERS");
                                $(this).removeClass("lblError");
                                $("#tipoTarjeta").html("");
                            }
                            else 
                            {
                                if(franquicia.indexOf("VISA") == -1 && franquicia.indexOf("AME") == -1)
                                {
                                    $(this).addClass("lblError");
                                    $("#tipoTarjeta").html("FRANQUICIA INCORRECTA");
                                }
                            }
                        }
                        /* !VALIDACION DE TIPO */

                        /* ALGORITMO */
                        if ((charCount > 14 && franquicia.indexOf("DIN") != -1) || (charCount > 15 && franquicia.indexOf("AME") != -1) || 
                            (charCount > 16 && franquicia.indexOf("VISA") != -1)) 
                        {
                            $(this).addClass("lblError");
                            $("#tipoTarjeta").html("CANTIDAD DE DIGITOS INCORRECTA");
                        }
                        else
                        {
                            if ((charCount > 2 && charCount < 14 && franquicia.indexOf("DIN") != -1) || 
                                (charCount > 2 && charCount < 15 && franquicia.indexOf("AME") != -1) || 
                                (charCount > 1 && charCount < 16 && franquicia.indexOf("VISA") != -1)) 
                            {
                                $(this).addClass("lblError");
                                $("#tipoTarjeta").html("CANTIDAD DE DIGITOS INCORRECTA");
                            }
                            else
                            {
                                if ((charCount == 14 && franquicia.indexOf("DIN") != -1) || (charCount == 15 && franquicia.indexOf("AME") != -1) || 
                                    (charCount == 16 && franquicia.indexOf("VISA") != -1)) 
                                {
                                    var valid = isValid(num, charCount);
                                    if (valid) 
                                    {
                                        $("#tipoTarjeta").html("TARJETA VALIDA");
                                        //$("input").attr("name", "checkout.cardNumber").attr("class", "valid-card");
                                        $(this).removeClass("lblError");
                                    } 
                                    else 
                                    {
                                        $("#tipoTarjeta").html("TARJETA INVALIDA");
                                        $(this).addClass("lblError");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        $(this).removeClass("lblError");
                         $("#tipoTarjeta").html("");
                    }
                    /* !ALGORITMO */
                });
            });
        }
    });
})(jQuery)
function isValid(ccNum, charCount) {
    var double = true;
    var numArr = [];
    var sumTotal = 0;
    for (i = 0; i < charCount; i++) {
        var digit = parseInt(ccNum.charAt(i));

        if (double) {
            digit = digit * 2;
            digit = toSingle(digit);
            double = false;
        } else {
            double = true;
        }
        numArr.push(digit);
    }


    for (i = 0; i < numArr.length; i++) {
        sumTotal += numArr[i];
    }
    var diff = eval(sumTotal % 10);
    //console.log(diff);
    //console.log(diff == "0");
    return (diff == "0");
}

function toSingle(digit) {
    if (digit > 9) {
        var tmp = digit.toString();
        var d1 = parseInt(tmp.charAt(0));
        var d2 = parseInt(tmp.charAt(1));
        return (d1 + d2);
    } else {
        return digit;
    }
}
