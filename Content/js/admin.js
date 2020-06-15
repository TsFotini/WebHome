var dataSet = [];
function Get_Data(url, data) {
    $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        async: false
    }).done(function (result) {
        console.log(result[0]);
        dataSet = result;
    }).fail(function (xhr) {

    });
}


$('#example').DataTable({
    data: Get_Data(WebSiteUrl + '/Admin/GetUsers'),
        columns: [
            { title: "ID" },
            { title: "USERNAME" },
            { title: "EMAIL" },
            { title: "ACCEPTED" },
        ]
});

