import React, { useState, useRef, useEffect, useContext } from 'react';
import Form from "react-bootstrap/Form";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import ScrollLabelList from "./ScrollLableList";
import { ToastContainer } from 'react-toastify';
import { useQuery } from "@tanstack/react-query";
import { fetchData } from "../service/DeleteFetch";
import { ModalContext } from '../context/modalProvider'; 


function MyVerticallyCenteredModal(props) {
    const [xCoord, setXCoord] = useState("");
    const [yCoord, setYCoord] = useState("");
    const [PointName, setName] = useState("");
    const [PointNumber, setNumber] = useState("");

    const { isQueryOpen, toggleQuery

         } = useContext(ModalContext);

    const [selectedItem, setSelectedItem] = useState(null);
    const [filteredItems, setFilteredItems] = useState([]);

    const nameRef = useRef(null);
    const numberRef = useRef(null);

    const latitude1Ref = useRef(null);
    const longitude1Ref = useRef(null);


    const addName = (e) => {
        setName(e.target.value);
    };
    const addNumber = (e) => {
        setNumber(e.target.value);
    };
    const addX = (e) => {
        setXCoord(e.target.value);
    };
    const addY = (e) => {
        setYCoord(e.target.value);
    };

    const handleItemClick = (item) => {
        setSelectedItem(item);
    };

    const { data, isError, isLoading } = useQuery({
        queryKey: ['Point', PointName, PointNumber, xCoord, yCoord],
        queryFn: () => fetchData(PointName, PointNumber, xCoord, yCoord),
    });

    //useEffect(() => {
    //    if (data) {
    //        const valuesArray = Object.keys(data).map(key => data[key]);
    //        setFilteredItems([...valuesArray]);
    //    }
    //}, [data]);

    if (isLoading) return <div>Loading...</div>;
    if (isError) return <div>Error fetching data</div>;

    const handleDeleteButtonClick = () => {
        setName("");
        setNumber("");
        setXCoord("");
        setYCoord("");
    };


    return (
        <Modal
            {...props}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
           
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Query Point{" "}
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form>
                    <Form.Group
                        className="mb-3 d-flex justify-content-between"
                        controlId="exampleForm.ControlInput1"
                    >
                        <div className="mx-2">
                            <Form.Label>Name</Form.Label>
                            <Form.Control
                                className="mx-2"
                                type="text"
                                placeholder="Filter Your Coordinate Name"
                                onChange={addName}
                                value={PointName}
                                ref={nameRef}
                            />
                        </div>
                        <div className="mx-2">
                            <Form.Label>Number</Form.Label>
                            <Form.Control
                                className="mx-2"
                                type="text"
                                placeholder="Filter Your Coordinate Number"
                                autoFocus
                                onChange={addNumber}
                                value={PointNumber}
                                ref={numberRef}
                            />
                        </div>
                        <div className="mx-2">
                            <Form.Label>Latitude</Form.Label>
                            <Form.Control
                                className="mb-2"
                                type="text"
                                placeholder="Filter Your Latitude"
                                autoFocus
                                onChange={addX}
                                value={xCoord}
                                ref={latitude1Ref}
                            />
                        </div>
                        <div className="mx-2">
                            <Form.Label>Longitude</Form.Label>
                            <Form.Control
                                type="text"
                                placeholder="Filter Your Longitude"
                                autoFocus
                                onChange={addY}
                                value={yCoord}
                                ref={longitude1Ref}
                            />
                        </div>
                    </Form.Group>
                    <div className="mb-3 d-flex justify-content-end">
                        <Button className="mx-3 border-0 bg-black">Filter</Button>
                        <Button onClick={handleDeleteButtonClick} className="bg-black border-0">Delete</Button>
                    </div>
                </Form>
                <div className="row">

                    {PointName || PointNumber || xCoord || yCoord ? (

                        <ScrollLabelList
                            items={data}
                            selectedItem={selectedItem}
                            handleItemClick={handleItemClick}
                        />
                    ) : (
                        <ScrollLabelList
                            items={data}
                            selectedItem={selectedItem}
                            handleItemClick={handleItemClick}
                        />
                    )}

                </div>
            </Modal.Body>
            <Modal.Footer>
                <ToastContainer />
                <Button className="mx-3 bg-black border-0" onClick={toggleQuery}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

const QueryPoint = ({ show, onHide }) => {
    return <MyVerticallyCenteredModal show={show} onHide={onHide} />;
};

export default QueryPoint;