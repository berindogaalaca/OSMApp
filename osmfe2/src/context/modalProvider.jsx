import { useState, createContext } from 'react';

export const ModalContext = createContext();


export const ModalProvider = ({ children }) => {
    const [isQueryOpen, setQueryOpen] = useState(false);
    const [isAddOpen, setAddOpen] = useState(false);
    const [isModifyOpen, setModifyOpen] = useState(false);
    const [selectedPoint, setSelectedPoint] = useState(null);
    const [isInteractionOpen, setInteractionOpen] = useState(false);


    const toggleQuery = () => {
        setQueryOpen(!isQueryOpen);
    };
    const toggleAdd = () => {
        setAddOpen(!isAddOpen)
    }
    const toggleInteraction = () => {
        setInteractionOpen(!isInteractionOpen)
    }

    const toggleModify = () => {
        setModifyOpen(!isModifyOpen);
        setQueryOpen(!isQueryOpen);
      
    };
    const selectPoint = (item) => {
        setSelectedPoint(item);
        console.log(item);
    }
    const closeAllModal = () => {
        setQueryOpen(false);
        setModifyOpen(false);
    };

    return (
        <ModalContext.Provider
            value={{
                isAddOpen,
                toggleAdd,
                isQueryOpen,
                toggleQuery,
                isModifyOpen,
                toggleModify,
                closeAllModal,
                selectPoint,
                selectedPoint,
                isInteractionOpen,
                toggleInteraction
            }}
        >
            {children}
        </ModalContext.Provider>
    );
};


