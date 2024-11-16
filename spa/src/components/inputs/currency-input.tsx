import { NativeSelect, rem, TextInput, TextInputProps } from '@mantine/core';

const CURRENCIES = [{ value: 'eur', label: '🇪🇺 EUR' }];

type CurrencyInputProps = TextInputProps;

export const CurrencyInput = (props: CurrencyInputProps) => {
  const select = (
    <NativeSelect
      data={CURRENCIES}
      rightSectionWidth={28}
      variant={props.variant}
      styles={{
        input: {
          fontWeight: 500,
          borderTopLeftRadius: 0,
          borderBottomLeftRadius: 0,
          width: rem(92),
          marginRight: rem(-2),
        },
      }}
    />
  );

  return <TextInput type="number" rightSection={select} rightSectionWidth={92} {...props} />;
};
