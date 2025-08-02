// src/api/tokenService.ts
import AsyncStorage from '@react-native-async-storage/async-storage';
import {TokenPair} from "@/api/models/TokenPair";

const ACCESS_TOKEN_KEY = 'access_token';
const REFRESH_TOKEN_KEY = 'refresh_token';

export const tokenHandler = {
    // Access token
    async saveAccessToken(token: string) {
        await AsyncStorage.setItem(ACCESS_TOKEN_KEY, token);
    },

    async getAccessToken(): Promise<string | null> {
        return AsyncStorage.getItem(ACCESS_TOKEN_KEY);
    },

    async removeAccessToken() {
        await AsyncStorage.removeItem(ACCESS_TOKEN_KEY);
    },

    async saveRefreshToken(token: string) {
        await AsyncStorage.setItem(REFRESH_TOKEN_KEY, token);
    },

    async getRefreshToken(): Promise<string | null> {
        return AsyncStorage.getItem(REFRESH_TOKEN_KEY);
    },

    async removeRefreshToken() {
        await AsyncStorage.removeItem(REFRESH_TOKEN_KEY);
    },

    async saveTokens(pair: TokenPair) {
        await Promise.all([
            this.saveRefreshToken(pair.refresh),
            this.saveAccessToken(pair.token),
        ]);
    },

    async clearTokens() {
        await Promise.all([
            AsyncStorage.removeItem(ACCESS_TOKEN_KEY),
            AsyncStorage.removeItem(REFRESH_TOKEN_KEY),
        ]);
    },
};
