import { useState, createContext } from 'react';
export const ModalContext = createContext();


export const ModalProvider = ({ children }) => {
    const [isQueryOpen, setQueryOpen] = useState(false);
    const [isAddOpen, setAddOpen] = useState(false);
    const [isModifyOpen, setModifyOpen] = useState(false);
    const [selectedPoint, setSelectedPoint] = useState(null);
    const [isInteractionOpen, setInteractionOpen] = useState(false);
    const [isDrawInteractionOpen, setDrawInteractionOpen] = useState(false);


    const toggleQuery = () => {
        setQueryOpen(!isQueryOpen);
    };
    const toggleAdd = () => {
        setAddOpen(!isAddOpen)
    }
    const handleCoordinateChange = () => {
        setQueryOpen(false)
    }
    const toggleInteraction = () => {
        setInteractionOpen(!isInteractionOpen)
    }

    const toggleModify = () => {
        setModifyOpen(!isModifyOpen);
        setQueryOpen(!isQueryOpen);

    };
    const toggleModifyOpen = () => {
        setModifyOpen(!isModifyOpen);
    };
    const toggleModifyClose = () => {
        setModifyOpen(!isModifyOpen);
        setQueryOpen(false);
    };
    const selectPoint = (item) => {
        setSelectedPoint(item);

    }
    const closeAllModal = () => {
        setQueryOpen(false);
        setModifyOpen(false);
    };
    const toggleDrawInteraction = () => {
        setDrawInteractionOpen(!isInteractionOpen)
        closeAllModal()

    }
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
                toggleInteraction,
                isDrawInteractionOpen,
                toggleDrawInteraction,
                toggleModifyClose,
                handleCoordinateChange,
                toggleModifyOpen
            }}
        >
            {children}
        </ModalContext.Provider>
    );
};