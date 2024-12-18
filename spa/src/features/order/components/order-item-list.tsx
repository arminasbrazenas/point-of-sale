import { Button, Card, Checkbox, Group, Paper, Select, SimpleGrid, Stack, Text, TextInput } from '@mantine/core';
import { EnhancedCreateOrderItemInput } from './order-product';
import { OrderItem } from './order-item';
import { formatDate, toReadablePricingStrategy, toReadablePricingStrategyAmount } from '@/utilities';
import { useState } from 'react';
import { DiscountTarget, DiscountType, PricingStrategy, Reservation, ServiceCharge } from '@/types/api';
import { CreateOrUpdateDiscountInput, createOrUpdateOrderDiscountInputSchema } from '../api/create-order';
import { useForm, zodResolver } from '@mantine/form';
import { EnhancedOrderDiscount } from './order-item-form';

export type OrderItemListProps = {
  orderItems: EnhancedCreateOrderItemInput[];
  onConfirm: (
    serviceChargeIds: number[],
    discounts: CreateOrUpdateDiscountInput[],
    reservationId: number | undefined,
  ) => void;
  updateOrderItem: (orderItem: EnhancedCreateOrderItemInput) => void;
  removeOrderItem: (orderItem: EnhancedCreateOrderItemInput) => void;
  isLoading: boolean;
  confirmText: string;
  serviceCharges: ServiceCharge[];
  selectedServiceCharges: string[];
  discounts: CreateOrUpdateDiscountInput[];
  reservations?: Reservation[];
  reservation?: Reservation;
};

export const OrderItemList = (props: OrderItemListProps) => {
  const [selectedReservationId, setSelectedReservationId] = useState<number | undefined>(props.reservation?.id);
  const [selectedServiceChargeNames, setSelectedServiceChargeNames] = useState<string[]>(props.selectedServiceCharges);
  const [discounts, setDiscounts] = useState<EnhancedOrderDiscount[]>(
    props.discounts.map((d) => ({ ...d, id: crypto.randomUUID() })),
  );

  const discountForm = useForm<EnhancedOrderDiscount>({
    mode: 'uncontrolled',
    initialValues: {
      id: '',
      pricingStrategy: PricingStrategy.Percentage,
      amount: 0,
      type: DiscountType.Flexible,
    },
    validate: zodResolver(createOrUpdateOrderDiscountInputSchema),
  });

  const onConfirm = () => {
    const serviceChargeIds = props.serviceCharges
      .filter((c) => selectedServiceChargeNames.includes(c.name))
      .map((c) => c.id);
    props.onConfirm(
      serviceChargeIds,
      discounts.filter((d) => d.type === DiscountType.Flexible),
      selectedReservationId,
    );
  };

  const addServiceCharge = (name: string) => {
    setSelectedServiceChargeNames((prev) => [...prev, name]);
  };

  const removeServiceCharge = (name: string) => {
    setSelectedServiceChargeNames((prev) => prev.filter((c) => c !== name));
  };

  const addOrderDiscount = (values: CreateOrUpdateDiscountInput) => {
    setDiscounts((prev) => [...prev, { ...values, id: crypto.randomUUID() }]);
  };

  const removeOrderDiscount = (id: string) => {
    setDiscounts((prev) => prev.filter((d) => d.id != id));
  };

  const formatReservationLabel = (r: Reservation) =>
    `${r.customer.firstName} ${r.customer.lastName} (${r.description} at ${formatDate(r.date.start)} by ${
      r.employee.fullName
    })`;

  return (
    <Card withBorder>
      {props.reservation && (
        <>
          <Text fw={600}>Reservation</Text>
          <Text mb="sm">{formatReservationLabel(props.reservation)}</Text>
        </>
      )}

      {props.reservations && (
        <Select
          label="Reservation"
          data={props.reservations.map((r) => ({
            label: formatReservationLabel(r),
            value: r.id.toString(),
          }))}
          value={selectedReservationId ? selectedReservationId.toString() : null}
          onChange={(value) => {
            if (!value) {
              setSelectedReservationId(undefined);
            } else {
              setSelectedReservationId(parseInt(value));
            }
          }}
          nothingFoundMessage="No active reservations..."
          mb="sm"
        />
      )}

      {props.orderItems.length > 0 && (
        <>
          <Text fw={600}>Order items</Text>
          <Stack gap="xs" mt="xs" mb="sm">
            {props.orderItems.map((orderItem, idx) => (
              <OrderItem
                update={props.updateOrderItem}
                remove={props.removeOrderItem}
                orderItem={orderItem}
                key={idx}
                orderItemId={idx + 1}
              />
            ))}
          </Stack>
        </>
      )}

      {props.serviceCharges.length > 0 && (
        <>
          <Text fw={600}>Service charges</Text>
          <Stack gap="xs" my="xs">
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
        </>
      )}

      <SimpleGrid cols={2}>
        <Stack gap="xs">
          <Text fw={600}>Apply discount</Text>
          <form onSubmit={discountForm.onSubmit(addOrderDiscount)}>
            <Stack gap="xs">
              <Select
                label="Target"
                placeholder="Target"
                data={[
                  { value: DiscountTarget.Order, label: 'Order' },
                  {
                    value: DiscountTarget.Product,
                    label: 'Product',
                  },
                ]}
                value={discountForm.getInputProps('target').defaultValue}
                allowDeselect={false}
                onChange={(value) => (value ? discountForm.setFieldValue('target', value) : {})}
                withAsterisk
              />
              <Select
                label="Type"
                placeholder="Type"
                data={[
                  { value: PricingStrategy.Percentage, label: toReadablePricingStrategy(PricingStrategy.Percentage) },
                  {
                    value: PricingStrategy.FixedAmount,
                    label: toReadablePricingStrategy(PricingStrategy.FixedAmount),
                  },
                ]}
                value={discountForm.getInputProps('pricingStrategy').defaultValue}
                allowDeselect={false}
                onChange={(value) => (value ? discountForm.setFieldValue('pricingStrategy', value) : {})}
                withAsterisk
              />
              <TextInput
                label="Amount"
                placeholder="Amount"
                withAsterisk
                key={discountForm.key('amount')}
                {...discountForm.getInputProps('amount')}
              />
              <Button variant="light" type="submit">
                Add discount
              </Button>
            </Stack>
          </form>
        </Stack>

        <Stack gap="xs">
          <Text fw={600}>Applied discounts</Text>
          <Stack gap="xs">
            {discounts.map((d) => (
              <Paper withBorder px="md" py="xs" key={d.id}>
                <Group justify="space-between">
                  <Text>{toReadablePricingStrategyAmount(d.amount, d.pricingStrategy as PricingStrategy)}</Text>
                  <Button
                    variant="light"
                    color="red"
                    onClick={() => removeOrderDiscount(d.id)}
                    disabled={d.type === DiscountType.Predefined}
                  >
                    Remove
                  </Button>
                </Group>
              </Paper>
            ))}
          </Stack>
        </Stack>
      </SimpleGrid>

      <Button onClick={onConfirm} loading={props.isLoading} mt="lg">
        {props.confirmText}
      </Button>
    </Card>
  );
};
