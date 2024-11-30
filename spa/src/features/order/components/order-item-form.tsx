import { Button, Group, NumberInput, Text } from '@mantine/core';
import { EnhancedCreateOrderItemInput } from './order-product';
import { convertToMoney } from '@/utilities';
import { useForm, zodResolver } from '@mantine/form';
import { CreateOrUpdateOrderItemInput, createOrUpdateOrderItemInputSchema } from '../api/create-order';
import { Product } from '@/types/api';

type OrderItemFormProps = {
  product: Product;
  orderItem?: EnhancedCreateOrderItemInput;
  onCancel: () => void;
  onRemove?: () => void;
  onConfirm: (orderItem: EnhancedCreateOrderItemInput) => void;
  confirmText: string;
};

export const OrderItemForm = (props: OrderItemFormProps) => {
  const form = useForm<CreateOrUpdateOrderItemInput>({
    mode: 'uncontrolled',
    initialValues: {
      productId: props.product.id,
      modifierIds: [],
      quantity: props.orderItem?.quantity ?? 1,
    },
    validate: zodResolver(createOrUpdateOrderItemInputSchema),
  });

  const addOrUpdateOrderItem = (values: CreateOrUpdateOrderItemInput) => {
    props.onConfirm({
      ...values,
      orderedQuantity: props.orderItem?.orderedQuantity,
      cartItemId: props.orderItem?.cartItemId ?? crypto.randomUUID(),
      product: props.product,
      price: props.product.priceWithTaxes * values.quantity,
    });
  };

  return (
    <form onSubmit={form.onSubmit(addOrUpdateOrderItem)}>
      <Text mt="md" fw={600}>
        {props.product.name}
      </Text>
      <Text opacity={0.5}>{convertToMoney(props.product.priceWithTaxes)}€</Text>
      <NumberInput
        label="Quantity"
        mt="xs"
        withAsterisk
        key={form.key('quantity')}
        {...form.getInputProps('quantity')}
      />
      <Group justify="space-between" mt="lg">
        <Button
          color="red"
          variant="light"
          style={{ visibility: props.onRemove ? 'visible' : 'hidden' }}
          onClick={props.onRemove}
        >
          Remove
        </Button>
        <Group justify="flex-end">
          <Button variant="default" onClick={props.onCancel}>
            Cancel
          </Button>
          <Button type="submit">{props.confirmText}</Button>
        </Group>
      </Group>
    </form>
  );
};
