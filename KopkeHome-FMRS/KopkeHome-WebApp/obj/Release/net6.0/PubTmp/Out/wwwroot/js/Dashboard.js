


/*const { debug } = require("util");*/
$(document).ajaxSend(function (event, jqXHR, ajaxOptions) {
    if (ajaxOptions.type === 'POST') {
        jqXHR.setRequestHeader('__RequestVerificationToken', $('input[name="__RequestVerificationToken"]').val());
    }
});

var spanishDatableObject = {
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_ registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "Ningún dato disponible en esta tabla",
    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sInfoPostFix": "",
    "sSearch": "Buscar:",
    "sUrl": "",
    "sInfoThousands": ",",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior"
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }
}

$(document).ready(function () {
    $('#example').DataTable({

        "language": $('#ProhzLangDDL').val() == 'es' ? spanishDatableObject : null,
        "scrollX": true,
        "scrollY": "100%",
        "scrollCollapse": true,
        "responsive": true,
    });
    var urlprefix = '';
    if (window.location.href.indexOf("KopkeHome-FMRS-QA") > -1)
    {
        urlprefix = '/KopkeHome-FMRS-QA';
    }
    var ProhzGreyLogo = '<img style="height: 90px;" src="' + urlprefix + '/images/ProhzGreyLogo.new.png">';
    
    
    $(document).ready(function () {

        //business profile
        $("#YesCompanyWebsiteURL").click(function () {
            $("#web4").show();
        });
        $("#NoCompanyWebsiteURL").click(function () {
            $('input[name="CompanyWebsiteURL"]').val("");
            $("#web4").hide();
        });
        $("#credit3").click(function () {
            $("#webfacebook").show();
        });
        $("#credit4").click(function () {
            $('input[name="FacebookPageURL"]').val("");
            $("#webfacebook").hide();
        });

        $("#credit20").click(function () {
            $("#webEstimateCharge").show();
        });
        $("#credit19").click(function () {
            $('input[name="EstimateCharge"]').val("");
            $("#webEstimateCharge").hide();
        });

        $("#credit21").click(function () {
            $("#webDesignServices").show();
        });
        $("#credit22").click(function () {
            $('input[name="DesignServices"]').val("");
            $("#webDesignServices").hide();
        });

        //files fileds show hide
        $("#credit13").click(function () {
            $("#webBusinessOrTradeLicenseFiles").show();
        });
        $("#credit14").click(function () {

            $('input[name="BusinessOrTradeLicenseFiles"]').val("");
            $("#webBusinessOrTradeLicenseFiles").hide();
        });
        $("#credit15").click(function () {
            $("#webLiabilityInsuranceFile").show();
        });
        $("#credit16").click(function () {
            $('input[name="LiabilityInsuranceFile"]').val("");
            $("#webLiabilityInsuranceFile").hide();
        });
        $("#credit17").click(function () {
            $("#webWorkmanCompensationInsuranceFile").show();
        });
        $("#credit18").click(function () {
            $('input[name="WorkmanCompensationInsuranceFile"]').val("");
            $("#webWorkmanCompensationInsuranceFile").hide();
        });
        //webIsPaymentApps 
        $("#credit33").click(function () {
            $("#webIsPaymentApps").show();
        });
        $("#credit34").click(function () {
            $('input[name="WhichPaymentApps"]').val("");
            $("#webIsPaymentApps").hide();
        });




    });


});

$(document).ready(function () {
    $('#contractor-list').DataTable({
        "scrollX": true,
        "scrollY": "100%",
        "scrollCollapse": true,
        "responsive": true,
    });

    
   

    $('.select_continent').click(function ()
    {
        var data = $(this).data('value');
        
        $.ajax({
            type: "POST",
            url: urlprefix+ "/Dashboard/ContractorDetails",
            data: {
                edit: data
            },

        });
       
    });


    //$('.green1').click(function () {

    //    
    //    var dataq = $(this).attr('id');
    //    var userid = $(this).attr('data-userId');
    //    var dataq21 = $(this).attr('data-type');
    //    var Isliked = false;
    //    if (dataq21=="1") {
    //        Isliked = true;
    //    }


    //    var model = {};
       
    //    model.UserId = $(this).attr('data-userId');
    //    model.ContractorId = $(this).attr('id');
    //    model.IsLiked = Isliked;
    //    model.Comments = "";
       

    //    $.ajax({
    //        //type: "POST",
    //        url: urlprefix + "/Dashboard/ContractorsReview",
    //        type: "POST",
    //       // async: true,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        data: JSON.stringify(model),

    //    });

    //});
    $('.red1').click(function () {
        var Isliked = false;
        if (dataq21 == "1") {
            Isliked = true;
        }



        var model = {};

        model.UserId = $(this).attr('data-userId');
        model.ContractorId = $(this).attr('id');
        model.IsLiked = Isliked;
        model.Comments = "";


        $.ajax({
            type: "POST",
            url: urlprefix + "/Dashboard/ContractorsReview",
            type: "POST",
            // async: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(model),

        });

    });

    
   
    
});

$(".toggle-").click(function () {
    $(this).toggleClass("fa-eye fa-eye-slash");
    input = $(this).parent().find("input");
    if (input.attr("type") == "password") {
        input.attr("type", "text");
        input.attr()
        $('#password').addClass($(this).attr(''))
    }
    else
    {
        input.attr("type", "password");
    }
});




//loader functionality
$body = $("body");
$(document).on({
    ajaxStart: function () {
        $body.addClass("loading");
    },
    ajaxStop: function () { $body.removeClass("loading"); }
});

document.onreadystatechange = function () {
    if (document.readyState !== "complete") {

        document.querySelector(".logo-loader").style.visibility = "visible";
    } else {

        document.querySelector("body").style.visibility = "visible";
        document.querySelector(".logo-loader").style.display = "none";
    }
};

$(document).ready(function () {
    $('input[type="number"]').on('keypress', function (e) {
        var maxlength = $(this).prop('maxlength');
        if (maxlength !== -1) {  // Prevent execute statement for non-set maxlength prop inputs
            var length = $(this).val().trim().length;
            if (length + 1 > maxlength) e.preventDefault();
        }
    });

    //onclick = "document.getElementById('myInput').value = ''
    $("#example").on("click", ".btnnew", function () {
        var t = $(this).attr('id');
        $.ajax({
            url: urlprefix + "/Dashboard/GetContractorDetails",
            type: "GET",
            dataType: "json",
            data: { edit: t },
            success: function (data) {
                if (data === 1) {
                    window.location = urlprefix + "/Dashboard/contractorsdetails";
                }


            }
        })
    });
    ///This click events likes a contractor based on logged in user
    $("#example").on("click", ".green1", function () {
        
        var t = $(this).attr('id');
        /*alert(t);*/
        //alert(t);prop("classList")
        if ($(this).hasClass('greenClr')) {
          
            
            $(this).removeClass("greenClr").addClass("grey");
            var dataq21 = $(this).attr('data-type');
            var Isliked = false;
            if (dataq21 == "1")
            {
                Isliked = true;
            }


            var model = {};

            model.UserId = $(this).attr('data-userId');
            model.ContractorId = $(this).attr('id');
            model.IsLiked = false;
            model.Comments = "";


            $.ajax({
                //type: "POST",
                url: urlprefix + "/Dashboard/ContractorsReview",
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(model),

            });
          
        }
        else {
           
            $(this).removeClass("grey").addClass("greenClr");
            $(this).next('button').removeClass('redClr');
            var model = {};

            model.UserId = $(this).attr('data-userId');
            model.ContractorId = $(this).attr('id');
            model.IsLiked = true;
            model.Comments = "";


            $.ajax({
                //type: "POST",
                url: urlprefix + "/Dashboard/ContractorsReview",
                type: "POST",
                async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(model),

            });
           
        }
        
    });

    ///This click events dislikes a contractor based on logged in user
    $("#example").on("click", ".red1", function () {

        var t = $(this).attr('id');
        
        if ($(this).hasClass('redClr')) {
         
            $(this).removeClass("redClr").addClass("grey");
            var dataq21 = $(this).attr('data-type');
            var Isliked = false;
            if (dataq21 == "1") {
                Isliked = true;
            }


            var model = {};

            model.UserId = $(this).attr('data-userId');
            model.ContractorId = $(this).attr('id');
            model.IsDisLiked = false;
            model.Comments = "";


            $.ajax({
                //type: "POST",
                url: urlprefix + "/Dashboard/ContractorsReview",
                type: "POST",
                // async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(model),

            });
            
        }
        else {
          
            $(this).removeClass("grey").addClass("redClr");
           
            $(this).prev('button').removeClass('greenClr');

            var dataq21 = $(this).attr('data-type');
         


            var model = {};

            model.UserId = $(this).attr('data-userId');
            model.ContractorId = $(this).attr('id');
            model.IsDisLiked = true;
            model.Comments = "";


            $.ajax({
                //type: "POST",
                url: urlprefix + "/Dashboard/ContractorsReview",
                type: "POST",
                // async: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(model),

            });
           
        }

    });
 

    $('.inputzip').keypress(function (e) {

        var charCode = (e.which) ? e.which : event.keyCode

        if (String.fromCharCode(charCode).match(/[^0-9]/g))

            return false;

    });

    $(function () {

        $("form[name='searchboxform']").validate({
           
            rules: {
                ZipCode: {
                    required: true,
                    minlength: 5,
                    maxlength: 5,
                },
                Categories: "required"



            },
            // Specify validation error messages
            messages: {
                Categories: localizer("Please enter category.", "Por favor, introduzca la categoría", $('#ProhzLangDDL option:selected').val()),


                ZipCode: localizer("Please enter zip code.", "Por favor, introduzca el código postal", $('#ProhzLangDDL option:selected').val())
            },

            submitHandler: function (form) {
                form.submit();
            }
        });
    });
    $('#pro').change(function () {
        $("#ProfilePicture-error").html("");
        
        var ext = $('#pro').val().split('.').pop().toLowerCase();
        var file_size = $('#pro')[0].files[0].size;
    if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'psd']) == -1) {
        $("#ProfilePicture-error").empty();
        $("#ProfilePicture-error").text(localizer('invalid file format.Please upload image in JPG,JPEG,PNG or PSD', 'formato de archivo no válido. Cargue la imagen en JPG, JPEG, PNG o PSD', $('#ProhzLangDDL option:selected').val()));

        this.value = '';
    }
    else if (file_size > 5242880) {
        $("#ProfilePicture-error").empty();
        $("#ProfilePicture-error").text(localizer('file size is too large.Pleasae upload image under 5MB', 'el tamaño del archivo es demasiado grande. Cargue la imagen por debajo de 5 MB', $('#ProhzLangDDL option:selected').val()));

                this.value = '';
                return false;


        }
        $('input[type="number"]').on('keypress', function (e) {
            var maxlength = $(this).prop('maxlength');
            if (maxlength !== -1) {  // Prevent execute statement for non-set maxlength prop inputs
                var length = $(this).val().trim().length;
                if (length + 1 > maxlength) e.preventDefault();
            }
        });
        $(document).on("keydown", 'input[type="text"]', function (evt) {
            var caretPos = $(this)[0].selectionStart
            if (evt.keyCode == 32 && caretPos == 0) {
                return false;
            }
        });

        $(document).on("input", ".numeric", function () {
            this.value =Number( this.value.replace(/\D/g, ''));


            if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);
        });

        // Validate SEARCH CONTRACTOR form fields in DASHBAORD form 
       
       
});

    $("#autocomplete-address").autocomplete({
        
        source: function (request, response) {
            
            var zipIsNULL = $("#autocomplete-zipcode").val();
            if (jQuery.trim(zipIsNULL).length == 0) {
                $("#autocomplete-address").val("");
                var InfoIcon = '<img style="height: 22px;padding:2px;margin-bottom:2px;margin-right:2px" src="' + urlprefix + '/images/circle-info-solid.svg">';
               // alert(InfoIcon);
                Swal.fire({
                    title: 'Please select zip code first.',
                    iconHtml: ProhzGreyLogo,
                    html: InfoIcon + localizer('We have to have the zip code first because every zip code has a different list of contractors.', 'Tenemos que tener el código postal primero porque cada código postal tiene una lista diferente de contratistas', $('#ProhzLangDDL option:selected').val())

                })
                return false;
            }
           
            $.ajax({
                url: urlprefix+ "/Dashboard/CategoriesList",
                type: "POST",
                dataType: "json",
                data: {
                    __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val(),
                    Prefix: request.term,
                    zipcode: zipIsNULL
                },
                success: function (data) {

                    
                    //var result = JSON.parse(data);
                    if (data[0] == undefined) {
                        var InfoIcon = '<img style="height: 22px;padding:2px;margin-bottom:2px;margin-right:2px" src="' + urlprefix + '/images/circle-info-solid.svg">';
                            
                        Swal.fire({
                            title: 'Sorry.',
                            iconHtml: ProhzGreyLogo,
                            html: InfoIcon + localizer(' Right now this service is not available on the zip code ', 'En este momento este servicio no está disponible en el código postal', $('#ProhzLangDDL option:selected').val()) + zipIsNULL,

                        })
                           
                        
                     
                    }
                    else {
                        response($.map(data, function (item) {
                            // console.log(data[i].Name);

                            //console.log(item.id + item.name);
                            return { label: item.name, value: item.name };
                        }))
                    }
                   

                }
            })
        },
       
    });


   

    $("#autocomplete-zipcode").autocomplete({
       
        source: function (request, response) {
            $("#autocomplete-address").val("");
            var userID = $("#CurrentLoggedInUserId").val();
            $.ajax({
                url: urlprefix+ "/Dashboard/ZipcodeList",
                type: "POST",
                dataType: "json",
                data: {
                    __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val(),
                    Prefix: request.term,
                    UserID: userID
                },
                success: function (data) {
                    
                   // console.log(data[i].Zipcode);
                   // console.log(data);

                    if (data.length > 0) {
                        response($.map(data, function (item) {
                            // 
                            // console.log(item.id + item.zipcode);

                            //if (item==null) {
                            //    this.alert("No contractors registred on this zip code");
                            //}
                            return { label: item.zipcode, value: item.zipcode };
                        }));
                    }
                    else {
                       // $("#LBLautocomplete-zipcode").val("");
                       // Swal.fire('', '', 'info');
                        $("#LBLautocomplete-zipcode").html(localizer("You have't selected or buy above zip code.", "No ha seleccionado ni comprado por encima del código postal", $('#ProhzLangDDL option:selected').val()));
                        $("#LBLautocomplete-zipcode").css('display', 'inline-block');

                       
                        return { label: 0, value: 0 };
                    }
                    

                }
            })
        },

    });

   
})
//on window resize
$(function () {
    $(window).bind("resize", function () {
        if ($(this).width() <= 768) {
            $('#searchboxform').removeClass('d-flex').addClass('red')
        }
        else {
            $('#searchboxform').addClass('d-flex')
        }
    }).resize();

    //$(window).bind("resize", function () {
    //    if ($(this).width() <= 1200) {
            
    //        $('#searchbutton').css({ 'display': 'None' });
    //    }
    //    else {
    //        $('#searchbutton').css({ 'display': 'None' });
    //    }
    //}).resize();


});

var loadFile = function (event) {
   
    var output = document.getElementById('blah');
    output.src = URL.createObjectURL(event.target.files[0]);
    output.onload = function () {
        URL.revokeObjectURL(output.src) // free memory
    }
};


function ChangeWorkStatus(form) {

    var selectedVal = "";
    var selected = $("input[type='radio'][name='radioWorkStatus']:checked");
    if (selected.length > 0) {
        selectedVal = selected.val();
    }
    $.ajax({
        type: 'POST',
        url: urlprefix + '/user/ChangeStatus',
        data: $(form).serialize(),
        success: function (res) {
            if (res == 1) {
                window.location.reload();
            }
        }
    });
    return false;
}


function ChangePassword(form) {
    $('#spnMessage').text('');
  
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/user/ChangePassword',
            data: $(form).serialize(),
            success: function (res) {

                const result = JSON.parse(res);
                if (result["statuscode"] == "200") {
                    $('#ChangePassword')[0].reset();
                    $("#subcontactorModal").modal('show');
                    $('#subcontactorModal').modal({
                        backdrop: 'static',
                        keyboard: false  // to prevent closing with Esc button (if you want this too)
                    });
                    
                   

                }
                else if (result["statuscode"] == "208") {

                    $('#spnMessage').css('color', 'red');
                    $('#spnMessage').text(result["message"]);

                }
                else if (result["statuscode"] == "400") {

                    $('#spnMessage2').css('color', 'red');
                    $('#spnMessage2').text(result["message"]);

                }
                else {
                    $('#spnMessage').text(result["message"]);
                    alert(result["message"]);
                }

            }
        });
    }



    return false;
}

function checkPhoneNumberIsNotNull() {
    var IsNullNumber = false;
    
    var PhoneNumberOffice = $.trim($('#PhoneNumberOfficeEdit').val());
    var PhoneNumber = $.trim($('#PhoneNumberEdit').val());
    //$('#PhoneNumberEdit').removeAttr('data-val-required');
    //$('#PhoneNumberOfficeEdit').removeAttr('data-val-required');
    // Check if empty or not
    if (PhoneNumber === '' && PhoneNumberOffice === '') {

        $('span[data-valmsg-for="PhoneNumberOffice"]').text(localizer('Please enter either  phone number or office number.', 'Ingrese el número de teléfono o el número de la oficina.', $('#ProhzLangDDL option:selected').val()));
        /* $('span[data-valmsg-for="PhoneNumber"]').html('Please enter either phone/office number.');*/

        $('#PhoneNumberval').text(localizer('Please enter either phone number or office number.', 'Ingrese el número de teléfono o el número de la oficina', $('#ProhzLangDDL option:selected').val()));
    }
    else {

        $('#PhoneNumberval').text('');
        $('span[data-valmsg-for="PhoneNumber"]').text('');
        $('span[data-valmsg-for="PhoneNumberOffice"]').text('');
        IsNullNumber = true;
    }

    return IsNullNumber;
}

$("#PhoneNumberOfficeEdit").keypress(function () {
    $('#PhoneNumberval').text('');
});
$("#PhoneNumberEdit").keypress(function () {
    $('#PhoneNumberval').text('');
});




function UpdateUserBasicInfo(form) {
   // console.log("entered function");
    //var fileUpload = $("#pro").get(0);

    //var name = fileUpload.files[0]?.name;
    //var file = $('#pro').prop('files');
    
    //if (name!=null) {
    //    $('input[name=ProfilePicture]').val(name);
    //}
    //else {
    //    $('input[name=ProfilePicture]').val("DefaultProfile.png");
    //}
   
    var a = $(form).serialize();
    console.log(a);
    
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/Dashboard/UpdateUserBasicInfo',
            data: $(form).serialize(),
            success: function (res) {
                console.log("success");
                const result = JSON.parse(res);
               
                if (result["statuscode"] == "200") {
                    window.location = urlprefix + "/Dashboard/UserProfileDetails";
                    //alert(result["data"]["emailHo"]);
                    //$("#Email1").val(result["data"]["emailHo"]);
                    console.log("passed!");

                }
                else if (result["statuscode"] == "208") {

                    console.log("208");

                }
                else {
                    console.log(result["message"]);
                }
            }
        });
    }
    return false;
}



function UpdateContractorBusinessProfile(form)
{
    

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/Dashboard/UpdateContractorBusinessProfile',
            data: $(form).serialize(),
            success: function (res) {
               // console.log("success");
                const result = JSON.parse(res);

                if (result["statuscode"] == "200") {
                    window.location = urlprefix + "/Dashboard/businessprofiledetail";
                    //alert(result["data"]["emailHo"]);
                    //$("#Email1").val(result["data"]["emailHo"]);
                    console.log("passed!");

                }
                else if (result["statuscode"] == "208") {

                    console.log("208");

                }
                else {
                    console.log(result["message"]);
                }
            }
        });
    }
    return false;

}

function UpdateOtherContractorBusinessProfile(form) {


    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/Dashboard/UpdateOtherContractorBusinessProfile',
            data: $(form).serialize(),
            success: function (res) {
                console.log("success");
                const result = JSON.parse(res);

                if (result["statuscode"] == "200") {
                    window.location = urlprefix + "/Dashboard/businessprofiledetail";
                    //alert(result["data"]["emailHo"]);
                    //$("#Email1").val(result["data"]["emailHo"]);
                    console.log("passed!");

                }
                else if (result["statuscode"] == "208") {

                    console.log("208");

                }
                else {
                    console.log(result["message"]);
                }
            }
        });
    }
    return false;

}

$("#creditchk").change(function () {
    if ($(this).is(":checked")) {
        $("#creditchk").val('1');
    } else if ($(this).not(":checked")) {
        $("#creditchk").val('0');
    }
});
$("#Visachk").change(function () {
    if ($(this).is(":checked")) {
        $("#Visachk").val('1');
    } else if ($(this).not(":checked")) {
        $("#Visachk").val('0');
    }
});
$("#AmExchk").change(function () {
    if ($(this).is(":checked")) {
        $("#AmExchk").val('1');
    } else if ($(this).not(":checked")) {
        $("#AmExchk").val('0');
    }
});

$('input[name="ProfilePicture"]').change(function () {
    $('span[data-valmsg-for="ProfilePicture"]').text('');

    var ext = $('input[name="ProfilePicture"]').val().split('.').pop().toLowerCase();
    var file_size = $('input[name="ProfilePicture"]').get(0).files[0].size;
    if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'pdf', 'psd']) == -1) {
        $("#ProfilePicture-error").empty();
        $('span[data-valmsg-for="ProfilePicture"]').text(localizer('invalid file format.Please upload image in JPG,JPEG,PNG or PSD', 'formato de archivo no válido. Cargue la imagen en JPG, JPEG, PNG o PSD', $('#ProhzLangDDL option:selected').val()));

        this.value = '';
    }
    else if (file_size > 5242880) {
        $("#ProfilePicture-error").empty();
        $('span[data-valmsg-for="ProfilePicture"]').text(localizer('file size is too large.Pleasae upload image under 5MB', 'el tamaño del archivo es demasiado grande. Cargue la imagen por debajo de 5 MB', $('#ProhzLangDDL option:selected').val()));
            this.value = '';
            return false;
    }


});

$('input[name="LiabilityInsuranceFile"]').change(function () {
    $('span[data-valmsg-for="LiabilityInsuranceFile"]').text('');
    var ext = $('input[name="LiabilityInsuranceFile"]').val().split('.').pop().toLowerCase();

    if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'pdf', 'txt', 'xlsx', 'xls', 'docx', 'doc']) == -1) {
        $('span[data-valmsg-for="LiabilityInsuranceFile"]').text(localizer('invalid file format!', '¡Formato de archivo inválido!', $('#ProhzLangDDL option:selected').val()));

        this.value = '';
    }


});
$('input[name="WorkmanCompensationInsuranceFile"]').change(function () {
    $('span[data-valmsg-for="WorkmanCompensationInsuranceFile"]').text('');
    var ext = $('input[name="WorkmanCompensationInsuranceFile"]').val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['png', 'jpg', 'jpeg', 'pdf', 'txt', 'xlsx', 'xls', 'docx', 'doc']) == -1) {
        $('span[data-valmsg-for="WorkmanCompensationInsuranceFile"]').text(localizer('invalid file format!', '¡Formato de archivo inválido!', $('#ProhzLangDDL option:selected').val()));

        this.value = '';
    }


});
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
                    $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text(localizer('invalid file format!', '¡Formato de archivo inválido!', $('#ProhzLangDDL option:selected').val()));
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
                $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text(localizer('File size is too big.File should be under 5 MB', 'El tamaño del archivo es demasiado grande. El archivo debe tener menos de 5 MB', $('#ProhzLangDDL option:selected').val()) );

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
                    $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text(localizer('invalid file format!', '¡Formato de archivo inválido!', $('#ProhzLangDDL option:selected').val()));
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
                $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text(localizer('File size is too big.File should be under 5 MB', 'El tamaño del archivo es demasiado grande. El archivo debe tener menos de 5 MB', $('#ProhzLangDDL option:selected').val()) );

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
                    $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text(localizer('invalid file format!', '¡Formato de archivo inválido!', $('#ProhzLangDDL option:selected').val()));
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
                $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text(localizer('File size is too big.File should be under 5 MB', 'El tamaño del archivo es demasiado grande. El archivo debe tener menos de 5 MB', $('#ProhzLangDDL option:selected').val()) );

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
                    $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text(localizer('invalid file format!', '¡Formato de archivo inválido!', $('#ProhzLangDDL option:selected').val()));
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
                $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text(localizer('File size is too big.File should be under 5 MB', 'El tamaño del archivo es demasiado grande. El archivo debe tener menos de 5 MB', $('#ProhzLangDDL option:selected').val()) );

                this.value = '';
                return false;
            }
        }
        else {
            console.log("no files selectes");
        }

    });



})
$(".toggle-favourite-green").click(function () {
    $(this).toggleClass("favourite-green");
    //input = $(this).parent().find("input");
    //if (input.attr("type") == "password") {
    //    input.attr("type", "text");
    //} else {
    //    input.attr("type", "password");
    //}
});
$(".toggle-favourite-red").click(function () {
    $(this).toggleClass("favourite-red");
    //input = $(this).parent().find("input");
    //if (input.attr("type") == "password") {
    //    input.attr("type", "text");
    //} else {
    //    input.attr("type", "password");
    //}
});
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






