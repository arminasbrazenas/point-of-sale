import { Button, Container, Group, Title } from '@mantine/core';
import React from 'react';
import { Link } from 'react-router-dom';

type OverviewContainerProps = {
  title: string;
  addButton?: {
    text: string;
    href: string;
  };
  children: React.ReactNode;
};

export const OverviewContainer = (props: OverviewContainerProps) => {
  return (
    <Container w="100%">
      <Group justify="space-between" mb="md">
        <Title order={2} size="h4">
          {props.title}
        </Title>
        {props.addButton && (
          <Button component={Link} to={props.addButton.href}>
            {props.addButton.text}
          </Button>
        )}
      </Group>
      {props.children}
    </Container>
  );
};