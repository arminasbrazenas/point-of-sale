import { Button, NumberInput, Paper, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useAppStore } from '@/lib/app-store';
import { CreateReservationInput, createReservationInputSchema, useCreateReservation } from '../api/create-reservation';

export const AddReservation = () => {
  const navigate = useNavigate();
  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  if (!businessId) {
    throw new Error('Business ID is required to create a reservation.');
  }

  const form = useForm<CreateReservationInput>({
    mode: 'uncontrolled',
    initialValues: {
      employeeId: 0,
      serviceId: 0,
      startDate: '',
      customer: {
        firstName: '',
        lastName: '',
      },
    },
    validate: zodResolver(createReservationInputSchema),
  });

  const createReservationMutation = useCreateReservation({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Reservation added successfully.',
        });

        navigate(paths.management.services.getHref());
      },
    },
  });

  const createReservation = (values: CreateReservationInput) => {
    createReservationMutation.mutate({ data: { ...values, businessId } });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(createReservation)}>
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
          <Button type="submit" mt="xs" fullWidth loading={createReservationMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
