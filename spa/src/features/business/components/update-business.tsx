import { Button, Group, Modal, Stack, TextInput, Text, Paper, PasswordInput } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useBusiness } from '../api/get-business';
import { UpdateBusinessInput, useUpdateBusiness } from '../api/update-business';
import { useDeleteBusiness } from '../api/delete-business';
import { CreateBusinessInput, createBusinessInputSchema } from '../api/create-business';

export const UpdateBusiness = ({ businessId }: { businessId: number }) => {
    const businessQuery = useBusiness({ businessId });
    const navigate = useNavigate();
    const [updatedBusinessProperties, setUpdatedBusinessProperties] = useState<UpdateBusinessInput>({});
    const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

    const deleteBusinessMutation = useDeleteBusiness({
        mutationConfig: {
            onSuccess: () => {
                showNotification({
                    type: 'success',
                    title: 'Business deleted successfully.',
                });

                navigate(paths.businessManagement.businesses.getHref());
            },
        },
    });

    const updateBusinessMutation = useUpdateBusiness({
        mutationConfig: {
            onSuccess: () => {
                showNotification({
                    type: 'success',
                    title: 'Business updated successfully.',
                });

                setUpdatedBusinessProperties({});
            },
        },
    });

    const filterEmptyFields = (data: UpdateBusinessInput) => {
        return Object.fromEntries(
            Object.entries(data).filter(([_, value]) => value !== undefined && value !== null && value !== '')
        ) as UpdateBusinessInput;
    };

    const form = useForm<UpdateBusinessInput>({
        mode: 'uncontrolled',
        initialValues: {
            name: '',
            address: '',
            email: '',
            phoneNumber: '',
        },
        validate: zodResolver(createBusinessInputSchema),
        onValuesChange: (updatedBusiness) => {
            const business = businessQuery.data;
            if (!business) {
                setUpdatedBusinessProperties({});
                return;
            }

            setUpdatedBusinessProperties({
                name: business.name === updatedBusiness.name.trim() ? undefined : updatedBusiness.name,
                address: business.address === updatedBusiness.address.trim() ? undefined : updatedBusiness.address,
                email: business.email === updatedBusiness.email.trim() ? undefined : updatedBusiness.email,
                phoneNumber: business.phoneNumber === updatedBusiness.phoneNumber.trim() ? undefined : updatedBusiness.phoneNumber,
            });
        },
    });

    useEffect(() => {
        if (!businessQuery.data) {
            return;
        }

        form.setFieldValue('name', businessQuery.data.name);
        form.setFieldValue('address', businessQuery.data.address);
        form.setFieldValue('email', businessQuery.data.email);
        form.setFieldValue('phoneNumber', businessQuery.data.phoneNumber);
    }, [businessQuery.data]);

    if (businessQuery.isLoading) {
        return <div>loading...</div>;
    }

    const business = businessQuery.data;
    if (!business) {
        return null;
    }

    const deleteBusiness = () => {
        deleteBusinessMutation.mutate({ businessId });
    };

    const updateBusiness = () => {
        const filteredData = filterEmptyFields(updatedBusinessProperties);
        updateBusinessMutation.mutate({ businessId, data: filteredData });
    };

    return (
        <Paper withBorder p="lg">
            <form onSubmit={form.onSubmit(updateBusiness)}>
                <Stack>
                    <TextInput
                        label="Name"
                        placeholder="Name"
                        key={form.key('name')}
                        {...form.getInputProps('name')}
                    />
                    <TextInput
                        label="Address"
                        placeholder="Address"
                        key={form.key('address')}
                        {...form.getInputProps('address')}
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
                    <Group justify="space-between" mt="xs">
                        <Button color="red" variant="light" onClick={openDeleteModal}>
                            Delete
                        </Button>
                        <Group>
                            <Button variant="default" onClick={() => navigate(paths.businessManagement.businesses.getHref())}>
                                Cancel
                            </Button>
                            <Button type="submit" loading={updateBusinessMutation.isPending}>
                                Save
                            </Button>
                        </Group>
                    </Group>
                </Stack>
            </form>

            <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Delete business">
                <Text mt="md">
                    Are you sure you want to delete{' '}
                    <Text component="span" fw={600}>
                        {business.name}
                    </Text>
                    ?
                </Text>
                <Group mt="lg" justify="flex-end">
                    <Button variant="default" onClick={closeDeleteModal}>
                        Cancel
                    </Button>
                    <Button color="red" variant="light" loading={deleteBusinessMutation.isPending} onClick={deleteBusiness}>
                        Delete
                    </Button>
                </Group>
            </Modal>
        </Paper>
    );
};
