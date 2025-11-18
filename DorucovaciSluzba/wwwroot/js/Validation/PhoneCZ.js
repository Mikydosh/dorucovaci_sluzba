// PHONE NUMBER
$.validator.addMethod("phonecz",
    function (value, element) {
        if (!value) return true;

        return /^(\d{9}|\d{3}\s\d{3}\s\d{3})$/.test(value);
});

$.validator.unobtrusive.adapters.addBool("phonecz");