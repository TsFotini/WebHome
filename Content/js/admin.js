data = []

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

async function Create_table() {
    dataSet = Get_Data(WebSiteUrl + '/Admin/GetUsers'); 
    $('#example').DataTable({
        data: await Get_Data(WebSiteUrl + '/Admin/GetUsers') ,
        columns: [
            { title: "ID" },
            { title: "USERNAME" },
            { title: "EMAIL" },
            { title: "ACCEPTED" },
        ]
    });

}

Create_table();




    








