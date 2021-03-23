var clipboard = new ClipboardJS('.copy');
let encryptpassword = null;
clipboard.on('success', function (e) {
    alertify.notify('copied', 'success', 5, function () { });
});

clipboard.on('error', function (e) {
    alertify.notify('error', 'error', 5, function () { });
});

//Encrypting and Decrypting Password
$(document).ready(function () {
    $('#searchBtn').on('click', function () {
        search();
    });
    $('#searchString').on('keyup', function () {
        search();
    });
});
function DecriptPassword(password) {
    encryptpassword = password;
    ValidatePassword();
}

function ValidatePassword() {

    var url = "/Home/ValidateUser";

    $("#validateModelBody").load(url, function () {
        $("#validateModel").modal("show");

    })

}