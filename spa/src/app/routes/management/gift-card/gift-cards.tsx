import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { GiftCardList } from '@/features/gift-card/components/gift-card-list';

export const GiftCardsManagementRoute = () => {
  return (
    <OverviewContainer
      title="Gift cards"
      addButton={{ text: 'Add gift card', href: paths.management.addGiftCard.getHref() }}
    >
      <GiftCardList />
    </OverviewContainer>
  );
};
