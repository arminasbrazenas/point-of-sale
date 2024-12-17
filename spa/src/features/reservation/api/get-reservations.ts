import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { Reservation, ReservationStatus } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

type ReservationFilter = {
  status: ReservationStatus;
};

export const getReservations = (
  paginationFilter: PaginationFilter,
  businessId: number,
  filter?: ReservationFilter,
): Promise<PagedResponse<Reservation>> => {
  let queryParams = { ...paginationFilter, businessId };
  if (filter) {
    queryParams = { ...queryParams, ...filter };
  }

  return api.get('/v1/reservations', {
    params: queryParams,
  });
};

export const getReservationsQueryOptions = (
  paginationFilter: PaginationFilter,
  businessId: number,
  filter?: ReservationFilter,
) => {
  return queryOptions({
    queryKey: ['reservations', paginationFilter, businessId],
    queryFn: () => getReservations(paginationFilter, businessId),
  });
};

type UseReservationsOptions = {
  paginationFilter: PaginationFilter;
  filter?: ReservationFilter;
  queryConfig?: QueryConfig<typeof getReservationsQueryOptions>;
};

export const useReservations = ({ paginationFilter, queryConfig, filter }: UseReservationsOptions) => {
  const businessId = useAppStore((state) => {
    const applicationUser = state.applicationUser;

    if (!applicationUser || applicationUser.businessId === null) {
      throw new Error('Application user with business is required to get reservations.');
    }

    return applicationUser.businessId;
  });

  return useQuery({
    ...getReservationsQueryOptions(paginationFilter, businessId, filter),
    ...queryConfig,
  });
};
