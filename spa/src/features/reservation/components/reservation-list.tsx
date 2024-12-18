import { Center, Pagination, Paper, Table } from '@mantine/core';
import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useReservations } from '../api/get-reservations';
import { formatDate } from '@/utilities';

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
              <Table.Th>Description</Table.Th>
              <Table.Th>Customer</Table.Th>
              <Table.Th>Employee</Table.Th>
              <Table.Th>Start</Table.Th>
              <Table.Th>End</Table.Th>
              <Table.Th>Status</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {reservations.map((r) => (
              <Table.Tr key={r.id} onClick={() => navigate(paths.employee.updateReservation.getHref(r.id))}>
                <Table.Td>{r.description}</Table.Td>
                <Table.Td>{`${r.customer.firstName} ${r.customer.lastName}`}</Table.Td>
                <Table.Td>{r.employee.fullName}</Table.Td>
                <Table.Td>{formatDate(r.date.start)}</Table.Td>
                <Table.Td>{formatDate(r.date.end)}</Table.Td>
                <Table.Td>{r.status}</Table.Td>
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
