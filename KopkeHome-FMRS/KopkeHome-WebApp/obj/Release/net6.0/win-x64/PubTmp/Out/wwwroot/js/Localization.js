




$(document).ready(function () {
    $("#ProhzLangDDL").change(function () {
       // alert("HELLO");
        translateValidationMessages(this.value);
       // alert(this.value);
        console.log("Setting language to " + this.value);
    });

    if (currentLang == "es") {
        $.extend($.validator.messages, message.es);
    } else {
        $.extend($.validator.messages, message.en);
    }

    function translateValidationMessages(currentLang) {
        message = {
            en: {
                required: "Required.",
                remote: "Please fix this field.",
                email: "Wrong email.",
                url: "Please enter a valid URL.",
                date: "Please enter a valid date.",
                dateISO: "Please enter a valid date (ISO).",
                number: "Please enter a valid number.",
                digits: "Please enter only digits.",
                creditcard: "Please enter a valid credit card number.",
                equalTo: "Please enter the same value again.",
                maxlength: $.validator.format("Please enter no more than {0} characters."),
                minlength: $.validator.format("Please enter at least {0} characters."),
                rangelength: $.validator.format("Please enter a value between {0} and {1} characters long."),
                range: $.validator.format("Please enter a value between {0} and {1}."),
                max: $.validator.format("Please enter a value less than or equal to {0}."),
                min: $.validator.format("Please enter a value greater than or equal to {0}.")
            },
            es: {
                required: "See väli peab olema täidetud.",
                maxlength: $.validator.format("Palun sisestage vähem kui {0} tähemärki."),
                minlength: $.validator.format("Palun sisestage vähemalt {0} tähemärki."),
                rangelength: $.validator.format("Palun sisestage väärtus vahemikus {0} kuni {1} tähemärki."),
                email: "Palun sisestage korrektne e-maili aadress.",
                url: "Palun sisestage korrektne URL.",
                date: "Palun sisestage korrektne kuupäev.",
                dateISO: "Palun sisestage korrektne kuupäev (YYYY-MM-DD).",
                number: "Palun sisestage korrektne number.",
                digits: "Palun sisestage ainult numbreid.",
                equalTo: "Palun sisestage sama väärtus uuesti.",
                range: $.validator.format("Palun sisestage väärtus vahemikus {0} kuni {1}."),
                max: $.validator.format("Palun sisestage väärtus, mis on väiksem või võrdne arvuga {0}."),
                min: $.validator.format("Palun sisestage väärtus, mis on suurem või võrdne arvuga {0}."),
                creditcard: "Palun sisestage korrektne krediitkaardi number."
            }
        };
        // console.log("Translating validation messages to: " + currentLang);

        


    }
});