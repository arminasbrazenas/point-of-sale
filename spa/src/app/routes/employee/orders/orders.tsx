import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { OrderList } from '@/features/order/components/order-list';

export const OrdersEmployeeRoute = () => {
  return (
    <OverviewContainer title="Orders" addButton={{ text: 'New order', href: paths.employee.newOrder.getHref() }}>
      <OrderList />
    </OverviewContainer>
  );
};
