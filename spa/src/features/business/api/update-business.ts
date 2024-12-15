import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Business } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getBusinessQueryOptions } from '../api/get-business';

export const updateBusinessInputSchema = z.object({
    name: z.string().optional(),
    address: z.string().optional(),
    email: z.string().optional(),
    phoneNumber: z.string().optional(),
});

export type UpdateBusinessInput = z.infer<typeof updateBusinessInputSchema>;

export const updateBusiness = ({
  data,
  businessId,
}: {
  data: UpdateBusinessInput;
  businessId: number;
}): Promise<Business> => {
  return api.patch(`/v1/businesses/${businessId}`, data);
};

type UseUpdateBusinessOptions = {
  mutationConfig?: MutationConfig<typeof updateBusiness>;
};

export const useUpdateBusiness = ({ mutationConfig }: UseUpdateBusinessOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (business, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['businesses'],
      });
      queryClient.setQueryData(getBusinessQueryOptions(business.id).queryKey, () => business);
      onSuccess?.(business, ...args);
    },
    ...restConfig,
    mutationFn: updateBusiness,
  });
};
