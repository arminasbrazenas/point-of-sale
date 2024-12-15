import { api } from '@/lib/api-client';
import { MutationConfig } from '@/lib/react-query';
import { ApplicationUser } from '@/types/api';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { z } from 'zod';
import { getEmployeeQueryOptions } from './get-employee';

export const updateEmployeeInputSchema = z.object({
    firstName: z.string().optional(),
    lastName: z.string().optional(),
    email: z.string().optional(),
    phoneNumber: z.string().optional(),
    password: z.string().optional(),
});

export type UpdateEmployeeInput = z.infer<typeof updateEmployeeInputSchema>;

export const updateEmployee = ({
  data,
  employeeId,
}: {
  data: UpdateEmployeeInput;
  employeeId: number;
}): Promise<ApplicationUser> => {
  return api.patch(`/v1/users/${employeeId}`, data);
};

type UseUpdateEmployeeOptions = {
  mutationConfig?: MutationConfig<typeof updateEmployee>;
};

export const useUpdateEmployee = ({ mutationConfig }: UseUpdateEmployeeOptions) => {
  const queryClient = useQueryClient();

  const { onSuccess, ...restConfig } = mutationConfig || {};

  return useMutation({
    onSuccess: (employee, ...args) => {
      queryClient.invalidateQueries({
        queryKey: ['employees'],
      });
      queryClient.setQueryData(getEmployeeQueryOptions(employee.id).queryKey, () => employee);
      onSuccess?.(employee, ...args);
    },
    ...restConfig,
    mutationFn: updateEmployee,
  });
};
