import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { ProductList } from '@/features/product/components/product-list';

export const ProductsManagementRoute = () => {
  return (
    <OverviewContainer
      title="Products"
      addButton={{ text: 'Add product', href: paths.management.addProduct.getHref() }}
    >
      <ProductList />
    </OverviewContainer>
  );
};
