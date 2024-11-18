import { Button, NumberInput, Paper, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { CreateTaxInput, createTaxInputSchema, useCreateTax } from '../api/create-tax';

export const AddTax = () => {
  const navigate = useNavigate();

  const form = useForm<CreateTaxInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      rate: 0,
    },
    validate: zodResolver(createTaxInputSchema),
  });

  const createTaxMutation = useCreateTax({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Tax added successfully.',
        });

        navigate(paths.management.taxes.getHref());
      },
    },
  });

  const createTax = (values: CreateTaxInput) => {
    createTaxMutation.mutate({ data: values });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(createTax)}>
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
          <Button type="submit" mt="xs" fullWidth loading={createTaxMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
