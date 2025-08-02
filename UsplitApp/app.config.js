import 'dotenv/config';

export default {
    expo: {
        name: 'App',
        slug: 'app',
        extra: {
            apiBaseUrl: process.env.API_BASE_URL
        }
    }
}