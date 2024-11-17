import { useMemo } from 'react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { paths } from '../config/paths';
import { ManagementRoot } from './routes/management/root';
import { QueryClient, useQueryClient } from '@tanstack/react-query';

const createAppRouter = (queryClient: QueryClient) =>
  createBrowserRouter([
    {
      path: paths.management.root.path,
      element: <ManagementRoot />,
      children: [
        {
          path: paths.management.dashboard.path,
          lazy: async () => {
            const { ManagementDashboardRoute } = await import('./routes/management/dashboard');
            return { Component: ManagementDashboardRoute };
          },
        },
        {
          path: paths.management.products.path,
          lazy: async () => {
            const { ProductManagementRoute } = await import('./routes/management/product-management');
            return { Component: ProductManagementRoute };
          },
        },
        {
          path: paths.management.updateProduct.path,
          lazy: async () => {
            const { UpdateProductRoute } = await import('./routes/management/update-product');
            return { Component: UpdateProductRoute };
          },
        },
        {
          path: paths.management.addProduct.path,
          lazy: async () => {
            const { NewProductManagementRoute } = await import('./routes/management/new-product');
            return { Component: NewProductManagementRoute };
          },
        },
      ],
    },
  ]);

export const AppRouter = () => {
  const queryClient = useQueryClient();

  const router = useMemo(() => createAppRouter(queryClient), [queryClient]);

  return <RouterProvider router={router} />;
};
