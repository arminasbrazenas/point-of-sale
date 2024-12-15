import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { Modifier } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getModifiers = (paginationFilter: PaginationFilter, businessId: number): Promise<PagedResponse<Modifier>> => {
  return api.get('/v1/modifiers', {
    params: {...paginationFilter, businessId},
  });
};

export const getModifiersQueryOptions = (paginationFilter: PaginationFilter, businessId: number) => {
  return queryOptions({
    queryKey: ['modifiers', paginationFilter, businessId],
    queryFn: () => getModifiers(paginationFilter, businessId),
  });
};

type UseModifiersOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getModifiersQueryOptions>;
};

export const useModifiers = ({ paginationFilter, queryConfig }: UseModifiersOptions) => {
  const businessId = useAppStore((state) => {
      const applicationUser = state.applicationUser;
  
      if (!applicationUser || applicationUser.businessId === null) {
        throw new Error('Application user with business is required to get modifiers.');
      }
  
      return applicationUser.businessId;
    });
  
  return useQuery({
    ...getModifiersQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};
