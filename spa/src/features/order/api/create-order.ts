import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
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
  serviceChargeIds: z.array(z.number()),
});

export type CreateOrUpdateOrderItemInput = z.infer<typeof createOrUpdateOrderItemInputSchema>;

export type CreateOrderInput = z.infer<typeof createOrderInputSchema>;

export const createOrder = ({ data }: { data: CreateOrderInput & { businessId: number }}): Promise<Order> => {
  return api.post('/v1/orders', data);
};

type UseCreateOrderOptions = {
  mutationConfig?: MutationConfig<typeof createOrder>;
};

export const useCreateOrder = ({ mutationConfig }: UseCreateOrderOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessId = useAppStore((state) => state.applicationUser?.businessId);

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
    mutationFn: async ({ data }: { data: CreateOrderInput }) => {
      if (!businessId) {
        const error = new Error('Forbidden');
        (error as any).statusCode = 403;
        throw error;
      }

      return createOrder({ data: { ...data, businessId } });
    },
  });
};
