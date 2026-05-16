$(document).ajaxSend(function (event, jqXHR, ajaxOptions) {
    if (ajaxOptions.type === 'POST') {
        jqXHR.setRequestHeader('__RequestVerificationToken', $('input[name="__RequestVerificationToken"]').val());
    }
});

$("[name='BusinessOrTradeLicenseFiles']").on("change", function () {
    if ($("[name='BusinessOrTradeLicenseFiles']")[0].files.length > 4) {
        $("[name='BusinessOrTradeLicenseFiles']").val('');
        $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('');
        $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text(localizer('You can select only 4 Files', 'Puede seleccionar solo 4 archivos', $('#ProhzLangDDL option:selected').val()));
    } else {
        $('span[data-valmsg-for="BusinessOrTradeLicenseFiles"]').text('');
    }
});

var urlprefix = '';
if (window.location.href.indexOf("KopkeHome-FMRS-QA") > -1) {
    urlprefix = '/KopkeHome-FMRS-QA';
}



var ProhzGreyLogo = '<img style="height: 90px;" src="' + urlprefix + '/images/ProhzGreyLogo.new.png">';

$.validator.addMethod("regx", function (value, element, regexpr) {
    return regexpr.test(value);
}, localizer("Please enter a valid pasword.", "Por favor, introduzca una contraseña válida", $('#ProhzLangDDL option:selected').val()));


$(function () {
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("#homeownerform").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side

            FirstNameHo: "required",
            LastNameHo: "required",
            EmailHo: {
                required: true,
                // Specify that email should be validated
                // by the built-in "email" rule
                //email: true,
                regx: /^[a-zA-Z0-9_\.-]+@([a-zA-Z0-9-]+\.)+[a-zA-Z]{2,6}$/
            },
            PasswordHo: {
                required: true,
                minlength: 8,
                maxlength: 16,
            },
            ConfirmPasswordHo: {
                required: true,

                equalTo: '[name="PasswordHo"]'
            },
            PhoneNumberOfficeHo: {

                minlength: 12,
                maxlength: 12,
            },
            PhoneNumberHo: {
                required: true,
                minlength: 12,
                maxlength: 12,
            },
            StateHo: {
                required: true,
            },
            ZipCodeHo: {
                required: true,
                minlength: 5,
                maxlength: 5,
            },
            HeardAboutProhzFromHo:
            {
                required: true,
            },
        },
        // Specify validation error messages
        messages: {
            FirstNameHo: localizer("Please enter your first name", "Por favor, introduzca su nombre de pila", $('#ProhzLangDDL option:selected').val()),
            LastNameHo: localizer("Please enter your last name", "Por favor ingrese su apellido", $('#ProhzLangDDL option:selected').val()),
            PasswordHo: {
                required: localizer("Password is required", "Se requiere contraseña", $('#ProhzLangDDL option:selected').val()),
                minlength: localizer('Password must be between 8 to 16 characters', 'La contraseña debe tener entre 8 y 16 caracteres', $('#ProhzLangDDL option:selected').val()),
                maxlength: localizer('Password must be between 8 to 16 characters', 'La contraseña debe tener entre 8 y 16 caracteres', $('#ProhzLangDDL option:selected').val())
            },
            ConfirmPasswordHo: {
                required: localizer("Confirm password is required", "Se requiere confirmar contraseña", $('#ProhzLangDDL option:selected').val()),
                equalTo: localizer("Password and Confirm password do not match.", "Contraseña y Confirmar contraseña no coinciden", $('#ProhzLangDDL option:selected').val())
            },
            EmailHo: localizer("Please enter a valid email address", "Por favor, introduce una dirección de correo electrónico válida", $('#ProhzLangDDL option:selected').val()),
            //PhoneNumberOfficeHo: {

            //    minlength: localizer("Please enter a valid phone number.", "Por favor ingrese un número de teléfono válido.", $('#ProhzLangDDL option:selected').val()),
            //    maxlength: localizer("Please enter a valid phone number.", "Por favor ingrese un número de teléfono válido.", $('#ProhzLangDDL option:selected').val())
            //},
            //PhoneNumberHo: {
            //    required: localizer("PhoneNumber is required.", "El número de teléfono es obligatorio", $('#ProhzLangDDL option:selected').val()),
            //    minlength: localizer("Please enter a valid phone number.", "Por favor ingrese un número de teléfono válido.", $('#ProhzLangDDL option:selected').val()),
            //    maxlength: localizer("Please enter a valid phone number.", "Por favor ingrese un número de teléfono válido.", $('#ProhzLangDDL option:selected').val())
            //},
            ZipCodeHo: {
                required: localizer("Zip code is required.", "El código postal es obligatorio", $('#ProhzLangDDL option:selected').val()),
                minlength: localizer("Please enter a valid zip code.", "Por favor ingrese un código postal válido.", $('#ProhzLangDDL option:selected').val()),
                maxlength: localizer("Please enter a valid zip code.", "Por favor ingrese un código postal válido.", $('#ProhzLangDDL option:selected').val()),
            },
            StateHo: {
                required: localizer("State is required.", "Se requiere estado", $('#ProhzLangDDL option:selected').val()),

            },
            HeardAboutProhzFromHo: localizer("Please mention how did you hear about prohz", "Por favor, mencione cómo se enteró de prohz", $('#ProhzLangDDL option:selected').val()),
        },

    });
});


$(document).ready(function () {
    function disableBack() {
        window.history.forward()
    }
    window.onload = disableBack();

    $('#contractorbasic').on('change', '#TermsAndConditionschk', function () {

        this.checked ? this.value = '1' : this.value = '0';
    });

    $(".chkTermAndCon").change(function () {
        if (this.checked) {


            $('#btn_submit').removeAttr('disabled');

        } else {

            $('#btn_submit').attr('disabled', 'disabled');
        }
    });
    //terms and conditions home owner
    $(".chkTermAndConHw").change(function () {
        if (this.checked) {

            $('#btn_submitHw').removeAttr('disabled');


        } else {

            $('#btn_submitHw').attr('disabled', 'disabled');

        }
    });
    //zipcode remove lable error text 





    var ProhzGreyLogo = '<img style="height: 90px;" src="' + urlprefix + '/images/ProhzGreyLogo.new.png">';

    $("Input#ZipCodeHo").on("keyup", function (event) {
        
        if (this.value.length == this.getAttribute('maxlength')) {
            if ($(this).valid()) {


                $.ajax({
                    type: "Post",
                    url: urlprefix + "/User/CheckContractorsOnZipcode",
                    data: {
                        Zipcode: $("Input#ZipCodeHo").val()
                    },
                    success: function (res) {

                        if (res == false) {
                            // $('#btn_submitHw').remove();
                            //Swal.fire('', 'Thank you for trying to join Prohz, but your zip code has not yet been released. To increase your options and satisfaction, we will only release a zip code after several local contractors have registered. Please check back regularly to see if your zip code is open, and thank you for your patience.', 'info')

                            //var youtubeimgsrc = document.getElementById("Logoimgurl").src;

                            Swal.fire({
                                title: '',
                                text: 'Thank you for trying to join Prohz, but your zip code has not yet been released. To increase your options and satisfaction, we will only release a zip code after several local contractors have registered. Please check back regularly to see if your zip code is open, and thank you for your patience.',
                                iconHtml: ProhzGreyLogo,

                            })
                        }



                    }
                });

            }
        }
    });


    $("#HeardAboutUsContrcactor").change(function (e) {
        e.stopImmediatePropagation();
        if ($(this).val() == 2) {
            
            $('.ClosedSalesAsst').css("display", "none");
            $('input[name="SalesAssociate"]').val('');
        }
        else if ($(this).val() == 14) {
            $('.ClosedSalesAsst').css("display", "block");
            
        
        }
        else {
            $('.ClosedSalesAsst').css("display", "none");
        
            $('input[name="SalesAssociate"]').val('');
        }
        //console.log($(this).val());
    });

    $("#HeardAboutUsHO").change(function (e) {
        e.stopImmediatePropagation();
        if ($(this).val() == 2) {
            $('.ClosedMemberRefHo').css("display", "block");

            $('.ClosedSalesAsstHo').css("display", "none");
            $('input[name="SalesAssociateHo"]').val('');
        }
        else if ($(this).val() == 14) {
            $('.ClosedSalesAsstHo').css("display", "block");
            $('.ClosedMemberRefHo').css("display", "none");
            $('input[name="MemberReferralIdHo"]').val('');
        }
        else {
            $('.ClosedSalesAsstHo').css("display", "none");
            $('.ClosedMemberRefHo').css("display", "none");
            $('input[name="MemberReferralIdHo"]').val('');
            $('input[name="SalesAssociateHo"]').val('');

        }
    });
});


$.validator.addMethod('filesize', function (value, element, param) {
    return this.optional(element) || (element.files[0].size <= param * 1000000)
}, localizer('File size must be less than {0}', 'El tamaño del archivo debe ser inferior a {0}', $('#ProhzLangDDL option:selected').val()));

$(function () {
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("form[name='ContractorBusinessProfile']").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            BusinessDescription: "required",
            YearsInBusiness: "required",

            IsCompanyWebsite: { required: true },
            IsCompanyWebsite1: {
                required: function (element) {
                    return $('input[name="IsCompanyWebsite"]:checked').val() == 'true';
                }
            },
            IsFacebookPage: { required: true },
            IsFacebookPage1: {
                required: function (element) {
                    return $('input[name="IsFacebookPage"]:checked').val() == 'true';
                }
            },
            CommercialLocation: { required: true },
            CommercialLocation1: {
                required: function (element) {
                    return $('input[name="CommercialLocation"]:checked').val() == 'true';
                }
            },


            NumberOfEmployees: "required",
            JobSiteCrews: "required",

            IsPhoneCallSupport: { required: true },
            IsPhoneCallSupport1: {
                required: function (element) {
                    return $('input[name="IsPhoneCallSupport"]:checked').val() == 'true';
                }
            },
            NormalBusinessHours: "required",
            Is24HoursPhoneAnswering: { required: true },
            Is24HoursPhoneAnswering1: {
                required: function (element) {
                    return $('input[name="Is24HoursPhoneAnswering"]:checked').val() == 'true';
                }
            },
            IsOfferEmergencyServices: { required: true },
            IsOfferEmergencyServices1: {
                required: function (element) {
                    return $('input[name="IsOfferEmergencyServices"]:checked').val() == 'true';
                }
            },

            IsBusinessOrTradeLicense: { required: true },



            IsLiabilityInsurance: { required: true },

            IsWorkmanCompensationInsurance: { required: true },


            IsEstimateCharge: { required: true },
            IsEstimateCharge1: {
                required: function (element) {
                    return $('input[name="IsEstimateCharge"]:checked').val() == 'true';
                }
            },


            IsDesignServices: { required: true },
            IsDesignServices1: {
                required: function (element) {
                    return $('input[name="IsDesignServices"]:checked').val() == 'true';
                }
            },





            IsContactedByHomeowners: { required: true },
            IsContactedByHomeowners1: {
                required: function (element) {
                    return $('input[name="IsContactedByHomeowners"]:checked').val() == 'true';
                }
            },
            IsContactedBySubcontractors: { required: true },
            IsContactedBySubcontractors1: {
                required: function (element) {
                    return $('input[name="IsContactedBySubcontractors"]:checked').val() == 'true';
                }
            },




            IsCash: { required: true },
            IsCash1: {
                required: function (element) {
                    return $('input[name="IsCash"]:checked').val() == 'true';
                }
            },

            PersonalChecks: { required: true },
            PersonalChecks1: {
                required: function (element) {
                    return $('input[name="PersonalChecks"]:checked').val() == 'true';
                }
            },
            IsPaymentApps: { required: true },
            IsPaymentApps1: {
                required: function (element) {
                    return $('input[name="IsPaymentApps"]:checked').val() == 'true';
                }
            },

            WhichPaymentApps: "required",

            ProfilePicture: {
                required: true,
                extension: "jpg,jpeg,png,psd",
                filesize: 5,
            },

        },
        // Specify validation error messages
        messages: {
            BusinessDescription: localizer("Please enter your Business Description....", "Ingrese la descripción de su empresa..", $('#ProhzLangDDL option:selected').val()),
            YearsInBusiness: localizer("Please enter your Years In Business", "Ingrese sus años en el negocio", $('#ProhzLangDDL option:selected').val()),

            IsCompanyWebsite: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsFacebookPage: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            CommercialLocation: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            CompanyWebsiteURL: localizer("Please enter your Company Website URL", "Ingrese la URL del sitio web de su empresa", $('#ProhzLangDDL option:selected').val()),
            FacebookPageURL: localizer("Please enter your Facebook Page URL", "Por favor ingrese la URL de su página de Facebook", $('#ProhzLangDDL option:selected').val()),

            NumberOfEmployees: localizer("Please enter number of Employees", "Por favor, introduzca el número de empleados", $('#ProhzLangDDL option:selected').val()),
            JobSiteCrews: localizer("Please enter job site crews", "Ingrese las cuadrillas del sitio de trabajo", $('#ProhzLangDDL option:selected').val()),

            IsPhoneCallSupport: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            NormalBusinessHours: localizer("Please enter Normal Business Hours", "Ingrese el horario comercial normal", $('#ProhzLangDDL option:selected').val()),

            Is24HoursPhoneAnswering: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsOfferEmergencyServices: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),

            IsEstimateCharge: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsDesignServices: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsContactedByHomeowners: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsContactedBySubcontractors: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsCash: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            PersonalChecks: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsPaymentApps: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),

            WhichPaymentApps: localizer("Please provide app name", "Proporcione el nombre de la aplicación", $('#ProhzLangDDL option:selected').val()),

            ProfilePicture: {
                required: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
                extension: localizer("Invalid file format. Please upload the image in JPG,  JPEG, PNG, or PSD", "Formato de archivo no válido. Cargue la imagen en JPG, JPEG, PNG o PSD", $('#ProhzLangDDL option:selected').val()),
                filesize: localizer("The file is too big. Please upload file under 5MB", "El archivo es demasiado grande. Cargue un archivo de menos de 5 MB", $('#ProhzLangDDL option:selected').val())
            },
        },


    });
});

//this methode checks email is valid or not
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}


function ForgotPasswordLinkSend() {

    if ($.trim($('#EmailForgotPassword').val()) != '') {
        if (isEmail($("#EmailForgotPassword").val())) {


            $.ajax({
                type: "GET",
                url: urlprefix + "/User/GenerateLinkForgotPassword",
                data: {
                    Email: $("#EmailForgotPassword").val()
                },
                success: function (res) {
                    const result = JSON.parse(res);
                    if (result["statuscode"] == "200") {

                        $("#PwdResetLinkModal").modal('show');
                        $('#PwdResetLinkModal').modal({
                            backdrop: 'static',
                            keyboard: false  // to prevent closing with Esc button (if you want this too)
                        });
                    }
                    else {

                        $("#EmailForgotPasswordlbl").html(result["message"]);
                    }

                }
            });
        }
        else {
            $("#EmailForgotPasswordlbl").html(localizer("Invalid Email address.", "Dirección de correo electrónico no válida", $('#ProhzLangDDL option:selected').val()));
        }
    }
    else {
        $("#EmailForgotPasswordlbl").html(localizer("Please enter registered email.", "Por favor, introduzca el correo electrónico registrado", $('#ProhzLangDDL option:selected').val()));
    }
    return false;
}
function CreateNewPassword(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/user/ChangeforgotpasswordOfUser',
            data: $(form).serialize(),
            success: function (res) {
                const result = JSON.parse(res);
                if (result["statuscode"] == "200") {

                    $("#CreateNewPasswordModal").modal('show');
                    $('#CreateNewPasswordModal').modal({
                        backdrop: 'static',
                        keyboard: false  // to prevent closing with Esc button (if you want this too)
                    });
                }
                else {

                    $('span[data-valmsg-for="Password"]').text(localizer('Token Expired. Please try again.', 'Token caducado. Inténtalo de nuevo.', $('#ProhzLangDDL option:selected').val()));
                }
            }
        });
    }
    return false;

}


function Payment(form) {

    try {
        $.validator.unobtrusive.parse(form);
        if ($(form).valid()) {
            $.ajax({
                type: 'POST',
                url: urlprefix + '/Membership/Payment',
                data: $(form).serialize(),

                success: function (res) {


                    alert(res);
                }
            });
        }

    }
    catch (err) {
        alert(err.message);
    }
    return false;
}







function RegstrHomeOwner(form) {

    $('#spnMessage').text('');
    $('#form')[0].reset();//resetting email verification form.




    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/user/HomeOwnerSignUp',
            // url: '/User/HomeOwnerSignUp',
            data: $(form).serialize(),

            success: function (res) {

                const result = JSON.parse(res);
                if (result["statuscode"] == "200") {
                    $("#contactorModal").modal('show');
                    $('#contactorModal').modal({
                        backdrop: 'static',
                        keyboard: false  // to prevent closing with Esc button (if you want this too)
                    });
                    //alert(result["data"]["emailHo"]);
                    $("#Email1").val(result["data"]["emailHo"]);
                    $("#VerificationEmail1").html(result["data"]["emailHo"]);


                }
                else if (result["statuscode"] == "208") {

                    $('#idEmailmessageHO').css('color', 'red');
                    $('#idEmailmessageHO').text(localizer("This email is already being used.", "Este correo electrónico ya se está utilizando", $('#ProhzLangDDL option:selected').val()));

                }
                else {
                    alert(result["message"]);
                }
            }
        });
    }
    return false;
}

function clearbasicform() {
    /* $('#contractorbasic')[0].reset();*/
    $(':input', '#contractorbasic')
        .not(':button, :submit, :reset, :hidden')
        .val('')
        .prop('checked', false)
        .prop('selected', false);

}
$("#general_Contractor").on("change", function () {
    $("#RoleId").val('1');
    $("#tabs").show();
    clearbasicform();

});
$("#Sub_Contractor").on("change", function () {
    $("#RoleId").val('2');
    $("#tabs").show();
    $('#contractorbasic')[0].reset();


});
$("#ind_Contractor").on("change", function () {
    $("#RoleId").val('3');
    $("#tabs").show();
    clearbasicform();

});
$("#home_Owner").on("change", function () {
    $("#RoleId").val('4');
    clearbasicform();

    //location.reload();ContractorBusinessProfile
});

$(".not-first").keyup(function () {
    if (this.value.length == this.maxLength) {
        $(this).next('.not-first').focus();
    }
});
$('input').on('input propertychange', function () {
    if (this.value.length >= this.maxLength) {
        $(this).next().focus();
    }
}).keyup(function (e) {
    if (e.which === 8 && !this.value) {
        $(this).prev().focus();
    }
});



//chechk box value for creditcard type on bussiness profile page for contractor

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
$("#OtherCreditCardchk").change(function () {
    if ($(this).is(":checked")) {
        $("#OtherCreditCardchk").val('1');
    } else if ($(this).not(":checked")) {
        $("#OtherCreditCardchk").val('0');
    }
});



$("#ddlStates").click(function () {
    $('span[data-valmsg-for="State"]').text('');
});

$("#PhoneNumber").keypress(function () {
    $('#PhoneNumberval').text('');
});
$("#PhoneNumberOffice").keypress(function () {
    $('#PhoneNumberval').text('');
});
//business profile
$("#credit1").click(function () {
    $("#web3").show();
});
$("#credit2").click(function () {
    $('input[name="CompanyWebsiteURL"]').val("");
    $("#web3").hide();
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

//credit cards
$("#credit35").click(function () {
    $("#creditcards").show();
});
$("#credit36").click(function () {
    $('input[name="Cards"]').val("");

    $('#Visachk').prop('checked', false);
    $('#Visachk').val('0');
    $('#creditchk').prop('checked', false);
    $('#creditchk').val('0');
    $('#AmExchk').prop('checked', false);
    $('#AmExchk').val('0');

    $("#creditcards").hide();
});

//$(document).ready(function () {
//    //set initial state.


//    $("input[name='IsContactedByHomeowners']").change(function () {
//        debugger
//        if (this.checked) {

//            var v22 = $(this).val();
//            if ($(this).val() == "false") {
//                $("#credit25").prop("checked", true).trigger("click");

//            }
//        } 
//    });


//    $("input[name='IsContactedBySubcontractors']").change(function () {

//        if (this.checked) {

//            var v = $(this).val();
//            if ($(this).val() == "false") {
//                $("#credit23").prop('checked', true);
//            }

//        }
//    });

//    $("input[name='IsContactedByContractors']").change(function () {

//        if (this.checked) {

//            var v = $(this).val();
//            if ($(this).val() == "false") {
//                $("#credit23").prop('checked', true);
//            }

//        }
//    });
//});

function checkPhoneNumberIsNotNull() {
    var IsNullNumber = false;

    var PhoneNumberOffice = $.trim($('#PhoneNumberOffice').val());
    var PhoneNumber = $.trim($('#PhoneNumber').val());
   
    // Check if empty or not
    if (PhoneNumber === '' && PhoneNumberOffice === '') {

        $('span[data-valmsg-for="PhoneNumberOffice"]').text('Please enter either  phone number or office number.');

        $('#PhoneNumberval').text('Please enter either phone number or office number.');
    }
    else {

        $('#PhoneNumberval').text('');
        $('span[data-valmsg-for="PhoneNumber"]').text('');
        $('span[data-valmsg-for="PhoneNumberOffice"]').text('');
        IsNullNumber = true;
    }

    return IsNullNumber;
}


function validateDropdown() {
    var IsSelected = false;
    var e = document.getElementById("ddlStates");
    var optionSelIndex = e.options[e.selectedIndex].value;
    var optionSelectedText = e.options[e.selectedIndex].text;
    if (optionSelIndex == "") {

        $('span[data-valmsg-for="State"]').text('Please select state');
        IsSelected = false;
    }
    else {
        $('span[data-valmsg-for="State"]').text('');
        IsSelected = true;
    }

    return IsSelected;
}

function ddlNext(form) {
    $('#spnMessage').text('');
    $('#form')[0].reset();//resetting email verification form.

    var a = checkPhoneNumberIsNotNull();//validating if user provided ewither contact number

    var ddl = validateDropdown();//validating dropdown slected or not

    $.validator.unobtrusive.parse(form);
    if ($(form).valid() && a == true && ddl == true) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/user/SignUp',
            data: $(form).serialize(),
            success: function (res) {
                if (res != "") {
                   
                    const result = JSON.parse(res);
                    if (result["statuscode"] == "200") {
                        $("#contactorModal").modal('show');
                        $('#contactorModal').modal({
                            backdrop: 'static',
                            keyboard: false  // to prevent closing with Esc button (if you want this too)
                        });
                        $("#Email1").val(result["data"]["email"]);
                        $("#VerificationEmail1").html(result["data"]["email"]);

                    }
                    else if (result["statuscode"] == "208") {

                        $('#idEmailmessage').css('color', 'red');
                        $('#idEmailmessage').text("This email is already being used.");

                    }
                    else {
                        alert(result["message"]);
                    }
                }
                else {
                    alert("something went wrong");
                }
            }
        });
    }



    return false;
}



function submitVerifyOtp(form) {
    /*alert("varify otp function called");*/
    $('#spnMessage').text("");
    $.validator.unobtrusive.parse(form);
    var In1 = $("#first").val();
    var In2 = $("#second").val();
    var In3 = $("#third").val();
    var In4 = $("#fourth").val();
    var In5 = $("#fifth").val();
    var In6 = $("#sixth").val();
    var OTP = (In1 + In2 + In3 + In4 + In5 + In6);
    var Email = $("#Email1").val();
    let isEmptyOTP = checkOTPValidationEmpty();
    if (!isEmptyOTP) {
        if ($(form).valid(OTP)) {
            $.ajax({
                type: 'POST',
                url: urlprefix + '/user/VerifyOTPs' + '?Verificationcode=' + OTP + "&Email=" + Email,
                data: $(form).serialize(),
                success: function (res) {
                    $('#spnMessage').text('');

                    $('#form')[0].reset();
                    if (res == "0") {
                        /* $('#spnMessage').css('color', 'red');*/

                        $('#spnMessage').text("Invalid verification code.");
                    }
                    else if (res == "1") {

                        window.onpageshow = function (e) {
                            if (e.persisted)
                                disableBack();
                        }

                        $("#succefully").modal('show');
                        $("#contactorModal").modal('hide');
                        window.location = urlprefix + "/BusinessProfile/ContractorProfile/";

                    } else if (res == "2") {
                        window.onpageshow = function (e) {
                            if (e.persisted)
                                disableBack();
                        }
                        $('#form')[0].reset();
                        $("#succefully").modal('show');
                        $("#contactorModal").modal('hide');
                        window.location = urlprefix + "/BusinessProfile/OtherContractorsProfile/";

                    }
                    else if (res == "3") {
                        window.onpageshow = function (e) {
                            if (e.persisted)
                                disableBack();
                        }
                        $('#form')[0].reset();
                        $("#succefully").modal('show');
                        $("#contactorModal").modal('hide');
                        window.location = urlprefix + "/BusinessProfile/OtherContractorsProfile/";

                    }
                    else if (res == "4") {

                        window.onpageshow = function (e) {
                            if (e.persisted)
                                disableBack();
                        }


                        $("#succefully").modal('show');
                        $("#contactorModal").modal('hide');
                        window.location = urlprefix + "/membership/HomeOwnerplan/";

                    }
                    else {

                    }
                }
            });
        }
    }

    return false;
}

function checkOTPValidationEmpty() {
    let Isvalid = false;
    var In1 = $("#first").val();
    var In2 = $("#second").val();
    var In3 = $("#third").val();
    var In4 = $("#fourth").val();
    var In5 = $("#fifth").val();
    var In6 = $("#sixth").val();

    if (In1 == null || In1 == undefined || In1 == "") {
        $('#spnMessage').text(localizer("Enter verification code", "Ingrese el código de verificación", $('#ProhzLangDDL option:selected').val()));
        Isvalid = true;
    }
    if (In2 == null || In2 == undefined || In2 == "") {
        $('#spnMessage').text(localizer("Enter verification code", "Ingrese el código de verificación", $('#ProhzLangDDL option:selected').val()));
        Isvalid = true;
    }
    if (In3 == null || In3 == undefined || In3 == "") {
        $('#spnMessage').text(localizer("Enter verification code", "Ingrese el código de verificación", $('#ProhzLangDDL option:selected').val()));
        Isvalid = true;
    }
    if (In4 == null || In4 == undefined || In4 == "") {
        $('#spnMessage').text(localizer("Enter verification code", "Ingrese el código de verificación", $('#ProhzLangDDL option:selected').val()));
        Isvalid = true;
    }
    if (In5 == null || In5 == undefined || In5 == "") {

        $('#spnMessage').text(localizer("Enter verification code", "Ingrese el código de verificación", $('#ProhzLangDDL option:selected').val()));
        Isvalid = true;
    }
    if (In6 == null || In6 == undefined || In6 == "") {

        $('#spnMessage').text(localizer("Enter verification code", "Ingrese el código de verificación", $('#ProhzLangDDL option:selected').val()));
        Isvalid = true;

    }
    return Isvalid;
}
$('input[name=intervaltype]').click(function () {
    removeActives();


    switch (this.id) {
        case 'general_Contractor':
            $("#Contractor").addClass('active')
            $('#RoleId').val("1");
            break;
        case 'Sub_Contractor':
            $("#Contractor").addClass('active')
            $('#RoleId').val("2");
            break;
        case 'Ind_Contractor':
            $("#Contractor").addClass('active')
            $('#RoleId').val("3");
            break;
        case 'home_Owner':
            $("#Home-Owner").addClass('active')
            $('#RoleId').val("4");
            break;
    }

})
function removeActives() {
    $("#Contractor").removeClass('active');
    $("#Sub-Contractor").removeClass('active');
    $("#Ind-Contractor").removeClass('active');
    $("#Home-Owner").removeClass('active');
}




$(document).ready(function () {
    $("#myform112").click(function () {
        $("#subcontactorModal11").modal('show');
    });
});




$(document).ready(function () {

    $("#BronzePriceMonthly").click(function () {
        $('input:checked').removeAttr('checked');
    });

});


function localizer(engLan, espanolLan, selectedLang) {
    if (selectedLang == "es") {
        return espanolLan;
    } else {
        return engLan;
    }
}