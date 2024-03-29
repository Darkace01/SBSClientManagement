﻿let errorVal = document.getElementById("error");
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
        nameError.innerHTML = "Input Server Name";
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
        usernameError.innerHTML = "Input Server Username";
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
        passwordError.innerHTML = "Input Server Password";
        return false;
    }
}

//the form calls this
function validateForm() {
    if (validateDetails() == true) {
        sendData();
    } else {
        return false;
    }
}



//submits the form
function sendData() {
    var valdata = $("#newServerForm").serialize();
    $.ajax({
        url: "/Server/Edit",
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: valdata,
        success: function (result) {
            if (result.statusCode == 200) {
                alertify.notify('Updated', 'success', 3, function () { location.reload(); });

            } else {
                let ex = "Error Updating " + result.value;
                alertify.notify(ex, 'error', 3, function () { });
                return false;
            }
        },
        error: function (result) {
            let ex = "Error Updating " + result.value;
            alertify.notify(ex, 'error', 3, function () { });
            return false;
        }
    });
}