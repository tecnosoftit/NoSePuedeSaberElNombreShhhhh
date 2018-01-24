// JavaScript Document
$(function(){
	$("#mostrar").click(function(event) {
	event.preventDefault();
	$("#caja").slideToggle();
});
$("#caja a").click(function(event) {
	event.preventDefault();
	$("#caja").slideUp();
});
});