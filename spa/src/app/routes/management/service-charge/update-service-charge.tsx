import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateServiceCharge } from '@/features/service-charge/components/update-service-charge';
import { useParams } from 'react-router-dom';

export const UpdateServiceChargeManagementRoute = () => {
  const params = useParams();
  const serviceChargeId = Number(params.serviceChargeId);

  return (
    <EntityViewContainer title="Update service charge">
      <UpdateServiceCharge serviceChargeId={serviceChargeId} />
    </EntityViewContainer>
  );
};
