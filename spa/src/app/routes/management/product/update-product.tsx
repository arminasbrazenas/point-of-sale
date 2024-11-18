import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateProduct } from '@/features/product/components/update-product';
import { useParams } from 'react-router-dom';

export const UpdateProductManagementRoute = () => {
  const params = useParams();
  const productId = Number(params.productId);

  return (
    <EntityViewContainer title="Update product">
      <UpdateProduct productId={productId} />
    </EntityViewContainer>
  );
};
