function UserApply() {
    var role;
    var host = document.getElementById("userhost");
    var tenant = document.getElementById("usertenant");
    role = -1;
    if (host.checked && !(tenant.checked)) {
        role = 1;
    }
    else if (tenant.checked && !(host.checked)) {
        role = 2;
    }
    else if (host.checked && tenant.checked) {
        role = 3;
    }
    console.log(role);
    if (role != -1) {
        Put_Data_Username(WebSiteUrl + '/AccountSettings/UpdateRole', role.toString());
    }
    

}

function Get_Usernames_Request(url, data) {
    $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true
    }).done(function (valid) {
        if (valid.valid == 1) {
            alert("The username you have chosen already exists, please pick another username");
            usr = document.getElementById("usrname1");
            usr.value = "";
        } else {

        }
        return valid;
    }).fail(function (xhr) {

    });
}

function UserSubmit() {
    Put_Data_Register(WebSiteUrl + '/AccountSettings/UpdateAccount');
}

function UpdateUserName() {
    var username = document.getElementById("userusrname1").value;
    Put_Data_Username(WebSiteUrl + '/AccountSettings/UpdateUsername', username);

}

function passes() {
    password1 = document.getElementById("userpass1").value;
    password2 = document.getElementById("userpassver").value;
    if (password1 != password2) {
        alert("Passwords do not match try again!");
        document.getElementById("userpass1").value = "";
        document.getElementById("userpassver").value = "";
    }
    else {
        Put_Data_Register(WebSiteUrl + '/AccountSettings/UpdatePass', password1);
    }
}

function passmail() {
    var email = document.getElementById("usermail").value;
    Put_Data_Register(WebSiteUrl + '/AccountSettings/UpdateMail', email);
}

function passname() {
    var name = document.getElementById("username").value;
    Put_Data_Register(WebSiteUrl + '/AccountSettings/UpdateName', name);
}

function passphone() {
    var phone = document.getElementById("usermobile").value;
    Put_Data_Register(WebSiteUrl + '/AccountSettings/UpdatePhone', phone);
}


function passsurname() {
    var sname = document.getElementById("usersurname").value;
    Put_Data_Register(WebSiteUrl + '/AccountSettings/UpdateSurname', sname);
}

function passphoto() {
    var photo = document.getElementById("userphoto").value;
    Put_Data_Register(WebSiteUrl + '/AccountSettings/UpdatePhoto', photo);
}

async function Put_Data_Register(url, data) {
    await $.ajax(url, {
        method: "PUT",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {

    }).fail(function (xhr) {

    });
}

async function Put_Data_Username(url, data) {
    await $.ajax(url, {
        method: "PUT",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {
        if (result == 0) {
            alert("Please change username! This is currently used by another user!");
            document.getElementById("userusrname1").value = "";
        }
    }).fail(function (xhr) {

    });
}