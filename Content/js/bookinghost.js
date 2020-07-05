data1 = new Array();


async function Get_Data(url, data) {
    await $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true,
        async: true
    }).done(function (result) {

        for (i = 0; i < result.length; i++) {
            datamore = [];
            datamore.push(parseInt(result[i].id));
            datamore.push(result[i].image_apartment.toString());
            datamore.push(result[i].reserved_from.toString());
            datamore.push(result[i].reserved_to.toString());
            datamore.push(result[i].from_username.toString());
            datamore.push(result[i].image_user.toString());
            if (result[i].closed == 0) {
                datamore.push('<button class="acceptButton" onclick="accept()">Ok</button>');
            }
            else {
                datamore.push('<i class="fa fa-check-circle" style="font-size:48px;color:green"></i>');
            }
            datamore.push('<button class="deleteButton" onclick="deleted()">Delete</button>')
            data1.push(datamore);
        }
    }).fail(function (xhr) {

    });
    return data1;
}

function Insert_Request(url, data) {
    $.ajax(url, {
        method: "PUT",
        dataType: "json",
        async: true,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {

    }).fail(function (xhr) {

    });
}

accepted = 0;
function accept(id) {
    accepted++;
    Insert_Request(WebSiteUrl + '/BookingHost/AcceptBooking', id.toString());
}
deleted_ = 0;
function deleted(id){
    deleted_++;
    Delete_Request_Details(WebSiteUrl + '/BookingHost/DeleteBooking', id.toString());
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

async function Create_table() {
    var table = $('#examplebooking').DataTable({
        data: await Get_Data(WebSiteUrl + '/BookingHost/GetBookings?value=' + currUserlogged.toString()),
        columns: [
            { title: "ID" },
            { title: "APARTMENT" },
            { title: "RESERVED FROM" },
            { title: "RESERVED TO" },
            { title: "FROM USER" },
            { title: "VIEW USER" },
            { title: "CLOSED" },
            { title: "REMOVE" }
        ]
    });
    $('#examplebooking tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        if (accepted == 1) {
            accept(data[0])
        }
        if (deleted_ == 1) {
            deleted(data[0]);
        }
        
    });
}

Create_table();