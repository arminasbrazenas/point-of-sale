import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddDiscount } from '@/features/discount/components/add-discount';

export const AddDiscountManagementRoute = () => {
  return (
    <EntityViewContainer title="Add discount">
      <AddDiscount />
    </EntityViewContainer>
  );
};
