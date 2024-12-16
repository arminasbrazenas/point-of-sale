import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddService } from '@/features/service/components/add-service';

export const AddServiceManagementRoute = () => {
  return (
    <EntityViewContainer title="Add service">
      <AddService />
    </EntityViewContainer>
  );
};
