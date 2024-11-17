import { Button, NumberInput, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { CreateProductInput, createProductInputSchema, useCreateProduct } from '../api/create-product';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { showNotification } from '@/lib/notifications';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';

export const CreateProduct = () => {
  const navigate = useNavigate();

  const form = useForm<CreateProductInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      stock: 0,
      price: 0,
      taxIds: [],
    },
    validate: zodResolver(createProductInputSchema),
  });

  const createProductMutation = useCreateProduct({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Product added successfully.',
        });

        navigate(paths.management.products.getHref());
      },
    },
  });

  const handleSubmit = (values: CreateProductInput) => {
    createProductMutation.mutate({ data: values });
  };

  return (
    <form onSubmit={form.onSubmit(handleSubmit)}>
      <Stack>
        <TextInput
          label="Name"
          placeholder="Name"
          withAsterisk
          key={form.key('name')}
          {...form.getInputProps('name')}
        />
        <NumberInput
          label="Stock"
          placeholder="Stock"
          withAsterisk
          key={form.key('stock')}
          {...form.getInputProps('stock')}
        />
        <CurrencyInput
          label="Price"
          placeholder="Price"
          withAsterisk
          key={form.key('price')}
          {...form.getInputProps('price')}
        />
        <Button type="submit" mt="xs" fullWidth loading={createProductMutation.isPending}>
          Add
        </Button>
      </Stack>
    </form>
  );
};
