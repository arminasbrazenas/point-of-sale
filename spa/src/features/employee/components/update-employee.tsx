import { Button, Group, Modal, NumberInput, Stack, TextInput, Text, Paper, PasswordInput } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useMemo, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useEmployee } from '../api/get-employee';
import { UpdateEmployeeInput, useUpdateEmployee } from '../api/update-employee';
import { useDeleteEmployee } from '../api/delete-employee';
import { CreateEmployeeInput, createEmployeeInputSchema } from '../api/create-employee';

export const UpdateEmployee = ({ employeeId }: { employeeId: number }) => {
    const employeeQuery = useEmployee({ employeeId });
    const navigate = useNavigate();
    const [updatedEmployeeProperties, setUpdatedEmployeeProperties] = useState<UpdateEmployeeInput>({});
    const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

    const deleteEmployeeMutation = useDeleteEmployee({
        mutationConfig: {
            onSuccess: () => {
                showNotification({
                    type: 'success',
                    title: 'Employee deleted successfully.',
                });

                navigate(paths.businessManagement.employees.getHref());
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
            },
        },
    });

    const filterEmptyFields = (data: UpdateEmployeeInput) => {
        return Object.fromEntries(
            Object.entries(data).filter(([_, value]) => value !== undefined && value !== null && value !== '')
        ) as UpdateEmployeeInput;
    };

    const form = useForm<CreateEmployeeInput>({
        mode: 'uncontrolled',
        initialValues: {
            firstName: '',
            lastName: '',
            email: '',
            phoneNumber: '',
            password: '',
        },
        validate: zodResolver(createEmployeeInputSchema),
        onValuesChange: (updatedEmployee) => {
            const employee = employeeQuery.data;
            if (!employee) {
                setUpdatedEmployeeProperties({});
                return;
            }

            setUpdatedEmployeeProperties({
                firstName: employee.firstName === updatedEmployee.firstName.trim() ? undefined : updatedEmployee.firstName,
                lastName: employee.lastName === updatedEmployee.lastName.trim() ? undefined : updatedEmployee.lastName,
                email: employee.email === updatedEmployee.email.trim() ? undefined : updatedEmployee.email,
                phoneNumber: employee.phoneNumber === updatedEmployee.phoneNumber.trim() ? undefined : updatedEmployee.phoneNumber,
            });
        },
    });

    useEffect(() => {
        if (!employeeQuery.data) {
            return;
        }

        form.setFieldValue('firstName', employeeQuery.data.firstName);
        form.setFieldValue('lastName', employeeQuery.data.lastName);
        form.setFieldValue('email', employeeQuery.data.email);
        form.setFieldValue('phoneNumber', employeeQuery.data.phoneNumber);
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