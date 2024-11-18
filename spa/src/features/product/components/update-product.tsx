import { Button, Group, Modal, NumberInput, Stack, TextInput, Text, Paper } from '@mantine/core';
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

export const UpdateProduct = ({ productId }: { productId: number }) => {
  const productQuery = useProduct({ productId });
  const navigate = useNavigate();
  const [updatedProductProperties, setUpdatedProductProperties] = useState<UpdateProductInput>({});
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
    },
    validate: zodResolver(createProductInputSchema),
    onValuesChange: (updatedProduct) => {
      if (!productQuery.data) {
        setUpdatedProductProperties({});
        return;
      }

      const product = productQuery.data;
      setUpdatedProductProperties({
        name: product.name === updatedProduct.name.trim() ? undefined : updatedProduct.name,
        stock: product.stock === updatedProduct.stock ? undefined : updatedProduct.stock,
        price: product.price === updatedProduct.price ? undefined : updatedProduct.price,
      });
    },
  });

  useEffect(() => {
    if (!productQuery.data) {
      return;
    }

    form.setFieldValue('name', productQuery.data.name);
    form.setFieldValue('stock', productQuery.data.stock);
    form.setFieldValue('price', productQuery.data.price);
  }, [productQuery.data]);

  const isAnyProductPropertyChanged = useMemo(
    () => Object.values(updatedProductProperties).every((o) => !o),
    [updatedProductProperties],
  );

  if (productQuery.isLoading) {
    return <div>loading...</div>;
  }

  const product = productQuery.data;
  if (!product) {
    return null;
  }

  const deleteProduct = () => {
    deleteProductMutation.mutate({ productId });
  };

  const updateProduct = (values: UpdateProductInput) => {
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
            label="Price"
            placeholder="Price"
            withAsterisk
            key={form.key('price')}
            {...form.getInputProps('price')}
          />
          <Group justify="space-between" mt="xs">
            <Button color="red" variant="light" onClick={openDeleteModal}>
              Delete
            </Button>
            <Group>
              <Button variant="default" onClick={() => navigate(paths.management.products.getHref())}>
                Cancel
              </Button>
              <Button type="submit" disabled={isAnyProductPropertyChanged} loading={updateProductMutation.isPending}>
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
