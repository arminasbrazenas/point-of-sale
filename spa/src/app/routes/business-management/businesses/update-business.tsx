import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateBusiness } from '@/features/business/components/update-business';
import { useParams } from 'react-router-dom';

export const UpdateBusinessBusinessManagementRoute = () => {
  const params = useParams();
  const businessId = Number(params.businessId);

  return (
    <EntityViewContainer title="Business" size="md">
      <UpdateBusiness businessId={businessId} />
    </EntityViewContainer>
  );
};