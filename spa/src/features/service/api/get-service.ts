import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Service } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getService = ({ serviceId }: { serviceId: number }): Promise<Service> => {
  return api.get(`v1/services/${serviceId}`);
};

export const getServiceQueryOptions = (serviceId: number) => {
  return queryOptions({
    queryKey: ['service', serviceId],
    queryFn: () => getService({ serviceId }),
  });
};

type UseGetServiceOptions = {
  serviceId: number;
  queryConfig?: QueryConfig<typeof getServiceQueryOptions>;
};

export const useService = ({ serviceId, queryConfig }: UseGetServiceOptions) => {
  return useQuery({
    ...getServiceQueryOptions(serviceId),
    ...queryConfig,
  });
};
