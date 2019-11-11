$(document).ready(function() {
    $(window).resize(function() {
        // if window is smaller than 500 px
        if (window.matchMedia('(max-width: 500px)').matches) {
            // hide small-hide
            $(".small-hide").addClass("hide");
            // show small-show
            $(".small-show").removeClass("hide");
        } else {
            // window is bigger than 500px
            // show small-hide
            $(".small-hide").removeClass("hide");
            // hide small-show
            $(".small-show").addClass("hide");
        }
    }).resize(); // This will simulate a resize to trigger the initial run.
});