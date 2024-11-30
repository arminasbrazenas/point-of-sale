import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Order } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getOrder = ({ orderId }: { orderId: number }): Promise<Order> => {
  return api.get(`v1/orders/${orderId}`);
};

export const getOrderQueryOptions = (orderId: number) => {
  return queryOptions({
    queryKey: ['order', orderId],
    queryFn: () => getOrder({ orderId }),
  });
};

type UseGetOrderOptions = {
  orderId: number;
  queryConfig?: QueryConfig<typeof getOrderQueryOptions>;
};

export const useOrder = ({ orderId, queryConfig }: UseGetOrderOptions) => {
  return useQuery({
    ...getOrderQueryOptions(orderId),
    ...queryConfig,
  });
};
