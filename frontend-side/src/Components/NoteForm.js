import React, { useState, useEffect } from 'react';
import axios from '../axiosConfig';

const NoteForm = ({ fetchNotes, editingNote, setEditingNote, token }) => {
    const [content, setContent] = useState('');
    const [tag, setTag] = useState('');
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        if (editingNote) {
            setContent(editingNote.content);
            setTag(editingNote.tag);
            setCategories(editingNote.categories || []); // Asegurar que categories sea un arreglo
        }
    }, [editingNote]);

    const validateFields = () => {
        if (!content.trim() || !tag.trim() || categories.length === 0) {
            return false;
        }
        return true;
    };

    const handleCategoryChange = (index, value) => {
        const newCategories = [...categories];
        newCategories[index] = value;
        setCategories(newCategories);
    };

    const addCategory = () => {
        setCategories([...categories, '']);
    };

    const removeCategory = (index) => {
        setCategories(categories.filter((_, i) => i !== index));
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        if (!validateFields()) {
            console.error('All fields are required');
            return;
        }

        try {
            const headers = { Authorization: `Bearer ${token}` };
            if (editingNote) {
                await axios.put(`/api/notes/${editingNote.id}`, {
                    id: editingNote.id,
                    content,
                    tag,
                    categories,
                    isArchived: editingNote.isArchived,
                }, { headers });
                setEditingNote(null); // Clear the editing note after successful update
            } else {
                await axios.post('/api/notes', {
                    content,
                    tag,
                    categories,
                    isArchived: false,
                }, { headers });
            }
            setContent('');
            setTag('');
            setCategories([]);
            fetchNotes();
        } catch (error) {
            console.error('Error saving note:', error);
            if (error.response) {
                console.error('Response data:', error.response.data);
                console.error('Response status:', error.response.status);
                console.error('Response headers:', error.response.headers);
            }
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Content:</label>
                <input
                    type="text"
                    value={content}
                    onChange={(e) => setContent(e.target.value)}
                />
            </div>
            <div>
                <label>Tag:</label>
                <input
                    type="text"
                    value={tag}
                    onChange={(e) => setTag(e.target.value)}
                />
            </div>
            <div>
                <label>Categories:</label>
                {categories.map((category, index) => (
                    <div key={index}>
                        <input
                            type="text"
                            value={category}
                            onChange={(e) => handleCategoryChange(index, e.target.value)}
                        />
                        <button type="button" onClick={() => removeCategory(index)}>Remove</button>
                    </div>
                ))}
                <button type="button" onClick={addCategory}>Add Category</button>
            </div>
            <button type="submit">{editingNote ? 'Update Note' : 'Add Note'}</button>
        </form>
    );
};

export default NoteForm;
