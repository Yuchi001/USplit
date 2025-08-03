import axios, {AxiosInstance, AxiosResponse} from "axios";
import {tokenHandler} from "@/api/TokenHandler";
import {jwtDecode} from "jwt-decode";
import dayjs from "dayjs";

export class HttpClient {
    private readonly axiosInstance: AxiosInstance;

    constructor(apiRoot: string) {
        // noinspection JSAnnotator
        this.axiosInstance = axios.create({
            baseURL: apiRoot,
            timeout: 10000,
            headers: {
                "Content-Type": "application/json",
            },
        });
    }

    public initInterceptor = async (refreshTokenFunc: () => Promise<boolean>) => {
        this.axiosInstance.interceptors.request.use(
            (config) => {
                return new Promise(async (resolve, reject) => {
                    try {
                        const token = await tokenHandler.getAccessToken();
                        const refreshToken = await tokenHandler.getRefreshToken();
                        if (!token || !refreshToken) return resolve(config);

                        const decodedToken = jwtDecode(token);
                        const isExpired = dayjs.unix(decodedToken.exp ?? -1).diff(dayjs()) < 1;

                        if (isExpired) {
                            await refreshTokenFunc();
                        }

                        const newAccessToken = await tokenHandler.getAccessToken();
                        config.headers.Authorization = `Bearer ${newAccessToken}`;

                        resolve(config);
                    } catch (error) {
                        reject(error);
                    }
                });
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

}