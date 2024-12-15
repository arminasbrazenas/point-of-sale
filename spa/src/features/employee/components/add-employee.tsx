import { Button, Paper, Stack, TextInput, PasswordInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { CreateEmployeeInput, createEmployeeInputSchema, useCreateEmployee } from '../api/create-employee';
import { useAppStore } from '@/lib/app-store';

export const AddEmployee = () => {
    const navigate = useNavigate();

    const businessId = useAppStore((state) => state.applicationUser?.businessId);

    if (!businessId) {
        throw new Error('Business ID is required to create an employee.');
    }

    const form = useForm<CreateEmployeeInput>({
        mode: 'uncontrolled',
        initialValues: {
            firstName: '',
            lastName: '',
            email: '',
            password: '',
            phoneNumber: '',
        },
        validate: zodResolver(createEmployeeInputSchema),
    });

    const createEmployeeMutation = useCreateEmployee({
        mutationConfig: {
            onSuccess: () => {
                showNotification({
                    type: 'success',
                    title: 'Employee added successfully.',
                });

                navigate(paths.businessManagement.employees.getHref());
            },
        },
    });

    const handleSubmit = (values: CreateEmployeeInput) => {
        createEmployeeMutation.mutate({
            data: { ...values, businessId }
        });
    };

    return (
        <Paper withBorder p="lg">
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <Stack>
                    <TextInput
                        label="First Name"
                        placeholder="FirstName"
                        withAsterisk
                        key={form.key('firstName')}
                        {...form.getInputProps('firstName')}
                    />
                    <TextInput
                        label="Last Name"
                        placeholder="LastName"
                        withAsterisk
                        key={form.key('lastName')}
                        {...form.getInputProps('lastName')}
                    />
                    <TextInput
                        label="Email"
                        placeholder="Email"
                        withAsterisk
                        key={form.key('email')}
                        {...form.getInputProps('email')}
                    />
                    <TextInput
                        label="Phone Number"
                        placeholder="PhoneNumber"
                        withAsterisk
                        key={form.key('phoneNumber')}
                        {...form.getInputProps('phoneNumber')}
                    />
                    <PasswordInput
                        label="Password"
                        placeholder="Password"
                        withAsterisk
                        key={form.key('password')}
                        {...form.getInputProps('password')}
                    />
                    <Button type="submit" mt="xs" fullWidth loading={createEmployeeMutation.isPending}>
                        Add
                    </Button>
                </Stack>
            </form>
        </Paper>
    );
};
