import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { Order } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getOrders = (paginationFilter: PaginationFilter, businessId: number): Promise<PagedResponse<Order>> => {
  return api.get('/v1/orders', {
    params: {...paginationFilter, businessId},
  });
};

export const getOrdersQueryOptions = (paginationFilter: PaginationFilter, businessId: number) => {
  return queryOptions({
    queryKey: ['orders', paginationFilter, businessId],
    queryFn: () => getOrders(paginationFilter, businessId),
  });
};

type UseOrdersOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getOrdersQueryOptions>;
};

export const useOrders = ({ paginationFilter, queryConfig }: UseOrdersOptions) => {
  const businessId = useAppStore((state) => {
      const applicationUser = state.applicationUser;
  
      if (!applicationUser || applicationUser.businessId === null) {
        throw new Error('Application user with business is required to get orders.');
      }
  
      return applicationUser.businessId;
    });
  return useQuery({
    ...getOrdersQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};
