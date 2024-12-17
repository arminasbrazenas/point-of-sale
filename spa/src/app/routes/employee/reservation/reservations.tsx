import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { ReservationList } from '@/features/reservation/components/reservation-list';

export const ReservationEmployeeRoute = () => {
  return (
    <OverviewContainer
      title="Reservations"
      addButton={{ text: 'New reservation', href: paths.employee.newReservation.getHref() }}
    >
      <ReservationList />
    </OverviewContainer>
  );
};
