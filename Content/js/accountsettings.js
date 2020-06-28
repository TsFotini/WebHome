function UserApply() {
    var role;
    var host = document.getElementById("userhost");
    var tenant = document.getElementById("usertenant");
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
    return role;

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
    var role = UserApply();
    var username = document.getElementById("userusrname1").value;
    var password = document.getElementById("userpass1").value;
    var email = document.getElementById("usermail").value;
    var name = document.getElementById("username").value;
    var surname = document.getElementById("usersurname").value;
    var mobile = document.getElementById("usermobile").value;
    var photo = document.getElementById("userphoto").value;
    var data = { usrname: username, pass: password, email: email, name: name, surname: surname, phone_number: mobile, images: photo, role_id: role }
    Put_Data_Register(WebSiteUrl + '/AccountSettings/UpdateAccount', data);
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
