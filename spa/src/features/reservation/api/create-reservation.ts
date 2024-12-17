import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Reservation } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createReservationSchema = z.object({
  serviceId: z.number(),
  customerId: z.number(),
  appointmentTime: z.date(),
});

export type CreateReservationInput = z.infer<typeof createReservationSchema>;

export const createReservation = ({ data }: { data: CreateReservationInput }): Promise<Reservation> => {
  return api.post('/v1/reservation/create', {
    ...data,
    appointmentTime: Math.floor(data.appointmentTime.getTime() / 1000), // Convert to Unix timestamp
  });
};

type UseCreateReservationOptions = {
  mutationConfig?: MutationConfig<typeof createReservation>;
};

export const useCreateReservation = ({ mutationConfig }: UseCreateReservationOptions = {}) => {
  const queryClient = useQueryClient();

  return useMutation({
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ['reservations'],
      });
    },
    ...mutationConfig,
    mutationFn: createReservation,
  });
};