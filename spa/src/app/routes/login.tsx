import {
    Button,
    Stack,
    TextInput,
    Container,
    Title,
    PasswordInput,
    Paper,
    Divider,
} from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { useForm, zodResolver } from '@mantine/form';

import {
    loginApplicationUserSchema,
    LoginApplicationUserInput,
    useLoginApplicationUser,
} from '@/features/application-user/api/login-application-user';

export const LoginRoute = () => {
    const navigate = useNavigate();

    const form = useForm<LoginApplicationUserInput>({
        initialValues: {
            email: '',
            password: '',
        },
        validate: zodResolver(loginApplicationUserSchema),
    });

    const loginSuccessCallback = () => {
        navigate('/');
    };

    const { mutate: login } = useLoginApplicationUser({
        mutationConfig: {
            onSuccess: loginSuccessCallback,
        },
    });

    const onLogin = (values: LoginApplicationUserInput) => {
        login({ data: values });
    };

    return (
        <Container size={420} my={40}>
            <Title order={1} ta='center'>
                Login
            </Title>

            <Paper withBorder shadow="sm" p="lg" mt="lg">
                <form onSubmit={form.onSubmit(onLogin)}>
                    <Stack>
                        <TextInput
                            label="Email"
                            placeholder="Enter your email"
                            {...form.getInputProps('email')}
                        />

                        <PasswordInput
                            label="Password"
                            placeholder="Enter your password"
                            {...form.getInputProps('password')}
                        />

                        <Button type="submit" fullWidth mt="md">
                            {'Sign in'}
                        </Button>
                    </Stack>
                </form>
                <Divider my="md" />
            </Paper>
        </Container>
    );
};