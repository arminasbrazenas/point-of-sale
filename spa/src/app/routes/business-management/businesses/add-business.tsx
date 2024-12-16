import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddBusiness } from '@/features/business/components/add-business';

export const AddBusinessBusinessManagementRoute = () => {
  return (
    <EntityViewContainer title="Add business">
      <AddBusiness />
    </EntityViewContainer>
  );
};
