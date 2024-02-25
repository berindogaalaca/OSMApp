import React, { useState } from "react";

const ScrollableList = () => {
  const items = [
    {
      pointName: "test",
      pointNumber: "test",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test2",
      pointNumber: "test2",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test3",
      pointNumber: "test3",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test4",
      pointNumber: "test4",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test5",
      pointNumber: "test5",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test6",
      pointNumber: "test6",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test7",
      pointNumber: "test7",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test8",
      pointNumber: "test8",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test9",
      pointNumber: "test9",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test10",
      pointNumber: "test10",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test11",
      pointNumber: "test11",
      latitude: "123",
      longitude: "456",
    },
    {
      pointName: "test12",
      pointNumber: "test12",
      latitude: "123",
      longitude: "456",
    },
  ];
  const [selectedItem, setSelectedItem] = useState(null);

  const handleItemClick = (item) => {
    setSelectedItem(item);
  };

  return (
    <div
      className="mt-3 ms-1 row"
      style={{
        overflowY: "auto",
        maxHeight: "280px",
        border: "2px solid #ccc",
      }}
    >
      <table style={{ borderCollapse: "collapse", width: "100%" }}>
        <thead>
          <tr>
            <th
              style={{
                position: "sticky",
                top: "0",
                backgroundColor: "#f0f0f0",
              }}
            >
              Point Name
            </th>
            <th
              style={{
                position: "sticky",
                top: "0",
                backgroundColor: "#f0f0f0",
              }}
            >
              Point Number
            </th>
            <th
              style={{
                position: "sticky",
                top: "0",
                backgroundColor: "#f0f0f0",
              }}
            >
              Point X Coordinate
            </th>
            <th
              style={{
                position: "sticky",
                top: "0",
                backgroundColor: "#f0f0f0",
              }}
            >
              Point Y Coordinate
            </th>
          </tr>
        </thead>
        <tbody>
          {items.map((item, index) => (
            <tr
              key={index}
              onClick={() => handleItemClick(item)} 
              style={{
                cursor: "pointer",
                backgroundColor:
                  selectedItem === item ? "#f0f0f0" : "transparent",
              }}
            >
              <td>{item.pointName}</td>
              <td>{item.pointNumber}</td>
              <td>{item.latitude}</td>
              <td>{item.longitude}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ScrollableList;