import axios from 'axios';

const baseURL = `https://localhost:7226`;

const axiosInstance = axios.create({
  baseURL,
  withCredentials: true,
});

export default axiosInstance;