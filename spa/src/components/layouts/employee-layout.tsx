import { paths } from '@/config/paths';
import { AppShell, Button, Center, Group, NavLink, Title } from '@mantine/core';
import React from 'react';
import { Link } from 'react-router-dom';

type EmployeeLayoutProps = {
  children: React.ReactNode;
};

export const EmployeeLayout = (props: EmployeeLayoutProps) => {
  return (
    <AppShell header={{ height: 48 }} navbar={{ width: 300, breakpoint: 'sm' }}>
      <AppShell.Header bg="white">
        <Group h="100%" px="sm" justify="space-between">
          <Title order={1} size="h5">
            Employee portal
          </Title>
          <Button component={Link} to={paths.home.getHref()}>
            Home
          </Button>
        </Group>
      </AppShell.Header>
      <AppShell.Navbar bg="white">
        <NavLink label="Orders" component={Link} to={paths.employee.orders.getHref()} fw={600} />
        <NavLink label="Reservations" component={Link} to={paths.employee.reservations.getHref()} fw={600} />
      </AppShell.Navbar>
      <AppShell.Main>
        <Center px="md" py="lg">
          {props.children}
        </Center>
      </AppShell.Main>
    </AppShell>
  );
};
