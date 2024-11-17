import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Product } from '@/types/product';
import { PagedResponse, PaginationFilter } from '@/types/shared';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getProducts = (paginationFilter: PaginationFilter): Promise<PagedResponse<Product>> => {
  return api.get('/v1/products', {
    params: paginationFilter,
  });
};

export const getProductsQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['products', paginationFilter],
    queryFn: () => getProducts(paginationFilter),
  });
};

type UseProductsOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getProductsQueryOptions>;
};

export const useProducts = ({ paginationFilter, queryConfig }: UseProductsOptions) => {
  return useQuery({
    ...getProductsQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
