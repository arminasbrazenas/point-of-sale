import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { GiftCard } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getGiftCards = (paginationFilter: PaginationFilter): Promise<PagedResponse<GiftCard>> => {
  return api.get('/v1/gift-cards', {
    params: paginationFilter,
  });
};

export const getGiftCardsQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['gift-cards', paginationFilter],
    queryFn: () => getGiftCards(paginationFilter),
  });
};

type UseGiftCardsOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getGiftCardsQueryOptions>;
};

export const useGiftCards = ({ paginationFilter, queryConfig }: UseGiftCardsOptions) => {
  return useQuery({
    ...getGiftCardsQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
