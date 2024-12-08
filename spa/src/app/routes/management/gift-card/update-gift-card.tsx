import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateGiftCard } from '@/features/gift-card/components/update-gift-card';
import { useParams } from 'react-router-dom';

export const UpdateGiftCardManagementRoute = () => {
  const params = useParams();
  const giftCardId = Number(params.giftCardId);

  return (
    <EntityViewContainer title="Update gift card">
      <UpdateGiftCard giftCardId={giftCardId} />
    </EntityViewContainer>
  );
};
