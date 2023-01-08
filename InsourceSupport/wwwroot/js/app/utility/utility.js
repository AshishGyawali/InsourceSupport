function isFunction(obj) {
    return jQuery.type(obj) === "function";
}

(function ($) {
    $.fn.getData = function (options) {
        var settings = $.extend({
            method: "GET",
            beforeSend: function () {
                console.log("started")
            },
            complete: function () {
                console.log("completed")
            },
            error: function () {
                alert("Error")
            }
        }, options);

        if (!settings.url) {
            console.error('Url is missing.');
            return false;
        }

        $.ajax(options);
    }
}(jQuery));


(function ($) {
$.fn.loadList = function (options) {
    let $this = $(this);
    var settings = $.extend({
        displayExpr: "Name",
        valueExpr: "Id",
        optionalLabel: "--Select--",
        useOptionalLabel: true,
    }, options);

    if (!settings.url) {
        console.error('Url is missing.');
        return false;
    }

    $("").getData({
        url: settings.url,
        success: function (data) {
            if (!data) {
                $this.empty();
            }
            else {
                let html = '';
                if (settings.useOptionalLabel) {
                    html += '<option value="">' + settings.optionalLabel + '</option>';
                }
                for (var i = 0; i < data.length; i++) {
                    let row = data[i];
                    html += '<option value="' + row[settings.valueExpr] + '">' + row[settings.displayExpr] + '</option>'
                }
                $this.html(html);
                if (settings.onSuccess != null && isFunction(settings.onSuccess)) {
                    settings.onSuccess(data);
                }
            }
        }
    })
}

}(jQuery));





