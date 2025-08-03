import {View, Text, StyleSheet} from "react-native";
import {Button, Dialog, HelperText, Portal, TextInput} from "react-native-paper";
import * as yup from 'yup';
import {Formik, FormikHelpers} from "formik";
import {useAuth} from "@/hooks/useAuth";
import Values from "ajv/lib/vocabularies/jtd/values";
import {useState} from "react";
import { Link } from 'expo-router';

const LoginPage = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [isError, setIsError] = useState(false);
    const [hidePassword, setHidePassword] = useState(true);
    const { loginFunc } = useAuth();

    const schema = yup.object({
        email: yup.string()
            .required('This field is required')
            .email('This is not a valid email'),
        password: yup.string().required('This field is required'),
    });

    const handleSubmit = async (values: Values, formikHelpers: FormikHelpers<Values>) => {
        setIsLoading(true);
        const logged = await loginFunc(values['email'], values['password']);
        setIsLoading(false);
        setIsError(true);
        if (!logged) return;


    }

    // noinspection TypeScriptValidateTypes
    return <View>
        <Formik initialValues={{
            email: '',
            password: '',
        }} onSubmit={handleSubmit} validationSchema={schema}>
            {(props) => (
                <View style={styles.container}>
                    <Text style={styles.title}>Welcome Back</Text>
                    <TextInput mode="flat"
                               error={props.errors['email']}
                               id="email"
                               label="E-mail"
                               textContentType="emailAddress"
                               value={props.values.email}
                               onChangeText={props.handleChange('email')} />
                    <HelperText type="error" visible={props.errors['email']}>
                        <Text>{props.errors['email']}</Text>
                    </HelperText>

                    <TextInput mode="flat"
                               error={props.errors['password']}
                               id="password"
                               label="Password"
                               secureTextEntry={hidePassword}
                               right={<TextInput.Icon icon={hidePassword ? 'eye' : 'eye-off'} onPress={() => setHidePassword(!hidePassword)} />}
                               textContentType="password"
                               value={props.values.password}
                               onChangeText={props.handleChange('password')} />
                    <HelperText type="error" visible={props.errors['password']}>
                        <Text>{props.errors['password']}</Text>
                    </HelperText>

                    <Button loading={isLoading} mode="elevated" onPress={props.handleSubmit}>
                        <Text>SIGN IN</Text>
                    </Button>
                    <HelperText type="info">
                        <Text>
                            Don't have an account? You can <Link style={styles.link} href="/register">register here</Link>.
                        </Text>
                    </HelperText>
                </View>
            )}
        </Formik>
        <Portal>
            <Dialog visible={isError} onDismiss={() => setIsError(false)}>
                <Dialog.Title>
                    <Text>Can not perform login!</Text>
                </Dialog.Title>
                <Dialog.Content>
                    <Text>Something went wrong while trying to log in!</Text>
                </Dialog.Content>
                <Dialog.Actions>
                    <Button onPress={() => setIsError(false)}>
                        <Text>Ok</Text>
                    </Button>
                </Dialog.Actions>
            </Dialog>
        </Portal>
    </View>
}


const styles = StyleSheet.create({
    link: {
        color: 'blue',
        textDecorationLine: 'underline',
    },
    container: {
        marginTop: 15,
        marginLeft: 30,
        marginRight: 30,
        gap: 2,
    },
    title: {
        fontSize: 40,
        alignSelf: "center",
        marginBottom: 20,
        fontWeight: "light"
    }
});

export default LoginPage;