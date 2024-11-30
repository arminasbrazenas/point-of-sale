import { Container, MantineSize, Title } from '@mantine/core';

type EntityViewContainerProps = {
  title: string;
  size?: MantineSize;
  children: React.ReactNode;
};

export const EntityViewContainer = (props: EntityViewContainerProps) => {
  return (
    <Container w="100%" size={props.size ?? 'xs'}>
      <Title order={2} size="h3" mt="xs" mb="md">
        {props.title}
      </Title>
      {props.children}
    </Container>
  );
};
