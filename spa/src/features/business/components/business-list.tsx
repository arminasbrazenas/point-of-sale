import { useMemo, useState } from 'react';
import { useBusinesses } from '../api/get-businesses';
import { useNavigate } from 'react-router-dom';
import { Center, Pagination, Paper, Table } from '@mantine/core';
import { paths } from '@/config/paths';
import { formatTime } from './business';

export const BusinessList = () => {
  const [page, setPage] = useState<number>(1);
  const businessesQuery = useBusinesses({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (businessesQuery.data == null) {
      return 0;
    }

    return Math.ceil(businessesQuery.data.totalItems / businessesQuery.data.itemsPerPage);
  }, [businessesQuery.data]);

  if (businessesQuery.isLoading) {
    return <div>loading...</div>;
  }

  const businesses = businessesQuery.data?.items;
  if (!businesses) {
    return null;
  }

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Business ID</Table.Th>
              <Table.Th>Name</Table.Th>
              <Table.Th>Address</Table.Th>
              <Table.Th>Email</Table.Th>
              <Table.Th>Phone Number</Table.Th>
              <Table.Th>Start Time</Table.Th>
              <Table.Th>End Time</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {businesses.map((business) => (
              <Table.Tr
                key={business.id}
                onClick={() => navigate(paths.businessManagement.updateBusiness.getHref(business.id))}
              >
                <Table.Td>#{business.id}</Table.Td>
                <Table.Td>{business.name}</Table.Td>
                <Table.Td>{business.address}</Table.Td>
                <Table.Td>{business.email}</Table.Td>
                <Table.Td>{business.phoneNumber}</Table.Td>
                <Table.Td>{formatTime(business.startHour,business.startMinute )}</Table.Td>
                <Table.Td>{formatTime(business.endHour,business.endMinute )}</Table.Td>
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
