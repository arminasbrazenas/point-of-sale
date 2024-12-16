import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { ServiceList } from '@/features/service/components/service-list';

export const ServicesManagementRoute = () => {
  return (
    <OverviewContainer
      title="Services"
      addButton={{ text: 'Add service', href: paths.management.addService.getHref() }}
    >
      <ServiceList />
    </OverviewContainer>
  );
};
