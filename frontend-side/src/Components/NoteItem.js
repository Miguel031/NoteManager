import React from 'react';
import axios from '../axiosConfig';

const NoteItem = ({ note, fetchNotes, handleEdit, token }) => {
    const archiveNote = async () => {
        await axios.patch(`/api/notes/${note.id}/archive`, {}, {
            headers: { Authorization: `Bearer ${token}` }
        });
        fetchNotes();
    };

    const unarchiveNote = async () => {
        await axios.patch(`/api/notes/${note.id}/unarchive`, {}, {
            headers: { Authorization: `Bearer ${token}` }
        });
        fetchNotes();
    };

    const deleteNote = async () => {
        await axios.delete(`/api/notes/${note.id}`, {
            headers: { Authorization: `Bearer ${token}` }
        });
        fetchNotes();
    };

    return (
        <li>
            <strong>{note.content}</strong> - {note.categories.join(', ')}
            <div>
                <button onClick={() => handleEdit(note)}>Edit</button>
                {!note.isArchived ? (
                    <button className="archive-button" onClick={archiveNote}>Archive</button>
                ) : (
                    <button className="unarchive-button" onClick={unarchiveNote}>Unarchive</button>
                )}
                <button className="delete-button" onClick={deleteNote}>Delete</button>
            </div>
        </li>
    );
};

export default NoteItem;
