import { CreateProduct } from '@/features/products/components/create-product';
import { Container, Paper, Text, Title } from '@mantine/core';

export const NewProductManagementRoute = () => {
  return (
    <Container w="100%" size="xs">
      <Title order={2} size="h3" mt="xs" mb="md">
        Add product
      </Title>
      <Paper withBorder p="lg">
        <CreateProduct />
      </Paper>
    </Container>
  );
};
