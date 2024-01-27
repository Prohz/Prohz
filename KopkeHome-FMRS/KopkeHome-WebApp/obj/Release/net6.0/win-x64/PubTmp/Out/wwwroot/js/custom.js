$(document).ready(function () {
    $('input[name="intervaltype"]').click(function () {
        $(this).tab('show');
        $(this).removeClass('active');
    });
    var urlprefix = '';
    if (window.location.href.indexOf("KopkeHome-FMRS-QA") > -1) {
        urlprefix = '/KopkeHome-FMRS-QA';
    }

    $.ajax({
        type: "GET",
        url: urlprefix+"/User/StateList",
        dataType: "json",
        success: function (data) {

            var s = '<option value="">Select  State</option>';


            for (var i = 0; i < data.length; i++) {
                s += '<option value="' + data[i].stateName + '">' + data[i].stateName + '</option>';
            }
            $("#ddlStates").html(s);
            $("#ddlStates2").html(s);
        }
    });

    $('input[type="number"]').on('keypress', function (e) {
        var maxlength = $(this).prop('maxlength');
        if (maxlength !== -1) {  // Prevent execute statement for non-set maxlength prop inputs
            var length = $(this).val().trim().length;
            if (length + 1 > maxlength) e.preventDefault();
        }
    });
   
    
    //below code prevents user from entering space oninput field
    $(document).on("keydown", 'input[type="text"]', function (evt) {
        var caretPos = $(this)[0].selectionStart
        if (evt.keyCode == 32 && caretPos == 0) {
            return false;
        }
    });
   
    
});




$('input[name="LiabilityInsuranceFile"]').change(function () {
    $('span[data-valmsg-for="LiabilityInsuranceFile"]').text('');
    var ext = $('input[name="LiabilityInsuranceFile"]').val().split('.').pop().toLowerCase();

    if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'pdf', 'txt', 'xlsx', 'xls', 'docx', 'doc']) == -1) {
        $('span[data-valmsg-for="LiabilityInsuranceFile"]').text('invalid file format!');

        this.value = '';
    }


});
$('input[name="WorkmanCompensationInsuranceFile"]').change(function () {
    $('span[data-valmsg-for="WorkmanCompensationInsuranceFile"]').text('');
    var ext = $('input[name="WorkmanCompensationInsuranceFile"]').val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['png', 'jpg', 'jpeg','pdf', 'txt', 'xlsx', 'xls', 'docx', 'doc']) == -1) {
        $('span[data-valmsg-for="WorkmanCompensationInsuranceFile"]').text('invalid file format!');

        this.value = '';
    }

});
$body = $("body");
$(document).on({
    ajaxStart: function ()
    {
        $body.addClass("loading");
    },
    ajaxStop: function () { $body.removeClass("loading"); }
});


$(".toggle-password").click(function () {
    $(this).toggleClass("fa-eye fa-eye-slash");
    input = $(this).parent().find("input");
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});


document.onreadystatechange = function () {
    if (document.readyState !== "complete") {

        document.querySelector(".logo-loader").style.visibility = "visible";
    } else {

        document.querySelector("body").style.visibility = "visible";
        document.querySelector(".logo-loader").style.display = "none";
    }
};















/*----------new-------------*/




$(function () {
    var dropZoneId = "drop-zone";
    var dropZone = $("#" + dropZoneId);

    var inputFile = dropZone.find("input");
    inputFile.on('change', function (e) {
        
        $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('');
        if (inputFile[0].files.length != 0) {
            

            var file1 = inputFile[0].files[0].name;
            var file_size = this.files[0].size;
            
            if (file_size < 5242880) {
                var ext = file1.split('.').pop().toLowerCase();


                if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'pdf', 'txt', 'xlsx', 'xls', 'docx', 'doc']) === -1) {
                    console.log("invalid file formate");
                    $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('invalid file format!');
                    this.value = '';
                    return false;
                }
                else {

                    var fileNum = this.files.length,
                        initial = 0,
                        counter = 0;
                    for (initial; initial < fileNum; initial++) {
                        $("#filename").empty();
                        $('#filename').append('<div class="mlt-file-list">  <span class="multiple-file-elips">' + this.files[initial].name + '</span><span id="Removefilename" class="fa fa-times-circle ml-1 mr-2 fa-lg closeBtn" title="Remove"></span></div>');
                    }
                }

            }
            else {
                $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('File size is too big.File should be under 5 MB');

                this.value = '';
                return false;
            }
        }
        else {
            console.log("no files selectes");
        }
        
    });

})






$(function () {
    var dropZoneId = "drop-zone1";
    var dropZone = $("#" + dropZoneId);


    var inputFile = dropZone.find("input");
    inputFile.on('change', function (e) {

        $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('');
        if (inputFile[0].files.length != 0) {


            var file1 = inputFile[0].files[0].name;
            var file_size = this.files[0].size;

            if (file_size < 5242880) {
                var ext = file1.split('.').pop().toLowerCase();


                if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'pdf', 'txt', 'xlsx', 'xls', 'docx', 'doc']) === -1) {
                    console.log("invalid file formate");
                    $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('invalid file format!');
                    this.value = '';
                    return false;
                }
                else {

                    var fileNum = this.files.length,
                        initial = 0,
                        counter = 0;
                    for (initial; initial < fileNum; initial++) {
                        $("#filename1").empty();
                        $('#filename1').append('<div class="mlt-file-list">  <span class="multiple-file-elips">' + this.files[initial].name + '</span><span id="Removefilename1" class="fa fa-times-circle ml-1 mr-2 fa-lg closeBtn" title="Remove"></span></div>');
                    }
                }

            }
            else {
                $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('File size is too big.File should be under 5 MB');

                this.value = '';
                return false;
            }
        }
        else {
            console.log("no files selectes");
        }

    });






  

})


$(function () {
    var dropZoneId = "drop-zone2";
    var dropZone = $("#" + dropZoneId);


    var inputFile = dropZone.find("input");
    inputFile.on('change', function (e) {

        $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('');
        if (inputFile[0].files.length != 0) {


            var file1 = inputFile[0].files[0].name;
            var file_size = this.files[0].size;

            if (file_size < 5242880) {
                var ext = file1.split('.').pop().toLowerCase();


                if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'pdf', 'txt', 'xlsx', 'xls', 'docx', 'doc']) === -1) {
                    console.log("invalid file formate");
                    $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('invalid file format!');
                    this.value = '';
                    return false;
                }
                else {


                    var fileNum = this.files.length,
                        initial = 0,
                        counter = 0;
                    for (initial; initial < fileNum; initial++) {
                        $("#filename2").empty();
                        $('#filename2').append('<div class="mlt-file-list">  <span class="multiple-file-elips">' + this.files[initial].name + '</span><span id="Removefilename2" class="fa fa-times-circle ml-1 mr-2 fa-lg closeBtn" title="Remove"></span></div>');
                    }

                   
                }

            }
            else {
                $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('File size is too big.File should be under 5 MB');

                this.value = '';
                return false;
            }
        }
        else {
            console.log("no files selectes");
        }

    });





})



$(function () {
    var dropZoneId = "drop-zone3";
    var dropZone = $("#" + dropZoneId);


    var inputFile = dropZone.find("input");
    inputFile.on('change', function (e) {

        $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('');
        if (inputFile[0].files.length != 0) {


            var file1 = inputFile[0].files[0].name;
            var file_size = this.files[0].size;

            if (file_size < 5242880) {
                var ext = file1.split('.').pop().toLowerCase();


                if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'pdf', 'txt', 'xlsx', 'xls', 'docx', 'doc']) === -1) {
                    console.log("invalid file formate");
                    $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('invalid file format!');
                    this.value = '';
                    return false;
                }
                else {

                    var fileNum = this.files.length,
                        initial = 0,
                        counter = 0;
                    for (initial; initial < fileNum; initial++) {
                        $("#filename3").empty();
                        $('#filename3').append('<div class="mlt-file-list">  <span class="multiple-file-elips">' + this.files[initial].name + '</span><span id="Removefilename3" class="fa fa-times-circle ml-1 mr-2 fa-lg closeBtn" title="Remove"></span></div>');
                    }



                    


                }

            }
            else {
                $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('File size is too big.File should be under 5 MB');

                this.value = '';
                return false;
            }
        }
        else {
            console.log("no files selectes");
        }

    });



})

////remove files
$(document).on('click', '#Removefilename', function () {
   
    $("#filenameInput").val('');
     $("#filename").empty();
});
$(document).on('click', '#Removefilename1', function () {
    
    $("#filename1Input").val('');
    $("#filename1").empty();
});
$(document).on('click', '#Removefilename2', function () {
    
    $("#filename2Input").val('');
    $("#filename2").empty();
});
$(document).on('click', '#Removefilename3', function () {
   
    $("#filename3Input").val('');
    $("#filename3").empty();
});
$(document).ready(function () {


    var readURL = function (input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.profile-pic').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }


    $(".file-upload").on('change', function () {
        readURL(this);
    });

    $(".upload-button").on('click', function () {
        $(".file-upload").click();
    });
});



