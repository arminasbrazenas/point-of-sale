import { UpdateProduct } from '@/features/products/components/update-product';
import { Container, Paper, Title } from '@mantine/core';
import { useParams } from 'react-router-dom';

export const UpdateProductRoute = () => {
  const params = useParams();
  const productId = Number(params.productId);

  return (
    <Container w="100%" size="xs">
      <Title order={2} size="h3" mt="xs" mb="md">
        Update product
      </Title>
      <Paper withBorder p="lg">
        <UpdateProduct productId={productId} />
      </Paper>
    </Container>
  );
};
