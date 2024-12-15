import { paths } from '@/config/paths';
import { Button, Stack } from '@mantine/core';
import { useNavigate } from 'react-router-dom';

export const HomeRoute = () => {
  const navigate = useNavigate();

  return (
    <Stack maw={400} gap="xs" m="md">
      <Button onClick={() => navigate(paths.employee.root.getHref())}>Employee portal</Button>
      <Button onClick={() => navigate(paths.management.root.getHref())}>Management portal</Button>
      <Button onClick={() => navigate(paths.businessManagement.root.getHref())}>Business management portal</Button>
    </Stack>
  );
};
