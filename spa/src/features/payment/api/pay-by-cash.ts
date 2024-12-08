import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { CashPayment } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const payByCashInputSchema = z.object({
  orderId: z.coerce.number(),
  paymentAmount: z.coerce.number(),
});

export type PayByCashInput = z.infer<typeof payByCashInputSchema>;

export const payByCash = ({ data }: { data: PayByCashInput }): Promise<CashPayment> => {
  return api.post('/v1/payments/cash', data);
};

type UsePayByCashOptions = {
  mutationConfig?: MutationConfig<typeof payByCash>;
};

export const usePayByCash = ({ mutationConfig }: UsePayByCashOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['payments'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: payByCash,
  });
};
