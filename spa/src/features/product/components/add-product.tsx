import { Button, MultiSelect, NumberInput, Paper, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { CreateProductInput, createProductInputSchema, useCreateProduct } from '../api/create-product';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useEffect, useState } from 'react';
import { useTaxes } from '@/features/taxes/api/get-taxes';
import { useModifiers } from '@/features/modifier/api/get-modifiers';
import { useAppStore } from '@/lib/app-store';

export const AddProduct = () => {
  const [selectedTaxNames, setSelectedTaxNames] = useState<string[]>([]);
  const [selectedModifierNames, setSelectedModifierNames] = useState<string[]>([]);
  const taxesQuery = useTaxes({ paginationFilter: { page: 1, itemsPerPage: 50 } });
  const modifiersQuery = useModifiers({ paginationFilter: { page: 1, itemsPerPage: 50 } });
  const navigate = useNavigate();
  const businessId = useAppStore((state) => state.applicationUser?.businessId);
        if (!businessId) {
          throw new Error("Business ID is required to create a product.");
        }
      

  const form = useForm<CreateProductInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      stock: 0,
      price: 0,
      taxIds: [],
      modifierIds: [],
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

  useEffect(() => {
    const modifiers = modifiersQuery.data?.items;
    if (!modifiers) {
      return;
    }

    const selectedModifierIds = modifiers.filter((m) => selectedModifierNames.includes(m.name)).map((m) => m.id);
    form.setFieldValue('modifierIds', selectedModifierIds);
  }, [modifiersQuery.data, selectedModifierNames]);

  const handleSubmit = (values: CreateProductInput) => { 
    createProductMutation.mutate({ 
      data: { ...values, businessId } 
    });
  };

  if (taxesQuery.isLoading || modifiersQuery.isLoading) {
    return <div>loading...</div>;
  }

  const taxes = taxesQuery.data?.items;
  const modifiers = modifiersQuery.data?.items;
  if (!taxes || !modifiers) {
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
            label="Price (pre-tax)"
            placeholder="Price (pre-tax)"
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
          <MultiSelect
            label="Modifiers"
            placeholder="Compatible modifiers"
            data={modifiers.map((m) => m.name)}
            value={selectedModifierNames}
            onChange={setSelectedModifierNames}
          />
          <Button type="submit" mt="xs" fullWidth loading={createProductMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
