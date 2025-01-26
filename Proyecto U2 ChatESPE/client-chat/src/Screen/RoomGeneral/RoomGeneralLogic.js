import axios from "axios";
const urlApi = "http://localhost:8003/api";

export const handleGetMessages = async (roomId) => {
    const response = await axios.get(`${urlApi}/rooms-general/${roomId}`);
    return response.data;
};

export const handleSendMessages = async (message, roomId, userId) => {
    await axios.post(`${urlApi}/rooms-general`, {
        message,
        "room": {"id": roomId},
        "user": {"id": userId}
    });
};
