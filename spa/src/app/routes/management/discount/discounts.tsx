import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { DiscountList } from '@/features/discount/components/discount-list';

export const DiscountsManagementRoute = () => {
  return (
    <OverviewContainer
      title="Discounts"
      addButton={{ text: 'Add discount', href: paths.management.addDiscount.getHref() }}
    >
      <DiscountList />
    </OverviewContainer>
  );
};
