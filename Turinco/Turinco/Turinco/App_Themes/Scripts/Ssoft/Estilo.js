

/*Esta funcion es utilizada para mostras y/o ocultar controles con el toggle*/
//$(document).ready(function(){

//	//Hide (Collapse) the toggle containers on load
//	$(".toggle_container_Preguntas").hide(); 

//	//Switch the "Open" and "Close" state per click then slide up/down (depending on open/close state)
//	$("h4.trigger").click(function(){
//		$(this).toggleClass("active").next().slideToggle("slow");
//	});
///*Estas funcion es utilizada para la paginacion, ordenamiento y friltro de las grillas*/
//});
$(document).ready( function() 
{
    $('.example').dataTable( 
    {
        "bJQueryUI": true,
        "sPaginationType": "full_numbers"
    } );
} );			


	$(function(){

		// Accordion
		$("#accordion").accordion({ header: "h3" });

		// Tabs
		$("#tabs").tabs();

		// Dialog			
		$('#dialog').dialog({
			autoOpen: false,
			width: 600,
			buttons: {
				"Ok": function() { 
					$(this).dialog("close"); 
				}, 
				"Cancel": function() { 
					$(this).dialog("close"); 
				} 
			}
		});
		
		// Dialog Link
		$('#dialog_link').click(function(){
			$('#dialog').dialog('open');
			return false;
		});

		
		// Slider
		$('#slider').slider({
			range: true,
			values: [17, 67]
		});
		
		// Progressbar
		$("#progressbar").progressbar({
			value: 20 
		});
		
		//hover states on the static widgets
		$('#dialog_link, ul#icons li').hover(
			function() { $(this).addClass('ui-state-hover'); }, 
			function() { $(this).removeClass('ui-state-hover'); }
		);
		
	});
	
	$(".area").fadeTransition({
	    pauseTime: 5000, 
	    transitionTime: 2000, 
	    delayStart: 3000, 
	    pauseNavigation: true
	});

      (function ($) {
        $.fn.fadeTransition = function(options) {
          var options = $.extend({pauseTime: 5000, transitionTime: 2000, ignore: null, delayStart: 0, pauseNavigation: false}, options);
          var transitionObject;

          Trans = function(obj) {
            var timer = null;
            var current = 0;
            var els = (options.ignore)?$("> *:not(" + options.ignore + ")", obj):$("> *", obj);
            $(obj).css("position", "relative");
            els.css("display", "none").css("left", "0").css("top", "0").css("position", "absolute");
            
            if (options.delayStart > 0) {
              setTimeout(showFirst, options.delayStart);
            }
            else
              showFirst();

            function showFirst() {
              if (options.ignore) {
                $(options.ignore, obj).fadeOut(options.transitionTime);
                $(els[current]).fadeIn(options.transitionTime);
              }
              else {
                $(els[current]).css("display", "block");
              }
            }

            function transition(next) {
              $(els[current]).fadeOut(options.transitionTime);
              $(els[next]).fadeIn(options.transitionTime);
              current = next;
              cue();
            };

            function cue() {
              if ($("> *", obj).length < 2) return false;
              if (timer) clearTimeout(timer);
              if (!options.pauseNavigation) {
                timer = setTimeout(function() { transition((current + 1) % els.length | 0)} , options.pauseTime);
              }
            };
            
            this.showItem = function(item) {
              if (timer) clearTimeout(timer);
              transition(item);
            };

            cue();
          }

          this.showItem = function(item) {
            transitionObject.showItem(item);
          };

          return this.each(function() {
            transitionObject = new Trans(this);
          });
        }

      })(jQuery);
    
      var page = {
        tr: null,
        init: function() {
          page.tr = $(".area").fadeTransition({pauseTime: 5000, transitionTime: 2000, ignore: "#introslide", delayStart: 2000});
          $("div.navigation").each(function() {
            $(this).children().each( function(idx) {
              if ($(this).is("a"))
                $(this).click(function() { page.tr.showItem(idx); return false; })
            });
          });
        },

        show: function(idx) {
          if (page.tr.timer) clearTimeout(page.tr.timer);
          page.tr.showItem(idx);
        }
      };

      $(document).ready(page.init);    

$(document).ready(function(){
  
$("#promociones").wslide({
    width: 560,
    height: 375,
    autolink: false,
    fade: true,
    duration: 1000
 });
});

/*Funcion Trim*/
function trim(myString)
{
    return myString.replace(/^\s+/g,'').replace(/\s+$/g,'')
}
 
/* Transicion Imagenes Corporativo */ 
  (function ($) {
    $.fn.fadeTransition = function(options) {
      var options = $.extend({pauseTime: 0, transitionTime: 0, ignore: null, delayStart: 0, pauseNavigation: false}, options);
      var transitionObject;

      Trans = function(obj) {
        var timer = null;
        var current = 0;
        var els = (options.ignore)?$("> *:not(" + options.ignore + ")", obj):$("> *", obj);
        $(obj).css("position", "relative");
        els.css("display", "none").css("left", "0").css("top", "0").css("position", "relative");
        
        if (options.delayStart > 0) {
          setTimeout(showFirst, options.delayStart);
        }
        else
          showFirst();

        function showFirst() {
          if (options.ignore) {
            $(options.ignore, obj).fadeOut(options.transitionTime);
            $(els[current]).fadeIn(options.transitionTime);
          }
          else {
            $(els[current]).css("display", "block");
          }
        }

        function transition(next) {
          $(els[current]).fadeOut(options.transitionTime);
          $(els[next]).fadeIn(options.transitionTime);
          current = next;
          cue();
        };

        function cue() {
          if ($("> *", obj).length < 2) return false;
          if (timer) clearTimeout(timer);
          if (!options.pauseNavigation) {
            timer = setTimeout(function() { transition((current + 1) % els.length | 0)} , options.pauseTime);
          }
        };
        
        this.showItem = function(item) {
          if (timer) clearTimeout(timer);
          transition(item);
        };

        cue();
      }

      this.showItem = function(item) {
        transitionObject.showItem(item);
      };

      return this.each(function() {
        transitionObject = new Trans(this);
      });
    }

  })(jQuery);

//  var page = {
//    tr: null,
//    init: function() {
//      page.tr = $(".banners").fadeTransition({pauseTime: 0, transitionTime: 0, ignore: "#introslide", delayStart: 0});
//      $("div.navigation").each(function() {
//        $(this).children().each( function(idx) {
//          if ($(this).is("a"))
//            $(this).click(function() { page.tr.showItem(idx); return false; })
//        });
//      });
//    },

//    show: function(idx) {
//      if (page.tr.timer) clearTimeout(page.tr.timer);
//      page.tr.showItem(idx);
//    }
//  };

  $(document).ready(page.init);    
 

/*ABRE LA GALERIA DE IMAGENES*/
function AbrirGaleria(intCodigo)
{
    $('#iGaleria').attr('src', 'Galeria.aspx?Id=' + intCodigo);
    $find('MPEEGaleria').show();   
}

/* Galeria Destino Sugerido */
$(document).ready(function() {

	$.featureList(
		$(".galeria li a"),
		$(".output li"), {
			start_item	:	0
		}
	);

});


//jQuery(document).ready(function($) {
//    // We only want these styles applied when javascript is enabled
//    $('div.content').css('display', 'block');

//    // Initially set opacity on thumbs and add
//    // additional styling for hover effect on thumbs
//    var onMouseOutOpacity = 0.67;
//    $('#thumbs ul.thumbs li, div.navigation a.pageLink').opacityrollover({
//	    mouseOutOpacity:   onMouseOutOpacity,
//	    mouseOverOpacity:  1.0,
//	    fadeSpeed:         'fast',
//	    exemptionSelector: '.selected'
//    });
//	
//	
//    // Initialize Advanced Galleriffic Gallery
//    var gallery = $('#thumbs').galleriffic({
//	    delay:                     2500,
//	    numThumbs:                 10,
//	    preloadAhead:              10,
//	    enableTopPager:            false,
//	    enableBottomPager:         false,
//	    imageContainerSel:         '#slideshow',
//	    controlsContainerSel:      '#controls',
//	    captionContainerSel:       '#caption',
//	    loadingContainerSel:       '#loading',
//	    renderSSControls:          true,
//	    renderNavControls:         true,
//	    playLinkText:              'Play Slideshow',
//	    pauseLinkText:             'Pause Slideshow',
//	    prevLinkText:              '&lsaquo; Previous Photo',
//	    nextLinkText:              'Next Photo &rsaquo;',
//	    nextPageLinkText:          'Next &rsaquo;',
//	    prevPageLinkText:          '&lsaquo; Prev',
//	    enableHistory:             true,
//	    autoStart:                 false,
//	    syncTransitions:           true,
//	    defaultTransitionDuration: 900,
//	    onSlideChange:             function(prevIndex, nextIndex) {
//		    // 'this' refers to the gallery, which is an extension of $('#thumbs')
//		    this.find('ul.thumbs').children()
//			    .eq(prevIndex).fadeTo('fast', onMouseOutOpacity).end()
//			    .eq(nextIndex).fadeTo('fast', 1.0);

//		    // Update the photo index display
//		    this.$captionContainer.find('div.photo-index')
//			    .html('Photo '+ (nextIndex+1) +' of '+ this.data.length);
//	    },
//	    onPageTransitionOut:       function(callback) {
//		    this.fadeTo('fast', 0.0, callback);
//	    },
//	    onPageTransitionIn:        function() {
//		    var prevPageLink = this.find('a.prev').css('visibility', 'hidden');
//		    var nextPageLink = this.find('a.next').css('visibility', 'hidden');
//			
//		    // Show appropriate next / prev page links
//		    if (this.displayedPage > 0)
//			    prevPageLink.css('visibility', 'visible');

//		    var lastPage = this.getNumPages() - 1;
//		    if (this.displayedPage < lastPage)
//			    nextPageLink.css('visibility', 'visible');

//		    this.fadeTo('fast', 1.0);
//	    }
//    });

//    /**************** Event handlers for custom next / prev page links **********************/

//    gallery.find('a.prev').click(function(e) {
//	    gallery.previousPage();
//	    e.preventDefault();
//    });

//    gallery.find('a.next').click(function(e) {
//	    gallery.nextPage();
//	    e.preventDefault();
//    });

//    /****************************************************************************************/

//    /**** Functions to support integration of galleriffic with the jquery.history plugin ****/

//    // PageLoad function
//    // This function is called when:
//    // 1. after calling $.historyInit();
//    // 2. after calling $.historyLoad();
//    // 3. after pushing "Go Back" button of a browser
//    function pageload(hash) {
//	    // alert("pageload: " + hash);
//	    // hash doesn't contain the first # character.
//	    if(hash) {
//		    $.galleriffic.gotoImage(hash);
//	    } else {
//		    gallery.gotoIndex(0);
//	    }
//    }

//    // Initialize history plugin.
//    // The callback is called at once by present location.hash. 
//    $.historyInit(pageload, "advanced.html");

//    // set onlick event for buttons using the jQuery 1.3 live method
//    $("a[rel='history']").live('click', function(e) {
//	    if (e.button != 0) return true;

//	    var hash = this.href;
//	    hash = hash.replace(/^.*#/, '');

//	    // moves to a new page. 
//	    // pageload is called at once. 
//	    // hash don't contain "#", "?"
//	    $.historyLoad(hash);

//	    return false;
//    });

//    /****************************************************************************************/
//});
    

/* Slide Promociones Index*/
$(document).ready(function(){	
	$(".utilitarios").easySlider({
		auto: true,
		continuous: true 
	});
});	


function Imagenes_Cambio()
{
    $("h2").append('<em></em>') 
    $(".galeriaPlan a").click(function(){
 
        var largePath = $(this).attr("href");
        var largeAlt = $(this).attr("title");
 
    $(".largeImg").attr({ src: largePath, alt: largeAlt });
 
    $("h2 em").html(" (" + largeAlt + ")"); return false;
    });
}

/* Tabs Destinos */
$(document).ready(function()
{
	Tabs_Destinos();
	Imagenes_Cambio();
}); 

function Tabs_Destinos(){
    $(".tab_content").hide();
	$("ul.tabs li:first").addClass("active").show();
	$(".tab_content:first").show();

	$("ul.tabs li").click(function()
       {
		$("ul.tabs li").removeClass("active");
		$(this).addClass("active");
		$(".tab_content").hide();

		var activeTab = $(this).find("a").attr("href");
		$(activeTab).fadeIn();
		return false;
	});
}

/* Carrusel vertical */
jQuery(document).ready(function() {
    jQuery('#carruselNoticias').jcarousel({
        vertical: true,
        scroll: 2
    });
});