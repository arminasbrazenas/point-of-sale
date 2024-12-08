import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { GiftCard } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getGiftCardQueryOptions } from './get-gift-card';

export const updateGiftCardInputSchema = z.object({
  code: z.string().optional(),
  amount: z.coerce.number().optional(),
  expiresAt: z.date().optional(),
});

export type UpdateGiftCardInput = z.infer<typeof updateGiftCardInputSchema>;

export const updateGiftCard = ({
  data,
  giftCardId,
}: {
  data: UpdateGiftCardInput;
  giftCardId: number;
}): Promise<GiftCard> => {
  return api.patch(`/v1/gift-cards/${giftCardId}`, data);
};

type UseUpdateGiftCardOptions = {
  mutationConfig?: MutationConfig<typeof updateGiftCard>;
};

export const useUpdateGiftCard = ({ mutationConfig }: UseUpdateGiftCardOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (giftCard, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['gift-cards'],
      });
      queryClient.setQueryData(getGiftCardQueryOptions(giftCard.id).queryKey, () => giftCard);
      onSuccess?.(giftCard, ...args);
    },
    ...restConfig,
    mutationFn: updateGiftCard,
  });
};
