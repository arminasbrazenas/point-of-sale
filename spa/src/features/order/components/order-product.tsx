import { OrderItemModifier, Product } from '@/types/api';
import { Card, Text, Divider, Modal, Group } from '@mantine/core';
import { useDisclosure } from '@mantine/hooks';
import { CreateOrUpdateOrderItemInput } from '../api/create-order';
import { useMemo } from 'react';
import { OrderItemForm } from './order-item-form';

export type EnhancedCreateOrderItemInput = CreateOrUpdateOrderItemInput & {
  cartItemId: number;
  orderedQuantity?: number;
  product: Product;
  modifiers: OrderItemModifier[];
  price: number;
};

type OrderProductProps = {
  product: Product;
  addToOrder: (orderItem: EnhancedCreateOrderItemInput) => void;
  orderItems: EnhancedCreateOrderItemInput[];
};

export const OrderProduct = (props: OrderProductProps) => {
  const [isModalOpen, { open: openModal, close: closeModal }] = useDisclosure(false);

  const handleAddToOrder = (orderItem: EnhancedCreateOrderItemInput) => {
    props.addToOrder(orderItem);
    closeModal();
  };

  const stock = useMemo(() => {
    const productQuantityInCart = props.orderItems
      .filter((x) => x.productId == props.product.id)
      .map((x) => x.quantity)
      .reduce((acc, curr) => acc + curr, 0);
    const orderedQuantity = props.orderItems
      .filter((x) => x.productId == props.product.id)
      .map((x) => x.orderedQuantity ?? 0)
      .reduce((acc, curr) => acc + curr, 0);
    return props.product.stock - productQuantityInCart + orderedQuantity;
  }, [props.orderItems]);

  return (
    <>
      <Modal opened={isModalOpen} onClose={closeModal} title="Order product">
        <OrderItemForm
          product={props.product}
          onCancel={closeModal}
          onConfirm={handleAddToOrder}
          confirmText="Add to order"
          cartItemId={props.orderItems.length + 1}
        />
      </Modal>

      <Card withBorder onClick={openModal}>
        <Text fw={600}>{props.product.name}</Text>

        <Group gap="xs">
          {props.product.discounts.length > 0 && (
            <Text opacity={0.5} td="line-through">
              {props.product.priceDiscountExcluded}€
            </Text>
          )}
          <Text c="blue">{props.product.price}€</Text>
        </Group>
        <Divider my="sm" />
        <Text opacity={0.5}>Stock: {stock}</Text>
      </Card>
    </>
  );
};
