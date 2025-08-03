import React, {useEffect, useState} from "react";
import {tokenHandler} from "@/api/TokenHandler";
import {User} from "@/api/models/User";
import {AuthContext, AuthContextType} from "@/contexts/auth/AuthContext";
import {api} from "@/app/_layout";

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
    const [user, setUser] = useState<User | null>(null);
    const logged = user != null;

    useEffect(() => {
        async function initInterceptor(): Promise<void> {
            await api.initInterceptor(refreshTokenFunc)
        }

        initInterceptor();
    }, []);

    const loginFunc = async (email: string, password: string): Promise<boolean> => {
        const tokenPair = await api.login(email, password);
        if (!tokenPair) return false;

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

    const providerObject: AuthContextType = {
        logged,
        user,
        loginFunc,
        registerFunc,
        checkEmailFunc,
        refreshTokenFunc,
    } as AuthContextType;

    return (
        <AuthContext.Provider value={providerObject}>
            {children}
        </AuthContext.Provider>
    )
}