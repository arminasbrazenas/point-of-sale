import { Center, Pagination, Paper, Table } from '@mantine/core';
import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { formatDate } from '@/utilities';
import { useGiftCards } from '../api/get-gift-cards';

export const GiftCardList = () => {
  const [page, setPage] = useState<number>(1);
  const giftCardsQuery = useGiftCards({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (giftCardsQuery.data == null) {
      return 0;
    }

    return Math.ceil(giftCardsQuery.data.totalItems / giftCardsQuery.data.itemsPerPage);
  }, [giftCardsQuery.data]);

  if (giftCardsQuery.isLoading) {
    return <div>loading...</div>;
  }

  const giftCards = giftCardsQuery.data?.items;
  if (!giftCards) {
    return null;
  }

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Code</Table.Th>
              <Table.Th>Amount</Table.Th>
              <Table.Th>Valid until</Table.Th>
              <Table.Th>Used</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {giftCards.map((g) => (
              <Table.Tr key={g.id} onClick={() => navigate(paths.management.updateGiftCard.getHref(g.id))}>
                <Table.Td>{g.code}</Table.Td>
                <Table.Td>{g.amount}€</Table.Td>
                <Table.Td>{formatDate(g.expiresAt)}</Table.Td>
                <Table.Td>{g.usedAt == null ? 'No' : 'Yes'}</Table.Td>
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
