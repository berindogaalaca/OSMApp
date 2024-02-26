import React from "react";

const ListItem = ({ item, onClick, isSelected }) => {
    return (
        <tr
            onClick={() => onClick(item)}
            style={{
                cursor: "pointer",
                backgroundColor: isSelected ? "#f0f0f0" : "transparent",
            }}
        >
            <td>{item.pointName}</td>
            <td>{item.pointNumber}</td>
            <td>{item.latitude}</td>
            <td>{item.longitude}</td>
        </tr>
    );
};

export default ListItem;
