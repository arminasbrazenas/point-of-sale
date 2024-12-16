import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { ApplicationUser, PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getBusinessOwners = (paginationFilter?: PaginationFilter): Promise<PagedResponse<ApplicationUser>> => {
  return api.get('/v1/users', {
    params: {
      ...paginationFilter,
      role: 'BusinessOwner',
      businessId: null,
    },
  });
};

export const getBusinessOwnersQueryOptions = (paginationFilter?: PaginationFilter) => {
  return queryOptions({
    queryKey: ['businessOwners', paginationFilter],
    queryFn: () => getBusinessOwners(paginationFilter),
  });
};

type UseBusinessOwnersOptions = {
  paginationFilter?: PaginationFilter;
  queryConfig?: QueryConfig<typeof getBusinessOwnersQueryOptions>;
};

export const useBusinessOwners = ({ paginationFilter, queryConfig }: UseBusinessOwnersOptions = {}) => {
  return useQuery({
    ...getBusinessOwnersQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
