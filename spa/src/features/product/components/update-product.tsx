import { Button, Group, Modal, NumberInput, Stack, TextInput, Text, Paper, MultiSelect } from '@mantine/core';
import { useProduct } from '../api/get-product';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { CreateProductInput, createProductInputSchema } from '../api/create-product';
import { useEffect, useMemo, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { useDeleteProduct } from '../api/delete-product';
import { showNotification } from '@/lib/notifications';
import { UpdateProductInput, useUpdateProduct } from '../api/update-product';
import { useTaxes } from '@/features/taxes/api/get-taxes';
import { useModifiers } from '@/features/modifier/api/get-modifiers';
import { isSameNumberSet } from '@/utilities';

export const UpdateProduct = ({ productId }: { productId: number }) => {
  const productQuery = useProduct({ productId });
  const taxesQuery = useTaxes({ paginationFilter: { page: 1, itemsPerPage: 50 } });
  const modifiersQuery = useModifiers({ paginationFilter: { page: 1, itemsPerPage: 50 } });
  const navigate = useNavigate();
  const [updatedProductProperties, setUpdatedProductProperties] = useState<UpdateProductInput>({});
  const [selectedTaxNames, setSelectedTaxNames] = useState<string[]>([]);
  const [selectedModifierNames, setSelectedModifierNames] = useState<string[]>([]);
  const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

  const deleteProductMutation = useDeleteProduct({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Product deleted successfully.',
        });

        navigate(paths.management.products.getHref());
      },
    },
  });

  const updateProductMutation = useUpdateProduct({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Product updated successfully.',
        });

        setUpdatedProductProperties({});
      },
    },
  });

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
    onValuesChange: (updatedProduct) => {
      const product = productQuery.data;
      const taxes = taxesQuery.data?.items;
      const modifiers = modifiersQuery.data?.items;
      if (!product || !taxes || !modifiers) {
        setUpdatedProductProperties({});
        return;
      }

      const productTaxIds = product.taxes.map((tax) => tax.id);
      const selectedTaxIds = taxes.filter((tax) => selectedTaxNames.includes(tax.name)).map((tax) => tax.id);
      const productModifierIds = product.modifiers.map((m) => m.id);
      const selectedModifierIds = modifiers.filter((m) => selectedModifierNames.includes(m.name)).map((m) => m.id);

      setUpdatedProductProperties({
        name: product.name === updatedProduct.name.trim() ? undefined : updatedProduct.name,
        stock: product.stock === updatedProduct.stock ? undefined : updatedProduct.stock,
        price: product.basePrice === updatedProduct.price ? undefined : updatedProduct.price,
        taxIds: isSameNumberSet(productTaxIds, selectedTaxIds) ? undefined : selectedTaxIds,
        modifierIds: isSameNumberSet(productModifierIds, selectedModifierIds) ? undefined : selectedModifierIds,
      });
    },
  });

  useEffect(() => {
    const product = productQuery.data;
    if (!product) {
      return;
    }

    setSelectedTaxNames(product.taxes.map((tax) => tax.name));
    setSelectedModifierNames(product.modifiers.map((m) => m.name));
    form.setFieldValue('name', product.name);
    form.setFieldValue('stock', product.stock);
    form.setFieldValue('price', product.basePrice);
  }, [productQuery.data]);

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

  if (productQuery.isLoading || taxesQuery.isLoading || modifiersQuery.isLoading) {
    return <div>loading...</div>;
  }

  const product = productQuery.data;
  const taxes = taxesQuery.data?.items;
  const modifiers = modifiersQuery.data?.items;
  if (!product || !taxes || !modifiers) {
    return null;
  }

  const deleteProduct = () => {
    deleteProductMutation.mutate({ productId });
  };

  const updateProduct = () => {
    updateProductMutation.mutate({ productId, data: updatedProductProperties });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(updateProduct)}>
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
          <CurrencyInput label="Price (with taxes)" value={product.priceDiscountExcluded} disabled />
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
          <Group justify="space-between" mt="xs">
            <Button color="red" variant="light" onClick={openDeleteModal}>
              Delete
            </Button>
            <Group>
              <Button variant="default" onClick={() => navigate(paths.management.products.getHref())}>
                Cancel
              </Button>
              <Button type="submit" loading={updateProductMutation.isPending}>
                Save
              </Button>
            </Group>
          </Group>
        </Stack>
      </form>

      <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Delete product">
        <Text mt="md">
          Are you sure you want to delete{' '}
          <Text component="span" fw={600}>
            {product.name}
          </Text>
          ?
        </Text>
        <Group mt="lg" justify="flex-end">
          <Button variant="default" onClick={closeDeleteModal}>
            Cancel
          </Button>
          <Button color="red" variant="light" loading={deleteProductMutation.isPending} onClick={deleteProduct}>
            Delete
          </Button>
        </Group>
      </Modal>
    </Paper>
  );
};
