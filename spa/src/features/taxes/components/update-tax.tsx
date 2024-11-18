import { Button, Group, Modal, NumberInput, Stack, TextInput, Text, Paper } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useMemo, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useTax } from '../api/get-tax';
import { UpdateTaxInput, useUpdateTax } from '../api/update-tax';
import { useDeleteTax } from '../api/delete-tax';
import { CreateTaxInput, createTaxInputSchema } from '../api/create-tax';

export const UpdateTax = ({ taxId }: { taxId: number }) => {
  const taxQuery = useTax({ taxId });
  const navigate = useNavigate();
  const [updatedTaxProperties, setUpdatedTaxProperties] = useState<UpdateTaxInput>({});
  const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

  const deleteTaxMutation = useDeleteTax({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Tax deleted successfully.',
        });

        navigate(paths.management.taxes.getHref());
      },
    },
  });

  const updateTaxMutation = useUpdateTax({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Tax updated successfully.',
        });

        setUpdatedTaxProperties({});
      },
    },
  });

  const form = useForm<CreateTaxInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      rate: 0,
    },
    validate: zodResolver(createTaxInputSchema),
    onValuesChange: (updatedTax) => {
      if (!taxQuery.data) {
        setUpdatedTaxProperties({});
        return;
      }

      const tax = taxQuery.data;
      setUpdatedTaxProperties({
        name: tax.name === updatedTax.name.trim() ? undefined : updatedTax.name,
        rate: tax.rate === updatedTax.rate ? undefined : updatedTax.rate,
      });
    },
  });

  useEffect(() => {
    if (!taxQuery.data) {
      return;
    }

    form.setFieldValue('name', taxQuery.data.name);
    form.setFieldValue('rate', taxQuery.data.rate);
  }, [taxQuery.data]);

  const isAnyTaxPropertyChanged = useMemo(
    () => Object.values(updatedTaxProperties).every((o) => !o),
    [updatedTaxProperties],
  );

  if (taxQuery.isLoading) {
    return <div>loading...</div>;
  }

  const tax = taxQuery.data;
  if (!tax) {
    return null;
  }

  const deleteTax = () => {
    deleteTaxMutation.mutate({ taxId });
  };

  const updateTax = () => {
    updateTaxMutation.mutate({ taxId, data: updatedTaxProperties });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(updateTax)}>
        <Stack>
          <TextInput
            label="Name"
            placeholder="Name"
            withAsterisk
            key={form.key('name')}
            {...form.getInputProps('name')}
          />
          <NumberInput
            label="Rate"
            placeholder="Rate"
            withAsterisk
            key={form.key('rate')}
            {...form.getInputProps('rate')}
          />
          <Group justify="space-between" mt="xs">
            <Button color="red" variant="light" onClick={openDeleteModal}>
              Delete
            </Button>
            <Group>
              <Button variant="default" onClick={() => navigate(paths.management.taxes.getHref())}>
                Cancel
              </Button>
              <Button type="submit" disabled={isAnyTaxPropertyChanged} loading={updateTaxMutation.isPending}>
                Save
              </Button>
            </Group>
          </Group>
        </Stack>
      </form>

      <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Delete tax">
        <Text mt="md">
          Are you sure you want to delete{' '}
          <Text component="span" fw={600}>
            {tax.name}
          </Text>
          ?
        </Text>
        <Group mt="lg" justify="flex-end">
          <Button variant="default" onClick={closeDeleteModal}>
            Cancel
          </Button>
          <Button color="red" variant="light" loading={deleteTaxMutation.isPending} onClick={deleteTax}>
            Delete
          </Button>
        </Group>
      </Modal>
    </Paper>
  );
};
