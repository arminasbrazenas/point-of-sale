import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Tax } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getTax = ({ taxId }: { taxId: number }): Promise<Tax> => {
  return api.get(`v1/taxes/${taxId}`);
};

export const getTaxQueryOptions = (taxId: number) => {
  return queryOptions({
    queryKey: ['tax', taxId],
    queryFn: () => getTax({ taxId }),
  });
};

type UseGetTaxOptions = {
  taxId: number;
  queryConfig?: QueryConfig<typeof getTaxQueryOptions>;
};

export const useTax = ({ taxId, queryConfig }: UseGetTaxOptions) => {
  return useQuery({
    ...getTaxQueryOptions(taxId),
    ...queryConfig,
  });
};
