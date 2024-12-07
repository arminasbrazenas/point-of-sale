import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { ServiceChargeList } from '@/features/service-charge/components/service-charge-list';

export const ServiceChargesManagementRoute = () => {
  return (
    <OverviewContainer
      title="Service charges"
      addButton={{ text: 'Add service charge', href: paths.management.addServiceCharge.getHref() }}
    >
      <ServiceChargeList />
    </OverviewContainer>
  );
};
