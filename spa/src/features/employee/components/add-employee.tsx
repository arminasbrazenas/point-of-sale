import { Button, Paper, Stack, TextInput, PasswordInput, Select } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useCreateEmployee } from '../api/create-employee';
import { useAppStore } from '@/lib/app-store';
import { useState } from 'react';
import { useBusinesses } from '@/features/business/api/get-businesses';
import { z } from 'zod';

export const AddEmployee = () => {
    const navigate = useNavigate();

    const role = useAppStore((state) => state.applicationUser?.role);
    const businessIdFromStore = useAppStore((state) => state.applicationUser?.businessId);

    const [selectedBusinessId, setSelectedBusinessId] = useState<string | null>(null);

    const businessesQuery = role === 'Admin'
        ? useBusinesses({ paginationFilter: { page: 1, itemsPerPage: 50 } })
        : { data: { items: [] }, isLoading: false };

    const createEmployeeFormInputSchema = z.object({
        firstName: z.string().min(1, 'First name is required'),
        lastName: z.string().min(1, 'Last name is required'),
        email: z.string().email('Invalid email address'),
        phoneNumber: z.string().min(1, 'Phone number is required'),
        password: z.string().min(6, 'Password must be at least 6 characters'),
        role: z.string(),
        businessId: z.number().nullable(),
    }).superRefine((data, ctx) => {
        if (data.role === 'Employee' && data.businessId === null) {
            ctx.addIssue({
                code: z.ZodIssueCode.custom,
                path: ['businessId'],
                message: 'Business ID is required when role is Employee',
            });
        }
    });

    type CreateEmployeeFormInput = z.infer<typeof createEmployeeFormInputSchema>;

    const initialRole = role === 'Admin' ? 'Employee' : 'Employee';

    const initialBusinessId = role === 'Admin' ? null : businessIdFromStore ?? null;

    const form = useForm<CreateEmployeeFormInput>({
        initialValues: {
            firstName: '',
            lastName: '',
            email: '',
            password: '',
            phoneNumber: '',
            role: initialRole,
            businessId: initialBusinessId,
        },
        validate: zodResolver(createEmployeeFormInputSchema),
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
            onError: (error) => {
                console.error('Error creating employee:', error);

                showNotification({
                    type: 'failure',
                    title: 'Failed to add employee',
                    message: error.message || 'An unexpected error occurred',
                });
            },
        },
    });

    const handleSubmit = (values: CreateEmployeeFormInput) => {
        if (values.role === 'Employee' && !values.businessId) {
          return;
        }
      
        createEmployeeMutation.mutate({
          data: values,
        });
      };

    if (role === 'Admin' && businessesQuery.isLoading) {
        return <div>Loading businesses...</div>;
    }

    const businesses = businessesQuery.data?.items || [];

    return (
        <Paper withBorder p="lg">
            <form onSubmit={form.onSubmit(handleSubmit)}>
                <Stack>
                    {role === 'Admin' && (
                        <Select
                            label="Business"
                            placeholder="Select a business"
                            data={businesses.map((b) => ({ value: b.id.toString(), label: b.name }))}
                            value={form.values.businessId?.toString() || ''}
                            onChange={(value) => form.setFieldValue('businessId', value ? Number(value) : null)}
                        />
                    )}

                    {role === 'Admin' && (
                        <Select
                            label="Role"
                            placeholder="Select a role"
                            withAsterisk
                            data={[
                                { value: 'Employee', label: 'Employee' },
                                { value: 'BusinessOwner', label: 'Business Owner' },
                            ]}
                            value={form.values.role}
                            onChange={(value) => form.setFieldValue('role', value || 'Employee')}
                        />
                    )}
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
