import {useAuth} from "@/hooks/useAuth";
import {useEffect} from "react";
import axios from "axios";
import {tokenHandler} from "@/api/TokenHandler";
import {jwtDecode} from "jwt-decode";
import dayjs from "dayjs";

export const useAuthToken = () => {
    const { refreshTokenFunc } = useAuth();

    useEffect(() => {
        const interceptor = axios.interceptors.request.use(
            async (config) => {
                const token = await tokenHandler.getAccessToken();
                const refreshToken = await tokenHandler.getRefreshToken();
                if (!token || !refreshToken) return config;

                const decodedToken = jwtDecode(token);
                const isExpired = dayjs.unix(decodedToken.exp ?? -1).diff(dayjs()) < 1;

                if (isExpired) {
                    await refreshTokenFunc();
                }

                const newAccessToken = await tokenHandler.getAccessToken();
                config.headers.Authorization = `Bearer ${newAccessToken}`;
                return config;
            },
            (error) => Promise.reject(error)
        );

        return () => {
            axios.interceptors.request.eject(interceptor); // wyczyść interceptor przy unmount
        };
    }, [refreshTokenFunc]);
}