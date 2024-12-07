import { Button, MultiSelect, NumberInput, Paper, Select, Stack } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { CreateDiscountInput, createDiscountInputSchema, useCreateDiscount } from '../api/create-discount';
import { PricingStrategy } from '@/types/api';
import { useProducts } from '@/features/product/api/get-products';
import { toReadablePricingStrategy } from '@/utilities';
import { DateTimePicker } from '@mantine/dates';
import { useState } from 'react';

export const AddDiscount = () => {
  const [selectedProductNames, setSelectedProductNames] = useState<string[]>([]);
  const navigate = useNavigate();
  const productsQuery = useProducts({ paginationFilter: { page: 1, itemsPerPage: 50 } });

  const form = useForm<CreateDiscountInput>({
    mode: 'uncontrolled',
    initialValues: {
      amount: 0,
      pricingStrategy: PricingStrategy.Percentage,
      validUntil: new Date(),
      appliesToProductIds: [],
    },
    validate: zodResolver(createDiscountInputSchema),
  });

  const createDiscountMutation = useCreateDiscount({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Discount added successfully.',
        });

        navigate(paths.management.discounts.getHref());
      },
    },
  });

  if (productsQuery.isLoading) {
    return <div>loading...</div>;
  }

  const products = productsQuery.data?.items;
  if (!products) {
    return null;
  }

  const handleSubmit = (values: CreateDiscountInput) => {
    const appliesToProductIds = products.filter((p) => selectedProductNames.includes(p.name)).map((p) => p.id);
    createDiscountMutation.mutate({ data: { ...values, appliesToProductIds } });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(handleSubmit)}>
        <Stack>
          <Select
            label="Type"
            placeholder="Type"
            data={[
              { value: PricingStrategy.Percentage, label: toReadablePricingStrategy(PricingStrategy.Percentage) },
              { value: PricingStrategy.FixedAmount, label: toReadablePricingStrategy(PricingStrategy.FixedAmount) },
            ]}
            value={form.getInputProps('pricingStrategy').defaultValue}
            allowDeselect={false}
            onChange={(value) => (value ? form.setFieldValue('pricingStrategy', value) : {})}
            withAsterisk
          />
          <NumberInput
            label={`Amount (${
              form.getInputProps('pricingStrategy').defaultValue === PricingStrategy.Percentage ? '%' : '€'
            })`}
            placeholder="Amount"
            withAsterisk
            key={form.key('amount')}
            {...form.getInputProps('amount')}
          />
          <DateTimePicker
            label="Valid until"
            withAsterisk
            key={form.key('validUntil')}
            {...form.getInputProps('validUntil')}
          />
          <MultiSelect
            label="Applies to products"
            data={products.map((product) => product.name)}
            value={selectedProductNames}
            onChange={setSelectedProductNames}
          />
          <Button type="submit" mt="xs" fullWidth loading={createDiscountMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
