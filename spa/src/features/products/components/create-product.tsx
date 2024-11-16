import { Button, NumberInput, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { CreateProductInput, createProductInputSchema, useCreateProduct } from '../api/create-product';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { showNotification } from '@/lib/notifications';
import { useState } from 'react';

export const CreateProduct = () => {
  const [isCreating, setIsCreating] = useState<boolean>(false);

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
      },
      onSettled: () => {
        setIsCreating(false);
      },
    },
  });

  const handleSubmit = (values: CreateProductInput) => {
    setIsCreating(true);
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
        <Button type="submit" mt="xs" fullWidth loading={isCreating}>
          Add
        </Button>
      </Stack>
    </form>
  );
};
