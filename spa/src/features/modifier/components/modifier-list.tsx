import { Center, Pagination, Paper, Table } from '@mantine/core';
import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useModifiers } from '../api/get-modifiers';

export const ModifierList = () => {
  const [page, setPage] = useState<number>(1);
  const modifiersQuery = useModifiers({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (modifiersQuery.data == null) {
      return 0;
    }

    return Math.ceil(modifiersQuery.data.totalItems / modifiersQuery.data.itemsPerPage);
  }, [modifiersQuery.data]);

  if (modifiersQuery.isLoading) {
    return <div>loading...</div>;
  }

  const modifiers = modifiersQuery.data?.items;
  if (!modifiers) {
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
              <Table.Th>Price</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {modifiers.map((modifier) => (
              <Table.Tr
                key={modifier.id}
                onClick={() => navigate(paths.management.updateModifier.getHref(modifier.id))}
              >
                <Table.Td>{modifier.name}</Table.Td>
                <Table.Td>{modifier.stock}</Table.Td>
                <Table.Td>{modifier.priceTaxExcluded}€</Table.Td>
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
