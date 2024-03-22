import React, { useState, useEffect, useRef } from "react";
import Button from "react-bootstrap/Button";
import Modal from "react-bootstrap/Modal";
import Form from "react-bootstrap/Form";
import ScrollLabelList from "./ScrollLableList";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';

function MyVerticallyCenteredModal(props) {
    const [xCoord, setXCoord] = useState("");
    const [yCoord, setYCoord] = useState("");
    const [PointName, setName] = useState("");
    const [PointNumber, setNumber] = useState("");
    const [filteredItems, setFilteredItems] = useState([]);
    const [selectedItem, setSelectedItem] = useState(null);

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
    const Latitude = xCoord;
    const Longitude = yCoord;

    const addX = (e) => {
        setXCoord(e.target.value);
    };
    const addY = (e) => {
        setYCoord(e.target.value);
    };


    useEffect(() => {
        console.log("Filtered items:", filteredItems[0]);
    }, [filteredItems]);

    const handleItemClick = (item) => {
        setSelectedItem(item);
    };

    const queryPoint = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(
                `https://localhost:7000/api/point?PointName=${PointName}&PointNumber=${PointNumber}&Latitude=${Latitude}&Longitude=${Longitude}`,
                {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                    },
                }
            );

            if (response.ok) {
                console.log("Query Point successfully!");
                const data = await response.json();
                const valuesArray = Object.keys(data).map(key => data[key]);

                setFilteredItems([...valuesArray]);
            } else {
                toast.error(response.statusText, {
                    position: "top-right",
                    autoClose: 5000,
                    hideProgressBar: false,
                    closeOnClick: true,
                    pauseOnHover: true,
                    draggable: true,
                    progress: undefined,
                });
            }
        } catch (error) {
            toast.error(error.message, {
                position: "top-right",
                autoClose: 5000,
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
            });
        }
    };

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
                      <Button className="mx-3 border-0 bg-black" onClick={queryPoint }>Filter</Button>
                      <Button onClick={handleDeleteButtonClick } className="bg-black border-0">Delete</Button>
          </div>
              </Form>
              <div className="row">
                  {PointName || PointNumber || xCoord || yCoord ? (
                     
                      <ScrollLabelList
                          items={filteredItems}
                          selectedItem={selectedItem}
                          handleItemClick={handleItemClick}
                      />
                  ) : (
                          <ScrollLabelList
                          items={filteredItems[0]}
                          selectedItem={selectedItem}
                          handleItemClick={handleItemClick}
                      />
                  )}
              </div>
      
      </Modal.Body>
          <Modal.Footer>
              <ToastContainer />

        <Button className="mx-3 bg-black border-0" onClick={props.onHide}>
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
