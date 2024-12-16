import { Center, Pagination, Paper, Table } from '@mantine/core';
import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useServices } from '../api/get-services';

export const ServiceList = () => {
  const [page, setPage] = useState<number>(1);
  const servicesQuery = useServices({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (servicesQuery.data == null) {
      return 0;
    }

    return Math.ceil(servicesQuery.data.totalItems / servicesQuery.data.itemsPerPage);
  }, [servicesQuery.data]);

  if (servicesQuery.isLoading) {
    return <div>loading...</div>;
  }

  const services = servicesQuery.data?.items;
  if (!services) {
    return null;
  }

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Name</Table.Th>
              <Table.Th>Duration</Table.Th>
              <Table.Th>Price</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {services.map((s) => (
              <Table.Tr key={s.id} onClick={() => navigate(paths.management.updateService.getHref(s.id))}>
                <Table.Td>{s.name}</Table.Td>
                <Table.Td>{s.duration}</Table.Td>
                <Table.Td>{s.price}</Table.Td>
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
