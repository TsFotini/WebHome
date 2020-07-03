function calculateCost() {
    var cost = document.getElementById('costday').value;
    var days = document.getElementById('numdays').value;
    var result = cost * days;
    document.getElementById('calc_cost').value = result.toString();
}
function calculateCostPerPerson() {
    var cost = document.getElementById('costdayperson').value;
    var days = document.getElementById('numdays').value;
    var result = cost * days;
    document.getElementById('costdaypersontotal').value = result.toString();
}
function checkAll() {
    var proceed = 0;
    var from = new Date(document.getElementById('reservefrom').value);
    var from_compare = new Date(currfrom.toString());
    var to_compare = new Date(currto.toString());
    var to;
    if (document.getElementById('reserveto').value != "") {
        to = new Date(document.getElementById('reserveto').value);
    }
    if (document.getElementById('reserveto').value == "") {
        proceed = 1;
    }
   
    if (from < from_compare) {
        alert("Please enter valid date to start from!");
        document.getElementById('reservefrom').value = "";
        proceed = 1;
    }
    if (to > to_compare) {
        alert("Seems that after this date the apartment will be occupied!");
        document.getElementById('reserveto').value = "";
        proceed = 1;
    }

    if (proceed != 1) {
        var Difference_In_Time = to.getTime() - from.getTime();

        // To calculate the no. of days between two dates 
        var Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);
        document.getElementById('numdays').value = Difference_In_Days.toString();
        calculateCost();
        calculateCostPerPerson();
    }
    
}

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

function ConfirmPlace() {
    var from = document.getElementById('reservefrom').value;
    var to = document.getElementById('reserveto').value;
    var data = { from_user_id: parseInt(currUserlogged), apartment_id: parseInt(currapartment), closed: 0, reserved_from: new Date(from.toString()), reserved_to: new Date(to.toString()) }
    Insert_Request(WebSiteUrl + "/Booking/Confirm", JSON.stringify(data));
    alert("Please wait for host to accept your booking request!");
}