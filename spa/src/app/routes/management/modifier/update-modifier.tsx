import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateModifier } from '@/features/modifier/components/update-modifier';
import { useParams } from 'react-router-dom';

export const UpdateModifierManagementRoute = () => {
  const params = useParams();
  const modifierId = Number(params.modifierId);

  return (
    <EntityViewContainer title="Update modifier">
      <UpdateModifier modifierId={modifierId} />
    </EntityViewContainer>
  );
};
