import { Button, Group, Paper, Stack, Text } from '@mantine/core';
import { useOrder } from '../api/get-order';
import { useCancelOrder } from '../api/cancel-order';
import { showNotification } from '@/lib/notifications';
import { OrderItemList } from './order-item-list';
import { EnhancedCreateOrderItemInput } from './order-product';
import { useProducts } from '@/features/product/api/get-products';
import { useEffect, useMemo, useState } from 'react';
import { OrderProducts } from './order-products';
import { Product } from '@/types/api';

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

  const saveOrderItems = () => {};

  // const updateOrderItem = (orderItem: EnhancedCreateOrderItemInput) => {
  //   setOrderItems((prev) => prev.map((x) => (x.id === orderItem.id ? orderItem : x)));
  // };

  // const removeOrderItem = (orderItem: EnhancedCreateOrderItemInput) => {
  //   setOrderItems((prev) => prev.filter((x) => x.id !== orderItem.id));
  // };

  return (
    <Stack>
      <Paper withBorder p="lg">
        <Text>ID: {order.id}</Text>
        <Text>Status: {order.status}</Text>
        <Button color="red" variant="light" onClick={cancelOrder}>
          Cancel order
        </Button>
      </Paper>

      <OrderProducts orderItems={enhancedOrderItems} orderId={order.id} />

      {/* <OrderItemList
        orderItems={orderItems}
        confirmText="Update order items"
        onConfirm={saveOrderItems}
        updateOrderItem={updateOrderItem}
        removeOrderItem={removeOrderItem}
      /> */}
    </Stack>
  );
};
