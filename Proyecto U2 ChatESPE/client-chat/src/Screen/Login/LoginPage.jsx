import { useState } from 'react';
import { handleSubmitLogin, } from './LoginLogic.js';
import './LoginPage.css';

const LoginPage = () => {
    const [loginUsername, setLoginUsername] = useState('');
    const [loginPassword, setLoginPassword] = useState('');
    const [registerUsername, setRegisterUsername] = useState('');
    const [registerPassword, setRegisterPassword] = useState('');

    return (
        <div className="page-container">
            <div className="split-container">
                <div className="split-section login-section">
                    <div className="content-wrapper">
                        <h1 className="main-title">ChatESPE</h1>
                        <h2>Iniciar Sesión</h2>
                        <form onSubmit={(e) => {
                            e.preventDefault();
                            handleSubmitLogin(loginUsername, loginPassword);
                        }}>
                            <div className="form-group">
                                <label htmlFor="loginUsername">Usuario:</label>
                                <input
                                    type="email"
                                    id="loginUsername"
                                    name="loginUsername"
                                    value={loginUsername}
                                    onChange={(e) => setLoginUsername(e.target.value)}
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="loginPassword">Contraseña:</label>
                                <input
                                    type="password"
                                    id="loginPassword"
                                    name="loginPassword"                                    
                                    value={loginPassword}
                                    onChange={(e) => setLoginPassword(e.target.value)}
                                    required
                                />
                            </div>
                            <button type="submit">Ingresar</button>
                        </form>
                    </div>
                </div>
                <div className="split-section register-section">
                    <div className="content-wrapper">
                        <h2>Registro</h2>
                        <form onSubmit={(e) => {
                            e.preventDefault();
                            //handleSubmitRegister(registerUsername, registerPassword);
                        }}>
                            <div className="form-group">
                                <label htmlFor="registerUsername">Usuario:</label>
                                <input
                                    type="email"
                                    id="registerUsername"
                                    name="registerUsername"
                                    value={registerUsername}
                                    onChange={(e) => setRegisterUsername(e.target.value)}
                                    required
                                />
                            </div>
                            <div className="form-group">
                                <label htmlFor="registerPassword">Contraseña:</label>
                                <input
                                    type="password"
                                    id="registerPassword"
                                    name="registerPassword"
                                    value={registerPassword}
                                    onChange={(e) => setRegisterPassword(e.target.value)}
                                    required
                                />
                            </div>
                            <button type="submit">Registrar</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;