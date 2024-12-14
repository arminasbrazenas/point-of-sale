import { Button, Group, Modal, NumberInput, Stack, TextInput, Text, Paper } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useMemo, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useModifier } from '../api/get-modifier';
import { UpdateModifierInput, useUpdateModifier } from '../api/update-modifier';
import { useDeleteModifier } from '../api/delete-modifier';
import { CreateModifierInput, createModifierInputSchema } from '../api/create-modifier';
import { CurrencyInput } from '@/components/inputs/currency-input';

export const UpdateModifier = ({ modifierId }: { modifierId: number }) => {
  const modifierQuery = useModifier({ modifierId });
  const navigate = useNavigate();
  const [updatedModifierProperties, setUpdatedModifierProperties] = useState<UpdateModifierInput>({});
  const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

  const deleteModifierMutation = useDeleteModifier({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Modifier deleted successfully.',
        });

        navigate(paths.management.modifiers.getHref());
      },
    },
  });

  const updateModifierMutation = useUpdateModifier({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Modifier updated successfully.',
        });

        setUpdatedModifierProperties({});
      },
    },
  });

  const form = useForm<CreateModifierInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      price: 0,
      stock: 0,
    },
    validate: zodResolver(createModifierInputSchema),
    onValuesChange: (updatedModifier) => {
      const modifier = modifierQuery.data;
      if (!modifier) {
        setUpdatedModifierProperties({});
        return;
      }

      setUpdatedModifierProperties({
        name: modifier.name === updatedModifier.name.trim() ? undefined : updatedModifier.name,
        price: modifier.priceTaxExcluded === updatedModifier.price ? undefined : updatedModifier.price,
        stock: modifier.stock === updatedModifier.stock ? undefined : updatedModifier.stock,
      });
    },
  });

  useEffect(() => {
    if (!modifierQuery.data) {
      return;
    }

    form.setFieldValue('name', modifierQuery.data.name);
    form.setFieldValue('price', modifierQuery.data.priceTaxExcluded);
    form.setFieldValue('stock', modifierQuery.data.stock);
  }, [modifierQuery.data]);

  const isAnyModifierPropertyChanged = useMemo(
    () => Object.values(updatedModifierProperties).every((o) => !o),
    [updatedModifierProperties],
  );

  if (modifierQuery.isLoading) {
    return <div>loading...</div>;
  }

  const modifier = modifierQuery.data;
  if (!modifier) {
    return null;
  }

  const deleteModifier = () => {
    deleteModifierMutation.mutate({ modifierId });
  };

  const updateModifier = () => {
    updateModifierMutation.mutate({ modifierId, data: updatedModifierProperties });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(updateModifier)}>
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
              <Button variant="default" onClick={() => navigate(paths.management.modifiers.getHref())}>
                Cancel
              </Button>
              <Button type="submit" disabled={isAnyModifierPropertyChanged} loading={updateModifierMutation.isPending}>
                Save
              </Button>
            </Group>
          </Group>
        </Stack>
      </form>

      <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Delete modifier">
        <Text mt="md">
          Are you sure you want to delete{' '}
          <Text component="span" fw={600}>
            {modifier.name}
          </Text>
          ?
        </Text>
        <Group mt="lg" justify="flex-end">
          <Button variant="default" onClick={closeDeleteModal}>
            Cancel
          </Button>
          <Button color="red" variant="light" loading={deleteModifierMutation.isPending} onClick={deleteModifier}>
            Delete
          </Button>
        </Group>
      </Modal>
    </Paper>
  );
};
