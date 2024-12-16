import React, { useState } from 'react';
import axios from '../axiosConfig';

const FilterNotes = ({ setNotes, token }) => {
    const [category, setCategory] = useState('');

    const handleFilter = async () => {
        try {
            const response = category.trim() === ''
                ? await axios.get('/api/notes', {
                    headers: { Authorization: `Bearer ${token}` }
                })
                : await axios.get(`/api/notes/category/${encodeURIComponent(category)}`, {
                    headers: { Authorization: `Bearer ${token}` }
                });
            setNotes(response.data);
        } catch (error) {
            console.error('Error filtering notes:', error);
        }
    };

    return (
        <div className="filter-form">
            <input
                type="text"
                value={category}
                onChange={(e) => setCategory(e.target.value)}
                placeholder="Filter by category"
            />
            <button onClick={handleFilter}>Filter</button>
        </div>
    );
};

export default FilterNotes;
