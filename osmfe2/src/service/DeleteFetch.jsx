import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';

export const fetchData = async (PointName, PointNumber, Latitude, Longitude) => {
    const response = await fetch(`https://localhost:7000/api/point?PointName=${PointName}&PointNumber=${PointNumber}&Latitude=${Latitude}&Longitude=${Longitude}`);
    return response.json();
}

export const deleteData = async (pointId) => {
    const response = await fetch(`https://localhost:7000/api/point/${pointId}`, {
        method: "DELETE",
    });

    const data = await response.json();

    if (response.ok) {
        console.log(data)
        const message = data?.message;
        console.log(message)
        toast.success(message, {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined, className: "toast-message",
        });

    } else {
        toast.error('You must fill in all fields', {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
        });
    }

    return data;
}
export const addData = async (Data) => {
    const response = await fetch(`https://localhost:7000/api/point`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(Data),
    });

    const data = await response.json();


    if (response.ok) { 
        console.log(data)
        const message = data?.message;
        console.log(message)
        toast.success(message, {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined, className: "toast-message",
        });
       
    } else {
        toast.error('You must fill in all fields', {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
        });
    }

    return data;
}

export const updateData = async (Data) => {
    const response = await fetch(`https://localhost:7000/api/point/${Data.pointId}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(Data),
    });

    const data = await response.json();

    if (response.ok) {
        console.log(data)
        const message = data?.message;
        console.log(message)
        toast.success(message, {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined, className: "toast-message",
        });

    } else {
        toast.error('You must fill in all fields', {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: true,
            pauseOnHover: true,
            draggable: true,
            progress: undefined,
        });
    }

    return data;
}