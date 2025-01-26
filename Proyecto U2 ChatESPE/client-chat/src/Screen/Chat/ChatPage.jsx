import React, { useState, useEffect } from 'react';
import { handleCreateGroup, handleSendMessage, handlePrivateChat, handleStartPrivateChat } from './ChatLogic.js';
import Header from '../../Components/Header.jsx';
import './ChatPage.css';
import { useNavigate } from 'react-router-dom';
const ChatPage = () => {
    const [groupName, setGroupName] = useState('');
    const [selectedGroup, setSelectedGroup] = useState('');
    const [selectedUser, setSelectedUser] = useState('');
    const [users, setUsers] = useState([]);
    const navigate = useNavigate();
    useEffect(() => {
        const fetchUsers = async () => {
            const usersData = await handlePrivateChat();
            setUsers(usersData);
        };
        fetchUsers();
    }, []);

    return (
        <div className="chat-container">
            <Header />
            <h1 className="chat-title">Sala de Chat</h1>

            <div className="group-chat">
                <p className="section-title">Chat General</p>
                <input
                    className="group-input"
                    placeholder="Crear grupo"
                    value={groupName}
                    onChange={(e) => setGroupName(e.target.value)}
                />
                <button className="group-button" onClick={handleCreateGroup}>Crear</button>

                <p className="section-title">Buscar Grupo</p>
                <select
                    className="group-select"
                    value={selectedGroup}
                    onChange={(e) => setSelectedGroup(e.target.value)}
                >
                    <option>Seleccione un grupo</option>
                    {/* Aquí puedes mapear grupos existentes */}
                </select>
                <button className="send-button" onClick={handleSendMessage}>Enviar</button>
            </div>

            <div className="private-chat">
                <p className="section-title">Chat Privado</p>
                <select
                    className="user-select"
                    value={selectedUser}
                    onChange={(e) => setSelectedUser(e.target.value)}
                >
                    <option value="">Selecciona un usuario</option>
                    {users.map((user) => (
                        <option key={user.id} value={user.id}>
                            {user.username.split('@')[0]}
                        </option>
                    ))}
                </select>
                <button className="private-chat-button" onClick={(e) => handleStartPrivateChat(selectedUser, navigate)}>Iniciar Chat</button>
            </div>
        </div>
    );
}

export default ChatPage;
