import React, { useState, useRef } from 'react';
import AddPointPanel from '../pages/AddPoint';
import QueryPointPanel from '../pages/QueryPoint';
import Button from './Button';
import MapPage from '../pages/Mapp';
import './AddPoint.css';

const Navbar = () => {
    const [isAddPointOpen, setIsAddPointOpen] = useState(false);
    const [isQueryPointOpen, setIsQueryPointOpen] = useState(false);
    const [clickedCoord, setClickedCoord] = useState(null);

    const modalRef = useRef(null);

    const handleAddPointClick = () => {
        setIsAddPointOpen(true);
        setIsQueryPointOpen(false);
    };

    const handleCloseAddPoint = () => setIsAddPointOpen(false);
    const handleQueryPointClick = () => setIsQueryPointOpen(true);
    const handleCloseQueryPoint = () => setIsQueryPointOpen(false);

    const handleOverlayClick = (event) => {
        if (modalRef.current && event.target === modalRef.current) {
            setIsAddPointOpen(false);
            setIsQueryPointOpen(false);
        }
    };

    const handleMapClick = () => {
        setIsAddPointOpen(true);
        setIsQueryPointOpen(false);
    };

    const handleCardClick = (event) => {
        event.stopPropagation();
    };

    const handleCoordinateClick = (coord) => {
        setClickedCoord(coord);
        handleAddPointClick();
    };
    return (
        <div>
            <nav className="navbar py-0">
                <div className="container-fluid bg-dark">
                    <div className="navbar-menu my-3">
                        <div className="navbar-end">
                            <div className="navbar-item">
                                <div className="buttons">
                                    <Button buttontext="Add Point" buttonclick={handleAddPointClick} />
                                    <Button buttontext="Query Point" buttonclick={handleQueryPointClick} />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </nav>
            {(isAddPointOpen || isQueryPointOpen) && (
                <div
                    className="modal-overlay"
                    onClick={handleOverlayClick}
                    ref={modalRef}
                >
                    {isAddPointOpen && (
                        <AddPointPanel
                            handleClose={handleCloseAddPoint}
                            coord={clickedCoord} 
                        />
                    )}
                    {isQueryPointOpen && (
                        <QueryPointPanel
                            handleClose={handleCloseQueryPoint}
                            onClick={handleCardClick}
                        />
                    )}
                </div>
            )}


            <div onClick={handleMapClick}>
                <MapPage onCoordinateClick={handleCoordinateClick} />
            </div>
        </div>
    );
};

export default Navbar;