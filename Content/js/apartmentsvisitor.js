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
        getMyModal(data[0]);
    });
}

Create_table();
