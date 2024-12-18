import { paths } from '@/config/paths';
import { useAppStore } from '@/lib/app-store';
import { AppShell, Button, Center, Group, NavLink, Title } from '@mantine/core';
import React from 'react';
import { Link } from 'react-router-dom';

type BusinessManagementLayoutProps = {
  children: React.ReactNode;
};

export const BusinessManagementLayout = (props: BusinessManagementLayoutProps) => {
  const role = useAppStore((state) => state.applicationUser?.role);
  const businessId = useAppStore((state) => state.applicationUser?.businessId);

  const canViewEmployees =
    role === 'Admin' || (role === 'BusinessOwner' && businessId !== null && businessId !== undefined);

  const businessNavLink =
    role === 'Admin'
      ? { label: 'Businesses', path: paths.businessManagement.businesses.getHref() }
      : { label: 'Business', path: paths.businessManagement.business.getHref() };

  return (
    <AppShell header={{ height: 48 }} navbar={{ width: 300, breakpoint: 'sm' }}>
      <AppShell.Header bg="white">
        <Group h="100%" px="sm" justify="space-between">
          <Title order={1} size="h5">
            Business management portal
          </Title>
          <Button component={Link} to={paths.home.getHref()}>
            Home
          </Button>
        </Group>
      </AppShell.Header>
      <AppShell.Navbar bg="white">
        {canViewEmployees && (
          <NavLink label="Employees" component={Link} to={paths.businessManagement.employees.getHref()} fw={600} />
        )}
        <NavLink label={businessNavLink.label} component={Link} to={businessNavLink.path} fw={600} />{' '}
      </AppShell.Navbar>
      <AppShell.Main>
        <Center px="md" py="lg">
          {props.children}
        </Center>
      </AppShell.Main>
    </AppShell>
  );
};
