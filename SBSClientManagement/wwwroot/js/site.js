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
    var identify = document.getElementById(password);
    var identifyIcon = document.getElementById(password + 'icon');
    var identifyClass = identify.classList;
    var decry = "";
    if (identifyClass.contains("encrypt")) {
        ValidatePassword();
    } else {
        decry = SiteUtils.encrypt(identify.innerText);
        identify.classList.remove("decrypt");
        identify.classList.add("encrypt");
        identifyIcon.classList.remove("fa-unlock");
        identifyIcon.classList.add("fa-lock");
        identify.innerText = decry;
    }
}

function ValidatePassword() {

    var url = "/Home/ValidateUser";

    $("#validateModelBody").load(url, function () {
        $("#validateModel").modal("show");

    })

}