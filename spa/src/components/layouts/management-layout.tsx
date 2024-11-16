import { paths } from '@/config/paths';
import { AppShell, Center, NavLink } from '@mantine/core';
import React from 'react';
import { Link } from 'react-router-dom';

type ManagementLayoutProps = {
  children: React.ReactNode;
};

export const ManagementLayout = (props: ManagementLayoutProps) => {
  return (
    <AppShell navbar={{ width: 300, breakpoint: 'sm' }}>
      <AppShell.Navbar bg="white">
        <NavLink label="Dashboard" component={Link} to={paths.management.dashboard.getHref()} />
        <NavLink label="Product management" component={Link} to={paths.management.products.getHref()} />
      </AppShell.Navbar>
      <AppShell.Main>
        <Center p="md">{props.children}</Center>
      </AppShell.Main>
    </AppShell>
  );
};
