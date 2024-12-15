import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { ApplicationUser } from '@/types/api';
import { PagedResponse, PaginationFilter } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getEmployees = (paginationFilter: PaginationFilter): Promise<PagedResponse<ApplicationUser>> => {
  return api.get('/v1/users', {
    params: paginationFilter,
  });
};

export const getEmployeesQueryOptions = (paginationFilter: PaginationFilter) => {
  return queryOptions({
    queryKey: ['employees', paginationFilter],
    queryFn: () => getEmployees(paginationFilter),
  });
};

type UseEmployeesOptions = {
  paginationFilter: PaginationFilter;
  queryConfig?: QueryConfig<typeof getEmployeesQueryOptions>;
};

export const useEmployees = ({ paginationFilter, queryConfig }: UseEmployeesOptions) => {
  return useQuery({
    ...getEmployeesQueryOptions(paginationFilter),
    ...queryConfig,
  });
};
