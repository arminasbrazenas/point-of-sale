import { Button, Checkbox, Divider, Group, NumberInput, Paper, Select, Stack, Text } from '@mantine/core';
import { EnhancedCreateOrderItemInput } from './order-product';
import { toReadablePricingStrategy, toReadablePricingStrategyAmount, toRoundedPrice } from '@/utilities';
import { useForm, zodResolver } from '@mantine/form';
import {
  CreateOrUpdateDiscountInput,
  createOrUpdateOrderDiscountInputSchema,
  CreateOrUpdateOrderItemInput,
  createOrUpdateOrderItemInputSchema,
} from '../api/create-order';
import { DiscountType, PricingStrategy, Product } from '@/types/api';

type OrderItemFormProps = {
  product: Product;
  orderItem?: EnhancedCreateOrderItemInput;
  onCancel: () => void;
  onRemove?: () => void;
  onConfirm: (orderItem: EnhancedCreateOrderItemInput) => void;
  confirmText: string;
  cartItemId: number;
};

export type EnhancedOrderDiscount = CreateOrUpdateDiscountInput & {
  id: string;
};

export const OrderItemForm = (props: OrderItemFormProps) => {
  const discountForm = useForm<EnhancedOrderDiscount>({
    mode: 'uncontrolled',
    initialValues: {
      id: '',
      type: DiscountType.Flexible,
      amount: 0,
      pricingStrategy: PricingStrategy.Percentage,
    },
    validate: zodResolver(createOrUpdateOrderDiscountInputSchema),
  });

  const form = useForm<CreateOrUpdateOrderItemInput>({
    mode: 'uncontrolled',
    initialValues: {
      productId: props.product.id,
      modifierIds: props.orderItem?.modifierIds ?? [],
      quantity: props.orderItem?.quantity ?? 1,
      discounts: props.orderItem?.discounts ?? [],
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
      cartItemId: props.cartItemId,
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

  const addDiscount = (data: CreateOrUpdateDiscountInput) => {
    const discounts = [...form.getInputProps('discounts').defaultValue, { ...data, id: crypto.randomUUID() }];
    form.setFieldValue('discounts', discounts);
  };

  const removeDiscount = (discountId: string) => {
    const discounts = form
      .getInputProps('discounts')
      .defaultValue.filter((d: EnhancedOrderDiscount) => d.id !== discountId);
    form.setFieldValue('discounts', discounts);
  };

  return (
    <>
      <form onSubmit={form.onSubmit(addOrUpdateOrderItem)}>
        <Text mt="md" fw={600}>
          {props.product.name}
        </Text>
        <Group gap="xs">
          {props.product.discounts.length > 0 && (
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

      <Divider my="md" />

      <Text fw={600}>Discounts</Text>
      <form onSubmit={discountForm.onSubmit(addDiscount)}>
        <Stack gap="xs" mt="xs">
          <Select
            label="Type"
            placeholder="Type"
            data={[
              { value: PricingStrategy.Percentage, label: toReadablePricingStrategy(PricingStrategy.Percentage) },
              { value: PricingStrategy.FixedAmount, label: toReadablePricingStrategy(PricingStrategy.FixedAmount) },
            ]}
            value={discountForm.getInputProps('pricingStrategy').defaultValue}
            allowDeselect={false}
            onChange={(value) => (value ? discountForm.setFieldValue('pricingStrategy', value) : {})}
            withAsterisk
          />
          <NumberInput
            label={`Amount (${
              discountForm.getInputProps('pricingStrategy').defaultValue === PricingStrategy.Percentage ? '%' : '€'
            })`}
            placeholder="Amount"
            withAsterisk
            key={discountForm.key('amount')}
            {...discountForm.getInputProps('amount')}
          />
          <Button type="submit" variant="light">
            Add discount
          </Button>
        </Stack>
      </form>

      <Stack gap="xs" mt="md">
        {form.getInputProps('discounts').defaultValue.map((d: EnhancedOrderDiscount) => (
          <Paper key={d.id} withBorder px="md" py="xs">
            <Group justify="space-between">
              <Text>{toReadablePricingStrategyAmount(d.amount, d.pricingStrategy as PricingStrategy)}</Text>
              <Button
                variant="light"
                color="red"
                onClick={() => removeDiscount(d.id)}
                disabled={d.type === DiscountType.Predefined}
              >
                Remove
              </Button>
            </Group>
          </Paper>
        ))}
      </Stack>
    </>
  );
};
