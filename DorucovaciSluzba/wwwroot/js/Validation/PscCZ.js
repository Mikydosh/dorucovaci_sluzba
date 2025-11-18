// PSC
$.validator.addMethod("psccz",
    function (value, element) {
        if (!value) return true;

        return /^\d{3}\s?\d{2}$/.test(value);
    });

$.validator.unobtrusive.adapters.addBool("psccz");