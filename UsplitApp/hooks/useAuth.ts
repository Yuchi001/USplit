import {useState} from "react";
import {api} from "@/app/_layout";
import {tokenHandler} from "@/api/TokenHandler";
import {User} from "@/api/models/User";

export const useAuth = () => {
    const [user, setUser] = useState<User | null>(null);
    const logged = user != null;

    const loginFunc = async (email: string, password: string): Promise<boolean> => {
        const tokenPair = await api.login(email, password);
        if (!tokenPair) return false;

        console.log(tokenPair);

        await tokenHandler.saveTokens(tokenPair);

        const loggedUser = await api.getUserData();

        setUser(loggedUser);

        return true;
    }

    const registerFunc = async (email: string, displayName: string, password: string) => {
        await api.register(email, displayName, password);
        await loginFunc(email, password);
    }

    const refreshTokenFunc = async (): Promise<boolean> => {
        const refreshToken = await tokenHandler.getRefreshToken();
        if (!refreshToken) return false;

        const tokenPair = await api.refreshToken(refreshToken);
        await tokenHandler.saveTokens(tokenPair);

        if (logged) return true;

        const loggedUser = await api.getUserData();
        setUser(loggedUser);
        return true;
    }

    const checkEmailFunc = async (email: string): Promise<boolean> => await api.checkEmail(email);

    return {
        logged,
        loginFunc,
        registerFunc,
        checkEmailFunc,
        refreshTokenFunc
    }
}