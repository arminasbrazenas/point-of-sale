import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddTax } from '@/features/taxes/components/add-tax';

export const AddTaxManagementRoute = () => {
  return (
    <EntityViewContainer title="Add tax">
      <AddTax />
    </EntityViewContainer>
  );
};
