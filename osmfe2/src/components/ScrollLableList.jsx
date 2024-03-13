import React from "react"
import Table from "react-bootstrap/Table";


const ScrollLabelList = ({ items, selectedItem, handleItemClck }) => {

    return (<div style={{ overflowY: "auto", maxHeight: "280px" }}>
        {items && items.length > 0 ? (
            <Table responsive="sm">
                <thead>
                    <tr>
                        
                        <th>Name</th>
                        <th>Number</th>
                        <th>Latitude</th>
                        <th>Longitude</th>
                    </tr>
                </thead>
                <tbody>
                    {items.map((item, index) => (
                        <tr key={index} onClick={() => handleItemClck(item)} style
=                            {{
                            cursor: "pointer",
                            backgroundColor:selectedItem&&selectedItem===item?"black":"transparent",
                        }} >

                            <td>{item.pointName}</td>
                            <td>{item.pointNumber}</td>
                            <td>{item.latitude}</td>
                            <td>{item.longitude}</td>
                        </tr>

                    ))}


                </tbody>
            </Table>

        ) : (<div> No items to Display</div>)}</div>);
};
       


export default ScrollLabelList;