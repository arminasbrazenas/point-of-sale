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
import { RegisterApplicationUserInput, registerApplicationUserSchema, useRegisterApplicationUser } from '@/features/application-user/api/register-application-user';

export const RegisterRoute = () => {
    const navigate = useNavigate();

    const form = useForm<RegisterApplicationUserInput>({
        initialValues: {
            firstName:'',
            lastName: '',
            phoneNumber: '',
            email: '',
            password: '',
            role: 'BusinessOwner',
        },
        validate: zodResolver(registerApplicationUserSchema),
    });

    const registerSuccessCallback = () => {
        navigate('/login');
    };

    const { mutate: register } = useRegisterApplicationUser({
        mutationConfig: {
            onSuccess: registerSuccessCallback,
        },
    });

    const onLRegister = (values: RegisterApplicationUserInput) => {
        register({ data: values });
    };

    return (
        <Container size={420} my={40}>
            <Title order={1} ta='center'>
                Register
            </Title>

            <Paper withBorder shadow="sm" p="lg" mt="lg">
                <form onSubmit={form.onSubmit(onLRegister)}>
                    <Stack>
                    <TextInput
                            label="First Name"
                            withAsterisk
                            placeholder="Enter your first name"
                            {...form.getInputProps('firstName')}
                        />
                        <TextInput
                            label="Last Name"
                            withAsterisk
                            placeholder="Enter your last name"
                            {...form.getInputProps('lastName')}
                        />
                        <TextInput
                            label="Phone Number"
                            withAsterisk
                            placeholder="Enter your phone number"
                            {...form.getInputProps('phoneNumber')}
                        />
                        <TextInput
                            label="Email"
                            withAsterisk
                            placeholder="Enter your email"
                            {...form.getInputProps('email')}
                        />
                        <PasswordInput
                            label="Password"
                            withAsterisk
                            placeholder="Enter your password"
                            {...form.getInputProps('password')}
                        />
                        <Button type="submit" fullWidth mt="md">
                            {'Sign up'}
                        </Button>
                    </Stack>
                </form>
                <Divider my="md" />
            </Paper>
        </Container>
    );
};