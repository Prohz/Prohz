////$.validator.addMethod("requiredIfTradeLiceChecked", function (val, ele, arg) {
////    if ($('input[name="IsBusinessOrTradeLicense"]:checked').val() == 'true' && ($.trim(val) == '')) { return false; }
////    return true;
////}, "This field is required if IsBusinessOrTradeLicense is checked...");


//$.validator.addMethod("requiredIfLiabilityInsuranceChecked", function (val, ele, arg) {
//    if ($('input[name="IsLiabilityInsurance"]:checked').val() == 'true' && ($.trim(val) == '')) { return false; }
//    return true;
//}, "This field is required if Liability Insurance is checked...");


//$.validator.addMethod("requiredIfWorkmanCompensationChecked", function (val, ele, arg) {
//    if ($('input[name="IsWorkmanCompensationInsurance"]:checked').val() == 'true' && ($.trim(val) == '')) { return false; }
//    return true;
//}, "This field is required if Workman’s Compensation is checked...");
$.validator.addMethod('filesize', function (value, element, param) {
    return this.optional(element) || (element.files[0].size <= param * 1000000)
}, 'File size must be less than {0}');


$(function () {
    
    $("form[name='CreateSubContractorProfile']").validate({
        
        rules: {
            
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
            //IsBusinessOrTradeLicense1: {
            //    required: function (element) {
            //        return $('input[name="IsBusinessOrTradeLicense"]:checked').val() == 'true';
            //    }
            //},
           // BusinessOrTradeLicenseFiles: { requiredIfTradeLiceChecked: true },


            IsLiabilityInsurance: { required: true },
            //IsLiabilityInsurance1: {
            //    required: function (element) {
            //        return $('input[name="IsLiabilityInsurance"]:checked').val() == 'true';
            //    }
            //},
            /*LiabilityInsuranceFile: { requiredIfLiabilityInsuranceChecked: true },*/
            IsWorkmanCompensationInsurance: { required: true },
            //IsWorkmanCompensationInsurance1: {
            //    required: function (element) {
            //        return $('input[name="IsWorkmanCompensationInsurance"]:checked').val() == 'true';
            //    }
            //},
            //WorkmanCompensationInsuranceFile: { requiredIfWorkmanCompensationChecked: true },

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
            IsContactedByContractors: { required: true },
            IsContactedByContractors1: {
                required: function (element) {
                    return $('input[name="IsContactedByContractors"]:checked').val() == 'true';
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
            IsBusinessOrTradeLicense: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),

            BusinessOrTradeLicenseFiles: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
            IsLiabilityInsurance: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            LiabilityInsuranceFile: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
            IsWorkmanCompensationInsurance: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            WorkmanCompensationInsuranceFile: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
            IsEstimateCharge: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),

            IsDesignServices: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsContactedByHomeowners: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsContactedByContractors: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsCash: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            PersonalChecks: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsPaymentApps: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),

            WhichPaymentApps: localizer("Please provide app name", "Proporcione el nombre de la aplicación", $('#ProhzLangDDL option:selected').val()),

            ProfilePicture: {
                required: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
                extension: localizer("Invalid file format. Please upload the image in JPG,  JPEG, PNG, or PSD", "Formato de archivo no válido. Cargue la imagen en JPG, JPEG, PNG o PSD", $('#ProhzLangDDL option:selected').val()),
                filesize: localizer("The file is too big. Please upload file under 5MB", "El archivo es demasiado grande. Cargue un archivo de menos de 5 MB", $('#ProhzLangDDL option:selected').val()),
            },
        },

        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        //submitHandler: function (form) {
        //    form.submit();
        //}
    });
});










$(function () {

    $("form[name='UpdateContractorBusinessProfile']").validate({

        rules: {

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
            //IsBusinessOrTradeLicense1: {
            //    required: function (element) {
            //        return $('input[name="IsBusinessOrTradeLicense"]:checked').val() == 'true';
            //    }
            //},
            // BusinessOrTradeLicenseFiles: { requiredIfTradeLiceChecked: true },


            IsLiabilityInsurance: { required: true },
            //IsLiabilityInsurance1: {
            //    required: function (element) {
            //        return $('input[name="IsLiabilityInsurance"]:checked').val() == 'true';
            //    }
            //},
            /*LiabilityInsuranceFile: { requiredIfLiabilityInsuranceChecked: true },*/
            IsWorkmanCompensationInsurance: { required: true },
            //IsWorkmanCompensationInsurance1: {
            //    required: function (element) {
            //        return $('input[name="IsWorkmanCompensationInsurance"]:checked').val() == 'true';
            //    }
            //},
            //WorkmanCompensationInsuranceFile: { requiredIfWorkmanCompensationChecked: true },

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
            IsContactedByContractors: { required: true },
            IsContactedByContractors1: {
                required: function (element) {
                    return $('input[name="IsContactedByContractors"]:checked').val() == 'true';
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
            IsBusinessOrTradeLicense: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),

            BusinessOrTradeLicenseFiles: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
            IsLiabilityInsurance: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            LiabilityInsuranceFile: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
            IsWorkmanCompensationInsurance: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            WorkmanCompensationInsuranceFile: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
            IsEstimateCharge: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsDesignServices: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsContactedByHomeowners: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsContactedByContractors: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsCash: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            PersonalChecks: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsPaymentApps: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),

            WhichPaymentApps: localizer("Please provide app name", "Proporcione el nombre de la aplicación", $('#ProhzLangDDL option:selected').val())

            
        },

        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        //submitHandler: function (form) {
        //    form.submit();
        //}
    });
});








$(function () {

    $("form[name='UpdateOtherContractorBusinessProfile']").validate({

        rules: {

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
            //IsBusinessOrTradeLicense1: {
            //    required: function (element) {
            //        return $('input[name="IsBusinessOrTradeLicense"]:checked').val() == 'true';
            //    }
            //},
            // BusinessOrTradeLicenseFiles: { requiredIfTradeLiceChecked: true },


            IsLiabilityInsurance: { required: true },
            //IsLiabilityInsurance1: {
            //    required: function (element) {
            //        return $('input[name="IsLiabilityInsurance"]:checked').val() == 'true';
            //    }
            //},
            /*LiabilityInsuranceFile: { requiredIfLiabilityInsuranceChecked: true },*/
            IsWorkmanCompensationInsurance: { required: true },
            //IsWorkmanCompensationInsurance1: {
            //    required: function (element) {
            //        return $('input[name="IsWorkmanCompensationInsurance"]:checked').val() == 'true';
            //    }
            //},
            //WorkmanCompensationInsuranceFile: { requiredIfWorkmanCompensationChecked: true },

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
            IsContactedByContractors: { required: true },
            IsContactedByContractors1: {
                required: function (element) {
                    return $('input[name="IsContactedByContractors"]:checked').val() == 'true';
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
            NormalBusinessHours: localizer("Please enter Normal Business Hours","Ingrese el horario comercial normal", $('#ProhzLangDDL option:selected').val()),

            Is24HoursPhoneAnswering: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsOfferEmergencyServices: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsBusinessOrTradeLicense: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),

            BusinessOrTradeLicenseFiles: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
            IsLiabilityInsurance: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            LiabilityInsuranceFile: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
            IsWorkmanCompensationInsurance: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            WorkmanCompensationInsuranceFile: localizer("Please choose required file", "Por favor elija el archivo requerido", $('#ProhzLangDDL option:selected').val()),
            IsEstimateCharge: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsDesignServices: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsContactedByHomeowners: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsContactedByContractors: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),


            IsCash: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            PersonalChecks: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),
            IsPaymentApps: localizer("Please choose at least one", "Por favor elige al menos uno", $('#ProhzLangDDL option:selected').val()),

            WhichPaymentApps: localizer("Please provide app name", "Proporcione el nombre de la aplicación", $('#ProhzLangDDL option:selected').val()),

        },

        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        //submitHandler: function (form) {
        //    form.submit();
        //}
    });
});

$.validator.addMethod("regx", function (value, element, regexpr) {
    return regexpr.test(value);
}, localizer("Please enter a valid pasword.", "Por favor, introduzca una contraseña válida", $('#ProhzLangDDL option:selected').val()));


$(function () {
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("form[name='UpdateUserBasicInfo1']").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side

            FirstName: "required",
            LastName: "required",
            
            State: {
                required: true,
            },
            ZipCode: {
                required: true,
                minlength: 5,
                maxlength: 5,
            },
           
        },
        // Specify validation error messages
        messages: {
            FirstName: localizer("Please enter your first name", "Por favor, introduzca su nombre de pila", $('#ProhzLangDDL option:selected').val()),
            LastName: localizer("Please enter your last name","Por favor ingrese su apellido", $('#ProhzLangDDL option:selected').val()),
            
            
            ZipCode: {
                required: localizer("Zip code is required.", "Se requiere código postal", $('#ProhzLangDDL option:selected').val()),
                minlength: localizer("Please enter a valid zip code.", "Por favor ingrese un código postal válido.", $('#ProhzLangDDL option:selected').val()),
                maxlength: localizer("Please enter a valid zip code.", "Por favor ingrese un código postal válido.", $('#ProhzLangDDL option:selected').val()),
            },
            State: {
                required: localizer("State is required.", "Se requiere estado", $('#ProhzLangDDL option:selected').val()),

            },
           
        },

    });
});


function localizer(engLan, espanolLan, selectedLang) {
    if (selectedLang == "es") {
        return espanolLan;
    } else {
        return engLan;
    }
}