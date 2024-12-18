import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { Tax } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getTaxes = (paginationFilter: PaginationFilter, businessId: number): Promise<PagedResponse<Tax>> => {
  return api.get('/v1/taxes', {
    params: { ...paginationFilter, businessId },
  });
};

export const getTaxesQueryOptions = (paginationFilter: PaginationFilter, businessId: number) => {
  return queryOptions({
    queryKey: ['taxes', paginationFilter],
    queryFn: () => getTaxes(paginationFilter, businessId),
  });
};

type UseTaxesOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getTaxesQueryOptions>;
};

export const useTaxes = ({ paginationFilter, queryConfig }: UseTaxesOptions) => {
  const businessId = useAppStore((state) => {
    const applicationUser = state.applicationUser;

    if (!applicationUser || applicationUser.businessId === null) {
      throw new Error('Application user with business is required to get discounts.');
    }

    return applicationUser.businessId;
  });
  return useQuery({
    ...getTaxesQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};
