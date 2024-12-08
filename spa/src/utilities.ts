import { formatDate as formatDateFn } from 'date-fns';
import { PaymentMethod, PricingStrategy } from './types/api';

export const toRoundedPrice = (price: number): number => {
  const factor = Math.pow(10, 2); // 2 decimal places
  return Math.round(price * factor + Math.sign(price) * 1e-10) / factor;
};

export const formatDate = (date: string): string => {
  return formatDateFn(new Date(date), 'yyyy-MM-dd HH:mm');
};

export const isSameNumberSet = (a: number[], b: number[]): boolean => {
  if (a.length != b.length) {
    return false;
  }

  const sortedA = a.sort((a, b) => a - b);
  const sortedB = b.sort((a, b) => a - b);
  for (let i = 0; i < sortedA.length; i++) {
    if (sortedA[i] != sortedB[i]) {
      return false;
    }
  }

  return true;
};

export const toReadablePricingStrategy = (s: PricingStrategy) => {
  switch (s) {
    case PricingStrategy.FixedAmount:
      return 'Fixed amount';
    case PricingStrategy.Percentage:
      return 'Percentage';
    default:
      return 'Unknown';
  }
};

export const toReadablePricingStrategyAmount = (value: number, s: PricingStrategy) => {
  switch (s) {
    case PricingStrategy.FixedAmount:
      return `${value}€`;
    case PricingStrategy.Percentage:
      return `${value}%`;
    default:
      return value;
  }
};

export const toReadablePaymentMethod = (method: PaymentMethod) => {
  switch (method) {
    case PaymentMethod.Cash:
      return 'Cash';
    case PaymentMethod.GiftCard:
      return 'Gift card';
    default:
      return method;
  }
};
