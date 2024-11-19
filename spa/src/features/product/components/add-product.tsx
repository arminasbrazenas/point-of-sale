import { Button, MultiSelect, NumberInput, Paper, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { CreateProductInput, createProductInputSchema, useCreateProduct } from '../api/create-product';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useEffect, useState } from 'react';
import { useTaxes } from '@/features/taxes/api/get-taxes';

export const AddProduct = () => {
  const [selectedTaxNames, setSelectedTaxNames] = useState<string[]>([]);
  const taxesQuery = useTaxes({ paginationFilter: { page: 1, itemsPerPage: 50 } });
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

  useEffect(() => {
    const taxes = taxesQuery.data?.items;
    if (!taxes) {
      return;
    }

    const selectedTaxIds = taxes.filter((tax) => selectedTaxNames.includes(tax.name)).map((tax) => tax.id);
    form.setFieldValue('taxIds', selectedTaxIds);
  }, [taxesQuery.data, selectedTaxNames]);

  const handleSubmit = (values: CreateProductInput) => {
    createProductMutation.mutate({ data: values });
  };

  if (taxesQuery.isLoading) {
    return <div>loading...</div>;
  }

  const taxes = taxesQuery.data?.items;
  if (!taxes) {
    return null;
  }

  return (
    <Paper withBorder p="lg">
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
            label="Price (without taxes)"
            placeholder="Price (without taxes)"
            withAsterisk
            key={form.key('price')}
            {...form.getInputProps('price')}
          />
          <MultiSelect
            label="Taxes"
            placeholder="Applicable taxes"
            data={taxes.map((tax) => tax.name)}
            value={selectedTaxNames}
            onChange={setSelectedTaxNames}
          />
          <Button type="submit" mt="xs" fullWidth loading={createProductMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
