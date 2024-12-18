import { paths } from '@/config/paths';
import { useAppStore } from '@/lib/app-store';
import { Roles } from '@/types/api';
import { Button, Stack } from '@mantine/core';
import { useNavigate } from 'react-router-dom';

export const HomeRoute = () => {
  const navigate = useNavigate();
  const role = useAppStore((state) => state.applicationUser?.role || null);
  const businessId = useAppStore((state) => state.applicationUser?.businessId || null);

  const hasAccessToBusinessManagement =
    role === Roles.BusinessOwner || role === Roles.Admin;

  const hasAccessToOrderManagement =
    (role === Roles.BusinessOwner && businessId != null)|| role === Roles.Employee;

  return (
    <Stack maw={400} gap="xs" m="md">
      {hasAccessToOrderManagement && (
        <>
          <Button onClick={() => navigate(paths.employee.root.getHref())}>
            Employee portal
          </Button>
          <Button onClick={() => navigate(paths.management.root.getHref())}>
            Management portal
          </Button>
        </>
      )}

      {hasAccessToBusinessManagement && (
        <Button onClick={() => navigate(paths.businessManagement.root.getHref())}>
          Business management portal
        </Button>
      )}
    </Stack>
  );
};