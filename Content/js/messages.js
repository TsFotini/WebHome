function Insert_Request(url, data) {
    $.ajax(url, {
        method: "POST",
        dataType: "json",
        async: true,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {

    }).fail(function (xhr) {

    });
}

function Send() {
    var message = document.getElementById('message').value;
    var data = { from_user_id: parseInt(currUserlogged), to_user_id: parseInt(currhost), apartment_id: parseInt(currapartment), message_body: message.toString() }
    Insert_Request(WebSiteUrl + '/Messages/Send', JSON.stringify(data));
    alert("Your message has been sent! Please wait for host's answer!");
}

function Book() {
    if (curr_role != -1) {
        window.location.replace(WebSiteUrl + "/Booking/Index");
    }
    else {
        alert("Please Log in to confirm booking!");
    }
   
}

