import React from 'react';
import './Header.css';

const Header = () => {
  const username = sessionStorage.getItem("user");

  const handleLogout = () => {
    sessionStorage.removeItem("user");
    sessionStorage.removeItem("userId");
    window.location.href = "/"; 
  };

  return (
    <div className="header-container">
        <h1 className="greeting">Hola {username.split('@')[0].toUpperCase()}</h1>
        <button className="logout-button" onClick={handleLogout}>Logout</button>
    </div>
  );
};

export default Header;
