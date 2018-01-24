// JavaScript Document

// Integration function. Add custom code here
function ClickTale_WhenAvailableCallback() {
	ClickTale(6789,1,"www09");
}
 
function ClickTale_GetRecorderFileUrl() {
	return "http://s.clicktale.net/WRb.js";
}
 
window.CallClickTaleWhenAvailable = function() {
	// on first call, remove the function from the window
	window.CallClickTaleWhenAvailable = undefined;
 
	var loopCount = 0,
		// set to maximum number of tries running clicktale on page
		loopMax = 200;
 
	function CheckForClickTale() {
		if(typeof ClickTale == "function") {
			if(typeof ClickTale_WhenAvailableCallback != "function") return;
			ClickTale_WhenAvailableCallback();
		} else if(loopCount < loopMax) {
			loopCount++;
			setTimeout(arguments.callee, 100);
		}
	}
 
	// the first call
	CheckForClickTale();
};
// minified version of window.CallClickTaleWhenAvailable
// window.CallClickTaleWhenAvailable=function(){window.CallClickTaleWhenAvailable=undefined;var a=0,b=200;function c(){if(typeof ClickTale=="function"){if(typeof ClickTale_WhenAvailableCallback!="function"){return}ClickTale_WhenAvailableCallback()}else{if(a<b){a++;setTimeout(arguments.callee,100)}}}c()};
 
document.write('<div id="ClickTaleDiv" style="display: none;"></div>');
document.write('<script src="' + ClickTale_GetRecorderFileUrl() + '" type="text/javascript"></script>');
document.write('<script type="text/javascript">');
document.write('window.CallClickTaleWhenAvailable();');
document.write('</script>');