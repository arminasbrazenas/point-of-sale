import { paths } from '@/config/paths';
import { AppShell, Button, Center, Group, NavLink, Title } from '@mantine/core';
import React from 'react';
import { Link } from 'react-router-dom';

type ManagementLayoutProps = {
  children: React.ReactNode;
};

export const ManagementLayout = (props: ManagementLayoutProps) => {
  return (
    <AppShell header={{ height: 48 }} navbar={{ width: 300, breakpoint: 'sm' }}>
      <AppShell.Header bg="white">
        <Group h="100%" px="sm" justify="space-between">
          <Title order={1} size="h5">
            Management portal
          </Title>
          <Button component={Link} to={paths.home.getHref()}>
            Home
          </Button>
        </Group>
      </AppShell.Header>
      <AppShell.Navbar bg="white">
        <NavLink label="Products" component={Link} to={paths.management.products.getHref()} fw={600} />
        <NavLink label="Modifiers" component={Link} to={paths.management.modifiers.getHref()} fw={600} />
        <NavLink label="Taxes" component={Link} to={paths.management.taxes.getHref()} fw={600} />
        <NavLink label="Service charges" component={Link} to={paths.management.serviceCharges.getHref()} fw={600} />
        <NavLink label="Discounts" component={Link} to={paths.management.discounts.getHref()} fw={600} />
        <NavLink label="Gift cards" component={Link} to={paths.management.giftCards.getHref()} fw={600} />
        <NavLink label="Services" component={Link} to={paths.management.services.getHref()} fw={600} />
      </AppShell.Navbar>
      <AppShell.Main>
        <Center px="md" py="lg">
          {props.children}
        </Center>
      </AppShell.Main>
    </AppShell>
  );
};
