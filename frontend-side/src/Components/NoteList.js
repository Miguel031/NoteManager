import React from 'react';
import NoteItem from './NoteItem';

const NoteList = ({ notes, fetchNotes, handleEdit, token }) => {
    return (
        <div>
            <h2>Active Notes</h2>
            <ul className="note-list">
                {notes.filter(note => !note.isArchived).map(note => (
                    <NoteItem key={note.id} note={note} fetchNotes={fetchNotes} handleEdit={handleEdit} token={token} />
                ))}
            </ul>
            <h2>Archived Notes</h2>
            <ul className="note-list">
                {notes.filter(note => note.isArchived).map(note => (
                    <NoteItem key={note.id} note={note} fetchNotes={fetchNotes} handleEdit={handleEdit} token={token} />
                ))}
            </ul>
        </div>
    );
};

export default NoteList;
