//Popup Functions

async function LogIn() {
    var username = document.getElementById("usrname").value;
    var password = document.getElementById("pass").value;
    var data = { usrname: username, pass: password }
    Insert_Credentials_Request(WebSiteUrl + "/Login/Authenticate", data);
    Get_Log_Request(WebSiteUrl + "/Login/Log");
}

//register functions
function Apply() {
    
    var host = document.getElementById("host");
    var tenant = document.getElementById("tenant");
    if (host.checked && !(tenant.checked)) {
        role = 1;
    }
    else if (tenant.checked && !(host.checked)) {
        role = 2;
    }
    else if (host.checked && tenant.checked) {
        role = 3;
    }
    //console.log(role);
    return role;
    
}

function check_usrname() {
    var username = document.getElementById("usrname1").value;
    exists = Get_Usernames_Request(WebSiteUrl + "Register/UsrExist?value=" + username.toString());
}

function check_credentials() {
    password1 = document.getElementById("pass1").value;
    password2 = document.getElementById("passver").value;
    var username = document.getElementById("usrname1").value;
    var email = document.getElementById("mail").value;
    var value = false;
    
    if (username == '') {
        alert("Please enter Username");
    }
    if (email == '') {
        alert("Please enter Email");
    }
    if (password1 == '') {
        alert("Please enter Password");
    }
    // If confirm password not entered 
    else if (password2 == '') {
        alert("Please enter confirm password");

    }
    // If Not same return False.     
    else if (password1 != password2) {
        alert("\nPassword did not match: Please try again...")

    }
    else {
        value = true;
    }
    return value;
} 

//register
function Submit() {
    if (check_credentials() != false) {
        var role = Apply();
        var username = document.getElementById("usrname1").value;
        var password = document.getElementById("pass1").value;
        var email = document.getElementById("mail").value;
        var name = document.getElementById("name").value;
        var surname = document.getElementById("surname").value;
        var mobile = document.getElementById("mobile").value;
        var photo = document.getElementById("photo").value;
        var data = { usrname: username, pass: password, email: email, name: name, surname: surname, phone_number: mobile, images: photo, role_id: role }
        Insert_Data_Register(WebSiteUrl + "/Register/Register", data);
        if (role == 1 || role == 3)
            alert("We will send your form to the admin, please wait for the responce later");
    }
   
}

function Insert_Credentials_Request(url, data) {
    $.ajax(url, {
        method: "POST",
        dataType: "json",
        async: false, //
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {

    }).fail(function (xhr) {
        
    });
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


async function Insert_Data_Register(url, data) {
    await $.ajax(url, {
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {
        
    }).fail(function (xhr) {

    });
}

async function Get_Log_Request(url, data) {
    await $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true
    }).done(function (valid) {
        if (valid.valid == 1) {
            alert("Admin is in");
            window.location.replace(WebSiteUrl + "/Admin/Index");
           
        }
        else if (valid.valid == 2) {
            alert("Host is in");
            window.location.replace(WebSiteUrl + "/Apartments/Index");
        }
        else {
            window.location.replace(WebSiteUrl);
        }
        return valid;
    }).fail(function (xhr) {

    });
}

//After login
function AccountLogIn(is_logged_in,role_id) {
    if (is_logged_in == 1) {
        var str = '<ul class="navbar-nav flex-grow-1">' +
            '<li class="nav-item">' +
            '<button class="userBtn" onclick="RelocateSettings()">' + usrname + '</button>' +
            '</li>' +
            '</ul>';
        $('#userlogged').append(str);
        if (role_id == 0) {
            str = '<ul class="navbar-nav flex-grow-1">' +
                '<li class="nav-item">' +
                '<button class="pageBtn" onclick="RelocateAdmin()">Admin</button>' +
                '</li>' +
                '</ul>';
            $('#pageuser').append(str);
        }
        else if (role_id == 1) {
            str = '<ul class="navbar-nav flex-grow-1">' +
                '<li class="nav-item">' +
                '<button class="pageBtn" onclick="RelocateHost()">Host Page</button>' +
                '</li>' +
                '</ul>';
            $('#pageuser').append(str);
        }
        var str_logout = '<ul class="navbar-nav flex-grow-1">' +
            '<li class="nav-item">' +
            '<button class="logoutBtn" onclick="LogOut()"> Logout ' + usrname + '</button>' +
            '</li>' +
            '</ul>';
        $('#logout').append(str_logout);
    }
}

function Get_User_Request(url, data) {
    
    $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true
    }).done(function (valid) {
        
    }).fail(function (xhr) {

    });
   
}

AccountLogIn(log, role_user);

async function RelocateAdmin() {
    await window.location.replace(WebSiteUrl + '/Admin/Index');
}

async function RelocateHost() {
    await window.location.replace(WebSiteUrl + '/Apartments/Index');
}

function LogOut() {
    Get_User_Request(WebSiteUrl + '/Login/Logout');
    window.location.replace(WebSiteUrl + '/Home/Index');
}

async function RelocateSettings() {
    await Get_User_Request(WebSiteUrl + "/AccountSettings/GetCurrUser?value=" + userid.toString());
    await window.location.replace(WebSiteUrl + '/AccountSettings/Index');
}


