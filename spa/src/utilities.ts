import { formatDate as formatDateFn } from 'date-fns';

export const convertToMoney = (val: number): string => {
  return (Number(Math.floor(val * 100).toFixed(0)) / 100).toFixed(2);
};

export const formatDate = (date: string): string => {
  return formatDateFn(new Date(date), 'yyyy-MM-dd HH:mm');
};
