import { Card, Center, Pagination, Paper, Table } from '@mantine/core';
import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useTaxes } from '../api/get-taxes';

export const TaxList = () => {
  const [page, setPage] = useState<number>(1);
  const taxesQuery = useTaxes({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (taxesQuery.data == null) {
      return 0;
    }

    return Math.ceil(taxesQuery.data.totalItems / taxesQuery.data.itemsPerPage);
  }, [taxesQuery.data]);

  if (taxesQuery.isLoading || !taxesQuery.data) {
    return <div>loading...</div>;
  }

  const taxes = taxesQuery.data.items;

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Name</Table.Th>
              <Table.Th>Rate</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {taxes.map((tax) => (
              <Table.Tr key={tax.id} onClick={() => navigate(paths.management.updateTax.getHref(tax.id))}>
                <Table.Td>{tax.name}</Table.Td>
                <Table.Td>{tax.rate}</Table.Td>
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
