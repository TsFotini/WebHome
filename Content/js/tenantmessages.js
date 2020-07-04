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
            datamore.push(result[i].image_apartment.toString());
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
            datamore.push(result[i].image_apartment.toString());
            data2.push(datamore);
        }
    }).fail(function (xhr) {

    });
    return data2;
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
async function Insert_Request(url, data) {
    await $.ajax(url, {
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

async function Reply() {
    var user = document.getElementById("user").value;
    await Insert_From(WebSiteUrl + '/TenantMessages/From', user);
    var message = document.getElementById("reply").value;
    await Insert_From(WebSiteUrl + '/TenantMessages/Reply', message);
}

async function getMyModal(id) {
    
    var modal = document.getElementById("myModalnewtenant");
    var span = document.getElementsByClassName("close")[0];
    modal.style.display = "block";
    var obj = await Get_Info(WebSiteUrl + '/TenantMessages/GetMessage?value=' + id.toString());
    var str = '<div id="innerdiv">' + '<label for="address"><b>To :</b></label><input type="text" class="inpu" id="user" name="address" value="' + obj.from_username + '">' +
        '<label for="desc"><b>Reply Message :</b></label><textarea class="inpu" id ="reply" rows ="3" cols ="80" name="desc" ></textarea ><br/><br/>' +
        '<button class="replyBtn" onclick="Reply()">Reply</button>'
        + '</div > ';
    $('#newmodaltenant').append(str);

    span.onclick = function () {
        modal.style.display = "none";
        document.getElementById("newmodaltenant").innerHTML = "";
        Insert_Request(WebSiteUrl + '/TenantMessages/Seen', id.toString());
        location.reload(true);
    }
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
            document.getElementById("newmodaltenant").innerHTML = "";

        }
    }
}

async function Create_table() {
    console.log(curruserid_);
    var table = $('#examplemessagestenant').DataTable({
        
        data: await Get_Data(WebSiteUrl + '/TenantMessages/GetMessages?value=' + curruserid_.toString()),
        columns: [
            { title: "ID" },
            { title: "FROM" },
            { title: "MESSAGE" },
            { title: "USER" },
            { title: "NEW MESSAGE" },
            { title: "APARTMENT" }
        ]
    });
    $('#examplemessagestenant tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        getMyModal(data[0]);
    });
}



async function Create_table_old() {
    var table = $('#exampleoldtenant').DataTable({
        data: await Get_Data_old(WebSiteUrl + '/TenantMessages/GetOldMessages?value=' + curruserid_.toString()),
        columns: [
            { title: "ID" },
            { title: "FROM" },
            { title: "MESSAGE" },
            { title: "USER" },
            { title: "SEEN" },
            { title: "APARTMENT" }
        ]
    });
    /*$('#example1 tbody').on('click', 'tr', async function () {
        var data = table.row(this).data();
        await getMyModal(data[0]);
    });*/
}

Create_table();
Create_table_old();