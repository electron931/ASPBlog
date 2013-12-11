$(document).ready(function () {
    var color = $("#body").css("background-color").toString();
    if (color == "rgb(85, 85, 85)") {
        $("#logo").attr("src", "App_Themes/Christmas/Images/santa-hat-clip-art.png");
        $.fn.snow();
    }

});