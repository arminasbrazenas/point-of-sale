import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { useMutation, useQueryClient } from '@tanstack/react-query';

export const deleteModifier = ({ modifierId }: { modifierId: number }): Promise<void> => {
  return api.delete(`/v1/modifiers/${modifierId}`);
};

type UseDeleteModifierOptions = {
  mutationConfig?: MutationConfig<typeof deleteModifier>;
};

export const useDeleteModifier = ({ mutationConfig }: UseDeleteModifierOptions) => {
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
    mutationFn: deleteModifier,
  });
};
