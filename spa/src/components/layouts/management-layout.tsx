import { paths } from '@/config/paths';
import { AppShell, Center, Group, NavLink, Title } from '@mantine/core';
import React from 'react';
import { Link } from 'react-router-dom';

type ManagementLayoutProps = {
  children: React.ReactNode;
};

export const ManagementLayout = (props: ManagementLayoutProps) => {
  return (
    <AppShell header={{ height: 48 }} navbar={{ width: 300, breakpoint: 'sm' }}>
      <AppShell.Header bg="white">
        <Group h="100%" px="sm">
          <Title order={1} size="h5">
            Business management
          </Title>
        </Group>
      </AppShell.Header>
      <AppShell.Navbar bg="white">
        <NavLink label="Dashboard" component={Link} to={paths.management.dashboard.getHref()} fw={600} />
        <NavLink label="Products" component={Link} to={paths.management.products.getHref()} fw={600} />
        <NavLink label="Taxes" component={Link} to={paths.management.taxes.getHref()} fw={600} />
      </AppShell.Navbar>
      <AppShell.Main>
        <Center px="md" py="lg">
          {props.children}
        </Center>
      </AppShell.Main>
    </AppShell>
  );
};
