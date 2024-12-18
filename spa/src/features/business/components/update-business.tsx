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
import { TimeInput } from '@mantine/dates';
import { z } from 'zod';
import { logoutApplicationUser } from '@/features/application-user/api/logout-application-user';

export const UpdateBusiness = ({ businessId }: { businessId: number }) => {
  const role = useAppStore((state) => state.applicationUser?.role);
  const businessQuery = useBusiness({ businessId });
  const navigate = useNavigate();
  const [updatedBusinessProperties, setUpdatedBusinessProperties] = useState<Partial<UpdateBusinessInput>>({});
  const [isDeleteModalOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);
  const resetApplicationUser = useAppStore.getState().resetApplicationUser;

  const deleteBusinessMutation = useDeleteBusiness({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Business deleted successfully.',
        });
        if (role == 'Admin')
          navigate(paths.businessManagement.businesses.getHref())
        else {
          logoutApplicationUser();
          resetApplicationUser();
        }
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
        role === 'Admin'
          ? navigate(paths.businessManagement.businesses.getHref())
          : navigate(paths.businessManagement.business.getHref());
      },
    },
  });

  const updateBusinessFormInputSchema = z.object({
    name: z.string(),
    address: z.string(),
    email: z.string(),
    phoneNumber: z.string(),
    startTime: z.string().regex(/\d{2}:\d{2}/, 'Invalid time format'),
    endTime: z.string().regex(/\d{2}:\d{2}/, 'Invalid time format'),
  });

  type UpdateBusinessFormInput = z.infer<typeof updateBusinessFormInputSchema>;

  const form = useForm<UpdateBusinessFormInput>({
    initialValues: {
      name: '',
      address: '',
      email: '',
      phoneNumber: '',
      startTime: '',
      endTime: '',
    },
    validate: zodResolver(updateBusinessFormInputSchema),
  });

  useEffect(() => {
    if (businessQuery.data) {
      const { name, address, email, phoneNumber, startHour, startMinute, endHour, endMinute } = businessQuery.data;
      form.setValues({
        name,
        address,
        email,
        phoneNumber,
        startTime: `${startHour.toString().padStart(2, '0')}:${startMinute.toString().padStart(2, '0')}`,
        endTime: `${endHour.toString().padStart(2, '0')}:${endMinute.toString().padStart(2, '0')}`,
      });
    }
  }, [businessQuery.data]);

  const handleUpdateSubmit = (values: UpdateBusinessFormInput) => {
    const [startHour, startMinute] = values.startTime.split(':').map(Number);
    const [endHour, endMinute] = values.endTime.split(':').map(Number);

    const updateData = {
      ...values,
      startHour,
      startMinute,
      endHour,
      endMinute,
    };

    updateBusinessMutation.mutate({
      businessId,
      data: updateData,
    });
  };

  const deleteBusiness = () => {
    deleteBusinessMutation.mutate({ businessId });
  };

  if (businessQuery.isLoading) {
    return <div>Loading...</div>;
  }

  const business = businessQuery.data;
  if (!business) {
    return <div>Business not found.</div>;
  }

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(handleUpdateSubmit)}>
        <Stack>
          <TextInput label="Name" placeholder="Name" {...form.getInputProps('name')} />
          <TextInput label="Address" placeholder="Address" {...form.getInputProps('address')} />
          <TextInput label="Email" placeholder="Email" {...form.getInputProps('email')} />
          <TextInput label="Phone Number" placeholder="Phone Number" {...form.getInputProps('phoneNumber')} />
          <TimeInput label="Start Time" placeholder="Start Time" {...form.getInputProps('startTime')} />
          <TimeInput label="End Time" placeholder="End Time" {...form.getInputProps('endTime')} />
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

      <Modal opened={isDeleteModalOpen} onClose={closeDeleteModal} title="Delete business">
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