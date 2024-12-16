import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { useParams } from 'react-router-dom';

export const UpdateServiceManagementRoute = () => {
  const params = useParams();
  const serviceId = Number(params.serviceChargeId);

  return (
    <EntityViewContainer title="Update service">
      TODO
      {/* <UpdateService serviceChargeId={serviceId} /> */}
    </EntityViewContainer>
  );
};
