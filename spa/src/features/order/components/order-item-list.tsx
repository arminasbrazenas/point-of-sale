import { Button, Card, Stack, Text } from '@mantine/core';
import { EnhancedCreateOrderItemInput } from './order-product';
import { OrderItem } from './order-item';
import { convertToMoney } from '@/utilities';
import { useMemo } from 'react';

export type OrderItemListProps = {
  orderItems: EnhancedCreateOrderItemInput[];
  onConfirm: () => void;
  updateOrderItem: (orderItem: EnhancedCreateOrderItemInput) => void;
  removeOrderItem: (orderItem: EnhancedCreateOrderItemInput) => void;
  isLoading: boolean;
  confirmText: string;
};

export const OrderItemList = (props: OrderItemListProps) => {
  const totalPrice = useMemo(() => {
    return props.orderItems.map((x) => x.price).reduce((acc, curr) => acc + curr, 0);
  }, [props.orderItems]);

  if (props.orderItems.length <= 0) {
    return null;
  }

  return (
    <Card withBorder>
      <Text fw={600}>Order items</Text>
      <Stack gap="xs" mt="xs">
        {props.orderItems.map((orderItem, idx) => (
          <OrderItem update={props.updateOrderItem} remove={props.removeOrderItem} orderItem={orderItem} key={idx} />
        ))}
        <Text fw={600} ta="right">
          Total price: {convertToMoney(totalPrice)}€
        </Text>
        <Button onClick={props.onConfirm} loading={props.isLoading}>
          {props.confirmText}
        </Button>
      </Stack>
    </Card>
  );
};
