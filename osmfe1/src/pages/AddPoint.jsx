import React, { useState, useEffect } from "react";
import Input from "../components/Input";
import Button from "../components/Button";
import BackArrow from "../components/BackArrow";


const AddPointPanel = ({ coord }) => {
    const [xCoord, setXCoord] = useState('');
    const [yCoord, setYCoord] = useState('');
    const [PointName, setName] = useState("");
    const [PointNumber, setNumber] = useState("");


    const addName = (e) => {
        setName(e.target.value);
    };
    const addNumber = (e) => {
        setNumber(e.target.value);
    };
    const Latitude = xCoord;
    const Longitude = yCoord;

    useEffect(() => {
        if (coord) {
            setXCoord(coord[0]);
            setYCoord(coord[1]);
        }
    }, [coord]);
    const addPoint = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch("https://localhost:7000/api/point", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ PointName, PointNumber, Latitude, Longitude }),
            });

            if (response.ok) {
                console.log("Add Point successfully!");
                console.log(response);

                return response;
            } else {
                console.error("Error add point:", response.statusText);
            }
            return response;
        } catch (error) {
            console.error("Error add point:", error.message);
        }
    };
    return (
        <div
            className="container-fluid d-flex justify-content-center align-items-center"
            style={{ height: "865px" }}
        >
            <div className="card" style={{ width: "800px", height: "500px" }}>
                <div className="card-header pt-4">
                    <div>
                        <BackArrow />
                    </div>
                    <h5 className="card-title pt-2">Add Point</h5>
                </div>
                <div className="card-body">
                    <div className="row mb-4">
                        <Input
                            labelFor="name"
                            labelText="Name"
                            inputType="text"
                            placeholder="Enter your location name"
                            onInputChange={addName}
                        />
                    </div>
                    <div className="row mb-4">
                        <Input
                            labelFor="number"
                            labelText="Number"
                            inputType="text"
                            placeholder="Enter your location number"
                            onInputChange={addNumber}
                                />
                    </div>
                    <div className="row mb-4">
                        <div className="col-6">
                            <Input
                                labelText="X Coordinate"
                                inputType="text"
                                placeholder="Enter your location x coordinate"
                                inputName="Xcoordinate"
                                value={xCoord} onChange={(e) => setXCoord(e.target.value)}
                                                          />
                        </div>
                        <div className="col-6">
                            <Input
                                labelText="Y Coordinate"
                                inputType="text"
                                placeholder="Enter your location y coordinate"
                                inputName="Ycoordinate"
                                value={yCoord} onChange={(e) => setYCoord(e.target.value)}
                                
                            />
                        </div>
                    </div>
                    <div className="d-flex justify-content-end">
                        <Button
                            buttontext="Add Point"
                            buttonStyle={{ width: "130px", height: "40px" }}
                            buttonclick={addPoint}
                        />
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AddPointPanel;
