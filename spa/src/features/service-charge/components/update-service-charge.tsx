import { Button, Group, Modal, NumberInput, Stack, TextInput, Text, Paper, Select } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useMemo, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useServiceCharge } from '../api/get-service-charge';
import { UpdateServiceChargeInput, useUpdateServiceCharge } from '../api/update-service-charge';
import { useDeleteServiceCharge } from '../api/delete-service-charge';
import { CreateServiceChargeInput, createServiceChargeInputSchema } from '../api/create-service-charge';
import { PricingStrategy } from '@/types/api';
import { toReadablePricingStrategy } from '@/utilities';

export const UpdateServiceCharge = ({ serviceChargeId }: { serviceChargeId: number }) => {
  const serviceChargeQuery = useServiceCharge({ serviceChargeId });
  const navigate = useNavigate();
  const [updatedServiceChargeProperties, setUpdatedServiceChargeProperties] = useState<UpdateServiceChargeInput>({});
  const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

  const deleteServiceChargeMutation = useDeleteServiceCharge({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Service charge deleted successfully.',
        });

        navigate(paths.management.serviceCharges.getHref());
      },
    },
  });

  const updateServiceChargeMutation = useUpdateServiceCharge({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Service charge updated successfully.',
        });

        setUpdatedServiceChargeProperties({});
      },
    },
  });

  const form = useForm<CreateServiceChargeInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      amount: 0,
      pricingStrategy: PricingStrategy.Percentage,
    },
    validate: zodResolver(createServiceChargeInputSchema),
    onValuesChange: (updatedServiceCharge) => {
      const serviceCharge = serviceChargeQuery.data;
      if (!serviceCharge) {
        setUpdatedServiceChargeProperties({});
        return;
      }

      setUpdatedServiceChargeProperties({
        name: serviceCharge.name === updatedServiceCharge.name.trim() ? undefined : updatedServiceCharge.name,
        amount: serviceCharge.amount === updatedServiceCharge.amount ? undefined : updatedServiceCharge.amount,
        pricingStrategy:
          serviceCharge.pricingStrategy === updatedServiceCharge.pricingStrategy
            ? undefined
            : updatedServiceCharge.pricingStrategy,
      });
    },
  });

  useEffect(() => {
    const serviceCharge = serviceChargeQuery.data;
    if (!serviceCharge) {
      return;
    }

    form.setFieldValue('name', serviceCharge.name);
    form.setFieldValue('amount', serviceCharge.amount);
    form.setFieldValue('pricingStrategy', serviceCharge.pricingStrategy);
  }, [serviceChargeQuery.data]);

  const isAnyServiceChargePropertyChanged = useMemo(
    () => Object.values(updatedServiceChargeProperties).every((o) => !o),
    [updatedServiceChargeProperties],
  );

  if (serviceChargeQuery.isLoading) {
    return <div>loading...</div>;
  }

  const serviceCharge = serviceChargeQuery.data;
  if (!serviceCharge) {
    return null;
  }

  const deleteServiceCharge = () => {
    deleteServiceChargeMutation.mutate({ serviceChargeId });
  };

  const updateServiceCharge = () => {
    updateServiceChargeMutation.mutate({ serviceChargeId, data: updatedServiceChargeProperties });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(updateServiceCharge)}>
        <Stack>
          <TextInput
            label="Name"
            placeholder="Name"
            withAsterisk
            key={form.key('name')}
            {...form.getInputProps('name')}
          />
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
          <Group justify="space-between" mt="xs">
            <Button color="red" variant="light" onClick={openDeleteModal}>
              Delete
            </Button>
            <Group>
              <Button variant="default" onClick={() => navigate(paths.management.serviceCharges.getHref())}>
                Cancel
              </Button>
              <Button
                type="submit"
                disabled={isAnyServiceChargePropertyChanged}
                loading={updateServiceChargeMutation.isPending}
              >
                Save
              </Button>
            </Group>
          </Group>
        </Stack>
      </form>

      <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Delete service charge">
        <Text mt="md">
          Are you sure you want to delete{' '}
          <Text component="span" fw={600}>
            {serviceCharge.name}
          </Text>
          ?
        </Text>
        <Group mt="lg" justify="flex-end">
          <Button variant="default" onClick={closeDeleteModal}>
            Cancel
          </Button>
          <Button
            color="red"
            variant="light"
            loading={deleteServiceChargeMutation.isPending}
            onClick={deleteServiceCharge}
          >
            Delete
          </Button>
        </Group>
      </Modal>
    </Paper>
  );
};
