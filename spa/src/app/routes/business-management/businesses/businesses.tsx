import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { BusinessList } from '@/features/business/components/business-list';

export const BusinessesBusinessManagementRoute = () => {
  return (
    <OverviewContainer title="Businesses" addButton={{ text: 'Add new businesses', href: paths.businessManagement.newBusiness.getHref() }}>
      <BusinessList />
    </OverviewContainer>
  );
};