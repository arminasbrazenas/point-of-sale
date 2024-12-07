import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Modifier } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getModifierQueryOptions } from './get-modifier';

export const updateModifierInputSchema = z.object({
  name: z.string().optional(),
  stock: z.coerce.number().optional(),
  price: z.coerce.number().optional(),
});

export type UpdateModifierInput = z.infer<typeof updateModifierInputSchema>;

export const updateModifier = ({
  data,
  modifierId,
}: {
  data: UpdateModifierInput;
  modifierId: number;
}): Promise<Modifier> => {
  return api.patch(`/v1/modifiers/${modifierId}`, data);
};

type UseUpdateModifierOptions = {
  mutationConfig?: MutationConfig<typeof updateModifier>;
};

export const useUpdateModifier = ({ mutationConfig }: UseUpdateModifierOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (modifier, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['modifiers'],
      });
      queryClient.setQueryData(getModifierQueryOptions(modifier.id).queryKey, () => modifier);
      onSuccess?.(modifier, ...args);
    },
    ...restConfig,
    mutationFn: updateModifier,
  });
};
