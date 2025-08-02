import axios, {AxiosInstance, AxiosResponse} from "axios";
import {tokenHandler} from "@/api/TokenHandler";
import {jwtDecode} from "jwt-decode";
import dayjs from "dayjs";
import {useAuth} from "@/hooks/useAuth";

//const { refreshTokenFunc } = useAuth();

export class HttpClient {
    private readonly axiosInstance: AxiosInstance;

    constructor(apiRoot: string) {
        this.axiosInstance = axios.create({
            baseURL: apiRoot,
            headers: {
                "Content-Type": "application/json",
            },
        });

        this.axiosInstance.interceptors.request.use(
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
    }

    get<T = never>(route: string): Promise<AxiosResponse<T>> {
        return this.axiosInstance.get<T, AxiosResponse<T>>(route);
    }

    put<T = never>(route: string, data?: never): Promise<AxiosResponse<T>> {
        return this.axiosInstance.put<T, AxiosResponse<T>>(route, data);
    }

    post<T = never>(route: string, data?: never): Promise<AxiosResponse<T>> {
        return this.axiosInstance.post<T, AxiosResponse<T>>(route, data);
    }

    get instance(): AxiosInstance {
        return this.axiosInstance;
    }
}