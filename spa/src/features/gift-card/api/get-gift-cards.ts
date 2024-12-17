import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { GiftCard } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getGiftCards = (paginationFilter: PaginationFilter, businessId: number): Promise<PagedResponse<GiftCard>> => {
  return api.get('/v1/gift-cards', {
    params: {...paginationFilter, businessId},
  });
};

export const getGiftCardsQueryOptions = (paginationFilter: PaginationFilter, businessId: number) => {
  return queryOptions({
    queryKey: ['gift-cards', paginationFilter],
    queryFn: () => getGiftCards(paginationFilter, businessId),
  });
};

type UseGiftCardsOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getGiftCardsQueryOptions>;
};

export const useGiftCards = ({ paginationFilter, queryConfig }: UseGiftCardsOptions) => {
  const businessId = useAppStore((state) => {
        const applicationUser = state.applicationUser;
    
        if (!applicationUser || applicationUser.businessId === null) {
          throw new Error('Application user with business is required to get discounts.');
        }
    
        return applicationUser.businessId;
      });
  return useQuery({
    ...getGiftCardsQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};
