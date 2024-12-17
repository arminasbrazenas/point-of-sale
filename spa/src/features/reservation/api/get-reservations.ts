import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { Reservation } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getReservations = (
  paginationFilter: PaginationFilter,
  businessId: number,
): Promise<PagedResponse<Reservation>> => {
  return api.get('/v1/reservations', {
    params: { ...paginationFilter, businessId },
  });
};

export const getReservationsQueryOptions = (paginationFilter: PaginationFilter, businessId: number) => {
  return queryOptions({
    queryKey: ['reservations', paginationFilter, businessId],
    queryFn: () => getReservations(paginationFilter, businessId),
  });
};

type UseReservationsOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getReservationsQueryOptions>;
};

export const useReservations = ({ paginationFilter, queryConfig }: UseReservationsOptions) => {
  const businessId = useAppStore((state) => {
    const applicationUser = state.applicationUser;

    if (!applicationUser || applicationUser.businessId === null) {
      throw new Error('Application user with business is required to get reservations.');
    }

    return applicationUser.businessId;
  });

  return useQuery({
    ...getReservationsQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};
