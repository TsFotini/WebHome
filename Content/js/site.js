//Popup Functions
function loginBtnClick() {
    var popup = document.getElementById("popupId");
    popup.style.display = "block";
}

function closePopup() {
    var popup = document.getElementById("popupId");
    popup.style.display = "none";
}

function LogIn() {
    var username = document.getElementById("usrname").value;
    var password = document.getElementById("pass").value;
    var data = { usrname: username, pass: password }
    Insert_Credentials_Request(WebSiteUrl +"/Home/Login", data);
}


//register functions
function Apply() {
    var role;
    var host = document.getElementById("host");
    var tenant = document.getElementById("tenant");
    if (host.checked) {
        role = 1;
    }
    else if (tenant.checked) {
        role = 2;
    }
    else if (host.checked && tenant.checked) {
        role = 3;
    }
    else {
        //visitor id
        role = 4;
    }
    //console.log(role);
    return role;
    
}

function check_usrname() {
    var username = document.getElementById("usrname1").value;
    exists = Get_Usernames_Request(WebSiteUrl + "Register/UsrExist?value=" + username.toString());
    if (exists == 1)
        document.getElementById("usrname").innerHTML = " "; 
    console.log(document.getElementById("usrname1").value);
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
        console.log(photo);
        var data = { usrname: username, pass: password, email: email, name: name, surname: surname, phone_number: mobile, images: photo, role_id: role }
        Insert_Data_Register(WebSiteUrl + "/Register/Register", data);
    }
   
}

function Insert_Credentials_Request(url, data) {
    $.ajax(url, {
        method: "POST",
        dataType: "json",
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
        if (valid == 1) {
            alert("The username you have chosen already exists, please pick another username");
            
        } else {

        }
        return valid;
    }).fail(function (xhr) {

    });
}


function Insert_Data_Register(url, data) {
    $.ajax(url, {
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {
        
    }).fail(function (xhr) {

    });
}



