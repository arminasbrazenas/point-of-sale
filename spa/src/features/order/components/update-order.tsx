import { Button, Paper, Stack, Text } from '@mantine/core';
import { useOrder } from '../api/get-order';
import { useCancelOrder } from '../api/cancel-order';
import { showNotification } from '@/lib/notifications';
import { EnhancedCreateOrderItemInput } from './order-product';
import { useProducts } from '@/features/product/api/get-products';
import { useMemo } from 'react';
import { OrderProducts } from './order-products';
import { Product } from '@/types/api';
import { formatDate } from '@/utilities';

export const UpdateOrder = ({ orderId }: { orderId: number }) => {
  const orderQuery = useOrder({ orderId });
  const productsQuery = useProducts({ paginationFilter: { itemsPerPage: 50, page: 1 } });
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
          priceWithTaxes: i.baseUnitPrice,
        } as Product);

      return {
        cartItemId: crypto.randomUUID(),
        product: product,
        productId: product.id ?? 0,
        modifierIds: [],
        quantity: i.quantity,
        price: i.totalPrice,
        orderedQuantity: i.quantity,
      };
    });
  }, [orderQuery.data, productsQuery.data]);

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

  return (
    <Stack>
      <Paper withBorder p="lg">
        <Text>ID: {order.id}</Text>
        <Text>Status: {order.status}</Text>
        <Text>Created at: {formatDate(order.createdAt)}</Text>

        <Stack mt="lg">
          <Button color="red" variant="light" onClick={cancelOrder}>
            Cancel order
          </Button>
        </Stack>
      </Paper>

      <OrderProducts orderItems={enhancedOrderItems} orderId={order.id} />
    </Stack>
  );
};
