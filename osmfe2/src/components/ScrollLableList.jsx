import React, { useState } from 'react';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { deleteData } from '../service/DeleteFetch';
import Modify from './Modify';
import QueryPoint from './QueryPoint'; 

const ScrollLabelList = ({ items, selectedItem, handleItemClick }) => {
    const [modalShowQuery, setModalShowQuery] = useState(false);
    const [modalShowModify, setModalShowModify] = useState(false);
    const queryClient = useQueryClient();

    const mutation = useMutation({
        mutationFn: deleteData,
        mutationKey: 'delete',
        onSuccess: () => {
            queryClient.invalidateQueries('data');
        },
    });

    const deleteHandler = (pointId) => {
        mutation.mutate(pointId);
    };

    const openModifyModal = () => {
        setModalShowModify(true);
        setModalShowQuery(false); 
    };

    const closeModifyModal = () => {
        setModalShowModify(false);
    };

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
                                    backgroundColor: selectedItem && selectedItem === item ? 'black' : 'transparent',
                                }}>
                                <td>{item.pointName}</td>
                                <td>{item.pointNumber}</td>
                                <td>{item.latitude}</td>
                                <td>{item.longitude}</td>
                                <td>
                                    <div className="d-flex justify-content-between">
                                        <Button
                                            className="bg-black border-0 mx-3"
                                            onClick={() => deleteHandler(item.pointId)}
                                            style={{ width: '100%', height: '5vh' }}>
                                            Delete
                                        </Button>
                                        <Button
                                            className="bg-black border-0 mx-3"
                                            onClick={() => openModifyModal()}
                                            style={{ width: '100%', height: '5vh' }}>
                                            Modify
                                        </Button>
                                        {modalShowModify && (
                                            <Modify
                                                show={modalShowModify}
                                                onHide={() => setModalShowModify(false)}
                                            />
                                        )} {modalShowQuery && <QueryPoint onHide={() => setModalShowQuery(false)} />} 
                                    </div>
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
