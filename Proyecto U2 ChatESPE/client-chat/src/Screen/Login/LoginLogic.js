import axios from 'axios';

const apiBaseUrl = 'http://localhost:8003/api';

export const handleSubmitLogin = async (username, password) => {
    try {
        const response = await axios.post(`${apiBaseUrl}/users/login`, {"username": username, "password":password})
        const responseData = response.data;
        if (response.status === 200) {
            alert('Inicio de sesión exitoso.');
            sessionStorage.setItem("user", username);
            sessionStorage.setItem("userId", responseData.id);
            window.location.href = '/chat';
        }
    } catch (error) {
        console.error('Error al iniciar sesión:', error);
        if (error.response && error.response.status === 401) {
            alert('Usuario o contraseña incorrectos.');
        } else {
            alert('Hubo un problema al iniciar sesión. Por favor, inténtalo de nuevo.');
        }
    }
};


export const handleSubmitRegister = async (username, password) => {
    try {
        const response = await axios.post(`${apiBaseUrl}/users/register`,  {"username": username, "password":password})

        if (response.status === 201) { 
            alert('Registro exitoso. Ahora puedes iniciar sesión.');
        }
    } catch (error) {
        console.error('Error al registrar usuario:', error);

        if (error.response && error.response.data) {
            // Muestra errores detallados del backend.
            const errorMessages = Object.values(error.response.data).join('\n');
            alert(`Error al registrar usuario:\n${errorMessages}`);
        } else {
            alert('Hubo un problema al registrar el usuario. Por favor, inténtalo de nuevo.');
        }
    }
};
