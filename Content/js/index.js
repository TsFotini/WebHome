const list = document.getElementById('apartments');

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



Get_Data(WebSiteUrl + "/Home/GetApartmentsTypes");


