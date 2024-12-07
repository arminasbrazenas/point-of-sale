import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { OrderReceipt } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getOrderReceipt = ({ orderId }: { orderId: number }): Promise<OrderReceipt> => {
  return api.get(`v1/orders/${orderId}/receipt`);
};

export const getOrderReceiptQueryOptions = (orderId: number) => {
  return queryOptions({
    queryKey: ['order', orderId, 'receipt'],
    queryFn: () => getOrderReceipt({ orderId }),
  });
};

type UseGetOrderReceiptOptions = {
  orderId: number;
  queryConfig?: QueryConfig<typeof getOrderReceiptQueryOptions>;
};

export const useOrderReceipt = ({ orderId, queryConfig }: UseGetOrderReceiptOptions) => {
  return useQuery({
    ...getOrderReceiptQueryOptions(orderId),
    ...queryConfig,
  });
};
