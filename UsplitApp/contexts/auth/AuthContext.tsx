import {User} from "@/api/models/User";
import {createContext} from "react";

export type AuthContextType = {
    logged: boolean,
    user: User | null,
    loginFunc: (email: string, password: string) => Promise<boolean>,
    registerFunc: (email: string, displayName: string, password: string) => Promise<User>,
    checkEmailFunc: (email: string) => Promise<boolean>,
    refreshTokenFunc: () => Promise<boolean>
};

export const AuthContext = createContext<AuthContextType | undefined>(undefined);