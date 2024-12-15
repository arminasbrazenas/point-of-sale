import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { Product } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getProducts = (paginationFilter: PaginationFilter, businessId: number): Promise<PagedResponse<Product>> => {
  return api.get('/v1/products', {
    params: {...paginationFilter, businessId},
  });
};

export const getProductsQueryOptions = (paginationFilter: PaginationFilter, businessId:number) => {
  return queryOptions({
    queryKey: ['products', paginationFilter, businessId],
    queryFn: () => getProducts(paginationFilter, businessId),
  });
};

type UseProductsOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getProductsQueryOptions>;
};

export const useProducts = ({ paginationFilter, queryConfig }: UseProductsOptions) => {
  const businessId = useAppStore((state) => {
      const applicationUser = state.applicationUser;
  
      if (!applicationUser || applicationUser.businessId === null) {
        throw new Error('Application user with business is required to get discounts.');
      }
  
      return applicationUser.businessId;
    });
    
  return useQuery({
    ...getProductsQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};
