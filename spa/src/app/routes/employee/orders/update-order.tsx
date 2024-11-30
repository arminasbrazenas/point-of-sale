import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateOrder } from '@/features/order/components/update-order';
import { useParams } from 'react-router-dom';

export const UpdateOrderEmployeeRoute = () => {
  const params = useParams();
  const orderId = Number(params.orderId);

  return (
    <EntityViewContainer title="Order" size="md">
      <UpdateOrder orderId={orderId} />
    </EntityViewContainer>
  );
};
