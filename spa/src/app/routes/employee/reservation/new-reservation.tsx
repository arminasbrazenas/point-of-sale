import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { AddReservation } from '@/features/reservation/components/add-reservation';

export const NewReservationEmployeeRoute = () => {
  return (
    <EntityViewContainer title="New reservation" size="md">
      <AddReservation />
    </EntityViewContainer>
  );
};
