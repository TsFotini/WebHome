data2 = new Array();

async function Get_Data_Aprtments_Visitors(url, data) {
    await $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true,
        async: true
    }).done(function (result) {

        for (var i = 0; i < result.length; i++) {
            temp = [];
            temp.push(parseInt(result[i].apartment.apartment.id));
            temp.push(result[i].apartment.images.toString());
            temp.push(Number(result[i].apartment.min_price));
            temp.push(result[i].apartment.type_description.toString());
            temp.push(parseInt(result[i].apartment.num_beds));
            temp.push(result[i].message_rate.toString());
            temp.push(parseInt(result[i].rate));
            data2.push(temp);
        }


    }).fail(function (xhr) {

    });
    return data2;
}

async function Get_Info(url, data) {
    await $.ajax(url, {
        method: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        traditional: true,
        async: true
    }).done(function (result) {
        
    }).fail(function (xhr) {

    });
   
}

async function transportdata(id) {
    await Get_Info(WebSiteUrl + '/ApartmentRent/GetApartment?value=' + id.toString());
    await Get_Info(WebSiteUrl + '/ApartmentRent/GetUserHost');
    await window.location.replace(WebSiteUrl + "/ApartmentRent/Index");
}

async function Create_table() {

    var table = $('#exampleApartments').DataTable({
        data: await Get_Data_Aprtments_Visitors(WebSiteUrl + '/ApartmentsVisitor/GetFilteredApartments'),
        "order": [[2, "desc"]],
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
        transportdata(data[0]);
    });
}

Create_table();
