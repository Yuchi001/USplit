export interface USplitAPI {
    // AUTH
    login(email: string, password: string);
    register(email: string, displayName: string, password: string);
    checkEmail(email: string);
    refreshToken(token: string);
    // END

    // USER
    getUserData();
    // END
}