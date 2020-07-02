var map = new ol.Map({
    target: 'smap',
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

