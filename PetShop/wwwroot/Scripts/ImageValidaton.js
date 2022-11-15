$.validator.addMethod("onlyimage",
    function (value, element, parameters) {
        var fileType = $(element)[0].files[0].type;
        var fileTypes = ["image/jpeg", "image/pjpeg", "image/gif", "image/bmp", "image/png", "image/x-png", "image/tiff"]
        var validExtension = $.inArray(fileType, fileTypes) !== -1;
        return validExtension;
    });

$.validator.unobtrusive.adapters.add("onlyimage", [], function (options) {
    var params = {
        fileextensions: $(options.element).data("val-onlyimage").split(',')
    };

    options.rules['onlyimage'] = params;
    options.messages['onlyimage'] = $(options.element).data("val-onlyimage");
});