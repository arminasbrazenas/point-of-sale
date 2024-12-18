import { useAppStore } from '@/lib/app-store';
import { useBusiness } from '../api/get-business';
import { useNavigate } from 'react-router-dom';
import { Center, Paper, Table } from '@mantine/core';
import { paths } from '@/config/paths';

export const formatTime = (hour:number, minute:number) => {
  return `${hour.toString().padStart(2, '0')}:${minute.toString().padStart(2, '0')}`;
};

export const Business = () => {
  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  const navigate = useNavigate();


  const businessQuery = useBusiness({
    businessId: businessId ?? 0, 
    queryConfig: {
      enabled: businessId !== null && businessId !== undefined,
    },
  });

  if (businessQuery.isLoading) {
    return <div>Loading...</div>;
  }

  const business = businessQuery.data;

  if (!business) {
    return <div>Business not found.</div>;
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
          </Table.Tbody>
        </Table>
      </Paper>
    </>
  );
};
