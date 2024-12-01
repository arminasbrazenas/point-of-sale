import { Button, Card, Paper, Stack, Table, Text } from '@mantine/core';
import { useOrder } from '../api/get-order';
import { useCancelOrder } from '../api/cancel-order';
import { showNotification } from '@/lib/notifications';
import { EnhancedCreateOrderItemInput } from './order-product';
import { useProducts } from '@/features/product/api/get-products';
import { useEffect, useMemo, useState } from 'react';
import { OrderProducts } from './order-products';
import { Product, OrderReceipt } from '@/types/api';
import { formatDate } from '@/utilities';
import { useOrderReceipt } from '../api/get-order-receipt';

export const UpdateOrder = ({ orderId }: { orderId: number }) => {
  const [receipt, setReceipt] = useState<OrderReceipt | undefined>(undefined);
  const orderQuery = useOrder({ orderId });
  const productsQuery = useProducts({ paginationFilter: { itemsPerPage: 50, page: 1 } });
  const receiptQuery = useOrderReceipt({ orderId, queryConfig: { enabled: false } });
  const cancelOrderMutation = useCancelOrder({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Order canceled successfully.',
        });
      },
    },
  });

  const enhancedOrderItems = useMemo((): EnhancedCreateOrderItemInput[] => {
    const orderItems = orderQuery.data?.orderItems;
    const products = productsQuery.data?.items;
    if (!orderItems || !products) {
      return [];
    }

    return orderItems.map((i): EnhancedCreateOrderItemInput => {
      const product: Product =
        products.find((p) => p.id == i.productId) ??
        ({
          name: i.name,
          priceWithTaxes: i.unitPrice,
        } as Product);

      return {
        cartItemId: crypto.randomUUID(),
        product: product,
        productId: product.id ?? 0,
        modifierIds: i.modifiers.map((m) => m.modifierId ?? 0),
        quantity: i.quantity,
        price: i.totalPrice,
        orderedQuantity: i.quantity,
        modifiers: i.modifiers,
      };
    });
  }, [orderQuery.data, productsQuery.data]);

  useEffect(() => {
    if (receiptQuery.data) {
      setReceipt(receiptQuery.data);
    }
  }, [receiptQuery.data]);

  if (orderQuery.isLoading || productsQuery.isLoading) {
    return <div>loading...</div>;
  }

  const order = orderQuery.data;
  const products = productsQuery.data?.items;
  if (!order || !products) {
    return null;
  }

  const cancelOrder = () => {
    cancelOrderMutation.mutate({ orderId });
  };

  const showReceipt = () => {
    receiptQuery.refetch();
  };

  return (
    <Stack>
      <Paper withBorder p="md">
        <Text>ID: {order.id}</Text>
        <Text>Status: {order.status}</Text>
        <Text>Created at: {formatDate(order.createdAt)}</Text>

        <Stack mt="lg" gap="xs">
          <Button onClick={showReceipt}>View receipt</Button>
          <Button color="red" variant="light" onClick={cancelOrder}>
            Cancel order
          </Button>
        </Stack>
      </Paper>

      {receipt && (
        <Paper withBorder p="md">
          <Text fw={600}>Receipt</Text>
          <Paper withBorder mt="xs">
            <Table>
              <Table.Thead>
                <Table.Tr>
                  <Table.Th>QTY</Table.Th>
                  <Table.Th>Description</Table.Th>
                  <Table.Th>Price</Table.Th>
                  <Table.Th>Amount</Table.Th>
                </Table.Tr>
              </Table.Thead>
              <Table.Tbody>
                {receipt.orderItems.map((x) => (
                  <Table.Tr key={x.id}>
                    <Table.Td>{x.quantity}</Table.Td>
                    <Table.Td>{x.name}</Table.Td>
                    <Table.Td>{x.unitPrice}€</Table.Td>
                    <Table.Td>{x.totalPrice}€</Table.Td>
                  </Table.Tr>
                ))}
              </Table.Tbody>
            </Table>
          </Paper>

          <Stack align="flex-end" mt="xs" gap="0">
            <Text fw={500}>Tax: {receipt.taxTotal}€</Text>
            <Text fw={500}>Total: {receipt.totalPrice}€</Text>
          </Stack>
        </Paper>
      )}

      <OrderProducts orderItems={enhancedOrderItems} orderId={order.id} />
    </Stack>
  );
};
