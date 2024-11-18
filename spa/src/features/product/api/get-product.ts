import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Product } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getProduct = ({ productId }: { productId: number }): Promise<Product> => {
  return api.get(`v1/products/${productId}`);
};

export const getProductQueryOptions = (productId: number) => {
  return queryOptions({
    queryKey: ['product', productId],
    queryFn: () => getProduct({ productId }),
  });
};

type UseGetProductOptions = {
  productId: number;
  queryConfig?: QueryConfig<typeof getProductQueryOptions>;
};

export const useProduct = ({ productId, queryConfig }: UseGetProductOptions) => {
  return useQuery({
    ...getProductQueryOptions(productId),
    ...queryConfig,
  });
};
