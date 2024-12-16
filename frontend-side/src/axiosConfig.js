import axios from 'axios';

const instance = axios.create({
    baseURL: 'http://localhost:5103', 
});

export default instance;
