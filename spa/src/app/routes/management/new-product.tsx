import { CreateProduct } from '@/features/products/components/create-product';
import { Container, Paper, Text } from '@mantine/core';

export const NewProductManagementRoute = () => {
  return (
    <Container w="100%" size="xs">
      <Paper withBorder p="lg">
        <Text fw={600} mb="sm">
          Add product
        </Text>
        <CreateProduct />
      </Paper>
    </Container>
  );
};
