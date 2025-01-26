
import { useLocation } from "react-router-dom";
import { useState, useEffect } from "react";
import { handleGetMessages, handleSendMessages } from "./RoomGeneralLogic";

const RoomGeneralPage = () => {
    const location = useLocation();
    const [idRoom, setIdRoom] = useState();
    const [idUser, setIdUser] = useState();
    const [messages, setMessages] = useState([]);
    const [newMessage, setNewMessage] = useState("");

    // Configure user and room IDs when the component loads
    useEffect(() => {
        setIdUser(sessionStorage.getItem("userId"));
        setIdRoom(location.state?.roomId);
    }, [location.state]);

    // Fetch messages when the room ID is set or changes
    useEffect(() => {
        if (idRoom) {
            handleGetMessages(idRoom)
                .then((fetchedMessages) => {
                    setMessages(fetchedMessages);
                })
                .catch((error) => {
                    console.error("Error fetching messages:", error);
                    // Optionally show an error to the user
                });
        }
    }, [idRoom]);

    // Handle sending a message
    const handleSend = () => {
        if (newMessage.trim()) {
            handleSendMessages(newMessage, idRoom, idUser)
                .then(() => {
                    // Refresh the message list after sending
                    handleGetMessages(idRoom)
                        .then((fetchedMessages) => {
                            setMessages(fetchedMessages);
                        })
                        .catch((error) => {
                            console.error("Error fetching messages after sending:", error);
                        });
                    setNewMessage("");
                })
                .catch((error) => {
                    console.error("Error sending message:", error);
                });
        }
    };

    return (
        <div className="room-container">
            <a href="/chat" className="btn-return">Regresar</a>
            <h1 className="room-title">Chat Grupal</h1>
            <p className="chat-info">Este es el chat general con ID: {idRoom}</p>

            <div className="chat-box">
                {messages.map((message, index) => (
                    <div key={index} className="message-container">
                        {/* Username alignment */}
                        <span className={`username ${message.user.id == idUser ? "my-username" : "their-username"}`}>
                            {message.user.username}
                        </span>
                        {/* Message content */}
                        <div className={`message ${message.user.id == idUser ? "my-message" : "their-message"}`}>
                            {message.message}
                        </div>
                    </div>
                ))}
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

export default RoomGeneralPage;



