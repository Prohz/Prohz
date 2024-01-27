
var urlprefix = '';
if (window.location.href.indexOf("KopkeHome-FMRS-QA") > -1) {
    urlprefix = '/KopkeHome-FMRS-QA';
}






var ProhzGreyLogo = '<img style="height: 90px;" src="' + urlprefix + '/images/ProhzGreyLogo.new.png">';

$(document).ready(function () {

    $('.UpdateStatusDocumentsVerification').on('change', function ()
    {
        // alert(this.value);
        var model = {};

        model.UserId = $(this).attr('data-userId');
        model.VerificationStatus = this.value;



        

        if (this.value==3) {
            Swal.fire({
                title: 'Are You Sure?',
                iconHtml: ProhzGreyLogo,
                text: '',
                showDenyButton: true,

                showCancelButton: false,
                confirmButtonText: 'Do not Approve',
                denyButtonText: `Yes, Approve it.`,
            }).then((result) => {

                if (result.isConfirmed) {

                }
                else if (result.isDenied) {

                    $.ajax({
                        type: "POST",
                        url: urlprefix + "/Admin/UpdateDocumentsStatus",
                        /*async: true,*/
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(model),
                        success: function (res) {
                            //  alert(res);
                            if (res == "1") {
                                Swal.fire(
                                    "Updated!",
                                    "Status updated successfully.",
                                    "success"
                                );
                                setTimeout(function () {

                                    location.reload();
                                }, 1000);
                            }
                        }
                    });
                }
            })
        }

        

    });
    $('input[name="LegalDocFiles"]').change(function () {
        $('#fileUploadAdminSpan').text('');
        var file_size = this.files[0].size;
        //console.log(file_size);
        if (file_size < 52428800) {
            var ext = $('input[name="LegalDocFiles"]').val().split('.').pop().toLowerCase();

            if ($.inArray(ext, ['pdf', 'doc','docx']) == -1) {
                $('#fileUploadAdminSpan').text('Invalid file format.File must be pdf or doc');

                this.value = '';
            }
        }
        else {
            ('#fileUploadAdminSpan').text('File size is too big.File should be under 50 MB');
            this.value = '';
        }



    });




    $('input[name="fileUpload"]').change(function () {
        $('#fileUploadAdminSpan').text('');
        var file_size = this.files[0].size;
        //console.log(file_size);
        if (file_size < 52428800) {
            var ext = $('input[name="fileUpload"]').val().split('.').pop().toLowerCase();

            if ($.inArray(ext, ['mp4','mkv']) == -1) {
                $('#fileUploadAdminSpan').text('Invalid file format.');

                this.value = '';
            }
        }
        else {
            ('#fileUploadAdminSpan').text('File size is too big.File should be under 50 MB');
            this.value = '';
        }
        


    });


    $(".sideMenu .AdminLeftMenuList").on("click", function () {
        $(".AdminLeftMenuList").find(".nav1-active").removeClass("nav1-active");
        $(this).addClass("nav1-active");
    });
    $('.ModalCloseFAQ').click(function () {
        $('#FAQViewModal').modal('hide');
        $('#AddFAQAdminViewModal').modal('hide');
        $('#AddVideoAdminViewModal').modal('hide');
        $('#AddCategoryAdminViewModal').modal('hide');
        $('#CategoryViewModal').modal('hide');
        $('#EditAdminFilesViewModal').modal('hide');
        $('#CustomPlanCreateViewModal').modal('hide');
        $('#fileUploadAdminSpan').text('');
        $('.error').text('');

      
        
    }); 
    $('.AddCategoryAdmin').click(function () {
        $('#AddCategoryAdminViewModal').modal('toggle');
        $('#AddCategoryAdminViewModal').modal('show');

    });
    $('.AddVideoAdmin').click(function ()
    {
        $('#AddVideoAdminViewModal').modal('toggle');
        $('#AddVideoAdminViewModal').modal('show');

    });
    $('.AddAdminFiles').click(function () {
        $('#AddAdminFilesViewModal').modal('toggle');
        $('#AddAdminFilesViewModal').modal('show');

        
        $('#EditAdminFilesViewModal').modal('toggle');
        $('#EditAdminFilesViewModal').modal('show');

        $('input[name="PrevFile"]').val($(this).attr('data-PrevFile'));
        $('#LegalId').val($(this).attr('data-id'));
        $('#TypeId').val($(this).attr('data-FileType'));


    });
    $('.AddFAQAdmin').click(function () {
        $('#AddFAQAdminViewModal').modal('toggle');
        $('#AddFAQAdminViewModal').modal('show');
    });
    
    $('.EditCategoryAdmin').click(function () {
        var data = $(this).attr('data-id');

        $.ajax({
            type: "Get",
            url: urlprefix + "/admin/GetCategoryById",
            data: {
                id: data
            },
            success: function (res) {

                $("#CategoryId").val(res.id);
                $("#CategoryNameAdmin").val(res.name);
               

                $('#CategoryViewModal').modal('toggle');
                $('#CategoryViewModal').modal('show');
                /* $('#myModal').modal('hide');*/
            }

        });

    });





    $('.EditFAQAdmin').click(function () {
        var data = $(this).attr('data-id');
       
        $.ajax({
            type: "Get",
            url: urlprefix + "/admin/GetFAQById",
            data: {
                id: data
            },
            success: function (res) {
                
                $("#FAQId").val(res.id);
                $("#FAQQuestion").val(res.question);
                $("#FAQAnswer").val(res.answer);

                $('#FAQViewModal').modal('toggle');
                $('#FAQViewModal').modal('show');
               /* $('#myModal').modal('hide');*/
            }

        });

    });

   


    $('.CreateCustomPlanAdmin').click(function () {
        var data = $(this).attr('data-requestId');
       // alert(data);
        $.ajax({
            type: "Get",
            url: urlprefix + "/admin/GetCustomReqById",
            data: {
                id: data
            },
            success: function (res) {
               
                console.log(res);
                if (res.mobileApp) {
                    $("#MobileAppVM").val("Yes");
                    $("#HdnMobileApp").val("true");
                }
                if (res.webApp) {
                    $("#WebAppVM").val("Yes");
                    $("#HdnWebApp").val("true");
                }
                if (res.isYearly) {
                    $("#IsYearlyVM").val("Yes");
                    $("#HdnIsYearly").val("true");
                }
                $("#ReUserId").val(res.userId);
                $("#NumberOfZipcodesVM").val(res.numberOfZipcodes);
                $("#NumberOfCategories").val(res.numberOfCategories);
                $("#DescrptionVM").val(res.descrption);
                $("#PriceVM").val(res.priceMonthly);
                //$("#FAQAnswer").val(res.answer);

                $('#CustomPlanCreateViewModal').modal('toggle');
                $('#CustomPlanCreateViewModal').modal('show');
                /* $('#myModal').modal('hide');*/
            }

        });

    });
     
    jQuery(function ($) {
        var path = window.location.href; // because the 'href' property of the DOM element is the absolute path
        $('.AdminLeftMenuList a').each(function () {
            if (this.href === path) {
                $(this).parent("li").addClass('nav1-active');
            }
            else {
                $(this).parent("li").removeClass('nav1-active');
            }
        });
    });
});
document.onreadystatechange = function () {
    if (document.readyState !== "complete") {

        document.querySelector(".logo-loader").style.visibility = "visible";
    } else {

        document.querySelector("body").style.visibility = "visible";
        document.querySelector(".logo-loader").style.display = "none";
    }
};

$(function () {

    $("form[name='AddFAQForm']").validate({

        rules: {

            Question: "required",
            Answer: "required",




        },
        // Specify validation error messages
        messages: {
            BusinessDescription: "Question can't be blank.",
            Answer: "Answer can't be blank.",

        },

        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        //submitHandler: function (form) {
        //    form.submit();
        //}
    });
});




$(function () {

    $("form[name='UpdateFAQForm']").validate({

        rules: {
            Question: "required",
            Answer: "required",
        },
        messages: {
            Question: "Question can't be blank.",
            Answer: "Answer can't be blank.",
        },
    });



    $("form[name='UpdateCategoryForm']").validate({

        rules: {
            Name: "required",
            
        },
        messages: {
            Name: "Category can't be blank.",
           
        },
    });

    $("form[name='AddCategoryForm']").validate({

        rules: {
            Name: "required",

        },
        messages: {
            Name: "Category name can't be blank.",

        },
    });
});

$('#uploadVideos').submit(function (e) {
    e.preventDefault(); // stop the standard form submission

    $.ajax({
        url: this.action,
        type: this.method,
        data: new FormData(this),
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data == "1") {
                Swal.fire(
                    "Saved!",
                    "Video added successfully.",
                    "success"
                );
                setTimeout(function () {

                    location.reload();
                }, 1000);

            }
            else if (data == "2") {
                Swal.fire({
                    title: '',
                    iconHtml: ProhzGreyLogo,
                    text: 'Please select Video.',

                });
                
            }
            else {

            }
        },
        error: function (xhr, error, status) {
            console.log(error, status);
        }
    });
});
function CheckValLegalFile() {
   
    if (document.getElementById("LegalUploadFile").files.length == 0) {
        $('#fileUploadAdminSpan').html('Please select a file.')
        console.log("no files selected");
        return false;
    }
    
    
}
function AddVideo(form) {
    
    var form = new FormData($('#movieFormData')[0]);
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/admin/AddVideo',
            data: $(form).serialize(),
            success: function (res) {

                if (res == "1") {
                    Swal.fire(
                        "Saved!",
                        "Video added successfully.",
                        "success"
                    );
                    setTimeout(function () {

                        location.reload();
                    }, 10000);

                }
                else {
                    Swal.fire('Network Issue.', '', 'info')
                }

            }
        });
    }



    return false;
}


function CreateCustomPlanFromAdmin(form) {
    
   
    
    if ($("#PriceVM").val().trim() > 0 && $("#PriceVM").val().trim() != "") {
        $.validator.unobtrusive.parse(form);
        if ($(form).valid()) {
            $("#CSTMLOADER").show();
            $.ajax({
                type: 'POST',
                url: urlprefix + '/admin/AddCreateCustomPlan',
                data: $(form).serialize(),
                success: function (res) {

                    if (res == "1") {
                        $("#CSTMLOADER").hide();
                        Swal.fire(
                            "Done!",
                            "Plan Created successfully.",
                            "success"
                        );
                        setTimeout(function () {

                            location.reload();
                        }, 1000);

                    }
                    else {
                        $("#CSTMLOADER").hide();
                        Swal.fire('Network Issue.Please try again later', '', 'info')
                    }

                }
            });
        }
    }
    else
    {
        Swal.fire("Please enter price.");
        return false;
    }
    
    return false;
}
function AddFAQ(form) {

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/admin/AddFAQ',
            data: $(form).serialize(),
            success: function (res) {

                if (res == "1") {
                    Swal.fire(
                        "Added!",
                        "FAQ added successfully.",
                        "success"
                    );
                    setTimeout(function () {

                        location.reload();
                    }, 1000);

                }
                else {
                    Swal.fire('Network Issue.', '', 'info')
                }

            }
        });
    }



    return false;
}

function UpdateFAQ(form) {
   
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/admin/UpdateFAQ',
            data: $(form).serialize(),
            success: function (res) {

                if (res == "1") {
                    Swal.fire(
                        "Updated!",
                        "FAQ updated successfully.",
                        "success"
                    );
                    setTimeout(function () {

                        location.reload();
                    }, 1000);

                }
                else {
                    Swal.fire('Network Issue.', '', 'info')
                }

            }
        });
    }



    return false;
}

function DeleteFAQ(id) {


    Swal.fire({
        title: 'Are You Sure?',
        iconHtml: ProhzGreyLogo,
        text: 'This FAQ will be deleted.',
        showDenyButton: true,

        showCancelButton: false,
        confirmButtonText: 'No',
        denyButtonText: `Yes, delete it.`,
    }).then((result) => {

        if (result.isConfirmed) {

        } else if (result.isDenied) {

            $.ajax({
                type: 'Get',
                url: urlprefix + '/admin/DeleteFAQ?Id=' + id,
                success: function (res) {
                    if (res == "1") {
                        Swal.fire(
                            "Deleted!",
                            "FAQ has been deleted.",
                            "success"
                        );
                        setTimeout(function () {

                            location.reload();
                        }, 1000);
                        
                    }
                    else {
                        Swal.fire('Network Issue.', '', 'info')
                    }


                }
            });
        }
    })
}


function DeleteVideoAdmin(id) {


    Swal.fire({
        title: 'Are You Sure?',
        iconHtml: ProhzGreyLogo,
        text: 'This Video will be deleted.',
        showDenyButton: true,

        showCancelButton: false,
        confirmButtonText: 'No',
        denyButtonText: `Yes, delete it.`,
    }).then((result) => {

        if (result.isConfirmed) {

        } else if (result.isDenied) {

            $.ajax({
                type: 'Get',
                url: urlprefix + '/admin/DeletePromoVideo?Id=' + id,
                success: function (res) {
                    if (res == "1") {
                        Swal.fire(
                            "Deleted!",
                            "Video has been deleted.",
                            "success"
                        );
                        setTimeout(function () {

                            location.reload();
                        }, 1000);

                    }
                    else {
                        Swal.fire('Network Issue.', '', 'info')
                    }


                }
            });
        }
    })
}

(function ($) {
    $.fn.currencyInput = function () {
        this.each(function () {
            var wrapper = $("<div class='currency-input' />");
            $(this).wrap(wrapper);
            $(this).before("<span class='currency-symbol'>$</span>");
            $(this).change(function () {
                var min = parseFloat($(this).attr("min"));
                var max = parseFloat($(this).attr("max"));
                var value = this.valueAsNumber;
                if (value < min)
                    value = min;
                else if (value > max)
                    value = max;
                $(this).val(value.toFixed(2));
            });
        });
    };
})(jQuery);

$(document).ready(function () {
    $('input.currency').currencyInput();
});

//Categories

function AddCategoryAdmin(form) {

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/admin/AddCategory',
            data: $(form).serialize(),
            success: function (res) {
             
                if (res == "1") {
                    Swal.fire(
                        "Added!",
                        "Category added successfully.",
                        "success"
                    );
                    setTimeout(function () {

                        location.reload();
                    }, 1000);

                }
                else if (res == "2"){
                    Swal.fire('Category Already Exist!.', '', 'info')
                }
                
                else {
                    Swal.fire('Network Issue.', '', 'info')
                }

            }
        });
    }



    return false;
}

function UpdateCategory(form) {

    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        $.ajax({
            type: 'POST',
            url: urlprefix + '/admin/UpdateCategory',
            data: $(form).serialize(),
            success: function (res) {

                if (res == "1") {
                    Swal.fire(
                        "Updated!",
                        "Category updated successfully.",
                        "success"
                    );
                    setTimeout(function () {

                        location.reload();
                    }, 1000);

                }
                else {
                    Swal.fire('Network Issue.', '', 'info')
                }

            }
        });
    }



    return false;
}

function DeleteCategory(id) {


    Swal.fire({
        title: 'Are You Sure?',
        iconHtml: ProhzGreyLogo,
        text: 'This Category will be deleted.',
        showDenyButton: true,

        showCancelButton: false,
        confirmButtonText: 'No',
        denyButtonText: `Yes, delete it.`,
    }).then((result) => {

        if (result.isConfirmed) {

        } else if (result.isDenied) {

            $.ajax({
                type: 'Get',
                url: urlprefix + '/admin/DeleteCategory?Id=' + id,
                success: function (res) {
                    if (res == "1") {
                        Swal.fire(
                            "Deleted!",
                            "Category has been deleted.",
                            "success"
                        );
                        setTimeout(function () {

                            location.reload();
                        }, 1000);

                    }
                    else {
                        Swal.fire('Network Issue.', '', 'info')
                    }


                }
            });
        }
    })
}