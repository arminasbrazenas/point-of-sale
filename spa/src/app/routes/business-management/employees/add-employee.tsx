import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddEmployee } from '@/features/employee/components/add-employee';

export const AddEmployeeBusinessManagementRoute = () => {
  return (
    <EntityViewContainer title="Add employee">
      <AddEmployee />
    </EntityViewContainer>
  );
};
