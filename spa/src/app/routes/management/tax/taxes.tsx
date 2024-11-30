import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { TaxList } from '@/features/taxes/components/tax-list';

export const TaxesManagementRoute = () => {
  return (
    <OverviewContainer title="Taxes" addButton={{ text: 'Add tax', href: paths.management.addTax.getHref() }}>
      <TaxList />
    </OverviewContainer>
  );
};
