import { Button, Text, Paper, Select, Stack, TextInput, Divider } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useAppStore } from '@/lib/app-store';
import { CreateReservationInput, createReservationInputSchema, useCreateReservation } from '../api/create-reservation';
import { useServices } from '@/features/service/api/get-services';
import { DateTimePicker } from '@mantine/dates';
import { useEffect, useState } from 'react';
import { Service, ServiceEmployee } from '@/types/api';

export const AddReservation = () => {
  const navigate = useNavigate();
  const [selectedService, setSelectedService] = useState<Service | undefined>();
  const [selectedEmployee, setSelectedEmployee] = useState<ServiceEmployee | undefined>();
  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  if (!businessId) {
    throw new Error('Business ID is required to create a reservation.');
  }

  const servicesQuery = useServices({ paginationFilter: { itemsPerPage: 50, page: 1 } });
  const form = useForm<CreateReservationInput>({
    mode: 'uncontrolled',
    initialValues: {
      employeeId: -1,
      serviceId: -1,
      startDate: new Date(),
      customer: {
        firstName: '',
        lastName: '',
        phoneNumber: '',
      },
    },
    validate: zodResolver(createReservationInputSchema),
  });

  useEffect(() => {
    setSelectedEmployee(undefined);
    if (selectedService) {
      form.setFieldValue('serviceId', selectedService.id);
    } else {
      form.setFieldValue('serviceId', -1);
    }
  }, [selectedService]);

  useEffect(() => {
    if (selectedEmployee) {
      form.setFieldValue('employeeId', selectedEmployee.id);
    } else {
      form.setFieldValue('employeeId', -1);
    }
  }, [selectedEmployee]);

  const createReservationMutation = useCreateReservation({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Reservation added successfully.',
        });

        navigate(paths.employee.reservations.getHref());
      },
    },
  });

  const createReservation = (values: CreateReservationInput) => {
    createReservationMutation.mutate({ data: { ...values, businessId } });
  };

  if (servicesQuery.isPending) {
    return <div>loading...</div>;
  }

  const services = servicesQuery.data?.items;
  if (!services) {
    return null;
  }

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(createReservation)}>
        <Stack>
          <Select
            label="Service"
            withAsterisk
            data={services.map((s) => ({
              label: `${s.name} (${s.durationInMinutes} minutes)`,
              value: s.id.toString(),
            }))}
            value={selectedService?.id.toString()}
            onChange={(value) => {
              const serviceId = parseInt(value ?? '');
              const service = services.find((s) => s.id == serviceId);
              setSelectedService(service);
            }}
            allowDeselect={false}
          />
          {selectedService && (
            <Select
              label="Employee"
              withAsterisk
              data={selectedService.providedByEmployees.map((e) => ({
                label: e.fullName,
                value: e.id.toString(),
              }))}
              value={selectedEmployee?.id == null ? null : selectedEmployee.id.toString()}
              onChange={(value) => {
                const employeeId = parseInt(value ?? '');
                const employee = selectedService.providedByEmployees.find((e) => e.id == employeeId);
                setSelectedEmployee(employee);
              }}
              allowDeselect={false}
            />
          )}
          <DateTimePicker
            label="Start date"
            withAsterisk
            key={form.key('startDate')}
            {...form.getInputProps('startDate')}
          />

          <Divider />
          <Text fw={500}>Customer details</Text>
          <TextInput
            label="First name"
            placeholder="First name"
            withAsterisk
            key={form.key('customer.firstName')}
            {...form.getInputProps('customer.firstName')}
          />
          <TextInput
            label="Last name"
            placeholder="Last name"
            withAsterisk
            key={form.key('customer.lastName')}
            {...form.getInputProps('customer.lastName')}
          />
          <TextInput
            label="Phone number"
            placeholder="Phone number"
            withAsterisk
            key={form.key('customer.phoneNumber')}
            {...form.getInputProps('customer.phoneNumber')}
          />
          <Button type="submit" mt="xs" fullWidth loading={createReservationMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
