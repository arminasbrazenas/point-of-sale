import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Order } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createOrUpdateOrderItemInputSchema = z.object({
  productId: z.number(),
  modifierIds: z.array(z.number()),
  quantity: z.coerce.number(),
});

export const createOrderInputSchema = z.object({
  orderItems: z.array(createOrUpdateOrderItemInputSchema),
});

export type CreateOrUpdateOrderItemInput = z.infer<typeof createOrUpdateOrderItemInputSchema>;

export type CreateOrderInput = z.infer<typeof createOrderInputSchema>;

export const createOrder = ({ data }: { data: CreateOrderInput }): Promise<Order> => {
  return api.post('/v1/orders', data);
};

type UseCreateOrderOptions = {
  mutationConfig?: MutationConfig<typeof createOrder>;
};

export const useCreateOrder = ({ mutationConfig }: UseCreateOrderOptions = {}) => {
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
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: createOrder,
  });
};
