import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { CreateReservation } from '@/features/reservation/components/create-reservation';

export const NewReservationRoute = () => {
  return (
    <EntityViewContainer title="New Reservation" size="md">
      <CreateReservation />
    </EntityViewContainer>
  );
};