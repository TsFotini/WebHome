const list = document.getElementById('apartments');
const mylist = document.getElementById('places');

function Get_Data(url, data) {
    $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true
    }).done(function (result) {
        list_type = []
        for (var i = 0; i < result.length; i++) {
            list_type.push(result[i].description)
            
        }
        list_type.forEach(item => {
            let option = document.createElement('option');
            option.value = item;
            list.appendChild(option);
        });
    }).fail(function (xhr) {

    });
}

function Get_Places(url, data) {
    $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true
    }).done(function (result) {
        result.forEach(item => {
            let option = document.createElement('option');
            option.value = item;
            mylist.appendChild(option);
        });
    }).fail(function (xhr) {

    });
}


$("#enddate").change(function () {
    var startDate = document.getElementById("startdate").value;
    var endDate = document.getElementById("enddate").value;

    if ((Date.parse(endDate) <= Date.parse(startDate))) {
        alert("End date should be greater than Start date");
        document.getElementById("enddate").value = "";
    }
});



Get_Data(WebSiteUrl + "/Home/GetApartmentsTypes");
Get_Places(WebSiteUrl + "/Home/GetPlaces");

var myIndex = 0;
carousel();

function carousel() {
    var i;
    var x = document.getElementsByClassName("mySlides");
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none";
    }
    myIndex++;
    if (myIndex > x.length) { myIndex = 1 }
    x[myIndex - 1].style.display = "block";
    setTimeout(carousel, 2000); // Change image every 2 seconds
}

async function Insert_Request_Infos(url, data) {
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

async function catchInfo() {
    apartmentsType = document.getElementById('aps').value;
    place = document.getElementById('place').value;
    startdate = document.getElementById('startdate').value;
    enddate = document.getElementById('enddate').value;
    quantity = document.getElementById('quantity').value;
    if (quantity == "") {
        alert("Please fill in number of people!");
    }
    else if (startdate == "" || enddate == "") {
        alert("Please fill in dates!");
    }
    data = { type: apartmentsType, address: place, num_people: parseInt(quantity), from: new Date(startdate.toString()), to: new Date(enddate.toString()) }
    await Insert_Request_Infos("/ApartmentsVisitor/CatchInfos", JSON.stringify(data));
    await window.location.replace(WebSiteUrl + "/ApartmentsVisitor/Index");
}