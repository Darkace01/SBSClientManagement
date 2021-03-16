let errorVal = document.getElementById("error");
function validateDetails() {
    let response = false
    if (valName() == true && valUsername() == true && valPassowrd() == true) {
        response = true;
    } else {
        response = false
    }
    return response;
}
//validate the name
function valName() {
    let nameError = document.getElementById("nameVal");
    nameError.innerHTML = "";
    let name = document.getElementById("name").value;
    if (name.length > 0) {
        return true;
    } else {
        nameError.innerHTML = "Input Vpn Name";
        return false;
    }
}
//validate the username
function valUsername() {
    let usernameError = document.getElementById("usernameVal");
    usernameError.innerHTML = "";
    let username = document.getElementById("username").value;
    if (username.length > 0) {
        return true;
    } else {
        usernameError.innerHTML = "Input Vpn Username";
        return false;
    }
}
//validate the password
function valPassowrd() {
    let passwordError = document.getElementById("passwordVal");
    passwordError.innerHTML = "";
    let password = document.getElementById("password").value;
    if (password.length > 0) {
        return true;
    } else {
        passwordError.innerHTML = "Input Vpn Password";
        return false;
    }
}

//the form calls this
function validateForm() {
    if (validateDetails() == true) {
        let name = document.getElementById("name").value;
        checkVpn(name);
        return false;
    } else {
        return false;
    }
}

//Validate if the vpn name already exists
function checkVpn(vpn) {

    $.ajax({
        url: "/Vpn/VpnExist/?vpnName=" + vpn,
        type: "GET",
        success: function (result) {
            if (result == false) {
                sendData();
            } else {
                errorVal.innerHTML = "Vpn Name Already Exist";
                return false;
            }
        }
    })
}

//submits the form
function sendData() {
    var valdata = $("#newVpnForm").serialize();
    $.ajax({
        url: "/Vpn/Create",
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: valdata,
        success: function (result) {
            if (result.statusCode == 200) {
                alertify.notify('Saved', 'success', 3, function () { location.reload(); });

            } else {
                let ex = "Error Saving " + result.value;
                alertify.notify(ex, 'error', 3, function () { });
                return false;
            }
        },
        error: function (result) {
            let ex = "Error Saving " + result.value;
            alertify.notify(ex, 'error', 3, function () { });
            return false;
        }
    });
}