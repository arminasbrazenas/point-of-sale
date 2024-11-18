import { ManagementContainer } from '@/components/layouts/management-container';
import { paths } from '@/config/paths';
import { TaxList } from '@/features/taxes/components/tax-list';

export const TaxesManagementRoute = () => {
  return (
    <ManagementContainer title="Taxes" addButton={{ text: 'Add tax', href: paths.management.addTax.getHref() }}>
      <TaxList />
    </ManagementContainer>
  );
};
