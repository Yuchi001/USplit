import React from "react";
import {MD3LightTheme as DefaultTheme, PaperProvider} from "react-native-paper";
import {AuthProvider} from "@/contexts/auth/AuthProvider";
import {Stack} from "expo-router";
import {Api} from "@/api/api";

export const api = new Api();
const theme = {
    ...DefaultTheme,
    colors: {
        "primary": "rgb(168, 54, 57)",
        "onPrimary": "rgb(255, 255, 255)",
        "primaryContainer": "rgb(255, 218, 216)",
        "onPrimaryContainer": "rgb(65, 0, 6)",
        "secondary": "rgb(151, 72, 18)",
        "onSecondary": "rgb(255, 255, 255)",
        "secondaryContainer": "rgb(255, 219, 202)",
        "onSecondaryContainer": "rgb(51, 18, 0)",
        "tertiary": "rgb(140, 80, 0)",
        "onTertiary": "rgb(255, 255, 255)",
        "tertiaryContainer": "rgb(255, 220, 191)",
        "onTertiaryContainer": "rgb(45, 22, 0)",
        "error": "rgb(186, 26, 26)",
        "onError": "rgb(255, 255, 255)",
        "errorContainer": "rgb(255, 218, 214)",
        "onErrorContainer": "rgb(65, 0, 2)",
        "background": "rgb(255, 251, 255)",
        "onBackground": "rgb(32, 26, 26)",
        "surface": "rgb(255, 251, 255)",
        "onSurface": "rgb(32, 26, 26)",
        "surfaceVariant": "rgb(244, 221, 220)",
        "onSurfaceVariant": "rgb(82, 67, 66)",
        "outline": "rgb(133, 115, 114)",
        "outlineVariant": "rgb(215, 193, 192)",
        "shadow": "rgb(0, 0, 0)",
        "scrim": "rgb(0, 0, 0)",
        "inverseSurface": "rgb(54, 47, 46)",
        "inverseOnSurface": "rgb(251, 238, 237)",
        "inversePrimary": "rgb(255, 179, 176)",
        "elevation": {
            "level0": "transparent",
            "level1": "rgb(251, 241, 245)",
            "level2": "rgb(248, 235, 239)",
            "level3": "rgb(245, 229, 233)",
            "level4": "rgb(245, 227, 231)",
            "level5": "rgb(243, 223, 227)"
        },
        "surfaceDisabled": "rgba(32, 26, 26, 0.12)",
        "onSurfaceDisabled": "rgba(32, 26, 26, 0.38)",
        "backdrop": "rgba(59, 45, 44, 0.4)"
    }
};
export default function RootLayout() {
    return  <AuthProvider>
        <PaperProvider theme={theme}>
            <Stack
                screenOptions={{
                    headerStyle: {
                        backgroundColor: '#F88379', // np. łososiowy
                    },
                    headerTintColor: '#fff', // kolor ikon i tekstu w headerze (np. biały)
                    headerTitleStyle: {
                        fontWeight: 'bold',
                    },
                    contentStyle: { backgroundColor: '#FDEDEF' },
                }}
            >
                <Stack.Screen name="index" options={{ title: "Home" }}/>
                <Stack.Screen name="login" options={{ title: "Login" }}/>
            </Stack>
        </PaperProvider>
    </AuthProvider>
}