import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateDiscount } from '@/features/discount/components/update-discount';
import { useParams } from 'react-router-dom';

export const UpdateDiscountManagementRoute = () => {
  const params = useParams();
  const discountId = Number(params.discountId);

  return (
    <EntityViewContainer title="Update discount">
      <UpdateDiscount discountId={discountId} />
    </EntityViewContainer>
  );
};
