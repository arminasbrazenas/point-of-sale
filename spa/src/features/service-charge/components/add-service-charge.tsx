import { Button, NumberInput, Paper, Select, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import {
  CreateServiceChargeInput,
  createServiceChargeInputSchema,
  useCreateServiceCharge,
} from '../api/create-service-charge';
import { PricingStrategy } from '@/types/api';
import { toReadablePricingStrategy } from '@/utilities';

export const AddServiceCharge = () => {
  const navigate = useNavigate();

  const form = useForm<CreateServiceChargeInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      amount: 0,
      pricingStrategy: PricingStrategy.Percentage,
    },
    validate: zodResolver(createServiceChargeInputSchema),
  });

  const createServiceChargeMutation = useCreateServiceCharge({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Service charge added successfully.',
        });

        navigate(paths.management.serviceCharges.getHref());
      },
    },
  });

  const createServiceCharge = (values: CreateServiceChargeInput) => {
    createServiceChargeMutation.mutate({ data: values });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(createServiceCharge)}>
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
          <Button type="submit" mt="xs" fullWidth loading={createServiceChargeMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
