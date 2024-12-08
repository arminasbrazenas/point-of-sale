import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteGiftCard = ({ giftCardId }: { giftCardId: number }): Promise<void> => {
  return api.delete(`/v1/gift-cards/${giftCardId}`);
};

type UseDeleteGiftCardOptions = {
  mutationConfig?: MutationConfig<typeof deleteGiftCard>;
};

export const useDeleteGiftCard = ({ mutationConfig }: UseDeleteGiftCardOptions) => {
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
    mutationFn: deleteGiftCard,
  });
};
