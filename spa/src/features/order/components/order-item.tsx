import { Button, Group, Modal, Paper, Text } from '@mantine/core';
import { EnhancedCreateOrderItemInput } from './order-product';
import { OrderItemForm } from './order-item-form';
import { useDisclosure } from '@mantine/hooks';
import { convertToMoney } from '@/utilities';

type OrderItemProps = {
  orderItem: EnhancedCreateOrderItemInput;
  remove: (orderItem: EnhancedCreateOrderItemInput) => void;
  update: (orderItem: EnhancedCreateOrderItemInput) => void;
};

export const OrderItem = (props: OrderItemProps) => {
  const [isModalOpen, { open: openModal, close: closeModal }] = useDisclosure(false);

  const update = (orderItem: EnhancedCreateOrderItemInput) => {
    props.update(orderItem);
    closeModal();
  };

  const remove = () => {
    props.remove(props.orderItem);
    closeModal();
  };

  return (
    <>
      <Modal title="Edit order item" opened={isModalOpen} onClose={closeModal}>
        <OrderItemForm
          product={props.orderItem.product}
          orderItem={props.orderItem}
          onCancel={closeModal}
          onConfirm={update}
          onRemove={remove}
          confirmText="Confirm"
        />
      </Modal>

      <Paper withBorder p="md">
        <Group justify="space-between" align="flex-start">
          <div>
            <Text fw={600}>
              {props.orderItem.quantity} x {props.orderItem.product.name}
            </Text>
            {props.orderItem.modifiers.map((m) => (
              <Text opacity={0.5}>{m.name}</Text>
            ))}
            <Text c="blue" mt="sm">
              {convertToMoney(props.orderItem.price)}€
            </Text>
          </div>

          <Button variant="light" onClick={openModal}>
            Edit
          </Button>
        </Group>
      </Paper>
    </>
  );
};
