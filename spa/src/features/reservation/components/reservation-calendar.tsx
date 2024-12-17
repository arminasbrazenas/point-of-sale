import { Calendar, DatePicker } from '@mantine/dates';
import { useReservations } from '../api/get-reservations';
import { Paper, Text, Group } from '@mantine/core';
import { useState } from 'react';
import { formatDate } from '@/utilities';

export const ReservationCalendar = () => {
  const [selectedDate, setSelectedDate] = useState<Date | null>(new Date());
  const reservationsQuery = useReservations({
    paginationFilter: { page: 1, itemsPerPage: 100 }
  });

  if (reservationsQuery.isLoading) {
    return <div>Loading...</div>;
  }

  const reservations = reservationsQuery.data?.items || [];
  const dayReservations = reservations.filter(r => {
    const reservationDate = new Date(r.appointmentTime);
    return selectedDate && 
      reservationDate.getDate() === selectedDate.getDate() &&
      reservationDate.getMonth() === selectedDate.getMonth() &&
      reservationDate.getFullYear() === selectedDate.getFullYear();
  });

  return (
    <Group align="flex-start">
      <Paper withBorder p="md">
        <Calendar
          value={selectedDate}
          onChange={setSelectedDate}
          size="xl"
        />
      </Paper>
      
      <Paper withBorder p="md" style={{ flex: 1 }}>
        <Text size="lg" fw={500} mb="md">
          Reservations for {selectedDate ? formatDate(selectedDate.toISOString()) : 'No date selected'}
        </Text>
        {dayReservations.map(reservation => (
          <Paper key={reservation.id} withBorder p="sm" mb="xs">
            <Text>
              {formatDate(reservation.appointmentTime)} - {reservation.customer.firstName} {reservation.customer.lastName}
            </Text>
            <Text size="sm" c="dimmed">
              Service: {reservation.service.name}
            </Text>
          </Paper>
        ))}
        {dayReservations.length === 0 && (
          <Text c="dimmed">No reservations for this date</Text>
        )}
      </Paper>
    </Group>
  );
};