import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { Business } from '@/features/business/components/business';

export const BusinessBusinessManagementRoute = () => {
  return (
    <OverviewContainer title="Business" addButton={{ text: 'Add new business', href: paths.businessManagement.newBusiness.getHref() }}>
      <Business />
    </OverviewContainer>
  );
};