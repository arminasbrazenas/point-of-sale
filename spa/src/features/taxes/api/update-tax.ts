import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Tax } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getTaxQueryOptions } from './get-tax';

export const updateTaxInputSchema = z.object({
  name: z.string().optional(),
  rate: z.coerce.number().optional(),
});

export type UpdateTaxInput = z.infer<typeof updateTaxInputSchema>;

export const updateTax = ({ data, taxId }: { data: UpdateTaxInput; taxId: number }): Promise<Tax> => {
  return api.patch(`/v1/taxes/${taxId}`, data);
};

type UseUpdateTaxOptions = {
  mutationConfig?: MutationConfig<typeof updateTax>;
};

export const useUpdateTax = ({ mutationConfig }: UseUpdateTaxOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (tax, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['taxes'],
      });
      queryClient.setQueryData(getTaxQueryOptions(tax.id).queryKey, () => tax);
      onSuccess?.(tax, ...args);
    },
    ...restConfig,
    mutationFn: updateTax,
  });
};
