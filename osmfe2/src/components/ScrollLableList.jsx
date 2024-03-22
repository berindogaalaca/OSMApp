import React from "react"
import Table from "react-bootstrap/Table";
import Button from "./Button.jsx"


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
                            <td><button className="button rounded-3 ms-3 px-2 bg-dark text-white" style={{ width: '100%', height: '5vh' }}><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
  <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5"/>
</svg></button></td>
                            <td><Button buttontext="Modify"/></td>
                        </tr>

                    ))}


                </tbody>
            </Table>

        ) : (<div> No items to Display</div>)}</div>);
};
       


export default ScrollLabelList;