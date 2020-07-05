data = []
var click_accepted = 0;
async function Get_Data(url, data) {
    await $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true,
        async: true
    }).done(function (result) {
        data = result;
        
    }).fail(function (xhr) {

    });
    return data;
}


async function Get_Info(url, data) {
    info = [];
    await $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true,
        async: true
    }).done(function (result) {
        info = result[0];
        console.log(info.usrname);
    }).fail(function (xhr) {

    });
    return info;
}

function Delete_Request_Details(url, data) {
    $.ajax(url, {
        method: "DELETE",
        dataType: "json",
        async: true,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {

    }).fail(function (xhr) {

    });
}


id_ = 0;
function deleted() {
    Delete_Request_Details(WebSiteUrl + '/Admin/Delete', id_.toString());
    location.reload(true);
}

async function getMyModal(id) {
    id_ = id;
    var modal = document.getElementById("myModal");
    var span = document.getElementsByClassName("close")[0];
    modal.style.display = "block";
    var obj = await Get_Info(WebSiteUrl + '/Admin/GetInfo?value=' + id.toString());
    var str = '<div id="innerdiv">' + '<p style="font-weight: bold;">Username: ' + obj.usrname + '</p >' +
        '<p style="font-weight: bold;">Email: ' + obj.email + '</p>' +
        '<p style="font-weight: bold;">Role: ' + obj.role + '</p>' +
        '<p style="font-weight: bold;">Name: ' + obj.name + '</p>' +
        '<p style="font-weight: bold;">Surname: ' + obj.surname + '</p>' +
        '<p style="font-weight: bold;">Phone: ' + obj.phone_number + '</p>' +
        '<p style="font-weight: bold;">Account created on: ' + obj.created_on.toString() + '</p>' +
        '</div > ';
    if (obj.accepted == 1) {
        $('#mb').append(str);
    }
    else {
        str = str + '<button type="button" class="acceptButton"'+ 'onclick="Acception(' + id +')">Accept</button>';
        $('#mb').append(str);   
    }
    var str1 = '<button class="deleteButton" onclick = "deleted()" > Delete</button>';
    $('#mb').append(str1);  
    span.onclick = function () {
        modal.style.display = "none";
        document.getElementById("mb").innerHTML = "";
        if (click_accepted > 0) {
            location.reload(true);
        }
        
    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            document.getElementById("mb").innerHTML = "";
            if (click_accepted > 0) {
                location.reload(true);
            }
        }
    }
}

function Insert_Acception(url, data) {
    $.ajax(url, {
        method: "PUT",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {

    }).fail(function (xhr) {

    });
}


function Acception(data) {
    click_accepted++;
    ok = document.getElementById("acceptButton");
    console.log(data);
    Insert_Acception(WebSiteUrl + '/Admin/SetAccepted',data);
}

async function Create_table() {
   //dataSet = Get_Data(WebSiteUrl + '/Admin/GetUsers'); 
    var table = $('#example').DataTable({
            data: await Get_Data(WebSiteUrl + '/Admin/GetUsers') ,
            columns: [
                { title: "ID" },
                { title: "USERNAME" },
                { title: "EMAIL" },
                { title: "ACCEPTED" },
                { title: "IMAGE"}
            ]
    });
    $('#example tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        
        if (data[0] == -1) {
            alert("Visitors do not have any information!");
        }
        else if (data[0] == 0) {
            alert("Hey this is you friend!");
            getMyModal(data[0]);
        }
        else {
            getMyModal(data[0]);
        }
    });
}

Create_table();

    








