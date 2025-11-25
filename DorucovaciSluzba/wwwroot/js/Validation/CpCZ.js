// CP
$.validator.addMethod("cpcz",
    function (value, element) {
        if (!value) return true;

        return /^\d+$/.test(value);
    });

$.validator.unobtrusive.adapters.addBool("cpcz");