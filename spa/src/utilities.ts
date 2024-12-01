import { formatDate as formatDateFn } from 'date-fns';
import { PricingStrategy } from './types/api';

export const convertToMoney = (val: number): string => {
  return (Number(Math.floor(val * 100).toFixed(0)) / 100).toFixed(2);
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
