import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Order } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getOrders = (paginationFilter: PaginationFilter): Promise<PagedResponse<Order>> => {
  return api.get('/v1/orders', {
    params: paginationFilter,
  });
};

export const getOrdersQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['orders', paginationFilter],
    queryFn: () => getOrders(paginationFilter),
  });
};

type UseOrdersOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getOrdersQueryOptions>;
};

export const useOrders = ({ paginationFilter, queryConfig }: UseOrdersOptions) => {
  return useQuery({
    ...getOrdersQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
