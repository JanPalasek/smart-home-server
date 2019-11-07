// on scroll if it's bellow certain line, display arrow that moves user to the top
$(window).on('scroll', function () {
    if ($(this).scrollTop() > 50) {
        $('#to-top').fadeIn(400);
    } else {
        $('#to-top').fadeOut(400);
    }
});

// on click move to top of the page
$("#to-top").click(function () {
    $("html, body").animate({ scrollTop: 0 }, 800);
});