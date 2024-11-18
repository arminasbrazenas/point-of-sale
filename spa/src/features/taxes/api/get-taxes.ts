import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Tax } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getTaxes = (paginationFilter: PaginationFilter): Promise<PagedResponse<Tax>> => {
  return api.get('/v1/taxes', {
    params: paginationFilter,
  });
};

export const getTaxesQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['taxes', paginationFilter],
    queryFn: () => getTaxes(paginationFilter),
  });
};

type UseTaxesOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getTaxesQueryOptions>;
};

export const useTaxes = ({ paginationFilter, queryConfig }: UseTaxesOptions) => {
  return useQuery({
    ...getTaxesQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
