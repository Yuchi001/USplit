import FontAwesome from '@expo/vector-icons/FontAwesome';
import {DarkTheme, DefaultTheme, ThemeProvider} from '@react-navigation/native';
import {useFonts} from 'expo-font';
import {Stack} from 'expo-router';
import * as SplashScreen from 'expo-splash-screen';
import {useEffect} from 'react';
import 'react-native-reanimated';

import {useColorScheme} from '@/components/useColorScheme';
import {Api} from "@/api/api";
import axios from "axios";
import {tokenHandler} from "@/api/TokenHandler";
import {jwtDecode} from "jwt-decode";
import dayjs from "dayjs";
import {useAuth} from "@/hooks/useAuth";

export const api = new Api();
const { refreshTokenFunc } = useAuth();
axios.interceptors.request.use(async (config) => {
        const token = await tokenHandler.getAccessToken();
        const refreshToken = await tokenHandler.getRefreshToken();
        if (!token || !refreshToken) return config;

        const decodedToken = jwtDecode(token);
        const isExpired = dayjs.unix(decodedToken.exp ?? -1).diff(dayjs()) < 1;
        if (!isExpired) await refreshTokenFunc();

        config.headers.Authorization = `Bearer ${token}`;
        return config;
    },
    (error) => Promise.reject(error)
);

export {
    // Catch any errors thrown by the Layout component.
    ErrorBoundary,
} from 'expo-router';

export const unstable_settings = {
    // Ensure that reloading on `/modal` keeps a back button present.
    initialRouteName: '(tabs)',
};

// Prevent the splash screen from auto-hiding before asset loading is complete.
SplashScreen.preventAutoHideAsync();

export default function RootLayout() {
    const [loaded, error] = useFonts({
        SpaceMono: require('../assets/fonts/SpaceMono-Regular.ttf'),
        ...FontAwesome.font,
    });

    // Expo Router uses Error Boundaries to catch errors in the navigation tree.
    useEffect(() => {
        if (error) throw error;
    }, [error]);

    useEffect(() => {
        if (loaded) {
            SplashScreen.hideAsync();
        }
    }, [loaded]);

    if (!loaded) {
        return null;
    }

    return <RootLayoutNav/>;
}

function RootLayoutNav() {
    const colorScheme = useColorScheme();

    return (
        <ThemeProvider value={colorScheme === 'dark' ? DarkTheme : DefaultTheme}>
            <Stack>
                <Stack.Screen name="(tabs)" options={{headerShown: false}}/>
                <Stack.Screen name="modal" options={{presentation: 'modal'}}/>
            </Stack>
        </ThemeProvider>
    );
}
