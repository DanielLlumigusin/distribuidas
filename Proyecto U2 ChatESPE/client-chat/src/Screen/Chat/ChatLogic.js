import axios from 'axios';

const urlapi = 'http://localhost:8003/api';
export const handleCreateGroup = () => {
    
    console.log('Creando grupo:', groupName);
};

export const handleSendMessage = () => {
    console.log('Enviando mensaje al grupo:', selectedGroup);
};

export const handlePrivateChat = async () => {
    try {
        const response = await axios.get(`${urlapi}/users`);
        const users = response.data;
        return users;
    } catch (error) {
        return [];
    }
};

export const handleStartPrivateChat = (destination_user_id, navigate) => {
    navigate(`/room`, { state: {destination_user_id } });
};