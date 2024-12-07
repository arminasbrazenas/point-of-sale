import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Modifier } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';

export const createModifierInputSchema = z.object({
  name: z.string(),
  stock: z.coerce.number(),
  price: z.coerce.number(),
});

export type CreateModifierInput = z.infer<typeof createModifierInputSchema>;

export const createModifier = ({ data }: { data: CreateModifierInput }): Promise<Modifier> => {
  return api.post('/v1/modifiers', data);
};

type UseCreateModifierOptions = {
  mutationConfig?: MutationConfig<typeof createModifier>;
};

export const useCreateModifier = ({ mutationConfig }: UseCreateModifierOptions = {}) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (...args) => {
      queryClient.invalidateQueries({
        queryKey: ['modifiers'],
      });
      onSuccess?.(...args);
    },
    ...restConfig,
    mutationFn: createModifier,
  });
};
