console.log(usrid);
data1 = new Array();


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
            datamore.push(result[i].id.toString());
            datamore.push(result[i].address.toString());
            datamore.push(result[i].free_from.toString());
            datamore.push(result[i].free_to.toString());
            data1.push(datamore);
        }
    }).fail(function (xhr) {

    });
    return data1;
}


var times_clicked = -1;
function checkIfDecimalPrice() {
    var priceperson = document.getElementById('priceperson').value;
    var decimal = /^[-+]?[0-9]+\.[0-9]+$/;
    if (priceperson < 0) {
        alert('Negative Number Inserted');
        document.getElementById('priceperson').value = "";
    }
    else if ((priceperson != "" && !(priceperson.match(decimal)))) {
        alert('Please enter a decimal value!!!!');
        document.getElementById('priceperson').value = "";
    }
}
function checkIfDecimal() {
    var p = document.getElementById('price').value;
    var decimal = /^[-+]?[0-9]+\.[0-9]+$/;
    if (p < 0) {
        alert('Negative Number Inserted');
        document.getElementById('price').value = "";
    }
    else if ( (p != "" && !(p.match(decimal)))) {
        alert('Please enter a decimal value!!!!');
        document.getElementById('price').value = "";
    }
}


async function Insert_Request(url, data) {
    await $.ajax(url, {
        method: "POST",
        dataType: "json",
        async: true,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data)
    }).done(function (result) {

    }).fail(function (xhr) {

    });
}

var lonlat = "";
async function SubmitPlace() {
    var address = document.getElementById('apaddress').value;
    var reach = document.getElementById('reach1').value;
    var price = document.getElementById('price').value;
    var area = document.getElementById('area').value;
    var priceperson = document.getElementById('priceperson').value;
    var people = document.getElementById('people').value;
    var beds = document.getElementById('beds').value;
    var baths = document.getElementById('baths').value;
    var bedrooms = document.getElementById('bedrooms').value;
    var description = document.getElementById('description').value;
    var rules = document.getElementById('rules').value;
    var image = document.getElementById('imageflat').value;
    var to = document.getElementById('enddate1').value;
    var from = document.getElementById('startdate1').value;
    var types = document.getElementById('types').value;
    if (address == "" || price == "" || area == "" || priceperson == "" || people == "" || beds == "" || baths == "" || bedrooms == "" || to == "" || from == "") {
        alert("Please fill in the boxes that need to be filled!");
    }
    answer = false; 
    if (lonlat == "") {
       answer = confirm("Do you want to click on the map to insert coordinate?");
    }
    var apartment = {
        address: address,
        free_from: new Date(from.toString()),
        free_to: new Date(to.toString())
    }
    var data = {
            user_id: parseInt(usrid),
            reach_place: reach,
            max_people: parseInt(people),
            description: description,
            rules: rules,
            num_beds: parseInt(beds),
            num_baths: parseInt(baths),
            num_bedrooms: parseInt(bedrooms),
            area: parseInt(area),
            min_price: Number(price),
            cost_per_person: Number(priceperson),
            lonlat: lonlat.toString(),
            type_description: types.toString(),
            apartment: apartment,
            images: image.toString()
    }
    if (answer == false) {
        Insert_Request(WebSiteUrl + '/Apartments/InsertApartment', JSON.stringify(data));
        location.reload();
    }
        
   
    
}
async function AddPlace() {
    times_clicked++;
    var str = '<label for="address"><b>Address :</b></label><input type="text" class="inpu" placeholder="Enter Address" name="address" id="apaddress">' +
        '<br /><br /><label for="reach"><b>How to reach place  : </b></label><br />' +
        '<input type="text" class="inpu" placeholder="Enter busses, metro, other" name="reach" id="reach1"><br /><br />' +
        '<div id="map1" class="map"></div>'+
        '<label for="money"><b>Price :</b></label><input type="text" class="inpu" placeholder="Enter Price For Rent" name="money" id="price" onchange="checkIfDecimal()"><br /><br />' +
        '<label for="moneyperson"><b>Price per Person :</b></label><input type="text" class="inpu" placeholder="Enter Price per person" name="moneyperson" id="priceperson" onchange="checkIfDecimalPrice()"><br /><br />' +
        '<label for="areaa"><b>Area :</b></label><input type="number" class="inpu" placeholder="Enter Area in cubic meters" name="areaa" id="area" min="1"><br /><br />' +
        '<label for="typos"><b>Type of Apartment :</b></label><input type="text" class="inpu" placeholder="Enter Single Room,Double Room, Mansion or other" name="typos" id="types"><br /><br />'+
        '<label for="numpeople"><b>Maximum Number Of People :</b></label><input type="number" class="inpu"  name="numpeople" id="people" min="1" max="10"><br /><br />' +
        '<label for= "bd" > <b>Maximum Number Of Beds :</b></label > <input type="number" class="inpu" name="bd" id="beds" min="1" max="10"><br /><br />' +
        '<label for= "bathroom" > <b>Maximum Number Of Bathrooms :</b></label > <input type="number" class="inpu" name="bathroom" id="baths" min="1" max="10"><br /><br />' +
        '<label for= "bedr" > <b>Maximum Number Of Bedrooms :</b></label > <input type="number" class="inpu" name="bedr" id="bedrooms" min="1" max="10"><br /><br />' +
        '<label for="desc"><b>Description :</b></label><textarea class="inpu" id = "description" rows = "3" cols = "80" name="desc" ></textarea ><br/><br/>' +
        '<label for="rule"><b>Rules :</b></label><textarea class="inpu" id = "rules" rows = "3" cols = "80" name="rule" ></textarea ><br/><br/>' +
        '<label for="ima"><b>Image :</b></label><input type="file" class="inpu" placeholder="Enter Image Path" name="ima" id="imageflat"  ><br /><br />' +
        '<label for="start1"><b>From:</b></label><input type="date" id="startdate1" name="start1"><label for="end1"><b>To:</b></label><input type="date" id="enddate1" name="end1">' +
        '<button type="button" class="registerButton" onclick="SubmitPlace()">Submit Place</button>';
    if (times_clicked > 0) {
        document.getElementById("innerdiv").innerHTML = "";
        times_clicked = -1;
    }
    else {
        $('#innerdiv').append(str);
       
    }
    var map = new ol.Map({
        target: 'map1',
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
    map.on('click', await function (evt) {
        var coordinates = evt.coordinate;
        var answer = confirm('You have chosen place with long =  ' + coordinates[0] + ' and lat = ' + coordinates[1]);
        if (answer == true) {
            //parse to object
            lonlat = coordinates;
        }
        
    });
}

async function getMyModal(id) {
    await Get_Info(WebSiteUrl + '/ApartmentsDetails/GetInfoApartments?value=' + id.toString());
    await window.location.replace(WebSiteUrl + "/ApartmentsDetails/Index");
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
        info = result[0];
        console.log(info.usrname);
    }).fail(function (xhr) {

    });
    return info;
}


async function Create_table() {
    var table = $('#example1').DataTable({
        data: await Get_Data(WebSiteUrl + '/Apartments/GetApartments?value=' + usrid.toString()),
        columns: [
            { title: "ID" },
            { title: "ADDRESS" },
            { title: "FREE FROM" },
            { title: "FREE UP TO" }
        ]
    });
    $('#example1 tbody').on('click', 'tr',async function () {
        var data = table.row(this).data();
        await getMyModal(data[0]);
    });   
}

Create_table();