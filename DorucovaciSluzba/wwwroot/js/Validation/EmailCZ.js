// E-MAIL
$.validator.addMethod("emailcz",
    function (value, element) {
        if (!value) return true;

        return /^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,63}$/.test(value);
});

$.validator.unobtrusive.adapters.addBool("emailcz");