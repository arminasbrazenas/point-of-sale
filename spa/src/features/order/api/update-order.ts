import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Order } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { createOrUpdateOrderDiscountInputSchema, createOrUpdateOrderItemInputSchema } from './create-order';

export const updateOrderInputSchema = z.object({
  orderItems: z.array(createOrUpdateOrderItemInputSchema).optional(),
  serviceChargeIds: z.array(z.number()).optional(),
  discounts: z.array(createOrUpdateOrderDiscountInputSchema).optional(),
  reservationId: z.number().optional(),
});

export type UpdateOrderInput = z.infer<typeof updateOrderInputSchema>;

export const updateOrder = ({ orderId, data }: { orderId: number; data: UpdateOrderInput }): Promise<Order> => {
  return api.patch(`/v1/orders/${orderId}`, data);
};

type UseUpdateOrderOptions = {
  mutationConfig?: MutationConfig<typeof updateOrder>;
};

export const useUpdateOrder = ({ mutationConfig }: UseUpdateOrderOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['products'],
      });
      queryClient.invalidateQueries({
        queryKey: ['product'],
      });
      queryClient.invalidateQueries({
        queryKey: ['orders'],
      });
      queryClient.invalidateQueries({
        queryKey: ['order'],
      });
      queryClient.invalidateQueries({
        queryKey: ['payments'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: updateOrder,
  });
};
