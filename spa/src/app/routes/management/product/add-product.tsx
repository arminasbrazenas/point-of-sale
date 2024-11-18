import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddProduct } from '@/features/product/components/add-product';

export const AddProductManagementRoute = () => {
  return (
    <EntityViewContainer title="Add product">
      <AddProduct />
    </EntityViewContainer>
  );
};
