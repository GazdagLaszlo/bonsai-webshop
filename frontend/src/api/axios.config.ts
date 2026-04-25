import axios from "axios";
import { tokenKeyName } from "@/constants/constants";
import { refreshAccessToken } from "./RefreshAccessToken";

const baseURL = `https://localhost:7226`;

const axiosInstance = axios.create({
  baseURL,
  withCredentials: true,
});

axiosInstance.interceptors.request.use(
  (config) => {
    const accessToken = sessionStorage.getItem(tokenKeyName);
    if (accessToken) {
      config.headers = config.headers ?? {};
      config.headers["Authorization"] = `Bearer ${accessToken}`;
    }
    return config;
  },
  (error) => Promise.reject(error),
);

interface FailedRequest {
  resolve: (token: string | null) => void;
  reject: (error: unknown) => void;
}

let isRefreshing = false;
let failedQueue: FailedRequest[] = [];

const processQueue = (
  error: Error | null | unknown,
  token: string | null = null,
) => {
  failedQueue.forEach((prom) => {
    if (error) prom.reject(error);
    else prom.resolve(token);
  });
  failedQueue = [];
};

axiosInstance.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (originalRequest.url?.includes("/Login")) {
      return Promise.reject(error);
    }

    if (error.response?.status === 401 && !originalRequest._retry) {
      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject });
        })
          .then((token) => {
            originalRequest.headers["Authorization"] = `Bearer ${token}`;
            return axiosInstance(originalRequest);
          })
          .catch((err) => Promise.reject(err));
      }

      originalRequest._retry = true;
      isRefreshing = true;

      try {
        const newToken = await refreshAccessToken();
        isRefreshing = false;
        if (newToken) {
          processQueue(null, newToken);
          originalRequest.headers["Authorization"] = `Bearer ${newToken}`;
          return axiosInstance(originalRequest);
        }
      } catch (refreshError) {
        isRefreshing = false;
        processQueue(refreshError, null);

        // logout
        sessionStorage.removeItem(tokenKeyName);
        window.location.href = "/login";        
        return Promise.reject(refreshError);
      }
    }
    return Promise.reject(error);
  },
);

export default axiosInstance;
