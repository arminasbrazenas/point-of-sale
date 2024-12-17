import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const cancelReservation = ({ reservationId }: { reservationId: number }): Promise<void> => {
  return api.post(`/v1/reservation/cancel`, reservationId);
};

type UseCancelReservationOptions = {
  mutationConfig?: MutationConfig<typeof cancelReservation>;
};

export const useCancelReservation = ({ mutationConfig }: UseCancelReservationOptions) => {
  const queryClient = useQueryClient();

  return useMutation({
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ['reservations'],
      });
    },
    ...mutationConfig,
    mutationFn: cancelReservation,
  });
};