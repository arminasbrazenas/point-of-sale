import { Container, Title } from '@mantine/core';

type EntityViewContainerProps = {
  title: string;
  children: React.ReactNode;
};

export const EntityViewContainer = (props: EntityViewContainerProps) => {
  return (
    <Container w="100%" size="xs">
      <Title order={2} size="h3" mt="xs" mb="md">
        {props.title}
      </Title>
      {props.children}
    </Container>
  );
};
