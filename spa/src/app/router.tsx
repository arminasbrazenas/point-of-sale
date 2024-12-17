import { useMemo } from 'react';
import { createBrowserRouter, Navigate, RouterProvider, useLocation } from 'react-router-dom';
import { paths } from '../config/paths';
import { ManagementRoot } from './routes/management/root';
import { EmployeeRoot } from './routes/employee/root';
import { HomeRoute } from './routes/home';
import { LoginRoute } from './routes/login';
import { useAppStore } from '@/lib/app-store';
import { BusinessManagementRoot } from './routes/business-management/root';

export const ProtectedRoute: React.FC<{ children: JSX.Element }> = ({ children }) => {
  const isLoggedIn = useAppStore((state) => !!state.applicationUser);
  const location = useLocation();

  if (!isLoggedIn) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return children;
};


export const RedirectToHomeIfLoggedIn: React.FC<{ children: JSX.Element }> = ({ children }) => {
  const isLoggedIn = useAppStore((state) => !!state.applicationUser);
  const location = useLocation();

  if (isLoggedIn) {
    return <Navigate to="/" state={{ from: location }} replace />;
  }

  return children;
};
export const AppRouter = () => {
  const applicationUser = useAppStore((state) => state.applicationUser);

  const router = useMemo(
    () =>
      createBrowserRouter([
        {
          path: paths.home.path,
          element: (
            <ProtectedRoute>
              <HomeRoute />
            </ProtectedRoute>
          ),
        },
        {
          path: paths.login.path,
          element: (
            <RedirectToHomeIfLoggedIn>
              <LoginRoute />
            </RedirectToHomeIfLoggedIn>
          ),
        },
        {
          path: paths.businessManagement.root.path,
          element: (
            <ProtectedRoute>
              <BusinessManagementRoot />
            </ProtectedRoute>
          ),
          children: [
            {
              path: paths.businessManagement.businesses.path,
              lazy: async () => {
                const { BusinessesBusinessManagementRoute } = await import('./routes/business-management/businesses/businesses');
                return { Component: BusinessesBusinessManagementRoute };
              },
            },
            {
              path: paths.businessManagement.updateBusiness.path,
              lazy: async () => {
                const { UpdateBusinessBusinessManagementRoute } = await import('./routes/business-management/businesses/update-business');
                return { Component: UpdateBusinessBusinessManagementRoute };
              },
            },
            {
            path: paths.businessManagement.employees.path,
            lazy: async () => {
              const { EmployeesBusinessManagementRoute } = await import('./routes/business-management/employees/employees');
              return { Component: EmployeesBusinessManagementRoute };
            },
          },
          {
            path: paths.businessManagement.newEmployee.path,
            lazy: async () => {
              const { AddEmployeeBusinessManagementRoute } = await import('./routes/business-management/employees/add-employee');
              return { Component: AddEmployeeBusinessManagementRoute };
            },
          },
          {
            path: paths.businessManagement.updateEmployee.path,
            lazy: async () => {
              const { UpdateEmployeeBusinessManagementRoute } = await import('./routes/business-management/employees/update-employee');
              return { Component: UpdateEmployeeBusinessManagementRoute };
            },
          },
          ],
        },
        {
          path: paths.employee.root.path,
          element: (
            <ProtectedRoute>
              <EmployeeRoot />
            </ProtectedRoute>
          ),
        },
        {
          path: paths.management.root.path,
          element: (
            <ProtectedRoute>
              <ManagementRoot />
            </ProtectedRoute>
          ),
          children:[
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
            {
              path: paths.management.serviceCharges.path,
              lazy: async () => {
                const { ServiceChargesManagementRoute } = await import(
                  './routes/management/service-charge/service-charges'
                );
                return { Component: ServiceChargesManagementRoute };
              },
            },
            {
              path: paths.management.updateServiceCharge.path,
              lazy: async () => {
                const { UpdateServiceChargeManagementRoute } = await import(
                  './routes/management/service-charge/update-service-charge'
                );
                return { Component: UpdateServiceChargeManagementRoute };
              },
            },
            {
              path: paths.management.addServiceCharge.path,
              lazy: async () => {
                const { AddServiceChargeManagementRoute } = await import(
                  './routes/management/service-charge/add-service-charge'
                );
                return { Component: AddServiceChargeManagementRoute };
              },
            },
            {
              path: paths.management.discounts.path,
              lazy: async () => {
                const { DiscountsManagementRoute } = await import('./routes/management/discount/discounts');
                return { Component: DiscountsManagementRoute };
              },
            },
            {
              path: paths.management.updateDiscount.path,
              lazy: async () => {
                const { UpdateDiscountManagementRoute } = await import('./routes/management/discount/update-discount');
                return { Component: UpdateDiscountManagementRoute };
              },
            },
            {
              path: paths.management.addDiscount.path,
              lazy: async () => {
                const { AddDiscountManagementRoute } = await import('./routes/management/discount/add-discount');
                return { Component: AddDiscountManagementRoute };
              },
            },
            {
              path: paths.management.giftCards.path,
              lazy: async () => {
                const { GiftCardsManagementRoute } = await import('./routes/management/gift-card/gift-cards');
                return { Component: GiftCardsManagementRoute };
              },
            },
            {
              path: paths.management.updateGiftCard.path,
              lazy: async () => {
                const { UpdateGiftCardManagementRoute } = await import('./routes/management/gift-card/update-gift-card');
                return { Component: UpdateGiftCardManagementRoute };
              },
            },
            {
              path: paths.management.addGiftCard.path,
              lazy: async () => {
                const { AddGiftCardManagementRoute } = await import('./routes/management/gift-card/add-gift-card');
                return { Component: AddGiftCardManagementRoute };
              },
            },
          ]
        },
      ]),
    [applicationUser] 
  );

  return <RouterProvider router={router} />;
};
