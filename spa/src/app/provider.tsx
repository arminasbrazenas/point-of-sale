import '@mantine/core/styles.css';
import '@mantine/notifications/styles.css';
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
  primaryColor: 'teal',
  primaryShade: 8,
  fontFamily: 'Inter',
});

type AppProviderProps = {
  children: React.ReactNode;
};

export const AppProvider = (props: AppProviderProps) => {
  const [queryClient] = useState(() => new QueryClient({ defaultOptions: queryConfig }));

  return (
    <MantineProvider theme={theme} cssVariablesResolver={cssVariablesResolver}>
      <Notifications />
      <QueryClientProvider client={queryClient}>{props.children}</QueryClientProvider>
    </MantineProvider>
  );
};
