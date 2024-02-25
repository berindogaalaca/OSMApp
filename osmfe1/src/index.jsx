import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import HomePage from "./components/HomePage"
// import App from './App';
// import AddPointPanel from './pages/AddPoint';




const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <HomePage />
    </React.StrictMode>
);
