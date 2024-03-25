import React, { useRef, useEffect, useState, useContext } from 'react';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import TileLayer from 'ol/layer/Tile';
import XYZ from 'ol/source/XYZ';
import { Circle as CircleStyle, Fill, Stroke, Style, Icon } from 'ol/style';
import { fromLonLat } from 'ol/proj';
import { Vector as VectorLayer } from 'ol/layer';
import { Vector as VectorSource } from 'ol/source';
import Draw from 'ol/interaction/Draw';
import Modify from 'ol/interaction/Modify';
import AddPoint from './AddPoint';
import ModifyChange from './Modify';
import { ModalContext } from '../context/modalProvider';
import Feature from 'ol/Feature';
import Point from 'ol/geom/Point';
import { useQuery } from "@tanstack/react-query";
import { fetchData } from "../service/DeleteFetch";

const MapComponent = () => {
    const mapRef = useRef(null);
    const mapInstance = useRef(null);
    const [coordinate, setCoordinate] = useState(null);
    const { isAddOpen, toggleAdd, isInteractionOpen, toggleInteraction, isDrawInteractionOpen,
        toggleDrawInteraction,toggleModifyOpen } = useContext(ModalContext);
    const locationIconStyle = new Style({
        image: new Icon({
            src: 'locationicon.svg',
            imgSize: [32, 32],
            anchor: [0.5, 1],
        }),
    });

    const { data, isError, isLoading } = useQuery({
        queryKey: ['Point', "", "", "", ""],
        queryFn: () => fetchData("", "", "", ""),
    });


    useEffect(() => {
        if (isLoading) return;
        if (isError) return;

        if (!mapInstance.current) return;

        const vectorLayer = mapInstance.current?.getLayers().getArray().find(layer => layer instanceof VectorLayer);
        if (!vectorLayer) return;
        const valuesArray = Object.keys(data).map(key => data[key]);
        valuesArray[0].forEach(coordinate => {
            const { latitude, longitude } = coordinate;
            const coord = [longitude, latitude];
            const iconFeature = new Feature({ geometry: new Point(coord) });
            iconFeature.setStyle(locationIconStyle);
            vectorLayer.getSource().addFeature(iconFeature);
        });
    }, [isLoading, isError, locationIconStyle]);

    useEffect(() => {
        if (!mapRef.current) return;

        const source = new XYZ({
            url: 'https://{a-c}.tile.openstreetmap.org/{z}/{x}/{y}.png',
        });

        const vectorLayer = new VectorLayer({
            source: new VectorSource(),
            style: new Style({
                fill: new Fill({
                    color: 'rgba(255, 255, 255, 0.2)',
                }),
                stroke: new Stroke({
                    color: '#ffcc33',
                    width: 2,
                }),
                image: new CircleStyle({
                    radius: 7,
                    fill: new Fill({
                        color: '#ffcc33',
                    }),
                }),
            }),
        });

        const map = new Map({
            target: mapRef.current,
            layers: [
                new TileLayer({
                    source: source,
                }),
                vectorLayer,
            ],
            view: new View({
                center: fromLonLat([35.0, 38.0]),
                zoom: 6,
            }),
        });

        const draw = new Draw({
            source: vectorLayer.getSource(),
            type: 'Point',
            active: false, 
        });

        const modify = new Modify({
            source: vectorLayer.getSource(),
            active: false,
        });

        map.addInteraction(draw);
        map.addInteraction(modify);

        draw.on('drawend', (event) => {
            const feature = event.feature;
            feature.setStyle(locationIconStyle);
            const coords = feature.getGeometry().getCoordinates();
            setCoordinate(coords);
            toggleAdd(true);
        });

        modify.on('modifyend', (event) => {
            const feature = event.features.item(0); // ilk öğeyi alır
            if (feature) {
                feature.setStyle(locationIconStyle);
                const coords = feature.getGeometry().getCoordinates();
                setCoordinate(coords);
                toggleModifyOpen(true); // Modify açılsın
            }
        });


        mapInstance.current = map;

        return () => {
            if (map && draw) {
                map.removeInteraction(draw);
            }
            if (map && modify) {
                map.removeInteraction(modify);
            }
            if (mapInstance.current === map) {
                map.setTarget(null);
            }
        };
    }, []);

    useEffect(() => {
        const drawInteraction = mapInstance.current?.getInteractions().getArray().find(interaction => interaction instanceof Draw);
        if (drawInteraction) {
            drawInteraction.setActive(isInteractionOpen);
        }
    }, [isInteractionOpen]);
    useEffect(() => {
        const modifyInteraction = mapInstance.current?.getInteractions().getArray().find(interaction => interaction instanceof Modify);
        if (modifyInteraction) {
            modifyInteraction.setActive(isDrawInteractionOpen);
        }
    }, [isDrawInteractionOpen]);
    return (
        <>
            <div ref={mapRef} className="map" style={{ width: '100%', height: '91vh' }}></div>
            <AddPoint show={isAddOpen} onHide={() => toggleAdd(false)} coordinate={coordinate} onClick={toggleInteraction} />
            <ModifyChange coordinate={coordinate} onClick={toggleDrawInteraction} />
        </>
    );
};

export default MapComponent;