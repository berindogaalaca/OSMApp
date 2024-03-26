import { useState, useEffect,useContext } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import { ModalContext } from '../context/modalProvider'; 
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { addData } from '../service/DeleteFetch';


const AddPoint = ({onHide, coordinate }) => {
    const [, setShowModal] = useState(false);
    const { isAddOpen, toggleAdd, toggleInteraction } = useContext(ModalContext);
    const queryClient = useQueryClient();

    const [formData, setFormData] = useState({
        pointName: '',
        pointNumber: '',
        latitude: '',
        longitude: ''
    });

    useEffect(() => {
        setFormData({
            pointName: formData.pointName,
            pointNumber: formData.pointNumber,
            latitude: coordinate ? coordinate[1] : formData.latitude,
            longitude: coordinate ? coordinate[0] : formData.longitude,
        });
    }, [coordinate]);

    const addPoint = async (e) => {
        e.preventDefault();
        try {
            await addData(formData);
            handleClose();
            queryClient.invalidateQueries('data');
        } catch (error) {
            console.error(error);
        }
    };

    const handleClose = () => {
        setShowModal(false);
        onHide && onHide();
        toggleInteraction()
    };
    return (
        <Modal
            show={isAddOpen} onHide={() => toggleAdd()}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Add Point
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
                            value={formData.pointName} // value �zelli�i formData'n�n pointName alan�na ba�lan�r
                            onChange={(e) => setFormData({ ...formData, pointName: e.target.value })} // onChange ile formData'n�n pointName alan� g�ncellenir
                        />
                        <Form.Label>Number</Form.Label>
                        <Form.Control
                            className='mb-2'
                            type="number"
                            placeholder="Add Your Coordinate Number"
                            value={formData.pointNumber} // value �zelli�i formData'n�n pointNumber alan�na ba�lan�r
                            onChange={(e) => setFormData({ ...formData, pointNumber: e.target.value })} // onChange ile formData'n�n pointNumber alan� g�ncellenir
                        />
                        <Form.Label>Latitude</Form.Label>
                        <Form.Control
                            className='mb-2'
                            type="number"
                            placeholder="Add Your Latitude"
                            value={formData.latitude} // value �zelli�i formData'n�n latitude alan�na ba�lan�r
                            onChange={(e) => setFormData({ ...formData, latitude: e.target.value })} // onChange ile formData'n�n latitude alan� g�ncellenir
                            readOnly
                        />
                        <Form.Label>Longitude</Form.Label>
                        <Form.Control
                            type="number"
                            placeholder="Add Your Longitude"
                            value={formData.longitude} // value �zelli�i formData'n�n longitude alan�na ba�lan�r
                            onChange={(e) => setFormData({ ...formData, longitude: e.target.value })} // onChange ile formData'n�n longitude alan� g�ncellenir
                            readOnly
                        />
                    </Form.Group>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button className='bg-black border-0' onClick={addPoint}>Save</Button>
                <ToastContainer />
                <Button className='bg-black border-0' onClick={handleClose}>Close</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default AddPoint;