import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddModifier } from '@/features/modifier/components/add-modifier';

export const AddModifierManagementRoute = () => {
  return (
    <EntityViewContainer title="Add modifier">
      <AddModifier />
    </EntityViewContainer>
  );
};
