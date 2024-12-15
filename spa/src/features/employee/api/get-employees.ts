import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { QueryConfig } from '@/lib/react-query';
import { ApplicationUser } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getEmployees = (paginationFilter: PaginationFilter, businessId?: number | null): Promise<PagedResponse<ApplicationUser>> => {
  return api.get('/v1/users', {
    params: {
      ...paginationFilter,
      ...(businessId !== null && businessId !== undefined && { businessId }),
    },
  });
};

export const getEmployeesQueryOptions = (
  paginationFilter: PaginationFilter,
  businessId: number | null
) => {
  return queryOptions({
    queryKey: ['employees', paginationFilter, businessId],
    queryFn: () => getEmployees(paginationFilter, businessId),
  });
};

type UseEmployeesOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getEmployeesQueryOptions>;
};

export const useEmployees = ({ paginationFilter, queryConfig }: UseEmployeesOptions) => {
  const businessId = useAppStore((state) => {
    const applicationUser = state.applicationUser;
  
    if (!applicationUser) {
      throw new Error('Application user is required to get employees.');
    }
  
    return applicationUser.businessId ?? null;
  });

  return useQuery({
    ...getEmployeesQueryOptions(paginationFilter, businessId),
    ...queryConfig,
  });
};
