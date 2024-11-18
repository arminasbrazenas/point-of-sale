import { Button, Container, Group, Title } from '@mantine/core';
import React from 'react';
import { Link } from 'react-router-dom';

type ManagementContainerProps = {
  title: string;
  addButton: {
    text: string;
    href: string;
  };
  children: React.ReactNode;
};

export const ManagementContainer = (props: ManagementContainerProps) => {
  return (
    <Container w="100%">
      <Group justify="space-between" mb="md">
        <Title order={2} size="h4">
          {props.title}
        </Title>
        <Button component={Link} to={props.addButton.href}>
          {props.addButton.text}
        </Button>
      </Group>
      {props.children}
    </Container>
  );
};
