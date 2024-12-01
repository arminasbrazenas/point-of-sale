import { Card, Center, Pagination, Paper, Table } from '@mantine/core';
import { useMemo, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useServiceCharges } from '../api/get-service-charges';
import { toReadablePricingStrategy, toReadablePricingStrategyAmount } from '@/utilities';

export const ServiceChargeList = () => {
  const [page, setPage] = useState<number>(1);
  const serviceChargesQuery = useServiceCharges({ paginationFilter: { page, itemsPerPage: 25 } });
  const navigate = useNavigate();

  const totalPages = useMemo(() => {
    if (serviceChargesQuery.data == null) {
      return 0;
    }

    return Math.ceil(serviceChargesQuery.data.totalItems / serviceChargesQuery.data.itemsPerPage);
  }, [serviceChargesQuery.data]);

  if (serviceChargesQuery.isLoading) {
    return <div>loading...</div>;
  }

  const serviceCharges = serviceChargesQuery.data?.items;
  if (!serviceCharges) {
    return null;
  }

  return (
    <>
      <Paper withBorder bg="transparent">
        <Table striped stickyHeader highlightOnHover>
          <Table.Thead>
            <Table.Tr>
              <Table.Th>Name</Table.Th>
              <Table.Th>Type</Table.Th>
              <Table.Th>Amount</Table.Th>
            </Table.Tr>
          </Table.Thead>
          <Table.Tbody>
            {serviceCharges.map((s) => (
              <Table.Tr key={s.id} onClick={() => navigate(paths.management.updateServiceCharge.getHref(s.id))}>
                <Table.Td>{s.name}</Table.Td>
                <Table.Td>{toReadablePricingStrategy(s.pricingStrategy)}</Table.Td>
                <Table.Td>{toReadablePricingStrategyAmount(s.amount, s.pricingStrategy)}</Table.Td>
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
