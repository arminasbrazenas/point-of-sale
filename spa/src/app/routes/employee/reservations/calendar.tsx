import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { ReservationCalendar } from '@/features/reservation/components/reservation-calendar';

export const CalendarRoute = () => {
  return (
    <EntityViewContainer title="Calendar View" size="xl">
      <ReservationCalendar />
    </EntityViewContainer>
  );
};