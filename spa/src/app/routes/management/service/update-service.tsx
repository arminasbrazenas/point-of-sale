import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateService } from '@/features/service/components/update-service';
import { useParams } from 'react-router-dom';

export const UpdateServiceManagementRoute = () => {
  const params = useParams();
  const serviceId = Number(params.serviceId);

  return (
    <EntityViewContainer title="Update service">
      <UpdateService serviceId={serviceId} />
    </EntityViewContainer>
  );
};
