async function Insert_Request(url, data) {
    await $.ajax(url, {
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

function checkDates() {
    var from = new Date(document.getElementById('reservefrom').value);
    var to = new Date(document.getElementById('reserveto').value);
    var from_compare = new Date(currfrom.toString());
    var to_compare = new Date(currto.toString());
    if (from < from_compare) {
        alert("Please enter valid date to start from!");
        document.getElementById('reservefrom').value = "";
    }
    if (to > to_compare) {
        alert("Seems that after this date the apartment will be occupied!");
        document.getElementById('reserveto').value = "";
    }
   
    var Difference_In_Time = to.getTime() - from.getTime();

    // To calculate the no. of days between two dates 
    var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
    return Difference_In_Days;
}


click_accepted = 0;
function Book() {
    click_accepted++;
    var modal = document.getElementById("myModal1");
    var span = document.getElementsByClassName("close")[0];
    modal.style.display = "block";
    var str = '<h2>Please confirm details!</h2><br/>' +
        '<label for="from"><b>From:</b></label><input type="text" id="datestart" name="from" value="' + currfrom + '">' + '<br/>' +
        '<label for= "to" > <b>To:</b></label > <input type="text" id="dateend" name="to" value="' + currto + '">' + '<br/>' +
        '<label for="start"><b>From:</b></label><input type="date" id="reservefrom" name="start" onchange="checkDates()"><label for="end1"><b>To:</b></label><input type="date" id="reserveto" name="end1" onchange="checkDates()" >';
        
    $('#pop').append(str);
    diff = checkDates();
    str = str + '<br/><label for="days"><b>Days:</b></label><input type="number" id="days" name="days" value="' + diff + '">' + '<br/>'; //check diff
    $('#pop').append(str);
   
    
    span.onclick = function () {
        modal.style.display = "none";
        document.getElementById("pop").innerHTML = "";
        if (click_accepted > 0) {
            location.reload(true);
        }

    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            document.getElementById("pop").innerHTML = "";
            if (click_accepted > 0) {
                location.reload(true);
            }
        }
    }
}