import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { OrderProducts } from '@/features/order/components/order-products';

export const NewOrderRoute = () => {
  return (
    <EntityViewContainer title="New order" size="md">
      <OrderProducts />
    </EntityViewContainer>
  );
};
