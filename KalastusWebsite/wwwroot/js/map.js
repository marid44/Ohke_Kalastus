let map;
let riversLayer, roadsLayer;

// Define the ETRS-TM35FIN projection (EPSG:3067)
const crs = new L.Proj.CRS('EPSG:3067',
    '+proj=utm +zone=35 +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs',
    {
        origin: [-548576, 8388608],
        resolutions: [
            8192, 4096, 2048, 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1, 0.5, 0.25
        ],
        bounds: L.bounds([-548576, 6291456], [1548576, 8388608])
    }
);

function initializeMap(apiKey) {
    if (map) {
        map.remove(); // Destroy the existing map instance
        map = null;
    }

    // Initialize the map without a specific view, we'll use fitBounds
    map = L.map('map', {
        crs: crs,
        continuousWorld: true,
        worldCopyJump: false,
        maxZoom: 15,
        minZoom: 0
    });

    // Set the bounds to fit Finland
    const finlandBounds = [
        [59.5, 19.0], // Southwest corner (Helsinki region)
        [70.1, 31.0]  // Northeast corner (Lapland)
    ];
    map.fitBounds(finlandBounds); // Adjust map to show all of Finland

    // Rivers layer using WMTS
    const wmtsUrl = 'https://avoin-karttakuva.maanmittauslaitos.fi/avoin/wmts/1.0.0';
    riversLayer = L.tileLayer(`${wmtsUrl}/maastokartta/default/ETRS-TM35FIN/{z}/{y}/{x}.png?api-key=${apiKey}`, {
        attribution: '&copy; Maanmittauslaitos',
        maxZoom: 15,
        minZoom: 0,
        tileSize: 256,
        transparent: true
    });

    // Roads layer using WMTS
    roadsLayer = L.tileLayer(`${wmtsUrl}/liikenneverkko/default/ETRS-TM35FIN/{z}/{y}/{x}.png?api-key=${apiKey}`, {
        attribution: '&copy; Maanmittauslaitos',
        maxZoom: 15,
        minZoom: 0,
        tileSize: 256,
        transparent: true
    });

    // Automatically add the rivers layer
    riversLayer.addTo(map);
}

function toggleLayer(layerName, isVisible) {
    const layer = layerName === "rivers" ? riversLayer :
        layerName === "roads" ? roadsLayer : null;

    if (layer) {
        if (isVisible) {
            layer.addTo(map);
        } else {
            map.removeLayer(layer);
        }
    }
}

function searchLocation(query, apiKey) {
    if (!query || !apiKey) return;

    const geocodingUrl = `https://avoin-paikkatieto.maanmittauslaitos.fi/geocoding/v2/pelias/search`;

    fetch(`${geocodingUrl}?text=${encodeURIComponent(query)}&api-key=${apiKey}`)
        .then(response => {
            if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);
            return response.json();
        })
        .then(data => {
            if (data.features && data.features.length > 0) {
                const location = data.features[0];
                const coords = location.geometry.coordinates;
                map.setView([coords[1], coords[0]], 12);
            } else {
                console.log(`No location found for: ${query}`);
            }
        })
        .catch(error => console.error("Geocoding error:", error));
}
