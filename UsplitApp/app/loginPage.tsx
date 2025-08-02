import {View, Text} from "react-native";
import {Button, HelperText, TextInput} from "react-native-paper";
import {ReactNode, useState} from "react";
import * as yup from 'yup';
import {Formik, FormikHelpers, FormikProps, FormikValues} from "formik";
import {JSXElement} from "@babel/types";
import {useAuth} from "@/hooks/useAuth";
import Values from "ajv/lib/vocabularies/jtd/values";

const LoginPage = () => {
    const { loginFunc } = useAuth();

    const schema = yup.object({
        email: yup.string()
            .required('This field is required')
            .email('This is not a valid email'),
        password: yup.string().required('This field is required'),
    });

    const handleSubmit = async (values: Values, formikHelpers: FormikHelpers<Values>) => {
        await loginFunc(values['email'], values['password']);
    }

    // noinspection TypeScriptValidateTypes
    return <View>
        <Formik initialValues={{
            email: '',
            password: '',
        }} onSubmit={handleSubmit} validationSchema={schema}>
            {(props) => (
                <View>
                    <TextInput mode="outlined"
                               error={props.errors['email']}
                               id="email"
                               label="E-mail"
                               textContentType="emailAddress"
                               value={props.values.email}
                               onChange={props.handleChange} />
                    <HelperText type="error" visible={props.errors['email']}>
                        {props.errors['email']}
                    </HelperText>

                    <TextInput mode="outlined"
                               error={props.errors['password']}
                               id="password"
                               label="Password"
                               textContentType="password"
                               value={props.values.password}
                               onChange={props.handleChange} />
                    <HelperText type="error" visible={props.errors['password']}>
                        {props.errors['password']}
                    </HelperText>

                    <Button mode="elevated" onPress={props.handleSubmit}>
                        Submit
                    </Button>
                </View>
            )}
        </Formik>
    </View>
}

export default LoginPage;