import React, { useState, useContext } from 'react';
import Button from './Button';
import AddPoint from './AddPoint';
import QueryPoint from './QueryPoint';
import Modify from './Modify';
import MapComponent from './Map';

import { ModalContext } from '../context/modalProvider';

function Navbar() {
    const {
    toggleQuery, toggleInteraction } = useContext(ModalContext);

    const onClickHandler = () => {
        toggleQuery();
    };
    const onClickAdd = () => {
        toggleInteraction(); 
    };



    return (
        <div className="d-flex justify-content-center">
            <nav className="navbar navbar-expand-lg " style={{ height: '9vh' }}>
                <div className="container-fluid">
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav">
                            <li className="nav-item mx-3">
                                <Button
                                    buttontext="Add Point"
                                    buttonclick={onClickAdd}
                                />
                            </li>
                            <li className="nav-item">
                                <Button
                                    buttontext="Query Point"
                                    buttonclick={onClickHandler}
                                />
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
            <AddPoint  />
            <QueryPoint/>
            <Modify/>
        </div>
    );
}

export default Navbar;
