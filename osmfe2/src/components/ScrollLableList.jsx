import React from 'react';
import Table from 'react-bootstrap/Table';
import Button from "react-bootstrap/Button";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useMutation, useQueryClient } from "@tanstack/react-query"
import { deleteData } from "../service/DeleteFetch"

const ScrollLabelList = ({ items, selectedItem, handleItemClick }) => {

    const queryClient = useQueryClient();

    const mutation = useMutation({
        mutationFn: deleteData,
        mutationKey: "delete",
        onSuccess: () => {
            queryClient.invalidateQueries("data");
        },
    });
    const deleteHandler = (pointId) => {
        mutation.mutate(pointId);
    };
    console.log("items"+items)
    return (
        <div style={{ overflowY: 'auto', maxHeight: '280px' }}>
            {items && items.length > 0 ? (
                <Table responsive="sm">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Number</th>
                            <th>Latitude</th>
                            <th>Longitude</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {items.map((item, index) => (
                            
                            <tr
                                key={index}
                                onClick={() => handleItemClick(item)}
                                style={{
                                    cursor: 'pointer',
                                    backgroundColor:
                                        selectedItem && selectedItem === item ? 'black' : 'transparent',
                                }}
                            >
                                <td>{item.pointName}</td>
                                <td>{item.pointNumber}</td>
                                <td>{item.latitude}</td>
                                <td>{item.longitude}</td>
                                <td>
                                    <div className="d-flex justify-content-between">
                                        <Button
                                            className="bg-black border-0 mx-3"
                                            onClick={() => deleteHandler(item.pointId)}
                                            style={{ width: '100%', height: '5vh' }}
                                        >
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                                                <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5" />
                                            </svg>
                                        </Button>
                                        <Button className="bg-black border-0 mx-3" style={{ width: '100%', height: '5vh' }}>
                                            Modify
                                        </Button>                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            ) : (
                <div>No items to Display</div>
            )}
            <ToastContainer />
        </div>
    );
};

export default ScrollLabelList;
