import {Api} from "@/api/api";
import React from "react";
import {Stack} from "expo-router";
import {useAuthToken} from "@/hooks/useAuthToken";
import {PaperProvider} from "react-native-paper";

export const api = new Api();
export const NativeLayout = () => {
    useAuthToken();

    return  <PaperProvider>
        <Stack>
            <Stack.Screen name="loginPage" options={{ title: "Login" }}/>
        </Stack>
    </PaperProvider>
}