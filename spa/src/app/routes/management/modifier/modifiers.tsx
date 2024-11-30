import { OverviewContainer } from '@/components/layouts/overview-container';
import { paths } from '@/config/paths';
import { ModifierList } from '@/features/modifier/components/modifier-list';
import { ProductList } from '@/features/product/components/product-list';

export const ModifiersManagementRoute = () => {
  return (
    <OverviewContainer
      title="Modifiers"
      addButton={{ text: 'Add modifier', href: paths.management.addModifier.getHref() }}
    >
      <ModifierList />
    </OverviewContainer>
  );
};
