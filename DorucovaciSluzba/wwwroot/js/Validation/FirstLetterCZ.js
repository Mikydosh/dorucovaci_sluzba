// FIRST LETTER
$.validator.addMethod("firstlettercz",
    function (value, element) {
        if (!value) return true;

        return /^[A-ZÁČĎÉĚÍŇÓŘŠŤÚŮÝŽ][A-Za-záčďéěíňóřšťúůýž ]*$/.test(value.trim());
    });

$.validator.unobtrusive.adapters.addBool("firstlettercz");