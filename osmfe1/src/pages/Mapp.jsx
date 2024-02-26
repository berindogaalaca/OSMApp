import React, { useRef, useEffect } from 'react';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import TileLayer from 'ol/layer/Tile';
import XYZ from 'ol/source/XYZ';
import MousePosition from 'ol/control/MousePosition';
import { createStringXY } from 'ol/coordinate';
import { fromLonLat } from 'ol/proj';
import { Feature } from 'ol';
import { Point } from 'ol/geom';
import { Vector as VectorLayer } from 'ol/layer';
import { Vector as VectorSource } from 'ol/source';
import { Icon, Style } from 'ol/style';

const MapComponent = ({ onCoordinateClick }) => {
    const mapRef = useRef(null);

    useEffect(() => {
        const mousePositionControl = new MousePosition({
            coordinateFormat: createStringXY(4),
            projection: 'EPSG:4326',
            target: document.getElementById('mouse-position'),
            undefinedHTML: '&nbsp;'
        });

        const map = new Map({
            target: mapRef.current,
            layers: [
                new TileLayer({
                    source: new XYZ({
                        url: 'https://{a-c}.tile.openstreetmap.org/{z}/{x}/{y}.png'
                    })
                })
            ],
            view: new View({
                center: fromLonLat([35.0, 38.0]),
                zoom: 6.5
            }),
            controls: [],
        });

        map.addControl(mousePositionControl);

        const handleMapClick = (event) => {
            const clickedCoord = event.coordinate;
            onCoordinateClick(clickedCoord)
            addLocationIconToMap(map, clickedCoord);
        };

        map.on('click', handleMapClick);

        fetchCoordinatesFromDatabase(map);

        return () => {
            map.removeControl(mousePositionControl);
            map.un('click', handleMapClick);
            map.dispose();
        };
    }, []);

    const fetchCoordinatesFromDatabase = async (map) => {
        try {
            const response = await fetch('https://localhost:7000/api/point?PointName=&PointNumber=&Latitude=&Longitude=');
            if (!response.ok) {
                throw new Error('Failed to fetch coordinates from the database');
            }
            const data = await response.json();
            const valuesArray = Object.keys(data).map(key => data[key]);
            valuesArray[0].forEach(coordinate => {
                const { latitude, longitude } = coordinate;
                console.log("cor"+coordinate);
                const coord = [latitude,longitude ];
                addLocationIconToMap(map, coord);
                console.log("coord"+coord);
              
            });
        } catch (error) {
            console.error('Error fetching coordinates from the database:', error);
        }
    };

    const addLocationIconToMap = (map, coord) => {
        const iconStyle = new Style({
            image: new Icon({
                src: "locationicon.svg", 
                imgSize: [32, 32], 
                anchor: [0.5, 1], 
            })
        });

        const locationLayer = new VectorLayer({
            source: new VectorSource({
                features: [
                    new Feature({
                        geometry: new Point(coord)
                    })
                ]
            }),
            style: iconStyle
        });

        map.addLayer(locationLayer);
    };

    return (
        <div>
            <div id="mouse-position" className="mouse-position"></div>
            <div ref={mapRef} className="map" style={{ width: '100%', height: '900px' }}></div>
        </div>
    );
};

export default MapComponent;
