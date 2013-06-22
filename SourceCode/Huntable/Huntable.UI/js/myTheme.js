	//Our '.hero' and '.villain' in an epic battle to be styled the best!

$(document).ready(function(){
	$(".post:first").addClass("hero");
	$(".post:nth-child(2)").addClass("villain");
	
	//And our little animated sliding area uptop of the design.
	
	$('#tabContent>li:gt(0)').hide();
	$('#tabsNav li:first').addClass('active');
	$('#tabsAndContent #tabsNav li').bind('click', function() {
		$('li.active').removeClass('active');
		$(this).addClass('active');
		var target = $('a', this).attr('href');
		$(target).slideDown(400).siblings().slideUp(300);
		return false;
	});
});

$(document).ready(function () {
    $(".post:first").addClass("hero");
    $(".post:nth-child(2)").addClass("villain");

    //And our little animated sliding area uptop of the design.

    $('#tabContent>li:gt(0)').hide();
    $('#tabsNav1 li:first').addClass('active');
    $('#tabsAndContent #tabsNav1 li').bind('click', function () {
        $('li.active').removeClass('active');
        $(this).addClass('active');
        var target = $('a', this).attr('href');
        $(target).slideDown(400).siblings().slideUp(300);
        return false;
    });
});