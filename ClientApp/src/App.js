import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import Dashboard from './components/Dashboard';
import Bootcamps from './components/Bootcamps';
import Applications from './components/Applications';
import Applicants from './components/Applicants';
import Instructors from './components/Instructors';
import Login from './components/Login';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <nav className="navbar">
          <div className="container">
            <h1>Bootcamp Management System</h1>
            <div className="nav-links">
              <Link to="/">Dashboard</Link>
              <Link to="/bootcamps">Bootcamps</Link>
              <Link to="/applications">Applications</Link>
              <Link to="/applicants">Applicants</Link>
              <Link to="/instructors">Instructors</Link>
              <Link to="/login">Login</Link>
            </div>
          </div>
        </nav>

        <div className="container">
          <Routes>
            <Route path="/" element={<Dashboard />} />
            <Route path="/bootcamps" element={<Bootcamps />} />
            <Route path="/applications" element={<Applications />} />
            <Route path="/applicants" element={<Applicants />} />
            <Route path="/instructors" element={<Instructors />} />
            <Route path="/login" element={<Login />} />
          </Routes>
        </div>
      </div>
    </Router>
  );
}

export default App; 