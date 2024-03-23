import React, { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';


const AddPoint = ({ show, onHide, coordinate, deactivateInteraction }) => {
    const [, setShowModal] = useState(false);

    const [PointName, setName] = useState("");
    const [PointNumber, setNumber] = useState("");
    const [latitude, setLatitude] = useState('');
    const [longitude, setLongitude] = useState('');

    const addName = (e) => {
        setName(e.target.value);
    };
    const addNumber = (e) => {
        setNumber(e.target.value);
    };

  

    useEffect(() => {
        if (coordinate) {
            const [lon, lat] = coordinate;
            setLatitude(lat);
            setLongitude(lon);
        }
    }, [coordinate]);

    const handleClose = () => {
        setShowModal(false);
        onHide && onHide();

    };

   

    return (
        <Modal
            show={show}
            onHide={onHide}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Modify
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
                        <Form.Label>Name</Form.Label>
                        <Form.Control
                            className='mb-2'
                            type="text"
                            placeholder="Add Your Coordinate Name"
                            autoFocus
                            onChange={addName}
                        />
                        <Form.Label>Number</Form.Label>
                        <Form.Control
                            className='mb-2'
                            type="number"
                            placeholder="Add Your Coordinate Number"
                            onChange={addNumber}
                        />
                        <Form.Label>Latitude</Form.Label>
                        <Form.Control
                            className='mb-2'
                            type="number"
                            placeholder="Add Your Latitude"
                            value={latitude}
                            onChange={(e) => setLatitude(e.target.value)}
                            readOnly
                        />
                        <Form.Label>Longitude</Form.Label>
                        <Form.Control
                            type="number"
                            placeholder="Add Your Longitude"
                            value={longitude}
                            onChange={(e) => setLongitude(e.target.value)}
                            readOnly
                        />
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button className='bg-black border-0' >Save</Button>
                <Button className='bg-black border-0' >Change Coordinates</Button>
                <ToastContainer />
                <Button className='bg-black border-0' onClick={handleClose}>Close</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default AddPoint;
