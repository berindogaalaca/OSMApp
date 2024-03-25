import React, { useState, useEffect, useContext } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { ModalContext } from '../context/modalProvider';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { updateData } from '../service/DeleteFetch';

const Modify = ({ onClick, coordinate }) => {
    const queryClient = useQueryClient();
    const { toggleModify, isModifyOpen, selectedPoint } = useContext(ModalContext);


    const [formData, setFormData] = useState({
        pointName: '',
        pointNumber: '',
        latitude: '',
        longitude:''
    });
    console.log(coordinate)
    useEffect(() => {
        setFormData({
            pointName: selectedPoint?.pointName,
            pointNumber: selectedPoint?.pointNumber,
            latitude: coordinate ? coordinate[1] : selectedPoint?.latitude,
            longitude: coordinate ? coordinate[0] : selectedPoint?.longitude,
        });
    }, [selectedPoint,coordinate]);

    const mutation = useMutation({
        mutationFn: updateData,
        mutationKey: 'put',
        onSuccess: () => {
            queryClient.invalidateQueries('data');
            toast.success('Point updated successfully');
        },
        onError: (error) => {
            toast.error('Error updating point: ' + error.message);
        },
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const updateHandler = () => {
        const updatedPoint = {
            ...selectedPoint,
            ...formData,
        };
        mutation.mutate(updatedPoint);
    };
   
    return (
        <Modal
            show={isModifyOpen}
            onHide={toggleModify}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">Modify</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group className="mb-3" controlId="exampleForm.ControlInput1">
                        <Form.Label>Name</Form.Label>
                        <Form.Control
                            className='mb-2'
                            type="text"
                            name="pointName"
                            placeholder="Add Your Coordinate Name"
                            autoFocus
                            value={formData?.pointName}
                            onChange={handleInputChange}
                        />
                        <Form.Label>Number</Form.Label>
                        <Form.Control
                            className='mb-2'
                            type="number"
                            name="pointNumber"
                            value={formData?.pointNumber}
                            placeholder="Add Your Coordinate Number"
                            onChange={handleInputChange}
                        />
                        <Form.Label>Latitude</Form.Label>
                        <Form.Control
                            disabled
                            className='mb-2'
                            type="number"
                            name="latitude"
                            value={formData?.latitude}
                            placeholder="Add Your Latitude"
                            onChange={handleInputChange}
                        />
                        <Form.Label>Longitude</Form.Label>
                        <Form.Control
                            disabled
                            type="number"
                            name="longitude"
                            value={formData?.longitude}
                            placeholder="Add Your Longitude"
                            onChange={handleInputChange}
                        />
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button className='bg-black border-0' onClick={updateHandler}>Save</Button>
                <Button className='bg-black border-0' onClick={onClick}>Change Coordinates</Button>
                <ToastContainer />
            </Modal.Footer>
        </Modal>
    );
};

export default Modify;
