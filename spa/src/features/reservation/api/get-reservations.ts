import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Reservation } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getReservations = (paginationFilter: PaginationFilter): Promise<PagedResponse<Reservation>> => {
  return api.get('/v1/reservation/getList', {
    params: paginationFilter,
  });
};

export const getReservationsQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['reservations', paginationFilter],
    queryFn: () => getReservations(paginationFilter),
  });
};

type UseReservationsOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getReservationsQueryOptions>;
};

export const useReservations = ({ paginationFilter, queryConfig }: UseReservationsOptions) => {
  return useQuery({
    ...getReservationsQueryOptions(paginationFilter),
    ...queryConfig,
  });
};