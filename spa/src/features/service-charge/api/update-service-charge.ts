import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { ServiceCharge } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getServiceChargeQueryOptions } from './get-service-charge';

export const updateServiceChargeInputSchema = z.object({
  name: z.string().optional(),
  amount: z.coerce.number().optional(),
  pricingStrategy: z.coerce.string().optional(),
});

export type UpdateServiceChargeInput = z.infer<typeof updateServiceChargeInputSchema>;

export const updateServiceCharge = ({
  data,
  serviceChargeId,
}: {
  data: UpdateServiceChargeInput;
  serviceChargeId: number;
}): Promise<ServiceCharge> => {
  return api.patch(`/v1/service-charges/${serviceChargeId}`, data);
};

type UseUpdateServiceChargeOptions = {
  mutationConfig?: MutationConfig<typeof updateServiceCharge>;
};

export const useUpdateServiceCharge = ({ mutationConfig }: UseUpdateServiceChargeOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (serviceCharge, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['service-charges'],
      });
      queryClient.setQueryData(getServiceChargeQueryOptions(serviceCharge.id).queryKey, () => serviceCharge);
      onSuccess?.(serviceCharge, ...args);
    },
    ...restConfig,
    mutationFn: updateServiceCharge,
  });
};
