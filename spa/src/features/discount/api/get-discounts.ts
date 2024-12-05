import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Discount, Modifier } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getDiscounts = (paginationFilter: PaginationFilter): Promise<PagedResponse<Discount>> => {
  return api.get('/v1/discounts', {
    params: paginationFilter,
  });
};

export const getDiscountsQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['discounts', paginationFilter],
    queryFn: () => getDiscounts(paginationFilter),
  });
};

type UseDiscountsOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getDiscountsQueryOptions>;
};

export const useDiscounts = ({ paginationFilter, queryConfig }: UseDiscountsOptions) => {
  return useQuery({
    ...getDiscountsQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
