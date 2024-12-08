import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddGiftCard } from '@/features/gift-card/components/add-gift-card';

export const AddGiftCardManagementRoute = () => {
  return (
    <EntityViewContainer title="Add gift card">
      <AddGiftCard />
    </EntityViewContainer>
  );
};
