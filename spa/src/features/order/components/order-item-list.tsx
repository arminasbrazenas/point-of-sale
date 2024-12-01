import { Button, Card, Checkbox, Stack, Text } from '@mantine/core';
import { EnhancedCreateOrderItemInput } from './order-product';
import { OrderItem } from './order-item';
import { toReadablePricingStrategyAmount } from '@/utilities';
import { useState } from 'react';
import { ServiceCharge } from '@/types/api';

export type OrderItemListProps = {
  orderItems: EnhancedCreateOrderItemInput[];
  onConfirm: (serviceChargeIds: number[]) => void;
  updateOrderItem: (orderItem: EnhancedCreateOrderItemInput) => void;
  removeOrderItem: (orderItem: EnhancedCreateOrderItemInput) => void;
  isLoading: boolean;
  confirmText: string;
  serviceCharges: ServiceCharge[];
  selectedServiceCharges: string[];
};

export const OrderItemList = (props: OrderItemListProps) => {
  const [selectedServiceChargeNames, setSelectedServiceChargeNames] = useState<string[]>(props.selectedServiceCharges);

  const onConfirm = () => {
    const serviceChargeIds = props.serviceCharges
      .filter((c) => selectedServiceChargeNames.includes(c.name))
      .map((c) => c.id);
    props.onConfirm(serviceChargeIds);
  };

  const addServiceCharge = (name: string) => {
    setSelectedServiceChargeNames((prev) => [...prev, name]);
  };

  const removeServiceCharge = (name: string) => {
    setSelectedServiceChargeNames((prev) => prev.filter((c) => c !== name));
  };

  if (props.orderItems.length <= 0) {
    return null;
  }

  return (
    <Card withBorder>
      <Text fw={600}>Order items</Text>
      <Stack gap="xs" mt="xs" mb="md">
        {props.orderItems.map((orderItem, idx) => (
          <OrderItem update={props.updateOrderItem} remove={props.removeOrderItem} orderItem={orderItem} key={idx} />
        ))}
      </Stack>

      <Text fw={600}>Service charges</Text>
      <Stack gap="xs" mt="xs">
        {props.serviceCharges.map((c, idx) => (
          <Checkbox
            label={
              <Text size="sm">
                {c.name}{' '}
                <Text component="span" opacity={0.5}>
                  ({toReadablePricingStrategyAmount(c.amount, c.pricingStrategy)})
                </Text>
              </Text>
            }
            checked={selectedServiceChargeNames.includes(c.name)}
            onChange={(e) => (e.currentTarget.checked ? addServiceCharge(c.name) : removeServiceCharge(c.name))}
            key={idx}
          />
        ))}
      </Stack>

      <Button onClick={onConfirm} loading={props.isLoading} mt="lg">
        {props.confirmText}
      </Button>
    </Card>
  );
};
