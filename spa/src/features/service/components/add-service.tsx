import { Button, NumberInput, Paper, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useAppStore } from '@/lib/app-store';
import { CreateServiceInput, createServiceInputSchema, useCreateService } from '../api/create-service';

export const AddService = () => {
  const navigate = useNavigate();
  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  if (!businessId) {
    throw new Error('Business ID is required to create a service.');
  }

  const form = useForm<CreateServiceInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      price: 0,
      duration: '',
    },
    validate: zodResolver(createServiceInputSchema),
  });

  const createServiceMutation = useCreateService({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Service added successfully.',
        });

        navigate(paths.management.services.getHref());
      },
    },
  });

  const createService = (values: CreateServiceInput) => {
    createServiceMutation.mutate({ data: { ...values, businessId } });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(createService)}>
        <Stack>
          <TextInput
            label="Name"
            placeholder="Name"
            withAsterisk
            key={form.key('name')}
            {...form.getInputProps('name')}
          />
          <NumberInput
            label="Amount (€)"
            placeholder="Amount (€)"
            withAsterisk
            key={form.key('amount')}
            {...form.getInputProps('amount')}
          />
          <Button type="submit" mt="xs" fullWidth loading={createServiceMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
