import axios from 'axios';
import Constants from 'expo-constants';
import {tokenHandler} from "@/api/tokenHandler";

const api = axios.create({
    baseURL: Constants.expoConfig?.extra?.apiBaseUrl,
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json',
    },
});

api.interceptors.request.use(
    async (config) => {
        const token = await tokenHandler.getAccessToken();
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => Promise.reject(error)
);