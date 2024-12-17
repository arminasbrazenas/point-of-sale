import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Reservation } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { getReservationQueryOptions } from './get-reservation';

export const cancelReservation = ({ reservationId }: { reservationId: number }): Promise<Reservation> => {
  return api.post(`/v1/reservations/${reservationId}/cancel`);
};

type UseCancelReservationOptions = {
  mutationConfig?: MutationConfig<typeof cancelReservation>;
};

export const useCancelReservation = ({ mutationConfig }: UseCancelReservationOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (reservation, ...args) => {
      queryClient.setQueryData(getReservationQueryOptions(reservation.id).queryKey, () => reservation);
      onSuccess?.(reservation, ...args);
    },
    ...restConfig,
    mutationFn: cancelReservation,
  });
};
