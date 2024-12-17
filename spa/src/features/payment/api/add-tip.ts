import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Tip } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const addTipInputSchema = z.object({
  orderId: z.coerce.number(),
  tipAmount: z.coerce.number(),
  employeeId: z.coerce.number(),
});

export type AddTipInput = z.infer<typeof addTipInputSchema>;

export const addTip = ({ data }: { data: AddTipInput }): Promise<Tip> => {
  return api.post('/v1/payments/tips', data);
};

type UseAddTipOptions = {
  mutationConfig?: MutationConfig<typeof addTip>;
};

export const useAddTip = ({ mutationConfig }: UseAddTipOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['order-tips'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: addTip,
  });
};
