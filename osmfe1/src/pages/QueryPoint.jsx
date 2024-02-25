import React from "react";
import Input from "../components/Input";
import Button from "../components/Button";
import ScrollableList from "../components/ScrollableList";
import BackArrow from "../components/BackArrow";

const QueryPoint = () => {
  return (
    <div
      className="container-fluid d-flex justify-content-center align-items-center"
      style={{ height: "865px" }}
    >
      <div class="card" style={{ width: "800px", height: "550px" }}>
        <div className="card-header pt-4">
          <div>
            <BackArrow />
          </div>
          <h5 className="card-title pt-2">Add Point</h5>
        </div>
        <div class="card-body">
          <div className="row d-flex">
            <div className="col-3 mb-4">
              <Input
                labelText="Name"
                inputType="text"
                placeholder="Enter your location name"
                inputName="Name"
              />
            </div>
            <div className="col-3 mb-4">
              <Input
                labelText="Number"
                inputType="text"
                placeholder="Enter your location number"
                inputName="Number"
              />
            </div>
            <div className="col-3">
              <Input
                labelText="X Coordinate"
                inputType="text"
                placeholder="Enter your location x coordinate"
                inputName="Xcoordinate"
              />
            </div>
            <div className="col-3">
              <Input
                labelText="Y Coordinate"
                inputType="text"
                placeholder="Enter your location y coordinate"
                inputName="Ycoordinate"
              />
            </div>
          </div>
          <div className="row d-flex justify-content-end ">
            <div className="col-6 px-0">
              <Button
                buttontext="Filter"
                buttonStyle={{ width: "130px", height: "40px" }}
              />

              <Button
                buttontext="Delete"
                buttonStyle={{ width: "130px", height: "40px" }}
              />
            </div>
          </div>
          <div className="row">
            <ScrollableList />
          </div>
        </div>
      </div>
    </div>
  );
};

export default QueryPoint;