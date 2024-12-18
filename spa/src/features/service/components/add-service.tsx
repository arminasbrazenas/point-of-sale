import { Button, MultiSelect, NumberInput, Paper, Stack, TextInput } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useAppStore } from '@/lib/app-store';
import { CreateServiceInput, createServiceInputSchema, useCreateService } from '../api/create-service';
import { useEmployees } from '@/features/employee/api/get-employees';

export const AddService = () => {
  const navigate = useNavigate();
  const businessId = useAppStore((state) => state.applicationUser?.businessId);
  if (!businessId) {
    throw new Error('Business ID is required to create a service.');
  }

  const employeesQuery = useEmployees({ paginationFilter: { page: 1, itemsPerPage: 50 } });
  const form = useForm<CreateServiceInput>({
    mode: 'uncontrolled',
    initialValues: {
      name: '',
      price: 0,
      durationInMinutes: 0,
      providedByEmployeesWithId: [],
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

  if (employeesQuery.isPending) {
    return <div>loading...</div>;
  }

  const employees = employeesQuery.data?.items;
  if (!employees) {
    return null;
  }

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
            label="Price (€)"
            placeholder="Price (€)"
            withAsterisk
            key={form.key('price')}
            {...form.getInputProps('price')}
          />
          <NumberInput
            label="Duration (minutes)"
            placeholder="Duration (minutes)"
            withAsterisk
            key={form.key('durationInMinutes')}
            {...form.getInputProps('durationInMinutes')}
          />
          <MultiSelect
            label="Provided by employees"
            placeholder="Select employees"
            data={employees.map((e) => ({ value: e.id.toString(), label: `${e.firstName} ${e.lastName}` }))}
            value={form.getInputProps('providedByEmployeesWithId').defaultValue.map((id: number) => id.toString())}
            onChange={(values) => {
              const ids = values.map((v) => parseInt(v));
              form.setFieldValue('providedByEmployeesWithId', ids);
            }}
          />
          <Button type="submit" mt="xs" fullWidth loading={createServiceMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
