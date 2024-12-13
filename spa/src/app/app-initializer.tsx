import { api } from '@/lib/api-client';
import { useAppStore } from '@/lib/app-store';
import { ApplicationUser} from '@/types/api';
import { useQuery, useQueryClient } from '@tanstack/react-query';
import { useEffect } from 'react';
import { z } from 'zod';

export const createTaxInputSchema = z.object({
  name: z.string(),
  rate: z.coerce.number(),
});

export type CreateTaxInput = z.infer<typeof createTaxInputSchema>;

export const getCurrentUserInfo = (): Promise<ApplicationUser> => {
  return api.get('/v1/users/currentUser');
};

export const useGetCurrentUserInfo = () => {
    return useQuery({
      queryKey: ['currentUser'],
      queryFn: getCurrentUserInfo,
    });
  };

export const AppInitializer: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const { data, error,} = useGetCurrentUserInfo();
    const setApplicationUser = useAppStore((state) => state.setApplicationUser);
    const queryClient = useQueryClient();
    useEffect(() => {
        if (data) {
          setApplicationUser({
            id: data.id,
            businessId: data.businessId,
            role: data.role,
          });
        } else if (error) {
          setApplicationUser(null);
        }
        queryClient.invalidateQueries({
            queryKey: ['applicationUsers'],
        });
      }, [data, error, setApplicationUser]);
    return <>{children}</>;
  };