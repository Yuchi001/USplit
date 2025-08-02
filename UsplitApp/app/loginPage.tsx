import {View, Text} from "react-native";
import {TextInput} from "react-native-paper";
import {useState} from "react";
import * as yup from 'yup';
import {Formik} from "formik";

const LoginPage = () => {
    const schema = yup.object({
        email: yup.string()
            .required('This field is required')
            .email('This is not a valid email'),
        password: yup.string().required('This field is required'),
    });

    const handleSubmit = () => {

    }

    return <View>
        <Formik initialValues={{
            email: '',
            password: '',
        }} onSubmit={handleSubmit} validationSchema={schema}>
            {(props) => (
                <View>
                    <TextInput mode="outlined"
                               id="email"
                               label="E-mail"
                               textContentType="emailAddress"
                               value={props.values.email}
                               onChange={props.handleChange} />

                    <TextInput mode="outlined"
                               id="password"
                               label="Password"
                               textContentType="password"
                               value={props.values.password}
                               onChangeText={props.handleChange} />
                </View>
            )}
        </Formik>
    </View>
}

export default LoginPage;