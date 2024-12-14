import { Button, List, Paper, SimpleGrid, Stack, Table, Text } from '@mantine/core';
import { useOrder } from '../api/get-order';
import { useCancelOrder } from '../api/cancel-order';
import { showNotification } from '@/lib/notifications';
import { EnhancedCreateOrderItemInput } from './order-product';
import { useProducts } from '@/features/product/api/get-products';
import { useEffect, useMemo, useState } from 'react';
import { OrderProducts } from './order-products';
import { Product, OrderReceipt } from '@/types/api';
import { formatDate, toReadablePricingStrategyAmount } from '@/utilities';
import { useOrderReceipt } from '../api/get-order-receipt';
import { OrderPayments } from '@/features/payment/components/order-payments';
import { useCompleteOrder } from '../api/complete-order';

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

  const completeOrderMutation = useCompleteOrder({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Order completed successfully.',
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

    return orderItems.map((i, idx): EnhancedCreateOrderItemInput => {
      const product: Product =
        products.find((p) => p.id == i.productId) ??
        ({
          name: i.name,
          priceDiscountExcluded: i.unitPrice,
        } as Product);

      return {
        cartItemId: idx + 1,
        product: product,
        productId: product.id ?? 0,
        modifierIds: i.modifiers.map((m) => m.modifierId ?? 0),
        quantity: i.quantity,
        price: i.totalPrice,
        orderedQuantity: i.quantity,
        modifiers: i.modifiers,
        discounts: i.discounts,
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

  const completeOrder = () => {
    completeOrderMutation.mutate({ data: { orderId } });
  };

  const showReceipt = () => {
    receiptQuery.refetch();
  };

  return (
    <Stack>
      <SimpleGrid cols={2}>
        <Paper withBorder p="md">
          <Text fw={600}>Order #{order.id}</Text>
          <Text size="sm">Status: {order.status}</Text>
          <Text size="sm">Created at: {formatDate(order.createdAt)}</Text>

          <Stack mt="lg" gap="xs">
            <Button onClick={showReceipt} variant="light">
              View receipt
            </Button>
            <Button color="teal" variant="light" onClick={completeOrder}>
              Complete order
            </Button>
            <Button color="red" variant="light" onClick={cancelOrder}>
              Cancel order
            </Button>
          </Stack>
        </Paper>

        <OrderPayments orderId={order.id} />
      </SimpleGrid>

      {receipt && (
        <Paper withBorder p="md">
          <Text fw={600}>Receipt</Text>
          <Paper withBorder mt="xs">
            <Table>
              <Table.Thead>
                <Table.Tr>
                  <Table.Th>QTY</Table.Th>
                  <Table.Th>Description</Table.Th>
                  <Table.Th>Unit price</Table.Th>
                  <Table.Th>Discounts</Table.Th>
                  <Table.Th>Taxes</Table.Th>
                  <Table.Th>Amount</Table.Th>
                </Table.Tr>
              </Table.Thead>
              <Table.Tbody>
                {receipt.orderItems.map((x) => (
                  <Table.Tr key={x.id}>
                    <Table.Td>{x.quantity}</Table.Td>
                    <Table.Td>{x.name}</Table.Td>
                    <Table.Td>{x.unitPrice}€</Table.Td>
                    <Table.Td>{x.discountsTotal}€</Table.Td>
                    <Table.Td>{x.taxTotal}€</Table.Td>
                    <Table.Td>{x.totalPrice}€</Table.Td>
                  </Table.Tr>
                ))}
              </Table.Tbody>
            </Table>
          </Paper>

          {receipt.discounts.length > 0 && (
            <Paper withBorder mt="xs">
              <Table>
                <Table.Thead>
                  <Table.Tr>
                    <Table.Th>Discount</Table.Th>
                    <Table.Th>Amount</Table.Th>
                  </Table.Tr>
                </Table.Thead>
                <Table.Tbody>
                  {receipt.discounts.map((d) => (
                    <Table.Tr>
                      <Table.Td>{toReadablePricingStrategyAmount(d.amount, d.pricingStrategy)}</Table.Td>
                      <Table.Td>{d.appliedAmount}€</Table.Td>
                    </Table.Tr>
                  ))}
                </Table.Tbody>
              </Table>
            </Paper>
          )}

          {receipt.serviceCharges.length > 0 && (
            <Paper withBorder mt="xs">
              <Table>
                <Table.Thead>
                  <Table.Tr>
                    <Table.Th>Service charge</Table.Th>
                    <Table.Th>Amount</Table.Th>
                  </Table.Tr>
                </Table.Thead>
                <Table.Tbody>
                  {receipt.serviceCharges.map((s) => (
                    <Table.Tr>
                      <Table.Td>{toReadablePricingStrategyAmount(s.amount, s.pricingStrategy)}</Table.Td>
                      <Table.Td>{s.appliedAmount}€</Table.Td>
                    </Table.Tr>
                  ))}
                </Table.Tbody>
              </Table>
            </Paper>
          )}

          <Text fw={600} ta="right" mt="md">
            Total: {receipt.totalPrice}€
          </Text>
        </Paper>
      )}

      <OrderProducts
        orderItems={enhancedOrderItems}
        orderId={order.id}
        selectedServiceCharges={order.serviceCharges.map((c) => c.name)}
        discounts={order.discounts.map((d) => ({ ...d, id: crypto.randomUUID() }))}
      />
    </Stack>
  );
};
