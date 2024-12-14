import { Button, Group, Modal, Stack, TextInput, Text, Paper } from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useForm, zodResolver } from '@mantine/form';
import { useEffect, useMemo, useState } from 'react';
import { useDisclosure } from '@mantine/hooks';
import { showNotification } from '@/lib/notifications';
import { useGiftCard } from '../api/get-gift-card';
import { UpdateGiftCardInput, useUpdateGiftCard } from '../api/update-gift-card';
import { useDeleteGiftCard } from '../api/delete-gift-card';
import { CreateGiftCardInput, createGiftCardInputSchema } from '../api/create-gift-card';
import { DateTimePicker } from '@mantine/dates';
import { CurrencyInput } from '@/components/inputs/currency-input';

export const UpdateGiftCard = ({ giftCardId }: { giftCardId: number }) => {
  const giftCardQuery = useGiftCard({ giftCardId });
  const navigate = useNavigate();
  const [updatedGiftCardProperties, setUpdatedGiftCardProperties] = useState<UpdateGiftCardInput>({});
  const [isDeleteModelOpen, { open: openDeleteModal, close: closeDeleteModal }] = useDisclosure(false);

  const deleteGiftCardMutation = useDeleteGiftCard({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Gift card deleted successfully.',
        });

        navigate(paths.management.giftCards.getHref());
      },
    },
  });

  const updateGiftCardMutation = useUpdateGiftCard({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Gift card updated successfully.',
        });

        setUpdatedGiftCardProperties({});
      },
    },
  });

  const form = useForm<CreateGiftCardInput>({
    mode: 'uncontrolled',
    initialValues: {
      code: '',
      amount: 0,
      expiresAt: new Date(),
    },
    validate: zodResolver(createGiftCardInputSchema),
    onValuesChange: (updatedGiftCard) => {
      const giftCard = giftCardQuery.data;
      if (!giftCard) {
        setUpdatedGiftCardProperties({});
        return;
      }

      setUpdatedGiftCardProperties({
        code: giftCard.code === updatedGiftCard.code.trim() ? undefined : updatedGiftCard.code,
        amount: giftCard.amount === updatedGiftCard.amount ? undefined : updatedGiftCard.amount,
        expiresAt:
          new Date(giftCard.expiresAt).getTime() === updatedGiftCard.expiresAt.getTime()
            ? undefined
            : updatedGiftCard.expiresAt,
      });
    },
  });

  useEffect(() => {
    const giftCard = giftCardQuery.data;
    if (!giftCard) {
      return;
    }

    form.setFieldValue('code', giftCard.code);
    form.setFieldValue('amount', giftCard.amount);
    form.setFieldValue('expiresAt', new Date(giftCard.expiresAt));
  }, [giftCardQuery.data]);

  const isAnyGiftCardPropertyChanged = useMemo(
    () => Object.values(updatedGiftCardProperties).every((o) => !o),
    [updatedGiftCardProperties],
  );

  if (giftCardQuery.isLoading) {
    return <div>loading...</div>;
  }

  const giftCard = giftCardQuery.data;
  if (!giftCard) {
    return null;
  }

  const deleteGiftCard = () => {
    deleteGiftCardMutation.mutate({ giftCardId });
  };

  const updateGiftCard = () => {
    updateGiftCardMutation.mutate({ giftCardId, data: updatedGiftCardProperties });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(updateGiftCard)}>
        <Stack>
          <TextInput
            label="Code"
            placeholder="Code"
            withAsterisk
            key={form.key('code')}
            {...form.getInputProps('code')}
          />
          <CurrencyInput
            label="Amount"
            placeholder="Amount"
            withAsterisk
            key={form.key('amount')}
            {...form.getInputProps('amount')}
          />
          <DateTimePicker
            label="Expires at"
            withAsterisk
            key={form.key('expiresAt')}
            {...form.getInputProps('expiresAt')}
          />
          <Group justify="space-between" mt="xs">
            <Button color="red" variant="light" onClick={openDeleteModal}>
              Delete
            </Button>
            <Group>
              <Button variant="default" onClick={() => navigate(paths.management.giftCards.getHref())}>
                Cancel
              </Button>
              <Button type="submit" disabled={isAnyGiftCardPropertyChanged} loading={updateGiftCardMutation.isPending}>
                Save
              </Button>
            </Group>
          </Group>
        </Stack>
      </form>

      <Modal opened={isDeleteModelOpen} onClose={closeDeleteModal} title="Delete gift card">
        <Text mt="md">Are you sure you want to delete the gift card?</Text>
        <Group mt="lg" justify="flex-end">
          <Button variant="default" onClick={closeDeleteModal}>
            Cancel
          </Button>
          <Button color="red" variant="light" loading={deleteGiftCardMutation.isPending} onClick={deleteGiftCard}>
            Delete
          </Button>
        </Group>
      </Modal>
    </Paper>
  );
};
