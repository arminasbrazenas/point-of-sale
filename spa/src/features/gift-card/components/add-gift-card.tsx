import { Button, NumberInput, Paper, Select, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { PricingStrategy } from '@/types/api';
import { toReadablePricingStrategy } from '@/utilities';
import { CreateGiftCardInput, createGiftCardInputSchema, useCreateGiftCard } from '../api/create-gift-card';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { DateTimePicker } from '@mantine/dates';
import { useAppStore } from '@/lib/app-store';

export const AddGiftCard = () => {
  const navigate = useNavigate();

  const businessId = useAppStore((state) => state.applicationUser?.businessId);
            if (!businessId) {
              throw new Error("Business ID is required to create a product.");
            }

  const form = useForm<CreateGiftCardInput>({
    mode: 'uncontrolled',
    initialValues: {
      code: '',
      amount: 0,
      expiresAt: new Date(),
    },
    validate: zodResolver(createGiftCardInputSchema),
  });

  const createGiftCardMutation = useCreateGiftCard({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Gift card added successfully.',
        });

        navigate(paths.management.giftCards.getHref());
      },
    },
  });

  const createGiftCard = (values: CreateGiftCardInput) => {
    createGiftCardMutation.mutate({  data :{ ...values, businessId}});;
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(createGiftCard)}>
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
          <Button type="submit" mt="xs" fullWidth loading={createGiftCardMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
