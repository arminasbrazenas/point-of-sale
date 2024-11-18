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
            const { ManagementDashboardRoute } = await import('./routes/management/dashboard/dashboard');
            return { Component: ManagementDashboardRoute };
          },
        },
        {
          path: paths.management.products.path,
          lazy: async () => {
            const { ProductsManagementRoute } = await import('./routes/management/product/products');
            return { Component: ProductsManagementRoute };
          },
        },
        {
          path: paths.management.updateProduct.path,
          lazy: async () => {
            const { UpdateProductManagementRoute } = await import('./routes/management/product/update-product');
            return { Component: UpdateProductManagementRoute };
          },
        },
        {
          path: paths.management.addProduct.path,
          lazy: async () => {
            const { AddProductManagementRoute } = await import('./routes/management/product/add-product');
            return { Component: AddProductManagementRoute };
          },
        },
        {
          path: paths.management.taxes.path,
          lazy: async () => {
            const { TaxesManagementRoute } = await import('./routes/management/tax/taxes');
            return { Component: TaxesManagementRoute };
          },
        },
        {
          path: paths.management.updateTax.path,
          lazy: async () => {
            const { UpdateTaxManagementRoute } = await import('./routes/management/tax/update-tax');
            return { Component: UpdateTaxManagementRoute };
          },
        },
        {
          path: paths.management.addTax.path,
          lazy: async () => {
            const { AddTaxManagementRoute } = await import('./routes/management/tax/add-tax');
            return { Component: AddTaxManagementRoute };
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
