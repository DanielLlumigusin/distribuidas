import React, { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import { connectStomp, disconnectStomp, sendMessageStomp, handleGetMessage } from "./RoomLogic";
import "./RoomPage.css";

const RoomPage = () => {
    const location = useLocation();
    const [idUser, setIdUser] = useState(null);
    const [destination_user_id, setDestinationUser] = useState(null);
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState("");

    // Configurar IDs de usuario al cargar el componente
    useEffect(() => {
        setIdUser(sessionStorage.getItem("userId"));
        setDestinationUser(location.state?.destination_user_id);
    }, [location.state]);

    // Obtener mensajes previos y conectar al servidor STOMP
    useEffect(() => {
        if (idUser && destination_user_id) {
            const fetchMessages = async () => {
                const fetchedMessages = await handleGetMessage(idUser, destination_user_id);
                setMessages(fetchedMessages);
            };

            fetchMessages();

            // Conectarse al servidor STOMP
            connectStomp(idUser, destination_user_id, (newMessage) => {
                setMessages((prevMessages) => [...prevMessages, newMessage]);
            });

            // Actualizar mensajes cada segundo
            const interval = setInterval(() => {
                fetchMessages();
            }, 1000); // Cada 1 segundo

            // Limpiar el intervalo al desmontar el componente
            return () => {
                clearInterval(interval);
                disconnectStomp(idUser, destination_user_id); // Desconectar al salir del componente
            };
        }
    }, [idUser, destination_user_id]);

    // Desplazar la caja de chat al final cuando se actualizan los mensajes
    useEffect(() => {
        const chatBox = document.querySelector(".chat-box");
        if (chatBox) {
            chatBox.scrollTop = chatBox.scrollHeight;
        }
    }, [messages]);

    // Manejar el envío de mensajes
    const handleSend = () => {
        if (newMessage.trim() === "") return;

        const messageData = {
            senderId: idUser,
            receiverId: destination_user_id,
            message: newMessage,
        };

        // Añadir el mensaje localmente antes de enviarlo
        setMessages((prevMessages) => [
            ...prevMessages,
            { user: { id: idUser }, message: newMessage },
        ]);
        setNewMessage("");

        // Enviar mensaje al servidor
        sendMessageStomp(idUser, destination_user_id, newMessage);
    };

    return (
        <div className="room-container">
            <a href="/chat" className="btn-return">Regresar</a>
            <h1 className="room-title">Chat Privado</h1>
            <p className="chat-info">Este es el chat privado con el usuario ID: {destination_user_id}</p>

            <div className="chat-box">
                {(
                    messages.map((message, index) => (
                        <div
                            key={index}
                            className={`message ${message.user.id == idUser ? "my-message" : "their-message"}`}
                        >
                            {message.message}
                        </div>
                    ))
                )}
            </div>

            <div className="message-input-container">
                <input
                    type="text"
                    placeholder="Escribe un mensaje"
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                    className="message-input"
                />
                <button className="btn-send" onClick={handleSend}>
                    Enviar
                </button>
            </div>
        </div>
    );
};

export default RoomPage;
