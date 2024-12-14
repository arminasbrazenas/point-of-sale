import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Tip } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

type GetOrderTipsParams = {
  orderId: number;
};

export const getOrderTips = (params: GetOrderTipsParams): Promise<Tip[]> => {
  return api.get('/v1/payments/tips', {
    params,
  });
};

export const getOrderTipsQueryOptions = (params: GetOrderTipsParams) => {
  return queryOptions({
    queryKey: ['order-tips', params],
    queryFn: () => getOrderTips(params),
  });
};

type UseOrderTipsOptions = {
  params: GetOrderTipsParams;
  queryConfig?: QueryConfig<typeof getOrderTipsQueryOptions>;
};

export const useOrderTips = ({ params, queryConfig }: UseOrderTipsOptions) => {
  return useQuery({
    ...getOrderTipsQueryOptions(params),
    ...queryConfig,
  });
};
