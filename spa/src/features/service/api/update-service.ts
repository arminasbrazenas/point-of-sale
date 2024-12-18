import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { Service } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getServiceQueryOptions } from './get-service';

export const updateServiceInputSchema = z.object({
  name: z.string().optional(),
  price: z.coerce.number().optional(),
  durationInMinutes: z.coerce.number().optional(),
  providedByEmployeesWithId: z.array(z.number()).optional(),
});

export type UpdateServiceInput = z.infer<typeof updateServiceInputSchema>;

export const updateService = ({
  data,
  serviceId,
}: {
  data: UpdateServiceInput;
  serviceId: number;
}): Promise<Service> => {
  return api.patch(`/v1/services/${serviceId}`, data);
};

type UseUpdateServiceOptions = {
  mutationConfig?: MutationConfig<typeof updateService>;
};

export const useUpdateService = ({ mutationConfig }: UseUpdateServiceOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (service, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['services'],
      });
      queryClient.setQueryData(getServiceQueryOptions(service.id).queryKey, () => service);
      onSuccess?.(service, ...args);
    },
    ...restConfig,
    mutationFn: updateService,
  });
};
