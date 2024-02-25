import React from 'react';
// import './Panel.css';

const Panel = ({ isOpen, onClose, children }) => {
    return (
        <div className={`panel ${isOpen ? 'open' : ''}`}>
            <div className="panel-content">
                <button className="close-btn" onClick={onClose}>X</button>
                {children}
            </div>
        </div>
    );
};

export default Panel;
