export class ApiRoutes {
    // AUTH
    static login = (email: string, password: string) => `Auth/login?email=${email}&password=${password}`;
    static register = (email: string, displayName: string, password: string) => `Auth/register?email=${email}&password=${password}&displayName=${displayName}`;
    static checkEmail = (email: string) => `Auth/check-email?email=${email}`;
    static refreshToken = (token: string) => `Auth/refresh-token?token=${token}`;
    // END

    // USER
    static getUserData = 'User/get';
}