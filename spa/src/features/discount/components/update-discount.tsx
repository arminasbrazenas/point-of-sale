import { Button, Group, Modal, NumberInput, Stack, Text, Paper, MultiSelect, Select } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useMemo, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useDiscount } from '../api/get-discount';
import { useProducts } from '@/features/product/api/get-products';
import { UpdateDiscountInput, useUpdateDiscount } from '../api/update-discount';
import { useDeleteDiscount } from '../api/delete-discount';
import { CreateDiscountInput, createDiscountInputSchema } from '../api/create-discount';
import { PricingStrategy } from '@/types/api';
import { isSameNumberSet, toReadablePricingStrategy } from '@/utilities';
import { DateTimePicker } from '@mantine/dates';

export const UpdateDiscount = ({ discountId }: { discountId: number }) => {
  const discountQuery = useDiscount({ discountId });
  const productsQuery = useProducts({ paginationFilter: { page: 1, itemsPerPage: 50 } });
  const navigate = useNavigate();
  const [updatedDiscountProperties, setUpdatedDiscountProperties] = useState<UpdateDiscountInput>({});
  const [selectedProductNames, setSelectedProductNames] = useState<string[]>([]);
  const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

  const deleteDiscountMutation = useDeleteDiscount({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Discount deleted successfully.',
        });

        navigate(paths.management.discounts.getHref());
      },
    },
  });

  const updateDiscountMutation = useUpdateDiscount({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Discount updated successfully.',
        });

        setUpdatedDiscountProperties({});
      },
    },
  });

  const form = useForm<CreateDiscountInput>({
    mode: 'uncontrolled',
    initialValues: {
      amount: 0,
      pricingStrategy: PricingStrategy.Percentage,
      validUntil: new Date(),
      appliesToProductIds: [],
    },
    validate: zodResolver(createDiscountInputSchema),
    onValuesChange: (updatedDiscount) => {
      const discount = discountQuery.data;
      if (!discount) {
        setUpdatedDiscountProperties({});
        return;
      }

      setUpdatedDiscountProperties({
        amount: discount.amount === updatedDiscount.amount ? undefined : updatedDiscount.amount,
        pricingStrategy:
          discount.pricingStrategy === updatedDiscount.pricingStrategy ? undefined : updatedDiscount.pricingStrategy,
        validUntil:
          new Date(discount.validUntil).getTime() === updatedDiscount.validUntil.getTime()
            ? undefined
            : updatedDiscount.validUntil,
        appliesToProductIds: isSameNumberSet(discount.appliesToProductIds, updatedDiscount.appliesToProductIds)
          ? undefined
          : updatedDiscount.appliesToProductIds,
      });
    },
  });

  useEffect(() => {
    const discount = discountQuery.data;
    if (!discount) {
      return;
    }

    form.setFieldValue('amount', discount.amount);
    form.setFieldValue('pricingStrategy', discount.pricingStrategy);
    form.setFieldValue('validUntil', new Date(discount.validUntil));
    form.setFieldValue('appliesToProductIds', discount.appliesToProductIds);
  }, [discountQuery.data]);

  useEffect(() => {
    const discount = discountQuery.data;
    const products = productsQuery.data?.items;
    if (!discount || !products) {
      return;
    }

    const productNames = products.filter((p) => discount.appliesToProductIds.includes(p.id)).map((p) => p.name);
    setSelectedProductNames(productNames);
  }, [discountQuery.data, productsQuery.data]);

  useEffect(() => {
    const products = productsQuery.data?.items;
    if (!products) {
      return;
    }

    const productIds = products.filter((p) => selectedProductNames.includes(p.name)).map((p) => p.id);
    form.setFieldValue('appliesToProductIds', productIds);
  }, [productsQuery.data, selectedProductNames]);

  const isAnyDiscountPropertyChanged = useMemo(
    () => Object.values(updatedDiscountProperties).every((o) => !o),
    [updatedDiscountProperties],
  );

  if (discountQuery.isLoading || productsQuery.isLoading) {
    return <div>loading...</div>;
  }

  const discount = discountQuery.data;
  const products = productsQuery.data?.items;
  if (!discount || !products) {
    return null;
  }

  const deleteDiscount = () => {
    deleteDiscountMutation.mutate({ discountId });
  };

  const updateDiscount = () => {
    updateDiscountMutation.mutate({ discountId, data: updatedDiscountProperties });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(updateDiscount)}>
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
          <Group justify="space-between" mt="xs">
            <Button color="red" variant="light" onClick={openDeleteModal}>
              Delete
            </Button>
            <Group>
              <Button variant="default" onClick={() => navigate(paths.management.discounts.getHref())}>
                Cancel
              </Button>
              <Button type="submit" disabled={isAnyDiscountPropertyChanged} loading={updateDiscountMutation.isPending}>
                Save
              </Button>
            </Group>
          </Group>
        </Stack>
      </form>

      <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Delete discount">
        <Text mt="md">Are you sure you want to delete the discount?</Text>
        <Group mt="lg" justify="flex-end">
          <Button variant="default" onClick={closeDeleteModal}>
            Cancel
          </Button>
          <Button color="red" variant="light" loading={deleteDiscountMutation.isPending} onClick={deleteDiscount}>
            Delete
          </Button>
        </Group>
      </Modal>
    </Paper>
  );
};
