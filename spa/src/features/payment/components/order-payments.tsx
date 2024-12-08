import { Paper, Text, Button, Stack, Modal, Tabs, TextInput } from '@mantine/core';
import { useOrderPayments } from '../api/get-order-payments';
import { useDisclosure } from '@mantine/hooks';
import { useForm, zodResolver } from '@mantine/form';
import { PayByCashInput, payByCashInputSchema, usePayByCash } from '../api/pay-by-cash';
import { showNotification } from '@/lib/notifications';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { useCompleteOrderPayments } from '../api/complete-order-payments';
import { toReadablePaymentMethod } from '@/utilities';
import { PaymentMethod } from '@/types/api';
import { PayByGiftCardInput, payByGiftCardInputSchema, usePayByGiftCard } from '../api/pay-by-gift-card';

type OrderPaymentsProps = {
  orderId: number;
};

export const OrderPayments = (props: OrderPaymentsProps) => {
  const orderPaymentsQuery = useOrderPayments({ params: { orderId: props.orderId } });
  const [isCreateModelOpen, { open: openCreateModal, close: closeCreateModal }] = useDisclosure();

  const payByCashMutation = usePayByCash({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Successfully paid by cash.',
        });
        closeCreateModal();
      },
    },
  });

  const payByGiftCardMutation = usePayByGiftCard({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Successfully paid by gift card.',
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

  const payByCashForm = useForm<PayByCashInput>({
    mode: 'uncontrolled',
    initialValues: {
      orderId: props.orderId,
      paymentAmount: 0,
    },
    validate: zodResolver(payByCashInputSchema),
  });

  const payByGiftCardForm = useForm<PayByGiftCardInput>({
    mode: 'uncontrolled',
    initialValues: {
      orderId: props.orderId,
      giftCardCode: '',
    },
    validate: zodResolver(payByGiftCardInputSchema),
  });

  const payByCash = (values: PayByCashInput) => {
    payByCashMutation.mutate({ data: values });
  };

  const payByGiftCard = (values: PayByGiftCardInput) => {
    payByGiftCardMutation.mutate({ data: values });
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
        <Tabs mt="md" variant="pills" defaultValue={PaymentMethod.Cash}>
          <Tabs.List mb="md">
            <Tabs.Tab value={PaymentMethod.Cash}>{toReadablePaymentMethod(PaymentMethod.Cash)}</Tabs.Tab>
            <Tabs.Tab value={PaymentMethod.GiftCard}>{toReadablePaymentMethod(PaymentMethod.GiftCard)}</Tabs.Tab>
          </Tabs.List>

          <Tabs.Panel value={PaymentMethod.Cash}>
            <form onSubmit={payByCashForm.onSubmit(payByCash)}>
              <Stack>
                <CurrencyInput
                  label="Payment amount"
                  placeholder="Payment amount"
                  withAsterisk
                  key={payByCashForm.key('paymentAmount')}
                  {...payByCashForm.getInputProps('paymentAmount')}
                />
                <Button type="submit">Pay</Button>
              </Stack>
            </form>
          </Tabs.Panel>

          <Tabs.Panel value={PaymentMethod.GiftCard}>
            <form onSubmit={payByGiftCardForm.onSubmit(payByGiftCard)}>
              <Stack>
                <TextInput
                  label="Gift card code"
                  placeholder="Gift card code"
                  withAsterisk
                  key={payByGiftCardForm.key('giftCardCode')}
                  {...payByGiftCardForm.getInputProps('giftCardCode')}
                />
                <Button type="submit">Pay</Button>
              </Stack>
            </form>
          </Tabs.Panel>
        </Tabs>
      </Modal>

      <Paper withBorder p="md">
        <Text fw={600}>Order payments</Text>

        <Stack my="sm" gap="xs">
          {orderPayments.payments.map((p) => (
            <Paper key={p.id} withBorder p="sm">
              <Text size="sm">Method: {toReadablePaymentMethod(p.method)}</Text>
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
