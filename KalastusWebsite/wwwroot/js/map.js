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
    console.log("Map initialization started."); // Debug message

    if (map) {
        map.eachLayer(layer => map.removeLayer(layer)); // Clear all layers
        map.remove(); // Destroy the existing map instance
        map = null;
        console.log("Existing map removed."); // Debug message
    }

    // Initialize the map
    map = L.map('map', {
        crs: crs,
        continuousWorld: true,
        worldCopyJump: false,
        maxZoom: 15,
        minZoom: 0
    });
    console.log("Map instance created."); // Debug message

    const finlandBounds = [
        [59.5, 19.0],
        [70.1, 31.0]
    ];
    map.fitBounds(finlandBounds);
    console.log("Map bounds set."); // Debug message

    const wmtsUrl = 'https://avoin-karttakuva.maanmittauslaitos.fi/avoin/wmts/1.0.0';
    riversLayer = L.tileLayer(`${wmtsUrl}/maastokartta/default/ETRS-TM35FIN/{z}/{y}/{x}.png?api-key=${apiKey}`, {
        attribution: '&copy; Maanmittauslaitos',
        maxZoom: 15,
        minZoom: 0,
        tileSize: 256,
        transparent: true
    });
    riversLayer.addTo(map);
    console.log("Rivers layer added to map."); // Debug message

    // Test map click event
    map.on('click', function (e) {
        console.log("Map clicked."); // Debug message
        const { lat, lng } = e.latlng;
        console.log(`Clicked coordinates: Latitude = ${lat}, Longitude = ${lng}`); // Debug coordinates

        // Place a marker
        const marker = L.marker([lat, lng]).addTo(map);
        console.log("Marker placed on map."); // Debug marker placement
    });
}



async function fetchMarkers() {
    const userId = USER_ID; // Replace with the logged-in user's ID passed from Blazor
    if (!userId) return [];

    const response = await fetch(`/api/Marker?userId=${userId}`);
    if (response.ok) {
        return await response.json();
    } else {
        console.error("Failed to fetch markers:", response.statusText);
        return [];
    }
}


function toggleLayer(layerName, isVisible) {
    const layer = layerName === "rivers" ? riversLayer :
        layerName === "roads" ? roadsLayer : null;

    if (!layer) {
        console.warn(`Layer "${layerName}" not found.`);
        return;
    }

    if (isVisible) {
        layer.addTo(map);
    } else {
        map.removeLayer(layer);
    }
}

function searchLocation(query, apiKey) {
    if (!query || !apiKey) {
        console.warn("Invalid search query or missing API key.");
        return;
    }

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
                map.setView([coords[1], coords[0]], 12); // Center map to the location
                L.marker([coords[1], coords[0]])
                    .addTo(map)
                    .bindPopup(location.properties.label)
                    .openPopup();
            } else {
                alert(`No location found for: ${query}`);
            }
        })
        .catch(error => console.error("Geocoding error:", error));
}

async function saveMarker(latitude, longitude) {
    const userId = USER_ID; // Replace with the logged-in user's ID passed from Blazor
    if (!userId) {
        alert("You must be logged in to place a marker.");
        return;
    }

    const markerData = { latitude, longitude, userId };
    const response = await fetch("/api/Marker", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(markerData)
    });

    if (response.ok) {
        console.log("Marker saved successfully.");
    } else {
        console.error("Failed to save marker:", response.statusText);
    }
}

