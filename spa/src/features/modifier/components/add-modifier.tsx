import { Button, NumberInput, Paper, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { CreateModifierInput, createModifierInputSchema, useCreateModifier } from '../api/create-modifier';
import { useAppStore } from '@/lib/app-store';

export const AddModifier = () => {
  const navigate = useNavigate();

  const businessId = useAppStore((state) => state.applicationUser?.businessId);
        if (!businessId) {
          throw new Error("Business ID is required to create a product.");
        }
      

  const form = useForm<CreateModifierInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      stock: 0,
      price: 0,
    },
    validate: zodResolver(createModifierInputSchema),
  });

  const createModifierMutation = useCreateModifier({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Modifier added successfully.',
        });

        navigate(paths.management.modifiers.getHref());
      },
    },
  });

  const handleSubmit = (values: CreateModifierInput) => {
        createModifierMutation.mutate({ 
          data: { ...values, businessId } 
        });
  };

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
            label="Price"
            placeholder="Price"
            withAsterisk
            key={form.key('price')}
            {...form.getInputProps('price')}
          />
          <Button type="submit" mt="xs" fullWidth loading={createModifierMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
