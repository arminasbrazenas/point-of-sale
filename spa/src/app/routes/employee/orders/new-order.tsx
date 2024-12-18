import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { OrderProducts } from '@/features/order/components/order-products';
import { useReservations } from '@/features/reservation/api/get-reservations';
import { ReservationStatus } from '@/types/api';

export const NewOrderRoute = () => {
  const activeReservationsQuery = useReservations({
    paginationFilter: { itemsPerPage: 50, page: 1 },
    filter: { status: ReservationStatus.Active },
  });

  if (activeReservationsQuery.isPending) {
    return <div>loading...</div>;
  }

  const activeReservations = activeReservationsQuery.data?.items;
  if (!activeReservations) {
    return null;
  }

  return (
    <EntityViewContainer title="New order" size="md">
      <OrderProducts activeReservations={activeReservations} />
    </EntityViewContainer>
  );
};
