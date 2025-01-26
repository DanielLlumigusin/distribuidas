import React, { useState, useEffect } from 'react';
import { handleCreateRoom, handleOpenGroup, handlePrivateChat, handleStartPrivateChat, getRooms } from './ChatLogic.js';
import Header from '../../Components/Header.jsx';
import './ChatPage.css';
import { useNavigate } from 'react-router-dom';

const ChatPage = () => {
    const [rooms, setRooms] = useState([]); 
    const [groupName, setGroupName] = useState('');  
    const [selectedGroup, setSelectedGroup] = useState(''); 
    const [selectedUser, setSelectedUser] = useState('');
    const [users, setUsers] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchRooms = async () => {
            const roomsData = await getRooms();
            setRooms(roomsData);
        }

        const fetchUsers = async () => {
            const usersData = await handlePrivateChat();
            setUsers(usersData);
        };
        fetchRooms();
        fetchUsers();
    }, []);

    // Manejar la creación de un grupo
    const handleCreateGroupClick = () => {
        if (groupName.trim() !== "") {
            handleCreateRoom(groupName);  // Llamada a la API para crear el grupo
            setGroupName('');  // Limpiar el campo después de la creación
        } else {
            alert("Por favor ingresa un nombre para el grupo.");
        }
    };

    // Manejar el envío de un mensaje al grupo
    const handleSendMessageClick = () => {
        if (selectedGroup) {
            handleOpenGroup(selectedGroup, navigate);
            alert("Has seleccionado el grupo: " + selectedGroup);
        } else {
            alert("Por favor selecciona un grupo y escribe un mensaje.");
        }
    };

    // Iniciar chat privado
    const handleStartPrivateChatClick = () => {
        if (selectedUser) {
            handleStartPrivateChat(selectedUser, navigate);
        } else {
            alert("Por favor selecciona un usuario para el chat privado.");
        }
    };

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
                <button className="group-button" onClick={handleCreateGroupClick}>Crear</button>

                <p className="section-title">Buscar Grupo</p>
                <select
                    className="group-select"  
                    value={selectedGroup}
                    onChange={(e) => setSelectedGroup(e.target.value)}
                >
                    <option>Seleccione un grupo</option>
                    {rooms.map((room) => (
                        <option key={room.id} value={room.id}>
                            {room.name_room}
                        </option>
                    ))}
                </select>
                <button className="send-button" onClick={handleSendMessageClick}>Entrar</button>
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
                <button className="private-chat-button" onClick={handleStartPrivateChatClick}>Iniciar Chat</button>
            </div>
        </div>
    );
}

export default ChatPage;
