import { Center, Pagination, Paper, Table } from '@mantine/core';
import { useProducts } from '../api/get-products';
import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';

export const ProductList = () => {
  const [page, setPage] = useState<number>(1);
  const productsQuery = useProducts({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (productsQuery.data == null) {
      return 0;
    }

    return Math.ceil(productsQuery.data.totalItems / productsQuery.data.itemsPerPage);
  }, [productsQuery.data]);

  if (productsQuery.isLoading) {
    return <div>loading...</div>;
  }

  var products = productsQuery.data?.items;
  if (!products) {
    return null;
  }

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Name</Table.Th>
              <Table.Th>Stock</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {products.map((product) => (
              <Table.Tr key={product.id} onClick={() => navigate(paths.management.updateProduct.getHref(product.id))}>
                <Table.Td>{product.name}</Table.Td>
                <Table.Td>{product.stock}</Table.Td>
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
