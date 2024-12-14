import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { OrderPayments } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

type GetOrderPaymentsParams = {
  orderId: number;
};

export const getOrderPayments = (params: GetOrderPaymentsParams): Promise<OrderPayments> => {
  return api.get('/v1/payments', {
    params,
  });
};

export const getOrderPaymentsQueryOptions = (params: GetOrderPaymentsParams) => {
  return queryOptions({
    queryKey: ['payments', params],
    queryFn: () => getOrderPayments(params),
  });
};

type UseOrderPaymentsOptions = {
  params: GetOrderPaymentsParams;
  queryConfig?: QueryConfig<typeof getOrderPaymentsQueryOptions>;
};

export const useOrderPayments = ({ params, queryConfig }: UseOrderPaymentsOptions) => {
  return useQuery({
    ...getOrderPaymentsQueryOptions(params),
    ...queryConfig,
  });
};
