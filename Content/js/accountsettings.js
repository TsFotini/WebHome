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
