import { useMemo, useState } from 'react';
import { useOrders } from '../api/get-orders';
import { useNavigate } from 'react-router-dom';
import { Center, Pagination, Paper, Table } from '@mantine/core';
import { paths } from '@/config/paths';

export const OrderList = () => {
  const [page, setPage] = useState<number>(1);
  const ordersQuery = useOrders({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (ordersQuery.data == null) {
      return 0;
    }

    return Math.ceil(ordersQuery.data.totalItems / ordersQuery.data.itemsPerPage);
  }, [ordersQuery.data]);

  if (ordersQuery.isLoading || !ordersQuery.data) {
    return <div>loading...</div>;
  }

  const orders = ordersQuery.data.items;

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>ID</Table.Th>
              <Table.Th>Status</Table.Th>
              <Table.Th>Created at</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {orders.map((order) => (
              <Table.Tr key={order.id} onClick={() => navigate(paths.employee.updateOrder.getHref(order.id))}>
                <Table.Td>{order.id}</Table.Td>
                <Table.Td>{order.status}</Table.Td>
                <Table.Td>{order.createdAt}</Table.Td>
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
