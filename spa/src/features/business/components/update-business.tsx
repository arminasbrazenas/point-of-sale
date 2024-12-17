import {
    Button,
    Group,
    Modal,
    Stack,
    TextInput,
    Text,
    Paper,
} from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useBusiness } from '../api/get-business';
import { UpdateBusinessInput, useUpdateBusiness, updateBusinessInputSchema } from '../api/update-business';
import { useDeleteBusiness } from '../api/delete-business';
import { useAppStore } from '@/lib/app-store';

export const UpdateBusiness = ({ businessId }: { businessId: number }) => {
    const role = useAppStore((state) => state.applicationUser?.role);
    const businessQuery = useBusiness({ businessId });
    const navigate = useNavigate();
    const [updatedBusinessProperties, setUpdatedBusinessProperties] = useState<Partial<UpdateBusinessInput>>({});
    const [isDeleteModalOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

    const deleteBusinessMutation = useDeleteBusiness({
        mutationConfig: {
            onSuccess: () => {
                showNotification({
                    type: 'success',
                    title: 'Business deleted successfully.',
                });
                role === 'Admin' ? navigate(paths.businessManagement.businesses.getHref()) : navigate(paths.login.getHref());
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
                role === 'Admin' ? navigate(paths.businessManagement.businesses.getHref()) : navigate(paths.businessManagement.business.getHref());
            },
        },
    });

    const filterEmptyFields = (data: UpdateBusinessInput) => {
        return Object.fromEntries(
            Object.entries(data).filter(
                ([_, value]) => value !== undefined && value !== null && value.trim() !== ''
            )
        );
    };

    const form = useForm<UpdateBusinessInput>({
        mode: 'uncontrolled',
        initialValues: {
            name: '',
            address: '',
            email: '',
            phoneNumber: '',
        },
        validate: zodResolver(updateBusinessInputSchema),
        onValuesChange: (updatedValues) => {
            const business = businessQuery.data;
            if (!business) return;

            setUpdatedBusinessProperties({
                name: business.name === updatedValues.name?.trim() ? undefined : updatedValues.name?.trim() || '',
                address: business.address === updatedValues.address?.trim() ? undefined : updatedValues.address?.trim() || '',
                email: business.email === updatedValues.email?.trim() ? undefined : updatedValues.email?.trim() || '',
                phoneNumber:
                    business.phoneNumber === updatedValues.phoneNumber?.trim()
                        ? undefined
                        : updatedValues.phoneNumber?.trim() || '',
            });
        },
    });

    useEffect(() => {
        if (businessQuery.data) {
            const { name, address, email, phoneNumber } = businessQuery.data;
            form.setValues({ name, address, email, phoneNumber });
        }
    }, [businessQuery.data]);

    if (businessQuery.isLoading) {
        return <div>Loading...</div>;
    }

    const business = businessQuery.data;
    if (!business) {
        return <div>Business not found.</div>;
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
                    <TextInput label="Name" placeholder="Name" {...form.getInputProps('name')} />
                    <TextInput
                        label="Address"
                        placeholder="Address"
                        {...form.getInputProps('address')}
                    />
                    <TextInput
                        label="Email"
                        placeholder="Email"
                        {...form.getInputProps('email')}
                    />
                    <TextInput
                        label="Phone Number"
                        placeholder="PhoneNumber"
                        {...form.getInputProps('phoneNumber')}
                    />
                    <Group justify="space-between" mt="xs">
                        <Button color="red" variant="light" onClick={openDeleteModal}>
                            Delete
                        </Button>
                        <Group>
                            <Button
                                variant="default"
                                onClick={() =>
                                    role === 'BusinessOwner'
                                        ? navigate(paths.businessManagement.business.getHref())
                                        : navigate(paths.businessManagement.businesses.getHref())
                                }
                            >
                                Cancel
                            </Button>
                            <Button type="submit" loading={updateBusinessMutation.isPending}>
                                Save
                            </Button>
                        </Group>
                    </Group>
                </Stack>
            </form>

            <Modal
                opened={isDeleteModalOpen}
                onClose={closeDeleteModal}
                title="Delete business"
            >
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
                    <Button
                        color="red"
                        variant="light"
                        loading={deleteBusinessMutation.isPending}
                        onClick={deleteBusiness}
                    >
                        Delete
                    </Button>
                </Group>
            </Modal>
        </Paper>
    );
};
