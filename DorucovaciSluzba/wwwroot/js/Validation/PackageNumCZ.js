// PACKAGE NUM
$.validator.addMethod("packagenumcz",
    function (value, element) {
        if (!value) return true;

        return /^\d{3}-\d{2}-\d{2}$/.test(value);
    });

$.validator.unobtrusive.adapters.addBool("packagenumcz");