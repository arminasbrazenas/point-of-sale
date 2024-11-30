import { useMemo } from 'react';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { paths } from '../config/paths';
import { ManagementRoot } from './routes/management/root';
import { EmployeeRoot } from './routes/employee/root';

const createAppRouter = () =>
  createBrowserRouter([
    {
      path: paths.employee.root.path,
      element: <EmployeeRoot />,
      children: [
        {
          path: paths.employee.orders.path,
          lazy: async () => {
            const { OrdersEmployeeRoute } = await import('./routes/employee/orders/orders');
            return { Component: OrdersEmployeeRoute };
          },
        },
        {
          path: paths.employee.newOrder.path,
          lazy: async () => {
            const { NewOrderRoute } = await import('./routes/employee/orders/new-order');
            return { Component: NewOrderRoute };
          },
        },
        {
          path: paths.employee.updateOrder.path,
          lazy: async () => {
            const { UpdateOrderEmployeeRoute } = await import('./routes/employee/orders/update-order');
            return { Component: UpdateOrderEmployeeRoute };
          },
        },
      ],
    },
    {
      path: paths.management.root.path,
      element: <ManagementRoot />,
      children: [
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
        {
          path: paths.management.modifiers.path,
          lazy: async () => {
            const { ModifiersManagementRoute } = await import('./routes/management/modifier/modifiers');
            return { Component: ModifiersManagementRoute };
          },
        },
        {
          path: paths.management.updateModifier.path,
          lazy: async () => {
            const { UpdateModifierManagementRoute } = await import('./routes/management/modifier/update-modifier');
            return { Component: UpdateModifierManagementRoute };
          },
        },
        {
          path: paths.management.addModifier.path,
          lazy: async () => {
            const { AddModifierManagementRoute } = await import('./routes/management/modifier/add-modifier');
            return { Component: AddModifierManagementRoute };
          },
        },
      ],
    },
  ]);

export const AppRouter = () => {
  const router = useMemo(() => createAppRouter(), []);

  return <RouterProvider router={router} />;
};
