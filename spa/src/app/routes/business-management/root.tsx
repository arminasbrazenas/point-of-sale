import { BusinessManagementLayout } from '@/components/layouts/business-management-layout';
import { Outlet } from 'react-router-dom';

export const BusinessManagementRoot = () => {
  return (
    <BusinessManagementLayout>
      <Outlet />
    </BusinessManagementLayout>
  );
};
