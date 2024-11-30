import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Modifier } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getModifiers = (paginationFilter: PaginationFilter): Promise<PagedResponse<Modifier>> => {
  return api.get('/v1/modifiers', {
    params: paginationFilter,
  });
};

export const getModifiersQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['modifiers', paginationFilter],
    queryFn: () => getModifiers(paginationFilter),
  });
};

type UseModifiersOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getModifiersQueryOptions>;
};

export const useModifiers = ({ paginationFilter, queryConfig }: UseModifiersOptions) => {
  return useQuery({
    ...getModifiersQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
