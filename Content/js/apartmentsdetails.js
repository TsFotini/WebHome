var map = new ol.Map({
    target: 'detailsmap',
    layers: [
        new ol.layer.Tile({
            source: new ol.source.OSM()
        })
    ],
    view: new ol.View({
        center: ol.proj.fromLonLat([26.1, 45.1]), //change to Athens coordinates
        zoom: 6
    })
});

async function Insert_Request_Details(url, data) {
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

var lonlat = "";
async function ChangedAddress() {
    alert("Please click on the map to insert the coordinates of the new address!");
    map.on('click', await function (evt) {
        var coordinates = evt.coordinate;
        var answer = confirm('You have chosen place with long =  ' + coordinates[0] + ' and lat = ' + coordinates[1]);
        if (answer == true) {
            //parse to object
            lonlat = coordinates;
        }

    });
}

async function UpdatePlace() {
    var address = document.getElementById('detailsaddress').value;
    var reach = document.getElementById('detailsreach1').value;
    var price = document.getElementById('detailsprice').value;
    var area = document.getElementById('detailsarea').value;
    var priceperson = document.getElementById('detailspriceperson').value;
    var people = document.getElementById('detailspeople').value;
    var beds = document.getElementById('detailsbeds').value;
    var baths = document.getElementById('detailsbaths').value;
    var bedrooms = document.getElementById('detailsbedrooms').value;
    var description = document.getElementById('detailsdescription').value;
    var rules = document.getElementById('detailsrules').value;
    var image = document.getElementById('detailsimageflat').value;
    var to = document.getElementById('detailsenddate1').value;
    var from = document.getElementById('detailsstartdate1').value;
    var types = document.getElementById('detailstypes').value;
    var apartment = {
        id: parseInt(current_apartment_id),
        address: address,
        free_from: new Date(from.toString()),
        free_to: new Date(to.toString())
    }
    var data = {
        user_id: parseInt(usrname),
        reach_place: reach,
        max_people: parseInt(people),
        description: description,
        rules: rules,
        num_beds: parseInt(beds),
        num_baths: parseInt(baths),
        num_bedrooms: parseInt(bedrooms),
        images: image,
        area: parseInt(area),
        min_price: Number(price),
        cost_per_person: Number(priceperson),
        type_description: types.toString(),
        apartment: apartment,
        lonlat: lonlat.toString()
    }
    await Insert_Request_Details(WebSiteUrl + '/ApartmentsDetails/UpdatePlace', data);
}

async function Delete_Request_Details(url, data) {
    await $.ajax(url, {
        method: "DELETE",
        dataType: "json",
        async: true,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {

    }).fail(function (xhr) {

    });
}

function DeletePlace() {
    var answer = confirm("Are you sure you want to remove this apartments from list?");
    if (answer == true) {
       Delete_Request_Details(WebSiteUrl + '/ApartmentsDetails/DeletePlace', current_apartment_id);
    }
}