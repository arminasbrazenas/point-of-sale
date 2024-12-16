import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { ServiceCharge } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getServiceCharges = (paginationFilter: PaginationFilter, businessId:number): Promise<PagedResponse<ServiceCharge>> => {
  return api.get('/v1/service-charges', {
    params: {...paginationFilter, businessId},
  });
};

export const getServiceChargesQueryOptions = (paginationFilter: PaginationFilter, businessId: number) => {
  return queryOptions({
    queryKey: ['service-charges', paginationFilter, businessId],
    queryFn: () => getServiceCharges(paginationFilter, businessId),
  });
};

type UseServiceChargesOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getServiceChargesQueryOptions>;
};

export const useServiceCharges = ({ paginationFilter, queryConfig }: UseServiceChargesOptions) => {
  const businessId = useAppStore((state) => {
      const applicationUser = state.applicationUser;
  
      if (!applicationUser || applicationUser.businessId === null) {
        throw new Error('Application user with business is required to get service charges.');
      }
  
      return applicationUser.businessId;
    });
    
  return useQuery({
    ...getServiceChargesQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};
