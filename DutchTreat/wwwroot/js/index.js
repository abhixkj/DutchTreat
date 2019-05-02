$(document).ready(function () {

    $("#theForm").hide();

    $("#buyButton").on("click", function () {
        console.log("Buying Item.");
    });

    $(".product-props li").on("click", function () {
        console.log("Clicked on " + $(this).text());
    });

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.toggle(100);
    });

});




