import { Button, Group, Stack, TextInput, Text, Paper, Select, Divider } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useState } from 'react';
import { showNotification } from '@/lib/notifications';
import { useServices } from '@/features/service/api/get-services';
import { useAppStore } from '@/lib/app-store';
import { UpdateReservationInput, useUpdateReservation } from '../api/update-reservation';
import { CreateReservationInput, createReservationInputSchema } from '../api/create-reservation';
import { useReservation } from '../api/get-reservation';
import { DateTimePicker } from '@mantine/dates';
import { Service, ServiceEmployee } from '@/types/api';
import { useCancelReservation } from '../api/cancel-reservation';

export const UpdateReservation = ({ reservationId }: { reservationId: number }) => {
  const navigate = useNavigate();
  const [selectedService, setSelectedService] = useState<Service | undefined>();
  const [selectedEmployee, setSelectedEmployee] = useState<ServiceEmployee | undefined>();
  const [updatedReservationProperties, setUpdatedReservationProperties] = useState<UpdateReservationInput>({
    customer: {},
  });
  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  if (!businessId) {
    throw new Error('Business ID is required to update a reservation.');
  }

  const reservationQuery = useReservation({ reservationId });
  const servicesQuery = useServices({ paginationFilter: { page: 1, itemsPerPage: 50 } });

  const updateReservationMutation = useUpdateReservation({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Service updated successfully.',
        });

        setUpdatedReservationProperties({ customer: {} });
      },
    },
  });

  const cancelReservationMutation = useCancelReservation({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Reservation canceled successfully.',
        });
      },
    },
  });

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
    onValuesChange: (updatedReservation) => {
      const reservation = reservationQuery.data;
      if (!reservation) {
        setUpdatedReservationProperties({ customer: {} });
        return;
      }

      setUpdatedReservationProperties({
        employeeId:
          reservation.employee.id === updatedReservation.employeeId ? undefined : updatedReservation.employeeId,
        serviceId: reservation.serviceId === updatedReservation.serviceId ? undefined : updatedReservation.serviceId,
        startDate:
          new Date(reservation.date.start).getTime() === updatedReservation.startDate.getTime()
            ? undefined
            : updatedReservation.startDate,
        customer: {
          firstName:
            reservation.customer.firstName == updatedReservation.customer.firstName
              ? undefined
              : updatedReservation.customer.firstName,
          lastName:
            reservation.customer.lastName == updatedReservation.customer.lastName
              ? undefined
              : updatedReservation.customer.lastName,
          phoneNumber:
            reservation.customer.phoneNumber == updatedReservation.customer.phoneNumber
              ? undefined
              : updatedReservation.customer.phoneNumber,
        },
      });
    },
  });

  useEffect(() => {
    const reservation = reservationQuery.data;
    const services = servicesQuery.data?.items;
    if (!reservation || !services) {
      return;
    }

    setSelectedService(services.find((s) => s.id == reservation.serviceId));
    setSelectedEmployee(reservation.employee);

    form.setFieldValue('startDate', new Date(reservation.date.start));
    form.setFieldValue('customer.firstName', reservation.customer.firstName);
    form.setFieldValue('customer.lastName', reservation.customer.lastName);
    form.setFieldValue('customer.phoneNumber', reservation.customer.phoneNumber);
  }, [servicesQuery.data, reservationQuery.data]);

  useEffect(() => {
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

  if (servicesQuery.isLoading || reservationQuery.isLoading) {
    return <div>loading...</div>;
  }

  const reservation = reservationQuery.data;
  const services = servicesQuery.data?.items;
  if (!services || !reservation) {
    return null;
  }

  const updateReservation = () => {
    updateReservationMutation.mutate({ reservationId, data: updatedReservationProperties });
  };

  const cancelReservation = () => {
    cancelReservationMutation.mutate({ reservationId });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(updateReservation)}>
        <Stack>
          <Text>Status: {reservation.status}</Text>
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
              setSelectedEmployee(undefined);
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

          <Group justify="space-between" mt="xs">
            <Button color="red" variant="light" onClick={cancelReservation}>
              Cancel reservation
            </Button>
            <Group>
              <Button variant="default" onClick={() => navigate(paths.management.services.getHref())}>
                Cancel
              </Button>
              <Button type="submit" loading={updateReservationMutation.isPending}>
                Save
              </Button>
            </Group>
          </Group>
        </Stack>
      </form>
    </Paper>
  );
};
