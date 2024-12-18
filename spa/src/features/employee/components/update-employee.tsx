import { Button, Group, Modal, Stack, TextInput, Text, Paper, PasswordInput } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useEmployee } from '../api/get-employee';
import { UpdateEmployeeInput, updateEmployeeInputSchema, useUpdateEmployee } from '../api/update-employee';
import { useDeleteEmployee } from '../api/delete-employee';
import { useAppStore } from '@/lib/app-store';
import { logoutApplicationUser, useLogoutApplicationUser } from '@/features/application-user/api/logout-application-user';

export const UpdateEmployee = ({ employeeId }: { employeeId: number }) => {
    const employeeQuery = useEmployee({
        employeeId
    });
    const navigate = useNavigate();
    const [updatedEmployeeProperties, setUpdatedEmployeeProperties] = useState<Partial<UpdateEmployeeInput>>({});
    const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);
    const userId = useAppStore((state) => state.applicationUser?.id);
    const resetApplicationUser = useAppStore((state) => state.resetApplicationUser);

    const deleteEmployeeMutation = useDeleteEmployee({
        mutationConfig: {
            onSuccess: () => {
                showNotification({
                    type: 'success',
                    title: 'Employee deleted successfully.',
                });

                if (employeeId === userId) {
                    logoutApplicationUser();
                    resetApplicationUser();
                    navigate(paths.login.getHref());
                } else {
                    navigate(paths.businessManagement.employees.getHref());
                }
            },
        },
    });

    const updateEmployeeMutation = useUpdateEmployee({
        mutationConfig: {
            onSuccess: () => {
                showNotification({
                    type: 'success',
                    title: 'Employee updated successfully.',
                });

                setUpdatedEmployeeProperties({});

                navigate(paths.businessManagement.employees.getHref());
            },
        },
    });

    const filterEmptyFields = (data: UpdateEmployeeInput) => {
        return Object.fromEntries(
            Object.entries(data).filter(([_, value]) => value !== undefined && value !== null && value !== '')
        ) as UpdateEmployeeInput;
    };

    const form = useForm<UpdateEmployeeInput>({
        mode: 'uncontrolled',
        initialValues: {
            firstName: '',
            lastName: '',
            email: '',
            phoneNumber: '',
            password: '',
        },
        validate: zodResolver(updateEmployeeInputSchema),
        onValuesChange: (updatedEmployee) => {
            const employee = employeeQuery.data;
            if (!employee) {
                setUpdatedEmployeeProperties({});
                return;
            }

            setUpdatedEmployeeProperties({
                firstName: employee.firstName === updatedEmployee.firstName?.trim() ? undefined : updatedEmployee.firstName?.trim() || '',
                lastName: employee.lastName === updatedEmployee.lastName?.trim() ? undefined : updatedEmployee.lastName?.trim() || '',
                email: employee.email === updatedEmployee.email?.trim() ? undefined : updatedEmployee.email?.trim() || '',
                phoneNumber: employee.phoneNumber === updatedEmployee.phoneNumber?.trim()
                    ? undefined
                    : updatedEmployee.phoneNumber?.trim() || '',
                password: updatedEmployee.password?.trim() || '',
            });

        },
    });

    useEffect(() => {
        if (!employeeQuery.data) {
            return;
        }

        const { firstName, lastName, email, phoneNumber } = employeeQuery.data;

        form.setFieldValue('firstName', firstName);
        form.setFieldValue('lastName', lastName);
        form.setFieldValue('email', email);
        form.setFieldValue('phoneNumber', phoneNumber);
    }, [employeeQuery.data]);

    if (employeeQuery.isLoading) {
        return <div>loading...</div>;
    }

    const employee = employeeQuery.data;
    if (!employee) {
        return null;
    }

    const deleteEmployee = () => {
        deleteEmployeeMutation.mutate({ employeeId });
    };

    const updateEmployee = () => {
        const filteredData = filterEmptyFields(updatedEmployeeProperties);
        updateEmployeeMutation.mutate({ employeeId, data: filteredData });
    };

    return (
        <Paper withBorder p="lg">
            <form onSubmit={form.onSubmit(updateEmployee)}>
                <Stack>
                    <TextInput
                        label="First Name"
                        placeholder="FirstName"
                        key={form.key('firstName')}
                        {...form.getInputProps('firstName')}
                    />
                    <TextInput
                        label="Last Name"
                        placeholder="LastName"
                        key={form.key('lastName')}
                        {...form.getInputProps('lastName')}
                    />
                    <TextInput
                        label="Email"
                        placeholder="Email"
                        key={form.key('email')}
                        {...form.getInputProps('email')}
                    />
                    <TextInput
                        label="Phone Number"
                        placeholder="PhoneNumber"
                        key={form.key('phoneNumber')}
                        {...form.getInputProps('phoneNumber')}
                    />
                    <PasswordInput
                        label="Password"
                        placeholder="Password"
                        key={form.key('password')}
                        {...form.getInputProps('password')}
                    />
                    <Group justify="space-between" mt="xs">
                        <Button color="red" variant="light" onClick={openDeleteModal}>
                            Delete
                        </Button>
                        <Group>
                            <Button variant="default" onClick={() => navigate(paths.businessManagement.employees.getHref())}>
                                Cancel
                            </Button>
                            <Button type="submit" loading={updateEmployeeMutation.isPending}>
                                Save
                            </Button>
                        </Group>
                    </Group>
                </Stack>
            </form>

            <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Delete employee">
                <Text mt="md">
                    Are you sure you want to delete{' '}
                    <Text component="span" fw={600}>
                        {employee.firstName}
                    </Text>
                    ?
                </Text>
                <Group mt="lg" justify="flex-end">
                    <Button variant="default" onClick={closeDeleteModal}>
                        Cancel
                    </Button>
                    <Button color="red" variant="light" loading={deleteEmployeeMutation.isPending} onClick={deleteEmployee}>
                        Delete
                    </Button>
                </Group>
            </Modal>
        </Paper>
    );
};
