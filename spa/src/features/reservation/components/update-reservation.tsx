import { Button, Group, Modal, Stack, Text, Paper, Select } from '@mantine/core';
import { DateTimePicker } from '@mantine/dates';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useReservation } from '../api/get-reservation';
import { UpdateReservationInput, useUpdateReservation } from '../api/update-reservation';
import { useCancelReservation } from '../api/cancel-reservation';
import { ReservationStatus } from '@/types/api';

export const UpdateReservation = ({ reservationId }: { reservationId: number }) => {
  const reservationQuery = useReservation({ reservationId });
  const navigate = useNavigate();
  const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

  const cancelReservationMutation = useCancelReservation({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Reservation cancelled successfully.',
        });

        navigate(paths.employee.reservations.getHref());
      },
    },
  });

  const updateReservationMutation = useUpdateReservation({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Reservation updated successfully.',
        });
      },
    },
  });

  const form = useForm<UpdateReservationInput>({
    initialValues: {
      appointmentTime: new Date(),
      status: ReservationStatus.Pending,
    },
  });

  useEffect(() => {
    if (!reservationQuery.data) return;
    
    form.setValues({
      appointmentTime: new Date(reservationQuery.data.appointmentTime),
      status: reservationQuery.data.status,
    });
  }, [reservationQuery.data]);

  if (reservationQuery.isLoading) {
    return <div>loading...</div>;
  }

  const reservation = reservationQuery.data;
  if (!reservation) {
    return null;
  }

  const cancelReservation = () => {
    cancelReservationMutation.mutate({ reservationId });
  };

  const updateReservation = (values: UpdateReservationInput) => {
    updateReservationMutation.mutate({ reservationId, data: values });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(updateReservation)}>
        <Stack>
          <Select
            label="Status"
            data={Object.values(ReservationStatus).map(status => ({
              value: status,
              label: status,
            }))}
            {...form.getInputProps('status')}
          />
          <DateTimePicker
            label="Appointment Time"
            {...form.getInputProps('appointmentTime')}
          />
          <Group justify="space-between" mt="xs">
            <Button color="red" variant="light" onClick={openDeleteModal}>
              Cancel Reservation
            </Button>
            <Group>
              <Button variant="default" onClick={() => navigate(paths.employee.reservations.getHref())}>
                Back
              </Button>
              <Button type="submit" loading={updateReservationMutation.isPending}>
                Save
              </Button>
            </Group>
          </Group>
        </Stack>
      </form>

      <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Cancel Reservation">
        <Text mt="md">Are you sure you want to cancel this reservation?</Text>
        <Group mt="lg" justify="flex-end">
          <Button variant="default" onClick={closeDeleteModal}>
            No, keep it
          </Button>
          <Button color="red" variant="light" loading={cancelReservationMutation.isPending} onClick={cancelReservation}>
          Yes, cancel it
          </Button>
        </Group>
      </Modal>
    </Paper>
  );
};