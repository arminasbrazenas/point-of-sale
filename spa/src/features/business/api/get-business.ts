import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Business } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getBusiness = ({ businessId }: { businessId: number }): Promise<Business> => {
  return api.get(`v1/businesses/${businessId}`);
};

export const getBusinessQueryOptions = (businessId: number) => {
  return queryOptions({
    queryKey: ['businesses', businessId],
    queryFn: () => getBusiness({ businessId }),
  });
};

type UseGetBusinessOptions = {
    businessId: number;
  queryConfig?: QueryConfig<typeof getBusinessQueryOptions>;
};

export const useBusiness = ({ businessId, queryConfig }: UseGetBusinessOptions) => {
  return useQuery({
    ...getBusinessQueryOptions(businessId),
    ...queryConfig,
  });
};