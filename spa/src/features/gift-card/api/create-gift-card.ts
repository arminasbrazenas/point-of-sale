import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { ServiceCharge } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createGiftCardInputSchema = z.object({
  code: z.string(),
  amount: z.coerce.number(),
  expiresAt: z.date(),
});

export type CreateGiftCardInput = z.infer<typeof createGiftCardInputSchema>;

export const createGiftCard = ({ data }: { data: CreateGiftCardInput }): Promise<ServiceCharge> => {
  return api.post('/v1/gift-cards', data);
};

type UseCreateGiftCardOptions = {
  mutationConfig?: MutationConfig<typeof createGiftCard>;
};

export const useCreateGiftCard = ({ mutationConfig }: UseCreateGiftCardOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['gift-cards'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: createGiftCard,
  });
};
