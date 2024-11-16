import { paths } from '@/config/paths';
import { Button, Container, Group, Stack, Title } from '@mantine/core';
import { Link } from 'react-router-dom';

export const ProductManagementRoute = () => {
  return (
    <Container w="100%">
      <Group justify="space-between">
        <Title order={2} size="h4">
          Products
        </Title>
        <Button component={Link} to={paths.management.products.new.getHref()}>
          Add product
        </Button>
      </Group>
    </Container>
  );
};
