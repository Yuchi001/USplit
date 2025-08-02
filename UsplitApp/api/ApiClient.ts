import axios, {AxiosResponse} from "axios";

export class HttpClient {
    apiRoot: string;

    constructor(apiRoot: string) {
        this.apiRoot = apiRoot;
    }

    get<T = never>(route: string): Promise<AxiosResponse<T>> {
        return axios.get<T, AxiosResponse<T>>(this.apiRoute(route));
    }

    put<T = never>(route: string, data?: never): Promise<AxiosResponse<T>> {
        return axios.put<T, AxiosResponse<T>>(this.apiRoute(route), data);
    }

    post<T = never>(route: string, data?: never): Promise<AxiosResponse<T>> {
        return axios.post<T, AxiosResponse<T>>(this.apiRoute(route), data);
    }

    private apiRoute(route: string): string {
        return `${this.apiRoot}${route}`;
    }
}