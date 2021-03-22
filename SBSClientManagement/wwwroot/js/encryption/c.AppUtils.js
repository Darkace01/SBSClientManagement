/* ============================================================
 * Module to perform some javascript tasks"
 *
 * @author Mark Adesina <omark@simplexsystem.com>
 * @Dependencies aes.js,cleavejs,jquery-confirm.js
 *============================================================*/

var SiteUtils = SiteUtils || (function ($) {
    'use strict';
    
    var key = CryptoJS.enc.Utf8.parse('0001020304050607');
    var iv = CryptoJS.enc.Utf8.parse('1011121314151617');

    var utilsFunc = {

        get: function (data, url) {
            return jQuery.ajax({
                type: "GET",
                url: url,
                data: data
            });
        },
        getJson: function (data, url) {
            return jQuery.ajax({
                type: "GET",
                url: url,
                data: JSON.stringify(data),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            });
        },
        postJson: function (data, url) {
            return jQuery.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(data),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json'
            });
        },
        post: function (data, url) {
            return jQuery.ajax({
                type: "POST",
                url: url,
                data: data
            });
        },
        encrypt: function (data) {
            if (data === undefined) {
                return '';
            }
            var retData = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(data), key,
                { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });

            return retData;
        },

        decrypt: function (data) {
            var retData = CryptoJS.AES.decrypt(data, key,
                { keySize: 128 / 8, iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });

            return retData.toString(CryptoJS.enc.Utf8);

        },

        //Format Number
        formatNumber: function (num) {
            if (num === undefined) {
                return 0;
            }

            return accounting.formatMoney(num, '');

        },
        removeFormat: function (num) {
            return accounting.unformat(num);
        },

        toFixed: function (num) {
            return accounting.toFixed(num);
        },
        loading: function () { //Show Loading Icon
            var loaderStyle = '<style id="loaderStyle"> .loader {  position: absolute;left: 50%;top: 50%;border: 16px solid #f3f3f3;' +
                'border-top: 16px solid #047bf8;border-bottom: 16px solid #047bf8; border-radius: 50%;width: 90px; height: 90px;' +
                'animation: spin 1s linear infinite;margin: auto; z-index: 20000 !important; opacity: 1;}' +
                '@keyframes spin {0% { transform: rotate(0deg);} 100% { transform: rotate(360deg);}}</style>';

            jQuery('head').append(loaderStyle);
            jQuery('body')
                .append('<div style="position: fixed;top:0;right:0;bottom:0;left:0;z-index:19000;background-color:#000;opacity:0.7" class="loading-back-drop"></div>');

            jQuery('body').append('<div class="loader"></div>');
        },
        loadingOff: function () {
            jQuery('body').find(jQuery(".loading-back-drop")).hide().remove();
            jQuery('body').find(jQuery(".loader")).hide().remove();
            jQuery('head').find("#loaderStyle").remove();
        },
        showMessage: function (title, type, content) {

            jQuery.alert({
                content: content,
                type: type,
                title: title
            });
        },
        pageEncryp: function (data) {
            let dataVal = JSON.stringify(data);
            dataVal = SiteUtils.encrypt(dataVal).toString();
            return dataVal;
        },
        pageDecryp: function (data) {
            let res = SiteUtils.decrypt(data);
            res = JSON.parse(res);
            return res;
        },
        globalNotifierType: {
            "success":"success",
            "error": "error",
            "warning": "warning",
            "info": "info"
        },
        globalNotifier: function (message, type, title) {
            switch (type.toLowerCase()) {
                case "success":
                    toastr.success(message, title);
                    break;
                case "error":
                    toastr.error(message, title);
                    break;
                case "warning":
                    toastr.warning(message, title);
                    break;
                case "info":
                    toastr.info(message, title);
                    break;
            }
        }
    };

    return utilsFunc;



})(jQuery);

jQuery(document).ready(function () {

    jQuery('.currency').toArray().forEach(function (field) {
        new Cleave(field, {
            numeral: true,
            delimiter: ',',
            numeralPositiveOnly: true
        });
    });
    
    
    jQuery(".dateTime").datetimepicker({
        timepicker: false,
        format: 'd/m/Y',
        minDate: '-1970/01/02'
    });
   
});