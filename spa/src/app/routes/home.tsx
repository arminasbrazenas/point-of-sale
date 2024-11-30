import { paths } from '@/config/paths';
import { Button, Stack } from '@mantine/core';
import { useNavigate } from 'react-router-dom';

export const HomeRoute = () => {
  const navigate = useNavigate();

  return (
    <Stack maw={200} gap="xs" m="md">
      <Button onClick={() => navigate(paths.employee.root.getHref())}>Employee portal</Button>
      <Button onClick={() => navigate(paths.management.root.getHref())}>Management portal</Button>
    </Stack>
  );
};
