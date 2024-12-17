import { EntityViewContainer } from '@/components/layouts/entity-view-container';
import { UpdateReservation } from '@/features/reservation/components/update-reservation';
import { useParams } from 'react-router-dom';

export const UpdateReservationRoute = () => {
  const params = useParams();
  const reservationId = Number(params.reservationId);

  return (
    <EntityViewContainer title="Reservation" size="md">
      <UpdateReservation reservationId={reservationId} />
    </EntityViewContainer>
  );
};