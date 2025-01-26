import axios from 'axios';

const urlApi = 'http://localhost:8003/api';

// Crear un grupo
export const handleCreateRoom = async (groupName) => {
    try {
        const response = await axios.post(`${urlApi}/rooms`, { "name_room": groupName });
        console.log('Grupo creado:', response.data);
        window.location.reload();
    } catch (error) {
        console.error('Error al crear grupo:', error);
    }
};

// Entrar al chat grupal
export const handleOpenGroup = async (roomId, navigate) => {
    navigate(`/room-general`, {state: {roomId : roomId}});
};

// Obtener usuarios para el chat privado
export const handlePrivateChat = async () => {
    try {
        const response = await axios.get(`${urlApi}/users`);
        return response.data;
    } catch (error) {
        console.error('Error al obtener usuarios:', error);
        return [];
    }
};

// Iniciar chat privado con un usuario específico
export const handleStartPrivateChat = (destinationUserId, navigate) => {
    navigate(`/room`, { state: { destination_user_id: destinationUserId } });
};

// Obtener las salas
export const getRooms = async () => {
    try {
        const result = await axios.get(`${urlApi}/rooms`);
        return result.data;
    } catch (error) {
        throw error;
    }
};
