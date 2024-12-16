import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Reservation, Service } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getReservationQueryOptions } from './get-reservation';

export const updateReservationInputSchema = z.object({
  employeeId: z.number().optional(),
  serviceId: z.number().optional(),
  startDate: z.coerce.string().optional(),
  customer: z
    .object({
      firstName: z.string(),
      lastName: z.string(),
    })
    .optional(),
});

export type UpdateReservationInput = z.infer<typeof updateReservationInputSchema>;

export const updateReservation = ({
  data,
  reservationId,
}: {
  data: UpdateReservationInput;
  reservationId: number;
}): Promise<Reservation> => {
  return api.patch(`/v1/reservations/${reservationId}`, data);
};

type UseUpdateReservationOptions = {
  mutationConfig?: MutationConfig<typeof updateReservation>;
};

export const useUpdateReservation = ({ mutationConfig }: UseUpdateReservationOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (reservation, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['reservations'],
      });
      queryClient.setQueryData(getReservationQueryOptions(reservation.id).queryKey, () => reservation);
      onSuccess?.(reservation, ...args);
    },
    ...restConfig,
    mutationFn: updateReservation,
  });
};
