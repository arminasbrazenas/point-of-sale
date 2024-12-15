import { paths } from '@/config/paths';
import { AppShell, Center, Group, NavLink, Title } from '@mantine/core';
import React from 'react';
import { Link } from 'react-router-dom';

type BusinessManagementLayoutProps = {
  children: React.ReactNode;
};

export const BusinessManagementLayout = (props: BusinessManagementLayoutProps) => {
  return (
    <AppShell header={{ height: 48 }} navbar={{ width: 300, breakpoint: 'sm' }}>
      <AppShell.Header bg="white">
        <Group h="100%" px="sm">
          <Title order={1} size="h5">
            Business management portal
          </Title>
        </Group>
      </AppShell.Header>
      <AppShell.Navbar bg="white">
        <NavLink label="Employees" component={Link} to={paths.businessManagement.employees.getHref()} fw={600} />
      </AppShell.Navbar>
      <AppShell.Main>
        <Center px="md" py="lg">
          {props.children}
        </Center>
      </AppShell.Main>
    </AppShell>
  );
};
