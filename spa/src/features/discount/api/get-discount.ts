import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Discount, Modifier, Product } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getDiscount = ({ discountId }: { discountId: number }): Promise<Discount> => {
  return api.get(`v1/discounts/${discountId}`);
};

export const getDiscountQueryOptions = (discountId: number) => {
  return queryOptions({
    queryKey: ['discount', discountId],
    queryFn: () => getDiscount({ discountId }),
  });
};

type UseGetDiscountOptions = {
  discountId: number;
  queryConfig?: QueryConfig<typeof getDiscountQueryOptions>;
};

export const useDiscount = ({ discountId, queryConfig }: UseGetDiscountOptions) => {
  return useQuery({
    ...getDiscountQueryOptions(discountId),
    ...queryConfig,
  });
};
