import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { MutationConfig } from '@/lib/react-query';
import { Reservation } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createReservationInputSchema = z.object({
  employeeId: z.number(),
  serviceId: z.number(),
  startDate: z.coerce.string(),
  customer: z.object({
    firstName: z.string(),
    lastName: z.string(),
  }),
});

export type CreateReservationInput = z.infer<typeof createReservationInputSchema>;

export const createReservation = ({
  data,
}: {
  data: CreateReservationInput & { businessId: number };
}): Promise<Reservation> => {
  return api.post('/v1/reservations', data);
};

type UseCreateReservationOptions = {
  mutationConfig?: MutationConfig<typeof createReservation>;
};

export const useCreateReservatino = ({ mutationConfig }: UseCreateReservationOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  const businessId = useAppStore((state) => state.applicationUser?.businessId);

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['reservations'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: async ({ data }: { data: CreateReservationInput }) => {
      if (!businessId) {
        const error = new Error('Forbidden');
        (error as any).statusCode = 403;
        throw error;
      }

      return createReservation({ data: { ...data, businessId } });
    },
  });
};
