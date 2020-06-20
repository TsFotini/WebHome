console.log(usrname);
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
            datamore.push(result[i].type_description.toString());
            data1.push(datamore);
        }
    }).fail(function (xhr) {

    });
    return data1;
}


var times_clicked = -1;
function checkIfDecimal() {
        var price = document.getElementById('price').value;
        var decimal = /^[-+]?[0-9]+\.[0-9]+$/;
        console.log(price);
        if (price < 0) {
            alert('Negative Number Inserted')
        }
        else if (price != "" && !(price.match(decimal))) {
            alert('Please enter a decimal value!');
        }
    
}



function AddPlace() {
    times_clicked++;
    var str = '<label for="address"><b>Address :</b></label><input type="text" class="inpu" placeholder="Enter Address" name="address" id="apaddress">' +
        '<br /><br /><label for="reach"><b>How to reach place  : </b></label><br />' +
        '<input type="text" class="inpu" placeholder="Enter busses, metro, other" name="reach" id="reach1"><br /><br />' +
        '<div id="map1" class="map"></div>'+
        '<label for="money"><b>Price :</b></label><input type="text" class="inpu" placeholder="Enter Price For Rent" name="money" id="price" onchange="checkIfDecimal()"><br /><br />' +
        '<label for="moneyperson"><b>Price per Person :</b></label><input type="text" class="inpu" placeholder="Enter Price per person" name="moneyperson" id="priceperson"><br /><br />' +
        '<label for="areaa"><b>Area :</b></label><input type="text" class="inpu" placeholder="Enter Area in cubic meters" name="areaa" id="area"><br /><br />' +
        '<label for="typos"><b>Type of Apartment :</b></label><input type="text" class="inpu" placeholder="Enter Single Room,Double Room, Mansion or other" name="typos" id="types"><br /><br />'+
        '<label for="numpeople"><b>Maximum Number Of People :</b></label><input type="number" class="inpu"  name="numpeople" id="people" min="1" max="10"><br /><br />' +
        '<label for= "bd" > <b>Maximum Number Of Beds :</b></label > <input type="number" class="inpu" name="bd" id="beds" min="1" max="10"><br /><br />' +
        '<label for= "bathroom" > <b>Maximum Number Of Bathrooms :</b></label > <input type="number" class="inpu" name="bathroom" id="baths" min="1" max="10"><br /><br />' +
        '<label for= "bedr" > <b>Maximum Number Of Bedrooms :</b></label > <input type="number" class="inpu" name="bedr" id="bedrooms" min="1" max="10"><br /><br />' +
        '<label for="desc"><b>Description :</b></label><textarea class="inpu" id = "description" rows = "3" cols = "80" name="desc" ></textarea ><br/><br/>' +
        '<label for="rule"><b>Rules :</b></label><textarea class="inpu" id = "rules" rows = "3" cols = "80" name="rule" ></textarea ><br/><br/>' +
        '<label for="ima"><b>Image :</b></label><input type="text" class="inpu" placeholder="Enter Image Path" name="ima" id="imageflat"><br /><br />' +
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

    map.on('click', function (evt) {
        var coordinates = evt.coordinate;
        var answer = confirm('You have chosen place with long =  ' + coordinates[0] + ' and lat = ' + coordinates[1]);
        if (answer == true) {
            //parse to object
        }
        
    });
    checkIfDecimal();
    

}

async function Create_table() {
    var table = $('#example1').DataTable({
        data: await Get_Data(WebSiteUrl + '/Apartments/GetApartments?value=' + usrname.toString()),
        columns: [
            { title: "ID" },
            { title: "ADDRESS" },
            { title: "FREE FROM" },
            { title: "FREE UP TO" },
            { title: "TYPE" }
        ]
    });
    $('#example1 tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        getMyModal(data[0]);
    });
}

Create_table();