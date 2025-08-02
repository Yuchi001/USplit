import 'dotenv/config';

export default {
    expo: {
        name: 'USplitApp',
        slug: 'usplitapp',
        extra: {
            apiBaseUrl: process.env.API_BASE_URL
        }
    }
}