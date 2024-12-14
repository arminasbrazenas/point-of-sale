import { PaymentIntent } from '@/types/api';
import { Button } from '@mantine/core';
import { PaymentElement, useElements, useStripe } from '@stripe/react-stripe-js';
import { useState } from 'react';

type StripeCheckoutProps = {
  paymentIntent: PaymentIntent;
  onSuccess: (paymentIntent: PaymentIntent) => void;
  onFailure: (error: string) => void;
};

export const StripeCheckout = (props: StripeCheckoutProps) => {
  const stripe = useStripe();
  const stripeElements = useElements();
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const handleSubmit = async (event: any) => {
    event.preventDefault();
    if (!stripeElements || !stripe) {
      return;
    }

    setIsLoading(true);

    try {
      const result = await stripe.confirmPayment({
        elements: stripeElements,
        confirmParams: {
          return_url: 'https://localhost:3000', // stripe should never redirect since we only use card method
        },
        redirect: 'if_required',
      });

      if (result.error) {
        props.onFailure(result.error.message ?? result.error.code ?? 'Unknown error');
      } else {
        props.onSuccess(props.paymentIntent);
      }
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <PaymentElement />
      <Button fullWidth mt="lg" type="submit" disabled={!stripe || !stripeElements} loading={isLoading}>
        Pay
      </Button>
    </form>
  );
};
