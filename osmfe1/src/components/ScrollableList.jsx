import React from "react";

const ScrollableList = ({ items, selectedItem, handleItemClick }) => {
    return (
        <div
            className="mt-3 ms-1 row"
            style={{
                overflowY: "auto",
                maxHeight: "280px",
                border: "2px solid #ccc",
            }}
        >
            {items && items.length > 0 ? ( 
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
                                        selectedItem && selectedItem === item ? "#f0f0f0" : "transparent",
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
            ) : (
                // items tanýmlý deðilse veya boþsa bu durumu iþle
                <div>No items to display</div>
            )}
        </div>
    );
};

export default ScrollableList;
