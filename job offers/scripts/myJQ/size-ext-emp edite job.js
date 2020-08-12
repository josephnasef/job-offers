$(document).ready(function () {
    $("#save").click(function () {
    //cheack if celles is empety
    if ($("#myImage").val() == "") {
        $("#err-div").fadeIn();
        $("#view-err").append(" - pleas fill all celles ");
        return false;
    }
    if ($("#myImage").val() != "") {
        //cheack extintion
        var filename = document.getElementById("myImage").value;
        var extintion = filename.substr(filename.lastIndexOf('.') + 1);
        var validationextintion = ['jpg', 'png', 'gif', 'bmp'];
        if ($.inArray(extintion, validationextintion) == -1) {
            $("#err-div").fadeIn();
            $("#view-err").append(" - select valid file in image celles 'jpg', 'png', 'gif', 'bmp' ");
            return false;
        }
        //cheack size
        var filesize = document.getElementById("myImage").files[0].size / 1024 / 1024;
        if (filesize > 2) {
            $("#err-div").fadeIn();
            $("#view-err").append(" - pleas select file less than 2MP ");
            return false;
        }
        }
    });
});