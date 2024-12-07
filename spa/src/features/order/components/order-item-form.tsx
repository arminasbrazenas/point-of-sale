import { Button, Checkbox, Group, NumberInput, Stack, Text } from '@mantine/core';
import { EnhancedCreateOrderItemInput } from './order-product';
import { toRoundedPrice } from '@/utilities';
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
      modifierIds: props.orderItem?.modifierIds ?? [],
      quantity: props.orderItem?.quantity ?? 1,
    },
    validate: zodResolver(createOrUpdateOrderItemInputSchema),
  });

  const addOrUpdateOrderItem = (values: CreateOrUpdateOrderItemInput) => {
    const modifiers = props.product.modifiers.filter((m) => values.modifierIds.includes(m.id));
    const modifiersPrice = modifiers.map((m) => m.price).reduce((acc, curr) => acc + curr, 0);
    const unitPrice = props.product.price + modifiersPrice;
    const totalPrice = unitPrice * values.quantity;

    props.onConfirm({
      ...values,
      orderedQuantity: props.orderItem?.orderedQuantity,
      cartItemId: props.orderItem?.cartItemId ?? crypto.randomUUID(),
      product: props.product,
      price: toRoundedPrice(totalPrice),
      modifiers: modifiers,
    });
  };

  const addModifier = (modifierId: number) => {
    const modifierIds = [...form.getInputProps('modifierIds').defaultValue, modifierId];
    form.setFieldValue('modifierIds', modifierIds);
  };

  const removeModifier = (modifierId: number) => {
    const modifierIds = form.getInputProps('modifierIds').defaultValue.filter((m: number) => m !== modifierId);
    form.setFieldValue('modifierIds', modifierIds);
  };

  return (
    <form onSubmit={form.onSubmit(addOrUpdateOrderItem)}>
      <Text mt="md" fw={600}>
        {props.product.name}
      </Text>
      <Group gap="xs">
        {props.product.priceDiscountExcluded && (
          <Text opacity={0.5} td="line-through">
            {props.product.priceDiscountExcluded}€
          </Text>
        )}
        <Text c="blue">{props.product.price}€</Text>
      </Group>
      <NumberInput
        label="Quantity"
        mt="xs"
        withAsterisk
        key={form.key('quantity')}
        {...form.getInputProps('quantity')}
      />
      {props.product.modifiers.length > 0 && (
        <Stack mt="xs" gap="xs">
          <Text size="sm" fw={500}>
            Modifiers
          </Text>
          {props.product.modifiers.map((m) => (
            <Checkbox
              key={m.id}
              label={
                <Text size="sm">
                  {m.name}{' '}
                  <Text component="span" opacity={0.5}>
                    (+{m.price}€)
                  </Text>
                </Text>
              }
              checked={form.getInputProps('modifierIds').defaultValue.includes(m.id)}
              onChange={(e) => (e.currentTarget.checked ? addModifier(m.id) : removeModifier(m.id))}
            />
          ))}
        </Stack>
      )}
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
