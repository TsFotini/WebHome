
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



function Register() {
    location.replace(WebSiteUrl + "/Home/SignUp");
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



