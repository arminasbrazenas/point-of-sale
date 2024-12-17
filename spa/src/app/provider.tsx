import '@mantine/core/styles.css';
import '@mantine/notifications/styles.css';
import '@mantine/dates/styles.css';
import './index.css';
import '@fontsource/inter/400.css';
import '@fontsource/inter/500.css';
import '@fontsource/inter/600.css';
import '@fontsource/inter/700.css';
import { createTheme, CSSVariablesResolver, MantineProvider, MantineTheme } from '@mantine/core';
import { useState } from 'react';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { queryConfig } from '@/lib/react-query';
import { Notifications } from '@mantine/notifications';
import { AppInitializer } from './app-initializer';
import { DatesProvider } from '@mantine/dates';

const cssVariablesResolver: CSSVariablesResolver = (theme) => ({
  variables: {},
  light: {
    '--mantine-color-body': theme.colors.gray[0],
    '--mantine-color-black': theme.colors.dark[8],
  },
  dark: {},
});

const theme = createTheme({
  components: {
    Paper: {
      styles: (theme: MantineTheme) => ({
        root: {
          backgroundColor: theme.white,
        },
      }),
    },
  },
  fontFamily: 'Inter',
});

type AppProviderProps = {
  children: React.ReactNode;
};

export const AppProvider = (props: AppProviderProps) => {
  const [queryClient] = useState(() => new QueryClient({ defaultOptions: queryConfig }));

  return (
    <MantineProvider theme={theme} cssVariablesResolver={cssVariablesResolver}>
      <DatesProvider settings={{}}>
        <Notifications />
        <QueryClientProvider client={queryClient}>
          <AppInitializer>{props.children}</AppInitializer>
        </QueryClientProvider>
      </DatesProvider>
    </MantineProvider>
  );
};
