import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { GiftCardPayment } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const payByGiftCardInputSchema = z.object({
  orderId: z.coerce.number(),
  giftCardCode: z.string(),
  businessId: z.coerce.number(),
  employeeId: z.coerce.number(),
});

export type PayByGiftCardInput = z.infer<typeof payByGiftCardInputSchema>;

export const payByGiftCard = ({ data }: { data: PayByGiftCardInput }): Promise<GiftCardPayment> => {
  return api.post('/v1/payments/gift-cards', data);
};

type UsePayByGiftCardOptions = {
  mutationConfig?: MutationConfig<typeof payByGiftCard>;
};

export const usePayByGiftCard = ({ mutationConfig }: UsePayByGiftCardOptions = {}) => {
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
    mutationFn: payByGiftCard,
  });
};
