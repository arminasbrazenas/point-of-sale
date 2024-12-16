import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { Service } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getServices = (
  paginationFilter: PaginationFilter,
  businessId: number,
): Promise<PagedResponse<Service>> => {
  return api.get('/v1/services', {
    params: { ...paginationFilter, businessId },
  });
};

export const getServicesQueryOptions = (paginationFilter: PaginationFilter, businessId: number) => {
  return queryOptions({
    queryKey: ['services', paginationFilter, businessId],
    queryFn: () => getServices(paginationFilter, businessId),
  });
};

type UseServicesOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getServicesQueryOptions>;
};

export const useServices = ({ paginationFilter, queryConfig }: UseServicesOptions) => {
  const businessId = useAppStore((state) => {
    const applicationUser = state.applicationUser;

    if (!applicationUser || applicationUser.businessId === null) {
      throw new Error('Application user with business is required to get services.');
    }

    return applicationUser.businessId;
  });

  return useQuery({
    ...getServicesQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};
