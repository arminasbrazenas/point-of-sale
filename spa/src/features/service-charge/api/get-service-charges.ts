import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { ServiceCharge } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getServiceCharges = (paginationFilter: PaginationFilter): Promise<PagedResponse<ServiceCharge>> => {
  return api.get('/v1/service-charges', {
    params: paginationFilter,
  });
};

export const getServiceChargesQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['service-charges', paginationFilter],
    queryFn: () => getServiceCharges(paginationFilter),
  });
};

type UseServiceChargesOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getServiceChargesQueryOptions>;
};

export const useServiceCharges = ({ paginationFilter, queryConfig }: UseServiceChargesOptions) => {
  return useQuery({
    ...getServiceChargesQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
