import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteReservation = ({ reservationId }: { reservationId: number }): Promise<void> => {
  return api.delete(`/v1/reservations/${reservationId}`);
};

type UseDeleteReservationOptions = {
  mutationConfig?: MutationConfig<typeof deleteReservation>;
};

export const useDeleteReservation = ({ mutationConfig }: UseDeleteReservationOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['reservations'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: deleteReservation,
  });
};
