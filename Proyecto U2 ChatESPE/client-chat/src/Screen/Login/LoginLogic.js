import axios from 'axios';
import SockJS from 'sockjs-client';
import { Stomp } from '@stomp/stompjs';

// Configuración base de axios
const API_URL = 'http://localhost:8080/api';
const SOCKET_URL = 'http://localhost:8080/ws';

// Rutas de suscripción y envío de mensajes
const PUBLIC_CHANNEL = '/topic/public';
const PRIVATE_CHANNEL = '/user/{username}/private';
const CHAT_ENDPOINT = '/app/chat.send';
const PRIVATE_MESSAGE_ENDPOINT = '/app/private-message';

// Variable para mantener la conexión del WebSocket
let stompClient = null;

// Función para conectar al WebSocket
export const connectWebSocket = (onMessageReceived) => {
    const socket = new SockJS(SOCKET_URL);
    stompClient = Stomp.over(socket);

    stompClient.connect({}, () => {
        console.log('Conectado al WebSocket');
        stompClient.subscribe(PUBLIC_CHANNEL, (message) => {
            onMessageReceived(JSON.parse(message.body));
        });

        const user = JSON.parse(localStorage.getItem('user'));
        if (user) {
            stompClient.subscribe(PRIVATE_CHANNEL.replace('{username}', user.username), (message) => {
                onMessageReceived(JSON.parse(message.body));
            });
        }
    }, (error) => {
        console.error('Error en la conexión WebSocket:', error);
        setTimeout(() => connectWebSocket(onMessageReceived), 5000);
    });
};

// Función para enviar mensajes
export const sendMessage = (message) => {
    if (stompClient?.connected) {
        const user = JSON.parse(localStorage.getItem('user'));
        if (user) {
            stompClient.send(CHAT_ENDPOINT, {}, JSON.stringify({
                content: message,
                sender: user.username,
                type: 'CHAT',
                timestamp: new Date()
            }));
        }
    } else {
        console.error('No hay conexión WebSocket');
    }
};

// Función para enviar mensajes privados
export const sendPrivateMessage = (message, recipient) => {
    if (stompClient?.connected) {
        const user = JSON.parse(localStorage.getItem('user'));
        if (user) {
            stompClient.send(PRIVATE_MESSAGE_ENDPOINT, {}, JSON.stringify({
                content: message,
                sender: user.username,
                recipient,
                type: 'PRIVATE',
                timestamp: new Date()
            }));
        }
    } else {
        console.error('No hay conexión WebSocket');
    }
};

// Función para iniciar sesión
export const handleSubmitLogin = async (username, password) => {
    try {
        validateInput(username);
        validateInput(password);

        const response = await axiosInstance.post('/auth/login', { username, password });

        if (response.data) {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('user', JSON.stringify(response.data.user));

            connectWebSocket((message) => {
                console.log('Mensaje recibido:', message);
            });

            return response.data;
        }
    } catch (error) {
        handleError(error);
    }
};

// Función para desconectar el WebSocket
export const disconnect = () => {
    if (stompClient) {
        stompClient.disconnect();
        console.log('Desconectado del WebSocket');
    }
};

// Función para cerrar sesión
export const logout = () => {
    disconnect();
    localStorage.removeItem('token');
    localStorage.removeItem('user');
};

// Función auxiliar para validar el tamaño mínimo de un string
const validateInput = (input) => {
    if (input.length < 8) {
        throw new Error('No puede ser menor que 8 caracteres');
    }
};

// Función auxiliar para manejar errores
const handleError = (error) => {
    if (error.message === 'No puede ser menor que 8 caracteres') {
        alert(error.message);
    } else if (error.response) {
        alert(error.response.data.message || 'Error en las credenciales');
    } else if (error.request) {
        alert('Error de conexión con el servidor');
    } else {
        alert('Error en el proceso');
    }
};

// Instancia de axios con configuración base
const axiosInstance = axios.create({
    baseURL: API_URL,
    headers: { 'Content-Type': 'application/json' }
});

// Interceptor para agregar el token a todas las peticiones
axiosInstance.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);
