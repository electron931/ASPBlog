$(document).ready(function () {

    var slides = [$(".slider div ul.slides li.first_slide"),
				   $(".slider div ul.slides li.second_slide"),
				   $(".slider div ul.slides li.third_slide")];

    var bottom_buttons = [$(".slider div ol.flex-control-nav li a.first_button"),
						   $(".slider div ol.flex-control-nav li a.second_button"),
						   $(".slider div ol.flex-control-nav li a.third_button")];

    var i = 0;
    var j = 0;

    var captions = [$(".slider div ul.slides li.first_slide .caption_wrapper"),
					 $(".slider div ul.slides li.second_slide .caption_wrapper"),
					 $(".slider div ul.slides li.third_slide .caption_wrapper")];

    for (var k = 1; k < captions.length; k++)
        captions[k].slideUp("slow");

    $(".slider div ol.flex-control-nav li a").each(function (index) {
        $(this).click(function () {
            captions[i].slideUp("slow");
            bottom_buttons[j].removeClass("flex-active");
            $(this).addClass("flex-active");
            slides[i].fadeOut('slow');
            i = j = index;
            slides[index].fadeIn('slow');
            captions[i].slideDown("slow");
        });
    });

    $(".flex-next").click(function (e) {
        e.preventDefault();
        nextSlide();
    });

    $(".flex-prev").click(function (e) {
        e.preventDefault();
        prevSlide();
    });

    //search button
    $('.search_btn').click(function () {
        $('.sf-menu').toggle();
        $('.search_box').toggle();
        $(this).toggleClass("close");
    });

    function nextSlide() {
        captions[i].slideUp("slow");
        slides[i].fadeOut('slow');
        bottom_buttons[j].removeClass("flex-active");
        i++;
        j++;
        if (i > slides.length - 1) {
            i = j = 0;
        }
        bottom_buttons[j].addClass("flex-active");
        slides[i].fadeIn('slow');
        captions[i].slideDown("slow");
    }

    function prevSlide() {
        captions[i].slideUp("slow");
        slides[i].fadeOut('slow');
        bottom_buttons[j].removeClass("flex-active");
        i--;
        j--;
        if (i < 0) {
            i = j = slides.length - 1;
        }
        bottom_buttons[j].addClass("flex-active");
        slides[i].fadeIn('slow');
        captions[i].slideDown("slow");
    }

    $(".sf-with-ul").hover(function () {
        $(".sub-menu").css("visibility", "visible");
        $(".sub-menu").slideDown(500);
    });

    setInterval(nextSlide, 5000);

});