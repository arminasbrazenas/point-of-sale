import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateTax } from '@/features/taxes/components/update-tax';
import { useParams } from 'react-router-dom';

export const UpdateTaxManagementRoute = () => {
  const params = useParams();
  const taxId = Number(params.taxId);

  return (
    <EntityViewContainer title="Update tax">
      <UpdateTax taxId={taxId} />
    </EntityViewContainer>
  );
};
