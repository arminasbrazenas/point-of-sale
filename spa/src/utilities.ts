export const convertToMoney = (val: number): string => {
  return (Number(Math.floor(val * 100).toFixed(0)) / 100).toFixed(2);
};
