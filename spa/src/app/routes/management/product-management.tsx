import { paths } from '@/config/paths';
import { ProductList } from '@/features/products/components/product-list';
import { Button, Container, Group, Stack, Title } from '@mantine/core';
import { Link } from 'react-router-dom';

export const ProductManagementRoute = () => {
  return (
    <Container w="100%">
      <Group justify="space-between" mb="md">
        <Title order={2} size="h4">
          Products
        </Title>
        <Button component={Link} to={paths.management.addProduct.getHref()}>
          Add product
        </Button>
      </Group>
      <ProductList />
    </Container>
  );
};
