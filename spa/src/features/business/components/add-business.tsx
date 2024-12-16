import { Button, Paper, Stack, TextInput, Select } from '@mantine/core';
import { useForm, zodResolver } from '@mantine/form';
import { showNotification } from '@/lib/notifications';
import { useNavigate } from 'react-router-dom';
import { paths } from '@/config/paths';
import { useCreateBusiness } from '../api/create-business';
import { useAppStore } from '@/lib/app-store';
import { z } from 'zod';
import { useBusinessOwners } from '../api/get-business-owners';

export const AddBusiness = () => {
  const navigate = useNavigate();
  const role = useAppStore((state) => state.applicationUser?.role);
  const businessOwnerIdFromStore = useAppStore((state) => state.applicationUser?.id);

  const initialBusinessOwnerId = role === 'Admin' ? 0 : (businessOwnerIdFromStore ?? 0);

  const createBusinessFormInputSchema = z.object({
    name: z.string().min(1, 'Name is required'),
    email: z.string().email('Invalid email address'),
    phoneNumber: z.string().min(1, 'Phone number is required'),
    address: z.string().min(1, 'Address is required'),
    businessOwnerId: z.number().positive('Business Owner is required'),
  });

  type CreateBusinessFormInput = z.infer<typeof createBusinessFormInputSchema>;

  const form = useForm<CreateBusinessFormInput>({
    initialValues: {
      name: '',
      email: '',
      phoneNumber: '',
      address: '',
      businessOwnerId: initialBusinessOwnerId,
    },
    validate: zodResolver(createBusinessFormInputSchema),
  });

  const businessOwnersQuery = role === 'Admin'
    ? useBusinessOwners({ paginationFilter: { page: 1, itemsPerPage: 50 } })
    : { data: { items: [] }, isLoading: false };

  const createBusinessMutation = useCreateBusiness({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Business added successfully.',
        });
        navigate(paths.businessManagement.businesses.getHref());
      },
      onError: (error) => {
        console.error('Error creating business:', error);
        showNotification({
          type: 'failure',
          title: 'Failed to add business',
          message: error.message || 'An unexpected error occurred',
        });
      },
    },
  });

  if (role === 'Admin' && businessOwnersQuery.isLoading) {
    return <div>Loading possible business owners...</div>;
  }

  const potentialOwners = businessOwnersQuery.data?.items || [];

  const handleSubmit = (values: CreateBusinessFormInput) => {
    createBusinessMutation.mutate({
      data: values,
    });
  };

  return (
    <Paper withBorder p="lg">
      <form onSubmit={form.onSubmit(handleSubmit)}>
        <Stack>
          {role === 'Admin' && (
            <Select
              label="Business Owner"
              placeholder="Select a business owner"
              withAsterisk
              data={potentialOwners.map((owner) => ({
                value: owner.id.toString(),
                label: `${owner.firstName} ${owner.lastName}`,
              }))}
              value={form.values.businessOwnerId > 0 ? form.values.businessOwnerId.toString() : ''}
              onChange={(value) => form.setFieldValue('businessOwnerId', value ? Number(value) : 0)}
            />
          )}

          <TextInput
            label="Name"
            placeholder="Name"
            withAsterisk
            {...form.getInputProps('name')}
          />
          <TextInput
            label="Address"
            placeholder="Address"
            withAsterisk
            {...form.getInputProps('address')}
          />
          <TextInput
            label="Email"
            placeholder="Email"
            withAsterisk
            {...form.getInputProps('email')}
          />
          <TextInput
            label="Phone Number"
            placeholder="Phone Number"
            withAsterisk
            {...form.getInputProps('phoneNumber')}
          />
          <Button type="submit" mt="xs" fullWidth loading={createBusinessMutation.isPending}>
            Add
          </Button>
        </Stack>
      </form>
    </Paper>
  );
};
