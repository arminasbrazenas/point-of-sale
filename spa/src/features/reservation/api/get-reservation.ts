import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Reservation, Service } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getReservation = ({ reservationId }: { reservationId: number }): Promise<Reservation> => {
  return api.get(`v1/reservations/${reservationId}`);
};

export const getReservationQueryOptions = (reservationId: number) => {
  return queryOptions({
    queryKey: ['reservations', reservationId],
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
