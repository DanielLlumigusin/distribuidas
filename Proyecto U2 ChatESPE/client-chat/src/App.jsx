import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import './App.css';
import LoginPage from "./Screen/Login/LoginPage.jsx";
import ChatPage from './Screen/Chat/ChatPage.jsx';
import RoomPage from './Screen/Room/RoomPage.jsx';

function App() {
  const isAuthenticated = sessionStorage.getItem("user");

  return (
    <BrowserRouter>
      <Routes>
        <Route 
          path='/' 
          element={isAuthenticated ? <Navigate to="/chat" /> : <LoginPage />}
        />
        <Route 
          path='/chat' 
          element={isAuthenticated ? <ChatPage /> : <Navigate to="/" />}
        />
        <Route 
          path='/room' 
          element={isAuthenticated ? <RoomPage /> : <Navigate to="/" />}
        />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
