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

$("#enddate").change(function () {
    var startDate = document.getElementById("startdate").value;
    var endDate = document.getElementById("enddate").value;

    if ((Date.parse(endDate) <= Date.parse(startDate))) {
        alert("End date should be greater than Start date");
        document.getElementById("enddate").value = "";
    }
});



Get_Data(WebSiteUrl + "/Home/GetApartmentsTypes");

var map = new ol.Map({
    target: 'map',
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM()
        })
    ],
    view: new ol.View({
        center: ol.proj.fromLonLat([37.41, 8.82]),
        zoom: 4
    })
});

map.on('click', function (evt) {
    var coordinates = evt.coordinate;
});



