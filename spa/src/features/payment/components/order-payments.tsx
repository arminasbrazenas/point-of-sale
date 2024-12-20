import { Paper, Text, Button, Stack, Modal, Tabs, TextInput, Divider, List, ListItem } from '@mantine/core';
import { useOrderPayments } from '../api/get-order-payments';
import { useDisclosure } from '@mantine/hooks';
import { useForm, zodResolver } from '@mantine/form';
import { PayByCashInput, payByCashInputSchema, usePayByCash } from '../api/pay-by-cash';
import { showNotification } from '@/lib/notifications';
import { CurrencyInput } from '@/components/inputs/currency-input';
import { useCompleteOrderPayments } from '../api/complete-order-payments';
import { toReadablePaymentMethod } from '@/utilities';
import { PaymentIntent, PaymentMethod } from '@/types/api';
import { PayByGiftCardInput, payByGiftCardInputSchema, usePayByGiftCard } from '../api/pay-by-gift-card';
import { AddTipInput, addTipInputSchema, useAddTip } from '../api/add-tip';
import { useOrderTips } from '../api/get-order-tips';
import {
  CreateCardPaymentIntentInput,
  createCardPaymentIntentSchema,
  useCreateCardPaymentIntent,
} from '../api/create-card-payment-intent';
import { useState } from 'react';
import { Elements } from '@stripe/react-stripe-js';
import { stripePromise } from '@/lib/stripe-client';
import { StripeCheckout } from './stripe-checkout';
import { useConfirmPaymentIntent } from '../api/confirm-payment-intent';
import { useAppStore } from '@/lib/app-store';
import { useRefundOrderPayments } from '../api/refund-order-payments';

type OrderPaymentsProps = {
  orderId: number;
};

export const OrderPayments = (props: OrderPaymentsProps) => {
  const orderPaymentsQuery = useOrderPayments({ params: { orderId: props.orderId } });
  const orderTipsQuery = useOrderTips({ params: { orderId: props.orderId } });
  const [isCreateModelOpen, { open: openCreateModal, close: closeCreateModal }] = useDisclosure();
  const [isTipModalOpen, { open: openTipModal, close: closeTipModal }] = useDisclosure();
  const [paymentIntent, setPaymentIntent] = useState<PaymentIntent | null>(null);

  const confirmPaymentIntent = useConfirmPaymentIntent();

  const userId = useAppStore((state) => {
    const applicationUser = state.applicationUser;

    if (!applicationUser || applicationUser.id === null) {
      throw new Error('Application user with id is required to get discounts.');
    }

    return applicationUser.id;
  });
  const businessId = useAppStore((state) => {
    const applicationUser = state.applicationUser;

    if (!applicationUser || applicationUser.businessId === null) {
      throw new Error('Application user with business is required to get discounts.');
    }

    return applicationUser.businessId;
  });

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

  const addTipMutation = useAddTip({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Tip added successfully.',
        });
        closeTipModal();
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

  const createCardPaymentIntentMutation = useCreateCardPaymentIntent({
    mutationConfig: {
      onSuccess: (result) => {
        showNotification({
          type: 'success',
          title: 'Card payment intent created successfully.',
        });
        setPaymentIntent(result);
      },
    },
  });

  const refundOrderPaymentsMutation = useRefundOrderPayments({
    mutationConfig: {
      onSuccess: () => {
        showNotification({
          type: 'success',
          title: 'Order payments refunded successfully.',
        });
      },
    },
  });

  const payByCashForm = useForm<PayByCashInput>({
    mode: 'uncontrolled',
    initialValues: {
      orderId: props.orderId,
      paymentAmount: 0,
      businessId: businessId,
      employeeId: userId,
    },
    validate: zodResolver(payByCashInputSchema),
  });

  const payByGiftCardForm = useForm<PayByGiftCardInput>({
    mode: 'uncontrolled',
    initialValues: {
      orderId: props.orderId,
      giftCardCode: '',
      businessId: businessId,
      employeeId: userId,
    },
    validate: zodResolver(payByGiftCardInputSchema),
  });

  const addTipForm = useForm<AddTipInput>({
    mode: 'uncontrolled',
    initialValues: {
      orderId: props.orderId,
      tipAmount: 0,
      employeeId: userId,
    },
    validate: zodResolver(addTipInputSchema),
  });

  const createCardPaymentIntentForm = useForm<CreateCardPaymentIntentInput>({
    mode: 'uncontrolled',
    initialValues: {
      orderId: props.orderId,
      paymentAmount: 0,
      tipAmount: 0,
      businessId: businessId,
      employeeId: userId,
    },
    validate: zodResolver(createCardPaymentIntentSchema),
  });

  const payByCash = (values: PayByCashInput) => {
    payByCashMutation.mutate({ data: values });
  };

  const payByGiftCard = (values: PayByGiftCardInput) => {
    payByGiftCardMutation.mutate({ data: values });
  };

  const createCardPaymentIntent = (values: CreateCardPaymentIntentInput) => {
    createCardPaymentIntentMutation.mutate({ data: values });
  };

  const addTip = (values: AddTipInput) => {
    addTipMutation.mutate({ data: values });
  };

  const completePayments = () => {
    completeOrderPayments.mutate({ data: { orderId: props.orderId } });
  };

  const onCardPaymentSuccess = (paymentIntent: PaymentIntent) => {
    showNotification({
      type: 'success',
      title: 'Card payment succeeded.',
    });
    confirmPaymentIntent.mutate({ paymentIntentId: paymentIntent.paymentIntentId });
    setPaymentIntent(null);
    closeCreateModal();
  };

  const refundOrder = () => {
    refundOrderPaymentsMutation.mutate({ data: { orderId: props.orderId } });
  };

  const onCardPaymentFailure = (error: string) => {
    showNotification({
      type: 'failure',
      title: error,
    });
  };

  const orderPayments = orderPaymentsQuery.data;
  const orderTips = orderTipsQuery.data;
  if (orderPaymentsQuery.isLoading || !orderPayments || orderTipsQuery.isLoading || !orderTips) {
    return null;
  }

  return (
    <>
      <Modal opened={isCreateModelOpen} onClose={closeCreateModal} title="Create payment">
        <Tabs mt="md" variant="pills" defaultValue={PaymentMethod.Cash}>
          <Tabs.List mb="md">
            <Tabs.Tab value={PaymentMethod.Cash}>{toReadablePaymentMethod(PaymentMethod.Cash)}</Tabs.Tab>
            <Tabs.Tab value={PaymentMethod.GiftCard}>{toReadablePaymentMethod(PaymentMethod.GiftCard)}</Tabs.Tab>
            <Tabs.Tab value={PaymentMethod.Card}>{toReadablePaymentMethod(PaymentMethod.Card)}</Tabs.Tab>
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

          <Tabs.Panel value={PaymentMethod.Card}>
            {paymentIntent ? (
              <Elements stripe={stripePromise} options={{ clientSecret: paymentIntent.clientSecret }}>
                <StripeCheckout
                  onSuccess={onCardPaymentSuccess}
                  onFailure={onCardPaymentFailure}
                  paymentIntent={paymentIntent}
                />
              </Elements>
            ) : (
              <form onSubmit={createCardPaymentIntentForm.onSubmit(createCardPaymentIntent)}>
                <Stack>
                  <CurrencyInput
                    label="Payment amount"
                    placeholder="Payment amount"
                    withAsterisk
                    key={createCardPaymentIntentForm.key('paymentAmount')}
                    {...createCardPaymentIntentForm.getInputProps('paymentAmount')}
                  />
                  <CurrencyInput
                    label="Tip amount"
                    placeholder="Tip amount"
                    withAsterisk
                    key={createCardPaymentIntentForm.key('tipAmount')}
                    {...createCardPaymentIntentForm.getInputProps('tipAmount')}
                  />
                  <Button type="submit" loading={createCardPaymentIntentMutation.isPending}>
                    Begin payment
                  </Button>
                </Stack>
              </form>
            )}
          </Tabs.Panel>
        </Tabs>
      </Modal>

      <Modal opened={isTipModalOpen} onClose={closeTipModal} title="Add tip">
        <form onSubmit={addTipForm.onSubmit(addTip)}>
          <Stack mt="md">
            <CurrencyInput
              label="Tip amount"
              placeholder="Tip amount"
              withAsterisk
              key={addTipForm.key('tipAmount')}
              {...addTipForm.getInputProps('tipAmount')}
            />
            <Button type="submit">Tip</Button>
          </Stack>
        </form>
      </Modal>

      <Paper withBorder p="md">
        <Text fw={600}>Order payments</Text>

        <Stack my="sm" gap="xs">
          {orderPayments.payments.map((p) => (
            <Paper key={p.id} withBorder p="sm">
              <Text size="sm">Method: {toReadablePaymentMethod(p.method)}</Text>
              <Text size="sm">Amount: {p.amount}€</Text>
              <Text size="sm">Status: {p.status}</Text>
            </Paper>
          ))}
        </Stack>

        <Text ta="right" size="sm" fw={500}>
          Total amount: {orderPayments.totalAmount}€
        </Text>
        <Text ta="right" size="sm" fw={500}>
          Paid amount: {orderPayments.paidAmount}€
        </Text>
        <Text ta="right" size="sm" fw={500}>
          Unpaid amount: {orderPayments.unpaidAmount}€
        </Text>

        <Button fullWidth mt="xs" onClick={openCreateModal}>
          Create payment
        </Button>

        <Text fw={600} mt="sm">
          Order tips
        </Text>

        <List>
          {orderTips.map((tip) => (
            <ListItem opacity={0.75}>{tip.amount}€</ListItem>
          ))}
        </List>

        <Button fullWidth mt="xs" onClick={openTipModal}>
          Add cash tip
        </Button>

        <Divider my="md" />

        <Stack gap="xs">
          <Button color="teal" variant="light" onClick={completePayments}>
            Complete payments
          </Button>
          <Button color="yellow" variant="light" onClick={refundOrder}>
            Refund all payments
          </Button>
        </Stack>
      </Paper>
    </>
  );
};
