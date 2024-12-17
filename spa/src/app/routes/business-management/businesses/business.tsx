import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { Business } from '@/features/business/components/business';
import { useAppStore } from '@/lib/app-store';

export const BusinessBusinessManagementRoute = () => {
    const role = useAppStore((state) => state.applicationUser?.role);
    const businessId = useAppStore((state) => state.applicationUser?.businessId);
    
    return role === 'BusinessOwner' && (businessId === null || businessId === undefined) ? (
        <OverviewContainer
          title="Business"
          addButton={{ text: 'Add new business', href: paths.businessManagement.newBusiness.getHref() }}
        >
          <Business />
        </OverviewContainer>
      ) : (
        <OverviewContainer title="Business">
          <Business />
        </OverviewContainer>
      );
};