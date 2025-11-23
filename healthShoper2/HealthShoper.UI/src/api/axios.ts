import axios from "axios";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL, // поменяй на свой URL
  withCredentials: true,           // если используются cookie
});

// Если нужно добавлять токен:
api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

export default api;