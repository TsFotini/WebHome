data1 = new Array();
data2 = new Array();
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
            datamore.push(result[i].from_username.toString());
            datamore.push(result[i].message_body.toString());
            datamore.push(result[i].images.toString());
            datamore.push(result[i].seen.toString());
            data1.push(datamore);
        }
    }).fail(function (xhr) {

    });
    return data1;
}

async function Get_Data_old(url, data) {
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
            datamore.push(result[i].from_username.toString());
            datamore.push(result[i].message_body.toString());
            datamore.push(result[i].images.toString());
            datamore.push(result[i].seen.toString());
            data2.push(datamore);
        }
    }).fail(function (xhr) {

    });
    return data2;
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

function Insert_From(url, data) {
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
        info = result;
    }).fail(function (xhr) {

    });
    return info;
}

click_accepted = 0;

async function Reply() {
    var user = document.getElementById("user").value;
    await Insert_From(WebSiteUrl + '/MessagesApartment/From', user);
    var message = document.getElementById("reply").value;
    await Insert_From(WebSiteUrl + '/MessagesApartment/Reply', message); 
}
async function getMyModal(id) {
    click_accepted++;
    var modal = document.getElementById("myModalnew");
    var span = document.getElementsByClassName("close")[0];
    modal.style.display = "block";
    var obj = await Get_Info(WebSiteUrl + '/MessagesApartment/GetMessage?value=' + id.toString());
    var str = '<div id="innerdiv">' + '<label for="address"><b>From :</b></label><input type="text" class="inpu" id="user" name="address" value="' + obj.from_username + '">' +
        '<label for="desc"><b>Reply Message :</b></label><textarea class="inpu" id ="reply" rows ="3" cols ="80" name="desc" ></textarea ><br/><br/>' +
        '<button class="replyBtn" onclick="Reply()">Reply</button>'
        + '</div > ';
    $('#newmodal').append(str);

    span.onclick = function () {
        modal.style.display = "none";
        document.getElementById("newmodal").innerHTML = "";
        Insert_Request(WebSiteUrl + '/MessagesApartment/Seen', id.toString());
        location.reload(true);
    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            document.getElementById("newmodal").innerHTML = "";
            
        }
    }
}


async function Create_table() {
    var table = $('#examplemessages').DataTable({
        data: await Get_Data(WebSiteUrl + '/MessagesApartment/GetMessages'),
        columns: [
            { title: "ID" },
            { title: "FROM" },
            { title: "MESSAGE" },
            { title: "USER" },
            { title: "NEW MESSAGE" }
        ]
    });
    $('#examplemessages tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        getMyModal(data[0]);
    });
}



async function Create_table_old() {
    var table = $('#exampleold').DataTable({
        data: await Get_Data_old(WebSiteUrl + '/MessagesApartment/GetOldMessages'),
        columns: [
            { title: "ID" },
            { title: "FROM" },
            { title: "MESSAGE" },
            { title: "USER" },
            { title: "SEEN" }
        ]
    });
    /*$('#example1 tbody').on('click', 'tr', async function () {
        var data = table.row(this).data();
        await getMyModal(data[0]);
    });*/
}

Create_table();
Create_table_old();