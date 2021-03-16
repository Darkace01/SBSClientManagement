//Get the Servers by Client Id
$(document).ready(function () {
    $('#ClientId').on('change', function () {
        let clientId = $('#ClientId option:selected').val();
        $.ajax({
            type: 'GET',
            data: { clientId: clientId },
            url: '/SqlServer/GetClientByServerId',
            success: function (result) {
                var c = '<option value="-1">Select Server</option>';
                for (var i = 0, l = result.length; i < l; i++) {
                    c += '<option value="' + result[i].id + '">' + result[i].name + '</option>';
                }
                c += '<option value="-1">None</option>';
                $('#ServerId').html(c);
            }
        });
    });
});


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
//validate the instancename
function valName() {
    let nameError = document.getElementById("nameVal");
    nameError.innerHTML = "";
    let name = document.getElementById("instancename").value;
    if (name.length > 0) {
        return true;
    } else {
        nameError.innerHTML = "Input SqlServer Name";
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
        usernameError.innerHTML = "Input SqlServer Username";
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
        passwordError.innerHTML = "Input SqlServer Password";
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
    var valdata = $("#newSqlServerForm").serialize();
    $.ajax({
        url: "/SqlServer/Edit",
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