
import {BrowserRouter, Routes, Route} from 'react-router-dom';
import './App.css'
import LoginPage from "./Screen/Login/LoginPage.jsx";

function App() {
  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<LoginPage />} />
        </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
