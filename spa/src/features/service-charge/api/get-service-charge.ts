import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { ServiceCharge } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getServiceCharge = ({ serviceChargeId }: { serviceChargeId: number }): Promise<ServiceCharge> => {
  return api.get(`v1/service-charges/${serviceChargeId}`);
};

export const getServiceChargeQueryOptions = (serviceChargeId: number) => {
  return queryOptions({
    queryKey: ['service-charge', serviceChargeId],
    queryFn: () => getServiceCharge({ serviceChargeId }),
  });
};

type UseGetServiceChargeOptions = {
  serviceChargeId: number;
  queryConfig?: QueryConfig<typeof getServiceChargeQueryOptions>;
};

export const useServiceCharge = ({ serviceChargeId, queryConfig }: UseGetServiceChargeOptions) => {
  return useQuery({
    ...getServiceChargeQueryOptions(serviceChargeId),
    ...queryConfig,
  });
};
