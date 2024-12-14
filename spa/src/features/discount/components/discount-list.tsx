import { Center, Pagination, Paper, Table } from '@mantine/core';
import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useDiscounts } from '../api/get-discounts';
import { formatDate, toReadablePricingStrategy, toReadablePricingStrategyAmount } from '@/utilities';

export const DiscountList = () => {
  const [page, setPage] = useState<number>(1);
  const discountsQuery = useDiscounts({ paginationFilter: { page, itemsPerPage: 50 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (discountsQuery.data == null) {
      return 0;
    }

    return Math.ceil(discountsQuery.data.totalItems / discountsQuery.data.itemsPerPage);
  }, [discountsQuery.data]);

  if (discountsQuery.isLoading) {
    return <div>loading...</div>;
  }

  const discounts = discountsQuery.data?.items;
  if (!discounts) {
    return null;
  }

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Type</Table.Th>
              <Table.Th>Amount</Table.Th>
              <Table.Th>Valid until</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {discounts.map((discount) => (
              <Table.Tr
                key={discount.id}
                onClick={() => navigate(paths.management.updateDiscount.getHref(discount.id))}
              >
                <Table.Td>{toReadablePricingStrategy(discount.pricingStrategy)}</Table.Td>
                <Table.Td>{toReadablePricingStrategyAmount(discount.amount, discount.pricingStrategy)}</Table.Td>
                <Table.Td>{formatDate(discount.validUntil)}</Table.Td>
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
