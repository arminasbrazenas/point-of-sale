export enum OrderStatus {
  Open = 'Open',
  Completed = 'Completed',
  Closed = 'Closed',
  Canceled = 'Canceled',
  Refunded = 'Refunded',
}

export enum PricingStrategy {
  FixedAmount = 'FixedAmount',
  Percentage = 'Percentage',
}

export enum PaymentMethod {
  Cash = 'Cash',
  GiftCard = 'GiftCard',
  Card = 'Card',
}

export enum PaymentStatus {
  Pending = 'Pending',
  Canceled = 'Canceled',
  Succeeded = 'Succeeded',
}

export type EntityBase = {
  id: number;
  createdAt: string;
  modifiedAt: string;
};

export type Entity<T> = {
  [K in keyof T]: T[K];
} & EntityBase;

export type PaginationFilter = {
  page: number;
  itemsPerPage: number;
};

export type PagedResponse<T> = {
  page: number;
  itemsPerPage: number;
  totalItems: number;
  items: T[];
};

export type ErrorResponse = {
  errorMessage: string;
};

export type Product = Entity<{
  name: string;
  basePrice: number;
  priceDiscountExcluded?: number;
  price: number;
  stock: number;
  taxes: Tax[];
  modifiers: Modifier[];
}>;

export type Tax = Entity<{ name: string; rate: number }>;

export type OrderItemModifier = Entity<{ modifierId?: number; name: string; price: number }>;

export type OrderItem = Entity<{
  productId?: number;
  name: string;
  quantity: number;
  unitPrice: number;
  totalPrice: number;
  modifiers: OrderItemModifier[];
}>;

export type OrderServiceCharge = Entity<{
  name: string;
  amount: number;
  pricingStrategy: PricingStrategy;
  appliedAmount: number;
}>;

export type Order = Entity<{
  orderItems: OrderItem[];
  totalPrice: number;
  status: OrderStatus;
  serviceCharges: OrderServiceCharge[];
}>;

export type OrderReceipt = {
  totalPrice: number;
  orderItems: OrderItem[];
  taxTotal: number;
  serviceCharges: OrderServiceCharge[];
  serviceChargeTotal: number;
};

export type Modifier = Entity<{ name: string; priceTaxExcluded: number; price: number; stock: number }>;

export type ServiceCharge = Entity<{ name: string; pricingStrategy: PricingStrategy; amount: number }>;

export type Discount = Entity<{
  amount: number;
  pricingStrategy: PricingStrategy;
  validUntil: string;
  appliesToProductIds: number[];
}>;

export type Payment = Entity<{
  amount: number;
  method: PaymentMethod;
  status: PaymentStatus;
}>;

export type CashPayment = Payment;

export type GiftCardPayment = Payment & {
  giftCardCode: string;
};

export type OrderPayments = {
  payments: Payment[];
  paidAmount: number;
  unpaidAmount: number;
  totalAmount: number;
};

export type GiftCard = Entity<{
  code: string;
  amount: number;
  expiresAt: string;
  usedAt?: string;
}>;

export type Tip = Entity<{
  amount: number;
}>;

export type PaymentIntent = {
  clientSecret: string;
};
