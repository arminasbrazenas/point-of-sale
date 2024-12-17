import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Reservation, ReservationStatus } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const updateReservationSchema = z.object({
  serviceId: z.number().optional(),
  customerId: z.number().optional(),
  appointmentTime: z.date().optional(),
  status: z.nativeEnum(ReservationStatus).optional(),
});

export type UpdateReservationInput = z.infer<typeof updateReservationSchema>;

export const updateReservation = ({
  data,
  reservationId,
}: {
  data: UpdateReservationInput;
  reservationId: number;
}): Promise<Reservation> => {
  return api.post('/v1/reservation/update', {
    reservationId,
    ...data,
    appointmentTime: data.appointmentTime ? Math.floor(data.appointmentTime.getTime() / 1000) : undefined,
  });
};

type UseUpdateReservationOptions = {
  mutationConfig?: MutationConfig<typeof updateReservation>;
};

export const useUpdateReservation = ({ mutationConfig }: UseUpdateReservationOptions) => {
  const queryClient = useQueryClient();

  return useMutation({
    onSuccess: (reservation) => {
      queryClient.invalidateQueries({
        queryKey: ['reservations'],
      });
      queryClient.setQueryData(['reservation', reservation.id], reservation);
    },
    ...mutationConfig,
    mutationFn: updateReservation,
  });
};