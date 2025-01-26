import { Client } from "@stomp/stompjs";
import SockJS from "sockjs-client";
import axios from "axios";

let stompClient = null;

const urlSocket = 'http://localhost:8080/ws';
const urlApi = 'http://localhost:8003/api';

// Conectar al servidor WebSocket
export const connectStomp = (senderId, receiverId, onMessageReceived) => {
    const socket = new SockJS(urlSocket); // URL WebSocket del servidor
    stompClient = new Client({
        webSocketFactory: () => socket,
        onConnect: () => {
            console.log("Conexión WebSocket exitosa!");

            // Suscribirse al canal de mensajes del receptor
            stompClient.subscribe(`/user/queue/messages/${receiverId}`, (message) => {
                onMessageReceived(JSON.parse(message.body));
                console.log(JSON.parse(message.body));
            });

            // Enviar un mensaje de bienvenida o conexión exitosa
            stompClient.publish({
                destination: `/app/chat/${senderId}/${receiverId}`,
                body: JSON.stringify({ senderId, receiverId, message: '¡Conexión establecida!' })
            });
        },
        onStompError: (error) => {
            console.error("Error en la conexión STOMP:", error);
        }
    });

    stompClient.activate();
};

// Desconectar del servidor WebSocket
export const disconnectStomp = (senderId, receiverId) => {
    if (stompClient !== null) {
        stompClient.deactivate(() => {
            console.log("Desconectado del servidor WebSocket.");
        });
    }
};

// Enviar mensaje a través de WebSocket y guardarlo en la base de datos
export const sendMessageStomp = (senderId, receiverId, message) => {
    if (stompClient !== null && stompClient.connected) {
        const messageData = { senderId, receiverId, message };

        // Publicar el mensaje al servidor WebSocket
        stompClient.publish({
            destination: `/app/chat/${senderId}/${receiverId}`,
            body: JSON.stringify(messageData)
        });

        // Guardar el mensaje en la base de datos
        handleSaveMessage(senderId, receiverId, message);
    } else {
        console.error("No conectado al servidor WebSocket. No se puede enviar el mensaje.");
    }
};

// Obtener mensajes previos entre dos usuarios
export const handleGetMessage = async (idUser, destinationUserId) => {
    try {
        const response = await axios.get(`${urlApi}/messages/private`, {
            params: {
                senderId: idUser,
                receiverId: destinationUserId
            }
        });
        return response.data; // Retorna el array de mensajes obtenidos
    } catch (error) {
        console.error("Error al obtener mensajes:", error);
        return [];
    }
};

// Guardar un mensaje en la base de datos
export const handleSaveMessage = async (idUser, destinationUserId, message) => {
    try {
        const response = await axios.post(`${urlApi}/messages`, {
            "message": message,
            "user": { "id": idUser },
            "destinationUser": { "id": destinationUserId }
        });
        
        // Retornar la respuesta si es necesario
        return response.data;
    } catch (error) {
        console.error("Error al guardar el mensaje:", error);
        throw error; // Lanza el error para ser manejado más arriba si es necesario
    }
};
