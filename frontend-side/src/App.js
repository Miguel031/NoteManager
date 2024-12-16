import React, { useState, useEffect } from 'react';
import axios from './axiosConfig';
import NoteList from './Components/NoteList';
import NoteForm from './Components/NoteForm';
import FilterNotes from './Components/FilterNotes';
import Login from './Components/Login';
import './App.css';

function App() {
    const [notes, setNotes] = useState([]);
    const [editingNote, setEditingNote] = useState(null);
    const [token, setToken] = useState(localStorage.getItem('token') || null);

    const fetchNotes = async () => {
        if (token) {
            try {
                const response = await axios.get('/api/notes', {
                    headers: { Authorization: `Bearer ${token}` }
                });
                setNotes(response.data);
            } catch (error) {
                console.error('Error fetching notes:', error);
                if (error.response && error.response.status === 401) {
                    setToken(null);
                    localStorage.removeItem('token');
                }
            }
        }
    };

    useEffect(() => {
        fetchNotes();
    }, [token]);

    const handleEdit = (note) => {
        setEditingNote(note);
    };

    const handleLogout = () => {
        setToken(null);
        localStorage.removeItem('token');
    };

    return (
        <div className="App">
            <header className="App-header">
                <h1>Note App</h1>
                {token && <button className="logout-button" onClick={handleLogout}>Logout</button>}
            </header>
            <main>
                {!token ? (
                    <Login setToken={setToken} />
                ) : (
                    <>
                        <NoteForm fetchNotes={fetchNotes} editingNote={editingNote} setEditingNote={setEditingNote} token={token} />
                        <FilterNotes setNotes={setNotes} token={token} />
                        <NoteList notes={notes} fetchNotes={fetchNotes} handleEdit={handleEdit} token={token} />
                    </>
                )}
            </main>
        </div>
    );
}

export default App;
