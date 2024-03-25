export const fetchData = async (PointName, PointNumber, Latitude, Longitude) => {
    const response = await fetch(`https://localhost:7000/api/point?PointName=${PointName}&PointNumber=${PointNumber}&Latitude=${Latitude}&Longitude=${Longitude}`);
    return response.json();
}

export const deleteData = async (pointId) => {
    const response = await fetch(`https://localhost:7000/api/point/${pointId}`, {
        method: "DELETE",
    });
    return response.json();
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

    if (!response.ok) {
        throw new Error(data?.Message || 'Something went wrong');
    }

    return data;
}