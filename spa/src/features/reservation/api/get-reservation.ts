import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Reservation } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getReservation = ({ reservationId }: { reservationId: number }): Promise<Reservation> => {
  return api.get(`v1/reservation/${reservationId}`);
};

export const getReservationQueryOptions = (reservationId: number) => {
  return queryOptions({
    queryKey: ['reservation', reservationId],
    queryFn: () => getReservation({ reservationId }),
  });
};

type UseGetReservationOptions = {
  reservationId: number;
  queryConfig?: QueryConfig<typeof getReservationQueryOptions>;
};

export const useReservation = ({ reservationId, queryConfig }: UseGetReservationOptions) => {
  return useQuery({
    ...getReservationQueryOptions(reservationId),
    ...queryConfig,
  });
};