import { Button, Paper, Stack, Select } from '@mantine/core';
import { DateTimePicker } from '@mantine/dates';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useCustomers } from '@/features/customer/api/get-customers';
import { useServices } from '@/features/service/api/get-services';
import { CreateReservationInput, createReservationSchema, useCreateReservation } from '../api/create-reservation';

export const CreateReservation = () => {
  const navigate = useNavigate();
  const customersQuery = useCustomers({ paginationFilter: { page: 1, itemsPerPage: 50 } });
  const servicesQuery = useServices({ paginationFilter: { page: 1, itemsPerPage: 50 } });

  const form = useForm<CreateReservationInput>({
    initialValues: {
      serviceId: 0,
      customerId: 0,
      appointmentTime: new Date(),
    },
    validate: zodResolver(createReservationSchema),
  });

  const createReservationMutation = useCreateReservation({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Reservation created successfully.',
        });
        navigate(paths.employee.reservations.getHref());
      },
    },
  });

  if (customersQuery.isLoading || servicesQuery.isLoading) {
    return <div>Loading...</div>;
  }

  const customers = customersQuery.data?.items || [];
  const services = servicesQuery.data?.items || [];

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit((values) => createReservationMutation.mutate({ data: values }))}>
        <Stack>
          <Select
            label="Customer"
            data={customers.map(customer => ({
              value: customer.id.toString(),
              label: `${customer.firstName} ${customer.lastName}`,
            }))}
            onChange={(value) => form.setFieldValue('customerId', Number(value))}
            withAsterisk
          />
          <Select
            label="Service"
            data={services.map(service => ({
              value: service.id.toString(),
              label: service.name,
            }))}
            onChange={(value) => form.setFieldValue('serviceId', Number(value))}
            withAsterisk
          />
          <DateTimePicker
            label="Appointment Time"
            {...form.getInputProps('appointmentTime')}
            withAsterisk
          />
          <Button type="submit" mt="xs" fullWidth loading={createReservationMutation.isPending}>
            Create Reservation
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};