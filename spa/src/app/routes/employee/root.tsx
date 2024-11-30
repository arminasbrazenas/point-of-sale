import { EmployeeLayout } from '@/components/layouts/employee-layout';
import { Outlet } from 'react-router-dom';

export const EmployeeRoot = () => {
  return (
    <EmployeeLayout>
      <Outlet />
    </EmployeeLayout>
  );
};
