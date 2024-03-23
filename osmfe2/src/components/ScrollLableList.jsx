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
    const [modalShowModify, setModalShowModify] = useState(false);
    const [modalShowQuery, setModalShowQuery] = useState(false);

    const openModifyModal = () => {
        setModalShowModify(true);
        setModalShowQuery(false);
    };

    const closeModifyModal = () => {
        setModalShowModify(false);
    };

    const handleModifyButtonClick = () => {
        // QueryPoint modalýný kapat
        setModalShowQuery(false);

        // Modify modalýný aç
        openModifyModal();
    };

    const handleQueryPointClose = () => {
        setModalShowQuery(false);
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
                                    backgroundColor:
                                        selectedItem && selectedItem === item
                                            ? 'black'
                                            : 'transparent',
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
                                            Delete
                                        </Button>
                                        <Button
                                            className="bg-black border-0 mx-3"
                                            onClick={() => handleModifyButtonClick()} // handleModifyButtonClick fonksiyonu çaðrýldý
                                            style={{ width: '100%', height: '5vh' }}
                                        >
                                            Modify
                                        </Button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            ) : (
                <div>No items to Display</div>
            )}

            {modalShowModify && <Modify show={modalShowModify} onHide={closeModifyModal} />} {/* Modify modalý */}
            {modalShowQuery && <QueryPoint show={modalShowQuery} onHide={handleQueryPointClose} />} {/* QueryPoint modalý */}

            <ToastContainer />
        </div>
    );
};

export default ScrollLabelList;
