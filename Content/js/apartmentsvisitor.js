data = [];
function Create_table() {

    var table = $('#exampleApartments').DataTable({
        data: Get_Data_Aprtments_Visitors(WebSiteUrl + '/ApartmentsVisitor/GetApartments'),
        columns: [
            { title: "ID" },
            { title: "IMAGE" },
            { title: "COST" },
            { title: "TYPE" },
            { title: "NUM. OF BEDS" },
            { title: "CRITICS" },
            { title: "RATINGS" }
        ]
    });
    $('#exampleApartments tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        getMyModal(data[0]);
    });
}

async function Get_Data_Aprtments_Visitors(url, data) {
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


Create_table();
