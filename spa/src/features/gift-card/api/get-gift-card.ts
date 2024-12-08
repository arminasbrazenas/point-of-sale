import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { GiftCard } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getGiftCard = ({ giftCardId }: { giftCardId: number }): Promise<GiftCard> => {
  return api.get(`v1/gift-cards/${giftCardId}`);
};

export const getGiftCardQueryOptions = (giftCardId: number) => {
  return queryOptions({
    queryKey: ['gift-card', giftCardId],
    queryFn: () => getGiftCard({ giftCardId }),
  });
};

type UseGetGiftCardOptions = {
  giftCardId: number;
  queryConfig?: QueryConfig<typeof getGiftCardQueryOptions>;
};

export const useGiftCard = ({ giftCardId, queryConfig }: UseGetGiftCardOptions) => {
  return useQuery({
    ...getGiftCardQueryOptions(giftCardId),
    ...queryConfig,
  });
};
