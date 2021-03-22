var clipboard = new ClipboardJS('.copy');

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
    var identify = document.getElementById(password);
    var identifyClass = identify.classList;
    var decry = "";
    if (identifyClass.contains("encrypt")) {
        decry = SiteUtils.decrypt(identify.innerText);
        identify.classList.remove("encrypt");
        identify.classList.add("decrypt");
    } else if (identifyClass.contains("decrypt")) {
        decry = SiteUtils.encrypt(identify.innerText);
        identify.classList.remove("decrypt");
        identify.classList.add("encrypt");
    }
    identify.innerText = decry;
}