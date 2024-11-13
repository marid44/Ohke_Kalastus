let map;

// Define the ETRS-TM35FIN projection (EPSG:3067)
const crs = new L.Proj.CRS('EPSG:3067',
    '+proj=utm +zone=35 +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs',
    {
        origin: [-548576, 8388608], // Adjust origin if needed
        resolutions: [
            8192, 4096, 2048, 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1, 0.5, 0.25, 0.125, 0.0625
        ],
        bounds: L.bounds([-548576, 6291456], [1548576, 8388608])
    }
);

function initializeMap(apiKey) {
    if (map) return; // Prevent reinitializing the map

    // Initialize the map centered over Finland
    map = L.map('map', {
        crs: crs,
        continuousWorld: true,
        worldCopyJump: false,
        maxZoom: 14,
        minZoom: 0
    }).setView([63.2467777, 25.9209164], 5);

    // Fetch TileJSON metadata
    fetch(`https://avoin-karttakuva.maanmittauslaitos.fi/vectortiles/tilejson/taustakartta/1.0.0/taustakartta/default/v20/ETRS-TM35FIN/tilejson.json?api-key=${apiKey}`)
        .then(response => response.json())
        .then(data => {
            const tileUrlTemplate = data.tiles[0]; // Use the tile URL template from TileJSON

            // Load vector tiles using L.vectorGrid.protobuf with correct alignment
            L.vectorGrid.protobuf(tileUrlTemplate, {
                vectorTileLayerStyles: {
                    symboli: { fillColor: "#ff7800", color: "#ff7800", weight: 1 },
                    hallintoalue: { color: "#00ff00", weight: 1, fillOpacity: 0.2 },
                    maasto_viiva: { color: "#666666", weight: 0.5 },
                    vesisto_alue: { color: "#3388ff", fillColor: "#3388ff", weight: 1, fillOpacity: 0.3 },
                    poi: { fillColor: "#000000", color: "#000000", weight: 1 },
                    rakennelma: { color: "#8b4513", fillColor: "#8b4513", weight: 1 },
                    maastoalue: { color: "#4CAF50", fillColor: "#4CAF50", weight: 1, fillOpacity: 0.3 },
                    liikenne: { color: "#FF0000", weight: 1 },
                    maankaytto: { color: "#FF00FF", weight: 1, fillOpacity: 0.2 },
                    vesisto_viiva: { color: "#00BFFF", weight: 1 },
                    maastoalue: { color: "#DAA520", weight: 1, fillOpacity: 0.3 },
                    korkeus: { color: "#A9A9A9", weight: 0.5 },
                    nimisto: { color: "#000000", weight: 1 },
                    maasto_piste: { color: "#FF4500", weight: 1 },
                    rakennus: { color: "#8B4513", fillColor: "#A0522D", weight: 1, fillOpacity: 0.5 },
                    alueraja: { color: "#FFD700", weight: 1 },
                    selite: { color: "#808080", weight: 1 }
                },
                maxZoom: data.maxzoom || 14,
                minZoom: data.minzoom || 0,
                interactive: true
            }).addTo(map);
        })
        .catch(error => console.error("Error fetching TileJSON metadata:", error));
}
