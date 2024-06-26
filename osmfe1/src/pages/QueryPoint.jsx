import React, { useState, useEffect, useRef } from "react";
import Input from "../components/Input";
import Button from "../components/Button";
import ScrollableList from "../components/ScrollableList";
import BackArrow from "../components/BackArrow";

const QueryPoint = ({ coord }) => {
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
        if (coord) {
            setXCoord(coord[0]);
            setYCoord(coord[1]);
        }
    }, [coord]);

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
                console.error("Error query Point:", response.statusText);
            }
        } catch (error) {
            console.error("Error query Point:", error.message);
        }
    };

    const handleDeleteButtonClick = () => {
        setName("");
        setNumber("");
        setXCoord("");
        setYCoord("");
    };

    return (
        <div className="container-fluid d-flex justify-content-center align-items-center" style={{ height: "865px" }}>
            <div className="card" style={{ width: "800px", height: "550px" }}>
                <div className="card-header pt-4">
                    <div>
                        <BackArrow />
                    </div>
                    <h5 className="card-title pt-2">Query Point</h5>
                </div>
                <div className="card-body">
                    <div className="row d-flex">
                        <div className="col-3 mb-4">
                            <Input
                                labelText="Name"
                                inputType="text"
                                placeholder="Enter your location name"
                                inputName="Name"
                                onInputChange={addName}
                                value={PointName}
                                ref={nameRef}
                            />
                        </div>
                        <div className="col-3 mb-4">
                            <Input
                                labelText="Number"
                                inputType="text"
                                placeholder="Enter your location number"
                                inputName="Number"
                                onInputChange={addNumber}
                                value={PointNumber}
                                ref={numberRef}
                            />
                        </div>
                        <div className="col-3">
                            <Input
                                labelText="X Coordinate"
                                inputType="text"
                                placeholder="Enter your location x coordinate"
                                inputName="Xcoordinate"
                                onInputChange={addX}
                                value={xCoord}
                                ref={latitude1Ref}
                            />
                        </div>
                        <div className="col-3">
                            <Input
                                labelText="Y Coordinate"
                                inputType="text"
                                placeholder="Enter your location y coordinate"
                                inputName="Ycoordinate"
                                onInputChange={addY}
                                value={yCoord}
                                ref={longitude1Ref}
                            />
                        </div>
                    </div>
                    <div className="row d-flex justify-content-end ">
                        <div className="col-6 px-0">
                            <Button
                                buttontext="Filter"
                                buttonStyle={{ width: "130px", height: "40px" }}
                                buttonclick={queryPoint}
                            />

                            <Button
                                buttontext="Delete"
                                buttonStyle={{ width: "130px", height: "40px" }}
                                buttonclick={handleDeleteButtonClick}
                            />
                        </div>
                    </div>
                    <div className="row">
                        {PointName || PointNumber || xCoord || yCoord ? (
                            <ScrollableList
                                items={filteredItems}
                                selectedItem={selectedItem}
                                handleItemClick={handleItemClick}
                            />
                        ) : (
                            <ScrollableList
                                items={filteredItems[0]}
                                selectedItem={selectedItem}
                                handleItemClick={handleItemClick}
                            />
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default QueryPoint;
