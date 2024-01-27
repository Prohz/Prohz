

var urlprefix = '';
if (window.location.href.indexOf("KopkeHome-FMRS-QA") > -1) {
    urlprefix = '/KopkeHome-FMRS-QA';
}

function AfterCancelSubscription(s) {
    Swal.fire({
        icon: 'error',
        title: localizer('Current Plan is Cancelled.', 'El plan actual está cancelado', $('#ProhzLangDDL option:selected').val()),
        text: localizer('You have cancelled current plan.You can still use the application as long as your last plan is valid.', 'Ha cancelado el plan actual. Todavía puede usar la aplicación siempre que su último plan sea válido.', $('#ProhzLangDDL option:selected').val()),
       /* footer: '<a href="">Why do I have this issue?</a>'*/
    })
}
var ProhzGreyLogo = '<img style="height: 90px;" src="' + urlprefix + '/images/ProhzGreyLogo.new.png">';

function CancelSubscription(d) {
    var id = d.getAttribute("id");
    var EndDate = "";
    EndDate = d.getAttribute("data-EndDate");
    Swal.fire({
        title: localizer('Are You Sure?', 'Estas seguro?', $('#ProhzLangDDL option:selected').val()),
        iconHtml: ProhzGreyLogo,
        text: localizer('Current Plan will be cancelled but you can still use the benefits and services till the end of the current billing cycle i.e ' + EndDate + ', Do you want to cancel it ?', 'El plan actual se cancelará, pero aún puede usar los beneficios y servicios hasta el final del ciclo de facturación actual, es decir,' + EndDate +', ¿Quieres cancelarla?', $('#ProhzLangDDL option:selected').val()),
        showDenyButton: true,
        
        showCancelButton: false,
        confirmButtonText: localizer('Do not cancel', 'No cancelar', $('#ProhzLangDDL option:selected').val()),
        denyButtonText: localizer(`Yes, cancel it.`, `Sí, cancelarlo.`, $('#ProhzLangDDL option:selected').val()),
    }).then((result) => {
        
        if (result.isConfirmed) {

        } else if (result.isDenied) {
           
            $.ajax({
                type: 'Get',
                url: urlprefix + '/Payment/CancelSubscription?subId='+id,
                success: function (res) {
                    if (res == "1") {
                        Swal.fire(localizer('Subscription Cancelled!', `¡Suscripción cancelada!`, $('#ProhzLangDDL option:selected').val()), '', 'success')
                        location.reload();
                    }
                    else {
                        Swal.fire(localizer('Network Issue!', `¡Problema de red!`, $('#ProhzLangDDL option:selected').val()), '', 'info')
                    }
                   

                }
            });
        }
    })
}

function UpgradeToGoldSubscription() {
    debugger
    var PlanId = "";
    var subId = "";
    var CusId = "";
    var PriceId = "";
    var selectedVal = "";
    var currentSubs = "";
    var PrevPlanId = "";
    var Msg = "";
    var MsgFor = "";
    var LastDate = "";
    
    var selected = $("input[type='radio'][name='GoldPrice']:checked");
    var upgradeMsg
    if (selected.length > 0) {
        PrevPlanId = selected.attr("data-PrevPlanId");
        currentSubs = selected.attr("data-CurrentsubId");
        selectedVal = selected.val();
        subId = selected.attr("data-subId");
        PlanId = selected.attr("Id");
        PriceId = selected.attr("data-PriceId");
        CusId = selected.attr("data-CusId");
        LastDate = selected.attr("data-LastDate");
        if (currentSubs == selected.attr("data-PriceId")) {
            Swal.fire(localizer('Already subscribed to this plan. please change different plan or billing cycle.', 'Ya está suscrito a este plan. cambie el plan o el ciclo de facturación por otro diferente.', $('#ProhzLangDDL option:selected').val()))
        }
        else {
            if (PrevPlanId < PlanId || PrevPlanId == PlanId) {
                Msg = localizer("Upgrade", "Potenciar", $('#ProhzLangDDL option:selected').val());
                MsgFor = localizer("Subscription Plan will be Upgraded", "El plan de suscripción se actualizará", $('#ProhzLangDDL option:selected').val());

            } else {
                Msg = localizer("Downgrade", "Degradar", $('#ProhzLangDDL option:selected').val());
                MsgFor = localizer(
                        "Subscription plan will end " + LastDate + ". Once you downgrade to a lower plan you won't be able to use the services of your current plan. Current plan will be downgraded with immediate effect",
                        "El plan de suscripción terminará" + LastDate + ". Una vez que baje de categoría a un plan inferior, no podrá utilizar los servicios de su plan actual. El plan actual se degradará con efecto inmediato",
                        $('#ProhzLangDDL option:selected').val()
                    )
            }
            Swal.fire({
                title: localizer('Are You Sure?', "Estas seguro?", $('#ProhzLangDDL option:selected').val()),
                iconHtml: ProhzGreyLogo,
                text: localizer('Current ' + MsgFor + ', Do you wish to continue ?', "Actual " + MsgFor +" Desea continuar ?", $('#ProhzLangDDL option:selected').val()),
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: localizer('Do not ' + Msg, 'No ' + Msg, $('#ProhzLangDDL option:selected').val()),
                denyButtonText: localizer(`Yes, ` + Msg + ' it.', `Sí, ` + Msg + ' eso.', $('#ProhzLangDDL option:selected').val()),
            }).then((result) => {

                if (result.isConfirmed) {
                  
                    $("input[type='radio'][name='GoldPrice']").prop("checked", false);
                }
                else if (result.isDenied) {

                    if (PrevPlanId < PlanId || PrevPlanId == PlanId) {
                        $("#CSTMLOADER").show();
                        $.ajax({
                            type: 'Get',
                            url: urlprefix + '/Payment/UpgradeSubscription?subId=' + subId + '&CusId=' + CusId + '&PriceId=' + PriceId + '&PlanId=' + PlanId,
                            success: function (res) {

                                window.location = res;
                                if (res == "1") {
                                    Swal.fire(localizer('Subscription Upgraded!', '¡Suscripción actualizada!', $('#ProhzLangDDL option:selected').val()), '', 'success')
                                    setTimeout(function () {

                                        location.reload(true);
                                    }, 2000);
                                }



                            }
                        });
                    }
                    else {
                        $("#CSTMLOADER").show();
                        $.ajax({
                            type: 'Get',
                            url: urlprefix + '/Payment/DowngradeSubscription?subId=' + subId + '&CusId=' + CusId + '&PriceId=' + PriceId + '&PlanId=' + PlanId,
                            success: function (res) {

                                window.location = res;
                                if (res == "1") {
                                    Swal.fire(localizer('Subscription Downgraded!', '¡Suscripción degradada!', $('#ProhzLangDDL option:selected').val()), '', 'success')
                                    setTimeout(function () {

                                        location.reload(true);
                                    }, 2000);
                                }



                            }
                        });
                    }
                    
                }
            })
        }
    }
   
    else
    {
/*        Swal.fire('Please select Subscription Plan type Monthly/Yearly', '', 'info')*/
        Swal.fire({
            title: localizer('Please select Subscription Plan type Quarterly/Yearly', 'Seleccione el tipo de plan de suscripción Trimestral/Anual', $('#ProhzLangDDL option:selected').val()),
            iconHtml: ProhzGreyLogo,
            text: '',
           
        })
    }
   
}


function UpgradeToSilverSubscription() {
    debugger
    var PlanId = "";
    var subId = "";
    var CusId = "";
    var PriceId = "";
    var selectedVal = "";
    var currentSubs = "";
    var PrevPlanId = "";
    var Msg = "";
    var MsgFor = "";
    var LastDate = "";
    var selected = $("input[type='radio'][name='SilverPrice']:checked");
    
    if (selected.length > 0) {
        PrevPlanId = selected.attr("data-PrevPlanId");
        currentSubs = selected.attr("data-CurrentsubId");
        selectedVal = selected.val();
        subId = selected.attr("data-subId");
        PlanId = selected.attr("Id");
        PriceId = selected.attr("data-PriceId");
        CusId = selected.attr("data-CusId");
        LastDate = selected.attr("data-LastDate");

        if (currentSubs == PriceId) {
            Swal.fire({
                title: localizer('Already Subscribed to this plan. please chose different plan or billing cycle.', 'Ya está suscrito a este plan. elija otro plan o ciclo de facturación.', $('#ProhzLangDDL option:selected').val()),
                iconHtml: ProhzGreyLogo,
                text: '',

            })
        }
        else {
            if (PrevPlanId < PlanId || PrevPlanId == PlanId) {
                Msg = localizer("Upgrade","Potenciar", $('#ProhzLangDDL option:selected').val());
                MsgFor = localizer("Subscription Plan will be Upgraded", "El plan de suscripción se actualizará", $('#ProhzLangDDL option:selected').val());

            } else {
                Msg = localizer("Downgrade", "Degradar", $('#ProhzLangDDL option:selected').val());
                MsgFor = localizer("Subscription plan will end " + LastDate + ". Once you downgrade to a lower plan you won't be able to use the services of your current plan. Current plan will be downgraded with immediate effect", "El plan de suscripción finalizará" + LastDate + ". Una vez que cambie a un plan inferior, no podrá utilizar los servicios de su plan actual. El plan actual se degradará con efecto inmediato", $('#ProhzLangDDL option:selected').val());
            }
            Swal.fire({
                title: localizer('Are You Sure?', 'Estas segura?', $('#ProhzLangDDL option:selected').val()),
                iconHtml: ProhzGreyLogo,
                text: localizer('Current ' + MsgFor + ', Do you wish to continue ?', 'Current' + MsgFor + ', ¿Desea continuar?', $('#ProhzLangDDL option:selected').val()),
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: localizer('Do not ' + Msg, 'No ' + Msg, $('#ProhzLangDDL option:selected').val()),
                denyButtonText: `Yes, ` + Msg + ' it.',
            }).then((result) => {

                if (result.isConfirmed) {
                    $("input[type='radio'][name='SilverPrice']").prop("checked", false);
                }
                else if (result.isDenied) {

                    if (PrevPlanId < PlanId || PrevPlanId == PlanId) {
                        $("#CSTMLOADER").show();
                        $.ajax({
                            type: 'Get',
                            url: urlprefix + '/Payment/UpgradeSubscription?subId=' + subId + '&CusId=' + CusId + '&PriceId=' + PriceId + '&PlanId=' + PlanId,
                            success: function (res) {

                                window.location = res;
                                if (res == "1") {
                                    Swal.fire(localizer('Subscription Upgraded!', '¡Suscripción actualizada!', $('#ProhzLangDDL option:selected').val()), '', 'success')
                                    setTimeout(function () {

                                        location.reload(true);
                                    }, 2000);
                                }



                            }
                        });
                    }
                    else {
                        $("#CSTMLOADER").show();
                        $.ajax({
                            type: 'Get',
                            url: urlprefix + '/Payment/DowngradeSubscription?subId=' + subId + '&CusId=' + CusId + '&PriceId=' + PriceId + '&PlanId=' + PlanId,
                            success: function (res) {

                                window.location = res;
                                if (res == "1") {
                                    Swal.fire(localizer('Subscription Downgraded!', '¡Suscripción degradada!', $('#ProhzLangDDL option:selected').val()), '', 'success')
                                    setTimeout(function () {

                                        location.reload(true);
                                    }, 2000);
                                }



                            }
                        });
                    }
                }
            })
        }
    }
    
    else {
       // Swal.fire('Please select Subscription Plan type Monthly/Yearly', '', 'info')
        Swal.fire({
            title: localizer('Please select Subscription Plan type Quarterly/Yearly', 'Seleccione el tipo de plan de suscripción Trimestral/Anual', $('#ProhzLangDDL option:selected').val()),
            iconHtml: ProhzGreyLogo,
            text: '',

        })
    }

}




function UpgradeToBronzeSubscription() {
    
    var PlanId = "";
    var subId = "";
    var CusId = "";
    var PriceId = "";
    var selectedVal = "";
    var currentSubs = "";
    var PrevPlanId = "";
    var Msg = "";
    var MsgFor = "";
    var LastDate = "";

    var selected = $("input[type='radio'][name='BronzePrice']:checked");

    if (selected.length > 0) {
        PrevPlanId = selected.attr("data-PrevPlanId");
        currentSubs = selected.attr("data-CurrentsubId");
        selectedVal = selected.val();
        subId = selected.attr("data-subId");
        PlanId = selected.attr("Id");
        PriceId = selected.attr("data-PriceId");
        CusId = selected.attr("data-CusId");
        LastDate = selected.attr("data-LastDate");

        if (currentSubs == PriceId) {
           
            Swal.fire({
                title: localizer('Already Subscribed to this plan. please chose different plan or billing cycle.', 'Ya está suscrito a este plan. elija otro plan o ciclo de facturación.', $('#ProhzLangDDL option:selected').val()),
                iconHtml: ProhzGreyLogo,
                text: '',

            })
        }
        else {
            debugger
            if (PrevPlanId < PlanId || PrevPlanId == PlanId) {
                Msg = localizer("Upgrade", "Potenciar", $('#ProhzLangDDL option:selected').val());
                MsgFor = localizer("Subscription Plan will be Upgraded", "El plan de suscripción se actualizará", $('#ProhzLangDDL option:selected').val());

            } else {
                Msg = localizer("Downgrade", "Degradar", $('#ProhzLangDDL option:selected').val());
                MsgFor = localizer("Subscription plan will end " + LastDate + ". Once you downgrade to a lower plan you won't be able to use the services of your current plan. Current plan will be downgraded with immediate effect", "El plan de suscripción finalizará" + LastDate + ". Una vez que cambie a un plan inferior, no podrá utilizar los servicios de su plan actual. El plan actual se degradará con efecto inmediato", $('#ProhzLangDDL option:selected').val());
            }
            Swal.fire({
                title: localizer('Are You Sure?', 'Estas segura?', $('#ProhzLangDDL option:selected').val()),
                iconHtml: ProhzGreyLogo,
                text: localizer('Current ' + MsgFor + ', Do you wish to continue ?', 'Current' + MsgFor + ', ¿Desea continuar?', $('#ProhzLangDDL option:selected').val()),
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: localizer('Do not ' + Msg, 'No ' + Msg, $('#ProhzLangDDL option:selected').val()),
                denyButtonText: localizer(`Yes, ` + Msg + ' it.', `Si, ` + Msg + ' it.', $('#ProhzLangDDL option:selected').val()),
            }).then((result) => {

                if (result.isConfirmed) {
                    $("input[type='radio'][name='BronzePrice']").prop("checked", false);
                }
                else if (result.isDenied) {

                    if (PrevPlanId < PlanId || PrevPlanId == PlanId) {
                        $("#CSTMLOADER").show();
                        $.ajax({
                            type: 'Get',
                            url: urlprefix + '/Payment/UpgradeSubscription?subId=' + subId + '&CusId=' + CusId + '&PriceId=' + PriceId + '&PlanId=' + PlanId,
                            success: function (res) {

                                window.location = res;
                                if (res == "1") {
                                    Swal.fire(localizer('Subscription Upgraded!', '¡Suscripción actualizada!', $('#ProhzLangDDL option:selected').val()), '', 'success')
                                    setTimeout(function () {

                                        location.reload(true);
                                    }, 2000);
                                }



                            }
                        });
                    }
                    else {
                        $("#CSTMLOADER").show();
                        $.ajax({
                            type: 'Get',
                            url: urlprefix + '/Payment/DowngradeSubscription?subId=' + subId + '&CusId=' + CusId + '&PriceId=' + PriceId + '&PlanId=' + PlanId,
                            success: function (res) {

                                window.location = res;
                                if (res == "1") {
                                    Swal.fire(localizer('Subscription Downgraded!', '¡Suscripción degradada!', $('#ProhzLangDDL option:selected').val()), '', 'success')
                                    setTimeout(function () {

                                        location.reload(true);
                                    }, 2000);
                                }



                            }
                        });
                    }
                }
            })
        }
    }

    else {
        //Swal.fire('Please select Subscription Plan type Monthly/Yearly', '', 'info')
        Swal.fire({
            title: localizer('Please select Subscription Plan type Quarterly/Yearly', 'Seleccione el tipo de plan de suscripción Trimestral/Anual', $('#ProhzLangDDL option:selected').val()),
            iconHtml: ProhzGreyLogo,
            text: '',

        })
    }

}



function localizer(engLan, espanolLan, selectedLang) {
    if (selectedLang == "es") {
        return espanolLan;
    } else {
        return engLan;
    }
}