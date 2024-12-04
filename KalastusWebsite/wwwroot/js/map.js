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

async function initializeMap(apiKey, userId) {
    console.log("Map initialization started.");
    console.log("USER_ID received:", userId);

    // Initialize the map
    if (map) {
        map.eachLayer(layer => map.removeLayer(layer)); // Remove all layers
        map.remove(); // Remove map instance
        map = null;
        console.log("Existing map removed.");
    }

    map = L.map('map', {
        crs: crs,
        continuousWorld: true,
        worldCopyJump: false,
        maxZoom: 15,
        minZoom: 0
    });
    console.log("Map instance created.");

    const finlandBounds = [
        [59.5, 19.0],
        [70.1, 31.0]
    ];
    map.fitBounds(finlandBounds);
    console.log("Map bounds set.");

    const wmtsUrl = 'https://avoin-karttakuva.maanmittauslaitos.fi/avoin/wmts/1.0.0';
    riversLayer = L.tileLayer(`${wmtsUrl}/maastokartta/default/ETRS-TM35FIN/{z}/{y}/{x}.png?api-key=${apiKey}`, {
        attribution: '&copy; Maanmittauslaitos',
        maxZoom: 15,
        minZoom: 0,
        tileSize: 256,
        transparent: true
    });
    riversLayer.addTo(map);
    console.log("Rivers layer added to map.");

    if (userId > 0) {
        // Fetch and display markers for logged-in users
        const markers = await fetchMarkers(userId);
        markers.forEach(marker => {
            L.marker([marker.latitude, marker.longitude])
                .addTo(map)
                .bindPopup(`Marker ID: ${marker.id}`);
        });

        // Add click event to place markers
        console.log("Adding map click event listener for logged-in user.");
        map.on("click", async function (e) {
            const { lat, lng } = e.latlng;
            console.log(`Clicked at Latitude: ${lat}, Longitude: ${lng}`);
            await saveMarker(lat, lng, userId);
            await updateMarkerCount(userId);
        });
    } else {
        console.log("User is not logged in. Markers and click functionality are disabled.");
    }
}


function resetMarkerCount() {
    const markerCountElement = document.getElementById("markerCount");
    if (markerCountElement) {
        markerCountElement.innerText = 0;
    }
    console.log("Marker count reset.");
}



// Fetch markers from the backend for a specific user
async function fetchMarkers(userId) {
    if (!userId) {
        console.error("User ID is missing. Cannot fetch markers.");
        return [];
    }

    const response = await fetch(`/api/Marker?userId=${userId}`);
    if (response.ok) {
        const markers = await response.json();
        console.log("Fetched markers:", markers);
        return markers;
    } else {
        console.error("Failed to fetch markers:", response.statusText);
        return [];
    }
}

// Save a marker to the backend
async function saveMarker(latitude, longitude, userId) {
    if (!userId) {
        alert("Please log in to place a marker.");
        return;
    }

    // Check if the user has reached the 10-marker limit
    const markerCount = await fetch(`/api/Marker?userId=${userId}`)
        .then(res => res.ok ? res.json() : Promise.reject(res.statusText))
        .then(data => data.length)
        .catch(() => 0);

    if (markerCount >= 10) {
        alert("You can only place up to 10 markers.");
        console.warn("Marker limit reached for user:", userId);
        return;
    }

    const markerData = { latitude, longitude, userId };
    console.log("Sending marker data:", markerData);

    const response = await fetch("/api/Marker", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(markerData)
    });

    if (response.ok) {
        console.log("Marker saved successfully.");

        // Immediately add the new marker to the map
        L.marker([latitude, longitude])
            .addTo(map)
            .bindPopup(`Marker added at Latitude: ${latitude}, Longitude: ${longitude}`)
            .openPopup();

        // Optionally update the marker count
        await updateMarkerCount(userId);
    } else {
        const errorMessage = await response.text();
        console.error("Failed to save marker:", response.statusText, errorMessage);
    }
}


// Update the marker count displayed on the map
async function updateMarkerCount(userId) {
    if (!userId) {
        console.error("User ID is missing. Cannot update marker count.");
        return;
    }

    const currentMarkerCount = await fetch(`/api/Marker?userId=${userId}`)
        .then(res => res.ok ? res.json() : Promise.reject(res.statusText))
        .then(data => data.length)
        .catch(error => {
            console.error("Failed to fetch marker count:", error);
            return 0;
        });

    document.getElementById("markerCount").innerText = currentMarkerCount;
    console.log("Updated marker count:", currentMarkerCount);
}


function clearMap() {
    if (map) {
        map.eachLayer(layer => map.removeLayer(layer)); // Remove all layers
        map.remove(); // Destroy the map instance
        map = null; // Reset the map variable
        console.log("Map cleared.");
    }
}


// Search for a location and center the map there
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

// Toggle map layers (e.g., rivers, roads)
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
