import React, { useRef, useEffect, useState } from 'react';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import TileLayer from 'ol/layer/Tile';
import XYZ from 'ol/source/XYZ';
import { Circle as CircleStyle, Fill, Stroke, Style, Icon } from 'ol/style'; 
import Draw from 'ol/interaction/Draw';
import { fromLonLat } from 'ol/proj';
import { Vector as VectorLayer } from 'ol/layer';
import { Vector as VectorSource } from 'ol/source';
import AddPoint from "./AddPoint";

const MapComponent = () => {
  const mapRef = useRef(null);
  const drawInteraction = useRef(null);
  const mapInstance = useRef(null);
  const [showAddPointModal, setShowAddPointModal] = useState(false);
  const [coordinate, setCoordinate] = useState(null);

  useEffect(() => {
    if (!mapRef.current) return;

    const source = new XYZ({
      url: 'https://{a-c}.tile.openstreetmap.org/{z}/{x}/{y}.png'
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

    const locationIconStyle = new Style({
      image: new Icon({
        src: "locationicon.svg",
        imgSize: [32, 32], 
        anchor: [0.5, 1],
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

    drawInteraction.current = new Draw({
      source: vectorLayer.getSource(),
      type: 'Point',
    });

    const activateInteraction = () => {
      if (drawInteraction.current) {
        map.addInteraction(drawInteraction.current);
        drawInteraction.current.on('drawend', (event) => {
          const feature = event.feature;
          feature.setStyle(locationIconStyle); 
          const coords = feature.getGeometry().getCoordinates();
          setCoordinate(coords);
          setShowAddPointModal(true);
        });
      } else {
        console.error("Etkileşim henüz oluşturulmadı.");
      }
    };

    const deactivateInteraction = () => {
      if (drawInteraction.current) {
        map.removeInteraction(drawInteraction.current);
      }
    };
    MapComponent.activateInteraction = activateInteraction;
    MapComponent.deactivateInteraction = deactivateInteraction;

    mapInstance.current = map;

    return () => {
      if (map && drawInteraction.current) {
        map.removeInteraction(drawInteraction.current);
      }
      if (mapInstance.current === map) {
        map.setTarget(null);
      }
    };
  }, []);

  return (
    <>
      <div ref={mapRef} className="map" style={{ width: '100%', height: '91vh' }}></div>
      <AddPoint show={showAddPointModal} onHide={() => setShowAddPointModal(false)} coordinate={coordinate} deactivateInteraction={MapComponent.deactivateInteraction}/>
    </>
  );
};

export default MapComponent;
