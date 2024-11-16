import { ManagementLayout } from '@/components/layouts/management-layout';
import { Outlet } from 'react-router-dom';

export const ManagementRoot = () => {
  return (
    <ManagementLayout>
      <Outlet />
    </ManagementLayout>
  );
};
