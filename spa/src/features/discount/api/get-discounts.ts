import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { Discount, Modifier } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getDiscounts = (paginationFilter: PaginationFilter, businessId: number): Promise<PagedResponse<Discount>> => {
  return api.get('/v1/discounts', {
    params: {...paginationFilter, businessId},
  });
};

export const getDiscountsQueryOptions = (paginationFilter: PaginationFilter, businessId: number) => {
  return queryOptions({
    queryKey: ['discounts', paginationFilter],
    queryFn: () => getDiscounts(paginationFilter, businessId),
  });
};

type UseDiscountsOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getDiscountsQueryOptions>;
};

export const useDiscounts = ({ paginationFilter, queryConfig }: UseDiscountsOptions) => {
  const businessId = useAppStore((state) => {
    const applicationUser = state.applicationUser;

    if (!applicationUser || applicationUser.businessId === null) {
      throw new Error('Application user with business is required to get discounts.');
    }

    return applicationUser.businessId;
  });

  return useQuery({
    ...getDiscountsQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};