import { ManagementContainer } from '@/components/layouts/management-container';
import { paths } from '@/config/paths';
import { ProductList } from '@/features/product/components/product-list';

export const ProductsManagementRoute = () => {
  return (
    <ManagementContainer
      title="Products"
      addButton={{ text: 'Add product', href: paths.management.addProduct.getHref() }}
    >
      <ProductList />
    </ManagementContainer>
  );
};
