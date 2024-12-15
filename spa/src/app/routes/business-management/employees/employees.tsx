import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { EmployeeList } from '@/features/employee/components/employee-list';

export const EmployeesBusinessManagementRoute = () => {
  return (
    <OverviewContainer title="Employees" addButton={{ text: 'Add new employee', href: paths.businessManagement.newEmployee.getHref() }}>
      <EmployeeList />
    </OverviewContainer>
  );
};