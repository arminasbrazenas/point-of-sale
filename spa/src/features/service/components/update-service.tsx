import { Button, Group, Modal, NumberInput, Stack, TextInput, Text, Paper } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useMemo, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useService } from '../api/get-service';
import { UpdateServiceInput, useUpdateService } from '../api/update-service';
import { useDeleteService } from '../api/delete-service';
import { CreateServiceInput, createServiceInputSchema } from '../api/create-service';

export const UpdateService = ({ serviceId }: { serviceId: number }) => {
  const serviceQuery = useService({ serviceId });
  const navigate = useNavigate();
  const [updatedServiceProperties, setUpdatedServiceProperties] = useState<UpdateServiceInput>({});
  const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

  const deleteServiceMutation = useDeleteService({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Service deleted successfully.',
        });

        navigate(paths.management.services.getHref());
      },
    },
  });

  const updateServiceMutation = useUpdateService({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Service updated successfully.',
        });

        setUpdatedServiceProperties({});
      },
    },
  });

  const form = useForm<CreateServiceInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      price: 0,
      durationInMinutes: 0,
    },
    validate: zodResolver(createServiceInputSchema),
    onValuesChange: (updatedService) => {
      const service = serviceQuery.data;
      if (!service) {
        setUpdatedServiceProperties({});
        return;
      }

      setUpdatedServiceProperties({
        name: service.name === updatedService.name.trim() ? undefined : updatedService.name,
        price: service.price === updatedService.price ? undefined : updatedService.price,
        durationInMinutes:
          service.durationInMinutes === updatedService.durationInMinutes ? undefined : updatedService.durationInMinutes,
      });
    },
  });

  useEffect(() => {
    const service = serviceQuery.data;
    if (!service) {
      return;
    }

    form.setFieldValue('name', service.name);
    form.setFieldValue('price', service.price);
    form.setFieldValue('durationInMinutes', service.durationInMinutes);
  }, [serviceQuery.data]);

  const isAnyServicePropertyChanged = useMemo(
    () => Object.values(updatedServiceProperties).every((o) => !o),
    [updatedServiceProperties],
  );

  if (serviceQuery.isLoading) {
    return <div>loading...</div>;
  }

  const service = serviceQuery.data;
  if (!service) {
    return null;
  }

  const deleteService = () => {
    deleteServiceMutation.mutate({ serviceId });
  };

  const updateService = () => {
    updateServiceMutation.mutate({ serviceId, data: updatedServiceProperties });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(updateService)}>
        <Stack>
          <TextInput
            label="Name"
            placeholder="Name"
            withAsterisk
            key={form.key('name')}
            {...form.getInputProps('name')}
          />
          <NumberInput
            label="Price (€)"
            placeholder="Price (€)"
            withAsterisk
            key={form.key('price')}
            {...form.getInputProps('price')}
          />
          <NumberInput
            label="Duration (minutes)"
            placeholder="Duration (minutes)"
            withAsterisk
            key={form.key('durationInMinutes')}
            {...form.getInputProps('durationInMinutes')}
          />

          <Group justify="space-between" mt="xs">
            <Button color="red" variant="light" onClick={openDeleteModal}>
              Delete
            </Button>
            <Group>
              <Button variant="default" onClick={() => navigate(paths.management.services.getHref())}>
                Cancel
              </Button>
              <Button type="submit" disabled={isAnyServicePropertyChanged} loading={updateServiceMutation.isPending}>
                Save
              </Button>
            </Group>
          </Group>
        </Stack>
      </form>

      <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Delete service">
        <Text mt="md">
          Are you sure you want to delete{' '}
          <Text component="span" fw={600}>
            {service.name}
          </Text>
          ?
        </Text>
        <Group mt="lg" justify="flex-end">
          <Button variant="default" onClick={closeDeleteModal}>
            Cancel
          </Button>
          <Button color="red" variant="light" loading={deleteServiceMutation.isPending} onClick={deleteService}>
            Delete
          </Button>
        </Group>
      </Modal>
    </Paper>
  );
};
