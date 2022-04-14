var app = (function () {
    function init() {

        // Product
        this.product = {
            index: {}
        };

        this.enableButton = function (e) {
            $(e).attr("disabled", false);
        }

        this.disableButton = function (e) {
            $(e).attr("disabled", true);
        }

        this.preventMultipleSubmission = function (form) {
            var btnSubmit = $("input:submit", $(form));
            if (!$(form).valid() || $(form).validate().pendingRequest !== 0) {
                return false;
            } else {
                btnSubmit.attr("disabled", "disabled");
            }
        }

        this.init = function (obj) {};
    }
    return new init();
})(jQuery);