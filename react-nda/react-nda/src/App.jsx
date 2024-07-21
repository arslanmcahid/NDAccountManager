import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { MsalProvider } from '@azure/msal-react';
import { msalInstance } from './azure-authConfig/authConfig'; // MSAL yapılandırma dosyanız

import Login from './pages/Login';
import Home from './pages/Home';
import Header from './components/Header';
import Footer from './components/Footer';
import "/Users/arslan/Desktop/NDAccountManager/react-nda/src/styles/style.css";

function App() {
  return (
    <MsalProvider instance={msalInstance}>
      <Router>
        <Header /> {/* Header bileşeni her sayfada görülecek */}
          <main className="main-content">
            <Routes>
              <Route path="/login" element={<Login />} />
              <Route path="/home" element={<Home />} />
              <Route path="/" element={<Login />} />
            </Routes>
          </main>
        <Footer /> {/* Footer bileşeni her sayfada görülecek */}
      </Router>
    </MsalProvider>
  );
}

export default App;
