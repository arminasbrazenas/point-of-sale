import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Business, PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getBusinesses = (paginationFilter: PaginationFilter): Promise<PagedResponse<Business>> => {
  return api.get('/v1/businesses', {
    params: paginationFilter
  });
};

export const getBusinessesQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['businesses', paginationFilter],
    queryFn: () => getBusinesses(paginationFilter),
  });
};

type UseBusinessesOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getBusinessesQueryOptions>;
};

export const useBusinesses = ({ paginationFilter, queryConfig }: UseBusinessesOptions) => {
  return useQuery({
    ...getBusinessesQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
