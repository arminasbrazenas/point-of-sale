import { api } from '@/lib/api-client';
import { QueryConfig } from '@/lib/react-query';
import { Modifier } from '@/types/api';
import { queryOptions, useQuery } from '@tanstack/react-query';

export const getModifier = ({ modifierId }: { modifierId: number }): Promise<Modifier> => {
  return api.get(`v1/modifiers/${modifierId}`);
};

export const getModifierQueryOptions = (modifierId: number) => {
  return queryOptions({
    queryKey: ['modifier', modifierId],
    queryFn: () => getModifier({ modifierId }),
  });
};

type UseGetModifierOptions = {
  modifierId: number;
  queryConfig?: QueryConfig<typeof getModifierQueryOptions>;
};

export const useModifier = ({ modifierId, queryConfig }: UseGetModifierOptions) => {
  return useQuery({
    ...getModifierQueryOptions(modifierId),
    ...queryConfig,
  });
};
