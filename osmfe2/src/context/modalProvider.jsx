import { useState, createContext } from 'react';

export const ModalContext = createContext();


export const ModalProvider = ({ children }) => {
    const [isQueryOpen, setQueryOpen] = useState(false);
    const [isModifyOpen, setModifyOpen] = useState(false);

    const toggleQuery = () => {
        setQueryOpen(!isQueryOpen);
    };

    const toggleModify = () => {
        setModifyOpen(!isModifyOpen);
        setQueryOpen(!isQueryOpen);
    };

    const closeAllModal = () => {
        setQueryOpen(false);
        setModifyOpen(false);
    };

    return (
        <ModalContext.Provider
            value={{

                isQueryOpen,
                toggleQuery,
                isModifyOpen,
                toggleModify,
                closeAllModal,
            }}
        >
            {children}
        </ModalContext.Provider>
    );
};


