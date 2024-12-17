import { Center, Pagination, Paper, Table, Badge } from '@mantine/core';
import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useReservations } from '../api/get-reservations';
import { formatDate } from '@/utilities';
import { ReservationStatus } from '@/types/api';

const getStatusColor = (status: ReservationStatus) => {
  switch (status) {
    case ReservationStatus.Confirmed:
      return 'green';
    case ReservationStatus.Pending:
      return 'yellow';
    case ReservationStatus.Cancelled:
      return 'red';
    default:
      return 'gray';
  }
};

export const ReservationList = () => {
  const [page, setPage] = useState<number>(1);
  const reservationsQuery = useReservations({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (reservationsQuery.data == null) {
      return 0;
    }

    return Math.ceil(reservationsQuery.data.totalItems / reservationsQuery.data.itemsPerPage);
  }, [reservationsQuery.data]);

  if (reservationsQuery.isLoading) {
    return <div>loading...</div>;
  }

  const reservations = reservationsQuery.data?.items;
  if (!reservations) {
    return null;
  }

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Customer</Table.Th>
              <Table.Th>Service</Table.Th>
              <Table.Th>Start Time</Table.Th>
              <Table.Th>End Time</Table.Th>
              <Table.Th>Status</Table.Th>
              <Table.Th>Employee</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {reservations.map((reservation) => (
              <Table.Tr
                key={reservation.id}
                onClick={() => navigate(paths.employee.updateReservation.getHref(reservation.id))}
              >
                <Table.Td>{reservation.customer.firstName} {reservation.customer.lastName}</Table.Td>
                <Table.Td>{reservation.service.name}</Table.Td>
                <Table.Td>{formatDate(reservation.appointmentTime)}</Table.Td>
                <Table.Td>{formatDate(reservation.appointmentendTime)}</Table.Td>
                <Table.Td>
                  <Badge color={getStatusColor(reservation.status)}>
                    {reservation.status}
                  </Badge>
                </Table.Td>
                <Table.Td>{reservation.employee.firstName} {reservation.employee.lastName}</Table.Td>
              </Table.Tr>
            ))}
          </Table.Tbody>
        </Table>
      </Paper>

      <Center>
        <Pagination total={totalPages} value={page} onChange={setPage} mt="md" />
      </Center>
    </>
  );
};