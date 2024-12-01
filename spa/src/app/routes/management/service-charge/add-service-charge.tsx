import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddServiceCharge } from '@/features/service-charge/components/add-service-charge';

export const AddServiceChargeManagementRoute = () => {
  return (
    <EntityViewContainer title="Add service charge">
      <AddServiceCharge />
    </EntityViewContainer>
  );
};
