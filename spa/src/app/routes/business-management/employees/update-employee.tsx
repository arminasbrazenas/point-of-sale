import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateEmployee } from '@/features/employee/components/update-employee';
import { useParams } from 'react-router-dom';

export const UpdateEmployeeBusinessManagementRoute = () => {
  const params = useParams();
  const employeeId = Number(params.employeeId);

  return (
    <EntityViewContainer title="Employee" size="md">
      <UpdateEmployee employeeId={employeeId} />
    </EntityViewContainer>
  );
};