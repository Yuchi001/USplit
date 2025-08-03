import {View} from "react-native";
import {useAuth} from "@/hooks/useAuth";
import {useEffect} from "react";
import {router} from "expo-router";

const HomePage = () => {
    const { logged } = useAuth();

    useEffect(() => {
        const timeout = setTimeout(() => {
            if (logged) return;

            router.replace('/login');
        }, 0);

        return () => clearTimeout(timeout);
    }, []);

    return <View>
        This is home
    </View>
}

export default HomePage;