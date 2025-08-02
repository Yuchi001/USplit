import Constants from 'expo-constants';
import {USplitAPI} from "@/api/USplitAPI";
import {HttpClient} from "@/api/ApiClient";
import {ApiRoutes} from "@/api/ApiRoutes";
import {User} from "@/api/models/User";
import {TokenPair} from "@/api/models/TokenPair";

export class Api implements USplitAPI {
    client: HttpClient;

    constructor() {
        this.client = new HttpClient(Constants.expoConfig?.extra?.apiBaseUrl)
    }

    // AUTH
    async checkEmail(email: string): Promise<boolean> {
        const res = await this.client.get(ApiRoutes.checkEmail(email));
        return res.data;
    }
    async login(email: string, password: string): Promise<TokenPair> {
        const res = await this.client.post(ApiRoutes.login(email, password))
        return res.data;
    }
    async refreshToken(token: string): Promise<TokenPair> {
        const res = await this.client.post(token);
        return res.data;
    }
    async register(email: string, displayName: string, password: string): Promise<User> {
        const res = await this.client.post(ApiRoutes.register(email, displayName, password));
        return res.data;
    }
    // END

    // USER
    async getUserData(): Promise<User> {
        const res = await this.client.get(ApiRoutes.getUserData);
        return res.data;
    }
    // END
}
