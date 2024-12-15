import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { ApplicationUser } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getEmployee = ({ employeeId }: { employeeId: number }): Promise<ApplicationUser> => {
  return api.get(`v1/users/${employeeId}`);
};

export const getEmployeeQueryOptions = (employeeId: number) => {
  return queryOptions({
    queryKey: ['employees', employeeId],
    queryFn: () => getEmployee({ employeeId }),
  });
};

type UseGetEmployeeOptions = {
    employeeId: number;
  queryConfig?: QueryConfig<typeof getEmployeeQueryOptions>;
};

export const useEmployee = ({ employeeId, queryConfig }: UseGetEmployeeOptions) => {
  return useQuery({
    ...getEmployeeQueryOptions(employeeId),
    ...queryConfig,
  });
};
