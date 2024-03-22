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
