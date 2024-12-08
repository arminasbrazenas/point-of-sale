import { Paper, Text, Button, Stack, Modal, ModalContent } from '@mantine/core';
import { useOrderPayments } from '../api/get-order-payments';
import { useDisclosure } from '@mantine/hooks';
import { useForm, zodResolver } from '@mantine/form';
import { CreatePaymentInput, createPaymentInputSchema, useCreateCashPayment } from '../api/create-cash-payment';
import { showNotification } from '@/lib/notifications';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { useCompleteOrderPayments } from '../api/complete-order-payments';

type OrderPaymentsProps = {
  orderId: number;
};

export const OrderPayments = (props: OrderPaymentsProps) => {
  const orderPaymentsQuery = useOrderPayments({ params: { orderId: props.orderId } });
  const [isCreateModelOpen, { open: openCreateModal, close: closeCreateModal }] = useDisclosure();

  const createCashPayment = useCreateCashPayment({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Cash payment created successfully.',
        });
        closeCreateModal();
      },
    },
  });

  const completeOrderPayments = useCompleteOrderPayments({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Order payments completed successfully.',
        });
      },
    },
  });

  const form = useForm<CreatePaymentInput>({
    mode: 'uncontrolled',
    initialValues: {
      orderId: props.orderId,
      paymentAmount: 0,
    },
    validate: zodResolver(createPaymentInputSchema),
  });

  const payByCash = (values: CreatePaymentInput) => {
    createCashPayment.mutate({ data: values });
  };

  const completePayments = () => {
    completeOrderPayments.mutate({ data: { orderId: props.orderId } });
  };

  const orderPayments = orderPaymentsQuery.data;
  if (orderPaymentsQuery.isLoading || !orderPayments) {
    return null;
  }

  return (
    <>
      <Modal opened={isCreateModelOpen} onClose={closeCreateModal} title="Create payment">
        <form onSubmit={form.onSubmit(payByCash)}>
          <Stack mt="md">
            <CurrencyInput
              label="Payment amount"
              placeholder="Payment amount"
              withAsterisk
              key={form.key('paymentAmount')}
              {...form.getInputProps('paymentAmount')}
            />
            <Button type="submit">Pay</Button>
          </Stack>
        </form>
      </Modal>

      <Paper withBorder p="md">
        <Text fw={600}>Order payments</Text>

        <Stack my="sm" gap="xs">
          {orderPayments.payments.map((p) => (
            <Paper key={p.id} withBorder p="sm">
              <Text size="sm">Method: {p.method}</Text>
              <Text size="sm">Amount: {p.amount}€</Text>
            </Paper>
          ))}
        </Stack>

        <Text ta="right" size="sm" fw={500}>
          Paid amount: {orderPayments.paidAmount}€
        </Text>
        <Text ta="right" size="sm" fw={500}>
          Unpaid amount: {orderPayments.unpaidAmount}€
        </Text>

        <Button fullWidth mt="xs" color="teal" variant="light" onClick={completePayments}>
          Complete payments
        </Button>
        <Button fullWidth mt="xs" onClick={openCreateModal}>
          Create payment
        </Button>
      </Paper>
    </>
  );
};
